using EmployeeManagementSystem.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Controller
{
    public class AttendanceAdminController
    {
        private readonly EmployeeManagementContext _context;

        public AttendanceAdminController(EmployeeManagementContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // ✅ Lấy thông tin Admin
        public async Task<Employee> GetAdminInfoAsync(int adminId)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.UserId == adminId &&
                                       e.RoleId == 3 &&
                                       e.Status == true);
        }

        // ✅ Lấy tất cả phòng ban
        public async Task<List<Department>> GetAllDepartmentsAsync()
        {
            return await _context.Departments
                .Include(d => d.Manager)
                .Where(d => d.Status == true)
                .OrderBy(d => d.Name)
                .ToListAsync();
        }

        // ✅ Lấy tất cả nhân viên theo phòng ban (bao gồm cả Manager)
        public async Task<List<Employee>> GetEmployeesByDepartmentAsync(int? departmentId = null)
        {
            var query = _context.Employees
                .Include(e => e.Department)
                .Where(e => e.Status == true);

            if (departmentId.HasValue)
            {
                query = query.Where(e => e.DepartmentId == departmentId.Value);
            }

            return await query
                .OrderBy(e => e.Department.Name)
                .ThenBy(e => e.RoleId) // Manager trước, Employee sau
                .ThenBy(e => e.Name)
                .ToListAsync();
        }

        // ✅ Báo cáo chấm công theo ngày cho tất cả phòng ban
        public async Task<List<AdminAttendanceReportViewModel>> GetDailyAttendanceReportAsync(
            DateTime selectedDate,
            int? departmentFilter = null,
            string shiftFilter = "All",
            string statusFilter = "All")
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"=== ADMIN ATTENDANCE REPORT ===");
                System.Diagnostics.Debug.WriteLine($"Date: {selectedDate:dd/MM/yyyy}, Dept: {departmentFilter}, Shift: {shiftFilter}, Status: {statusFilter}");

                // Lấy tất cả nhân viên (bao gồm Manager)
                var employees = await GetEmployeesByDepartmentAsync(departmentFilter);

                if (employees.Count == 0)
                {
                    System.Diagnostics.Debug.WriteLine("❌ Không có nhân viên nào");
                    return new List<AdminAttendanceReportViewModel>();
                }

                var result = new List<AdminAttendanceReportViewModel>();

                foreach (var employee in employees)
                {
                    // Xử lý filter shift
                    List<string> shiftsToShow = new List<string>();
                    if (shiftFilter == "All")
                    {
                        shiftsToShow.AddRange(new[] { "Sáng", "Chiều" });
                    }
                    else
                    {
                        shiftsToShow.Add(shiftFilter);
                    }

                    foreach (var shift in shiftsToShow)
                    {
                        // Tìm attendance record cho ca này
                        var attendance = await _context.Attendances
                            .FirstOrDefaultAsync(a => a.UserId == employee.UserId &&
                                                   a.Date.Date == selectedDate.Date &&
                                                   a.Shift == shift);

                        // Xử lý trạng thái
                        string displayStatus;
                        if (attendance != null)
                        {
                            if (attendance.ClockIn.HasValue && !attendance.ClockOut.HasValue)
                            {
                                displayStatus = $"{attendance.Status} - Chưa check out";
                            }
                            else
                            {
                                displayStatus = attendance.Status;
                            }
                        }
                        else
                        {
                            displayStatus = "Vắng mặt";
                        }

                        var reportItem = new AdminAttendanceReportViewModel
                        {
                            UserId = employee.UserId,
                            EmployeeName = employee.Name,
                            Position = employee.Position,
                            DepartmentName = employee.Department?.Name ?? "N/A",
                            RoleName = GetRoleName(employee.RoleId),
                            Shift = shift,
                            ClockIn = attendance?.ClockIn,
                            ClockOut = attendance?.ClockOut,
                            Status = displayStatus
                        };

                        // Áp dụng filter status
                        bool shouldInclude = true;
                        if (statusFilter != "All")
                        {
                            if (statusFilter == "Đúng giờ")
                            {
                                shouldInclude = displayStatus.Contains("Đúng giờ") && !displayStatus.Contains("Chưa check out");
                            }
                            else if (statusFilter == "Đi trễ")
                            {
                                shouldInclude = displayStatus.Contains("Đi trễ") && !displayStatus.Contains("Chưa check out");
                            }
                            else if (statusFilter == "Chưa check out")
                            {
                                shouldInclude = displayStatus.Contains("Chưa check out");
                            }
                            else if (statusFilter == "Vắng mặt")
                            {
                                shouldInclude = displayStatus == "Vắng mặt";
                            }
                            else
                            {
                                shouldInclude = displayStatus.Contains(statusFilter);
                            }
                        }

                        if (shouldInclude)
                        {
                            result.Add(reportItem);
                        }
                    }
                }

                System.Diagnostics.Debug.WriteLine($"✅ Tổng cộng {result.Count} records");
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi GetDailyAttendanceReportAsync: {ex.Message}");
                return new List<AdminAttendanceReportViewModel>();
            }
        }

        // ✅ Lấy danh sách status có sẵn
        public async Task<List<string>> GetAvailableStatusesAsync()
        {
            try
            {
                var statuses = await _context.Attendances
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

        // ✅ Helper method để lấy tên role
        private string GetRoleName(int roleId)
        {
            return roleId switch
            {
                1 => "Employee",
                2 => "Manager",
                3 => "Admin",
                _ => "Unknown"
            };
        }

        // ✅ Thống kê tổng quan
        public async Task<AttendanceStatistics> GetAttendanceStatisticsAsync(DateTime selectedDate, int? departmentFilter = null)
        {
            try
            {
                var employees = await GetEmployeesByDepartmentAsync(departmentFilter);
                var totalEmployees = employees.Count;

                var attendances = await _context.Attendances
                    .Where(a => a.Date.Date == selectedDate.Date)
                    .ToListAsync();

                var presentEmployees = attendances
                    .Where(a => employees.Any(e => e.UserId == a.UserId))
                    .Select(a => a.UserId)
                    .Distinct()
                    .Count();

                var lateEmployees = attendances
                    .Where(a => employees.Any(e => e.UserId == a.UserId) &&
                               a.Status.Contains("Đi trễ"))
                    .Select(a => a.UserId)
                    .Distinct()
                    .Count();

                var absentEmployees = totalEmployees - presentEmployees;

                return new AttendanceStatistics
                {
                    TotalEmployees = totalEmployees,
                    PresentEmployees = presentEmployees,
                    AbsentEmployees = absentEmployees,
                    LateEmployees = lateEmployees,
                    AttendanceRate = totalEmployees > 0 ? (decimal)presentEmployees / totalEmployees * 100 : 0
                };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi GetAttendanceStatisticsAsync: {ex.Message}");
                return new AttendanceStatistics();
            }
        }
    }

    // ✅ ViewModel cho báo cáo Admin
    public class AdminAttendanceReportViewModel
    {
        public int UserId { get; set; }
        public string EmployeeName { get; set; }
        public string Position { get; set; }
        public string DepartmentName { get; set; }
        public string RoleName { get; set; }
        public string Shift { get; set; }
        public DateTime? ClockIn { get; set; }
        public DateTime? ClockOut { get; set; }
        public string Status { get; set; }
    }

    // ✅ ViewModel cho thống kê
    public class AttendanceStatistics
    {
        public int TotalEmployees { get; set; }
        public int PresentEmployees { get; set; }
        public int AbsentEmployees { get; set; }
        public int LateEmployees { get; set; }
        public decimal AttendanceRate { get; set; }
    }
}
