using EmployeeManagementSystem.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Controller
{
    public class SalaryAdminController
    {
        private readonly EmployeeManagementContext _context;
        private const decimal STANDARD_WORKING_DAYS = 22m;

        public SalaryAdminController(EmployeeManagementContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Lấy thông tin Admin (khác với Manager)
        public async Task<Employee> GetAdminInfoAsync(int adminId)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.UserId == adminId &&
                                       e.RoleId == 3 &&
                                       e.Status == true);
        }

        // Lấy tất cả phòng ban (Admin có quyền xem tất cả)
        public async Task<List<Department>> GetAllDepartmentsForAdminAsync()
        {
            return await _context.Departments
                .Include(d => d.Manager)
                .Where(d => d.Status == true)
                .OrderBy(d => d.Name)
                .ToListAsync();
        }

        // Lấy tất cả nhân viên (bao gồm Manager) - khác với Manager chỉ xem phòng ban mình
        public async Task<List<Employee>> GetAllEmployeesForAdminAsync(int? departmentId = null)
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

        // Lấy lương cơ bản cho Admin (bao gồm cả Admin role)
        public async Task<decimal> GetBaseSalaryForAdminAsync(int userId)
        {
            try
            {
                var employee = await _context.Employees
                    .Include(e => e.Department)
                    .FirstOrDefaultAsync(e => e.UserId == userId && e.Status == true);

                if (employee == null) return 0m;

                decimal departmentBaseSalary = GetDepartmentBaseSalaryForAdmin(employee.DepartmentId);

                if (employee.RoleId == 2) // Manager
                {
                    return departmentBaseSalary * 1.30m; // +30%
                }
                else if (employee.RoleId == 3) // Admin
                {
                    return departmentBaseSalary * 1.50m; // +50%
                }
                else // Employee
                {
                    return departmentBaseSalary;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi GetBaseSalaryForAdminAsync: {ex.Message}");
                return 0m;
            }
        }

        //Lương cơ bản theo phòng ban cho Admin
        private decimal GetDepartmentBaseSalaryForAdmin(int departmentId)
        {
            return departmentId switch
            {
                1 => 3000000m, // IT Department
                2 => 2800000m, // HR Department  
                3 => 3200000m, // Finance Department
                4 => 2900000m, // Marketing Department
                5 => 3100000m, // QA Department
                6 => 3300000m, // DevOps Department
                _ => 3000000m  // Default
            };
        }

        // Tính lương cho Admin (có thể tính cho bất kỳ ai)
        public async Task<AdminPayrollCalculationResult> CalculatePayrollForAdminAsync(int userId, DateTime month, decimal bonus = 0m, List<DateTime> excludedDays = null)
        {
            try
            {
                var startDate = new DateTime(month.Year, month.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                var employee = await _context.Employees
                    .Include(e => e.Department)
                    .FirstOrDefaultAsync(e => e.UserId == userId && e.Status == true);

                if (employee == null)
                {
                    throw new ArgumentException("Không tìm thấy nhân viên");
                }

                decimal baseSalary = await GetBaseSalaryForAdminAsync(userId);
                decimal dailySalary = baseSalary / STANDARD_WORKING_DAYS;

                var attendances = await _context.Attendances
                    .Where(a => a.UserId == userId &&
                               a.Date >= startDate &&
                               a.Date <= endDate)
                    .ToListAsync();

                var leaveRequests = await _context.LeaveRequests
                    .Where(lr => lr.UserId == userId &&
                                lr.StartDate <= endDate &&
                                lr.EndDate >= startDate)
                    .ToListAsync();

                var result = new AdminPayrollCalculationResult
                {
                    UserId = userId,
                    EmployeeName = employee.Name,
                    EmployeeRole = GetRoleNameForAdmin(employee.RoleId),
                    DepartmentName = employee.Department?.Name ?? "N/A",
                    Month = month,
                    BaseSalary = baseSalary,
                    DailySalary = dailySalary,
                    StandardWorkingDays = (int)STANDARD_WORKING_DAYS,
                    AttendanceDetails = new List<AdminAttendanceDetail>(),
                    TotalDeduction = 0m,
                    Bonus = bonus,
                    ExcludedDays = excludedDays ?? new List<DateTime>(),
                    TotalSalary = baseSalary
                };

                var attendancesByDate = attendances
                    .GroupBy(a => a.Date.Date)
                    .ToDictionary(g => g.Key, g => g.ToList());

                var workingDays = GetWorkingDaysInMonthForAdmin(month);

                foreach (var workingDay in workingDays)
                {
                    var dayAttendances = attendancesByDate.ContainsKey(workingDay)
                        ? attendancesByDate[workingDay]
                        : new List<Attendance>();

                    var dayLeaveRequest = leaveRequests.FirstOrDefault(lr =>
                        workingDay >= lr.StartDate.Date &&
                        workingDay <= lr.EndDate.Date);

                    bool isExcludedDay = excludedDays != null && excludedDays.Any(ed => ed.Date == workingDay);

                    var dayDetail = CalculateDayDeductionForAdmin(workingDay, dayAttendances, dailySalary, dayLeaveRequest, isExcludedDay);
                    result.AttendanceDetails.Add(dayDetail);

                    if (!isExcludedDay)
                    {
                        result.TotalDeduction += dayDetail.DeductionAmount;
                    }
                }

                result.TotalSalary = (baseSalary - result.TotalDeduction) + bonus;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi tính lương cho Admin: {ex.Message}", ex);
            }
        }

        
        private AdminAttendanceDetail CalculateDayDeductionForAdmin(DateTime date, List<Attendance> dayAttendances, decimal dailySalary, LeaveRequest dayLeaveRequest, bool isExcludedDay)
        {
            var detail = new AdminAttendanceDetail
            {
                Date = date,
                DailySalary = dailySalary,
                LeaveRequest = dayLeaveRequest,
                MorningShift = null,
                AfternoonShift = null,
                DeductionAmount = 0m,
                DeductionReason = "",
                IsExcludedDay = isExcludedDay
            };

            var morningAttendance = dayAttendances.FirstOrDefault(a => a.Shift == "Sáng");
            var afternoonAttendance = dayAttendances.FirstOrDefault(a => a.Shift == "Chiều");

            detail.MorningShift = morningAttendance;
            detail.AfternoonShift = afternoonAttendance;

            if (isExcludedDay)
            {
                detail.DeductionAmount = dailySalary;
                detail.DeductionReason = "Ngày đã được xóa - Không tính lương";
                return detail;
            }

            if (dayLeaveRequest != null)
            {
                if (dayLeaveRequest.Status == "Đã duyệt")
                {
                    detail.DeductionReason = $"Nghỉ phép đã duyệt - Không trừ lương (Leave ID: {dayLeaveRequest.LeaveId})";
                    return detail;
                }
                else if (dayLeaveRequest.Status == "Từ chối" || dayLeaveRequest.Status == "Chờ duyệt")
                {
                    detail.DeductionReason += $"Leave request {dayLeaveRequest.Status} - ";
                }
            }

            bool morningAbsent = (morningAttendance == null);
            bool afternoonAbsent = (afternoonAttendance == null);

            if (morningAbsent && afternoonAbsent)
            {
                detail.DeductionAmount = dailySalary;
                detail.DeductionReason += "Vắng mặt nguyên ngày - Không tính lương";
                return detail;
            }

            bool hasViolation = false;
            string violationDetails = "";

            if (morningAttendance != null)
            {
                if (morningAttendance.Status.Contains("Đi trễ") ||
                    morningAttendance.Status.Contains("Về sớm") ||
                    morningAttendance.Status.Contains("Đi trễ và về sớm"))
                {
                    hasViolation = true;
                    violationDetails += $"Sáng: {morningAttendance.Status}; ";
                }
            }
            else
            {
                hasViolation = true;
                violationDetails += "Sáng: Vắng mặt; ";
            }

            if (afternoonAttendance != null)
            {
                if (afternoonAttendance.Status.Contains("Đi trễ") ||
                    afternoonAttendance.Status.Contains("Về sớm") ||
                    afternoonAttendance.Status.Contains("Đi trễ và về sớm"))
                {
                    hasViolation = true;
                    violationDetails += $"Chiều: {afternoonAttendance.Status}; ";
                }
            }
            else
            {
                hasViolation = true;
                violationDetails += "Chiều: Vắng mặt; ";
            }

            if (hasViolation)
            {
                detail.DeductionAmount = dailySalary * 0.05m;
                detail.DeductionReason += $"Vi phạm - Trừ 5% lương ngày ({violationDetails.TrimEnd(' ', ';')})";
            }
            else
            {
                detail.DeductionReason += "Không vi phạm";
            }

            return detail;
        }

        
        private List<DateTime> GetWorkingDaysInMonthForAdmin(DateTime month)
        {
            var workingDays = new List<DateTime>();
            var startDate = new DateTime(month.Year, month.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    workingDays.Add(date);
                }
            }

            return workingDays;
        }

        // Lưu payroll cho Admin
        public async Task<bool> SavePayrollForAdminAsync(AdminPayrollCalculationResult result)
        {
            try
            {
                var monthOnly = new DateTime(result.Month.Year, result.Month.Month, 1);

                var existingPayroll = await _context.Payrolls
                    .FirstOrDefaultAsync(p => p.UserId == result.UserId &&
                                           p.Month.Year == result.Month.Year &&
                                           p.Month.Month == result.Month.Month);

                if (existingPayroll != null)
                {
                    existingPayroll.BaseSalary = result.BaseSalary;
                    existingPayroll.DaysWorked = result.StandardWorkingDays;
                    existingPayroll.Bonus = result.Bonus;
                    existingPayroll.Deduction = result.TotalDeduction;
                    existingPayroll.TotalSalary = result.TotalSalary;
                }
                else
                {
                    var newPayroll = new Payroll(
                        result.UserId,
                        monthOnly,
                        result.BaseSalary,
                        result.StandardWorkingDays,
                        result.Bonus,
                        result.TotalDeduction,
                        result.TotalSalary
                    );
                    _context.Payrolls.Add(newPayroll);
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi lưu payroll cho Admin: {ex.Message}");
                return false;
            }
        }

        //  Lấy báo cáo lương toàn công ty cho Admin
        public async Task<List<AdminSalaryReportViewModel>> GetCompanySalaryReportForAdminAsync(DateTime month, int? departmentFilter = null)
        {
            try
            {
                var employees = await GetAllEmployeesForAdminAsync(departmentFilter);
                var result = new List<AdminSalaryReportViewModel>();

                foreach (var employee in employees)
                {
                    var payroll = await _context.Payrolls
                        .FirstOrDefaultAsync(p => p.UserId == employee.UserId &&
                                           p.Month.Year == month.Year &&
                                           p.Month.Month == month.Month);

                    var baseSalary = await GetBaseSalaryForAdminAsync(employee.UserId);

                    result.Add(new AdminSalaryReportViewModel
                    {
                        UserId = employee.UserId,
                        EmployeeName = employee.Name,
                        Position = employee.Position,
                        DepartmentName = employee.Department?.Name ?? "N/A",
                        RoleName = GetRoleNameForAdmin(employee.RoleId),
                        BaseSalary = baseSalary,
                        Bonus = payroll?.Bonus ?? 0m,
                        Deduction = payroll?.Deduction ?? 0m,
                        TotalSalary = payroll?.TotalSalary ?? baseSalary,
                        HasPayroll = payroll != null
                    });
                }

                return result.OrderBy(r => r.DepartmentName)
                           .ThenBy(r => r.RoleName)
                           .ThenBy(r => r.EmployeeName)
                           .ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi GetCompanySalaryReportForAdminAsync: {ex.Message}");
                return new List<AdminSalaryReportViewModel>();
            }
        }

        // Helper method để lấy tên role cho Admin
        private string GetRoleNameForAdmin(int roleId)
        {
            return roleId switch
            {
                1 => "Employee",
                2 => "Manager",
                3 => "Admin",
                _ => "Unknown"
            };
        }
    }

    // ViewModel riêng cho Admin (tránh trùng tên)
    public class AdminPayrollCalculationResult
    {
        public int UserId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeRole { get; set; }
        public string DepartmentName { get; set; }
        public DateTime Month { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal DailySalary { get; set; }
        public int StandardWorkingDays { get; set; }
        public List<AdminAttendanceDetail> AttendanceDetails { get; set; }
        public decimal TotalDeduction { get; set; }
        public decimal Bonus { get; set; }
        public List<DateTime> ExcludedDays { get; set; }
        public decimal TotalSalary { get; set; }
    }

    public class AdminAttendanceDetail
    {
        public DateTime Date { get; set; }
        public decimal DailySalary { get; set; }
        public LeaveRequest LeaveRequest { get; set; }
        public Attendance MorningShift { get; set; }
        public Attendance AfternoonShift { get; set; }
        public decimal DeductionAmount { get; set; }
        public string DeductionReason { get; set; }
        public bool IsExcludedDay { get; set; }
    }

    public class AdminSalaryReportViewModel
    {
        public int UserId { get; set; }
        public string EmployeeName { get; set; }
        public string Position { get; set; }
        public string DepartmentName { get; set; }
        public string RoleName { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deduction { get; set; }
        public decimal TotalSalary { get; set; }
        public bool HasPayroll { get; set; }
    }
}
