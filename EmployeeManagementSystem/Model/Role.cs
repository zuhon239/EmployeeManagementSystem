using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Model
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; } // Khóa chính

        [Required]
        [StringLength(50)]
        public string RoleName { get; private set; } // Tên vai trò

        // Thuộc tính điều hướng
        public virtual ICollection<User> Users { get; set; }

        // Constructor mặc định
        public Role()
        {
            RoleName = string.Empty;
            Users = new List<User>();
        }

        // Constructor với tham số
        public Role(string roleName)
        {
            RoleName = roleName ?? throw new ArgumentNullException(nameof(roleName));
            Users = new List<User>();
        }

        // Method để cập nhật tên vai trò
        public void UpdateRoleName(string newRoleName) => RoleName = newRoleName ?? throw new ArgumentNullException(nameof(newRoleName));
    }
}
