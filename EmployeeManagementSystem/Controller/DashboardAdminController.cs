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
                // Tính tổng số ca làm việc (2 ca/ngày x số ngày làm việc)
                var totalWorkingDays = CalculateWorkingDays(fromDate.Date, toDate.Date);
                var totalShifts = totalWorkingDays * 2; // 2 ca mỗi ngày

                if (totalShifts <= 0)
                {
                    return (0, 0, 0, "Không có ca làm việc trong khoảng thời gian này.");
                }

                // Lấy danh sách nhân viên
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

                // Lấy bản ghi chấm công
                var attendanceRecords = _context.Attendances
                    .Where(a => a.Date >= fromDate.Date && a.Date <= toDate.Date && employeeIds.Contains(a.UserId))
                    .ToList();

                var onTimeShifts = attendanceRecords.Count(a => a.Status == "Đúng giờ");
                var lateShifts = attendanceRecords.Count(a => a.Status == "Đi muộn" || a.Status == "Late");
                var earlyLeaveShifts = attendanceRecords.Count(a => a.Status == "Về sớm");
                var absentShifts = attendanceRecords.Count(a => a.Status == "Absent" || a.Status == "Vắng mặt");

                // Tính ca nghỉ phép được duyệt
                var approvedLeaveRequests = _context.LeaveRequests
                    .Where(lr => employeeIds.Contains(lr.UserId) && lr.Status == "Approved")
                    .Where(lr => lr.StartDate.Date <= toDate.Date && lr.EndDate.Date >= fromDate.Date)
                    .ToList();

                var approvedLeaveShifts = 0;
                var approvedLeaveDates = new HashSet<DateTime>();
                foreach (var lr in approvedLeaveRequests)
                {
                    var startDate = lr.StartDate.Date > fromDate.Date ? lr.StartDate.Date : fromDate.Date;
                    var endDate = lr.EndDate.Date < toDate.Date ? lr.EndDate.Date : toDate.Date;

                    for (var date = startDate; date <= endDate; date = date.AddDays(1))
                    {
          
                            approvedLeaveDates.Add(date);               
                    }
                }
                approvedLeaveShifts = approvedLeaveDates.Count * 2; // 2 ca mỗi ngày

                // Tính ca nghỉ phép không được duyệt
                var ineffectiveLeaveRequests = _context.LeaveRequests
                    .Where(lr => employeeIds.Contains(lr.UserId) && (lr.Status == "Pending" || lr.Status == "Rejected"))
                    .Where(lr => lr.StartDate.Date <= toDate.Date && lr.EndDate.Date >= fromDate.Date)
                    .ToList();

                var ineffectiveLeaveShifts = 0;
                var ineffectiveLeaveDates = new HashSet<DateTime>();
                foreach (var lr in ineffectiveLeaveRequests)
                {
                    var startDate = lr.StartDate.Date > fromDate.Date ? lr.StartDate.Date : fromDate.Date;
                    var endDate = lr.EndDate.Date < toDate.Date ? lr.EndDate.Date : toDate.Date;

                    for (var date = startDate; date <= endDate; date = date.AddDays(1))
                    {
                        if (!approvedLeaveDates.Contains(date))
                        {
                            ineffectiveLeaveDates.Add(date);
                        }
                    }
                }
                ineffectiveLeaveShifts = ineffectiveLeaveDates.Count * 2;

                // Tính ca không có bản ghi
                var totalRecordedShifts = attendanceRecords.Count;
                var unrecordedShifts = Math.Max(0, totalShifts - totalRecordedShifts - approvedLeaveShifts);

                // Tính phần trăm
                double effectiveTimePercentage = (onTimeShifts / (double)totalShifts) * 100;
                double approvedLeavePercentage = (approvedLeaveShifts / (double)totalShifts) * 100;
                double ineffectiveTimePercentage = ((lateShifts + earlyLeaveShifts + absentShifts + ineffectiveLeaveShifts + unrecordedShifts) / (double)totalShifts) * 100;

                // Tạo thông tin chi tiết
                var detailMessage = $"Tổng số ca làm việc: {totalShifts} ca ({totalWorkingDays} ngày x 2 ca)\n" +
                                   $"Ca đúng giờ: {onTimeShifts} ca\n" +
                                   $"Ca đi muộn: {lateShifts} ca\n" +
                                   $"Ca về sớm: {earlyLeaveShifts} ca\n" +
                                   $"Ca vắng mặt: {absentShifts} ca\n" +
                                   $"Ca nghỉ phép được duyệt: {approvedLeaveShifts} ca\n" +
                                   $"Ca nghỉ phép không được duyệt/chờ: {ineffectiveLeaveShifts} ca\n" +
                                   $"Ca không có bản ghi chấm công: {unrecordedShifts} ca";

                // Đảm bảo tổng không vượt quá 100%
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

        // Tính toán hiệu suất của tất cả phòng ban để so sánh
        public List<(int DepartmentId, string DepartmentName, double EffectiveTimePercentage, double ApprovedLeavePercentage, double IneffectiveTimePercentage)> CalculateAllDepartmentsPerformance(DateTime fromDate, DateTime toDate)
        {
            var result = new List<(int, string, double, double, double)>();
            var departments = _context.Departments.AsNoTracking().Select(d => new { d.DepartmentId, d.Name }).ToList();

            foreach (var dept in departments)
            {
                var (effective, approved, ineffective, _) = CalculatePerformanceDistribution(fromDate, toDate, dept.DepartmentId);
                result.Add((dept.DepartmentId, dept.Name, effective, approved, ineffective));
            }

            return result;
        }

        private int CalculateWorkingDays(DateTime startDate, DateTime endDate)
        {
            int workingDays = 0;
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
        
                    workingDays++;             
            }
            return workingDays;
        }
    }
}
