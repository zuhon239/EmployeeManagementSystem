using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Model
{
    public class Employee : User
    {
        [Required]
        [StringLength(100)]
        public string Name { get;  set; } // Họ tên

        [Required]
        [StringLength(10)]
        public string Gender { get;  set; } // Giới tính

        [Required]
        public DateTime DateOfBirth { get;  set; } // Ngày sinh

        [StringLength(15)]
        public string Phone { get;  set; } // Số điện thoại

        [Required]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; } // Khóa ngoại liên kết với Departments

        [Required]
        [StringLength(50)]
        public string Position { get; private set; } // Vị trí công việc

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime HireDate { get; set; } // Ngày tuyển dụng

        // Thuộc tính điều hướng
        public virtual Department Department { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<LeaveRequest> LeaveRequests { get; set; }
        public virtual ICollection<Payroll> Payrolls { get; set; }
        public virtual ICollection<Department> ManagedDepartments { get; set; } // Phòng ban do nhân viên này quản lý
        public virtual ICollection<LeaveRequest> ApprovedLeaveRequests { get; set; } // Yêu cầu nghỉ phép do nhân viên này phê duyệt

        // Constructor mặc định cho EF Core
        public Employee() : base()
        {
            Name = string.Empty;
            Gender = string.Empty;
            Phone = string.Empty;
            Position = string.Empty;
            Attendances = new List<Attendance>();
            LeaveRequests = new List<LeaveRequest>();
            Payrolls = new List<Payroll>();
            ManagedDepartments = new List<Department>();
            ApprovedLeaveRequests = new List<LeaveRequest>();
        }

        // Constructor với tham số
        public Employee(
            string username, string password, int roleId, bool status,string email,
            string name, string gender, DateTime dateOfBirth, string phone,
            int departmentId, string position, DateTime hireDate)
            : base(username, password,email, roleId, status)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Gender = gender ?? throw new ArgumentNullException(nameof(gender));
            DateOfBirth = dateOfBirth;
            Phone = phone;
            DepartmentId = departmentId;
            Email = email;
            Position = position ?? throw new ArgumentNullException(nameof(position));
            HireDate = hireDate;
            Attendances = new List<Attendance>();
            LeaveRequests = new List<LeaveRequest>();
            Payrolls = new List<Payroll>();
            ManagedDepartments = new List<Department>();
            ApprovedLeaveRequests = new List<LeaveRequest>();
        }

        // Methods để cập nhật thông tin
        public void UpdateName(string newName) => Name = newName ?? throw new ArgumentNullException(nameof(newName));
        public void UpdateGender(string newGender) => Gender = newGender ?? throw new ArgumentNullException(nameof(newGender));
        public void UpdateDateOfBirth(DateTime newDateOfBirth) => DateOfBirth = newDateOfBirth;
        public void UpdatePhone(string newPhone) => Phone = newPhone;
        public void UpdatePosition(string newPosition) => Position = newPosition ?? throw new ArgumentNullException(nameof(newPosition));
    }


}
