using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Model
{
    public class Attendance
    {
        [Key]
        public int AttendanceId { get; set; } // Khóa chính

        [Required]
        [ForeignKey("Employee")]
        public int UserId { get; set; } // Khóa ngoại liên kết với Users(UserId)

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; } // Ngày chấm công

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? ClockIn { get; set; } // Giờ vào

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? ClockOut { get; set; } // Giờ ra

        [Required]
        [StringLength(50)]
        public string Shift { get; set; } // Ca làm


        [Required]
        [StringLength(50)]
        public string Status { get; set; } // Trạng thái

        // Thuộc tính điều hướng
        public virtual Employee Employee { get; set; }

        // Constructor mặc định
        public Attendance()
        {
            Status = string.Empty;
            Shift = string.Empty;
        }

        // Constructor với tham số
        public Attendance(int userId, DateTime date, DateTime? clockIn, DateTime? clockOut, string status, string shift)
        {
            UserId = userId;
            Date = date;
            ClockIn = clockIn;
            ClockOut = clockOut;
            Status = status ?? throw new ArgumentNullException(nameof(status));
            Shift = shift ?? throw new ArgumentNullException(nameof(shift));
        }

        // Method để cập nhật trạng thái
        public void UpdateStatus(string newStatus) => Status = newStatus ?? throw new ArgumentNullException(nameof(newStatus));
        // Method để cập nhập ca làm
        public void UpdateShift(string newShift) => Shift = newShift ?? throw new ArgumentNullException(nameof(newShift));
    }


}
