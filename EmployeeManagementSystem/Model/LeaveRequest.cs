using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Model
{
    public class LeaveRequest
    {
        [Key]
        public int LeaveId { get; set; } // Khóa chính

        [Required]
        [ForeignKey("Employee")]
        public int UserId { get; set; } // Khóa ngoại liên kết với Users(UserId)

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; } // Ngày bắt đầu nghỉ

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; } // Ngày kết thúc nghỉ

        [Required]
        [StringLength(500)]
        public string Reason { get; set; } // Lý do nghỉ phép

        [Required]
        [StringLength(50)]
        public string Status { get; set; } // Trạng thái
                                                   

        [StringLength(50)]
        public string Shift { get; set; } //  Ca nghỉ

        [ForeignKey("Approver")]
        public int? ApproverId { get; set; } // Khóa ngoại liên kết với Users(UserId)

        // Thuộc tính điều hướng
        public virtual Employee Employee { get; set; }
        public virtual Employee Approver { get; set; }

        // Constructor mặc định
        public LeaveRequest()
        {
            Reason = string.Empty;
            Status = string.Empty;
            Shift = string.Empty;
        }

        // Constructor với tham số
        public LeaveRequest(int userId, DateTime startDate, DateTime endDate, string reason, string status,string shift, int? approverId)
        {
            UserId = userId;
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason ?? throw new ArgumentNullException(nameof(reason));
            Status = status ?? throw new ArgumentNullException(nameof(status));
            Shift = shift ?? throw new ArgumentNullException(nameof(shift));
            ApproverId = approverId;
        }

        // Methods để cập nhật
        public void UpdateReason(string newReason) => Reason = newReason ?? throw new ArgumentNullException(nameof(newReason));
        public void UpdateShift(string newShift) => Shift = newShift ?? throw new ArgumentNullException(nameof(newShift));
        public void UpdateStatus(string newStatus) => Status = newStatus ?? throw new ArgumentNullException(nameof(newStatus));
    }
}
