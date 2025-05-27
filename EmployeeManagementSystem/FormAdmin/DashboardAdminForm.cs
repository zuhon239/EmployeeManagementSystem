using EmployeeManagementSystem.Controller;
using EmployeeManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace EmployeeManagementSystem.FormAdmin
{
    public partial class DashboardAdminForm : Form
    {
        private readonly EmployeeManagementContext _context;
        private readonly DashboardAdminController _controller;
        private int _selectedDepartmentId;

        public DashboardAdminForm(int currentAdminId)
        {
            InitializeComponent();
            _context = new EmployeeManagementContext();

           
            var user = _context.Users.FirstOrDefault(u => u.UserId == currentAdminId);
            if (user?.RoleId != 3) 
            {
                MessageBox.Show("Chỉ admin mới có thể truy cập dashboard này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            _controller = new DashboardAdminController(_context);
            InitializeEvents();
            LoadDepartments();

            // Load default data (last 30 days, company-wide)
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

        private void LoadDepartments()
        {
            var (departmentNames, departmentIds) = _controller.GetDepartments();
            cbxDepartments.Items.Clear();

            for (int i = 0; i < departmentNames.Length; i++)
            {
                cbxDepartments.Items.Add(new KeyValuePair<int, string>(departmentIds[i], departmentNames[i]));
            }

            if (departmentNames.Length > 0)
            {
                cbxDepartments.DisplayMember = "Value";
                cbxDepartments.ValueMember = "Key";
                cbxDepartments.SelectedIndex = 0; // Default to "Total"
                _selectedDepartmentId = 0;
                UpdatePerformanceChartForSelectedDepartment();
            }
        }

        private void cbxDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDepartments.SelectedItem is KeyValuePair<int, string> selected)
            {
                _selectedDepartmentId = selected.Key;
                LoadData(dtpFrom.Value, dtpTo.Value); // Refresh all data with new department filter
            }
        }

        private void UpdatePerformanceChartForSelectedDepartment()
        {
            string selectedDepartmentName = "";
            if (cbxDepartments.SelectedItem is KeyValuePair<int, string> selected)
            {
                selectedDepartmentName = selected.Value;
            }
            UpdatePerformanceDistributionChart(dtpFrom.Value, dtpTo.Value);
        }

        private void LoadData(DateTime fromDate, DateTime toDate)
        {
            if (toDate <= fromDate)
            {
                MessageBox.Show("Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get employees query based on department selection
            var employeesQuery = _context.Employees.AsNoTracking();
            if (_selectedDepartmentId != 0)
            {
                employeesQuery = employeesQuery.Where(e => e.DepartmentId == _selectedDepartmentId);
            }

            var totalEmployees = employeesQuery.Count();
            lblTotalAmount.Text = totalEmployees.ToString();

            var activeEmployees = employeesQuery.Count(e => e.Status);
            label1.Text = activeEmployees.ToString();

            var attendanceCount = _context.Attendances
                .Where(a => a.Date >= fromDate.Date && a.Date <= toDate.Date)
                .Count(a => employeesQuery.Select(e => e.UserId).Contains(a.UserId));
            lblAmountAttendance.Text = attendanceCount.ToString();

            var pendingLeaveRequests = _context.LeaveRequests
                .Where(lr => lr.Status == "Pending" && lr.StartDate.Date >= fromDate.Date && lr.EndDate.Date <= toDate.Date)
                .Count(lr => employeesQuery.Select(e => e.UserId).Contains(lr.UserId));
            lblPendingAmount.Text = pendingLeaveRequests.ToString();

            var totalSalary = _context.Payrolls
                .Where(p => p.Month >= fromDate.Date && p.Month <= toDate.Date)
                .Where(p => employeesQuery.Select(e => e.UserId).Contains(p.UserId))
                .Sum(p => p.TotalSalary);
            label2.Text = totalSalary.ToString("C");

            UpdateSalaryChart(fromDate, toDate);
            UpdatePerformanceDistributionChart(fromDate, toDate);
        }

        private void UpdateSalaryChart(DateTime fromDate, DateTime toDate)
        {
            chartSalary.Series.Clear();
            chartSalary.Titles.Clear();
            var series = new Series("TotalSalary")
            {
                ChartType = SeriesChartType.Column,
                IsVisibleInLegend = false
            };

            if (_selectedDepartmentId == 0) // Tổng công ty
            {
                var payrolls = _context.Payrolls
                    .Where(p => p.Month >= fromDate && p.Month <= toDate)
                    .Join(_context.Employees, p => p.UserId, e => e.UserId, (p, e) => new { p, e.DepartmentId })
                    .GroupBy(x => x.DepartmentId)
                    .Select(g => new
                    {
                        DepartmentId = g.Key,
                        Total = g.Sum(x => x.p.TotalSalary)
                    })
                    .ToList();

                var departmentNames = _context.Departments
                    .AsNoTracking()
                    .ToDictionary(d => d.DepartmentId, d => d.Name);

                foreach (var payroll in payrolls)
                {
                    string deptName = departmentNames.ContainsKey(payroll.DepartmentId) ? departmentNames[payroll.DepartmentId] : $"Phòng {payroll.DepartmentId}";
                    series.Points.AddXY(deptName, payroll.Total);
                }

                if (payrolls.Count == 0)
                {
                    series.Points.AddXY("Không có dữ liệu", 0);
                    series.Points[0].Color = Color.Gray;
                    series.Points[0].ToolTip = "Không có bản ghi lương trong khoảng thời gian này";
                }

                chartSalary.Titles.Add($"Tổng lương các phòng ban từ {fromDate:dd/MM/yyyy} đến {toDate:dd/MM/yyyy}");
            }
            else // Phòng ban cụ thể
            {
                var employeesQuery = _context.Employees.AsNoTracking().Where(e => e.DepartmentId == _selectedDepartmentId);
                var payrolls = _context.Payrolls
                    .Where(p => p.Month >= fromDate && p.Month <= toDate)
                    .Where(p => employeesQuery.Select(e => e.UserId).Contains(p.UserId))
                    .GroupBy(p => p.UserId)
                    .Select(g => new
                    {
                        UserId = g.Key,
                        Total = g.Sum(p => p.TotalSalary)
                    })
                    .ToList();

                var employeeNames = _context.Employees
                    .Where(e => employeesQuery.Select(e => e.UserId).Contains(e.UserId))
                    .Select(e => new { e.UserId, e.Name })
                    .ToDictionary(e => e.UserId, e => e.Name);

                foreach (var payroll in payrolls)
                {
                    string employeeName = employeeNames.ContainsKey(payroll.UserId) ? employeeNames[payroll.UserId] : $"ID {payroll.UserId}";
                    series.Points.AddXY(employeeName, payroll.Total);
                }

                if (payrolls.Count == 0)
                {
                    series.Points.AddXY("Không có dữ liệu", 0);
                    series.Points[0].Color = Color.Gray;
                    series.Points[0].ToolTip = "Không có bản ghi lương trong khoảng thời gian này";
                }

                string deptName = cbxDepartments.SelectedItem is KeyValuePair<int, string> selected ? selected.Value : "Phòng ban";
                chartSalary.Titles.Add($"Tổng lương phòng {deptName} từ {fromDate:dd/MM/yyyy} đến {toDate:dd/MM/yyyy}");
            }

            chartSalary.Series.Add(series);
            chartSalary.ChartAreas[0].AxisX.Interval = 1;
            chartSalary.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chartSalary.ChartAreas[0].AxisX.IsLabelAutoFit = true;
            chartSalary.ChartAreas[0].AxisY.Title = "Tổng lương (VND)";
        }

        private void UpdatePerformanceDistributionChart(DateTime fromDate, DateTime toDate)
        {
            try
            {
                chart1.Series.Clear();
                chart1.Titles.Clear();
                chart1.Annotations.Clear();

                string deptName = cbxDepartments.SelectedItem is KeyValuePair<int, string> selected ? selected.Value : "Tổng công ty";
                string dateRange = $"{fromDate:dd/MM/yyyy} - {toDate:dd/MM/yyyy}";

                if (_selectedDepartmentId == 0) // So sánh giữa các phòng ban
                {
                    var series = new Series("PerformanceDistribution")
                    {
                        ChartType = SeriesChartType.Bar,
                        IsVisibleInLegend = true
                    };

                    var performances = _controller.CalculateAllDepartmentsPerformance(fromDate, toDate);
                    foreach (var perf in performances)
                    {
                        var point = series.Points.Add(perf.EffectiveTimePercentage);
                        point.AxisLabel = perf.DepartmentName;
                        point.LegendText = $"{perf.DepartmentName}: {perf.EffectiveTimePercentage:F1}%";
                        point.Color = Color.Green;
                        point.ToolTip = $"Hiệu quả: {perf.EffectiveTimePercentage:F1}%\nNghỉ phép: {perf.ApprovedLeavePercentage:F1}%\nKhông hiệu quả: {perf.IneffectiveTimePercentage:F1}%";
                    }

                    if (performances.Count == 0)
                    {
                        var point = series.Points.Add(100);
                        point.AxisLabel = "Chưa có dữ liệu";
                        point.LegendText = "Chưa có dữ liệu: 100%";
                        point.Color = Color.Gray;
                        point.ToolTip = "Chưa có dữ liệu chấm công";
                    }

                    chart1.Series.Add(series);
                    chart1.Titles.Add($"So sánh hiệu suất các phòng ban ({dateRange})");
                }
                else // Hiển thị hiệu suất của một phòng ban
                {
                    var series = new Series("PerformanceDistribution")
                    {
                        ChartType = SeriesChartType.Pie,
                        IsVisibleInLegend = true,
                        Label = "#PERCENT{P1}",
                        LegendText = "#VALX: #PERCENT{P1}"
                    };

                    var (effectiveTime, approvedLeave, ineffectiveTime, detailMessage) =
                        _controller.CalculatePerformanceDistribution(fromDate, toDate, _selectedDepartmentId);

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
                        point.ToolTip = "Chưa có bản ghi chấm công nào trong khoảng thời gian này";
                    }

                    series.ToolTip = detailMessage;
                    chart1.Series.Add(series);
                    chart1.Titles.Add($"Phân bố hiệu suất - {deptName} ({dateRange})");

                    var annotation = new TextAnnotation();
                    annotation.Text = detailMessage.Replace("\n", " | ");
                    annotation.Font = new Font("Arial", 8);
                    annotation.ForeColor = Color.DarkBlue;
                    annotation.AnchorX = 50;
                    annotation.AnchorY = 95;
                    chart1.Annotations.Add(annotation);
                }

                chart1.ChartAreas[0].Area3DStyle.Enable3D = false;
                chart1.Legends[0].Enabled = true;
                chart1.Legends[0].Docking = Docking.Right;
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
            lblAmountAttendance.Text = "0";
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
    }
}
