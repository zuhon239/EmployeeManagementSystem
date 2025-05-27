namespace EmployeeManagementSystem.FormManager
{
    partial class AttendanceManagerForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblDepartmentInfo;
        private DateTimePicker dtpMonth;
        private Label lblMonth;
        // ✅ Bỏ btnRefresh
        private DataGridView dgvAttendanceReport;
        // ✅ Bỏ gbStatistics và các label statistics
        private Label lblLegend;

        // Filter controls
        private ComboBox cmbShiftFilter;
        private ComboBox cmbStatusFilter;
        private Label lblShiftFilter;
        private Label lblStatusFilter;
        private Button btnToggleView;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblDepartmentInfo = new Label();
            dtpMonth = new DateTimePicker();
            lblMonth = new Label();
            dgvAttendanceReport = new DataGridView();
            lblLegend = new Label();

            // Filter controls
            cmbShiftFilter = new ComboBox();
            cmbStatusFilter = new ComboBox();
            lblShiftFilter = new Label();
            lblStatusFilter = new Label();
            btnToggleView = new Button();

            ((System.ComponentModel.ISupportInitialize)dgvAttendanceReport).BeginInit();
            SuspendLayout();

            // Form
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 600); // ✅ Giảm height vì bỏ statistics
            Name = "AttendanceManagerForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Báo Cáo Chấm Công Phòng Ban";
            Load += AttendanceManagerForm_Load;

            // lblDepartmentInfo
            lblDepartmentInfo.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold);
            lblDepartmentInfo.Location = new Point(20, 20);
            lblDepartmentInfo.Size = new Size(800, 30);
            lblDepartmentInfo.Text = "Thông tin phòng ban";

            // lblMonth
            lblMonth.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            lblMonth.Location = new Point(20, 60);
            lblMonth.Size = new Size(80, 25);
            lblMonth.Text = "Tháng/Năm:";

            // dtpMonth
            dtpMonth.Format = DateTimePickerFormat.Custom;
            dtpMonth.CustomFormat = "MM/yyyy";
            dtpMonth.Location = new Point(110, 60);
            dtpMonth.Size = new Size(100, 25);
            dtpMonth.Value = DateTime.Now;
            dtpMonth.ValueChanged += dtpMonth_ValueChanged;

            // ✅ lblShiftFilter
            lblShiftFilter.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            lblShiftFilter.Location = new Point(230, 60);
            lblShiftFilter.Size = new Size(40, 25);
            lblShiftFilter.Text = "Ca:";

            // ✅ cmbShiftFilter
            cmbShiftFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbShiftFilter.Location = new Point(280, 60);
            cmbShiftFilter.Size = new Size(80, 25);
            cmbShiftFilter.SelectedIndexChanged += cmbShiftFilter_SelectedIndexChanged;

            // ✅ lblStatusFilter
            lblStatusFilter.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            lblStatusFilter.Location = new Point(380, 60);
            lblStatusFilter.Size = new Size(70, 25);
            lblStatusFilter.Text = "Trạng thái:";

            // ✅ cmbStatusFilter
            cmbStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatusFilter.Location = new Point(460, 60);
            cmbStatusFilter.Size = new Size(120, 25);
            cmbStatusFilter.SelectedIndexChanged += cmbStatusFilter_SelectedIndexChanged;

            // ✅ btnToggleView
            btnToggleView.BackColor = Color.FromArgb(255, 193, 7);
            btnToggleView.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            btnToggleView.ForeColor = Color.Black;
            btnToggleView.Location = new Point(600, 58);
            btnToggleView.Size = new Size(100, 30);
            btnToggleView.Text = "Xem Chi Tiết";
            btnToggleView.UseVisualStyleBackColor = false;
            btnToggleView.Click += btnToggleView_Click;

            // ✅ Bỏ btnRefresh

            // lblLegend
            lblLegend.Font = new Font("Microsoft Sans Serif", 9F);
            lblLegend.Location = new Point(20, 95);
            lblLegend.Size = new Size(800, 30);
            lblLegend.Text = "Ký hiệu: P=Đúng giờ, L=Đi trễ, E=Về sớm, LE=Đi trễ&Về sớm, S=Sáng, C=Chiều, Trống=Vắng mặt";

            // dgvAttendanceReport - ✅ Tăng height vì bỏ statistics
            dgvAttendanceReport.AllowUserToAddRows = false;
            dgvAttendanceReport.AllowUserToDeleteRows = false;
            dgvAttendanceReport.Location = new Point(20, 130);
            dgvAttendanceReport.ReadOnly = true;
            dgvAttendanceReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAttendanceReport.Size = new Size(1160, 450); // ✅ Tăng height
            dgvAttendanceReport.ColumnHeadersHeight = 30;
            dgvAttendanceReport.ScrollBars = ScrollBars.Both;

            // ✅ Bỏ gbStatistics và tất cả label statistics

            // Add controls to form
            Controls.AddRange(new Control[] {
                lblDepartmentInfo, lblMonth, dtpMonth,
                lblShiftFilter, cmbShiftFilter, lblStatusFilter, cmbStatusFilter,
                btnToggleView, lblLegend, dgvAttendanceReport
                // ✅ Bỏ gbStatistics
            });

            ((System.ComponentModel.ISupportInitialize)dgvAttendanceReport).EndInit();
            ResumeLayout(false);
        }
    }
}
