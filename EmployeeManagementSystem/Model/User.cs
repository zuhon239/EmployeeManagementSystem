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
        }

        // Constructor với tham số
        public User(string username, string password, int roleId, bool status)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            RoleId = roleId;
            Status = status;
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
        public static User Authenticate(EmployeeManagementContext context, string username, string password)
        {
            return context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Username == username && u.Password == password && u.Status);
        }
    }
}
