namespace EmployeeManagementSystem.FormManager
{
    partial class DepartmentManagerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DepartmentManagerForm));
            pnlMenu = new Panel();
            btnBackToMenu = new Button();
            btnSalary = new Button();
            btnLeaveRequest = new Button();
            btnAttendance = new Button();
            btnManager = new Button();
            btnDashboard = new Button();
            pnlPersonalLogo = new Panel();
            pictureBox2 = new PictureBox();
            lblManager = new Label();
            lblManagerName = new Label();
            panel1 = new Panel();
            lblDepartment = new Label();
            pnlDesktopPane = new Panel();
            pictureBox1 = new PictureBox();
            pnlMenu.SuspendLayout();
            pnlPersonalLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel1.SuspendLayout();
            pnlDesktopPane.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pnlMenu
            // 
            pnlMenu.BackColor = Color.FromArgb(51, 51, 76);
            pnlMenu.Controls.Add(btnBackToMenu);
            pnlMenu.Controls.Add(btnSalary);
            pnlMenu.Controls.Add(btnLeaveRequest);
            pnlMenu.Controls.Add(btnAttendance);
            pnlMenu.Controls.Add(btnManager);
            pnlMenu.Controls.Add(btnDashboard);
            pnlMenu.Controls.Add(pnlPersonalLogo);
            pnlMenu.Dock = DockStyle.Left;
            pnlMenu.Location = new Point(0, 0);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(216, 564);
            pnlMenu.TabIndex = 0;
            // 
            // btnBackToMenu
            // 
            btnBackToMenu.Dock = DockStyle.Top;
            btnBackToMenu.FlatAppearance.BorderSize = 0;
            btnBackToMenu.FlatStyle = FlatStyle.Flat;
            btnBackToMenu.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnBackToMenu.ForeColor = Color.White;
            btnBackToMenu.ImageAlign = ContentAlignment.MiddleLeft;
            btnBackToMenu.Location = new Point(0, 401);
            btnBackToMenu.Name = "btnBackToMenu";
            btnBackToMenu.Size = new Size(216, 65);
            btnBackToMenu.TabIndex = 6;
            btnBackToMenu.Text = "Back";
            btnBackToMenu.UseVisualStyleBackColor = true;
            btnBackToMenu.Click += btnBackToMenu_Click;
            // 
            // btnSalary
            // 
            btnSalary.Dock = DockStyle.Top;
            btnSalary.FlatAppearance.BorderSize = 0;
            btnSalary.FlatStyle = FlatStyle.Flat;
            btnSalary.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSalary.ForeColor = Color.White;
            btnSalary.ImageAlign = ContentAlignment.MiddleLeft;
            btnSalary.Location = new Point(0, 336);
            btnSalary.Name = "btnSalary";
            btnSalary.Size = new Size(216, 65);
            btnSalary.TabIndex = 5;
            btnSalary.Text = "Salary";
            btnSalary.UseVisualStyleBackColor = true;
            btnSalary.Click += btnSalary_Click;
            // 
            // btnLeaveRequest
            // 
            btnLeaveRequest.Dock = DockStyle.Top;
            btnLeaveRequest.FlatAppearance.BorderSize = 0;
            btnLeaveRequest.FlatStyle = FlatStyle.Flat;
            btnLeaveRequest.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLeaveRequest.ForeColor = Color.White;
            btnLeaveRequest.ImageAlign = ContentAlignment.MiddleLeft;
            btnLeaveRequest.Location = new Point(0, 271);
            btnLeaveRequest.Name = "btnLeaveRequest";
            btnLeaveRequest.Size = new Size(216, 65);
            btnLeaveRequest.TabIndex = 4;
            btnLeaveRequest.Text = "LeaveRequest";
            btnLeaveRequest.UseVisualStyleBackColor = true;
            btnLeaveRequest.Click += btnLeaveRequest_Click;
            // 
            // btnAttendance
            // 
            btnAttendance.Dock = DockStyle.Top;
            btnAttendance.FlatAppearance.BorderSize = 0;
            btnAttendance.FlatStyle = FlatStyle.Flat;
            btnAttendance.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAttendance.ForeColor = Color.White;
            btnAttendance.ImageAlign = ContentAlignment.MiddleLeft;
            btnAttendance.Location = new Point(0, 206);
            btnAttendance.Name = "btnAttendance";
            btnAttendance.Size = new Size(216, 65);
            btnAttendance.TabIndex = 3;
            btnAttendance.Text = "Attendance";
            btnAttendance.UseVisualStyleBackColor = true;
            btnAttendance.Click += btnAttendance_Click;
            // 
            // btnManager
            // 
            btnManager.Dock = DockStyle.Top;
            btnManager.FlatAppearance.BorderSize = 0;
            btnManager.FlatStyle = FlatStyle.Flat;
            btnManager.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnManager.ForeColor = Color.White;
            btnManager.ImageAlign = ContentAlignment.MiddleLeft;
            btnManager.Location = new Point(0, 141);
            btnManager.Name = "btnManager";
            btnManager.Size = new Size(216, 65);
            btnManager.TabIndex = 2;
            btnManager.Text = "Employee";
            btnManager.UseVisualStyleBackColor = true;
            btnManager.Click += btnManager_Click;
            // 
            // btnDashboard
            // 
            btnDashboard.Dock = DockStyle.Top;
            btnDashboard.FlatAppearance.BorderSize = 0;
            btnDashboard.FlatStyle = FlatStyle.Flat;
            btnDashboard.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDashboard.ForeColor = Color.White;
            btnDashboard.ImageAlign = ContentAlignment.MiddleLeft;
            btnDashboard.Location = new Point(0, 76);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Size = new Size(216, 65);
            btnDashboard.TabIndex = 1;
            btnDashboard.Text = "Dashboard";
            btnDashboard.UseVisualStyleBackColor = true;
            btnDashboard.Click += btnDashboard_Click;
            // 
            // pnlPersonalLogo
            // 
            pnlPersonalLogo.BackColor = Color.FromArgb(39, 39, 58);
            pnlPersonalLogo.Controls.Add(pictureBox2);
            pnlPersonalLogo.Controls.Add(lblManager);
            pnlPersonalLogo.Controls.Add(lblManagerName);
            pnlPersonalLogo.Dock = DockStyle.Top;
            pnlPersonalLogo.Location = new Point(0, 0);
            pnlPersonalLogo.Name = "pnlPersonalLogo";
            pnlPersonalLogo.Size = new Size(216, 76);
            pnlPersonalLogo.TabIndex = 0;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(3, 9);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(46, 56);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // lblManager
            // 
            lblManager.AutoSize = true;
            lblManager.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblManager.ForeColor = Color.White;
            lblManager.Location = new Point(53, 15);
            lblManager.Name = "lblManager";
            lblManager.Size = new Size(49, 13);
            lblManager.TabIndex = 1;
            lblManager.Text = "Manager";
            // 
            // lblManagerName
            // 
            lblManagerName.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblManagerName.ForeColor = Color.White;
            lblManagerName.Location = new Point(52, 30);
            lblManagerName.Name = "lblManagerName";
            lblManagerName.Size = new Size(164, 39);
            lblManagerName.TabIndex = 0;
            lblManagerName.Text = "ManagerNamegsdgdsgdsgdsdg";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(53, 6, 153);
            panel1.Controls.Add(lblDepartment);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(216, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(906, 76);
            panel1.TabIndex = 1;
            // 
            // lblDepartment
            // 
            lblDepartment.Anchor = AnchorStyles.None;
            lblDepartment.Font = new Font("Bahnschrift", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDepartment.ForeColor = Color.White;
            lblDepartment.Location = new Point(66, 1);
            lblDepartment.Name = "lblDepartment";
            lblDepartment.Size = new Size(774, 75);
            lblDepartment.TabIndex = 0;
            lblDepartment.Text = "DepartmentName";
            lblDepartment.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlDesktopPane
            // 
            pnlDesktopPane.BackColor = Color.White;
            pnlDesktopPane.Controls.Add(pictureBox1);
            pnlDesktopPane.Dock = DockStyle.Fill;
            pnlDesktopPane.Location = new Point(216, 76);
            pnlDesktopPane.Name = "pnlDesktopPane";
            pnlDesktopPane.Size = new Size(906, 488);
            pnlDesktopPane.TabIndex = 2;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.None;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(225, 18);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(460, 426);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // DepartmentManagerForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1122, 564);
            Controls.Add(pnlDesktopPane);
            Controls.Add(panel1);
            Controls.Add(pnlMenu);
            MaximizeBox = false;
            Name = "DepartmentManagerForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DepartmentManagerForm";
            pnlMenu.ResumeLayout(false);
            pnlPersonalLogo.ResumeLayout(false);
            pnlPersonalLogo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel1.ResumeLayout(false);
            pnlDesktopPane.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMenu;
        private Panel pnlPersonalLogo;
        private Button btnDashboard;
        private Button btnSalary;
        private Button btnLeaveRequest;
        private Button btnAttendance;
        private Button btnManager;
        private Button btnBackToMenu;
        private Label lblManager;
        private Label lblManagerName;
        private Panel panel1;
        private Label lblDepartment;
        private Panel pnlDesktopPane;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
    }
}