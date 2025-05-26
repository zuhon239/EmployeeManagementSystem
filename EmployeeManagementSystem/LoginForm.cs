using EmployeeManagementSystem.Controller;
using EmployeeManagementSystem.Model;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagementSystem
{
    public partial class LoginForm : Form
    {
        private readonly LoginController _loginController;

        public LoginForm(LoginController loginController)
        {
            InitializeComponent();
            _loginController = loginController ?? throw new ArgumentNullException(nameof(loginController));
            txtPassword.UseSystemPasswordChar = true;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {

            string errorMessage;
            Form nextForm;

            if (_loginController.Login(txtUsername.Text, txtPassword.Text, out errorMessage, out nextForm))
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo");
                this.Hide();
                nextForm.ShowDialog();
                this.Close();
            }
            else
            {
                lblLoginError.Text = errorMessage;
            }
        }
        bool isPasswordShown = false;
        private void btnShowPassWord_Click(object sender, EventArgs e)
        {
            isPasswordShown = !isPasswordShown;
            txtPassword.UseSystemPasswordChar = !isPasswordShown;

            // (Tùy chọn) Thay đổi icon/text nút để phản hồi người dùng
            btnShowPassWord.Text = isPasswordShown ? "Hide" : "Show";
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnForgotPassWord_Click(object sender, EventArgs e)
        {
            var forgotpassword = new ForgotPassWordForm(_loginController);
            forgotpassword.ShowDialog();
        }
    }
}
