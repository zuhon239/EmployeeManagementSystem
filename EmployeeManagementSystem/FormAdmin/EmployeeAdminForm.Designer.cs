namespace EmployeeManagementSystem.FormAdmin
{
    partial class EmployeeAdminForm
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            dataGridView1 = new DataGridView();
            pnlData = new Panel();
            comboBox1 = new ComboBox();
            lblHeader = new Label();
            txtEmail = new TextBox();
            lblEmail = new Label();
            txtPosition = new TextBox();
            lblPosition = new Label();
            btnSaThai = new Button();
            button1 = new Button();
            btnThem = new Button();
            dateTimePicker1 = new DateTimePicker();
            cbGender = new ComboBox();
            lblUserName = new Label();
            txtUserName = new TextBox();
            lblPhone = new Label();
            txtPhone = new TextBox();
            lblDateOfBirth = new Label();
            lblGender = new Label();
            lblFullName = new Label();
            txtName = new TextBox();
            lblID = new Label();
            txtID = new TextBox();
            clUserID = new DataGridViewTextBoxColumn();
            clName = new DataGridViewTextBoxColumn();
            clGender = new DataGridViewTextBoxColumn();
            clDateofBirth = new DataGridViewTextBoxColumn();
            clPhone = new DataGridViewTextBoxColumn();
            clPosition = new DataGridViewTextBoxColumn();
            clHireDate = new DataGridViewTextBoxColumn();
            clUpdateRole = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            pnlData.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.BackgroundColor = Color.White;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { clUserID, clName, clGender, clDateofBirth, clPhone, clPosition, clHireDate, clUpdateRole });
            dataGridView1.GridColor = SystemColors.InactiveCaptionText;
            dataGridView1.Location = new Point(13, 38);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Size = new Size(839, 271);
            dataGridView1.TabIndex = 0;
            // 
            // pnlData
            // 
            pnlData.BackColor = Color.White;
            pnlData.BorderStyle = BorderStyle.Fixed3D;
            pnlData.Controls.Add(comboBox1);
            pnlData.Controls.Add(lblHeader);
            pnlData.Controls.Add(dataGridView1);
            pnlData.Location = new Point(12, 12);
            pnlData.Name = "pnlData";
            pnlData.Size = new Size(867, 325);
            pnlData.TabIndex = 1;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Manager", "Phòng phát triển phần mềm", "Phòng kiểm thử", "Phòng thiết kế giao diện", "Phòng DevOps", "Phòng dữ liệu", "Phòng hồ sơ kỹ thuật" });
            comboBox1.Location = new Point(590, 4);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(262, 27);
            comboBox1.TabIndex = 2;
            // 
            // lblHeader
            // 
            lblHeader.AutoSize = true;
            lblHeader.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblHeader.Location = new Point(14, 6);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(156, 25);
            lblHeader.TabIndex = 1;
            lblHeader.Text = "Employee's Data";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(576, 417);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(162, 26);
            txtEmail.TabIndex = 41;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(529, 421);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(41, 19);
            lblEmail.TabIndex = 40;
            lblEmail.Text = "Email";
            // 
            // txtPosition
            // 
            txtPosition.Location = new Point(576, 368);
            txtPosition.Name = "txtPosition";
            txtPosition.Size = new Size(162, 26);
            txtPosition.TabIndex = 39;
            // 
            // lblPosition
            // 
            lblPosition.AutoSize = true;
            lblPosition.Location = new Point(516, 371);
            lblPosition.Name = "lblPosition";
            lblPosition.Size = new Size(57, 19);
            lblPosition.TabIndex = 38;
            lblPosition.Text = "Position";
            // 
            // btnSaThai
            // 
            btnSaThai.Location = new Point(758, 470);
            btnSaThai.Name = "btnSaThai";
            btnSaThai.Size = new Size(121, 37);
            btnSaThai.TabIndex = 37;
            btnSaThai.Text = "Sa thải";
            btnSaThai.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(758, 414);
            button1.Name = "button1";
            button1.Size = new Size(121, 37);
            button1.TabIndex = 36;
            button1.Text = "Sửa";
            button1.UseVisualStyleBackColor = true;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(758, 359);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(121, 37);
            btnThem.TabIndex = 35;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Checked = false;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(368, 368);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(130, 26);
            dateTimePicker1.TabIndex = 34;
            // 
            // cbGender
            // 
            cbGender.FormattingEnabled = true;
            cbGender.Location = new Point(368, 417);
            cbGender.Name = "cbGender";
            cbGender.Size = new Size(130, 27);
            cbGender.TabIndex = 33;
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Location = new Point(7, 479);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(73, 19);
            lblUserName.TabIndex = 32;
            lblUserName.Text = "UserName";
            // 
            // txtUserName
            // 
            txtUserName.Location = new Point(86, 476);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(180, 26);
            txtUserName.TabIndex = 31;
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(314, 479);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(48, 19);
            lblPhone.TabIndex = 30;
            lblPhone.Text = "Phone";
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(368, 476);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(130, 26);
            txtPhone.TabIndex = 29;
            // 
            // lblDateOfBirth
            // 
            lblDateOfBirth.AutoSize = true;
            lblDateOfBirth.Location = new Point(275, 371);
            lblDateOfBirth.Name = "lblDateOfBirth";
            lblDateOfBirth.Size = new Size(87, 19);
            lblDateOfBirth.TabIndex = 28;
            lblDateOfBirth.Text = "Date of birth";
            // 
            // lblGender
            // 
            lblGender.AutoSize = true;
            lblGender.Location = new Point(308, 421);
            lblGender.Name = "lblGender";
            lblGender.Size = new Size(54, 19);
            lblGender.TabIndex = 27;
            lblGender.Text = "Gender";
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Location = new Point(35, 420);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(45, 19);
            lblFullName.TabIndex = 26;
            lblFullName.Text = "Name";
            // 
            // txtName
            // 
            txtName.Location = new Point(86, 417);
            txtName.Name = "txtName";
            txtName.Size = new Size(180, 26);
            txtName.TabIndex = 25;
            // 
            // lblID
            // 
            lblID.AutoSize = true;
            lblID.Location = new Point(57, 370);
            lblID.Name = "lblID";
            lblID.Size = new Size(23, 19);
            lblID.TabIndex = 24;
            lblID.Text = "ID";
            // 
            // txtID
            // 
            txtID.Location = new Point(86, 367);
            txtID.Name = "txtID";
            txtID.ReadOnly = true;
            txtID.Size = new Size(130, 26);
            txtID.TabIndex = 23;
            // 
            // clUserID
            // 
            clUserID.Frozen = true;
            clUserID.HeaderText = "ID";
            clUserID.Name = "clUserID";
            clUserID.ReadOnly = true;
            clUserID.Width = 70;
            // 
            // clName
            // 
            clName.Frozen = true;
            clName.HeaderText = "Tên";
            clName.Name = "clName";
            clName.ReadOnly = true;
            clName.Width = 180;
            // 
            // clGender
            // 
            clGender.Frozen = true;
            clGender.HeaderText = "Giới tính";
            clGender.Name = "clGender";
            clGender.ReadOnly = true;
            // 
            // clDateofBirth
            // 
            clDateofBirth.Frozen = true;
            clDateofBirth.HeaderText = "Sinh";
            clDateofBirth.Name = "clDateofBirth";
            clDateofBirth.ReadOnly = true;
            // 
            // clPhone
            // 
            clPhone.Frozen = true;
            clPhone.HeaderText = "Số điện thoại";
            clPhone.Name = "clPhone";
            clPhone.ReadOnly = true;
            // 
            // clPosition
            // 
            clPosition.Frozen = true;
            clPosition.HeaderText = "Vị trí";
            clPosition.Name = "clPosition";
            clPosition.ReadOnly = true;
            // 
            // clHireDate
            // 
            clHireDate.Frozen = true;
            clHireDate.HeaderText = "Ngày tuyển dụng";
            clHireDate.Name = "clHireDate";
            clHireDate.ReadOnly = true;
            // 
            // clUpdateRole
            // 
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            clUpdateRole.DefaultCellStyle = dataGridViewCellStyle2;
            clUpdateRole.Frozen = true;
            clUpdateRole.HeaderText = "Thăng chức";
            clUpdateRole.Name = "clUpdateRole";
            clUpdateRole.ReadOnly = true;
            clUpdateRole.Resizable = DataGridViewTriState.True;
            // 
            // EmployeeAdminForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(891, 532);
            Controls.Add(txtEmail);
            Controls.Add(lblEmail);
            Controls.Add(txtPosition);
            Controls.Add(lblPosition);
            Controls.Add(btnSaThai);
            Controls.Add(button1);
            Controls.Add(btnThem);
            Controls.Add(dateTimePicker1);
            Controls.Add(cbGender);
            Controls.Add(lblUserName);
            Controls.Add(txtUserName);
            Controls.Add(lblPhone);
            Controls.Add(txtPhone);
            Controls.Add(lblDateOfBirth);
            Controls.Add(lblGender);
            Controls.Add(lblFullName);
            Controls.Add(txtName);
            Controls.Add(lblID);
            Controls.Add(txtID);
            Controls.Add(pnlData);
            Name = "EmployeeAdminForm";
            Text = "EmployeeAdminForm";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            pnlData.ResumeLayout(false);
            pnlData.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Panel pnlData;
        private Label lblHeader;
        private ComboBox comboBox1;
        private TextBox txtEmail;
        private Label lblEmail;
        private TextBox txtPosition;
        private Label lblPosition;
        private Button btnSaThai;
        private Button button1;
        private Button btnThem;
        private DateTimePicker dateTimePicker1;
        private ComboBox cbGender;
        private Label lblUserName;
        private TextBox txtUserName;
        private Label lblPhone;
        private TextBox txtPhone;
        private Label lblDateOfBirth;
        private Label lblGender;
        private Label lblFullName;
        private TextBox txtName;
        private Label lblID;
        private TextBox txtID;
        private DataGridViewTextBoxColumn clUserID;
        private DataGridViewTextBoxColumn clName;
        private DataGridViewTextBoxColumn clGender;
        private DataGridViewTextBoxColumn clDateofBirth;
        private DataGridViewTextBoxColumn clPhone;
        private DataGridViewTextBoxColumn clPosition;
        private DataGridViewTextBoxColumn clHireDate;
        private DataGridViewTextBoxColumn clUpdateRole;
    }
}