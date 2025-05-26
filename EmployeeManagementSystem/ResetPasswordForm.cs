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
    public partial class ResetPasswordForm : Form
    {
        private readonly LoginController _loginController;
        private readonly string _email;
        private readonly string _username;
        private int _attemptCount = 0;
        private const int MAX_ATTEMPTS = 3;
        public ResetPasswordForm(LoginController loginController, string email, string username)
        {
            _loginController = loginController ?? throw new ArgumentNullException(nameof(loginController));
            _email = email ?? throw new ArgumentNullException(nameof(email));
            _username = username ?? throw new ArgumentNullException(nameof(username));
            InitializeComponent();
        }
        private void BtnResetPassword_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            string token = txtToken.Text.Trim().ToUpper();
            string newPassword = txtNewPassword.Text;

            // Disable button to prevent multiple clicks
            btnResetPassword.Enabled = false;
            btnResetPassword.Text = "Đang xử lý...";

            try
            {
                string errorMessage;
                if (_loginController.ResetPassword(_email, token, newPassword, out errorMessage))
                {
                    MessageBox.Show(
                        "Đặt lại mật khẩu thành công!\n" +
                        "Bạn có thể đăng nhập bằng mật khẩu mới.",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    _attemptCount++;
                    string attemptMessage = "";

                    if (_attemptCount >= MAX_ATTEMPTS)
                    {
                        attemptMessage = "\nBạn đã nhập sai quá nhiều lần. Vui lòng yêu cầu mã token mới.";
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                    }
                    else
                    {
                        attemptMessage = $"\nSố lần thử còn lại: {MAX_ATTEMPTS - _attemptCount}";
                    }

                    MessageBox.Show(
                        errorMessage + attemptMessage,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    // Clear token field for retry
                    txtToken.Clear();
                    txtToken.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Có lỗi không mong muốn xảy ra: {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnResetPassword.Enabled = true;
                btnResetPassword.Text = "Đặt lại mật khẩu";
            }
        }

        private bool ValidateInput()
        {
            // Validate token
            string token = txtToken.Text.Trim();
            if (string.IsNullOrEmpty(token))
            {
                MessageBox.Show("Vui lòng nhập mã token!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtToken.Focus();
                return false;
            }

            if (token.Length != 8)
            {
                MessageBox.Show("Mã token phải có đúng 8 ký tự!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtToken.Focus();
                return false;
            }

            // Validate new password
            string newPassword = txtNewPassword.Text;
            if (string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                return false;
            }

            if (newPassword.Length < 6)
            {
                MessageBox.Show("Mật khẩu mới phải có ít nhất 6 ký tự!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                return false;
            }

            // Validate confirm password
            string confirmPassword = txtConfirmPassword.Text;
            if (string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Vui lòng xác nhận mật khẩu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return false;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                txtConfirmPassword.SelectAll();
                return false;
            }

            return true;
        }

        private void ChkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            bool showPassword = chkShowPassword.Checked;
            txtNewPassword.UseSystemPasswordChar = !showPassword;
            txtConfirmPassword.UseSystemPasswordChar = !showPassword;
        }

        private void TxtToken_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Only allow alphanumeric characters and control keys
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // Handle Enter key
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtNewPassword.Focus();
            }
        }

        private void TxtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Handle Enter key
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                if (sender == txtNewPassword)
                {
                    txtConfirmPassword.Focus();
                }
                else if (sender == txtConfirmPassword)
                {
                    BtnResetPassword_Click(sender, e);
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            txtToken.Focus();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.None)
            {
                this.DialogResult = DialogResult.Cancel;
            }
            base.OnFormClosing(e);
        }
    }
}
