using EmployeeManagementSystem.Controller;
using EmployeeManagementSystem.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.ApplicationServices;
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
    public partial class EmployeeForm : Form
    {
        private readonly int _userId;
        private readonly LeaveRequestController _leaveRequestController;
        private readonly EmployeeManagementContext _context;
        private readonly AttendanceController _attendanceController;
        public EmployeeForm(int userId, LeaveRequestController leaveRequestController, EmployeeManagementContext context, AttendanceController attendanceController)
        {
            InitializeComponent();
            _userId = userId;
            _leaveRequestController = leaveRequestController ?? throw new ArgumentNullException(nameof(leaveRequestController));
            _attendanceController = attendanceController ?? throw new ArgumentNullException(nameof(attendanceController));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            LoadUserName();
        }
        private void LoadUserName()
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
                    lblWelcome.Text = $"Chào mừng {employee.Name} đến với hệ thống quản lý nhân sự!";
                }
                else
                {
                    lblWelcome.Text = $"Chào mừng UserId {_userId} đến với hệ thống quản lý nhân sự!";
                    MessageBox.Show($"Không tìm thấy nhân viên hoặc tên trống cho UserId: {_userId}");
                }
            }
            catch (Exception ex)
            {
                lblWelcome.Text = "Chào mừng đến với hệ thống quản lý nhân sự!";
                // Ghi log lỗi chi tiết
                System.Diagnostics.Debug.WriteLine($"Lỗi khi tải tên người dùng cho UserId {_userId}: {ex.Message}");
                MessageBox.Show($"Không thể tải thông tin người dùng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnAttendance_Click(object sender, EventArgs e)
        {
            var attendanceForm = new AttendanceForm(_userId);
            attendanceForm.ShowDialog();
        }
        private void BtnRequestLeave_Click(object sender, EventArgs e)
        {
            var leaveRequestForm = new LeaveRequestForm(_userId, _leaveRequestController, _context);
            leaveRequestForm.ShowDialog();
        }
        private void BtnLogout_Click(object sender, EventArgs e)
        {
            try
            {
                // Xác nhận đăng xuất
                var result = MessageBox.Show(
                    "Bạn có chắc chắn muốn đăng xuất khỏi hệ thống?",
                    "Xác nhận đăng xuất",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    // Ẩn form hiện tại
                    this.Hide();

                    // Tạo và hiển thị form đăng nhập mới
                    using (var serviceProvider = new ServiceCollection()
                        .AddDbContext<EmployeeManagementContext>()
                        .AddScoped<LeaveRequestController>()
                        .AddScoped<LoginController>()
                        .AddScoped<LoginForm>()
                        .AddScoped<AttendanceController>()
                        .BuildServiceProvider())
                    {
                        var loginForm = serviceProvider.GetService<LoginForm>();
                        loginForm.ShowDialog();
                    }

                    // Đóng form hiện tại sau khi đăng xuất
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đăng xuất: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Lỗi khi đăng xuất: {ex.Message}");
            }
        }
    }
}
