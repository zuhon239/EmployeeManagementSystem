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

        // ✅ Quy trình truy xuất theo ngày - BỎ HOÀN TOÀN FILTER
        public async Task<List<DailyAttendanceReportViewModel>> GetDailyAttendanceReportAsync(
    int managerId,
    DateTime selectedDate,
    string shiftFilter = "All",
    string statusFilter = "All")
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"=== BẮT ĐẦU QUY TRÌNH TRUY XUẤT ===");
                System.Diagnostics.Debug.WriteLine($"Manager ID: {managerId}, Date: {selectedDate:dd/MM/yyyy}, Shift: {shiftFilter}, Status: {statusFilter}");

                // Bước 1: Lấy thông tin Manager
                var manager = await GetManagerInfoAsync(managerId);
                if (manager == null)
                {
                    System.Diagnostics.Debug.WriteLine($"❌ Không tìm thấy Manager với ID: {managerId}");
                    return new List<DailyAttendanceReportViewModel>();
                }

                System.Diagnostics.Debug.WriteLine($"✅ Manager: {manager.Name}, Department: {manager.Department?.Name}");

                // Bước 2: Lấy tất cả nhân viên trong phòng ban (không bao gồm Manager)
                var employees = await _context.Employees
                    .Where(e => e.DepartmentId == manager.DepartmentId &&
                               e.Status == true &&
                               e.RoleId == 1 &&
                               e.UserId != managerId)
                    .OrderBy(e => e.Name)
                    .ToListAsync();

                System.Diagnostics.Debug.WriteLine($"✅ Tìm thấy {employees.Count} nhân viên trong phòng ban");

                if (employees.Count == 0)
                {
                    System.Diagnostics.Debug.WriteLine($"❌ Không có nhân viên nào trong phòng ban");
                    return new List<DailyAttendanceReportViewModel>();
                }

                var result = new List<DailyAttendanceReportViewModel>();

                foreach (var employee in employees)
                {
                    System.Diagnostics.Debug.WriteLine($"Xử lý nhân viên: {employee.Name} (ID: {employee.UserId})");

                    // ✅ ÁP DỤNG FILTER SHIFT
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
                        // ✅ Tìm attendance record cho ca này (có thể null)
                        var attendance = await _context.Attendances
                            .FirstOrDefaultAsync(a => a.UserId == employee.UserId &&
                                                   a.Date.Date == selectedDate.Date &&
                                                   a.Shift == shift);

                        // ✅ XỬ LÝ TRẠNG THÁI CHO NHÂN VIÊN
                        string displayStatus;
                        if (attendance != null)
                        {
                            if (attendance.ClockIn.HasValue && !attendance.ClockOut.HasValue)
                            {
                                // Đã check in nhưng chưa check out
                                displayStatus = $"{attendance.Status} - Chưa check out";
                            }
                            else
                            {
                                // Đã hoàn thành ca hoặc chỉ có thông tin không đầy đủ
                                displayStatus = attendance.Status;
                            }
                        }
                        else
                        {
                            // Không có attendance record
                            displayStatus = "Vắng mặt";
                        }

                        // ✅ TẠO RECORD CHO NHÂN VIÊN
                        var reportItem = new DailyAttendanceReportViewModel
                        {
                            UserId = employee.UserId,
                            EmployeeName = employee.Name,
                            Position = employee.Position,
                            Shift = shift,
                            ClockIn = attendance?.ClockIn,
                            ClockOut = attendance?.ClockOut,
                            Status = displayStatus
                        };

                        // ✅ ÁP DỤNG FILTER STATUS
                        bool shouldInclude = true;
                        if (statusFilter != "All")
                        {
                            // Filter theo status cụ thể
                            if (statusFilter == "Đúng giờ")
                            {
                                shouldInclude = displayStatus.Contains("Đúng giờ") && !displayStatus.Contains("Chưa check out");
                            }
                            else if (statusFilter == "Đi trễ")
                            {
                                shouldInclude = displayStatus.Contains("Đi trễ") && !displayStatus.Contains("Chưa check out");
                            }
                            else if (statusFilter == "Về sớm")
                            {
                                shouldInclude = displayStatus.Contains("Về sớm");
                            }
                            else if (statusFilter == "Vắng mặt")
                            {
                                shouldInclude = displayStatus == "Vắng mặt";
                            }
                            else if (statusFilter == "Chưa check out")
                            {
                                shouldInclude = displayStatus.Contains("Chưa check out");
                            }
                            else
                            {
                                // Filter khác
                                shouldInclude = displayStatus.Contains(statusFilter);
                            }
                        }

                        // ✅ CHỈ THÊM VÀO KẾT QUẢ NẾU PASS FILTER
                        if (shouldInclude)
                        {
                            result.Add(reportItem);
                            System.Diagnostics.Debug.WriteLine($"✅ Đã thêm: {employee.Name} - {shift} - {displayStatus}");
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"❌ Bị filter: {employee.Name} - {shift} - {displayStatus}");
                        }
                    }
                }

                System.Diagnostics.Debug.WriteLine($"✅ Tổng cộng {result.Count} records sau filter");
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi GetDailyAttendanceReportAsync: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"❌ Stack trace: {ex.StackTrace}");
                return new List<DailyAttendanceReportViewModel>();
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
    }

    // ViewModel cho báo cáo theo ngày
    public class DailyAttendanceReportViewModel
    {
        public int UserId { get; set; }
        public string EmployeeName { get; set; }
        public string Position { get; set; }
        public string Shift { get; set; }
        public DateTime? ClockIn { get; set; }
        public DateTime? ClockOut { get; set; }
        public string Status { get; set; }
    }
}
