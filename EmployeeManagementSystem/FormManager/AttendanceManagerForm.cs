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
        private bool _isDetailedView = false;

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

        // ✅ Load filter options
        private async void LoadFilterOptions()
        {
            try
            {
                // Load shift filter
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

        private async void dtpMonth_ValueChanged(object sender, EventArgs e)
        {
            await LoadAttendanceReportAsync();
        }

        // ✅ Bỏ btnRefresh_Click method

        // ✅ Filter changed events
        private async void cmbShiftFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadAttendanceReportAsync();
        }

        private async void cmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadAttendanceReportAsync();
        }

        // ✅ Toggle view button
        private async void btnToggleView_Click(object sender, EventArgs e)
        {
            _isDetailedView = !_isDetailedView;
            btnToggleView.Text = _isDetailedView ? "Xem Tổng Quan" : "Xem Chi Tiết";
            await LoadAttendanceReportAsync();
        }

        private async Task LoadAttendanceReportAsync()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"=== LOAD ATTENDANCE REPORT ===");

                DateTime selectedMonth = dtpMonth.Value;
                string shiftFilter = cmbShiftFilter.SelectedItem?.ToString() ?? "All";
                string statusFilter = cmbStatusFilter.SelectedItem?.ToString() ?? "All";

                // Clear existing columns
                dgvAttendanceReport.Columns.Clear();
                dgvAttendanceReport.Rows.Clear();

                if (_isDetailedView)
                {
                    // ✅ View chi tiết theo ca
                    await LoadDetailedViewAsync(selectedMonth, shiftFilter, statusFilter);
                }
                else
                {
                    // ✅ View tổng quan theo tháng
                    await LoadMonthlyViewAsync(selectedMonth);
                }

                // ✅ Bỏ LoadStatisticsAsync()
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi LoadAttendanceReportAsync: {ex.Message}");
                MessageBox.Show($"Lỗi tải báo cáo: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ Load view chi tiết theo ca
        private async Task LoadDetailedViewAsync(DateTime selectedMonth, string shiftFilter, string statusFilter)
        {
            var reportData = await _controller.GetDetailedAttendanceReportAsync(_managerId, selectedMonth, shiftFilter, statusFilter);

            if (reportData.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu phù hợp với filter!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Add columns for detailed view
            dgvAttendanceReport.Columns.Add("UserId", "ID");
            dgvAttendanceReport.Columns.Add("EmployeeName", "Tên Nhân Viên");
            dgvAttendanceReport.Columns.Add("Position", "Chức Vụ");
            dgvAttendanceReport.Columns.Add("Date", "Ngày");
            dgvAttendanceReport.Columns.Add("Shift", "Ca");
            dgvAttendanceReport.Columns.Add("ClockIn", "Giờ Vào");
            dgvAttendanceReport.Columns.Add("ClockOut", "Giờ Ra");
            dgvAttendanceReport.Columns.Add("Status", "Trạng Thái");

            // Add data rows
            foreach (var item in reportData)
            {
                dgvAttendanceReport.Rows.Add(
                    item.UserId,
                    item.EmployeeName,
                    item.Position,
                    item.Date.ToString("dd/MM/yyyy"),
                    item.Shift,
                    item.ClockIn?.ToString("HH:mm:ss") ?? "",
                    item.ClockOut?.ToString("HH:mm:ss") ?? "",
                    item.Status
                );
            }

            // Style the grid
            dgvAttendanceReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAttendanceReport.Columns["UserId"].Width = 50;
            dgvAttendanceReport.Columns["EmployeeName"].Width = 120;
            dgvAttendanceReport.Columns["Position"].Width = 100;
            dgvAttendanceReport.Columns["Date"].Width = 80;
            dgvAttendanceReport.Columns["Shift"].Width = 60;
            dgvAttendanceReport.Columns["ClockIn"].Width = 80;
            dgvAttendanceReport.Columns["ClockOut"].Width = 80;
            dgvAttendanceReport.Columns["Status"].Width = 120;

        }

        // ✅ Load view tổng quan theo tháng
        private async Task LoadMonthlyViewAsync(DateTime selectedMonth)
        {
            var reportData = await _controller.GetMonthlyAttendanceReportAsync(_managerId, selectedMonth);

            if (reportData.Count == 0)
            {
                MessageBox.Show("Không có nhân viên nào để hiển thị!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Add employee info columns
            dgvAttendanceReport.Columns.Add("UserId", "ID");
            dgvAttendanceReport.Columns.Add("EmployeeName", "Tên Nhân Viên");
            dgvAttendanceReport.Columns.Add("Position", "Chức Vụ");
            dgvAttendanceReport.Columns.Add("Phone", "Số ĐT");

            // Add day columns
            int daysInMonth = DateTime.DaysInMonth(selectedMonth.Year, selectedMonth.Month);
            for (int day = 1; day <= daysInMonth; day++)
            {
                var dayColumn = new DataGridViewTextBoxColumn
                {
                    Name = $"Day{day}",
                    HeaderText = day.ToString(),
                    Width = 50,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleCenter
                    }
                };
                dgvAttendanceReport.Columns.Add(dayColumn);
            }

            // Add data rows
            foreach (var employee in reportData)
            {
                var row = new object[4 + daysInMonth];
                row[0] = employee.UserId;
                row[1] = employee.EmployeeName;
                row[2] = employee.Position;
                row[3] = employee.Phone;

                for (int day = 1; day <= daysInMonth; day++)
                {
                    row[3 + day] = employee.DailyAttendances.ContainsKey(day)
                        ? employee.DailyAttendances[day]
                        : "";
                }

                dgvAttendanceReport.Rows.Add(row);
            }

            // Style the grid
            dgvAttendanceReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvAttendanceReport.Columns["UserId"].Width = 50;
            dgvAttendanceReport.Columns["EmployeeName"].Width = 150;
            dgvAttendanceReport.Columns["Position"].Width = 100;
            dgvAttendanceReport.Columns["Phone"].Width = 100;
        }

       

        

        private void AttendanceManagerForm_Load(object sender, EventArgs e)
        {
            // Form load
        }
    }
}
