using EmployeeManagementSystem.Controller;
using EmployeeManagementSystem.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagementSystem.FormManager
{
    public partial class AttendanceManagerForm : Form
    {
        private readonly AttendanceManagerController _controller;
        private readonly EmployeeManagementContext _context;
        private readonly int _managerId;
        private Employee _currentManager;

        public AttendanceManagerForm(int managerId)
        {
            InitializeComponent();
            _managerId = managerId;
            _context = new EmployeeManagementContext();
            _controller = new AttendanceManagerController(_context);

            LoadManagerInfo();
            LoadFilterOptions();
            _ = LoadAttendanceReportAsync();
        }

        private async void LoadManagerInfo()
        {
            try
            {
                _currentManager = await _controller.GetManagerInfoAsync(_managerId);

                if (_currentManager != null)
                {
                    lblDepartmentInfo.Text = $"Phòng ban: {_currentManager.Department?.Name} - Manager: {_currentManager.Name}";
                }
                else
                {
                    lblDepartmentInfo.Text = "Không tìm thấy thông tin Manager hoặc bạn không phải Manager";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải thông tin Manager: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ Load filter options (chỉ Sáng, Chiều)
        private async void LoadFilterOptions()
        {
            try
            {
                // Load shift filter - chỉ Sáng, Chiều
                cmbShiftFilter.Items.Clear();
                cmbShiftFilter.Items.Add("All");
                cmbShiftFilter.Items.Add("Sáng");
                cmbShiftFilter.Items.Add("Chiều");
                cmbShiftFilter.SelectedIndex = 0;

                // Load status filter
                cmbStatusFilter.Items.Clear();
                cmbStatusFilter.Items.Add("All");

                var statuses = await _controller.GetAvailableStatusesAsync(_managerId);
                foreach (var status in statuses)
                {
                    cmbStatusFilter.Items.Add(status);
                }
                cmbStatusFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi LoadFilterOptions: {ex.Message}");
            }
        }

        // ✅ Thay đổi từ tháng sang ngày
        private async void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            await LoadAttendanceReportAsync();
        }

        // ✅ Filter changed events
        private async void cmbShiftFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadAttendanceReportAsync();
        }

        private async void cmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadAttendanceReportAsync();
        }

        // ✅ Thêm nút Refresh
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
                System.Diagnostics.Debug.WriteLine($"=== LOAD ATTENDANCE REPORT ===");

                DateTime selectedDate = dtpDate.Value.Date;
                string shiftFilter = cmbShiftFilter.SelectedItem?.ToString() ?? "All";
                string statusFilter = cmbStatusFilter.SelectedItem?.ToString() ?? "All";

                System.Diagnostics.Debug.WriteLine($"Filters: Shift={shiftFilter}, Status={statusFilter}");

                // Clear existing columns
                dgvAttendanceReport.Columns.Clear();
                dgvAttendanceReport.Rows.Clear();

                // ✅ Lấy dữ liệu theo ngày VỚI FILTER
                var reportData = await _controller.GetDailyAttendanceReportAsync(_managerId, selectedDate, shiftFilter, statusFilter);

                System.Diagnostics.Debug.WriteLine($"✅ Controller trả về {reportData.Count} records sau filter");

                if (reportData.Count == 0)
                {
                    // ✅ Thông báo khác nhau tùy theo filter
                    string message;
                    if (shiftFilter != "All" || statusFilter != "All")
                    {
                        message = $"Không có dữ liệu phù hợp với filter:\n- Ca: {shiftFilter}\n- Trạng thái: {statusFilter}\n\nThử thay đổi filter hoặc chọn ngày khác.";
                    }
                    else
                    {
                        message = "Không có nhân viên nào trong phòng ban hoặc bạn không phải Manager!";
                    }

                    MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // ✅ Add columns theo yêu cầu
                dgvAttendanceReport.Columns.Add("UserId", "User ID");
                dgvAttendanceReport.Columns.Add("EmployeeName", "Tên Nhân Viên");
                dgvAttendanceReport.Columns.Add("Position", "Chức Vụ");
                dgvAttendanceReport.Columns.Add("Shift", "Ca");
                dgvAttendanceReport.Columns.Add("ClockIn", "Giờ Check In");
                dgvAttendanceReport.Columns.Add("ClockOut", "Giờ Check Out");
                dgvAttendanceReport.Columns.Add("Status", "Trạng Thái");

                // ✅ Add data rows
                foreach (var item in reportData)
                {
                    dgvAttendanceReport.Rows.Add(
                        item.UserId,
                        item.EmployeeName,
                        item.Position,
                        item.Shift,
                        item.ClockIn?.ToString("HH:mm:ss") ?? "",
                        item.ClockOut?.ToString("HH:mm:ss") ?? "",
                        item.Status ?? ""
                    );
                }

                // ✅ Style the grid
                dgvAttendanceReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvAttendanceReport.Columns["UserId"].Width = 80;
                dgvAttendanceReport.Columns["EmployeeName"].Width = 150;
                dgvAttendanceReport.Columns["Position"].Width = 120;
                dgvAttendanceReport.Columns["Shift"].Width = 60;
                dgvAttendanceReport.Columns["ClockIn"].Width = 100;
                dgvAttendanceReport.Columns["ClockOut"].Width = 100;
                dgvAttendanceReport.Columns["Status"].Width = 150;

                // Color code rows based on status
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

        // ✅ Color code cho rows
        private void ColorCodeRows()
        {
            foreach (DataGridViewRow row in dgvAttendanceReport.Rows)
            {
                var status = row.Cells["Status"].Value?.ToString() ?? "";

                Color rowColor = Color.White;

                if (status.Contains("Chưa check out"))
                {
                    rowColor = Color.LightBlue; // Xanh dương - Đang trong ca làm
                }
                else if (status == "Đúng giờ")
                {
                    rowColor = Color.LightGreen; // Xanh lá - Đúng giờ
                }
                else if (status == "Đi trễ")
                {
                    rowColor = Color.Orange; // Cam - Đi trễ
                }
                else if (status == "Về sớm")
                {
                    rowColor = Color.Yellow; // Vàng - Về sớm
                }
                else if (status == "Đi trễ và về sớm")
                {
                    rowColor = Color.Red; // Đỏ - Đi trễ và về sớm
                }
                else if (status == "Vắng mặt" || string.IsNullOrEmpty(status))
                {
                    rowColor = Color.LightGray; // Xám - Vắng mặt
                }

                row.DefaultCellStyle.BackColor = rowColor;
            }
        }

        private void AttendanceManagerForm_Load(object sender, EventArgs e)
        {
            // Form load
        }
    }
}
