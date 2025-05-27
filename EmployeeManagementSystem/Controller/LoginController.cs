using EmployeeManagementSystem.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Controller
{
    public class LoginController
    {
        private readonly EmployeeManagementContext _context;
        private readonly LeaveRequestController _leaveRequestController;
        private readonly Dictionary<string, (string Token, DateTime Expiry, int UserId)> _resetTokens;
        private readonly EmailService _emailService;
        private readonly AttendanceController _attendanceController;
        public LoginController(EmployeeManagementContext context, LeaveRequestController leaveRequestController, AttendanceController attendanceController)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _emailService = new EmailService();
            _leaveRequestController = leaveRequestController ?? throw new ArgumentNullException(nameof(leaveRequestController));
            _attendanceController = attendanceController ?? throw new ArgumentNullException(nameof(attendanceController));
            _resetTokens = new Dictionary<string, (string, DateTime, int)>();
        }

        public bool Login(string username, string password, out string errorMessage, out Form nextForm)
        {
            errorMessage = string.Empty;
            nextForm = null;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                errorMessage = "Username and password are required.";
                return false;
            }

            var user = User.Authenticate(_context, username, password);
            if (user == null)
            {
                errorMessage = "Invalid username or password.";
                return false;
            }

            // Navigate based on role
            switch (user.Role.RoleName)
            {
                case "Admin":
                    nextForm = new AdminForm(user.UserId, _context);
                    break;
                case "Manager":
                    nextForm = new ManagerForm(user.UserId, _leaveRequestController, _context, _attendanceController );
                    break;
                case "Employee":
                    nextForm = new EmployeeForm(user.UserId, _leaveRequestController, _context, _attendanceController);
                    break;
                default:
                    errorMessage = "Unknown role.";
                    return false;
            }
            return true;
        }
        public bool GeneratePasswordResetTokenByEmail(string email, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                // Kiểm tra email có tồn tại trong cơ sở dữ liệu không
                var user = _context.Users
                    .AsNoTracking()
                    .Where(u => u.Email == email && u.Status == true)
                    .Select(u => new { u.UserId, u.Username, u.Email })
                    .FirstOrDefault();

                if (user == null)
                {
                    errorMessage = "Email không tồn tại trong hệ thống hoặc tài khoản đã bị vô hiệu hóa.";
                    return false;
                }

                // Tạo mã token ngẫu nhiên
                string resetToken = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
                var expiry = DateTime.UtcNow.AddMinutes(15); // Token hết hạn sau 15 phút

                // Gửi email chứa token
                if (!_emailService.SendPasswordResetEmail(user.Email, resetToken, user.Username, out errorMessage))
                {
                    return false;
                }

                // Lưu token với email làm key, kèm theo UserId
                _resetTokens[email] = (resetToken, expiry, user.UserId);

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Có lỗi xảy ra khi tạo token: {ex.Message}";
                return false;
            }
        }

        public UserInfo GetUserInfoByEmail(string email)
        {
            try
            {
                var user = _context.Users
                    .AsNoTracking()
                    .Where(u => u.Email == email && u.Status == true)
                    .Select(u => new UserInfo
                    {
                        UserId = u.UserId,
                        Username = u.Username,
                        Email = u.Email
                    })
                    .FirstOrDefault();

                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool ResetPassword(string email, string token, string newPassword, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                // Kiểm tra token có tồn tại không
                if (!_resetTokens.ContainsKey(email))
                {
                    errorMessage = "Token không hợp lệ hoặc đã hết hạn.";
                    return false;
                }

                var (storedToken, expiry, userId) = _resetTokens[email];

                // Kiểm tra token và thời gian hết hạn
                if (storedToken != token || DateTime.UtcNow > expiry)
                {
                    errorMessage = "Token không hợp lệ hoặc đã hết hạn.";
                    _resetTokens.Remove(email); // Xóa token đã hết hạn
                    return false;
                }

                // Kiểm tra mật khẩu mới
                if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 6)
                {
                    errorMessage = "Mật khẩu mới phải có ít nhất 6 ký tự.";
                    return false;
                }

                // Tìm user theo UserId và Email để đảm bảo tính chính xác
                var user = _context.Users
                    .FirstOrDefault(u => u.UserId == userId && u.Email == email && u.Status == true);

                if (user == null)
                {
                    errorMessage = "Không tìm thấy người dùng hoặc tài khoản đã bị vô hiệu hóa.";
                    _resetTokens.Remove(email);
                    return false;
                }

                // Cập nhật mật khẩu
                user.UpdatePassword(newPassword);
                user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
                // Gửi email chứa token
                if (!_emailService.SendPasswordChangeNotification(user.Email, user.Username, out errorMessage))
                {
                    return false;
                }
                _context.SaveChanges();

                // Xóa token sau khi sử dụng thành công
                _resetTokens.Remove(email);

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Có lỗi xảy ra khi đặt lại mật khẩu: {ex.Message}";
                return false;
            }
        }

        public bool ValidateResetToken(string email, string token, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (!_resetTokens.ContainsKey(email))
            {
                errorMessage = "Token không hợp lệ.";
                return false;
            }

            var (storedToken, expiry, _) = _resetTokens[email];

            if (storedToken != token)
            {
                errorMessage = "Token không đúng.";
                return false;
            }

            if (DateTime.UtcNow > expiry)
            {
                errorMessage = "Token đã hết hạn.";
                _resetTokens.Remove(email);
                return false;
            }

            return true;
        }

        // Class helper để trả về thông tin user
        public class UserInfo
        {
            public int UserId { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
        }
    }
}
