﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Model
{
    public class Payroll
    {
        [Key]
        public int PayrollId { get; set; } // Khóa chính

        [Required]
        [ForeignKey("Employee")]
        public int UserId { get; set; } // Khóa ngoại liên kết với Users(UserId)

        [Required]
        public DateTime Month { get; set; } // Tháng lương

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal BaseSalary { get; set; } // Lương cơ bản

        [Required]
        public int DaysWorked { get; set; } // Số ngày làm việc

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Bonus { get; set; } // Tiền thưởng

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Deduction { get; set; } // Tiền thưởng

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalSalary { get; set; } // Tổng lương

        // Thuộc tính điều hướng
        public virtual Employee Employee { get; set; }

        // Constructor mặc định
        public Payroll()
        {
        }

        // Constructor với tham số
        public Payroll(int userId, DateTime month, decimal baseSalary, int daysWorked, decimal? bonus, decimal? deduction, decimal totalSalary)
        {
            UserId = userId;
            Month = month;
            BaseSalary = baseSalary;
            DaysWorked = daysWorked;
            Bonus = bonus;
            Deduction = deduction;
            TotalSalary = totalSalary;
        }

        // Methods để cập nhật
        public void UpdateBaseSalary(decimal newBaseSalary) => BaseSalary = newBaseSalary;
        public void UpdateBonus(decimal newBonus) => Bonus = newBonus;
        public void UpdateTotalSalary(decimal newTotalSalary) => TotalSalary = newTotalSalary;
    }
}
