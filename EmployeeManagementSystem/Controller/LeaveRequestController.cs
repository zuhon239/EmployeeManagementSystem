using EmployeeManagementSystem.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Controller
{
    public class LeaveRequestController
    {
        private readonly EmployeeManagementContext _context;
        public LeaveRequestController(EmployeeManagementContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateLeaveRequestAsync(int userId, DateTime startDate, DateTime endDate, string reason, string? shift)
        {
            // Kiểm tra nhân viên tồn tại
            try
            {
                // Kiểm tra nhân viên
                var employee = await _context.Users
                    .OfType<Employee>()
                    .FirstOrDefaultAsync(e => e.UserId == userId);
                if (employee == null)
                {
                    System.Diagnostics.Debug.WriteLine($"Không tìm thấy Employee với UserId: {userId}");
                    throw new ArgumentException("Nhân viên không tồn tại.");
                }

                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrWhiteSpace(reason))
                    throw new ArgumentException("Lý do không được để trống.");
                if (startDate > endDate)
                    throw new ArgumentException("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc.");
                if (startDate < DateTime.Today)
                    throw new ArgumentException("Không thể yêu cầu nghỉ phép trong quá khứ.");

                bool isSingleDay = startDate.Date == endDate.Date;
                if (isSingleDay && string.IsNullOrEmpty(shift))
                    throw new ArgumentException("Ca nghỉ là bắt buộc khi nghỉ trong một ngày.");
                if (!isSingleDay)
                    shift = null;

                // Tạo LeaveRequest
                var leaveRequest = new LeaveRequest
                {
                    UserId = userId,
                    StartDate = startDate,
                    EndDate = endDate,
                    Reason = reason,
                    Shift = shift,
                    Status = "Pending",
                    ApproverId = null // Không gán ApproverId
                };

                // Thêm và lưu
                _context.LeaveRequests.Add(leaveRequest);
                try
                {
                    await _context.SaveChangesAsync();
                    System.Diagnostics.Debug.WriteLine($"Lưu LeaveRequest thành công: LeaveId={leaveRequest.LeaveId}, UserId={userId}");
                }
                catch (DbUpdateException ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Lỗi khi lưu LeaveRequest: {ex.InnerException?.Message ?? ex.Message}");
                    throw new InvalidOperationException("Không thể lưu yêu cầu nghỉ phép. Vui lòng kiểm tra dữ liệu.", ex);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi trong CreateLeaveRequestAsync: {ex.Message}");
                throw;
            }
        }
    }
}
