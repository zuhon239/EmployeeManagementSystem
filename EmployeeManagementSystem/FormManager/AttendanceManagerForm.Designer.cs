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
            dtpDate = new DateTimePicker();
            lblDate = new Label();
            dgvAttendanceReport = new DataGridView();
            lblLegend = new Label();
            cmbShiftFilter = new ComboBox();
            cmbStatusFilter = new ComboBox();
            lblShiftFilter = new Label();
            lblStatusFilter = new Label();
            btnRefresh = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvAttendanceReport).BeginInit();
            SuspendLayout();
            // 
            // lblDepartmentInfo
            // 
            lblDepartmentInfo.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold);
            lblDepartmentInfo.Location = new Point(23, 25);
            lblDepartmentInfo.Name = "lblDepartmentInfo";
            lblDepartmentInfo.Size = new Size(914, 38);
            lblDepartmentInfo.TabIndex = 0;
            lblDepartmentInfo.Text = "Thông tin phòng ban";
            // 
            // dtpDate
            // 
            dtpDate.CustomFormat = "dd/MM/yyyy";
            dtpDate.Format = DateTimePickerFormat.Custom;
            dtpDate.Location = new Point(126, 76);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(130, 26);
            dtpDate.TabIndex = 2;
            dtpDate.Value = new DateTime(2025, 5, 28, 19, 31, 2, 566);
            dtpDate.ValueChanged += dtpDate_ValueChanged;
            // 
            // lblDate
            // 
            lblDate.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            lblDate.Location = new Point(23, 76);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(91, 32);
            lblDate.TabIndex = 1;
            lblDate.Text = "Ngày:";
            // 
            // dgvAttendanceReport
            // 
            dgvAttendanceReport.AllowUserToAddRows = false;
            dgvAttendanceReport.AllowUserToDeleteRows = false;
            dgvAttendanceReport.AllowUserToResizeColumns = false;
            dgvAttendanceReport.AllowUserToResizeRows = false;
            dgvAttendanceReport.ColumnHeadersHeight = 30;
            dgvAttendanceReport.Location = new Point(23, 165);
            dgvAttendanceReport.Name = "dgvAttendanceReport";
            dgvAttendanceReport.ReadOnly = true;
            dgvAttendanceReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAttendanceReport.Size = new Size(850, 349);
            dgvAttendanceReport.TabIndex = 9;
            // 
            // lblLegend
            // 
            lblLegend.Font = new Font("Microsoft Sans Serif", 9F);
            lblLegend.Location = new Point(23, 120);
            lblLegend.Name = "lblLegend";
            lblLegend.Size = new Size(914, 38);
            lblLegend.TabIndex = 8;
            lblLegend.Text = "Ký hiệu màu: Xanh=Đúng giờ, Xanh dương=Chưa check out, Cam=Đi trễ, Vàng=Về sớm, Đỏ=Đi trễ&Về sớm, Xám=Vắng mặt";
            // 
            // cmbShiftFilter
            // 
            cmbShiftFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbShiftFilter.Location = new Point(330, 76);
            cmbShiftFilter.Name = "cmbShiftFilter";
            cmbShiftFilter.Size = new Size(91, 27);
            cmbShiftFilter.TabIndex = 4;
            cmbShiftFilter.SelectedIndexChanged += cmbShiftFilter_SelectedIndexChanged;
            // 
            // cmbStatusFilter
            // 
            cmbStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatusFilter.Location = new Point(530, 76);
            cmbStatusFilter.Name = "cmbStatusFilter";
            cmbStatusFilter.Size = new Size(137, 27);
            cmbStatusFilter.TabIndex = 6;
            cmbStatusFilter.SelectedIndexChanged += cmbStatusFilter_SelectedIndexChanged;
            // 
            // lblShiftFilter
            // 
            lblShiftFilter.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            lblShiftFilter.Location = new Point(280, 76);
            lblShiftFilter.Name = "lblShiftFilter";
            lblShiftFilter.Size = new Size(46, 32);
            lblShiftFilter.TabIndex = 3;
            lblShiftFilter.Text = "Ca:";
            // 
            // lblStatusFilter
            // 
            lblStatusFilter.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            lblStatusFilter.Location = new Point(440, 76);
            lblStatusFilter.Name = "lblStatusFilter";
            lblStatusFilter.Size = new Size(80, 32);
            lblStatusFilter.TabIndex = 5;
            lblStatusFilter.Text = "Trạng thái:";
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(0, 123, 255);
            btnRefresh.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(690, 73);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(114, 38);
            btnRefresh.TabIndex = 7;
            btnRefresh.Text = "Làm Mới";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // AttendanceManagerForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(898, 537);
            Controls.Add(lblDepartmentInfo);
            Controls.Add(lblDate);
            Controls.Add(dtpDate);
            Controls.Add(lblShiftFilter);
            Controls.Add(cmbShiftFilter);
            Controls.Add(lblStatusFilter);
            Controls.Add(cmbStatusFilter);
            Controls.Add(btnRefresh);
            Controls.Add(lblLegend);
            Controls.Add(dgvAttendanceReport);
            Name = "AttendanceManagerForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Báo Cáo Chấm Công Phòng Ban";
            Load += AttendanceManagerForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvAttendanceReport).EndInit();
            ResumeLayout(false);
        }
    }
}
