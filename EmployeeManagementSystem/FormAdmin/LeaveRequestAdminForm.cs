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

namespace EmployeeManagementSystem.FormAdmin
{
    public partial class LeaveRequestAdminForm : Form
    {

        private int? _selectedLeaveId;
        private readonly EmployeeManagementContext _context;
        private readonly LeaveRequestController _controller;
        private readonly int _currentUserId;
        public LeaveRequestAdminForm(int currentUserId,EmployeeManagementContext context)
        {
            InitializeComponent();
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _controller = new LeaveRequestController(_context);
            _currentUserId = currentUserId;
            comboBox1.SelectedIndex = 0;
            LoadLeaveRequests(); // Initial load
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            dataGridView1.CellContentClick += DataGridView1_CellContentClick;
            dataGridView1.CellMouseEnter += DataGridView1_CellMouseEnter;
            dataGridView1.CellMouseLeave += DataGridView1_CellMouseLeave;
        }
        private void LoadLeaveRequests()
        {
            try
            {
                var selectedItem = comboBox1.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(selectedItem))
                {
                    MessageBox.Show("Please select a valid option.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                dataGridView1.Rows.Clear();

                if (selectedItem == "Manager")
                {
                    
                    var managerLeaveRequests = _context.LeaveRequests
                        .Include(lr => lr.Employee)
                        .Include(lr => lr.Employee.Role)
                        .Where(lr => lr.Employee.RoleId != 1 && lr.Status == "Chờ duyệt") 
                        .Select(lr => new
                        {
                            lr.LeaveId,
                            lr.UserId,
                            EmployeeName = lr.Employee != null ? lr.Employee.Name : "N/A",
                            RoleName = lr.Employee.Role != null ? lr.Employee.Role.RoleName : "N/A",
                            lr.StartDate,
                            lr.EndDate,
                            lr.Shift,
                            Detail = "View Details"
                        })
                        .ToList();

                    foreach (var request in managerLeaveRequests)
                    {
                        dataGridView1.Rows.Add(
                            request.LeaveId,
                            request.UserId,
                            request.EmployeeName,
                            request.RoleName,
                            request.StartDate.ToString("yyyy-MM-dd"),
                            request.EndDate.ToString("yyyy-MM-dd"),
                            request.Shift,
                            request.Detail
                        );
                    }
                }
                else
                {
                    // Load leave requests for employees in the selected department
                    var department = _context.Departments
                        .FirstOrDefault(d => d.Name == selectedItem);

                    if (department == null)
                    {
                        MessageBox.Show("Selected department not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var employeesInDepartment = _context.Employees
                        .Where(e => e.DepartmentId == department.DepartmentId && e.RoleId == 1)
                        .Select(e => e.UserId)
                        .ToList();

                    if (!employeesInDepartment.Any())
                    {
                        MessageBox.Show("No employees found in this department.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    var leaveRequests = _context.LeaveRequests
                        .Include(lr => lr.Employee)
                        .Include(lr => lr.Employee.Role)
                        .Where(lr => employeesInDepartment.Contains(lr.UserId) && lr.Status == "Chờ duyệt")
                        .Select(lr => new
                        {
                            lr.LeaveId,
                            lr.UserId,
                            EmployeeName = lr.Employee != null ? lr.Employee.Name : "N/A",
                            RoleName = lr.Employee.Role != null ? lr.Employee.Role.RoleName : "N/A",
                            lr.StartDate,
                            lr.EndDate,
                            lr.Shift,
                            Detail = "Xem chi tiết"
                        })
                        .ToList();

                    foreach (var request in leaveRequests)
                    {
                        dataGridView1.Rows.Add(
                            request.LeaveId,
                            request.UserId,
                            request.EmployeeName,
                            request.RoleName,
                            request.StartDate.ToString("yyyy-MM-dd"),
                            request.EndDate.ToString("yyyy-MM-dd"),
                            request.Shift,
                            request.Detail
                        );
                    }
                }

                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading leave requests: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLeaveRequests(); // Refresh data when selection changes
        }
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["clDetail"].Index && e.RowIndex >= 0)
            {
                try
                {
                    _selectedLeaveId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["clLeave_Id"].Value);

                    var leaveRequest = _context.LeaveRequests
                        .Include(lr => lr.Employee)
                        .FirstOrDefault(lr => lr.LeaveId == _selectedLeaveId);

                    if (leaveRequest != null)
                    {
                        lblName.Text = $"Họ và tên: {leaveRequest.Employee?.Name ?? "N/A"}";
                        lblReason.Text = $"Lý do: {leaveRequest.Reason ?? "No reason provided"}";
                    }
                    else
                    {
                        MessageBox.Show("Leave request details not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error displaying details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void DataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["clDetail"].Index && e.RowIndex >= 0)
            {
                dataGridView1.Cursor = Cursors.Hand;
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.Blue;
            }
        }
        private void DataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["clDetail"].Index && e.RowIndex >= 0)
            {
                dataGridView1.Cursor = Cursors.Default;
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.Black;
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
                    await _controller.ApproveOrRejectLeaveRequestAsync(_selectedLeaveId.Value, _currentUserId, true);
                    MessageBox.Show("Yêu cầu nghỉ phép đã được duyệt.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadLeaveRequests();

                    lblName.Text = "";
                    lblReason.Text = "";
                    _selectedLeaveId = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi duyệt yêu cầu: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnReject_Click(object sender, EventArgs e)
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
                    await _controller.ApproveOrRejectLeaveRequestAsync(_selectedLeaveId.Value, _currentUserId, false);
                    MessageBox.Show("Yêu cầu nghỉ phép đã bị từ chối.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadLeaveRequests();

                    lblName.Text = "";
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
