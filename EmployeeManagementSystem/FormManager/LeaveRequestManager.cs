using EmployeeManagementSystem.Controller;
using EmployeeManagementSystem.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagementSystem.FormManager
{
    public partial class LeaveRequestManager : Form
    {
        private int? _selectedLeaveId;
        private readonly EmployeeManagementContext _context;
        private readonly LeaveRequestController _controller;
        private readonly int _currentUserId; // ID của người dùng hiện tại (Manager/Admin)
        public LeaveRequestManager(int currentUserId, EmployeeManagementContext context)
        {
            InitializeComponent();
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _controller = new LeaveRequestController(_context);
            _currentUserId = currentUserId;
            LoadLeaveRequests(currentUserId);
            dataGridView1.CellContentClick += DataGridView1_CellContentClick;
        }
        private void LoadLeaveRequests(int currentManagerId) // Replace with actual Manager's UserId
        {
            try
            {
                var manager = _context.Users
                .OfType<Employee>()
                .FirstOrDefault(e => e.UserId == currentManagerId);

                if (manager == null)
                {
                    MessageBox.Show("No manager found with the given UserId.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var managedDepartmentId = manager.DepartmentId;

                if (managedDepartmentId == null)
                {
                    MessageBox.Show("No department found for this manager.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var employeesInDepartment = _context.Employees
                    .Where(e => e.DepartmentId == managedDepartmentId && e.RoleId == 1)
                    .Select(e => e.UserId)
                    .ToList();

                if (!employeesInDepartment.Any())
                {
                    MessageBox.Show("No employees found in this department.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Step 3: Load LeaveRequest data for those employees
                var leaveRequests = _context.LeaveRequests
                    .Include(lr => lr.Employee)
                    .Where(lr => employeesInDepartment.Contains(lr.UserId) && lr.Status == "Pending")
                    .Select(lr => new
                    {
                        lr.LeaveId,
                        lr.UserId,
                        EmployeeName = lr.Employee != null ? lr.Employee.Name : "N/A",
                        lr.StartDate,
                        lr.EndDate,
                        lr.Shift,
                        lr.Status
                    })
                    .ToList();

                // Clear existing rows
                dataGridView1.Rows.Clear();

                // Populate DataGridView
                foreach (var request in leaveRequests)
                {
                    dataGridView1.Rows.Add(
                        request.LeaveId,
                        request.UserId,
                        request.EmployeeName,
                        request.StartDate.ToString("yyyy-MM-dd"),
                        request.EndDate.ToString("yyyy-MM-dd"),
                        request.Shift,
                        request.Status,
                        "View Details"
                    );
                }

                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading leave requests: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu click vào cột "View Details" (cột thứ 7)
            if (e.ColumnIndex == dataGridView1.Columns["clDetail"].Index && e.RowIndex >= 0)
            {
                try
                {
                    // Lấy LeaveId từ cột đầu tiên
                    _selectedLeaveId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["clLeaveId"].Value);

                    // Tìm yêu cầu nghỉ phép trong cơ sở dữ liệu
                    var leaveRequest = _context.LeaveRequests
                        .Include(lr => lr.Employee)
                        .FirstOrDefault(lr => lr.LeaveId == _selectedLeaveId);

                    if (leaveRequest != null)
                    {
                        // Hiển thị thông tin trong panel
                        lblHoTen.Text = $"Họ và tên: {leaveRequest.Employee?.Name ?? "N/A"}";
                        lblReason.Text = $"Lý do: {leaveRequest.Reason ?? "No reason provided"}";
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin yêu cầu nghỉ phép.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi hiển thị chi tiết: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnAccept_Click(object sender, EventArgs e)
        {
            if (_selectedLeaveId == null)
            {
                MessageBox.Show("Vui lòng chọn một yêu cầu nghỉ phép để duyệt.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var result = MessageBox.Show(
                    "Xác nhận duyệt?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    // Gọi phương thức duyệt yêu cầu
                    await _controller.ApproveOrRejectLeaveRequestAsync(_selectedLeaveId.Value, _currentUserId, true);
                    MessageBox.Show("Yêu cầu nghỉ phép đã được duyệt.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Làm mới DataGridView
                    LoadLeaveRequests(_currentUserId);

                    // Xóa thông tin trong panel
                    lblHoTen.Text = "";
                    lblReason.Text = "";
                    _selectedLeaveId = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi duyệt yêu cầu: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private async void btnDeny_Click(object sender, EventArgs e)
        {
            
        }

        private async void btnDeny_Click_1(object sender, EventArgs e)
        {
            if (_selectedLeaveId == null)
            {
                MessageBox.Show("Vui lòng chọn một yêu cầu nghỉ phép để từ chối.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var result = MessageBox.Show(
                    "Xác nhận từ chối?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    // Gọi phương thức từ chối yêu cầu
                    await _controller.ApproveOrRejectLeaveRequestAsync(_selectedLeaveId.Value, _currentUserId, false);
                    MessageBox.Show("Yêu cầu nghỉ phép đã bị từ chối.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Làm mới DataGridView
                    LoadLeaveRequests(_currentUserId);

                    // Xóa thông tin trong panel
                    lblHoTen.Text = "";
                    lblReason.Text = "";
                    _selectedLeaveId = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi từ chối yêu cầu: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
           
        }
    }
}
