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

            // Load default data (last 30 days, company-wide) with explicit Date values
            DateTime defaultFrom = DateTime.Today.AddDays(-30).Date; // Chỉ lấy ngày, không giờ
            DateTime defaultTo = DateTime.Today.Date; // Chỉ lấy ngày, không giờ
            dtpFrom.Value = defaultFrom;
            dtpTo.Value = defaultTo;
            LoadData(defaultFrom, defaultTo);
        }

        private void InitializeEvents()
        {
            btnToday.Click += (s, e) =>
            {
                dtpFrom.Value = DateTime.Today;
                dtpTo.Value = DateTime.Today;
                LoadData(dtpFrom.Value.Date, dtpTo.Value.Date); // Sử dụng .Date để bỏ giờ
            };
            btnLast7days.Click += (s, e) =>
            {
                dtpFrom.Value = DateTime.Today.AddDays(-7);
                dtpTo.Value = DateTime.Today.AddDays(1).AddTicks(-1);
                LoadData(dtpFrom.Value.Date, dtpTo.Value.Date);
            };
            btnLast30days.Click += (s, e) =>
            {
                dtpFrom.Value = DateTime.Today.AddDays(-30);
                dtpTo.Value = DateTime.Today.AddDays(1).AddTicks(-1);
                LoadData(dtpFrom.Value.Date, dtpTo.Value.Date);
            };
            btnThisMonth.Click += (s, e) =>
            {
                dtpFrom.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                dtpTo.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month), 23, 59, 59);
                LoadData(dtpFrom.Value.Date, dtpTo.Value.Date);
            };
            dtpFrom.ValueChanged += (s, e) => LoadData(dtpFrom.Value.Date, dtpTo.Value.Date);
            dtpTo.ValueChanged += (s, e) => LoadData(dtpFrom.Value.Date, dtpTo.Value.Date);
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
                LoadData(dtpFrom.Value.Date, dtpTo.Value.Date); // Sử dụng .Date để bỏ giờ
            }
        }

        private void UpdatePerformanceChartForSelectedDepartment()
        {
            string selectedDepartmentName = "";
            if (cbxDepartments.SelectedItem is KeyValuePair<int, string> selected)
            {
                selectedDepartmentName = selected.Value;
            }
            UpdatePerformanceDistributionChart(dtpFrom.Value.Date, dtpTo.Value.Date); // Sử dụng .Date
        }

        private void LoadData(DateTime fromDate, DateTime toDate)
        {
            if (toDate < fromDate)
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

            var pendingLeaveRequests = _context.LeaveRequests
                .Where(lr => lr.Status == "Pending" && lr.StartDate.Date >= fromDate.Date && lr.EndDate.Date <= toDate.Date)
                .Count(lr => employeesQuery.Select(e => e.UserId).Contains(lr.UserId));
            lblPendingAmount.Text = pendingLeaveRequests.ToString();

            // Tổng lương dựa trên tháng chiếm ưu thế
            var (salaryData, totalSalary) = _controller.GetSalaryDistribution(fromDate, toDate, _selectedDepartmentId == 0 ? null : _selectedDepartmentId);
            label2.Text = totalSalary.ToString("C");

            UpdateSalaryChart(fromDate, toDate);
            UpdatePerformanceChartForSelectedDepartment(); // Gọi lại để cập nhật biểu đồ
        }

        private void UpdateSalaryChart(DateTime fromDate, DateTime toDate)
        {
            try
            {
                chartSalary.Series.Clear();
                chartSalary.Titles.Clear();

                var series = new Series("SalaryDistribution")
                {
                    ChartType = SeriesChartType.Pie, // Luôn dùng biểu đồ tròn
                    IsVisibleInLegend = true,
                    Label = "#PERCENT{P1}",
                    LegendText = "#VALX: #PERCENT{P1}"
                };

                var (salaryData, totalSalary) = _controller.GetSalaryDistribution(fromDate, toDate, _selectedDepartmentId == 0 ? null : _selectedDepartmentId);
                var dominantMonth = _controller.GetDominantMonth(fromDate, toDate);

                if (salaryData.Any() && totalSalary > 0)
                {
                    foreach (var data in salaryData)
                    {
                        if (data.TotalSalary > 0)
                        {
                            double percentage = (double)(data.TotalSalary / totalSalary) * 100;
                            var point = series.Points.Add(percentage);
                            point.AxisLabel = data.Name;
                            point.LegendText = $"{data.Name}: {percentage:F1}%";
                            point.ToolTip = $"{data.Name}: {data.TotalSalary:C} ({percentage:F1}%)";                           
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

                string deptName = cbxDepartments.SelectedItem is KeyValuePair<int, string> selected ? selected.Value : "Tổng công ty";
                string chartTitle = $"Phân bố lương - {deptName} ({dominantMonth:MM/yyyy})";
                chartSalary.Titles.Add(chartTitle);

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

        private void UpdatePerformanceDistributionChart(DateTime fromDate, DateTime toDate)
        {
            try
            {
                chart1.Series.Clear();
                chart1.Titles.Clear();
                chart1.Annotations.Clear();

                string deptName = cbxDepartments.SelectedItem is KeyValuePair<int, string> selected ? selected.Value : "Tổng công ty";
                string dateRange = $"{fromDate:dd/MM/yyyy} - {toDate:dd/MM/yyyy}";

                var series = new Series("PerformanceDistribution")
                {
                    ChartType = SeriesChartType.Pie,
                    IsVisibleInLegend = true,
                    Label = "#PERCENT{P1}",
                    LegendText = "#VALX: #PERCENT{P1}"
                };

                if (_selectedDepartmentId == 0) // Hiển thị hiệu suất tổng hợp của tất cả phòng ban
                {
                    var performances = _controller.CalculateAllDepartmentsPerformance(fromDate, toDate);
                    decimal totalPercentage = 0;

                    foreach (var perf in performances)
                    {
                        if (perf.EffectiveTimePercentage > 0) // Only include departments with positive effective time
                        {
                            totalPercentage += (decimal)perf.EffectiveTimePercentage;
                            var point = series.Points.Add(perf.EffectiveTimePercentage);
                            point.AxisLabel = perf.DepartmentName;
                            point.LegendText = $"{perf.DepartmentName}: {perf.EffectiveTimePercentage:F1}%";
                            point.ToolTip = $"Hiệu quả: {perf.EffectiveTimePercentage:F1}%";
                        }
                    }

                    if (performances.Count == 0 || totalPercentage == 0)
                    {
                        var point = series.Points.Add(100);
                        point.AxisLabel = "Chưa có dữ liệu";
                        point.LegendText = "Chưa có dữ liệu: 100%";
                        point.Color = Color.Gray;
                        point.ToolTip = "Chưa có dữ liệu chấm công";
                    }
                }
                else // Hiển thị hiệu suất của một phòng ban cụ thể
                {
                    var (effectiveTime, approvedLeave, ineffectiveTime, detailMessage) =
                        _controller.CalculatePerformanceDistribution(fromDate, toDate, _selectedDepartmentId);

                    if (effectiveTime > 0)
                    {
                        var point1 = series.Points.Add(effectiveTime);
                        point1.AxisLabel = "Thời gian làm việc hiệu quả";
                        point1.LegendText = $"Hiệu quả: {effectiveTime:F1}%";
                        point1.Color = Color.Green;
                        point1.ToolTip = $"{detailMessage}";
                    }

                    if (approvedLeave > 0)
                    {
                        var point2 = series.Points.Add(approvedLeave);
                        point2.AxisLabel = "Nghỉ phép được duyệt";
                        point2.LegendText = $"Nghỉ phép: {approvedLeave:F1}%";
                        point2.Color = Color.Orange;
                        point2.ToolTip = $"{detailMessage}";
                    }

                    if (ineffectiveTime > 0)
                    {
                        var point3 = series.Points.Add(ineffectiveTime);
                        point3.AxisLabel = "Thời gian không hiệu quả";
                        point3.LegendText = $"Không hiệu quả: {ineffectiveTime:F1}%";
                        point3.Color = Color.Red;
                        point3.ToolTip = $"{detailMessage}";
                    }

                    if (series.Points.Count == 0)
                    {
                        var point = series.Points.Add(100);
                        point.AxisLabel = "Chưa có dữ liệu chấm công";
                        point.LegendText = "Chưa có dữ liệu: 100%";
                        point.Color = Color.Gray;
                        point.ToolTip = "Chưa có bản ghi chấm công nào trong khoảng thời gian này";
                    }
                }

                chart1.Series.Add(series);
                chart1.Titles.Add($"Phân bố hiệu suất - {deptName} ({dateRange})");

                var annotation = new TextAnnotation();
                annotation.Text = series.Points.Count > 0 ? series.Points[0].ToolTip?.Replace("\n", " | ") ?? "" : "";
                annotation.Font = new Font("Arial", 8);
                annotation.ForeColor = Color.DarkBlue;
                annotation.AnchorX = 50;
                annotation.AnchorY = 95;
                chart1.Annotations.Add(annotation);

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
