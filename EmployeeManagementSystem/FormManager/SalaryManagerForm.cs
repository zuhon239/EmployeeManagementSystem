using EmployeeManagementSystem.Controller;
using EmployeeManagementSystem.Model;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagementSystem.FormManager
{
    public partial class SalaryManagerForm : Form
    {
        private readonly SalaryManagerController _controller;
        private readonly EmployeeManagementContext _context;
        private readonly int _managerId;
        private Employee _currentManager;

        public SalaryManagerForm(int managerId)
        {
            InitializeComponent();
            _managerId = managerId;
            _context = new EmployeeManagementContext();
            _controller = new SalaryManagerController(_context);

            LoadManagerInfo();
            LoadEmployees();
        }

        private async void LoadManagerInfo()
        {
            try
            {
                _currentManager = await _controller.GetManagerInfoAsync(_managerId);

                if (_currentManager != null)
                {
                    lblManagerInfo.Text = $"Phòng ban: {_currentManager.Department?.Name} - Manager: {_currentManager.Name}";
                }
                else
                {
                    lblManagerInfo.Text = "Không tìm thấy thông tin Manager";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải thông tin Manager: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void LoadEmployees()
        {
            try
            {
                var employees = await _controller.GetDepartmentEmployeesAsync(_managerId);

                cmbEmployee.Items.Clear();
                cmbEmployee.Items.Add("-- Chọn nhân viên --");

                foreach (var employee in employees)
                {
                    cmbEmployee.Items.Add($"{employee.Name} (ID: {employee.UserId})");
                }

                cmbEmployee.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách nhân viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ Cập nhật btnCalculate_Click với Bonus
        private async void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbEmployee.SelectedIndex <= 0)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ Lấy Bonus từ TextBox (thay vì lương cơ bản)
                if (!decimal.TryParse(txtBonus.Text, out decimal bonus) || bonus < 0)
                {
                    MessageBox.Show("Vui lòng nhập bonus hợp lệ (>= 0)!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string selectedText = cmbEmployee.SelectedItem.ToString();
                int userId = int.Parse(selectedText.Split('(')[1].Split(':')[1].Split(')')[0].Trim());

                DateTime selectedMonth = dtpMonth.Value;

                // ✅ Truyền bonus vào method tính lương
                var result = await _controller.CalculatePayrollAsync(userId, selectedMonth, bonus);

                DisplayPayrollResult(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tính lương: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ Cập nhật DisplayPayrollResult với Bonus
        // ✅ Cập nhật DisplayPayrollResult với logic mới
        private void DisplayPayrollResult(PayrollCalculationResult result)
        {
            lblEmployeeName.Text = $"Nhân viên: {result.EmployeeName}";
            lblMonth.Text = $"Tháng: {result.Month:MM/yyyy}";
            lblBaseSalary.Text = $"Lương cơ bản: {result.BaseSalary:N0} VNĐ";
            lblDailySalary.Text = $"Lương ngày: {result.DailySalary:N0} VNĐ";
            lblTotalDeduction.Text = $"Tổng khấu trừ: {result.TotalDeduction:N0} VNĐ";
            lblBonus.Text = $"Bonus: {result.Bonus:N0} VNĐ";
            lblTotalSalary.Text = $"Lương thực nhận: {result.TotalSalary:N0} VNĐ";

            dgvDetails.DataSource = result.AttendanceDetails.Select(d => new
            {
                Ngày = d.Date.ToString("dd/MM/yyyy"),
                Thứ = d.Date.ToString("dddd"),
                Ca_Sáng = d.MorningShift?.Status ?? "Vắng mặt",
                Ca_Chiều = d.AfternoonShift?.Status ?? "Vắng mặt",
                Leave_Request = d.LeaveRequest?.Status ?? "Không",
                Khấu_Trừ = d.DeductionAmount.ToString("N0") + " VNĐ",
                Lý_Do = d.DeductionReason
            }).ToList();

            dgvDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // ✅ Color coding với logic mới
            foreach (DataGridViewRow row in dgvDetails.Rows)
            {
                var leaveStatus = row.Cells["Leave_Request"].Value.ToString();
                var deduction = row.Cells["Khấu_Trừ"].Value.ToString();
                var reason = row.Cells["Lý_Do"].Value.ToString();

                if (leaveStatus == "Approved")
                {
                    row.DefaultCellStyle.BackColor = Color.LightBlue; // Nghỉ phép approved
                }
                else if (reason.Contains("Vắng mặt nguyên ngày"))
                {
                    row.DefaultCellStyle.BackColor = Color.DarkRed; // ✅ Đỏ đậm - Vắng mặt nguyên ngày
                    row.DefaultCellStyle.ForeColor = Color.White;
                }
                else if (deduction.Contains("0 VNĐ"))
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen; // Không trừ lương
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.LightPink; // Bị trừ 5%
                }
            }
        }


        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDetails.DataSource == null)
                {
                    MessageBox.Show("Chưa có dữ liệu để lưu!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string selectedText = cmbEmployee.SelectedItem.ToString();
                int userId = int.Parse(selectedText.Split('(')[1].Split(':')[1].Split(')')[0].Trim());
                decimal bonus = decimal.Parse(txtBonus.Text);
                DateTime selectedMonth = dtpMonth.Value;

                var result = await _controller.CalculatePayrollAsync(userId, selectedMonth, bonus);
                bool saved = await _controller.SavePayrollAsync(result);

                if (saved)
                {
                    MessageBox.Show("Lưu bảng lương thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Lỗi khi lưu bảng lương!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu bảng lương: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            cmbEmployee.SelectedIndex = 0;
            txtBonus.Text = "0"; // ✅ Reset về 0 thay vì 3000000
            dtpMonth.Value = DateTime.Now;
            dgvDetails.DataSource = null;

            lblEmployeeName.Text = "Nhân viên: ";
            lblMonth.Text = "Tháng: ";
            lblBaseSalary.Text = "Lương cơ bản: ";
            lblDailySalary.Text = "Lương ngày: ";
            lblTotalDeduction.Text = "Tổng khấu trừ: ";
            lblBonus.Text = "Bonus: "; // ✅ Reset Bonus
            lblTotalSalary.Text = "Lương thực nhận: ";
        }

        private void SalaryManagerForm_Load(object sender, EventArgs e)
        {
            // Form load
        }

        private void gbResult_Enter(object sender, EventArgs e)
        {

        }

        private void gbResult_Enter_1(object sender, EventArgs e)
        {

        }

        private void lblWarning_Click(object sender, EventArgs e)
        {

        }
    }
}
