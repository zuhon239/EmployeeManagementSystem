namespace EmployeeManagementSystem.FormAdmin
{
    partial class SalaryAdminForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblAdminInfo;
        private Label lblMonthLabel;
        private DateTimePicker dtpMonth;
        private Label lblDepartmentLabel;
        private ComboBox cmbDepartmentFilter;
        private Button btnRefresh;
        private Button btnToggleView;
        private Button btnCalculateSelected;
        private Button btnCalculateAll;
        private DataGridView dgvSalaryReport;
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
            lblMonthLabel = new Label();
            dtpMonth = new DateTimePicker();
            lblDepartmentLabel = new Label();
            cmbDepartmentFilter = new ComboBox();
            btnRefresh = new Button();
            btnToggleView = new Button();
            btnCalculateSelected = new Button();
            btnCalculateAll = new Button();
            dgvSalaryReport = new DataGridView();
            lblLegend = new Label();

            ((System.ComponentModel.ISupportInitialize)dgvSalaryReport).BeginInit();
            SuspendLayout();

            // ✅ Form - Auto-fit màn hình
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Name = "SalaryAdminForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản Lý Lương Toàn Công Ty - Admin";
            WindowState = FormWindowState.Normal;

            // ✅ Tự động tính kích thước form dựa trên màn hình
            var screenSize = Screen.PrimaryScreen.WorkingArea;
            ClientSize = new Size(
                Math.Min(1200, (int)(screenSize.Width * 0.85)), // 85% width hoặc tối đa 1200
                Math.Min(700, (int)(screenSize.Height * 0.85))   // 85% height hoặc tối đa 700
            );

            Load += SalaryAdminForm_Load;

            // ✅ lblAdminInfo
            lblAdminInfo.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblAdminInfo.ForeColor = Color.FromArgb(220, 53, 69);
            lblAdminInfo.Location = new Point(12, 12);
            lblAdminInfo.Size = new Size(ClientSize.Width - 24, 25);
            lblAdminInfo.Text = "Admin - Quản lý lương toàn công ty";
            lblAdminInfo.TextAlign = ContentAlignment.MiddleLeft;
            lblAdminInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // ✅ lblMonthLabel
            lblMonthLabel.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point);
            lblMonthLabel.Location = new Point(12, 50);
            lblMonthLabel.Size = new Size(65, 25);
            lblMonthLabel.Text = "Tháng:";
            lblMonthLabel.TextAlign = ContentAlignment.MiddleLeft;

            // ✅ dtpMonth
            dtpMonth.CustomFormat = "MM/yyyy";
            dtpMonth.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dtpMonth.Format = DateTimePickerFormat.Custom;
            dtpMonth.Location = new Point(80, 50);
            dtpMonth.Size = new Size(100, 24);
            dtpMonth.Value = DateTime.Now;
            dtpMonth.ValueChanged += dtpMonth_ValueChanged;

            // ✅ lblDepartmentLabel
            lblDepartmentLabel.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point);
            lblDepartmentLabel.Location = new Point(200, 50);
            lblDepartmentLabel.Size = new Size(80, 25);
            lblDepartmentLabel.Text = "Phòng ban:";
            lblDepartmentLabel.TextAlign = ContentAlignment.MiddleLeft;

            // ✅ cmbDepartmentFilter
            cmbDepartmentFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDepartmentFilter.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cmbDepartmentFilter.Location = new Point(290, 50);
            cmbDepartmentFilter.Size = new Size(160, 26);
            cmbDepartmentFilter.SelectedIndexChanged += cmbDepartmentFilter_SelectedIndexChanged;

            // ✅ btnRefresh
            btnRefresh.BackColor = Color.FromArgb(0, 123, 255);
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(470, 48);
            btnRefresh.Size = new Size(80, 28);
            btnRefresh.Text = "Làm Mới";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;

            // ✅ btnToggleView
            btnToggleView.BackColor = Color.FromArgb(108, 117, 125);
            btnToggleView.Cursor = Cursors.Hand;
            btnToggleView.FlatStyle = FlatStyle.Flat;
            btnToggleView.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnToggleView.ForeColor = Color.White;
            btnToggleView.Location = new Point(560, 48);
            btnToggleView.Size = new Size(90, 28);
            btnToggleView.Text = "Chi Tiết";
            btnToggleView.UseVisualStyleBackColor = false;
            btnToggleView.Click += btnToggleView_Click;

            // ✅ btnCalculateSelected
            btnCalculateSelected.BackColor = Color.FromArgb(40, 167, 69);
            btnCalculateSelected.Cursor = Cursors.Hand;
            btnCalculateSelected.FlatStyle = FlatStyle.Flat;
            btnCalculateSelected.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnCalculateSelected.ForeColor = Color.White;
            btnCalculateSelected.Location = new Point(660, 48);
            btnCalculateSelected.Size = new Size(90, 28);
            btnCalculateSelected.Text = "Tính NV";
            btnCalculateSelected.UseVisualStyleBackColor = false;
            btnCalculateSelected.Click += btnCalculateSelected_Click;

            // ✅ btnCalculateAll
            btnCalculateAll.BackColor = Color.FromArgb(220, 53, 69);
            btnCalculateAll.Cursor = Cursors.Hand;
            btnCalculateAll.FlatStyle = FlatStyle.Flat;
            btnCalculateAll.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnCalculateAll.ForeColor = Color.White;
            btnCalculateAll.Location = new Point(760, 48);
            btnCalculateAll.Size = new Size(90, 28);
            btnCalculateAll.Text = "Tính Tất Cả";
            btnCalculateAll.UseVisualStyleBackColor = false;
            btnCalculateAll.Click += btnCalculateAll_Click;

            // ✅ lblLegend
            lblLegend.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Regular, GraphicsUnit.Point);
            lblLegend.Location = new Point(12, 85);
            lblLegend.Size = new Size(ClientSize.Width - 24, 15);
            lblLegend.Text = "Ký hiệu màu: Vàng=Chưa tính lương, Xanh=Đã tính lương | Manager (xanh dương in đậm), Admin (đỏ in đậm)";
            lblLegend.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // ✅ dgvSalaryReport - Responsive size
            dgvSalaryReport.AllowUserToAddRows = false;
            dgvSalaryReport.AllowUserToDeleteRows = false;
            dgvSalaryReport.AllowUserToResizeRows = false;
            dgvSalaryReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSalaryReport.BackgroundColor = Color.White;
            dgvSalaryReport.BorderStyle = BorderStyle.Fixed3D;

            // Column headers style
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(220, 53, 69);
            dataGridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(220, 53, 69);
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvSalaryReport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvSalaryReport.ColumnHeadersHeight = 30;
            dgvSalaryReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Default cell style
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(220, 53, 69);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvSalaryReport.DefaultCellStyle = dataGridViewCellStyle2;

            dgvSalaryReport.EnableHeadersVisualStyles = false;
            dgvSalaryReport.GridColor = Color.LightGray;
            dgvSalaryReport.Location = new Point(12, 110);
            dgvSalaryReport.MultiSelect = false;
            dgvSalaryReport.ReadOnly = true;
            dgvSalaryReport.RowHeadersVisible = false;
            dgvSalaryReport.RowTemplate.Height = 22;
            dgvSalaryReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSalaryReport.ScrollBars = ScrollBars.Both;

            // ✅ DataGridView responsive size
            dgvSalaryReport.Size = new Size(ClientSize.Width - 24, ClientSize.Height - 130);
            dgvSalaryReport.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // ✅ Add controls to form
            Controls.AddRange(new Control[] {
                lblAdminInfo, lblMonthLabel, dtpMonth,
                lblDepartmentLabel, cmbDepartmentFilter,
                btnRefresh, btnToggleView, btnCalculateSelected, btnCalculateAll,
                lblLegend, dgvSalaryReport
            });

            ((System.ComponentModel.ISupportInitialize)dgvSalaryReport).EndInit();
            ResumeLayout(false);
        }
    }
}
