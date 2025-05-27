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
            cmbShiftFilter = new ComboBox();
            cmbStatusFilter = new ComboBox();
            lblShiftFilter = new Label();
            lblStatusFilter = new Label();
            btnToggleView = new Button();
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
            // dtpMonth
            // 
            dtpMonth.CustomFormat = "MM/yyyy";
            dtpMonth.Format = DateTimePickerFormat.Custom;
            dtpMonth.Location = new Point(126, 76);
            dtpMonth.Margin = new Padding(3, 4, 3, 4);
            dtpMonth.Name = "dtpMonth";
            dtpMonth.Size = new Size(114, 26);
            dtpMonth.TabIndex = 2;
            dtpMonth.Value = new DateTime(2025, 5, 27, 21, 16, 50, 37);
            dtpMonth.ValueChanged += dtpMonth_ValueChanged;
            // 
            // lblMonth
            // 
            lblMonth.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            lblMonth.Location = new Point(23, 76);
            lblMonth.Name = "lblMonth";
            lblMonth.Size = new Size(91, 32);
            lblMonth.TabIndex = 1;
            lblMonth.Text = "Tháng/Năm:";
            // 
            // dgvAttendanceReport
            // 
            dgvAttendanceReport.AllowUserToAddRows = false;
            dgvAttendanceReport.AllowUserToDeleteRows = false;
            dgvAttendanceReport.ColumnHeadersHeight = 30;
            dgvAttendanceReport.Location = new Point(23, 165);
            dgvAttendanceReport.Margin = new Padding(3, 4, 3, 4);
            dgvAttendanceReport.Name = "dgvAttendanceReport";
            dgvAttendanceReport.ReadOnly = true;
            dgvAttendanceReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAttendanceReport.Size = new Size(1149, 570);
            dgvAttendanceReport.TabIndex = 9;
            // 
            // lblLegend
            // 
            lblLegend.Font = new Font("Microsoft Sans Serif", 9F);
            lblLegend.Location = new Point(23, 120);
            lblLegend.Name = "lblLegend";
            lblLegend.Size = new Size(914, 38);
            lblLegend.TabIndex = 8;
            lblLegend.Text = "Ký hiệu: P=Đúng giờ, L=Đi trễ, E=Về sớm, LE=Đi trễ&Về sớm, S=Sáng, C=Chiều, Trống=Vắng mặt";
            // 
            // cmbShiftFilter
            // 
            cmbShiftFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbShiftFilter.Location = new Point(320, 76);
            cmbShiftFilter.Margin = new Padding(3, 4, 3, 4);
            cmbShiftFilter.Name = "cmbShiftFilter";
            cmbShiftFilter.Size = new Size(91, 27);
            cmbShiftFilter.TabIndex = 4;
            cmbShiftFilter.SelectedIndexChanged += cmbShiftFilter_SelectedIndexChanged;
            // 
            // cmbStatusFilter
            // 
            cmbStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatusFilter.Location = new Point(526, 76);
            cmbStatusFilter.Margin = new Padding(3, 4, 3, 4);
            cmbStatusFilter.Name = "cmbStatusFilter";
            cmbStatusFilter.Size = new Size(137, 27);
            cmbStatusFilter.TabIndex = 6;
            cmbStatusFilter.SelectedIndexChanged += cmbStatusFilter_SelectedIndexChanged;
            // 
            // lblShiftFilter
            // 
            lblShiftFilter.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            lblShiftFilter.Location = new Point(263, 76);
            lblShiftFilter.Name = "lblShiftFilter";
            lblShiftFilter.Size = new Size(46, 32);
            lblShiftFilter.TabIndex = 3;
            lblShiftFilter.Text = "Ca:";
            // 
            // lblStatusFilter
            // 
            lblStatusFilter.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            lblStatusFilter.Location = new Point(434, 76);
            lblStatusFilter.Name = "lblStatusFilter";
            lblStatusFilter.Size = new Size(80, 32);
            lblStatusFilter.TabIndex = 5;
            lblStatusFilter.Text = "Trạng thái:";
            // 
            // btnToggleView
            // 
            btnToggleView.BackColor = Color.FromArgb(255, 193, 7);
            btnToggleView.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            btnToggleView.ForeColor = Color.Black;
            btnToggleView.Location = new Point(686, 73);
            btnToggleView.Margin = new Padding(3, 4, 3, 4);
            btnToggleView.Name = "btnToggleView";
            btnToggleView.Size = new Size(114, 38);
            btnToggleView.TabIndex = 7;
            btnToggleView.Text = "Xem Chi Tiết";
            btnToggleView.UseVisualStyleBackColor = false;
            btnToggleView.Click += btnToggleView_Click;
            // 
            // AttendanceManagerForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 760);
            Controls.Add(lblDepartmentInfo);
            Controls.Add(lblMonth);
            Controls.Add(dtpMonth);
            Controls.Add(lblShiftFilter);
            Controls.Add(cmbShiftFilter);
            Controls.Add(lblStatusFilter);
            Controls.Add(cmbStatusFilter);
            Controls.Add(btnToggleView);
            Controls.Add(lblLegend);
            Controls.Add(dgvAttendanceReport);
            Margin = new Padding(3, 4, 3, 4);
            Name = "AttendanceManagerForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Báo Cáo Chấm Công Phòng Ban";
            Load += AttendanceManagerForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvAttendanceReport).EndInit();
            ResumeLayout(false);
        }
    }
}
