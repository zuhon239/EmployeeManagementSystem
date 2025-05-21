using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Model
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; } // Khóa chính

        [Required]
        [StringLength(100)]
        public string Name { get; set; } // Tên phòng ban

        [ForeignKey("Manager")]
        public int? ManagerId { get; set; } // Khóa ngoại liên kết với Users(UserId)

        [Required]
        public bool Status { get; set; } // Trạng thái

        // Thuộc tính điều hướng
        public virtual Employee Manager { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }

        // Constructor mặc định
        public Department()
        {
            Name = string.Empty;
            Employees = new List<Employee>();
        }

        // Constructor với tham số
        public Department(string name, int? managerId, bool status)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ManagerId = managerId;
            Status = status;
            Employees = new List<Employee>();
        }

        // Method để cập nhật tên phòng ban
        public void UpdateName(string newName) => Name = newName ?? throw new ArgumentNullException(nameof(newName));
    }

}
