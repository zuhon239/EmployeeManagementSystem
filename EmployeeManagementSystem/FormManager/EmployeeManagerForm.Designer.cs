namespace EmployeeManagementSystem.FormManager
{
    partial class EmployeeManagerForm
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
            dataGridView1 = new DataGridView();
            panel1 = new Panel();
            lblHeader = new Label();
            txtID = new TextBox();
            lblID = new Label();
            lblFullName = new Label();
            txtName = new TextBox();
            lblGender = new Label();
            lblDateOfBirth = new Label();
            lblPhone = new Label();
            txtPhone = new TextBox();
            label3 = new Label();
            txtUserName = new TextBox();
            comboBox1 = new ComboBox();
            dateTimePicker1 = new DateTimePicker();
            btnThem = new Button();
            button1 = new Button();
            btnSaThai = new Button();
            lblPosition = new Label();
            txtPosition = new TextBox();
            txtEmail = new TextBox();
            lblEmail = new Label();
            clUserId = new DataGridViewTextBoxColumn();
            clName = new DataGridViewTextBoxColumn();
            clGender = new DataGridViewTextBoxColumn();
            clDateofBirth = new DataGridViewTextBoxColumn();
            clPhone = new DataGridViewTextBoxColumn();
            clPosition = new DataGridViewTextBoxColumn();
            clHireDate = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.Anchor = AnchorStyles.None;
            dataGridView1.BackgroundColor = Color.White;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.Navy;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { clUserId, clName, clGender, clDateofBirth, clPhone, clPosition, clHireDate });
            dataGridView1.Location = new Point(9, 43);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Size = new Size(845, 229);
            dataGridView1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.None;
            panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(lblHeader);
            panel1.Controls.Add(dataGridView1);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(866, 288);
            panel1.TabIndex = 1;
            // 
            // lblHeader
            // 
            lblHeader.AutoSize = true;
            lblHeader.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblHeader.Location = new Point(12, 9);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(194, 25);
            lblHeader.TabIndex = 1;
            lblHeader.Text = "Danh sách nhân viên";
            // 
            // txtID
            // 
            txtID.Location = new Point(88, 322);
            txtID.Name = "txtID";
            txtID.ReadOnly = true;
            txtID.Size = new Size(130, 26);
            txtID.TabIndex = 2;
            // 
            // lblID
            // 
            lblID.AutoSize = true;
            lblID.Location = new Point(59, 325);
            lblID.Name = "lblID";
            lblID.Size = new Size(23, 19);
            lblID.TabIndex = 3;
            lblID.Text = "ID";
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Location = new Point(37, 377);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(30, 19);
            lblFullName.TabIndex = 5;
            lblFullName.Text = "Tên";
            // 
            // txtName
            // 
            txtName.Location = new Point(88, 374);
            txtName.Name = "txtName";
            txtName.Size = new Size(180, 26);
            txtName.TabIndex = 4;
            // 
            // lblGender
            // 
            lblGender.AutoSize = true;
            lblGender.Location = new Point(303, 381);
            lblGender.Name = "lblGender";
            lblGender.Size = new Size(61, 19);
            lblGender.TabIndex = 7;
            lblGender.Text = "Giới tính";
            // 
            // lblDateOfBirth
            // 
            lblDateOfBirth.AutoSize = true;
            lblDateOfBirth.Location = new Point(329, 329);
            lblDateOfBirth.Name = "lblDateOfBirth";
            lblDateOfBirth.Size = new Size(35, 19);
            lblDateOfBirth.TabIndex = 9;
            lblDateOfBirth.Text = "Sinh";
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(275, 436);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(89, 19);
            lblPhone.TabIndex = 11;
            lblPhone.Text = "Số điện thoại";
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(370, 433);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(130, 26);
            txtPhone.TabIndex = 10;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(9, 436);
            label3.Name = "label3";
            label3.Size = new Size(73, 19);
            label3.TabIndex = 13;
            label3.Text = "UserName";
            // 
            // txtUserName
            // 
            txtUserName.Location = new Point(88, 433);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(180, 26);
            txtUserName.TabIndex = 12;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(370, 377);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(130, 27);
            comboBox1.TabIndex = 14;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Checked = false;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(370, 325);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(130, 26);
            dateTimePicker1.TabIndex = 15;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(747, 316);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(121, 37);
            btnThem.TabIndex = 16;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(747, 371);
            button1.Name = "button1";
            button1.Size = new Size(121, 37);
            button1.TabIndex = 17;
            button1.Text = "Sửa";
            button1.UseVisualStyleBackColor = true;
            // 
            // btnSaThai
            // 
            btnSaThai.Location = new Point(747, 427);
            btnSaThai.Name = "btnSaThai";
            btnSaThai.Size = new Size(121, 37);
            btnSaThai.TabIndex = 18;
            btnSaThai.Text = "Sa thải";
            btnSaThai.UseVisualStyleBackColor = true;
            // 
            // lblPosition
            // 
            lblPosition.AutoSize = true;
            lblPosition.Location = new Point(533, 329);
            lblPosition.Name = "lblPosition";
            lblPosition.Size = new Size(38, 19);
            lblPosition.TabIndex = 19;
            lblPosition.Text = "Vị trí";
            // 
            // txtPosition
            // 
            txtPosition.Location = new Point(577, 325);
            txtPosition.Name = "txtPosition";
            txtPosition.Size = new Size(142, 26);
            txtPosition.TabIndex = 20;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(577, 377);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(142, 26);
            txtEmail.TabIndex = 22;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(530, 381);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(41, 19);
            lblEmail.TabIndex = 21;
            lblEmail.Text = "Email";
            // 
            // clUserId
            // 
            clUserId.Frozen = true;
            clUserId.HeaderText = "ID";
            clUserId.Name = "clUserId";
            clUserId.ReadOnly = true;
            clUserId.Width = 110;
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
            clDateofBirth.Width = 120;
            // 
            // clPhone
            // 
            clPhone.Frozen = true;
            clPhone.HeaderText = "Số điện thoại";
            clPhone.Name = "clPhone";
            clPhone.ReadOnly = true;
            clPhone.Width = 120;
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
            clHireDate.Width = 120;
            // 
            // EmployeeManagerForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(890, 483);
            Controls.Add(txtEmail);
            Controls.Add(lblEmail);
            Controls.Add(txtPosition);
            Controls.Add(lblPosition);
            Controls.Add(btnSaThai);
            Controls.Add(button1);
            Controls.Add(btnThem);
            Controls.Add(dateTimePicker1);
            Controls.Add(comboBox1);
            Controls.Add(label3);
            Controls.Add(txtUserName);
            Controls.Add(lblPhone);
            Controls.Add(txtPhone);
            Controls.Add(lblDateOfBirth);
            Controls.Add(lblGender);
            Controls.Add(lblFullName);
            Controls.Add(txtName);
            Controls.Add(lblID);
            Controls.Add(txtID);
            Controls.Add(panel1);
            Name = "EmployeeManagerForm";
            Text = "EmployeeManagerForm";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Panel panel1;
        private Label lblHeader;
        private TextBox txtID;
        private Label lblID;
        private Label lblFullName;
        private TextBox txtName;
        private Label lblGender;
        private Label lblDateOfBirth;
        private Label lblPhone;
        private TextBox txtPhone;
        private Label label3;
        private TextBox txtUserName;
        private ComboBox comboBox1;
        private DateTimePicker dateTimePicker1;
        private Button btnThem;
        private Button button1;
        private Button btnSaThai;
        private Label lblPosition;
        private TextBox txtPosition;
        private TextBox txtEmail;
        private Label lblEmail;
        private DataGridViewTextBoxColumn clUserId;
        private DataGridViewTextBoxColumn clName;
        private DataGridViewTextBoxColumn clGender;
        private DataGridViewTextBoxColumn clDateofBirth;
        private DataGridViewTextBoxColumn clPhone;
        private DataGridViewTextBoxColumn clPosition;
        private DataGridViewTextBoxColumn clHireDate;
    }
}