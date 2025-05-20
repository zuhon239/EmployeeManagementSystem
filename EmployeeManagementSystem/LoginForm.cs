using EmployeeManagementSystem.Controller;
using EmployeeManagementSystem.Model;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagementSystem
{
    public partial class LoginForm : Form
    {
        private readonly LoginController _loginController;

        public LoginForm()
        {
            InitializeComponent();
            _loginController = new LoginController(new EmployeeManagementContext());
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
                lblMessage.Text = errorMessage;
            }
        }
        bool isPasswordShown = false;       
        private void btnShowPassWord_Click(object sender, EventArgs e)
        {
            isPasswordShown = !isPasswordShown;

            if (isPasswordShown)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }
    }
}
