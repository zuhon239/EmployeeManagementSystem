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

        // ✅ Lấy thông tin Manager (không dùng ManagerId)
        public async Task<Employee> GetManagerInfoAsync(int managerId)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.UserId == managerId &&
                                       e.RoleId == 2 &&
                                       e.Status == true);
        }

        // ✅ Lấy lương cơ bản theo phòng ban và chức vụ
        public async Task<decimal> GetBaseSalaryAsync(int userId)
        {
            try
            {
                var employee = await _context.Employees
                    .Include(e => e.Department)
                    .FirstOrDefaultAsync(e => e.UserId == userId && e.Status == true);

                if (employee == null) return 0m;

                // ✅ Lương cơ bản theo phòng ban (có thể lưu trong bảng Department hoặc config)
                decimal departmentBaseSalary = GetDepartmentBaseSalary(employee.DepartmentId);

                // ✅ Manager lương cao hơn 30%
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

        // ✅ Lương cơ bản theo phòng ban (có thể config trong database sau)
        private decimal GetDepartmentBaseSalary(int departmentId)
        {
            // Tạm thời hardcode, sau này có thể lưu trong bảng DepartmentSalaryConfig
            return departmentId switch
            {
                1 => 3000000m, // IT Department
                2 => 2800000m, // HR Department  
                3 => 3200000m, // Finance Department
                4 => 2900000m, // Marketing Department
                _ => 3000000m  // Default
            };
        }

        // ✅ Lấy danh sách nhân viên trong phòng ban
        public async Task<List<Employee>> GetDepartmentEmployeesAsync(int managerId)
        {
            try
            {
                var manager = await GetManagerInfoAsync(managerId);
                if (manager == null) return new List<Employee>();

                return await _context.Employees
                    .Where(e => e.DepartmentId == manager.DepartmentId &&
                               e.Status == true &&
                               e.RoleId == 1) // Chỉ Employee, không lấy Manager
                    .OrderBy(e => e.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi GetDepartmentEmployeesAsync: {ex.Message}");
                return new List<Employee>();
            }
        }

        // ✅ Tính lương cho nhân viên theo tháng (Logic mới)
        public async Task<PayrollCalculationResult> CalculatePayrollAsync(int userId, DateTime month)
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

                // ✅ Lấy lương cơ bản theo phòng ban và chức vụ
                decimal baseSalary = await GetBaseSalaryAsync(userId);
                decimal dailySalary = baseSalary / STANDARD_WORKING_DAYS;

                var attendances = await _context.Attendances
                    .Where(a => a.UserId == userId &&
                               a.Date >= startDate &&
                               a.Date <= endDate)
                    .ToListAsync();

                // ✅ Lấy leave requests theo trạng thái
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

                    // ✅ Kiểm tra leave request cho ngày này
                    var dayLeaveRequest = leaveRequests.FirstOrDefault(lr =>
                        workingDay >= lr.StartDate.Date &&
                        workingDay <= lr.EndDate.Date);

                    var dayDetail = CalculateDayDeduction(workingDay, dayAttendances, dailySalary, dayLeaveRequest);
                    result.AttendanceDetails.Add(dayDetail);
                    result.TotalDeduction += dayDetail.DeductionAmount;
                }

                result.TotalSalary = baseSalary - result.TotalDeduction;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi tính lương: {ex.Message}", ex);
            }
        }

        // ✅ Tính khấu trừ cho một ngày cụ thể (Logic mới)
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

            // ✅ Logic mới: Kiểm tra leave request
            if (dayLeaveRequest != null)
            {
                if (dayLeaveRequest.Status == "Approved")
                {
                    // TH1: Ngày được duyệt nghỉ phép - KHÔNG BỊ TRỪ LƯƠNG dù có vi phạm
                    detail.DeductionReason = $"Nghỉ phép đã duyệt - Không trừ lương (Leave ID: {dayLeaveRequest.LeaveId})";
                    return detail; // Deduction = 0
                }
                else if (dayLeaveRequest.Status == "Rejected" || dayLeaveRequest.Status == "Pending")
                {
                    // TH2, TH3: Bị từ chối hoặc chưa reply - ÁP DỤNG BÌNH THƯỜNG
                    detail.DeductionReason += $"Leave request {dayLeaveRequest.Status} - ";
                }
            }

            // ✅ Kiểm tra vi phạm chấm công (áp dụng bình thường)
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

            // ✅ Tính khấu trừ: MỖI NGÀY CHỈ TRỪ TỐI ĐA 5%
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

        public async Task<bool> SavePayrollAsync(PayrollCalculationResult result)
        {
            try
            {
                var existingPayroll = await _context.Payrolls
                    .FirstOrDefaultAsync(p => p.UserId == result.UserId &&
                                           p.Month.Year == result.Month.Year &&
                                           p.Month.Month == result.Month.Month);

                if (existingPayroll != null)
                {
                    existingPayroll.BaseSalary = result.BaseSalary;
                    existingPayroll.DaysWorked = result.StandardWorkingDays;
                    existingPayroll.Deduction = result.TotalDeduction;
                    existingPayroll.TotalSalary = result.TotalSalary;
                }
                else
                {
                    var newPayroll = new Payroll(
                        result.UserId,
                        result.Month,
                        result.BaseSalary,
                        result.StandardWorkingDays,
                        0m,
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
        public decimal TotalSalary { get; set; }
    }

    // ✅ Cập nhật AttendanceDetail
    public class AttendanceDetail
    {
        public DateTime Date { get; set; }
        public decimal DailySalary { get; set; }
        public LeaveRequest LeaveRequest { get; set; } // ✅ Thay đổi từ bool sang LeaveRequest
        public Attendance MorningShift { get; set; }
        public Attendance AfternoonShift { get; set; }
        public decimal DeductionAmount { get; set; }
        public string DeductionReason { get; set; }
    }
}
