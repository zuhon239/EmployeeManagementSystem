namespace EmployeeManagementSystem
{
    partial class AttendanceForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblEmployeeInfo;
        private Button btnCheckIn;
        private Button btnCheckOut;
        private Button btnCheckWiFi;
        private Button btnRefresh;
        private DataGridView dgvHistory;

        // ✅ Thêm DateTimePicker và nút filter
        private DateTimePicker dtpFilterDate;
        private Button btnFilter;
        private Button btnClearFilter;
        private Label lblFilterDate;

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
            lblEmployeeInfo = new Label();
            btnCheckIn = new Button();
            btnCheckOut = new Button();
            btnCheckWiFi = new Button();
            btnRefresh = new Button();
            dgvHistory = new DataGridView();
            dtpFilterDate = new DateTimePicker();
            btnFilter = new Button();
            btnClearFilter = new Button();
            lblFilterDate = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
            SuspendLayout();
            // 
            // lblEmployeeInfo
            // 
            lblEmployeeInfo.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            lblEmployeeInfo.Location = new Point(20, 20);
            lblEmployeeInfo.Name = "lblEmployeeInfo";
            lblEmployeeInfo.Size = new Size(600, 25);
            lblEmployeeInfo.TabIndex = 0;
            lblEmployeeInfo.Text = "Thông tin nhân viên";
            // 
            // btnCheckIn
            // 
            btnCheckIn.BackColor = Color.FromArgb(0, 123, 255);
            btnCheckIn.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            btnCheckIn.ForeColor = Color.White;
            btnCheckIn.Location = new Point(20, 60);
            btnCheckIn.Name = "btnCheckIn";
            btnCheckIn.Size = new Size(100, 40);
            btnCheckIn.TabIndex = 1;
            btnCheckIn.Text = "Check In";
            btnCheckIn.UseVisualStyleBackColor = false;
            btnCheckIn.Click += btnCheckIn_Click;
            // 
            // btnCheckOut
            // 
            btnCheckOut.BackColor = Color.FromArgb(220, 53, 69);
            btnCheckOut.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            btnCheckOut.ForeColor = Color.White;
            btnCheckOut.Location = new Point(140, 60);
            btnCheckOut.Name = "btnCheckOut";
            btnCheckOut.Size = new Size(100, 40);
            btnCheckOut.TabIndex = 2;
            btnCheckOut.Text = "Check Out";
            btnCheckOut.UseVisualStyleBackColor = false;
            btnCheckOut.Click += btnCheckOut_Click;
            // 
            // btnCheckWiFi
            // 
            btnCheckWiFi.BackColor = Color.FromArgb(40, 167, 69);
            btnCheckWiFi.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            btnCheckWiFi.ForeColor = Color.White;
            btnCheckWiFi.Location = new Point(260, 60);
            btnCheckWiFi.Name = "btnCheckWiFi";
            btnCheckWiFi.Size = new Size(120, 40);
            btnCheckWiFi.TabIndex = 3;
            btnCheckWiFi.Text = "Kiểm Tra WiFi";
            btnCheckWiFi.UseVisualStyleBackColor = false;
            btnCheckWiFi.Click += btnCheckWiFi_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(108, 117, 125);
            btnRefresh.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(400, 60);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(100, 40);
            btnRefresh.TabIndex = 4;
            btnRefresh.Text = "Làm Mới";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // dgvHistory
            // 
            dgvHistory.AllowUserToAddRows = false;
            dgvHistory.AllowUserToDeleteRows = false;
            dgvHistory.Location = new Point(20, 160);
            dgvHistory.Name = "dgvHistory";
            dgvHistory.ReadOnly = true;
            dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistory.Size = new Size(860, 460);
            dgvHistory.TabIndex = 9;
            // 
            // dtpFilterDate
            // 
            dtpFilterDate.CustomFormat = "dd/MM/yyyy";
            dtpFilterDate.Format = DateTimePickerFormat.Custom;
            dtpFilterDate.Location = new Point(147, 120);
            dtpFilterDate.Name = "dtpFilterDate";
            dtpFilterDate.Size = new Size(120, 23);
            dtpFilterDate.TabIndex = 6;
            dtpFilterDate.Value = new DateTime(2025, 5, 27, 10, 41, 29, 576);
            // 
            // btnFilter
            // 
            btnFilter.BackColor = Color.FromArgb(255, 193, 7);
            btnFilter.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            btnFilter.ForeColor = Color.Black;
            btnFilter.Location = new Point(278, 115);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(80, 30);
            btnFilter.TabIndex = 7;
            btnFilter.Text = "Lọc";
            btnFilter.UseVisualStyleBackColor = false;
            btnFilter.Click += btnFilter_Click;
            // 
            // btnClearFilter
            // 
            btnClearFilter.BackColor = Color.FromArgb(108, 117, 125);
            btnClearFilter.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            btnClearFilter.ForeColor = Color.White;
            btnClearFilter.Location = new Point(375, 117);
            btnClearFilter.Name = "btnClearFilter";
            btnClearFilter.Size = new Size(80, 30);
            btnClearFilter.TabIndex = 8;
            btnClearFilter.Text = "Bỏ Lọc";
            btnClearFilter.UseVisualStyleBackColor = false;
            btnClearFilter.Click += btnClearFilter_Click;
            // 
            // lblFilterDate
            // 
            lblFilterDate.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            lblFilterDate.Location = new Point(20, 120);
            lblFilterDate.Name = "lblFilterDate";
            lblFilterDate.Size = new Size(121, 25);
            lblFilterDate.TabIndex = 5;
            lblFilterDate.Text = "Lọc theo ngày:";
            lblFilterDate.Click += lblFilterDate_Click;
            // 
            // AttendanceForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 650);
            Controls.Add(lblEmployeeInfo);
            Controls.Add(btnCheckIn);
            Controls.Add(btnCheckOut);
            Controls.Add(btnCheckWiFi);
            Controls.Add(btnRefresh);
            Controls.Add(lblFilterDate);
            Controls.Add(dtpFilterDate);
            Controls.Add(btnFilter);
            Controls.Add(btnClearFilter);
            Controls.Add(dgvHistory);
            Name = "AttendanceForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hệ Thống Chấm Công";
            Load += AttendanceForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
            ResumeLayout(false);
        }
    }
}
