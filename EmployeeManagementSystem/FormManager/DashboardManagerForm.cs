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
using System.Windows.Forms.DataVisualization.Charting;

namespace EmployeeManagementSystem.FormManager
{
    public partial class DashboardManagerForm : Form
    {
        private readonly EmployeeManagementContext _context;
        private readonly int _currentManagerId;
        public DashboardManagerForm(int currentManagerId)
        {
            InitializeComponent();
            _context = new EmployeeManagementContext();
            // Kiểm tra vai trò của người dùng
            var user = _context.Users.FirstOrDefault(u => u.UserId == currentManagerId);

            if (user?.RoleId != 2) 
            {
                MessageBox.Show("Chỉ manager mới có thể truy cập dashboard này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }
            _currentManagerId = currentManagerId;
  
            InitializeEvents();

            // Tải dữ liệu mặc định (30 ngày gần nhất)
            LoadData(DateTime.Today.AddDays(-30), DateTime.Today);
        }
        private void InitializeEvents()
        {
            btnToday.Click += (s, e) => LoadData(DateTime.Today, DateTime.Today);
            btnLast7days.Click += (s, e) => LoadData(DateTime.Today.AddDays(-7), DateTime.Today);
            btnLast30days.Click += (s, e) => LoadData(DateTime.Today.AddDays(-30), DateTime.Today);
            btnThisMonth.Click += (s, e) => LoadData(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1), DateTime.Today);
            dtpFrom.ValueChanged += (s, e) => LoadData(dtpFrom.Value, dtpTo.Value);
            dtpTo.ValueChanged += (s, e) => LoadData(dtpFrom.Value, dtpTo.Value);
        }
        private void LoadData(DateTime fromDate, DateTime toDate)
        {
            // Đảm bảo ngày kết thúc không nhỏ hơn ngày bắt đầu
            if (toDate < fromDate)
            {
                MessageBox.Show("Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tìm phòng ban của manager
            var department = _context.Users
               .OfType<Employee>()
               .AsNoTracking().Include(e => e.Department)
               .Select(e => new
               {
                   e.UserId,
                   e.DepartmentId,
                   e.Department.Name
               })
                                  .FirstOrDefault(e => e.UserId == _currentManagerId); ;

            if (department == null)
            {
                MessageBox.Show("Không tìm thấy phòng ban do bạn quản lý.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lblDepartmentName.Text = "Không có phòng ban";
                ClearDashboard();
                return;
            }

            // Hiển thị tên phòng ban
            lblDepartmentName.Text = department.Name;

            // Lấy danh sách nhân viên trong phòng ban
            var employeesQuery = _context.Employees
                .Include(e => e.Department)
                .Where(e => e.DepartmentId == department.DepartmentId)
                .AsNoTracking();

            // Tổng số nhân viên
            var totalEmployees = employeesQuery.Count();
            lblTotalAmount.Text = totalEmployees.ToString();

            // Số nhân viên đang hoạt động
            var activeEmployees = employeesQuery.Count(e => e.Status);
            label1.Text = activeEmployees.ToString();

            // Số lượt chấm công
            var attendanceCount = _context.Attendances
                .Where(a => a.Date >= fromDate && a.Date <= toDate)
                .Count(a => employeesQuery.Select(e => e.UserId).Contains(a.UserId));
            lblAmountAttendance.Text = attendanceCount.ToString();

            // Yêu cầu nghỉ phép đang chờ phê duyệt
            var pendingLeaveRequests = _context.LeaveRequests               
                .Where(lr => lr.Status == "Pending" && lr.StartDate >= fromDate && lr.EndDate <= toDate)
                .Count(lr => employeesQuery.Select(e => e.UserId).Contains(lr.UserId));
            lblPendingAmount.Text = pendingLeaveRequests.ToString();

            // Tổng lương
            var totalSalary = _context.Payrolls
                .Where(p => p.Month >= fromDate && p.Month <= toDate)
                .Where(p => employeesQuery.Select(e => e.UserId).Contains(p.UserId))
                .Sum(p => p.TotalSalary);
            label2.Text = totalSalary.ToString("C");

            // Cập nhật biểu đồ lương
            //UpdateSalaryChart(fromDate, toDate, employeesQuery);

            // Cập nhật biểu đồ trạng thái nghỉ phép
            //UpdateLeaveRequestChart(fromDate, toDate, employeesQuery);
        }

        private void UpdateSalaryChart(DateTime fromDate, DateTime toDate, IQueryable<Employee> employeesQuery)
        {
            chartSalary.Series.Clear();
            var series = new Series("TotalSalary")
            {
                ChartType = SeriesChartType.Column
            };

            var payrolls = _context.Payrolls
                .Where(p => p.Month >= fromDate && p.Month <= toDate)
                .Where(p => employeesQuery.Select(e => e.UserId).Contains(p.UserId))
                .GroupBy(p => p.Month)
                .Select(g => new { Month = g.Key, Total = g.Sum(p => p.TotalSalary) })
                .OrderBy(g => g.Month)
                .ToList();

            foreach (var item in payrolls)
            {
                series.Points.AddXY(item.Month.ToString("MMM yyyy"), item.Total);
            }

            chartSalary.Series.Add(series);
            chartSalary.Titles[0].Text = "Total Salary by Month";
        }

        private void UpdateLeaveRequestChart(DateTime fromDate, DateTime toDate, IQueryable<Employee> employeesQuery)
        {
            chart1.Series.Clear();
            var series = new Series("LeaveStatus")
            {
                ChartType = SeriesChartType.Pie
            };

            var leaveStats = _context.LeaveRequests
                .Where(lr => lr.StartDate >= fromDate && lr.EndDate <= toDate)
                .Where(lr => employeesQuery.Select(e => e.UserId).Contains(lr.UserId))
                .GroupBy(lr => lr.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToList();

            foreach (var stat in leaveStats)
            {
                series.Points.AddXY(stat.Status, stat.Count);
            }

            chart1.Series.Add(series);
            chart1.Titles[0].Text = "Leave Request Status";
        }

        private void ClearDashboard()
        {
            lblTotalAmount.Text = "0";
            label1.Text = "0";
            lblAmountAttendance.Text = "0";
            lblPendingAmount.Text = "0";
            label2.Text = "0";
            chartSalary.Series.Clear();
            chart1.Series.Clear();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            _context.Dispose(); // Giải phóng context khi form đóng
        }


        private void lblTotalSalary_Click(object sender, EventArgs e)
        {

        }
    }
}
