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
            //tắt show
            if (isPasswordShown)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            //show
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
