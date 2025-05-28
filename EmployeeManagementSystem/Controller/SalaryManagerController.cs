using EmployeeManagementSystem.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Controller
{
    public class SalaryManagerController
    {
        private readonly EmployeeManagementContext _context;
        private const decimal STANDARD_WORKING_DAYS = 26m;
        
        public SalaryManagerController(EmployeeManagementContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Employee> GetManagerInfoAsync(int managerId)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.UserId == managerId &&
                                       e.RoleId == 2 &&
                                       e.Status == true);
        }

        public async Task<decimal> GetBaseSalaryAsync(int userId)
        {
            try
            {
                var employee = await _context.Employees
                    .Include(e => e.Department)
                    .FirstOrDefaultAsync(e => e.UserId == userId && e.Status == true);

                if (employee == null) return 0m;

                decimal departmentBaseSalary = GetDepartmentBaseSalary(employee.DepartmentId);

                if (employee.RoleId == 2) // Manager
                {
                    return departmentBaseSalary * 1.30m; // +30%
                }
                else // Employee
                {
                    return departmentBaseSalary;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi GetBaseSalaryAsync: {ex.Message}");
                return 0m;
            }
        }

        private decimal GetDepartmentBaseSalary(int departmentId)
        {
            return departmentId switch
            {
                1 => 3000000m, // IT Department
                2 => 2800000m, // HR Department  
                3 => 3200000m, // Finance Department
                4 => 2900000m, // Marketing Department
                _ => 3000000m  // Default
            };
        }

        public async Task<List<Employee>> GetDepartmentEmployeesAsync(int managerId)
        {
            try
            {
                var manager = await GetManagerInfoAsync(managerId);
                if (manager == null) return new List<Employee>();

                return await _context.Employees
                    .Where(e => e.DepartmentId == manager.DepartmentId &&
                               e.Status == true &&
                               e.RoleId == 1)
                    .OrderBy(e => e.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi GetDepartmentEmployeesAsync: {ex.Message}");
                return new List<Employee>();
            }
        }

        // ✅ Tính lương với Bonus (Logic mới)
        public async Task<PayrollCalculationResult> CalculatePayrollAsync(int userId, DateTime month, decimal bonus = 0m)
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

                decimal baseSalary = await GetBaseSalaryAsync(userId);
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

                var result = new PayrollCalculationResult
                {
                    UserId = userId,
                    EmployeeName = employee.Name,
                    Month = month,
                    BaseSalary = baseSalary,
                    DailySalary = dailySalary,
                    StandardWorkingDays = (int)STANDARD_WORKING_DAYS,
                    AttendanceDetails = new List<AttendanceDetail>(),
                    TotalDeduction = 0m,
                    Bonus = bonus, // ✅ Thêm Bonus
                    TotalSalary = baseSalary
                };

                var attendancesByDate = attendances
                    .GroupBy(a => a.Date.Date)
                    .ToDictionary(g => g.Key, g => g.ToList());

                var workingDays = GetWorkingDaysInMonth(month);

                foreach (var workingDay in workingDays)
                {
                    var dayAttendances = attendancesByDate.ContainsKey(workingDay) 
                        ? attendancesByDate[workingDay] 
                        : new List<Attendance>();

                    var dayLeaveRequest = leaveRequests.FirstOrDefault(lr => 
                        workingDay >= lr.StartDate.Date && 
                        workingDay <= lr.EndDate.Date);

                    var dayDetail = CalculateDayDeduction(workingDay, dayAttendances, dailySalary, dayLeaveRequest);
                    result.AttendanceDetails.Add(dayDetail);
                    result.TotalDeduction += dayDetail.DeductionAmount;
                }

                // ✅ Tính lương thực nhận: (Lương cơ bản - Khấu trừ) + Bonus
                result.TotalSalary = (baseSalary - result.TotalDeduction) + bonus;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi tính lương: {ex.Message}", ex);
            }
        }

        // ✅ Tính khấu trừ cho một ngày cụ thể (Logic mới - vắng mặt nguyên ngày)
        private AttendanceDetail CalculateDayDeduction(DateTime date, List<Attendance> dayAttendances, decimal dailySalary, LeaveRequest dayLeaveRequest)
        {
            var detail = new AttendanceDetail
            {
                Date = date,
                DailySalary = dailySalary,
                LeaveRequest = dayLeaveRequest,
                MorningShift = null,
                AfternoonShift = null,
                DeductionAmount = 0m,
                DeductionReason = ""
            };

            var morningAttendance = dayAttendances.FirstOrDefault(a => a.Shift == "Sáng");
            var afternoonAttendance = dayAttendances.FirstOrDefault(a => a.Shift == "Chiều");

            detail.MorningShift = morningAttendance;
            detail.AfternoonShift = afternoonAttendance;

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

            // ✅ KIỂM TRA VẮNG MẶT NGUYÊN NGÀY
            bool morningAbsent = (morningAttendance == null);
            bool afternoonAbsent = (afternoonAttendance == null);

            if (morningAbsent && afternoonAbsent)
            {
                // ✅ VẮNG MẶT NGUYÊN NGÀY - KHÔNG TÍNH LƯƠNG NGÀY ĐÓ
                detail.DeductionAmount = dailySalary; // Trừ 100% lương ngày
                detail.DeductionReason += "Vắng mặt nguyên ngày - Không tính lương";
                return detail;
            }

            // ✅ CHỈ ĐI 1 CA - TÍNH LƯƠNG NHƯ HIỆN TẠI (TRỪ 5%)
            bool hasViolation = false;
            string violationDetails = "";

            // Kiểm tra ca sáng
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

            // Kiểm tra ca chiều
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

            // ✅ Tính khấu trừ: CHỈ ĐI 1 CA VẪN TRỪ 5%
            if (hasViolation)
            {
                detail.DeductionAmount = dailySalary * 0.05m; // 5% lương ngày
                detail.DeductionReason += $"Vi phạm - Trừ 5% lương ngày ({violationDetails.TrimEnd(' ', ';')})";
            }
            else
            {
                detail.DeductionReason += "Không vi phạm";
            }

            return detail;
        }


        private List<DateTime> GetWorkingDaysInMonth(DateTime month)
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

        // ✅ Lưu payroll với Bonus và chỉ lưu tháng
        public async Task<bool> SavePayrollAsync(PayrollCalculationResult result)
        {
            try
            {
                // ✅ Tạo DateTime chỉ với tháng (ngày 1 của tháng)
                var monthOnly = new DateTime(result.Month.Year, result.Month.Month, 1);

                var existingPayroll = await _context.Payrolls
                    .FirstOrDefaultAsync(p => p.UserId == result.UserId && 
                                           p.Month.Year == result.Month.Year && 
                                           p.Month.Month == result.Month.Month);

                if (existingPayroll != null)
                {
                    existingPayroll.BaseSalary = result.BaseSalary;
                    existingPayroll.DaysWorked = result.StandardWorkingDays;
                    existingPayroll.Bonus = result.Bonus; // ✅ Cập nhật Bonus
                    existingPayroll.Deduction = result.TotalDeduction;
                    existingPayroll.TotalSalary = result.TotalSalary;
                }
                else
                {
                    var newPayroll = new Payroll(
                        result.UserId,
                        monthOnly, // ✅ Chỉ lưu tháng (ngày 1)
                        result.BaseSalary,
                        result.StandardWorkingDays,
                        result.Bonus, // ✅ Lưu Bonus
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
                System.Diagnostics.Debug.WriteLine($"Lỗi lưu payroll: {ex.Message}");
                return false;
            }
        }
    }

    // ✅ Cập nhật PayrollCalculationResult - thêm Bonus
    public class PayrollCalculationResult
    {
        public int UserId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime Month { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal DailySalary { get; set; }
        public int StandardWorkingDays { get; set; }
        public List<AttendanceDetail> AttendanceDetails { get; set; }
        public decimal TotalDeduction { get; set; }
        public decimal Bonus { get; set; } // ✅ Thêm Bonus
        public decimal TotalSalary { get; set; } // (BaseSalary - TotalDeduction) + Bonus
    }

    public class AttendanceDetail
    {
        public DateTime Date { get; set; }
        public decimal DailySalary { get; set; }
        public LeaveRequest LeaveRequest { get; set; }
        public Attendance MorningShift { get; set; }
        public Attendance AfternoonShift { get; set; }
        public decimal DeductionAmount { get; set; }
        public string DeductionReason { get; set; }
    }
}
