namespace EmployeeManagementSystem
{
    partial class EmployeeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private Label lblWelcome;
        private Button btnRequestLeave;
        private Button btnAttendance; // New
        private Button btnLogout;     // New
        private TableLayoutPanel tableLayoutPanel;
        private Panel headerPanel;
        private Label lblHeader;
        private FlowLayoutPanel buttonPanel; // New
        private ToolTip toolTip; // New for tooltips

        private void InitializeComponent()
        {
            // Initialize ToolTip
            toolTip = new ToolTip();

            // Initialize TableLayoutPanel for layout
            tableLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                AutoSize = true
            };
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F)); // Header
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F)); // Welcome label
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Button panel

            // Header Panel
            headerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = System.Drawing.Color.FromArgb(0, 102, 204) // Dark blue
            };
            lblHeader = new Label
            {
                Text = "Hệ Thống Quản Lý Nhân Sự",
                Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.White,
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };
            headerPanel.Controls.Add(lblHeader);

            // Welcome Label
            lblWelcome = new Label
            {
                Text = "Chào mừng đến với hệ thống quản lý nhân sự!",
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular),
                ForeColor = System.Drawing.Color.Black,
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };

            // FlowLayoutPanel for Buttons
            buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(0, 20, 0, 20),
                Anchor = AnchorStyles.None // Center the panel
            };

            // Request Leave Button
            btnRequestLeave = new Button
            {
                Text = "Xin Nghỉ Phép",
                Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold),
                BackColor = System.Drawing.Color.FromArgb(0, 153, 76), // Green
                ForeColor = System.Drawing.Color.White,
                Size = new System.Drawing.Size(250, 50),
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(0, 10, 0, 10)
            };
            btnRequestLeave.FlatAppearance.BorderSize = 0;
            btnRequestLeave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(0, 128, 64); // Darker green on hover
            btnRequestLeave.Click += BtnRequestLeave_Click;
            toolTip.SetToolTip(btnRequestLeave, "Gửi yêu cầu nghỉ phép");

            // Attendance Button
            btnAttendance = new Button
            {
                Text = "Chấm công",
                Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold),
                BackColor = System.Drawing.Color.FromArgb(0, 153, 76), // Green
                ForeColor = System.Drawing.Color.White,
                Size = new System.Drawing.Size(250, 50),
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(0, 10, 0, 10)
            };
            btnAttendance.FlatAppearance.BorderSize = 0;
            btnAttendance.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(0, 128, 64); // Darker green on hover
            btnAttendance.Click += BtnAttendance_Click;
            toolTip.SetToolTip(btnAttendance, "Ghi lại thời gian làm việc của nhân viên");

            // Log Out Button
            btnLogout = new Button
            {
                Text = "Đăng xuất",
                Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold),
                BackColor = System.Drawing.Color.FromArgb(204, 0, 0), // Red
                ForeColor = System.Drawing.Color.White,
                Size = new System.Drawing.Size(250, 50),
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(0, 10, 0, 10)
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(179, 0, 0); // Darker red on hover
            btnLogout.Click += BtnLogout_Click;
            toolTip.SetToolTip(btnLogout, "Đăng xuất khỏi hệ thống");

            // Add buttons to FlowLayoutPanel
            buttonPanel.Controls.Add(btnAttendance);
            buttonPanel.Controls.Add(btnRequestLeave);
            buttonPanel.Controls.Add(btnLogout);

            // Center the FlowLayoutPanel
            buttonPanel.Location = new Point((tableLayoutPanel.Width - buttonPanel.PreferredSize.Width) / 2, 20);

            // Add controls to TableLayoutPanel
            tableLayoutPanel.Controls.Add(headerPanel, 0, 0);
            tableLayoutPanel.Controls.Add(lblWelcome, 0, 1);
            tableLayoutPanel.Controls.Add(buttonPanel, 0, 2);

            // Add TableLayoutPanel to form
            Controls.Add(tableLayoutPanel);

            // Form configuration
            Text = "Trang Chính Nhân Viên";
            Size = new System.Drawing.Size(600, 500); // Match ManagerForm size
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = System.Drawing.Color.FromArgb(245, 245, 245); // Light gray background

            // Ensure button panel is centered on form load
            Load += (s, e) =>
            {
                buttonPanel.Location = new Point((tableLayoutPanel.Width - buttonPanel.PreferredSize.Width) / 2, 20);
            };
        }
    }
}