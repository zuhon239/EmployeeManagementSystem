using EmployeeManagementSystem.Controller;
using EmployeeManagementSystem.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagementSystem
{
    public partial class AttendanceForm : Form
    {
        private readonly AttendanceController _attendanceController;
        private readonly int _currentUserId;
        private readonly EmployeeManagementContext _context;
        private bool _isFiltered = false;

        public AttendanceForm(int userId)
        {
            InitializeComponent();
            _currentUserId = userId;
            _context = new EmployeeManagementContext();
            _attendanceController = new AttendanceController(_context);

            LoadEmployeeInfo();
            _ = LoadAttendanceHistoryAsync();
        }

        private async void LoadEmployeeInfo()
        {
            try
            {
                var employee = await _context.Employees
                    .Include(e => e.Department)
                    .FirstOrDefaultAsync(e => e.UserId == _currentUserId);

                if (employee != null)
                {
                    lblEmployeeInfo.Text = $"{employee.Name} - {employee.Department?.Name} - {employee.Position}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnCheckIn_Click(object sender, EventArgs e)
        {
            try
            {
                string result = await _attendanceController.CheckInAsync(_currentUserId);
                MessageBox.Show(result, "Check In", MessageBoxButtons.OK,
                    result.Contains("thành công") ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                // ✅ Reload dữ liệu theo trạng thái filter hiện tại
                if (result.Contains("thành công"))
                {
                    if (_isFiltered)
                        await LoadFilteredDataAsync();
                    else
                        await LoadAttendanceHistoryAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnCheckOut_Click(object sender, EventArgs e)
        {
            try
            {
                string result = await _attendanceController.CheckOutAsync(_currentUserId);
                MessageBox.Show(result, "Check Out", MessageBoxButtons.OK,
                    result.Contains("thành công") ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                // ✅ Reload dữ liệu theo trạng thái filter hiện tại
                if (result.Contains("thành công"))
                {
                    if (_isFiltered)
                        await LoadFilteredDataAsync();
                    else
                        await LoadAttendanceHistoryAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCheckWiFi_Click(object sender, EventArgs e)
        {
            try
            {
                string bssid = _attendanceController.GetCurrentWiFiBSSID();
                bool isValid = _attendanceController.IsValidCompanyWiFi(bssid);

                MessageBox.Show($"BSSID: {bssid}\nTrạng thái: {(isValid ? "Hợp lệ" : "Không hợp lệ")}",
                    "WiFi Info", MessageBoxButtons.OK,
                    isValid ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ Nút Refresh - Load tất cả dữ liệu và bỏ filter
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            _isFiltered = false;
            await LoadAttendanceHistoryAsync();
            MessageBox.Show("Đã làm mới dữ liệu!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ✅ Nút Filter - Lọc theo ngày được chọn
        private async void btnFilter_Click(object sender, EventArgs e)
        {
            _isFiltered = true;
            await LoadFilteredDataAsync();
        }

        // ✅ Nút Clear Filter - Bỏ lọc và hiển thị tất cả
        private async void btnClearFilter_Click(object sender, EventArgs e)
        {
            _isFiltered = false;
            await LoadAttendanceHistoryAsync();
            MessageBox.Show("Đã bỏ lọc, hiển thị tất cả dữ liệu!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ✅ Load dữ liệu đã lọc theo ngày
        private async Task LoadFilteredDataAsync()
        {
            try
            {
                DateTime selectedDate = dtpFilterDate.Value.Date;
                var filteredHistory = await _attendanceController.GetAttendanceByDateAsync(_currentUserId, selectedDate);

                if (filteredHistory.Count > 0)
                {
                    dgvHistory.DataSource = filteredHistory.Select(h => new
                    {
                        User_ID = h.UserId,
                        Ngày = h.Date.ToString("dd/MM/yyyy"),
                        Ca = h.Shift,
                        Check_In = h.ClockIn?.ToString("HH:mm:ss") ?? "Chưa check in",
                        Check_Out = h.ClockOut?.ToString("HH:mm:ss") ?? "Chưa check out",
                        Trạng_Thái = h.Status
                    }).ToList();

                    MessageBox.Show($"Tìm thấy {filteredHistory.Count} bản ghi cho ngày {selectedDate:dd/MM/yyyy}",
                        "Kết quả lọc", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dgvHistory.DataSource = null;
                    MessageBox.Show($"Không có dữ liệu chấm công cho ngày {selectedDate:dd/MM/yyyy}",
                        "Kết quả lọc", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lọc dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ Load tất cả lịch sử (không filter)
        private async Task LoadAttendanceHistoryAsync()
        {
            try
            {
                var history = await _attendanceController.GetAttendanceHistoryAsync(_currentUserId);

                dgvHistory.DataSource = history.Select(h => new
                {
                    User_ID = h.UserId,
                    Ngày = h.Date.ToString("dd/MM/yyyy"),
                    Ca = h.Shift,
                    Check_In = h.ClockIn?.ToString("HH:mm:ss") ?? "Chưa check in",
                    Check_Out = h.ClockOut?.ToString("HH:mm:ss") ?? "Chưa check out",
                    Trạng_Thái = h.Status
                }).ToList();

                dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải lịch sử: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AttendanceForm_Load(object sender, EventArgs e)
        {
            // Form load
        }

        private void lblFilterDate_Click(object sender, EventArgs e)
        {

        }
    }
}
