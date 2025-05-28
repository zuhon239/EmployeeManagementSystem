﻿using EmployeeManagementSystem.Controller;
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
    public partial class AdminForm : Form
    {
        private Button currentButton;
        private Form ActiveForm;
        private readonly int _userId;
        private readonly EmployeeManagementContext _context;
        public AdminForm(int userId, EmployeeManagementContext context)
        {
            _userId = userId;
            _context = context;
            InitializeComponent();
            LoadManagerInfo();
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
                    lblAdminName.Text = $"{employee.Name}";
                }
                else
                {
                    MessageBox.Show($"Không tìm thấy nhân viên hoặc tên trống cho UserId: {_userId}");
                }
            }
            catch (Exception ex)
            {
                // Ghi log lỗi chi tiết
                System.Diagnostics.Debug.WriteLine($"Lỗi khi tải tên người dùng cho UserId {_userId}: {ex.Message}");
                MessageBox.Show($"Không thể tải thông tin người dùng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ActivateButton(object btnSender)
        {
            try
            {
                if (btnSender != null)
                {
                    if (currentButton != (Button)btnSender)
                    {
                        DisableButton();
                        currentButton = (Button)btnSender;
                        currentButton.BackColor = Color.FromArgb(73, 70, 243);
                        currentButton.ForeColor = Color.White;
                        currentButton.Font = new System.Drawing.Font("Segoe UI", 13F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{ex.Message}");
            }

        }
        private void DisableButton()
        {
            foreach (Control previousBtn in pnlMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(73, 40, 243);
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new System.Drawing.Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
                }
            }
        }
        private void OpenChildForm(Form ChildForm, object btnSender)
        {
            try
            {
                ActivateButton(btnSender);
                if (ActiveForm != null)
                {
                    ActiveForm.Close();
                }
                ActiveForm = ChildForm;
                ChildForm.TopLevel = false;
                ChildForm.FormBorderStyle = FormBorderStyle.None;
                ChildForm.Dock = DockStyle.Fill;
                this.pnlDesktopPane.Controls.Add(ChildForm);
                this.pnlDesktopPane.Tag
                    = ChildForm;
                ChildForm.BringToFront();
                ChildForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnDashBoard_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormAdmin.DashboardAdminForm(_userId), sender);
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormAdmin.EmployeeAdminForm(_userId, _context), sender);
        }

        private void btnAttendance_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormAdmin.AttendanceAdminForm(_userId), sender);
        }

        private void btnLeaveRequest_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormAdmin.LeaveRequestAdminForm(_userId, _context), sender);
        }

        private void btnSalary_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormAdmin.SalaryAdminForm(_userId), sender);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (ActiveForm != null)
            {
                ActiveForm.Close();
                DisableButton();
            }
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

        private void pnlDesktopPane_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
