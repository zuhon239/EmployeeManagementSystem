using EmployeeManagementSystem.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Controller
{
    public class DashboardManagerController
    {
        private readonly EmployeeManagementContext _context;
        private readonly int _currentManagerId;
        public DashboardManagerController(EmployeeManagementContext context, int currentManagerId)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _currentManagerId = currentManagerId;
        }

        public (string[] EmployeeNames, int[] EmployeeIds) GetEmployeesInManagedDepartment()
        {
            var department = _context.Users
               .OfType<Employee>()
               .AsNoTracking()
               .Include(e => e.Department)
               .Select(e => new
               {
                   e.UserId,
                   e.DepartmentId,
                   e.Department.Name
               })
               .FirstOrDefault(e => e.UserId == _currentManagerId);

            if (department == null || department.DepartmentId == 0)
            {
                return (new string[0], new int[0]);
            }

            var employees = _context.Employees
                .Where(e => e.DepartmentId == department.DepartmentId)
                .Select(e => new { e.UserId, e.Name }) // Giả sử có cột Name
                .ToArray();

            return (employees.Select(e => e.Name).ToArray(), employees.Select(e => e.UserId).ToArray());
        }

        public (double EffectiveTimePercentage, double ApprovedLeavePercentage,
             double IneffectiveTimePercentage, string DetailMessage) CalculatePerformanceDistribution(DateTime fromDate, DateTime toDate, int userId)
        {
            try
            {
                // Tính tổng số ngày làm việc trong khoảng thời gian (loại trừ weekend nếu cần)
                var totalWorkingDays = CalculateWorkingDays(fromDate.Date, toDate.Date);

                if (totalWorkingDays <= 0)
                {
                    return (0, 0, 0, "Không có ngày làm việc trong khoảng thời gian này.");
                }

                // Kiểm tra nhân viên có tồn tại không
                var employeeExists = _context.Employees.Any(e => e.UserId == userId);
                if (!employeeExists)
                {
                    return (0, 0, 0, "Nhân viên không tồn tại.");
                }

                // Đếm số ngày có bản ghi chấm công
                var attendanceRecords = _context.Attendances
                    .Where(a => a.Date >= fromDate.Date && a.Date <= toDate.Date && a.UserId == userId)
                    .ToList();

                var attendanceDates = attendanceRecords.Select(a => a.Date.Date).Distinct().ToList();
                var totalAttendanceDays = attendanceDates.Count;

                // Đếm số ngày chấm công đúng giờ
                var onTimeDays = attendanceRecords.Count(a => a.Status == "Đúng giờ");

                // Đếm số ngày đi muộn
                var lateDays = attendanceRecords.Count(a => a.Status == "Đi muộn" || a.Status == "Late");

                // Đếm số ngày về sớm
                var earlyLeaveDays = attendanceRecords.Count(a => a.Status == "Về sớm");

                // Đếm số ngày vắng mặt (có record nhưng absent)
                var absentWithRecordDays = attendanceRecords.Count(a => a.Status == "Absent" || a.Status == "Vắng mặt");

                // Tính số ngày nghỉ phép được duyệt
                var approvedLeaveRequests = _context.LeaveRequests
                    .Where(lr => lr.UserId == userId)
                    .Where(lr => lr.Status == "Approved")
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

                // Tính số ngày nghỉ phép không được duyệt hoặc đang chờ
                var ineffectiveLeaveRequests = _context.LeaveRequests
                    .Where(lr => lr.UserId == userId)
                    .Where(lr => lr.Status == "Pending" || lr.Status == "Rejected")
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

                // Tính số ngày không có bản ghi chấm công và không có nghỉ phép
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

                // Tính phần trăm
                double effectiveTimePercentage = (onTimeDays / (double)totalWorkingDays) * 100;
                double approvedLeavePercentage = (approvedLeaveDays / (double)totalWorkingDays) * 100;
                double ineffectiveTimePercentage = ((lateDays + earlyLeaveDays + absentWithRecordDays + ineffectiveLeaveDays + unrecordedDays) / (double)totalWorkingDays) * 100;

                // Tạo thông tin chi tiết
                var detailMessage = $"Tổng ngày làm việc: {totalWorkingDays}\n" +
                                  $"Đúng giờ: {onTimeDays} ngày\n" +
                                  $"Đi muộn: {lateDays} ngày\n" +
                                  $"Về sớm: {earlyLeaveDays} ngày\n" +
                                  $"Vắng mặt (có record): {absentWithRecordDays} ngày\n" +
                                  $"Nghỉ phép được duyệt: {approvedLeaveDays} ngày\n" +
                                  $"Nghỉ phép không được duyệt/chờ: {ineffectiveLeaveDays} ngày\n" +
                                  $"Không có bản ghi chấm công: {unrecordedDays} ngày";

                // Đảm bảo tổng không vượt quá 100%
                double totalPercentage = effectiveTimePercentage + approvedLeavePercentage + ineffectiveTimePercentage;
                if (totalPercentage > 100)
                {
                    effectiveTimePercentage = (effectiveTimePercentage / totalPercentage) * 100;
                    approvedLeavePercentage = (approvedLeavePercentage / totalPercentage) * 100;
                    ineffectiveTimePercentage = (ineffectiveTimePercentage / totalPercentage) * 100;
                }

                // Đảm bảo các giá trị không âm
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
            // Loại trừ thứ 7 và Chủ nhật (có thể tùy chỉnh theo quy định công ty)
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }
    }
}
