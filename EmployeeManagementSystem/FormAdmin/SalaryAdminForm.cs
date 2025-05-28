using EmployeeManagementSystem.Controller;
using EmployeeManagementSystem.Model;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagementSystem.FormAdmin
{
    public partial class SalaryAdminForm : Form
    {
        private readonly SalaryAdminController _controller;
        private readonly EmployeeManagementContext _context;
        private readonly int _adminId;
        private Employee _currentAdmin;
        private bool _isDetailView = false;

        public SalaryAdminForm(int adminId)
        {
            InitializeComponent();
            _adminId = adminId;
            _context = new EmployeeManagementContext();
            _controller = new SalaryAdminController(_context);

          
            LoadFilterOptions();
            _ = LoadSalaryReportAsync();
        }


        private async void LoadFilterOptions()
        {
            try
            {
                // Load department filter
                cmbDepartmentFilter.Items.Clear();
                cmbDepartmentFilter.Items.Add("Tất cả phòng ban");

                var departments = await _controller.GetAllDepartmentsForAdminAsync();
                foreach (var dept in departments)
                {
                    cmbDepartmentFilter.Items.Add($"{dept.Name} (ID: {dept.DepartmentId})");
                }
                cmbDepartmentFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi LoadFilterOptions: {ex.Message}");
            }
        }

        private async void dtpMonth_ValueChanged(object sender, EventArgs e)
        {
            await LoadSalaryReportAsync();
        }

        private async void cmbDepartmentFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadSalaryReportAsync();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadSalaryReportAsync();
            MessageBox.Show("Đã làm mới dữ liệu!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnToggleView_Click(object sender, EventArgs e)
        {
            _isDetailView = !_isDetailView;
            btnToggleView.Text = _isDetailView ? "Xem Tổng Quan" : "Xem Chi Tiết";
            await LoadSalaryReportAsync();
        }

        private async Task LoadSalaryReportAsync()
        {
            try
            {
                DateTime selectedMonth = dtpMonth.Value;

                int? departmentFilter = null;
                if (cmbDepartmentFilter.SelectedIndex > 0)
                {
                    string selectedDept = cmbDepartmentFilter.SelectedItem.ToString();
                    departmentFilter = int.Parse(selectedDept.Split('(')[1].Split(':')[1].Split(')')[0].Trim());
                }

                dgvSalaryReport.Columns.Clear();
                dgvSalaryReport.Rows.Clear();

                if (_isDetailView)
                {
                    await LoadDetailedView(selectedMonth, departmentFilter);
                }
                else
                {
                    await LoadOverviewReport(selectedMonth, departmentFilter);
                }

                ColorCodeRows();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải báo cáo lương: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadOverviewReport(DateTime selectedMonth, int? departmentFilter)
        {
            var reportData = await _controller.GetCompanySalaryReportForAdminAsync(selectedMonth, departmentFilter);

            if (reportData.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu lương!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Add columns for overview
            dgvSalaryReport.Columns.Add("UserId", "User ID");
            dgvSalaryReport.Columns.Add("EmployeeName", "Tên Nhân Viên");
            dgvSalaryReport.Columns.Add("Position", "Chức Vụ");
            dgvSalaryReport.Columns.Add("DepartmentName", "Phòng Ban");
            dgvSalaryReport.Columns.Add("RoleName", "Vai Trò");
            dgvSalaryReport.Columns.Add("BaseSalary", "Lương Cơ Bản");
            dgvSalaryReport.Columns.Add("Bonus", "Bonus");
            dgvSalaryReport.Columns.Add("Deduction", "Khấu Trừ");
            dgvSalaryReport.Columns.Add("TotalSalary", "Lương Thực Nhận");
            dgvSalaryReport.Columns.Add("Status", "Trạng Thái");

            foreach (var item in reportData)
            {
                dgvSalaryReport.Rows.Add(
                    item.UserId,
                    item.EmployeeName,
                    item.Position,
                    item.DepartmentName,
                    item.RoleName,
                    item.BaseSalary.ToString("N0") + " VNĐ",
                    item.Bonus.ToString("N0") + " VNĐ",
                    item.Deduction.ToString("N0") + " VNĐ",
                    item.TotalSalary.ToString("N0") + " VNĐ",
                    item.HasPayroll ? "Đã tính lương" : "Chưa tính lương"
                );
            }

            // Style columns
            dgvSalaryReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSalaryReport.Columns["UserId"].Width = 70;
            dgvSalaryReport.Columns["EmployeeName"].Width = 150;
            dgvSalaryReport.Columns["Position"].Width = 100;
            dgvSalaryReport.Columns["DepartmentName"].Width = 120;
            dgvSalaryReport.Columns["RoleName"].Width = 80;
            dgvSalaryReport.Columns["BaseSalary"].Width = 120;
            dgvSalaryReport.Columns["Bonus"].Width = 100;
            dgvSalaryReport.Columns["Deduction"].Width = 100;
            dgvSalaryReport.Columns["TotalSalary"].Width = 130;
            dgvSalaryReport.Columns["Status"].Width = 120;
        }

        private async Task LoadDetailedView(DateTime selectedMonth, int? departmentFilter)
        {
            // Implementation for detailed view would show individual employee calculation
            // For now, show the same overview with additional context menu
            await LoadOverviewReport(selectedMonth, departmentFilter);
        }

        private async void btnCalculateSelected_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSalaryReport.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên để tính lương!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedRow = dgvSalaryReport.SelectedRows[0];
                int userId = (int)selectedRow.Cells["UserId"].Value;
                string employeeName = selectedRow.Cells["EmployeeName"].Value.ToString();

                var bonusForm = new BonusInputForm(employeeName);
                if (bonusForm.ShowDialog() == DialogResult.OK)
                {
                    DateTime selectedMonth = dtpMonth.Value;
                    decimal bonus = bonusForm.BonusAmount;

                    var result = await _controller.CalculatePayrollForAdminAsync(userId, selectedMonth, bonus);
                    bool saved = await _controller.SavePayrollForAdminAsync(result);

                    if (saved)
                    {
                        MessageBox.Show($"Đã tính và lưu lương cho {employeeName}!\nLương thực nhận: {result.TotalSalary:N0} VNĐ",
                            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadSalaryReportAsync();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi khi lưu lương!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tính lương: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnCalculateAll_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Bạn có chắc muốn tính lương cho tất cả nhân viên?\nBonus sẽ được đặt là 0 cho tất cả.",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    DateTime selectedMonth = dtpMonth.Value;
                    int successCount = 0;
                    int totalCount = dgvSalaryReport.Rows.Count;

                    foreach (DataGridViewRow row in dgvSalaryReport.Rows)
                    {
                        try
                        {
                            int userId = (int)row.Cells["UserId"].Value;
                            var payrollResult = await _controller.CalculatePayrollForAdminAsync(userId, selectedMonth, 0m);
                            bool saved = await _controller.SavePayrollForAdminAsync(payrollResult);
                            if (saved) successCount++;
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"Lỗi tính lương cho UserId {row.Cells["UserId"].Value}: {ex.Message}");
                        }
                    }

                    MessageBox.Show($"Đã tính lương thành công cho {successCount}/{totalCount} nhân viên!",
                        "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadSalaryReportAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tính lương hàng loạt: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ColorCodeRows()
        {
            foreach (DataGridViewRow row in dgvSalaryReport.Rows)
            {
                var roleName = row.Cells["RoleName"].Value?.ToString() ?? "";
                var status = row.Cells["Status"].Value?.ToString() ?? "";

                Color rowColor = Color.White;

                if (status == "Chưa tính lương")
                {
                    rowColor = Color.LightYellow;
                }
                else if (status == "Đã tính lương")
                {
                    rowColor = Color.LightGreen;
                }

                // Highlight Manager và Admin
                if (roleName == "Manager")
                {
                    row.DefaultCellStyle.Font = new Font(dgvSalaryReport.Font, FontStyle.Bold);
                    row.DefaultCellStyle.ForeColor = Color.Blue;
                }
                else if (roleName == "Admin")
                {
                    row.DefaultCellStyle.Font = new Font(dgvSalaryReport.Font, FontStyle.Bold);
                    row.DefaultCellStyle.ForeColor = Color.Red;
                }

                row.DefaultCellStyle.BackColor = rowColor;
            }
        }

        private void SalaryAdminForm_Load(object sender, EventArgs e)
        {
            // Form load
        }

        private void lblAdminInfo_Click(object sender, EventArgs e)
        {

        }
    }

    // ✅ Form nhập Bonus
    public partial class BonusInputForm : Form
    {
        public decimal BonusAmount { get; private set; }

        public BonusInputForm(string employeeName)
        {
            InitializeComponent();
            this.Text = $"Nhập Bonus cho {employeeName}";
            lblEmployee.Text = $"Nhân viên: {employeeName}";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtBonus.Text, out decimal bonus) && bonus >= 0)
            {
                BonusAmount = bonus;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập bonus hợp lệ (>= 0)!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void InitializeComponent()
        {
            lblEmployee = new Label();
            lblBonusLabel = new Label();
            txtBonus = new TextBox();
            btnOK = new Button();
            btnCancel = new Button();

            // Form
            Size = new Size(350, 200);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            // lblEmployee
            lblEmployee.Location = new Point(20, 20);
            lblEmployee.Size = new Size(300, 20);
            lblEmployee.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);

            // lblBonusLabel
            lblBonusLabel.Location = new Point(20, 60);
            lblBonusLabel.Size = new Size(60, 20);
            lblBonusLabel.Text = "Bonus:";

            // txtBonus
            txtBonus.Location = new Point(90, 60);
            txtBonus.Size = new Size(120, 20);
            txtBonus.Text = "0";
            txtBonus.TextAlign = HorizontalAlignment.Right;

            // btnOK
            btnOK.Location = new Point(140, 120);
            btnOK.Size = new Size(75, 30);
            btnOK.Text = "OK";
            btnOK.Click += btnOK_Click;

            // btnCancel
            btnCancel.Location = new Point(230, 120);
            btnCancel.Size = new Size(75, 30);
            btnCancel.Text = "Hủy";
            btnCancel.Click += btnCancel_Click;

            Controls.AddRange(new Control[] { lblEmployee, lblBonusLabel, txtBonus, btnOK, btnCancel });
        }

        private Label lblEmployee;
        private Label lblBonusLabel;
        private TextBox txtBonus;
        private Button btnOK;
        private Button btnCancel;
    }
}
