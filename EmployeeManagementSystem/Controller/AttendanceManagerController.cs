using EmployeeManagementSystem.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Controller
{
    public class AttendanceManagerController
    {
        private readonly EmployeeManagementContext _context;

        public AttendanceManagerController(EmployeeManagementContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // ✅ Lấy thông tin Manager
        public async Task<Employee> GetManagerInfoAsync(int managerId)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.UserId == managerId &&
                                       e.RoleId == 2 &&
                                       e.Status == true);
        }

        // ✅ Quy trình truy xuất với filter theo ca và status
        public async Task<List<DetailedAttendanceReportViewModel>> GetDetailedAttendanceReportAsync(
            int managerId, 
            DateTime month, 
            string shiftFilter = "All", 
            string statusFilter = "All")
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"=== BẮT ĐẦU QUY TRÌNH TRUY XUẤT VỚI FILTER ===");
                System.Diagnostics.Debug.WriteLine($"Manager ID: {managerId}, Shift: {shiftFilter}, Status: {statusFilter}");

                // Bước 1: Lấy thông tin Manager
                var manager = await GetManagerInfoAsync(managerId);
                if (manager == null)
                {
                    System.Diagnostics.Debug.WriteLine($"❌ Không tìm thấy Manager với ID: {managerId}");
                    return new List<DetailedAttendanceReportViewModel>();
                }

                var startDate = new DateTime(month.Year, month.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                // Bước 2: Lấy nhân viên trong phòng ban (không bao gồm Manager)
                var employees = await _context.Employees
                    .Where(e => e.DepartmentId == manager.DepartmentId &&
                               e.Status == true &&
                               e.RoleId == 1 &&
                               e.UserId != managerId)
                    .OrderBy(e => e.Name)
                    .ToListAsync();

                System.Diagnostics.Debug.WriteLine($"✅ Tìm thấy {employees.Count} nhân viên");

                // Bước 3: Lấy attendance records với filter
                var attendanceQuery = _context.Attendances
                    .Where(a => employees.Select(e => e.UserId).Contains(a.UserId) &&
                               a.Date >= startDate && a.Date <= endDate);

                // ✅ Filter theo ca
                if (shiftFilter != "All")
                {
                    attendanceQuery = attendanceQuery.Where(a => a.Shift == shiftFilter);
                }

                // ✅ Filter theo status
                if (statusFilter != "All")
                {
                    attendanceQuery = attendanceQuery.Where(a => a.Status.Contains(statusFilter));
                }

                var attendances = await attendanceQuery.ToListAsync();

                System.Diagnostics.Debug.WriteLine($"✅ Tìm thấy {attendances.Count} records sau filter");

                var result = new List<DetailedAttendanceReportViewModel>();

                foreach (var employee in employees)
                {
                    var employeeAttendances = attendances
                        .Where(a => a.UserId == employee.UserId)
                        .OrderBy(a => a.Date)
                        .ThenBy(a => a.Shift)
                        .ToList();

                    // ✅ Tạo record cho từng attendance thay vì gộp theo ngày
                    foreach (var attendance in employeeAttendances)
                    {
                        var reportItem = new DetailedAttendanceReportViewModel
                        {
                            UserId = employee.UserId,
                            EmployeeName = employee.Name,
                            Position = employee.Position,
                            Phone = employee.Phone ?? "N/A",
                            Date = attendance.Date,
                            Shift = attendance.Shift,
                            ClockIn = attendance.ClockIn,
                            ClockOut = attendance.ClockOut,
                            Status = attendance.Status,
                            ShortStatus = GetShortStatus(attendance.Status)
                        };

                        result.Add(reportItem);
                    }
                }

                System.Diagnostics.Debug.WriteLine($"✅ Tổng cộng {result.Count} records trong báo cáo chi tiết");
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi GetDetailedAttendanceReportAsync: {ex.Message}");
                return new List<DetailedAttendanceReportViewModel>();
            }
        }

        // ✅ Lấy báo cáo tháng với cột động (giữ nguyên cho tổng quan)
        public async Task<List<MonthlyAttendanceReportViewModel>> GetMonthlyAttendanceReportAsync(int managerId, DateTime month)
        {
            try
            {
                var manager = await GetManagerInfoAsync(managerId);
                if (manager == null)
                {
                    return new List<MonthlyAttendanceReportViewModel>();
                }

                var startDate = new DateTime(month.Year, month.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                var employees = await _context.Employees
                    .Where(e => e.DepartmentId == manager.DepartmentId &&
                               e.Status == true &&
                               e.RoleId == 1 &&
                               e.UserId != managerId)
                    .OrderBy(e => e.Name)
                    .ToListAsync();

                var attendances = await _context.Attendances
                    .Where(a => employees.Select(e => e.UserId).Contains(a.UserId) &&
                               a.Date >= startDate && a.Date <= endDate)
                    .ToListAsync();

                var result = new List<MonthlyAttendanceReportViewModel>();

                foreach (var employee in employees)
                {
                    var employeeAttendances = attendances
                        .Where(a => a.UserId == employee.UserId)
                        .ToList();

                    var reportItem = new MonthlyAttendanceReportViewModel
                    {
                        UserId = employee.UserId,
                        EmployeeName = employee.Name,
                        Position = employee.Position,
                        Phone = employee.Phone ?? "N/A",
                        DailyAttendances = new Dictionary<int, string>()
                    };

                    // Tạo dictionary cho từng ngày trong tháng
                    for (int day = 1; day <= DateTime.DaysInMonth(month.Year, month.Month); day++)
                    {
                        var dayAttendances = employeeAttendances
                            .Where(a => a.Date.Day == day)
                            .ToList();

                        if (dayAttendances.Any())
                        {
                            // ✅ Hiển thị cả 2 ca: S=Sáng, C=Chiều
                            var morningAttendance = dayAttendances.FirstOrDefault(a => a.Shift == "Sáng");
                            var afternoonAttendance = dayAttendances.FirstOrDefault(a => a.Shift == "Chiều");

                            var statusText = "";
                            if (morningAttendance != null)
                            {
                                statusText += $"S:{GetShortStatus(morningAttendance.Status)}";
                            }
                            if (afternoonAttendance != null)
                            {
                                if (!string.IsNullOrEmpty(statusText)) statusText += "/";
                                statusText += $"C:{GetShortStatus(afternoonAttendance.Status)}";
                            }

                            reportItem.DailyAttendances[day] = statusText;
                        }
                        else
                        {
                            reportItem.DailyAttendances[day] = "";
                        }
                    }

                    result.Add(reportItem);
                }

                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi GetMonthlyAttendanceReportAsync: {ex.Message}");
                return new List<MonthlyAttendanceReportViewModel>();
            }
        }

        // ✅ Lấy danh sách status để làm filter
        public async Task<List<string>> GetAvailableStatusesAsync(int managerId)
        {
            try
            {
                var manager = await GetManagerInfoAsync(managerId);
                if (manager == null) return new List<string>();

                var statuses = await _context.Attendances
                    .Where(a => _context.Employees
                        .Any(e => e.UserId == a.UserId && 
                                 e.DepartmentId == manager.DepartmentId && 
                                 e.Status == true && 
                                 e.RoleId == 1 && 
                                 e.UserId != managerId))
                    .Select(a => a.Status)
                    .Distinct()
                    .OrderBy(s => s)
                    .ToListAsync();

                return statuses;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi GetAvailableStatusesAsync: {ex.Message}");
                return new List<string>();
            }
        }

        // Chuyển đổi status thành ký hiệu ngắn
        private string GetShortStatus(string status)
        {
            return status switch
            {
                "Đúng giờ" => "P",
                "Đi trễ" => "L",
                "Về sớm" => "E",
                "Đi trễ và về sớm" => "LE",
                _ => "A"
            };
        }

        private int GetWorkingDaysInMonth(DateTime month)
        {
            var startDate = new DateTime(month.Year, month.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            int workingDays = 0;

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                    workingDays++;
            }

            return workingDays;
        }
    }

    // ✅ ViewModel mới cho báo cáo chi tiết theo ca
    public class DetailedAttendanceReportViewModel
    {
        public int UserId { get; set; }
        public string EmployeeName { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }
        public string Shift { get; set; }
        public DateTime? ClockIn { get; set; }
        public DateTime? ClockOut { get; set; }
        public string Status { get; set; }
        public string ShortStatus { get; set; }
    }

    // ViewModel cho báo cáo tháng với cột động
    public class MonthlyAttendanceReportViewModel
    {
        public int UserId { get; set; }
        public string EmployeeName { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public Dictionary<int, string> DailyAttendances { get; set; }
    }

}
