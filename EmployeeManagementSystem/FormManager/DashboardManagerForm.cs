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
                dtpTo.Value = DateTime.Today.AddDays(1).AddTicks(-1); // Bao gồm toàn bộ ngày hiện tại
                LoadData(dtpFrom.Value, dtpTo.Value);
            };
            btnLast30days.Click += (s, e) =>
            {
                dtpFrom.Value = DateTime.Today.AddDays(-30);
                dtpTo.Value = DateTime.Today.AddDays(1).AddTicks(-1); // Bao gồm toàn bộ ngày hiện tại
                LoadData(dtpFrom.Value, dtpTo.Value);
            };
            btnThisMonth.Click += (s, e) =>
            {
                dtpFrom.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                dtpTo.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month), 23, 59, 59); // Ngày cuối tháng, 23:59:59
                LoadData(dtpFrom.Value, dtpTo.Value);
            };
            dtpFrom.ValueChanged += (s, e) => LoadData(dtpFrom.Value, dtpTo.Value);
            dtpTo.ValueChanged += (s, e) => LoadData(dtpFrom.Value, dtpTo.Value);
            cbxDepartments.SelectedIndexChanged += cbxDepartments_SelectedIndexChanged;
        }
        private void LoadEmployees()
        {
            var (employeeNames, employeeIds) = _controller.GetEmployeesInManagedDepartment();

            // Clear combobox (đổi tên từ cbxDepartments thành cbxEmployees nếu có thể)
            cbxDepartments.Items.Clear(); // Tạm thời vẫn dùng cbxDepartments

            // Load danh sách nhân viên vào combobox
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

                // Load biểu đồ tròn cho nhân viên đầu tiên
                LoadPerformanceChartForSelectedEmployee();
            }
        }
        // Method riêng để load biểu đồ tròn cho nhân viên được chọn

        private void cbxDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDepartments.SelectedItem is KeyValuePair<int, string> selected)
            {
                _selectedEmployeeId = selected.Key;

                // Chỉ cập nhật biểu đồ tròn hiệu suất cho nhân viên được chọn
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
            // Đảm bảo ngày kết thúc không nhỏ hơn ngày bắt đầu
            if (toDate < fromDate)
            {
                MessageBox.Show("Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tìm phòng ban của manager
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

        

            // Yêu cầu nghỉ phép đang chờ phê duyệt
            var pendingLeaveRequests = _context.LeaveRequests
                .Where(lr => lr.Status == "Pending" && lr.StartDate.Date >= fromDate.Date && lr.EndDate.Date <= toDate.Date)
                .Count(lr => employeesQuery.Select(e => e.UserId).Contains(lr.UserId));
            lblPendingAmount.Text = pendingLeaveRequests.ToString();

            // Tổng lương
            var totalSalary = _context.Payrolls
                .Where(p => p.Month >= fromDate.Date && p.Month <= toDate.Date)
                .Where(p => employeesQuery.Select(e => e.UserId).Contains(p.UserId))
                .Sum(p => p.TotalSalary);
            label2.Text = totalSalary.ToString("C");

            // Cập nhật biểu đồ lương
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

                // Cấu hình ChartArea
                if (chartSalary.ChartAreas.Count == 0)
                {
                    chartSalary.ChartAreas.Add(new ChartArea());
                }

                var chartArea = chartSalary.ChartAreas[0];
                chartArea.AxisX.Title = "Nhân viên";
                chartArea.AxisY.Title = "Lương (VNĐ)";
                chartArea.AxisX.LabelStyle.Angle = -45;
                chartArea.AxisY.LabelStyle.Format = "C0";
                chartArea.AxisY.LabelStyle.Font = new System.Drawing.Font("Arial", 8);

                chartSalary.Titles.Clear();

                var series = new Series("EmployeeSalary");
                series.ChartType = SeriesChartType.Column;
                series.IsVisibleInLegend = false;
                series.Color = System.Drawing.Color.SteelBlue;

                // Lấy thông tin phòng ban của manager hiện tại
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
                    var emptyPoint = series.Points.AddXY("Không có dữ liệu", 0);
                    series.Points[emptyPoint].ToolTip = "Không tìm thấy phòng ban";
                    series.Points[emptyPoint].Color = System.Drawing.Color.Gray;
                    chartSalary.Series.Add(series);
                    chartSalary.Titles.Add("Biểu đồ lương nhân viên - Không có dữ liệu");
                    return;
                }

                // *** TRUY VẤN 1: Tính tổng quan (tất cả nhân viên trong phòng ban) ***
                var allEmployeesInDepartment = _context.Employees
                    .Where(e => e.DepartmentId == department.DepartmentId)
                    .Select(e => e.UserId)
                    .ToList();

                var totalEmployeesInDepartment = allEmployeesInDepartment.Count;

                var employeesWithSalaryUserIds = _context.Payrolls
                    .Where(p => p.Month >= fromDate.Date && p.Month <= toDate.Date)
                    .Where(p => allEmployeesInDepartment.Contains(p.UserId))
                    .Select(p => p.UserId)
                    .Distinct()
                    .ToList();

                var employeesWithSalaryCount = employeesWithSalaryUserIds.Count;
                var employeesWithoutSalary = totalEmployeesInDepartment - employeesWithSalaryCount;

                var totalSalaryAmount = _context.Payrolls
                    .Where(p => p.Month >= fromDate.Date && p.Month <= toDate.Date)
                    .Where(p => allEmployeesInDepartment.Contains(p.UserId))
                    .Sum(p => p.TotalSalary);

                // *** TRUY VẤN 2: Lấy dữ liệu từ Payroll và JOIN với Employee để hiển thị ***
                var payrollChartData = _context.Payrolls
                                    .Include(p => p.Employee)
                                    .Where(p => p.Month >= fromDate.Date && p.Month <= toDate.Date)
                                    .Where(p => allEmployeesInDepartment.Contains(p.UserId))
                                    .GroupBy(p => new { p.UserId, p.Employee.Name, p.Employee.Status })
                                    .Select(g => new
                                    {
                                        UserId = g.Key.UserId,
                                        EmployeeName = g.Key.Name,
                                        EmployeeStatus = g.Key.Status,
                                        TotalSalary = g.Sum(p => p.TotalSalary),
                                        MonthCount = g.Count(),
                                        LatestMonth = g.Max(p => p.Month),
                                        AverageSalary = g.Average(p => p.TotalSalary)
                                    })
                                    .OrderBy(x => x.EmployeeName)
                                    .ToList();

                if (!payrollChartData.Any())
                {
                    var emptyPoint = series.Points.AddXY("Chưa có dữ liệu lương", 0);
                    series.Points[emptyPoint].ToolTip = $"Không có nhân viên nào có lương trong khoảng {fromDate:MM/yyyy} - {toDate:MM/yyyy}";
                    series.Points[emptyPoint].Color = System.Drawing.Color.LightGray;
                    chartSalary.Series.Add(series);
                    chartSalary.Titles.Add($"Biểu đồ lương nhân viên - {department.Name}");
                }
                else
                {
                    // Hiển thị dữ liệu lương từ Payroll
                    foreach (var data in payrollChartData)
                    {
                        string tooltipText = $"Nhân viên: {data.EmployeeName}\n" +
                                           $"User ID: {data.UserId}\n" +
                                           $"Tổng lương: {data.TotalSalary:C0}\n" +
                                           $"Lương trung bình: {data.AverageSalary:C0}\n" +
                                           $"Số tháng có lương: {data.MonthCount}\n" +
                                           $"Tháng gần nhất: {data.LatestMonth:MM/yyyy}\n" +
                                           $"Trạng thái: {(data.EmployeeStatus ? "Đang hoạt động" : "Không hoạt động")}";

                        System.Drawing.Color pointColor = data.EmployeeStatus ?
                            System.Drawing.Color.Green : System.Drawing.Color.DarkGreen;

                        var pointIndex = series.Points.AddXY(data.EmployeeName, (double)data.TotalSalary);
                        var point = series.Points[pointIndex];
                        point.ToolTip = tooltipText;
                        point.Color = pointColor;
                    }

                    chartSalary.Series.Add(series);
                    chartArea.AxisX.Interval = 1;
                    chartArea.AxisX.IntervalType = DateTimeIntervalType.Number;
                    chartArea.AxisX.LabelStyle.IsStaggered = false;
                }

                // Tiêu đề biểu đồ
                string dateRange = $"{fromDate:MM/yyyy} - {toDate:MM/yyyy}";
                string chartTitle = $"Biểu đồ lương nhân viên - {department.Name} ({dateRange})";
                chartSalary.Titles.Add(chartTitle);

                // Cấu hình biểu đồ
                chartSalary.BackColor = System.Drawing.Color.WhiteSmoke;
                chartArea.BackColor = System.Drawing.Color.White;

                // Legend
                if (chartSalary.Legends.Count == 0)
                {
                    chartSalary.Legends.Add(new Legend());
                }

                var legend = chartSalary.Legends[0];
                legend.Title = "Chú thích";
                legend.Enabled = true;
                legend.Docking = Docking.Bottom;
                legend.Alignment = StringAlignment.Center;

                legend.CustomItems.Clear();
                legend.CustomItems.Add(new LegendItem("Đang hoạt động", System.Drawing.Color.Green, ""));
                legend.CustomItems.Add(new LegendItem("Không hoạt động", System.Drawing.Color.DarkGreen, ""));

                // *** HIỂN THỊ TỔNG QUAN TỪ TRUY VẤN 1 ***
                var summaryTitle = $"Tổng quan: {totalEmployeesInDepartment} nhân viên | Có lương: {employeesWithSalaryCount} | Chưa có lương: {employeesWithoutSalary} | Tổng lương: {totalSalaryAmount:C0}";
                chartSalary.Titles.Add(summaryTitle);

                // Cấu hình trục Y
                chartArea.AxisY.Minimum = 0;
                if (payrollChartData.Any())
                {
                    var maxSalary = payrollChartData.Max(s => s.TotalSalary);
                    if (maxSalary > 0)
                    {
                        chartArea.AxisY.Maximum = (double)(maxSalary * 1.1m);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật biểu đồ lương: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                chartSalary.Series.Clear();
                chartSalary.Titles.Clear();

                if (chartSalary.ChartAreas.Count == 0)
                {
                    chartSalary.ChartAreas.Add(new ChartArea());
                }

                var errorSeries = new Series("Error");
                errorSeries.ChartType = SeriesChartType.Column;

                var errorPointIndex = errorSeries.Points.AddXY("Lỗi", 0);
                var errorPoint = errorSeries.Points[errorPointIndex];
                errorPoint.Color = System.Drawing.Color.Red;
                errorPoint.ToolTip = $"Lỗi: {ex.Message}";

                chartSalary.Series.Add(errorSeries);
                chartSalary.Titles.Add("Biểu đồ lương - Có lỗi xảy ra");
            }
        }

        private void UpdatePerformanceDistributionChart(DateTime fromDate, DateTime toDate, string employeeName)
        {
            try
            {
                chart1.Series.Clear();

                // Tạo series cho biểu đồ tròn
                var series = new Series("PerformanceDistribution")
                {
                    ChartType = SeriesChartType.Pie,
                    IsVisibleInLegend = true,
                    Label = "#PERCENT{P1}",
                    LegendText = "#VALX: #PERCENT{P1}"
                };

                // *** FIX 2: Lấy dữ liệu với thông tin chi tiết ***
                var (effectiveTime, approvedLeave, ineffectiveTime, detailMessage) =
                    _controller.CalculatePerformanceDistribution(fromDate, toDate, _selectedEmployeeId);

                // *** FIX 2: Hiển thị thông tin chi tiết trong tooltip hoặc label ***
                series.ToolTip = detailMessage;

                // Chỉ thêm các điểm có giá trị > 0 để tránh biểu đồ trống
                if (effectiveTime > 0)
                {
                    var point1 = series.Points.Add(effectiveTime);
                    point1.AxisLabel = "Thời gian làm việc hiệu quả";
                    point1.LegendText = $"Hiệu quả: {effectiveTime:F1}%";
                    point1.Color = System.Drawing.Color.Green;
                    point1.ToolTip = $"Thời gian làm việc hiệu quả: {effectiveTime:F1}%";
                }

                if (approvedLeave > 0)
                {
                    var point2 = series.Points.Add(approvedLeave);
                    point2.AxisLabel = "Nghỉ phép được duyệt";
                    point2.LegendText = $"Nghỉ phép: {approvedLeave:F1}%";
                    point2.Color = System.Drawing.Color.Orange;
                    point2.ToolTip = $"Nghỉ phép được duyệt: {approvedLeave:F1}%";
                }

                if (ineffectiveTime > 0)
                {
                    var point3 = series.Points.Add(ineffectiveTime);
                    point3.AxisLabel = "Thời gian không hiệu quả";
                    point3.LegendText = $"Không hiệu quả: {ineffectiveTime:F1}%";
                    point3.Color = System.Drawing.Color.Red;
                    point3.ToolTip = $"Thời gian không hiệu quả: {ineffectiveTime:F1}% (bao gồm: đi muộn, về sớm, vắng mặt, nghỉ không được duyệt, không chấm công)";
                }

                // *** FIX 2: Xử lý trường hợp không có dữ liệu ***
                if (series.Points.Count == 0)
                {
                    var point = series.Points.Add(100);
                    point.AxisLabel = "Chưa có dữ liệu chấm công";
                    point.LegendText = "Chưa có dữ liệu: 100%";
                    point.Color = System.Drawing.Color.Gray;
                    point.ToolTip = "Nhân viên chưa có bản ghi chấm công nào trong khoảng thời gian này";
                }

                chart1.Series.Add(series);

                // Cập nhật tiêu đề biểu đồ với tên nhân viên và khoảng thời gian
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

                // *** Thêm label hiển thị thông tin chi tiết bên dưới biểu đồ ***
                if (chart1.Annotations.Count > 0)
                {
                    chart1.Annotations.Clear();
                }

                var annotation = new TextAnnotation();
                annotation.Text = detailMessage.Replace("\n", " | ");
                annotation.Font = new System.Drawing.Font("Arial", 8);
                annotation.ForeColor = System.Drawing.Color.DarkBlue;
                annotation.AnchorX = 50;
                annotation.AnchorY = 95;
                chart1.Annotations.Add(annotation);

                // Cấu hình thêm cho biểu đồ tròn
                chart1.ChartAreas[0].Area3DStyle.Enable3D = false;
                chart1.Legends[0].Enabled = true;
                chart1.Legends[0].Docking = Docking.Right;

                // *** Kích hoạt tooltip ***
                chart1.GetToolTipText += (sender, e) => e.Text = detailMessage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật biểu đồ hiệu suất: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Hiển thị biểu đồ lỗi
                chart1.Series.Clear();
                var emptySeries = new Series("Error")
                {
                    ChartType = SeriesChartType.Pie
                };
                emptySeries.Points.AddXY("Lỗi dữ liệu", 100);
                emptySeries.Points[0].Color = System.Drawing.Color.Red;
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
            _context.Dispose(); // Giải phóng context khi form đóng
        }


        private void lblTotalSalary_Click(object sender, EventArgs e)
        {

        }
    }
}
