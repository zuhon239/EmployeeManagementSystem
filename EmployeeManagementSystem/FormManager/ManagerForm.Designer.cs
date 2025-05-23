namespace EmployeeManagementSystem
{
    partial class ManagerForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SuspendLayout();

            // TableLayoutPanel for structured layout
            tableLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                RowStyles = { new RowStyle(SizeType.Absolute, 80F), new RowStyle(SizeType.Absolute, 80F), new RowStyle(SizeType.Percent, 100F) },
                Padding = new Padding(20)
            };
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            // Header Panel for Welcome Title
            headerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(0, 122, 204), // Modern blue
            };

            lblWelcome = new Label
            {
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Text = "Employee Management System"
            };
            headerPanel.Controls.Add(lblWelcome);

            // Welcome Manager Label
            lblWelcomeManager = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(64, 64, 64), // Dark gray for contrast
                Dock = DockStyle.Top,
                Padding = new Padding(0, 20, 0, 20)
            };

            // FlowLayoutPanel for Buttons
            buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top, // Changed from Fill to Top to constrain height
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(0, 20, 0, 20),
                Anchor = AnchorStyles.None // Center the panel
            };

            // Button: Attendance
            btnAttendance = new Button
            {
                Size = new Size(250, 50),
                Name = "btnAttendance",
                Text = "Chấm công",
                ForeColor = Color.White,
                BackColor = Color.FromArgb(0, 153, 76), // Green
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(0, 10, 0, 10)
            };
            btnAttendance.FlatAppearance.BorderSize = 0;
            btnAttendance.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 128, 64); // Darker green on hover
            btnAttendance.Click += BtnAttendance_Click;

            // Button: Leave Request
            btnLeaveRequest = new Button
            {
                Size = new Size(250, 50),
                Name = "btnLeaveRequest",
                Text = "Xin nghỉ phép",
                ForeColor = Color.White,
                BackColor = Color.FromArgb(0, 153, 76),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(0, 10, 0, 10)
            };
            btnLeaveRequest.FlatAppearance.BorderSize = 0;
            btnLeaveRequest.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 128, 64);
            btnLeaveRequest.Click += BtnLeaveRequest_Click;

            // Button: Manage Employees
            btnManageEmployees = new Button
            {
                Size = new Size(250, 50),
                Name = "btnManageEmployees",
                Text = "Quản lý nhân sự",
                ForeColor = Color.White,
                BackColor = Color.FromArgb(0, 153, 76),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(0, 10, 0, 10)
            };
            btnManageEmployees.FlatAppearance.BorderSize = 0;
            btnManageEmployees.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 128, 64);
            btnManageEmployees.Click += BtnManageEmployees_Click;

            // Button: Log Out
            btnLogout = new Button
            {
                Size = new Size(250, 50),
                Name = "btnLogout",
                Text = "Đăng xuất",
                ForeColor = Color.White,
                BackColor = Color.FromArgb(204, 0, 0), // Red to distinguish from other buttons
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(0, 10, 0, 10)
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatAppearance.MouseOverBackColor = Color.FromArgb(179, 0, 0); // Darker red on hover
            btnLogout.Click += BtnLogout_Click;
            
            // Add tooltips
            toolTip = new ToolTip();
            toolTip.SetToolTip(btnAttendance, "Ghi lại thời gian làm việc của nhân viên");
            toolTip.SetToolTip(btnLeaveRequest, "Gửi yêu cầu nghỉ phép");
            toolTip.SetToolTip(btnManageEmployees, "Quản lý thông tin nhân viên");
            toolTip.SetToolTip(btnLogout, "Đăng xuất khỏi hệ thống");
            // Add controls to button panel
            buttonPanel.Controls.Add(btnAttendance);
            buttonPanel.Controls.Add(btnLeaveRequest);
            buttonPanel.Controls.Add(btnManageEmployees);
            buttonPanel.Controls.Add(btnLogout);
            // Center the FlowLayoutPanel within the TableLayoutPanel cell
            buttonPanel.Location = new Point((tableLayoutPanel.Width - buttonPanel.PreferredSize.Width) / 2, 20);

            // Add controls to TableLayoutPanel
            tableLayoutPanel.Controls.Add(headerPanel, 0, 0);
            tableLayoutPanel.Controls.Add(lblWelcomeManager, 0, 1);
            tableLayoutPanel.Controls.Add(buttonPanel, 0, 2);

            // Add TableLayoutPanel to form
            Controls.Add(tableLayoutPanel);

            // Form configuration
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 500); // Increased height to accommodate buttons
            Name = "ManagerForm";
            Text = "Trang Chủ Quản Lý";
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(245, 245, 245); // Light gray background for modern look

            // Ensure button panel is centered on form load
            Load += (s, e) =>
            {
                buttonPanel.Location = new Point((tableLayoutPanel.Width - buttonPanel.PreferredSize.Width) / 2, 20);
            };

            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel;
        private Panel headerPanel;
        private Button btnAttendance;
        private Button btnLeaveRequest;
        private Button btnManageEmployees;
        private Button btnLogout;
        private Label lblWelcome;
        private Label lblWelcomeManager;
        private FlowLayoutPanel buttonPanel;
        private ToolTip toolTip;

    }
}