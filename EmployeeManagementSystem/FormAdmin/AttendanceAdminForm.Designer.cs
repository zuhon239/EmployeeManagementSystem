namespace EmployeeManagementSystem.FormAdmin
{
    partial class AttendanceAdminForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblAdminInfo;
        private DateTimePicker dtpDate;
        private Label lblDate;
        private ComboBox cmbDepartmentFilter;
        private Label lblDepartmentFilter;
        private ComboBox cmbShiftFilter;
        private Label lblShiftFilter;
        private ComboBox cmbStatusFilter;
        private Label lblStatusFilter;
        private Button btnRefresh;
        private DataGridView dgvAttendanceReport;
        private Label lblLegend;

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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            lblAdminInfo = new Label();
            dtpDate = new DateTimePicker();
            lblDate = new Label();
            cmbDepartmentFilter = new ComboBox();
            lblDepartmentFilter = new Label();
            cmbShiftFilter = new ComboBox();
            lblShiftFilter = new Label();
            cmbStatusFilter = new ComboBox();
            lblStatusFilter = new Label();
            btnRefresh = new Button();
            dgvAttendanceReport = new DataGridView();
            lblLegend = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvAttendanceReport).BeginInit();
            SuspendLayout();
            // 
            // lblAdminInfo
            // 
            lblAdminInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblAdminInfo.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            lblAdminInfo.ForeColor = Color.FromArgb(220, 53, 69);
            lblAdminInfo.Location = new Point(12, 12);
            lblAdminInfo.Name = "lblAdminInfo";
            lblAdminInfo.Size = new Size(117, 25);
            lblAdminInfo.TabIndex = 0;
            lblAdminInfo.Text = "Admin - Quản lý chấm công toàn hệ thống";
            lblAdminInfo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dtpDate
            // 
            dtpDate.CustomFormat = "dd/MM/yyyy";
            dtpDate.Font = new Font("Microsoft Sans Serif", 8F);
            dtpDate.Format = DateTimePickerFormat.Custom;
            dtpDate.Location = new Point(65, 50);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(100, 20);
            dtpDate.TabIndex = 2;
            dtpDate.Value = new DateTime(2025, 5, 28, 18, 30, 11, 450);
            dtpDate.ValueChanged += dtpDate_ValueChanged;
            // 
            // lblDate
            // 
            lblDate.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            lblDate.Location = new Point(12, 50);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(50, 25);
            lblDate.TabIndex = 1;
            lblDate.Text = "Ngày:";
            lblDate.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbDepartmentFilter
            // 
            cmbDepartmentFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDepartmentFilter.Font = new Font("Microsoft Sans Serif", 8F);
            cmbDepartmentFilter.Location = new Point(255, 50);
            cmbDepartmentFilter.Name = "cmbDepartmentFilter";
            cmbDepartmentFilter.Size = new Size(140, 21);
            cmbDepartmentFilter.TabIndex = 4;
            cmbDepartmentFilter.SelectedIndexChanged += cmbDepartmentFilter_SelectedIndexChanged;
            // 
            // lblDepartmentFilter
            // 
            lblDepartmentFilter.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            lblDepartmentFilter.Location = new Point(180, 50);
            lblDepartmentFilter.Name = "lblDepartmentFilter";
            lblDepartmentFilter.Size = new Size(70, 25);
            lblDepartmentFilter.TabIndex = 3;
            lblDepartmentFilter.Text = "Phòng ban:";
            lblDepartmentFilter.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbShiftFilter
            // 
            cmbShiftFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbShiftFilter.Font = new Font("Microsoft Sans Serif", 8F);
            cmbShiftFilter.Location = new Point(445, 50);
            cmbShiftFilter.Name = "cmbShiftFilter";
            cmbShiftFilter.Size = new Size(70, 21);
            cmbShiftFilter.TabIndex = 6;
            cmbShiftFilter.SelectedIndexChanged += cmbShiftFilter_SelectedIndexChanged;
            // 
            // lblShiftFilter
            // 
            lblShiftFilter.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            lblShiftFilter.Location = new Point(410, 50);
            lblShiftFilter.Name = "lblShiftFilter";
            lblShiftFilter.Size = new Size(30, 25);
            lblShiftFilter.TabIndex = 5;
            lblShiftFilter.Text = "Ca:";
            lblShiftFilter.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbStatusFilter
            // 
            cmbStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatusFilter.Font = new Font("Microsoft Sans Serif", 8F);
            cmbStatusFilter.Location = new Point(605, 50);
            cmbStatusFilter.Name = "cmbStatusFilter";
            cmbStatusFilter.Size = new Size(100, 21);
            cmbStatusFilter.TabIndex = 8;
            cmbStatusFilter.SelectedIndexChanged += cmbStatusFilter_SelectedIndexChanged;
            // 
            // lblStatusFilter
            // 
            lblStatusFilter.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            lblStatusFilter.Location = new Point(530, 50);
            lblStatusFilter.Name = "lblStatusFilter";
            lblStatusFilter.Size = new Size(70, 25);
            lblStatusFilter.TabIndex = 7;
            lblStatusFilter.Text = "Trạng thái:";
            lblStatusFilter.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(0, 123, 255);
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(720, 48);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(80, 28);
            btnRefresh.TabIndex = 9;
            btnRefresh.Text = "Làm Mới";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // dgvAttendanceReport
            // 
            dgvAttendanceReport.AllowUserToAddRows = false;
            dgvAttendanceReport.AllowUserToDeleteRows = false;
            dgvAttendanceReport.AllowUserToResizeRows = false;
            dgvAttendanceReport.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvAttendanceReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAttendanceReport.BackgroundColor = Color.White;
            dgvAttendanceReport.BorderStyle = BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(220, 53, 69);
            dataGridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(220, 53, 69);
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvAttendanceReport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvAttendanceReport.ColumnHeadersHeight = 28;
            dgvAttendanceReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 7F);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(220, 53, 69);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvAttendanceReport.DefaultCellStyle = dataGridViewCellStyle2;
            dgvAttendanceReport.EnableHeadersVisualStyles = false;
            dgvAttendanceReport.GridColor = Color.LightGray;
            dgvAttendanceReport.Location = new Point(12, 106);
            dgvAttendanceReport.MultiSelect = false;
            dgvAttendanceReport.Name = "dgvAttendanceReport";
            dgvAttendanceReport.ReadOnly = true;
            dgvAttendanceReport.RowHeadersVisible = false;
            dgvAttendanceReport.RowTemplate.Height = 20;
            dgvAttendanceReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAttendanceReport.Size = new Size(935, 379);
            dgvAttendanceReport.TabIndex = 12;
            dgvAttendanceReport.CellContentClick += dgvAttendanceReport_CellContentClick;
            // 
            // lblLegend
            // 
            lblLegend.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblLegend.Font = new Font("Microsoft Sans Serif", 8F);
            lblLegend.Location = new Point(12, 88);
            lblLegend.Name = "lblLegend";
            lblLegend.Size = new Size(680, 15);
            lblLegend.TabIndex = 11;
            lblLegend.Text = "Ký hiệu màu: Xanh=Đúng giờ, Xanh dương=Chưa check out, Cam=Đi trễ, Vàng=Về sớm, Xám=Vắng mặt | Manager in đậm";
            lblLegend.Click += lblLegend_Click;
            // 
            // AttendanceAdminForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(959, 536);
            Controls.Add(lblAdminInfo);
            Controls.Add(lblDate);
            Controls.Add(dtpDate);
            Controls.Add(lblDepartmentFilter);
            Controls.Add(cmbDepartmentFilter);
            Controls.Add(lblShiftFilter);
            Controls.Add(cmbShiftFilter);
            Controls.Add(lblStatusFilter);
            Controls.Add(cmbStatusFilter);
            Controls.Add(btnRefresh);
            Controls.Add(lblLegend);
            Controls.Add(dgvAttendanceReport);
            Font = new Font("Microsoft Sans Serif", 9F);
            Name = "AttendanceAdminForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản Lý Chấm Công Toàn Hệ Thống - Admin";
            Load += AttendanceAdminForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvAttendanceReport).EndInit();
            ResumeLayout(false);
        }
    }
}
