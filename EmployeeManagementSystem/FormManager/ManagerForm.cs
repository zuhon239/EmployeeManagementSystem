using EmployeeManagementSystem.Controller;
using EmployeeManagementSystem.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagementSystem
{
    public partial class ManagerForm : Form
    {
        private readonly int _userId;
        private readonly EmployeeManagementContext _context; 
        private readonly LeaveRequestController _leaveRequestController; 
        private string _managerName;
        private string _departmentName;
        public ManagerForm(int userId, LeaveRequestController leaveRequestController, EmployeeManagementContext context)
        {
            _userId = userId;
            _context = context;
            _leaveRequestController = leaveRequestController;
            InitializeComponent();
            LoadManagerInfo();
            LoadDepartmentInfo();
        }
        private void LoadManagerInfo()
        {
            try
            {
                var employee = _context.Users
               .OfType<Employee>()
               .AsNoTracking()
               .Select(e => new { e.UserId, e.Name })
               .FirstOrDefault(e => e.UserId == _userId);

                if (employee != null && !string.IsNullOrEmpty(employee.Name))
                {
                    lblWelcomeManager.Text = $"Chào mừng Manager {employee.Name} đến với hệ thống quản lý nhân sự!";
                }
                else
                {
                    lblWelcomeManager.Text = $"Chào mừng UserId {_userId} đến với hệ thống quản lý nhân sự!";
                    MessageBox.Show($"Không tìm thấy nhân viên hoặc tên trống cho UserId: {_userId}");
                }
            }
            catch (Exception ex)
            {
                lblWelcomeManager.Text = "Chào mừng đến với hệ thống quản lý nhân sự!";
                // Ghi log lỗi chi tiết
                System.Diagnostics.Debug.WriteLine($"Lỗi khi tải tên người dùng cho UserId {_userId}: {ex.Message}");
                MessageBox.Show($"Không thể tải thông tin người dùng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadDepartmentInfo()
        {
            try
            {
                var department = _context.Users
               .OfType<Employee>()
               .AsNoTracking().Include(e => e.Department)
               .Select(e => new { e.UserId, 
                                  e.DepartmentId, 
                                  e.Department.Name })
                                  .FirstOrDefault(e => e.UserId == _userId); 
              
                if (department != null && !string.IsNullOrEmpty(department.Name))
                {
                    lblWelcome.Text = $"{department.Name}";
                }
                else
                {
                   
                    MessageBox.Show($"Không tìm thấy nhân viên hoặc tên trống cho UserId: {Name}");
                }
            }
            catch (Exception ex)
            {
                lblWelcomeManager.Text = "Chào mừng đến với hệ thống quản lý nhân sự!";
                // Ghi log lỗi chi tiết
                System.Diagnostics.Debug.WriteLine($"Lỗi khi tải tên người dùng cho UserId {_userId}: {ex.Message}");
                MessageBox.Show($"Không thể tải thông tin người dùng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnAttendance_Click(object sender, EventArgs e)
        {

        }

        private void BtnLeaveRequest_Click(object sender, EventArgs e)
        {
            // Mở form xin nghỉ phép
            var leaveRequestForm = new LeaveRequestForm(_userId, _leaveRequestController);
            leaveRequestForm.ShowDialog();
        }

        private void BtnManageEmployees_Click(object sender, EventArgs e)
        {

        }
    }
}
