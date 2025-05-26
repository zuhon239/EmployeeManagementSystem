using EmployeeManagementSystem.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagementSystem
{
    public partial class ForgotPassWordForm : Form
    {
        private readonly LoginController _loginController;

        public ForgotPassWordForm(LoginController loginController)
        {
            InitializeComponent();
            _loginController = loginController;
            SetupFormStyles();
        }

        private void SetupFormStyles()
        {
            // Set form properties
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(245, 247, 250);

            // Add shadow effect
            this.Paint += ForgotPassWordForm_Paint;

            // Set rounded corners for panels
            SetRoundedCorners();
        }

        private void SetRoundedCorners()
        {
            // Apply rounded corners to main content panel
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            int radius = 8;
            var rect = new Rectangle(0, 0, pnlContent.Width, pnlContent.Height);

            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            pnlContent.Region = new Region(path);

            // Apply rounded corners to header panel
            var headerPath = new System.Drawing.Drawing2D.GraphicsPath();
            var headerRect = new Rectangle(0, 0, pnlHeader.Width, pnlHeader.Height);

            headerPath.AddArc(headerRect.X, headerRect.Y, radius, radius, 180, 90);
            headerPath.AddArc(headerRect.X + headerRect.Width - radius, headerRect.Y, radius, radius, 270, 90);
            headerPath.AddLine(headerRect.X + headerRect.Width, headerRect.Y + radius, headerRect.X + headerRect.Width, headerRect.Y + headerRect.Height);
            headerPath.AddLine(headerRect.X + headerRect.Width, headerRect.Y + headerRect.Height, headerRect.X, headerRect.Y + headerRect.Height);
            headerPath.AddLine(headerRect.X, headerRect.Y + headerRect.Height, headerRect.X, headerRect.Y + radius);
            headerPath.CloseFigure();

            pnlHeader.Region = new Region(headerPath);
        }

        private void ForgotPassWordForm_Paint(object sender, PaintEventArgs e)
        {
            // Add subtle shadow effect
            var shadowBrush = new SolidBrush(Color.FromArgb(20, 0, 0, 0));
            e.Graphics.FillRectangle(shadowBrush, new Rectangle(45, 45, pnlMain.Width - 80, pnlMain.Height - 80));
        }

        private void btnSubmitEmail_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text?.Trim();

            // Validate input
            if (string.IsNullOrWhiteSpace(email))
            {
                ShowValidationError("Vui lòng nhập địa chỉ email!", txtEmail);
                return;
            }

            // Validate email format
            if (!IsValidEmail(email))
            {
                ShowValidationError("Định dạng email không hợp lệ!", txtEmail);
                return;
            }

            // Disable button and show loading state
            SetButtonLoadingState(true);

            try
            {
                string errorMessage;
                if (_loginController.GeneratePasswordResetTokenByEmail(email, out errorMessage))
                {
                    ShowSuccessMessage(
                        "Mã token đã được gửi đến email của bạn.\n\n" +
                        "Vui lòng kiểm tra email (kể cả thư mục spam) để lấy mã token và đặt lại mật khẩu.\n\n" +
                        "⏰ Mã token có hiệu lực trong 15 phút.",
                        "Gửi thành công!");

                    // Get user info and open reset password form
                    var userInfo = _loginController.GetUserInfoByEmail(email);
                    if (userInfo != null)
                    {
                        var resetForm = new ResetPasswordForm(_loginController, email, userInfo.Username);
                        this.Hide();
                        var result = resetForm.ShowDialog();

                        if (result == DialogResult.OK)
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            this.Show();
                        }
                    }
                    else
                    {
                        ShowErrorMessage("Có lỗi xảy ra khi lấy thông tin người dùng.", "Lỗi hệ thống");
                    }
                }
                else
                {
                    ShowErrorMessage(errorMessage, "Không thể gửi email");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Có lỗi không mong muốn xảy ra:\n{ex.Message}", "Lỗi hệ thống");
            }
            finally
            {
                SetButtonLoadingState(false);
            }
        }

        private void SetButtonLoadingState(bool isLoading)
        {
            btnSubmitEmail.Enabled = !isLoading;
            btnSubmitEmail.Text = isLoading ? "⏳ Đang xử lý..." : "📧 Gửi mã token";
            btnSubmitEmail.BackColor = isLoading ?
                Color.FromArgb(156, 163, 175) :
                Color.FromArgb(59, 130, 246);
        }

        private void ShowValidationError(string message, Control focusControl)
        {
            var result = MessageBox.Show(message, "⚠️ Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            focusControl?.Focus();
        }

        private void ShowErrorMessage(string message, string title)
        {
            MessageBox.Show(message, $"❌ {title}",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowSuccessMessage(string message, string title)
        {
            MessageBox.Show(message, $"✅ {title}",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnSubmitEmail_Click(sender, e);
            }
        }

        private void ForgotPassWordForm_Load(object sender, EventArgs e)
        {
            txtEmail?.Focus();

            // Add fade-in animation
            this.Opacity = 0;
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 20;
            timer.Tick += (s, args) =>
            {
                if (this.Opacity < 1)
                    this.Opacity += 0.20;
                else
                    timer.Stop();
            };
            timer.Start();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Button hover effects
        private void btnSubmitEmail_MouseEnter(object sender, EventArgs e)
        {
            if (btnSubmitEmail.Enabled)
                btnSubmitEmail.BackColor = Color.FromArgb(37, 99, 235);
        }

        private void btnSubmitEmail_MouseLeave(object sender, EventArgs e)
        {
            if (btnSubmitEmail.Enabled)
                btnSubmitEmail.BackColor = Color.FromArgb(59, 130, 246);
        }

        private void btnCancel_MouseEnter(object sender, EventArgs e)
        {
            btnCancel.BackColor = Color.FromArgb(249, 250, 251);
        }

        private void btnCancel_MouseLeave(object sender, EventArgs e)
        {
            btnCancel.BackColor = Color.White;
        }

        // Custom icon painting
        private void pictureBoxIcon_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Draw lock icon
            var iconColor = Color.FromArgb(59, 130, 246);
            var pen = new Pen(iconColor, 2);
            var brush = new SolidBrush(iconColor);

            // Lock body
            var lockRect = new Rectangle(15, 25, 20, 15);
            g.FillRectangle(brush, lockRect);

            // Lock shackle
            var shackleRect = new Rectangle(20, 15, 10, 12);
            g.DrawArc(pen, shackleRect, 0, 180);

            // Keyhole
            var keyhole = new Rectangle(23, 30, 4, 4);
            g.FillEllipse(new SolidBrush(Color.White), keyhole);

            pen.Dispose();
            brush.Dispose();
        }

        // Override form closing to add fade-out animation
        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(value);
            if (value && this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
        }
    }
}