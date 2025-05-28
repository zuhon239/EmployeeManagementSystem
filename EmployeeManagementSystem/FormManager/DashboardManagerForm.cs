using EmployeeManagementSystem.Controller;
using EmployeeManagementSystem.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeeManagementSystem.FormManager
{
    public partial class DashboardManagerForm : Form
    {
        private readonly EmployeeManagementContext _context;
        private readonly int _currentManagerId;
        private readonly DashboardManagerController _controller; 
        private int _selectedEmployeeId;
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
            _controller = new DashboardManagerController(_context, _currentManagerId);
            InitializeEvents();
            LoadEmployees();

            // Tải dữ liệu mặc định (30 ngày gần nhất) với toàn bộ phòng ban
            LoadData(DateTime.Today.AddDays(-30), DateTime.Today);

        }
        private void InitializeEvents()
        {
            btnToday.Click += (s, e) =>
            {
                dtpFrom.Value = DateTime.Today;
                dtpTo.Value = DateTime.Today;
                LoadData(dtpFrom.Value, dtpTo.Value);
            };
            btnLast7days.Click += (s, e) =>
            {
                dtpFrom.Value = DateTime.Today.AddDays(-7);
                dtpTo.Value = DateTime.Today.AddDays(1).AddTicks(-1);
                LoadData(dtpFrom.Value, dtpTo.Value);
            };
            btnLast30days.Click += (s, e) =>
            {
                dtpFrom.Value = DateTime.Today.AddDays(-30);
                dtpTo.Value = DateTime.Today.AddDays(1).AddTicks(-1);
                LoadData(dtpFrom.Value, dtpTo.Value);
            };
            btnThisMonth.Click += (s, e) =>
            {
                dtpFrom.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                dtpTo.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month), 23, 59, 59);
                LoadData(dtpFrom.Value, dtpTo.Value);
            };
            dtpFrom.ValueChanged += (s, e) => LoadData(dtpFrom.Value, dtpTo.Value);
            dtpTo.ValueChanged += (s, e) => LoadData(dtpFrom.Value, dtpTo.Value);
            cbxDepartments.SelectedIndexChanged += cbxDepartments_SelectedIndexChanged;
        }

        private void LoadEmployees()
        {
            var (employeeNames, employeeIds) = _controller.GetEmployeesInManagedDepartment();

            cbxDepartments.Items.Clear();

            for (int i = 0; i < employeeNames.Length; i++)
            {
                cbxDepartments.Items.Add(new KeyValuePair<int, string>(employeeIds[i], employeeNames[i]));
            }

            if (employeeNames.Length > 0)
            {
                cbxDepartments.DisplayMember = "Value";
                cbxDepartments.ValueMember = "Key";
                cbxDepartments.SelectedIndex = 0;
                _selectedEmployeeId = employeeIds[0];
                LoadPerformanceChartForSelectedEmployee();
            }
        }

        private void cbxDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDepartments.SelectedItem is KeyValuePair<int, string> selected)
            {
                _selectedEmployeeId = selected.Key;
                LoadPerformanceChartForSelectedEmployee();
            }
        }

        private void LoadPerformanceChartForSelectedEmployee()
        {
            string selectedEmployeeName = "";
            if (cbxDepartments.SelectedItem is KeyValuePair<int, string> selected)
            {
                selectedEmployeeName = selected.Value;
            }
            UpdatePerformanceDistributionChart(dtpFrom.Value, dtpTo.Value, selectedEmployeeName);
        }

        private void LoadData(DateTime fromDate, DateTime toDate)
        {
            if (toDate < fromDate)
            {
                MessageBox.Show("Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var department = _context.Users
                .OfType<Employee>()
                .AsNoTracking()
                .Include(e => e.Department)
                .Select(e => new
                {
                    e.UserId,
                    e.DepartmentId,
                    e.Department.Name
                })
                .FirstOrDefault(e => e.UserId == _currentManagerId);

            if (department == null)
            {
                MessageBox.Show("Không tìm thấy phòng ban do bạn quản lý.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lblDepartmentName.Text = "Không có phòng ban";
                ClearDashboard();
                return;
            }

            lblDepartmentName.Text = department.Name;

            var employeesQuery = _context.Employees
                .Include(e => e.Department)
                .Where(e => e.DepartmentId == department.DepartmentId)
                .AsNoTracking();

            var totalEmployees = employeesQuery.Count();
            lblTotalAmount.Text = totalEmployees.ToString();

            var activeEmployees = employeesQuery.Count(e => e.Status);
            label1.Text = activeEmployees.ToString();

            var pendingLeaveRequests = _context.LeaveRequests
                .Where(lr => lr.Status == "Pending" && lr.StartDate.Date >= fromDate.Date && lr.EndDate.Date <= toDate.Date)
                .Count(lr => employeesQuery.Select(e => e.UserId).Contains(lr.UserId));
            lblPendingAmount.Text = pendingLeaveRequests.ToString();

            // Tổng lương dựa trên tháng chiếm ưu thế
            var dominantMonth = _controller.GetDominantMonth(fromDate, toDate);
            var totalSalary = _context.Payrolls
                .Where(p => p.Month.Year == dominantMonth.Year && p.Month.Month == dominantMonth.Month)
                .Where(p => employeesQuery.Select(e => e.UserId).Contains(p.UserId))
                .Sum(p => p.TotalSalary);
            label2.Text = totalSalary.ToString("C");

            UpdateSalaryChart(fromDate, toDate);

            if (_selectedEmployeeId > 0)
            {
                string selectedEmployeeName = "";
                if (cbxDepartments.SelectedItem is KeyValuePair<int, string> selected)
                {
                    selectedEmployeeName = selected.Value;
                }
                UpdatePerformanceDistributionChart(fromDate, toDate, selectedEmployeeName);
            }
        }


        private void UpdateSalaryChart(DateTime fromDate, DateTime toDate)
        {
            try
            {
                chartSalary.Series.Clear();

                var series = new Series("SalaryDistribution")
                {
                    ChartType = SeriesChartType.Pie,
                    IsVisibleInLegend = true,
                    Label = "#PERCENT{P1}",
                    LegendText = "#VALX: #PERCENT{P1}"
                };

                var department = _context.Users
                    .OfType<Employee>()
                    .AsNoTracking()
                    .Include(e => e.Department)
                    .FirstOrDefault(e => e.UserId == _currentManagerId);

                if (department == null)
                {
                    series.Points.AddXY("Không có dữ liệu", 100);
                    series.Points[0].Color = Color.Gray;
                    series.Points[0].ToolTip = "Không tìm thấy phòng ban do bạn quản lý.";
                    chartSalary.Series.Add(series);
                    chartSalary.Titles.Clear();
                    chartSalary.Titles.Add("Phân bố lương - Không có dữ liệu");
                    return;
                }

                // Xác định tháng chiếm ưu thế
                var dominantMonth = _controller.GetDominantMonth(fromDate, toDate);

                // Lấy dữ liệu lương của tháng chiếm ưu thế
                var salaryData = _context.Payrolls
                    .Include(p => p.Employee)
                    .Where(p => p.Month.Year == dominantMonth.Year && p.Month.Month == dominantMonth.Month)
                    .Where(p => p.Employee.DepartmentId == department.DepartmentId)
                    .GroupBy(p => new { p.UserId, p.Employee.Name })
                    .Select(g => new
                    {
                        EmployeeName = g.Key.Name,
                        TotalSalary = g.Sum(p => p.TotalSalary)
                    })
                    .ToList();

                decimal totalDepartmentSalary = salaryData.Sum(s => s.TotalSalary);

                if (salaryData.Any() && totalDepartmentSalary > 0)
                {
                    foreach (var data in salaryData)
                    {
                        if (data.TotalSalary > 0)
                        {
                            double percentage = (double)(data.TotalSalary / totalDepartmentSalary) * 100;
                            var point = series.Points.Add(percentage);
                            point.AxisLabel = data.EmployeeName;
                            point.LegendText = $"{data.EmployeeName}: {percentage:F1}%";
                            point.ToolTip = $"{data.EmployeeName}: {data.TotalSalary:C} ({percentage:F1}%)";
                        }
                    }
                }
                else
                {
                    var point = series.Points.Add(100);
                    point.AxisLabel = "Chưa có dữ liệu lương";
                    point.LegendText = "Chưa có dữ liệu: 100%";
                    point.Color = Color.Gray;
                    point.ToolTip = $"Không có bản ghi lương nào cho tháng {dominantMonth:MM/yyyy}.";
                }

                chartSalary.Series.Add(series);

                string chartTitle = $"Phân bố lương - ({dominantMonth:MM/yyyy})";
                if (chartSalary.Titles.Count > 0)
                {
                    chartSalary.Titles[0].Text = chartTitle;
                }
                else
                {
                    chartSalary.Titles.Add(chartTitle);
                }

                chartSalary.ChartAreas[0].Area3DStyle.Enable3D = false;
                chartSalary.Legends[0].Enabled = true;
                chartSalary.Legends[0].Docking = Docking.Right;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật biểu đồ lương: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                chartSalary.Series.Clear();
                var emptySeries = new Series("Error")
                {
                    ChartType = SeriesChartType.Pie
                };
                emptySeries.Points.AddXY("Lỗi dữ liệu", 100);
                emptySeries.Points[0].Color = Color.Red;
                emptySeries.Points[0].ToolTip = $"Lỗi: {ex.Message}";
                chartSalary.Series.Add(emptySeries);
            }
        }
        private void UpdatePerformanceDistributionChart(DateTime fromDate, DateTime toDate, string employeeName)
        {
            try
            {
                chart1.Series.Clear();

                var series = new Series("PerformanceDistribution")
                {
                    ChartType = SeriesChartType.Pie,
                    IsVisibleInLegend = true,
                    Label = "#PERCENT{P1}",
                    LegendText = "#VALX: #PERCENT{P1}"
                };

                var (effectiveTime, approvedLeave, ineffectiveTime, detailMessage) =
                    _controller.CalculatePerformanceDistribution(fromDate, toDate, _selectedEmployeeId);

           

                if (effectiveTime > 0)
                {
                    var point1 = series.Points.Add(effectiveTime);
                    point1.AxisLabel = "Thời gian làm việc hiệu quả";
                    point1.LegendText = $"Hiệu quả: {effectiveTime:F1}%";
                    point1.Color = Color.Green;
                    point1.ToolTip = $"Thời gian làm việc hiệu quả: {effectiveTime:F1}%";
                }

                if (approvedLeave > 0)
                {
                    var point2 = series.Points.Add(approvedLeave);
                    point2.AxisLabel = "Nghỉ phép được duyệt";
                    point2.LegendText = $"Nghỉ phép: {approvedLeave:F1}%";
                    point2.Color = Color.Orange;
                    point2.ToolTip = $"Nghỉ phép được duyệt: {approvedLeave:F1}%";
                }

                if (ineffectiveTime > 0)
                {
                    var point3 = series.Points.Add(ineffectiveTime);
                    point3.AxisLabel = "Thời gian không hiệu quả";
                    point3.LegendText = $"Không hiệu quả: {ineffectiveTime:F1}%";
                    point3.Color = Color.Red;
                    point3.ToolTip = $"Thời gian không hiệu quả: {ineffectiveTime:F1}% (bao gồm: đi muộn, về sớm, vắng mặt, nghỉ không được duyệt, không chấm công)";
                }

                if (series.Points.Count == 0)
                {
                    var point = series.Points.Add(100);
                    point.AxisLabel = "Chưa có dữ liệu chấm công";
                    point.LegendText = "Chưa có dữ liệu: 100%";
                    point.Color = Color.Gray;
                    point.ToolTip = "Nhân viên chưa có bản ghi chấm công nào trong khoảng thời gian này";
                }

                chart1.Series.Add(series);

                string dateRange = $"{fromDate:dd/MM/yyyy} - {toDate:dd/MM/yyyy}";
                string chartTitle = string.IsNullOrEmpty(employeeName)
                    ? $"Phân bố hiệu suất làm việc ({dateRange})"
                    : $"Phân bố hiệu suất - {employeeName} ({dateRange})";

                if (chart1.Titles.Count > 0)
                {
                    chart1.Titles[0].Text = chartTitle;
                }
                else
                {
                    chart1.Titles.Add(chartTitle);
                }

                if (chart1.Annotations.Count > 0)
                {
                    chart1.Annotations.Clear();
                }

                var annotation = new TextAnnotation();
               
                annotation.Font = new Font("Arial", 8);
                annotation.ForeColor = Color.DarkBlue;
                annotation.AnchorX = 50;
                annotation.AnchorY = 95;
                chart1.Annotations.Add(annotation);

                chart1.ChartAreas[0].Area3DStyle.Enable3D = false;
                chart1.Legends[0].Enabled = true;
                chart1.Legends[0].Docking = Docking.Right;

                chart1.GetToolTipText += (sender, e) => e.Text = detailMessage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật biểu đồ hiệu suất: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                chart1.Series.Clear();
                var emptySeries = new Series("Error")
                {
                    ChartType = SeriesChartType.Pie
                };
                emptySeries.Points.AddXY("Lỗi dữ liệu", 100);
                emptySeries.Points[0].Color = Color.Red;
                emptySeries.Points[0].ToolTip = $"Lỗi: {ex.Message}";
                chart1.Series.Add(emptySeries);
            }
        }

        private void ClearDashboard()
        {
            lblTotalAmount.Text = "0";
            label1.Text = "0";
            lblPendingAmount.Text = "0";
            label2.Text = "0";
            chartSalary.Series.Clear();
            chart1.Series.Clear();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            _context.Dispose();
        }


        private void lblTotalSalary_Click(object sender, EventArgs e)
        {

        }
    }
}
