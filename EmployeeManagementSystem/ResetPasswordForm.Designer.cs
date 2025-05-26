namespace EmployeeManagementSystem
{
    partial class ResetPasswordForm
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
            this.SuspendLayout();

            // Form settings
            this.Text = "Đặt lại mật khẩu";
            this.Size = new Size(450, 380);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Title
            lblTitle = new Label
            {
                Text = "Đặt lại mật khẩu",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                AutoSize = true,
                Location = new Point(20, 20)
            };

            // User info
            lblUserInfo = new Label
            {
                Text = $"Tài khoản: {_username}\nEmail: {_email}",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.DarkGray,
                AutoSize = true,
                Location = new Point(20, 50)
            };

            // Instructions
            lblInstructions = new Label
            {
                Text = "Vui lòng nhập mã token đã được gửi đến email của bạn:",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(20, 85),
                MaximumSize = new Size(400, 0)
            };

            // Token
            lblToken = new Label
            {
                Text = "Mã token:",
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Location = new Point(20, 115)
            };

            txtToken = new TextBox
            {
                Font = new Font("Consolas", 10),
                Location = new Point(20, 135),
                Size = new Size(380, 25),
                MaxLength = 8,
                CharacterCasing = CharacterCasing.Upper,
                PlaceholderText = "Nhập mã token 8 ký tự"
            };

            // New password
            lblNewPassword = new Label
            {
                Text = "Mật khẩu mới:",
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Location = new Point(20, 175)
            };

            txtNewPassword = new TextBox
            {
                Font = new Font("Segoe UI", 9),
                Location = new Point(20, 195),
                Size = new Size(380, 25),
                UseSystemPasswordChar = true,
                PlaceholderText = "Nhập mật khẩu mới (ít nhất 6 ký tự)"
            };

            // Confirm password
            lblConfirmPassword = new Label
            {
                Text = "Xác nhận mật khẩu:",
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Location = new Point(20, 235)
            };

            txtConfirmPassword = new TextBox
            {
                Font = new Font("Segoe UI", 9),
                Location = new Point(20, 255),
                Size = new Size(380, 25),
                UseSystemPasswordChar = true,
                PlaceholderText = "Nhập lại mật khẩu mới"
            };

            // Show password checkbox
            chkShowPassword = new CheckBox
            {
                Text = "Hiển thị mật khẩu",
                Font = new Font("Segoe UI", 8),
                AutoSize = true,
                Location = new Point(20, 285)
            };

            // Buttons
            btnResetPassword = new Button
            {
                Text = "Đặt lại mật khẩu",
                Font = new Font("Segoe UI", 9),
                Size = new Size(120, 35),
                Location = new Point(200, 310),
                BackColor = Color.DodgerBlue,
                ForeColor = Color.White,
                UseVisualStyleBackColor = false
            };

            btnCancel = new Button
            {
                Text = "Hủy",
                Font = new Font("Segoe UI", 9),
                Size = new Size(80, 35),
                Location = new Point(330, 310),
                DialogResult = DialogResult.Cancel
            };

            // Add controls to form
            this.Controls.AddRange(new Control[]
            {
                lblTitle, lblUserInfo, lblInstructions, lblToken, txtToken,
                lblNewPassword, txtNewPassword, lblConfirmPassword, txtConfirmPassword,
                chkShowPassword, btnResetPassword, btnCancel
            });

            // Event handlers
            btnResetPassword.Click += BtnResetPassword_Click;
            chkShowPassword.CheckedChanged += ChkShowPassword_CheckedChanged;
            txtToken.KeyPress += TxtToken_KeyPress;
            txtNewPassword.KeyPress += TxtPassword_KeyPress;
            txtConfirmPassword.KeyPress += TxtPassword_KeyPress;

            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private void InitializeFormSettings()
        {
            this.CancelButton = btnCancel;
            this.AcceptButton = btnResetPassword;
        }
        // Controls
        private Label lblTitle;
        private Label lblUserInfo;
        private Label lblToken;
        private TextBox txtToken;
        private Label lblNewPassword;
        private TextBox txtNewPassword;
        private Label lblConfirmPassword;
        private TextBox txtConfirmPassword;
        private Button btnResetPassword;
        private Button btnCancel;
        private CheckBox chkShowPassword;
        private Label lblInstructions;
        #endregion
    }
}