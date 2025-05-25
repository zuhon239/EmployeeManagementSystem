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

namespace EmployeeManagementSystem
{
    public partial class LeaveHistoryForm : Form
    {
        private readonly EmployeeManagementContext _context;
        private readonly LeaveRequestController _controller;
        private readonly int _currentUserId; // ID của người dùng hiện tại (Manager/Admin)
        public LeaveHistoryForm(int currentUserId, EmployeeManagementContext context)
        {
            InitializeComponent();
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _controller = new LeaveRequestController(_context);
            _currentUserId = currentUserId;
            LoadLeaveRequests(currentUserId);
        }
        private void LoadLeaveRequests(int currentUserId)
        {
            try
            {
                var leaveRequests = _context.LeaveRequests
                    .Include(lr => lr.Employee)
                    .Include(lr => lr.Approver) // Include Approver for ApprovalName
                    .Where(lr => lr.UserId == currentUserId)
                    .Select(lr => new
                    {
                        lr.LeaveId,
                        lr.StartDate,
                        lr.EndDate,
                        lr.Shift,
                        lr.Status,
                        ApprovalName = lr.Approver != null ? lr.Approver.Name : "Chưa duyệt"
                    })
                    .ToList();

                if (!leaveRequests.Any())
                {
                    MessageBox.Show("Không tìm thấy yêu cầu nghỉ phép nào.", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();

                // Add columns to DataGridView
                dataGridView1.Columns.Add("LeaveId", "Leave_ID");
                dataGridView1.Columns.Add("StartDate", "Start Date");
                dataGridView1.Columns.Add("EndDate", "End Date");
                dataGridView1.Columns.Add("Shift", "Shift");
                dataGridView1.Columns.Add("Status", "Status");
                dataGridView1.Columns.Add("ApprovalName", "Approval Name");

                foreach (var request in leaveRequests)
                {
                    dataGridView1.Rows.Add(
                        request.LeaveId,
                        request.StartDate.ToString("yyyy-MM-dd"),
                        request.EndDate.ToString("yyyy-MM-dd"),
                        request.Shift,
                        request.Status,
                        request.ApprovalName
                    );
                }

                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách yêu cầu nghỉ phép: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
