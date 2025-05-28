using EmployeeManagementSystem.Controller;
using EmployeeManagementSystem.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagementSystem.FormAdmin
{
    public partial class AttendanceAdminForm : Form
    {
        private readonly AttendanceAdminController _controller;
        private readonly EmployeeManagementContext _context;
        private readonly int _adminId;
        private Employee _currentAdmin;

        public AttendanceAdminForm(int adminId)
        {
            InitializeComponent();
            _adminId = adminId;
            _context = new EmployeeManagementContext();
            _controller = new AttendanceAdminController(_context);


            LoadFilterOptions();
            _ = LoadAttendanceReportAsync();
        }



        private async void LoadFilterOptions()
        {
            try
            {
                // Load department filter
                cmbDepartmentFilter.Items.Clear();
                cmbDepartmentFilter.Items.Add("Tất cả phòng ban");

                var departments = await _controller.GetAllDepartmentsAsync();
                foreach (var dept in departments)
                {
                    cmbDepartmentFilter.Items.Add($"{dept.Name} (ID: {dept.DepartmentId})");
                }
                cmbDepartmentFilter.SelectedIndex = 0;

                // Load shift filter
                cmbShiftFilter.Items.Clear();
                cmbShiftFilter.Items.Add("All");
                cmbShiftFilter.Items.Add("Sáng");
                cmbShiftFilter.Items.Add("Chiều");
                cmbShiftFilter.SelectedIndex = 0;

                // Load status filter
                cmbStatusFilter.Items.Clear();
                cmbStatusFilter.Items.Add("All");
                cmbStatusFilter.Items.Add("Đúng giờ");
                cmbStatusFilter.Items.Add("Đi trễ");
                cmbStatusFilter.Items.Add("Về sớm");
                cmbStatusFilter.Items.Add("Vắng mặt");
                cmbStatusFilter.Items.Add("Chưa check out");

                var statuses = await _controller.GetAvailableStatusesAsync();
                foreach (var status in statuses)
                {
                    if (!cmbStatusFilter.Items.Contains(status))
                    {
                        cmbStatusFilter.Items.Add(status);
                    }
                }
                cmbStatusFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi LoadFilterOptions: {ex.Message}");
            }
        }

        private async void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            await LoadAttendanceReportAsync();
        }

        private async void cmbDepartmentFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadAttendanceReportAsync();
        }

        private async void cmbShiftFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadAttendanceReportAsync();
        }

        private async void cmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadAttendanceReportAsync();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadAttendanceReportAsync();
            MessageBox.Show("Đã làm mới dữ liệu!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async Task LoadAttendanceReportAsync()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"=== LOAD ADMIN ATTENDANCE REPORT ===");

                DateTime selectedDate = dtpDate.Value.Date;

                // Lấy department filter
                int? departmentFilter = null;
                if (cmbDepartmentFilter.SelectedIndex > 0)
                {
                    string selectedDept = cmbDepartmentFilter.SelectedItem.ToString();
                    departmentFilter = int.Parse(selectedDept.Split('(')[1].Split(':')[1].Split(')')[0].Trim());
                }

                string shiftFilter = cmbShiftFilter.SelectedItem?.ToString() ?? "All";
                string statusFilter = cmbStatusFilter.SelectedItem?.ToString() ?? "All";

                // Clear existing data
                dgvAttendanceReport.Columns.Clear();
                dgvAttendanceReport.Rows.Clear();

                // Lấy dữ liệu
                var reportData = await _controller.GetDailyAttendanceReportAsync(selectedDate, departmentFilter, shiftFilter, statusFilter);

                System.Diagnostics.Debug.WriteLine($"✅ Controller trả về {reportData.Count} records");

                if (reportData.Count == 0)
                {
                    string message = "Không có dữ liệu phù hợp với filter đã chọn!";
                    MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Add columns
                dgvAttendanceReport.Columns.Add("UserId", "User ID");
                dgvAttendanceReport.Columns.Add("EmployeeName", "Tên Nhân Viên");
                dgvAttendanceReport.Columns.Add("Position", "Chức Vụ");
                dgvAttendanceReport.Columns.Add("DepartmentName", "Phòng Ban");
                dgvAttendanceReport.Columns.Add("RoleName", "Vai Trò");
                dgvAttendanceReport.Columns.Add("Shift", "Ca");
                dgvAttendanceReport.Columns.Add("ClockIn", "Giờ Check In");
                dgvAttendanceReport.Columns.Add("ClockOut", "Giờ Check Out");
                dgvAttendanceReport.Columns.Add("Status", "Trạng Thái");

                // Add data rows
                foreach (var item in reportData)
                {
                    dgvAttendanceReport.Rows.Add(
                        item.UserId,
                        item.EmployeeName,
                        item.Position,
                        item.DepartmentName,
                        item.RoleName,
                        item.Shift,
                        item.ClockIn?.ToString("HH:mm:ss") ?? "",
                        item.ClockOut?.ToString("HH:mm:ss") ?? "",
                        item.Status ?? ""
                    );
                }

                // Style the grid
                dgvAttendanceReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvAttendanceReport.Columns["UserId"].Width = 70;
                dgvAttendanceReport.Columns["EmployeeName"].Width = 150;
                dgvAttendanceReport.Columns["Position"].Width = 100;
                dgvAttendanceReport.Columns["DepartmentName"].Width = 120;
                dgvAttendanceReport.Columns["RoleName"].Width = 80;
                dgvAttendanceReport.Columns["Shift"].Width = 60;
                dgvAttendanceReport.Columns["ClockIn"].Width = 100;
                dgvAttendanceReport.Columns["ClockOut"].Width = 100;
                dgvAttendanceReport.Columns["Status"].Width = 150;

                // Color code rows
                ColorCodeRows();



                System.Diagnostics.Debug.WriteLine($"✅ Hiển thị thành công {dgvAttendanceReport.Rows.Count} rows");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi LoadAttendanceReportAsync: {ex.Message}");
                MessageBox.Show($"Lỗi tải báo cáo: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void ColorCodeRows()
        {
            foreach (DataGridViewRow row in dgvAttendanceReport.Rows)
            {
                var status = row.Cells["Status"].Value?.ToString() ?? "";
                var roleName = row.Cells["RoleName"].Value?.ToString() ?? "";

                Color rowColor = Color.White;

                // Color theo status
                if (status.Contains("Chưa check out"))
                {
                    rowColor = Color.LightBlue;
                }
                else if (status == "Đúng giờ")
                {
                    rowColor = Color.LightGreen;
                }
                else if (status.Contains("Đi trễ"))
                {
                    rowColor = Color.Orange;
                }
                else if (status.Contains("Về sớm"))
                {
                    rowColor = Color.Yellow;
                }
                else if (status == "Vắng mặt")
                {
                    rowColor = Color.LightGray;
                }

                // Highlight Manager với border màu đỏ
                if (roleName == "Manager")
                {
                    row.DefaultCellStyle.Font = new Font(dgvAttendanceReport.Font, FontStyle.Bold);
                }

                row.DefaultCellStyle.BackColor = rowColor;
            }
        }

        private void AttendanceAdminForm_Load(object sender, EventArgs e)
        {
            // Form load
        }

        private void lblLateEmployees_Click(object sender, EventArgs e)
        {

        }

        private void lblLegend_Click(object sender, EventArgs e)
        {

        }

        private void dgvAttendanceReport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
