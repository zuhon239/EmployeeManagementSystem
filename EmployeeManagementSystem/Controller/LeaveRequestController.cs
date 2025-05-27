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
                // Kiểm tra ca nghỉ cho ngày hôm nay
                if (isSingleDay && startDate.Date == DateTime.Today)
                {
                    DateTime now = DateTime.Now;
                    bool isMorningShift = now.Hour >= 7 && now.Hour < 12; // Ca sáng: 7:00 - 12:00
                    bool isAfternoonShift = now.Hour >= 13 && now.Hour < 24; // Ca chiều: 12:00 - 19:00
                    bool isRest = now.Hour >= 12 && now.Hour < 13; 

                    if (isMorningShift && (shift == "Morning"||shift == "FullDay"))
                        throw new ArgumentException("Không thể xin nghỉ ca sáng trong khi đang trong ca sáng.");
                    if (isAfternoonShift && (shift == "Morning" || shift == "FullDay"))
                        throw new ArgumentException("Không thể xin nghỉ ca sáng trong khi đang trong buổi chiều.");
                    if (isAfternoonShift && (shift == "Afternoon" || shift == "FullDay"))
                        throw new ArgumentException("Không thể xin nghỉ ca chiều hoặc cả ngày trong khi đang trong ca chiều.");
                    if (isRest && (shift == "Morning" || shift == "FullDay"))
                        throw new ArgumentException("Không thể xin nghỉ ca sáng trong khi đã qua ca sáng rồi.");
                }
                if (!isSingleDay)
                    shift = null;
                // Kiểm tra trùng lặp yêu cầu nghỉ phép
                var existingRequests = await _context.LeaveRequests
                    .Where(lr => lr.UserId == userId && lr.Status != "Rejected")
                    .ToListAsync();

                foreach (var request in existingRequests)
                {
                    // Kiểm tra trùng lặp ngày
                    if (startDate.Date <= request.EndDate.Date && endDate.Date >= request.StartDate.Date)
                    {
                        if (isSingleDay && request.StartDate.Date == request.EndDate.Date && startDate.Date == request.StartDate.Date)
                        {
                            // Trường hợp nghỉ theo ca trong cùng một ngày
                            if (request.Shift != null && shift != null)
                            {
                                if (request.Shift == shift)
                                    throw new ArgumentException($"Đã có yêu cầu nghỉ ca {shift} trong ngày {startDate:dd/MM/yyyy}.");
                                if (request.Shift == "FullDay" || shift == "FullDay")
                                    throw new ArgumentException($"Đã có yêu cầu nghỉ cả ngày hoặc ca {request.Shift} trong ngày {startDate:dd/MM/yyyy}.");
                            }
                        }
                        else
                        {
                            // Trường hợp nghỉ nhiều ngày hoặc không phải ca
                            throw new ArgumentException($"Đã có yêu cầu nghỉ phép trùng lặp trong khoảng từ {request.StartDate:dd/MM/yyyy} đến {request.EndDate:dd/MM/yyyy}.");
                        }
                    }
                }
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
        public async Task ApproveOrRejectLeaveRequestAsync(int leaveId, int approverId, bool isApproved)
        {
            try
            {
                // Kiểm tra yêu cầu nghỉ phép tồn tại
                var leaveRequest = await _context.LeaveRequests
                    .Include(lr => lr.Employee)
                    .FirstOrDefaultAsync(lr => lr.LeaveId == leaveId);
                if (leaveRequest == null)
                {
                    System.Diagnostics.Debug.WriteLine($"Không tìm thấy LeaveRequest với LeaveId: {leaveId}");
                    throw new ArgumentException("Yêu cầu nghỉ phép không tồn tại.");
                }

                // Kiểm tra trạng thái hiện tại
                if (leaveRequest.Status != "Pending")
                {
                    System.Diagnostics.Debug.WriteLine($"Yêu cầu nghỉ phép LeaveId: {leaveId} không ở trạng thái Pending.");
                    throw new InvalidOperationException("Chỉ có thể duyệt/từ chối yêu cầu đang ở trạng thái Pending.");
                }

                // Kiểm tra quyền của người duyệt (Manager hoặc Admin)
                var approver = await _context.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.UserId == approverId);
                if (approver == null || (approver.Role.RoleName != "Manager" && approver.Role.RoleName != "Admin"))
                {
                    System.Diagnostics.Debug.WriteLine($"Người dùng UserId: {approverId} không có quyền duyệt.");
                    throw new InvalidOperationException("Chỉ Manager hoặc Admin mới có quyền duyệt yêu cầu nghỉ phép.");
                }

                // Cập nhật trạng thái và thông tin người duyệt
                leaveRequest.Status = isApproved ? "Approved" : "Rejected";
                leaveRequest.ApproverId = approverId;

                // Lưu thay đổi
                try
                {
                    await _context.SaveChangesAsync();
                    System.Diagnostics.Debug.WriteLine($"Cập nhật trạng thái LeaveRequest thành công: LeaveId={leaveId}, Status={leaveRequest.Status}");
                }
                catch (DbUpdateException ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Lỗi khi lưu trạng thái LeaveRequest: {ex.InnerException?.Message ?? ex.Message}");
                    throw new InvalidOperationException("Không thể cập nhật trạng thái yêu cầu nghỉ phép.", ex);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi trong ApproveOrRejectLeaveRequestAsync: {ex.Message}");
                throw;
            }
        }
    }
}
