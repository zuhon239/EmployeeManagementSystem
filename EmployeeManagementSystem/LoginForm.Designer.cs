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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            txtUsername = new TextBox();
            btnLogin = new Button();
            lblMessage = new Label();
            btnShowPassWord = new Button();
            errorProvider1 = new ErrorProvider(components);
            lblLoginError = new Label();
            lblWelcomeback = new Label();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            panel1 = new Panel();
            txtPassword = new TextBox();
            panel2 = new Panel();
            btnExit = new Button();
            btnOut = new Button();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // txtUsername
            // 
            txtUsername.BorderStyle = BorderStyle.None;
            txtUsername.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtUsername.Location = new Point(92, 165);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(214, 17);
            txtUsername.TabIndex = 0;
            txtUsername.TextChanged += txtUsername_TextChanged;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(0, 117, 214);
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Bahnschrift", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(195, 327);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(112, 42);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "LOG IN";
            btnLogin.UseVisualStyleBackColor = false;
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
            btnShowPassWord.FlatAppearance.BorderColor = Color.FromArgb(0, 117, 214);
            btnShowPassWord.FlatAppearance.BorderSize = 0;
            btnShowPassWord.FlatAppearance.MouseDownBackColor = Color.Gainsboro;
            btnShowPassWord.FlatAppearance.MouseOverBackColor = Color.Gainsboro;
            btnShowPassWord.FlatStyle = FlatStyle.Flat;
            btnShowPassWord.Font = new Font("Bahnschrift", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnShowPassWord.ForeColor = Color.FromArgb(0, 117, 214);
            btnShowPassWord.Location = new Point(247, 268);
            btnShowPassWord.Name = "btnShowPassWord";
            btnShowPassWord.Size = new Size(59, 29);
            btnShowPassWord.TabIndex = 3;
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
            lblLoginError.Location = new Point(39, 305);
            lblLoginError.Name = "lblLoginError";
            lblLoginError.Size = new Size(0, 19);
            lblLoginError.TabIndex = 8;
            // 
            // lblWelcomeback
            // 
            lblWelcomeback.AutoSize = true;
            lblWelcomeback.Font = new Font("Bauhaus 93", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblWelcomeback.ForeColor = Color.FromArgb(0, 117, 214);
            lblWelcomeback.Location = new Point(49, 76);
            lblWelcomeback.Name = "lblWelcomeback";
            lblWelcomeback.Size = new Size(258, 36);
            lblWelcomeback.TabIndex = 9;
            lblWelcomeback.Text = "WELCOME BACK";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(39, 151);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(47, 45);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 10;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(49, 214);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(28, 38);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 11;
            pictureBox2.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Black;
            panel1.ForeColor = Color.Black;
            panel1.Location = new Point(39, 195);
            panel1.Name = "panel1";
            panel1.Size = new Size(267, 1);
            panel1.TabIndex = 12;
            // 
            // txtPassword
            // 
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtPassword.Location = new Point(92, 229);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(214, 17);
            txtPassword.TabIndex = 1;
            txtPassword.TextChanged += txtPassword_TextChanged;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Black;
            panel2.ForeColor = Color.Black;
            panel2.Location = new Point(40, 254);
            panel2.Name = "panel2";
            panel2.Size = new Size(267, 1);
            panel2.TabIndex = 13;
            // 
            // btnExit
            // 
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExit.ForeColor = Color.FromArgb(0, 117, 214);
            btnExit.Location = new Point(289, -1);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(56, 33);
            btnExit.TabIndex = 14;
            btnExit.Text = "X\r\n";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // btnOut
            // 
            btnOut.BackColor = Color.White;
            btnOut.FlatAppearance.BorderColor = Color.FromArgb(0, 117, 214);
            btnOut.FlatStyle = FlatStyle.Flat;
            btnOut.Font = new Font("Bahnschrift", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnOut.ForeColor = Color.FromArgb(0, 117, 214);
            btnOut.Location = new Point(39, 327);
            btnOut.Name = "btnOut";
            btnOut.Size = new Size(112, 42);
            btnOut.TabIndex = 15;
            btnOut.Text = "CANCEL";
            btnOut.UseVisualStyleBackColor = false;
            btnOut.Click += btnOut_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.White;
            ClientSize = new Size(343, 447);
            Controls.Add(btnOut);
            Controls.Add(btnExit);
            Controls.Add(panel2);
            Controls.Add(txtPassword);
            Controls.Add(panel1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(lblWelcomeback);
            Controls.Add(lblLoginError);
            Controls.Add(btnShowPassWord);
            Controls.Add(lblMessage);
            Controls.Add(btnLogin);
            Controls.Add(txtUsername);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            Load += LoginForm_Load;
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtUsername;
        private Button btnLogin;
        private Label lblMessage;
        private Button btnShowPassWord;
        private ErrorProvider errorProvider1;
        private Label lblLoginError;
        private Label lblWelcomeback;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Panel panel1;
        private TextBox txtPassword;
        private Panel panel2;
        private Button btnExit;
        private Button btnOut;
    }
}
