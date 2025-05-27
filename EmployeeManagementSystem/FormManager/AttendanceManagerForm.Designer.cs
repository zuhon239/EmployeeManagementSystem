namespace EmployeeManagementSystem.FormManager
{
    partial class AttendanceManagerForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblDepartmentInfo;
        private DateTimePicker dtpDate; // ✅ Thay đổi từ dtpMonth sang dtpDate
        private Label lblDate; // ✅ Thay đổi từ lblMonth sang lblDate
        private DataGridView dgvAttendanceReport;
        private Label lblLegend;

        // Filter controls
        private ComboBox cmbShiftFilter;
        private ComboBox cmbStatusFilter;
        private Label lblShiftFilter;
        private Label lblStatusFilter;
        private Button btnRefresh; // ✅ Thêm nút Refresh

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
            dtpDate = new DateTimePicker(); // ✅ Thay đổi
            lblDate = new Label(); // ✅ Thay đổi
            dgvAttendanceReport = new DataGridView();
            lblLegend = new Label();

            // Filter controls
            cmbShiftFilter = new ComboBox();
            cmbStatusFilter = new ComboBox();
            lblShiftFilter = new Label();
            lblStatusFilter = new Label();
            btnRefresh = new Button(); // ✅ Thêm nút Refresh

            ((System.ComponentModel.ISupportInitialize)dgvAttendanceReport).BeginInit();
            SuspendLayout();

            // Form - ✅ Giữ nguyên kích thước
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 760); // ✅ Giữ nguyên độ rộng
            Name = "AttendanceManagerForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Báo Cáo Chấm Công Phòng Ban";
            Load += AttendanceManagerForm_Load;

            // lblDepartmentInfo
            lblDepartmentInfo.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold);
            lblDepartmentInfo.Location = new Point(23, 25);
            lblDepartmentInfo.Size = new Size(914, 38);
            lblDepartmentInfo.Text = "Thông tin phòng ban";

            // ✅ lblDate (thay đổi từ lblMonth)
            lblDate.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            lblDate.Location = new Point(23, 76);
            lblDate.Size = new Size(91, 32);
            lblDate.Text = "Ngày:"; // ✅ Thay đổi text

            // ✅ dtpDate (thay đổi từ dtpMonth)
            dtpDate.Format = DateTimePickerFormat.Custom;
            dtpDate.CustomFormat = "dd/MM/yyyy"; // ✅ Thay đổi format
            dtpDate.Location = new Point(126, 76);
            dtpDate.Size = new Size(130, 26); // ✅ Tăng width cho ngày
            dtpDate.Value = DateTime.Now;
            dtpDate.ValueChanged += dtpDate_ValueChanged; // ✅ Thay đổi event

            // ✅ lblShiftFilter
            lblShiftFilter.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            lblShiftFilter.Location = new Point(280, 76); // ✅ Điều chỉnh position
            lblShiftFilter.Size = new Size(46, 32);
            lblShiftFilter.Text = "Ca:";

            // ✅ cmbShiftFilter
            cmbShiftFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbShiftFilter.Location = new Point(330, 76); // ✅ Điều chỉnh position
            cmbShiftFilter.Size = new Size(91, 27);
            cmbShiftFilter.SelectedIndexChanged += cmbShiftFilter_SelectedIndexChanged;

            // ✅ lblStatusFilter
            lblStatusFilter.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            lblStatusFilter.Location = new Point(440, 76); // ✅ Điều chỉnh position
            lblStatusFilter.Size = new Size(80, 32);
            lblStatusFilter.Text = "Trạng thái:";

            // ✅ cmbStatusFilter
            cmbStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatusFilter.Location = new Point(530, 76); // ✅ Điều chỉnh position
            cmbStatusFilter.Size = new Size(137, 27);
            cmbStatusFilter.SelectedIndexChanged += cmbStatusFilter_SelectedIndexChanged;

            // ✅ btnRefresh
            btnRefresh.BackColor = Color.FromArgb(0, 123, 255);
            btnRefresh.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(690, 73);
            btnRefresh.Size = new Size(114, 38);
            btnRefresh.Text = "Làm Mới";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;

            // lblLegend
            lblLegend.Font = new Font("Microsoft Sans Serif", 9F);
            lblLegend.Location = new Point(23, 120);
            lblLegend.Size = new Size(914, 38);
            lblLegend.Text = "Ký hiệu màu: Xanh=Đúng giờ, Xanh dương=Chưa check out, Cam=Đi trễ, Vàng=Về sớm, Đỏ=Đi trễ&Về sớm, Xám=Vắng mặt";

            // dgvAttendanceReport - ✅ Giữ nguyên kích thước
            dgvAttendanceReport.AllowUserToAddRows = false;
            dgvAttendanceReport.AllowUserToDeleteRows = false;
            dgvAttendanceReport.Location = new Point(23, 165);
            dgvAttendanceReport.ReadOnly = true;
            dgvAttendanceReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAttendanceReport.Size = new Size(1149, 570); // ✅ Giữ nguyên kích thước
            dgvAttendanceReport.ColumnHeadersHeight = 30;
            dgvAttendanceReport.ScrollBars = ScrollBars.Both;

            // Add controls to form
            Controls.AddRange(new Control[] {
                lblDepartmentInfo, lblDate, dtpDate,
                lblShiftFilter, cmbShiftFilter, lblStatusFilter, cmbStatusFilter,
                btnRefresh, lblLegend, dgvAttendanceReport
            });

            ((System.ComponentModel.ISupportInitialize)dgvAttendanceReport).EndInit();
            ResumeLayout(false);
        }
    }
}
