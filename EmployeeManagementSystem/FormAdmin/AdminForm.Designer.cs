namespace EmployeeManagementSystem
{
    partial class AdminForm
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
            pnlMenu = new Panel();
            panel1 = new Panel();
            label1 = new Label();
            lblAdminName = new Label();
            btnBack = new Button();
            btnSalary = new Button();
            btnLeaveRequest = new Button();
            btnAttendance = new Button();
            btnEmployee = new Button();
            btnDashBoard = new Button();
            sqliteCommand1 = new Microsoft.Data.Sqlite.SqliteCommand();
            pnlDesktopPane = new Panel();
            pnlMenu.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMenu
            // 
            pnlMenu.BackColor = Color.FromArgb(73, 40, 243);
            pnlMenu.Controls.Add(panel1);
            pnlMenu.Controls.Add(btnBack);
            pnlMenu.Controls.Add(btnSalary);
            pnlMenu.Controls.Add(btnLeaveRequest);
            pnlMenu.Controls.Add(btnAttendance);
            pnlMenu.Controls.Add(btnEmployee);
            pnlMenu.Controls.Add(btnDashBoard);
            pnlMenu.Dock = DockStyle.Left;
            pnlMenu.Location = new Point(0, 0);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(200, 549);
            pnlMenu.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(lblAdminName);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(200, 75);
            panel1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(49, 19);
            label1.TabIndex = 1;
            label1.Text = "Admin";
            // 
            // lblAdminName
            // 
            lblAdminName.AutoSize = true;
            lblAdminName.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAdminName.ForeColor = Color.White;
            lblAdminName.Location = new Point(12, 28);
            lblAdminName.Name = "lblAdminName";
            lblAdminName.Size = new Size(57, 21);
            lblAdminName.TabIndex = 0;
            lblAdminName.Text = "label1";
            // 
            // btnBack
            // 
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(0, 488);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(200, 52);
            btnBack.TabIndex = 7;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // btnSalary
            // 
            btnSalary.FlatAppearance.BorderSize = 0;
            btnSalary.FlatStyle = FlatStyle.Flat;
            btnSalary.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSalary.ForeColor = Color.White;
            btnSalary.Location = new Point(0, 405);
            btnSalary.Name = "btnSalary";
            btnSalary.Size = new Size(200, 77);
            btnSalary.TabIndex = 6;
            btnSalary.Text = "Salary";
            btnSalary.UseVisualStyleBackColor = true;
            btnSalary.Click += btnSalary_Click;
            // 
            // btnLeaveRequest
            // 
            btnLeaveRequest.FlatAppearance.BorderSize = 0;
            btnLeaveRequest.FlatStyle = FlatStyle.Flat;
            btnLeaveRequest.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLeaveRequest.ForeColor = Color.White;
            btnLeaveRequest.Location = new Point(0, 322);
            btnLeaveRequest.Name = "btnLeaveRequest";
            btnLeaveRequest.Size = new Size(200, 77);
            btnLeaveRequest.TabIndex = 5;
            btnLeaveRequest.Text = "LeaveRequest";
            btnLeaveRequest.UseVisualStyleBackColor = true;
            btnLeaveRequest.Click += btnLeaveRequest_Click;
            // 
            // btnAttendance
            // 
            btnAttendance.FlatAppearance.BorderSize = 0;
            btnAttendance.FlatStyle = FlatStyle.Flat;
            btnAttendance.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAttendance.ForeColor = Color.White;
            btnAttendance.Location = new Point(0, 239);
            btnAttendance.Name = "btnAttendance";
            btnAttendance.Size = new Size(200, 77);
            btnAttendance.TabIndex = 4;
            btnAttendance.Text = "Attendance";
            btnAttendance.UseVisualStyleBackColor = true;
            btnAttendance.Click += btnAttendance_Click;
            // 
            // btnEmployee
            // 
            btnEmployee.FlatAppearance.BorderSize = 0;
            btnEmployee.FlatStyle = FlatStyle.Flat;
            btnEmployee.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnEmployee.ForeColor = Color.White;
            btnEmployee.Location = new Point(0, 156);
            btnEmployee.Name = "btnEmployee";
            btnEmployee.Size = new Size(200, 77);
            btnEmployee.TabIndex = 3;
            btnEmployee.Text = "Employee";
            btnEmployee.UseVisualStyleBackColor = true;
            btnEmployee.Click += btnEmployee_Click;
            // 
            // btnDashBoard
            // 
            btnDashBoard.FlatAppearance.BorderSize = 0;
            btnDashBoard.FlatStyle = FlatStyle.Flat;
            btnDashBoard.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDashBoard.ForeColor = Color.White;
            btnDashBoard.Location = new Point(0, 73);
            btnDashBoard.Name = "btnDashBoard";
            btnDashBoard.Size = new Size(200, 77);
            btnDashBoard.TabIndex = 2;
            btnDashBoard.Text = "Dashboard";
            btnDashBoard.UseVisualStyleBackColor = true;
            btnDashBoard.Click += btnDashBoard_Click;
            // 
            // sqliteCommand1
            // 
            sqliteCommand1.CommandTimeout = 30;
            sqliteCommand1.Connection = null;
            sqliteCommand1.Transaction = null;
            sqliteCommand1.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // pnlDesktopPane
            // 
            pnlDesktopPane.Dock = DockStyle.Fill;
            pnlDesktopPane.Location = new Point(200, 0);
            pnlDesktopPane.Name = "pnlDesktopPane";
            pnlDesktopPane.Size = new Size(907, 549);
            pnlDesktopPane.TabIndex = 2;
            // 
            // AdminForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1107, 549);
            Controls.Add(pnlDesktopPane);
            Controls.Add(pnlMenu);
            Name = "AdminForm";
            Text = "AdminForm";
            pnlMenu.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMenu;
        private Panel panel1;
        private Button btnDashBoard;
        private Button btnEmployee;
        private Button btnLeaveRequest;
        private Button btnAttendance;
        private Button btnBack;
        private Button btnSalary;
        private Microsoft.Data.Sqlite.SqliteCommand sqliteCommand1;
        private Label lblAdminName;
        private Panel pnlDesktopPane;
        private Label label1;
    }
}