using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
namespace EmployeeManagementSystem.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; } // Khóa chính

        [Required]
        [StringLength(50)] 
        public string Username { get; set; } // Tên đăng nhập

        [Required]
        [StringLength(256)] // Lưu mật khẩu đã mã hóa (bcrypt)
        public string Password { get; set; } // Mật khẩu

        [Required]
        [StringLength(256)]
        public string HashedPassword { get; set; } // Lưu chuỗi mã hóa
 
        [StringLength(256)]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string? Email { get; set; }

        [Required]
        [ForeignKey("Role")]
        public int RoleId { get; set; } // Khóa ngoại liên kết với Roles

        [Required]
        public bool Status { get; set; } // Trạng thái (true: active, false: inactive)

        // Thuộc tính điều hướng
        public virtual Role Role { get; set; }

        // Constructor mặc định cho EF Core
        public User()
        {
            Username = string.Empty;
            Password = string.Empty;
            HashedPassword = string.Empty;
            Email = null;
        }

        // Constructor với tham số
        public User(string username, string password, string? email, int roleId, bool status)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            Email = email;
            HashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            RoleId = roleId;
            Status = status;

            // Validate email format
            if (!string.IsNullOrWhiteSpace(email) && !IsValidEmail(email))
                throw new ArgumentException("Email không hợp lệ.", nameof(email));
        }

        // Methods để cập nhật thông tin (đảm bảo tính đóng gói)
        public void UpdateUsername(string newUsername)
        {
            Username = newUsername ?? throw new ArgumentNullException(nameof(newUsername));
        }

        public void UpdatePassword(string newPassword)
        {
            Password = newPassword ?? throw new ArgumentNullException(nameof(newPassword));
        }
        public void UpdateEmail(string newEmail)
        {
            // Chấp nhận email rỗng (null hoặc chuỗi rỗng)
            if (!string.IsNullOrWhiteSpace(newEmail))
            {
                // Validate định dạng email nếu email được cung cấp
                var emailValidator = new EmailAddressAttribute();
                if (!emailValidator.IsValid(newEmail))
                    throw new ArgumentException("Email không hợp lệ.", nameof(newEmail));
            }
            Email = newEmail;
        }
        // Static method for authentication
        public static User Authenticate(EmployeeManagementContext context, string username, string password)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            try
            {
                var user = context.Users
                     .Include(u => u.Role)
                     .FirstOrDefault(u => u.Username == username && u.Status);

                if (user != null && BCrypt.Net.BCrypt.Verify(password, user.HashedPassword))
                {
                    return user;
                }
            }
            catch (Exception)
            {
                // Log exception if needed
                return null;
            }

            return null;
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var emailValidator = new EmailAddressAttribute();
                return emailValidator.IsValid(email);
            }
            catch
            {
                return false;
            }
        }
        public static User FindByEmail(EmployeeManagementContext context, string email)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrWhiteSpace(email))
                return null;

            try
            {
                return context.Users
                    .Include(u => u.Role)
                    .FirstOrDefault(u => u.Email.ToLower() == email.ToLower().Trim() && u.Status);
            }
            catch (Exception)
            {
                return null;
            }
        }
        // Static method to check if email exists
        public static bool EmailExists(EmployeeManagementContext context, string email, int? excludeUserId = null)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var query = context.Users.Where(u => u.Email.ToLower() == email.ToLower().Trim());

                if (excludeUserId.HasValue)
                {
                    query = query.Where(u => u.UserId != excludeUserId.Value);
                }

                return query.Any();
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Static method to check if username exists
        public static bool UsernameExists(EmployeeManagementContext context, string username, int? excludeUserId = null)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrWhiteSpace(username))
                return false;

            try
            {
                var query = context.Users.Where(u => u.Username.ToLower() == username.ToLower().Trim());

                if (excludeUserId.HasValue)
                {
                    query = query.Where(u => u.UserId != excludeUserId.Value);
                }

                return query.Any();
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Override ToString for debugging
        public override string ToString()
        {
            return $"User: {Username} ({Email}) - Status: {(Status ? "Active" : "Inactive")}";
        }
    }
}
