using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Model
{
    public class EmployeeManagementContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public string DbPath { get; }

        public EmployeeManagementContext()        
        {
            var folder = Path.Combine(AppContext.BaseDirectory, "Data");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            DbPath = Path.Combine(folder, "database.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình TPH cho User và Employee
            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("UserType")
                .HasValue<User>("User")
                .HasValue<Employee>("Employee");

            // User - Role (N-1)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Employee - Department (N-1)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Department - Employee (Manager, 1-N)
            modelBuilder.Entity<Department>()
                .HasOne(d => d.Manager)
                .WithMany(e => e.ManagedDepartments)
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.SetNull);

            // Attendance - Employee (N-1)
            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Employee)
                .WithMany(e => e.Attendances)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // LeaveRequest - Employee (Người yêu cầu, N-1)
            modelBuilder.Entity<LeaveRequest>()
                .HasOne(lr => lr.Employee)
                .WithMany(e => e.LeaveRequests)
                .HasForeignKey(lr => lr.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // LeaveRequest - Employee (Người phê duyệt, N-1)
            modelBuilder.Entity<LeaveRequest>()
                .HasOne(lr => lr.Approver)
                .WithMany(e => e.ApprovedLeaveRequests)
                .HasForeignKey(lr => lr.ApproverId)
                .OnDelete(DeleteBehavior.SetNull);

            // Payroll - Employee (N-1)
            modelBuilder.Entity<Payroll>()
                .HasOne(p => p.Employee)
                .WithMany(e => e.Payrolls)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Dữ liệu khởi tạo cho Role
            modelBuilder.Entity<Role>().HasData(
                new Role("Employee") { RoleId = 1 },
                new Role("Manager") { RoleId = 2 },
                new Role("Admin") { RoleId = 3 }
            );        
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
    }
}
