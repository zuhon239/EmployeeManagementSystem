using EmployeeManagementSystem.Controller;
using EmployeeManagementSystem.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagementSystem.FormManager
{

    public partial class DepartmentManagerForm : Form
    {
        private Button currentButton;
        private Form ActiveForm;
        private readonly int _userId;
        private readonly EmployeeManagementContext _context;
        public DepartmentManagerForm(int userId, EmployeeManagementContext context)
        {
            _userId = userId;
            _context = context;
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
                    lblManagerName.Text = $"{employee.Name}";
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
        private void LoadDepartmentInfo()
        {
            try
            {
                var department = _context.Users
               .OfType<Employee>()
               .AsNoTracking().Include(e => e.Department)
               .Select(e => new
               {
                   e.UserId,
                   e.DepartmentId,
                   e.Department.Name
               })
                                  .FirstOrDefault(e => e.UserId == _userId);

                if (department != null && !string.IsNullOrEmpty(department.Name))
                {
                    lblDepartment.Text = $"{department.Name}";
                }
                else
                {

                    MessageBox.Show($"Không tìm thấy nhân viên hoặc tên trống cho UserId: {Name}");
                }
            }
            catch (Exception ex)
            {
                // Ghi log lỗi chi tiết
                System.Diagnostics.Debug.WriteLine($"Lỗi khi tải tên người dùng cho UserId {_userId}: {ex.Message}");
                MessageBox.Show($"Không thể tải thông tin người dùng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void OpenChildForm (Form ChildForm, object btnSender)
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
            this.pnlDesktopPane.Controls.Add( ChildForm );
            this.pnlDesktopPane.Tag
                = ChildForm;
            ChildForm.BringToFront();
            ChildForm.Show();
            if (ChildForm.Text == "LeaveRequestManager")
            {
                lblDepartment.Text = "Quản lý nghỉ phép";
            }
        }
        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = Color.FromArgb(45, 45, 76);
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }
        private void DisableButton()
        {
            foreach (Control previousBtn in pnlMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(51, 51, 76);
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }

        private void btnBackToMenu_Click(object sender, EventArgs e)
        {
            if (ActiveForm != null)
            {
                ActiveForm.Close();
                Reset();
            }
        }
        private void Reset()
        {
            DisableButton();
            LoadDepartmentInfo();           
        }

        private void lblManagerName_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormManager.LeaveRequestManager(_userId, _context), sender);
        }

        private void btnAttendance_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormManager.LeaveRequestManager(_userId, _context), sender);
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormManager.LeaveRequestManager(_userId, _context), sender);
        }

        private void btnManager_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormManager.LeaveRequestManager(_userId, _context), sender);
        }

        private void btnLeaveRequest_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormManager.LeaveRequestManager(_userId, _context), sender);
        }

        private void btnSalary_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormManager.LeaveRequestManager(_userId, _context), sender);
        }
    }
}
