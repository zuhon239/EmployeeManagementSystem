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
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            lblUsername = new Label();
            lblpassword = new Label();
            btnLogin = new Button();
            lblMessage = new Label();
            btnShowPassWord = new Button();
            SuspendLayout();
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(301, 96);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(172, 26);
            txtUsername.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(301, 152);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(172, 26);
            txtPassword.TabIndex = 1;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(224, 99);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(71, 19);
            lblUsername.TabIndex = 2;
            lblUsername.Text = "Username";
            // 
            // lblpassword
            // 
            lblpassword.AutoSize = true;
            lblpassword.Location = new Point(228, 155);
            lblpassword.Name = "lblpassword";
            lblpassword.Size = new Size(67, 19);
            lblpassword.TabIndex = 3;
            lblpassword.Text = "Password";
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(333, 217);
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
            btnShowPassWord.Location = new Point(490, 152);
            btnShowPassWord.Name = "btnShowPassWord";
            btnShowPassWord.Size = new Size(53, 26);
            btnShowPassWord.TabIndex = 7;
            btnShowPassWord.Text = "Show";
            btnShowPassWord.UseVisualStyleBackColor = true;
            btnShowPassWord.Click += btnShowPassWord_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnShowPassWord);
            Controls.Add(lblMessage);
            Controls.Add(btnLogin);
            Controls.Add(lblpassword);
            Controls.Add(lblUsername);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Name = "LoginForm";
            Text = "Login";
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
    }
}
