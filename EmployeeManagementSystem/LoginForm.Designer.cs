namespace EmployeeManagementSystem
{
    partial class LoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            lblUsername = new Label();
            lblpassword = new Label();
            btnLogin = new Button();
            lblMessage = new Label();
            btnShowPassWord = new Button();
            errorProvider1 = new ErrorProvider(components);
            lblLoginError = new Label();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(131, 55);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(172, 26);
            txtUsername.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(131, 111);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(172, 26);
            txtPassword.TabIndex = 1;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(54, 58);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(71, 19);
            lblUsername.TabIndex = 2;
            lblUsername.Text = "Username";
            // 
            // lblpassword
            // 
            lblpassword.AutoSize = true;
            lblpassword.Location = new Point(54, 114);
            lblpassword.Name = "lblpassword";
            lblpassword.Size = new Size(67, 19);
            lblpassword.TabIndex = 3;
            lblpassword.Text = "Password";
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(163, 164);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(75, 32);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // lblMessage
            // 
            lblMessage.AutoSize = true;
            lblMessage.Location = new Point(543, 422);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(0, 19);
            lblMessage.TabIndex = 5;
            // 
            // btnShowPassWord
            // 
            btnShowPassWord.Location = new Point(320, 111);
            btnShowPassWord.Name = "btnShowPassWord";
            btnShowPassWord.Size = new Size(53, 26);
            btnShowPassWord.TabIndex = 7;
            btnShowPassWord.Text = "Show";
            btnShowPassWord.UseVisualStyleBackColor = true;
            btnShowPassWord.Click += btnShowPassWord_Click;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // lblLoginError
            // 
            lblLoginError.AutoSize = true;
            lblLoginError.ForeColor = Color.OrangeRed;
            lblLoginError.Location = new Point(76, 217);
            lblLoginError.Name = "lblLoginError";
            lblLoginError.Size = new Size(0, 19);
            lblLoginError.TabIndex = 8;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(438, 257);
            Controls.Add(lblLoginError);
            Controls.Add(btnShowPassWord);
            Controls.Add(lblMessage);
            Controls.Add(btnLogin);
            Controls.Add(lblpassword);
            Controls.Add(lblUsername);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            MaximizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            Load += LoginForm_Load;
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtUsername;
        private TextBox txtPassword;
        private Label lblUsername;
        private Label lblpassword;
        private Button btnLogin;
        private Label lblMessage;
        private Button btnShowPassWord;
        private ErrorProvider errorProvider1;
        private Label lblLoginError;
    }
}
