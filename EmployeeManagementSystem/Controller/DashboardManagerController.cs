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
                // Tính tổng số ca làm việc trong khoảng thời gian (2 ca/ngày x số ngày làm việc)
                var totalWorkingDays = CalculateWorkingDays(fromDate.Date, toDate.Date);
                var totalShifts = totalWorkingDays * 2; // 2 ca mỗi ngày

                if (totalShifts <= 0)
                {
                    return (0, 0, 0, "Không có ca làm việc trong khoảng thời gian này.");
                }

                // Kiểm tra nhân viên có tồn tại không
                var employeeExists = _context.Employees.Any(e => e.UserId == userId);
                if (!employeeExists)
                {
                    return (0, 0, 0, "Nhân viên không tồn tại.");
                }

                // Lấy tất cả các bản ghi chấm công theo ca
                var attendanceRecords = _context.Attendances
                    .Where(a => a.Date >= fromDate.Date && a.Date <= toDate.Date && a.UserId == userId)
                    .ToList();

                // Đếm số ca theo từng trạng thái
                var onTimeShifts = attendanceRecords.Count(a => a.Status == "Đúng giờ");
                var lateShifts = attendanceRecords.Count(a => a.Status == "Đi muộn" || a.Status == "Late");
                var earlyLeaveShifts = attendanceRecords.Count(a => a.Status == "Về sớm");
                var absentShifts = attendanceRecords.Count(a => a.Status == "Absent" || a.Status == "Vắng mặt");

                // Tính số ca nghỉ phép được duyệt
                var approvedLeaveRequests = _context.LeaveRequests
                    .Where(lr => lr.UserId == userId)
                    .Where(lr => lr.Status == "Đã duyệt")
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
                approvedLeaveShifts = approvedLeaveDates.Count * 2; // 2 ca mỗi ngày nghỉ phép

                // Tính số ca nghỉ phép không được duyệt hoặc đang chờ
                var ineffectiveLeaveRequests = _context.LeaveRequests
                    .Where(lr => lr.UserId == userId)
                    .Where(lr => lr.Status == "Chưa duyệt" || lr.Status == "Từ chối")
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
                ineffectiveLeaveShifts = ineffectiveLeaveDates.Count * 2; // 2 ca mỗi ngày

                // Tính số ca không có bản ghi chấm công
                var totalRecordedShifts = attendanceRecords.Count;
                var expectedShiftsFromWorkingDays = totalWorkingDays * 2;
                var shiftsFromApprovedLeave = approvedLeaveShifts;
                var shiftsFromIneffectiveLeave = ineffectiveLeaveShifts;

                // Ca không có record = Tổng ca dự kiến - Ca có record - Ca nghỉ phép
                var unrecordedShifts = Math.Max(0, expectedShiftsFromWorkingDays - totalRecordedShifts - shiftsFromApprovedLeave);

                // Tính phần trăm dựa trên tổng số ca
                double effectiveTimePercentage = (onTimeShifts / (double)totalShifts) * 100;
                double approvedLeavePercentage = (approvedLeaveShifts / (double)totalShifts) * 100;

                // Thời gian không hiệu quả = Đi muộn + Về sớm + Vắng mặt + Nghỉ không phép + Không có record
                var ineffectiveShifts = lateShifts + earlyLeaveShifts + absentShifts + ineffectiveLeaveShifts + unrecordedShifts;
                double ineffectiveTimePercentage = (ineffectiveShifts / (double)totalShifts) * 100;

                // Tạo thông tin chi tiết
                var detailMessage = $"Tổng số ca làm việc: {totalShifts} ca ({totalWorkingDays} ngày x 2 ca)\n" +
                                  $"Ca đúng giờ: {onTimeShifts} ca\n" +
                                  $"Ca đi muộn: {lateShifts} ca\n" +
                                  $"Ca về sớm: {earlyLeaveShifts} ca\n" +
                                  $"Ca vắng mặt: {absentShifts} ca\n" +
                                  $"Ca nghỉ phép được duyệt: {approvedLeaveShifts} ca\n" +
                                  $"Ca nghỉ phép không được duyệt/chờ: {ineffectiveLeaveShifts} ca\n" +
                                  $"Ca không có bản ghi chấm công: {unrecordedShifts} ca\n" +
                                  $"Tổng ca có bản ghi: {totalRecordedShifts} ca";

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
                
                    workingDays++;
              
            }
            return workingDays;
        }
        public DateTime GetDominantMonth(DateTime fromDate, DateTime toDate)
        {
            var monthCounts = new Dictionary<(int Year, int Month), int>();
            var currentDate = fromDate.Date;
            while (currentDate <= toDate.Date)
            {
                var key = (currentDate.Year, currentDate.Month);
                monthCounts[key] = monthCounts.GetValueOrDefault(key) + 1;
                currentDate = currentDate.AddDays(1);
            }

            var dominant = monthCounts.OrderByDescending(m => m.Value).FirstOrDefault().Key;
            return new DateTime(dominant.Year, dominant.Month, 1);
        }



    }
}
