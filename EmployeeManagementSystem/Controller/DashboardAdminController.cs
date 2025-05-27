using EmployeeManagementSystem.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Controller
{
    public class DashboardAdminController
    {
        private readonly EmployeeManagementContext _context;

        public DashboardAdminController(EmployeeManagementContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Get list of departments for the combobox, including "Total" for company-wide data
        public (string[] DepartmentNames, int[] DepartmentIds) GetDepartments()
        {
            var departments = _context.Departments
                .AsNoTracking()
                .Select(d => new { d.DepartmentId, d.Name })
                .ToArray();

            // Add "Total" as an option for company-wide data
            var departmentNames = new[] { "Total" }.Concat(departments.Select(d => d.Name)).ToArray();
            var departmentIds = new[] { 0 }.Concat(departments.Select(d => d.DepartmentId)).ToArray();

            return (departmentNames, departmentIds);
        }

        // Calculate performance distribution for a department or entire company
        public (double EffectiveTimePercentage, double ApprovedLeavePercentage,
                double IneffectiveTimePercentage, string DetailMessage) CalculatePerformanceDistribution(
                DateTime fromDate, DateTime toDate, int? departmentId)
        {
            try
            {
                // Calculate total working days (excluding weekends)
                var totalWorkingDays = CalculateWorkingDays(fromDate.Date, toDate.Date);

                if (totalWorkingDays <= 0)
                {
                    return (0, 0, 0, "Không có ngày làm việc trong khoảng thời gian này.");
                }

                // Get employees in the specified department (or all if departmentId is null/0)
                var employeesQuery = _context.Employees.AsNoTracking();
                if (departmentId.HasValue && departmentId.Value != 0)
                {
                    employeesQuery = employeesQuery.Where(e => e.DepartmentId == departmentId.Value);
                }

                var employeeIds = employeesQuery.Select(e => e.UserId).ToList();
                if (!employeeIds.Any())
                {
                    return (0, 0, 0, "Không có nhân viên trong phòng ban này.");
                }

                // Count attendance records
                var attendanceRecords = _context.Attendances
                    .Where(a => a.Date >= fromDate.Date && a.Date <= toDate.Date && employeeIds.Contains(a.UserId))
                    .ToList();

                var attendanceDates = attendanceRecords.Select(a => a.Date.Date).Distinct().ToList();
                var totalAttendanceDays = attendanceDates.Count;

                var onTimeDays = attendanceRecords.Count(a => a.Status == "Đúng giờ");
                var lateDays = attendanceRecords.Count(a => a.Status == "Đi muộn" || a.Status == "Late");
                var earlyLeaveDays = attendanceRecords.Count(a => a.Status == "Về sớm");
                var absentWithRecordDays = attendanceRecords.Count(a => a.Status == "Absent" || a.Status == "Vắng mặt");

                // Calculate approved leave days
                var approvedLeaveRequests = _context.LeaveRequests
                    .Where(lr => employeeIds.Contains(lr.UserId) && lr.Status == "Approved")
                    .Where(lr => lr.StartDate.Date <= toDate.Date && lr.EndDate.Date >= fromDate.Date)
                    .ToList();

                var approvedLeaveDays = 0;
                var approvedLeaveDates = new HashSet<DateTime>();
                foreach (var lr in approvedLeaveRequests)
                {
                    var startDate = lr.StartDate.Date > fromDate.Date ? lr.StartDate.Date : fromDate.Date;
                    var endDate = lr.EndDate.Date < toDate.Date ? lr.EndDate.Date : toDate.Date;

                    for (var date = startDate; date <= endDate; date = date.AddDays(1))
                    {
                        if (IsWorkingDay(date))
                        {
                            approvedLeaveDates.Add(date);
                        }
                    }
                }
                approvedLeaveDays = approvedLeaveDates.Count;

                // Calculate ineffective leave days
                var ineffectiveLeaveRequests = _context.LeaveRequests
                    .Where(lr => employeeIds.Contains(lr.UserId) && (lr.Status == "Pending" || lr.Status == "Rejected"))
                    .Where(lr => lr.StartDate.Date <= toDate.Date && lr.EndDate.Date >= fromDate.Date)
                    .ToList();

                var ineffectiveLeaveDays = 0;
                var ineffectiveLeaveDates = new HashSet<DateTime>();
                foreach (var lr in ineffectiveLeaveRequests)
                {
                    var startDate = lr.StartDate.Date > fromDate.Date ? lr.StartDate.Date : fromDate.Date;
                    var endDate = lr.EndDate.Date < toDate.Date ? lr.EndDate.Date : toDate.Date;

                    for (var date = startDate; date <= endDate; date = date.AddDays(1))
                    {
                        if (IsWorkingDay(date) && !approvedLeaveDates.Contains(date))
                        {
                            ineffectiveLeaveDates.Add(date);
                        }
                    }
                }
                ineffectiveLeaveDays = ineffectiveLeaveDates.Count;

                // Calculate unrecorded days
                var allWorkingDates = new HashSet<DateTime>();
                for (var date = fromDate.Date; date <= toDate.Date; date = date.AddDays(1))
                {
                    if (IsWorkingDay(date))
                    {
                        allWorkingDates.Add(date);
                    }
                }

                var unrecordedDays = allWorkingDates
                    .Where(date => !attendanceDates.Contains(date) &&
                                  !approvedLeaveDates.Contains(date) &&
                                  !ineffectiveLeaveDates.Contains(date))
                    .Count();

                // Calculate percentages
                double effectiveTimePercentage = (onTimeDays / (double)totalWorkingDays) * 100;
                double approvedLeavePercentage = (approvedLeaveDays / (double)totalWorkingDays) * 100;
                double ineffectiveTimePercentage = ((lateDays + earlyLeaveDays + absentWithRecordDays + ineffectiveLeaveDays + unrecordedDays) / (double)totalWorkingDays) * 100;

                // Create detailed message
                var detailMessage = $"Tổng ngày làm việc: {totalWorkingDays}\n" +
                                   $"Đúng giờ: {onTimeDays} ngày\n" +
                                   $"Đi muộn: {lateDays} ngày\n" +
                                   $"Về sớm: {earlyLeaveDays} ngày\n" +
                                   $"Vắng mặt (có record): {absentWithRecordDays} ngày\n" +
                                   $"Nghỉ phép được duyệt: {approvedLeaveDays} ngày\n" +
                                   $"Nghỉ phép không được duyệt/chờ: {ineffectiveLeaveDays} ngày\n" +
                                   $"Không có bản ghi chấm công: {unrecordedDays} ngày";

                // Ensure total does not exceed 100%
                double totalPercentage = effectiveTimePercentage + approvedLeavePercentage + ineffectiveTimePercentage;
                if (totalPercentage > 100)
                {
                    effectiveTimePercentage = (effectiveTimePercentage / totalPercentage) * 100;
                    approvedLeavePercentage = (approvedLeavePercentage / totalPercentage) * 100;
                    ineffectiveTimePercentage = (ineffectiveTimePercentage / totalPercentage) * 100;
                }

                effectiveTimePercentage = Math.Max(0, effectiveTimePercentage);
                approvedLeavePercentage = Math.Max(0, approvedLeavePercentage);
                ineffectiveTimePercentage = Math.Max(0, ineffectiveTimePercentage);

                return (effectiveTimePercentage, approvedLeavePercentage, ineffectiveTimePercentage, detailMessage);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in CalculatePerformanceDistribution: {ex.Message}");
                return (0, 0, 0, $"Lỗi khi tính toán: {ex.Message}");
            }
        }

        private int CalculateWorkingDays(DateTime startDate, DateTime endDate)
        {
            int workingDays = 0;
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (IsWorkingDay(date))
                {
                    workingDays++;
                }
            }
            return workingDays;
        }

        private bool IsWorkingDay(DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }
    }
}
