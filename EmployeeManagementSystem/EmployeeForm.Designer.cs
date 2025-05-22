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
        private TableLayoutPanel tableLayoutPanel;
        private Panel headerPanel;
        private Label lblHeader;

        private void InitializeComponent()
        {
            // Khởi tạo TableLayoutPanel để bố cục
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
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F)); // Button

            // Header Panel
            headerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = System.Drawing.Color.FromArgb(0, 102, 204) // Màu xanh dương đậm
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

            // Request Leave Button
            btnRequestLeave = new Button
            {
                Text = "Xin Nghỉ Phép",
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular),
                BackColor = System.Drawing.Color.FromArgb(46, 204, 113), // Màu xanh lá
                ForeColor = System.Drawing.Color.White,
                Size = new System.Drawing.Size(200, 40),
                Anchor = AnchorStyles.None
            };
            btnRequestLeave.Click += BtnRequestLeave_Click;

            // Thêm các control vào TableLayoutPanel
            tableLayoutPanel.Controls.Add(headerPanel, 0, 0);
            tableLayoutPanel.Controls.Add(lblWelcome, 0, 1);
            tableLayoutPanel.Controls.Add(btnRequestLeave, 0, 2);

            // Thêm TableLayoutPanel vào form
            Controls.Add(tableLayoutPanel);

            // Cấu hình form
            Text = "Trang Chính Nhân Viên";
            Size = new System.Drawing.Size(500, 300); // Tăng kích thước form cho đẹp hơn
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen; // Hiển thị giữa màn hình
            BackColor = System.Drawing.Color.FromArgb(245, 245, 245); // Màu nền nhẹ
        }
    }
}