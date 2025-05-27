using EmployeeManagementSystem.Controller;
using EmployeeManagementSystem.FormManager;
using EmployeeManagementSystem.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly LoginController _loginController;
        private readonly AttendanceController _attendanceController;
        public ManagerForm(int userId, LeaveRequestController leaveRequestController, EmployeeManagementContext context, AttendanceController attendanceController)
        {
            _userId = userId;
            _context = context;
            _leaveRequestController = leaveRequestController;
            InitializeComponent();
            LoadManagerInfo();
            LoadDepartmentInfo();
            _attendanceController = attendanceController;
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
                   .AsNoTracking()
                   .Include(e => e.Department)
                   .Select(e => new {
                       e.UserId,
                       e.DepartmentId,
                       DepartmentName = e.Department.Name
                   })
                   .FirstOrDefault(e => e.UserId == _userId);

                if (department != null && !string.IsNullOrEmpty(department.DepartmentName))
                {
                    _departmentName = department.DepartmentName;
                    lblWelcome.Text = $"{department.DepartmentName.ToUpper()}";
                }
                else
                {
                    lblWelcome.Text = "PHÒNG QUẢN LÝ";
                    System.Diagnostics.Debug.WriteLine($"Không tìm thấy thông tin phòng ban cho UserId: {_userId}");
                }
            }
            catch (Exception ex)
            {
                lblWelcome.Text = "PHÒNG QUẢN LÝ";
                System.Diagnostics.Debug.WriteLine($"Lỗi khi tải thông tin phòng ban cho UserId {_userId}: {ex.Message}");
                MessageBox.Show($"Không thể tải thông tin phòng ban: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void BtnAttendance_Click(object sender, EventArgs e)
        {
            var attendanceForm = new AttendanceForm(_userId);
            attendanceForm.ShowDialog();
        }

        private void BtnLeaveRequest_Click(object sender, EventArgs e)
        {
            var leaveRequestForm = new LeaveRequestForm(_userId, _leaveRequestController, _context);
            leaveRequestForm.ShowDialog();
        }

        private void BtnManageEmployees_Click(object sender, EventArgs e)
        {
            var managerform = new DepartmentManagerForm(_userId, _context);
            managerform.ShowDialog();
        }
        private void BtnLogout_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show(
                    "Bạn có chắc chắn muốn đăng xuất khỏi hệ thống?",
                    "Xác nhận đăng xuất",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    
                    this.Hide();

                    using (var serviceProvider = new ServiceCollection()
                        .AddDbContext<EmployeeManagementContext>()
                        .AddScoped<LeaveRequestController>()
                        .AddScoped<AttendanceController>()
                        .AddScoped<LoginController>()
                        .AddScoped<LoginForm>()
                        .BuildServiceProvider())
                    {
                        var loginForm = serviceProvider.GetService<LoginForm>();
                        loginForm.ShowDialog();
                    }

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đăng xuất: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
