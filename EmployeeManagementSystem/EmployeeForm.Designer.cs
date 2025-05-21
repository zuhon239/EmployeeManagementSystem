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
        private void InitializeComponent()
        {
            lblWelcome = new Label
            {
                Text = "Chào mừng đến với hệ thống quản lý nhân sự!", // Sẽ được cập nhật trong LoadUserName
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(300, 20)
            };
            btnRequestLeave = new Button
            {
                Text = "Xin nghỉ phép",
                Location = new System.Drawing.Point(20, 50),
                Size = new System.Drawing.Size(150, 30)
            };

            btnRequestLeave.Click += BtnRequestLeave_Click;

            Controls.AddRange(new Control[] { lblWelcome, btnRequestLeave });

            Text = "Trang Chính Nhân Viên";
            Size = new System.Drawing.Size(400, 200);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }
    }
}