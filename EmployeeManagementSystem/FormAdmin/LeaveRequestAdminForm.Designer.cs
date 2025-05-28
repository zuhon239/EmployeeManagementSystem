namespace EmployeeManagementSystem.FormAdmin
{
    partial class LeaveRequestAdminForm
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
            comboBox1 = new ComboBox();
            dataGridView1 = new DataGridView();
            pnlDetail = new Panel();
            lblReason = new Label();
            lblName = new Label();
            btnAccept = new Button();
            btnReject = new Button();
            clLeave_Id = new DataGridViewTextBoxColumn();
            clUserID = new DataGridViewTextBoxColumn();
            clName = new DataGridViewTextBoxColumn();
            clRoleName = new DataGridViewTextBoxColumn();
            clStartDate = new DataGridViewTextBoxColumn();
            clEndDate = new DataGridViewTextBoxColumn();
            clShift = new DataGridViewTextBoxColumn();
            clDetail = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            pnlDetail.SuspendLayout();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Manager", "Phòng phát triển phần mềm", "Phòng kiểm thử", "Phòng thiết kế giao diện", "Phòng DevOps", "Phòng dữ liệu", "Phòng hồ sơ kỹ thuật" });
            comboBox1.Location = new Point(12, 12);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(163, 27);
            comboBox1.TabIndex = 1;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.BackgroundColor = SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { clLeave_Id, clUserID, clName, clRoleName, clStartDate, clEndDate, clShift, clDetail });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.Location = new Point(12, 45);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView1.Size = new Size(878, 264);
            dataGridView1.TabIndex = 2;
            // 
            // pnlDetail
            // 
            pnlDetail.Anchor = AnchorStyles.None;
            pnlDetail.BorderStyle = BorderStyle.Fixed3D;
            pnlDetail.Controls.Add(lblReason);
            pnlDetail.Controls.Add(lblName);
            pnlDetail.Location = new Point(13, 326);
            pnlDetail.Name = "pnlDetail";
            pnlDetail.Size = new Size(486, 167);
            pnlDetail.TabIndex = 3;
            // 
            // lblReason
            // 
            lblReason.Location = new Point(19, 68);
            lblReason.Name = "lblReason";
            lblReason.Size = new Size(425, 87);
            lblReason.TabIndex = 1;
            // 
            // lblName
            // 
            lblName.Location = new Point(19, 22);
            lblName.Name = "lblName";
            lblName.Size = new Size(269, 30);
            lblName.TabIndex = 0;
            // 
            // btnAccept
            // 
            btnAccept.Anchor = AnchorStyles.None;
            btnAccept.FlatAppearance.BorderSize = 0;
            btnAccept.FlatStyle = FlatStyle.Flat;
            btnAccept.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAccept.ForeColor = Color.LawnGreen;
            btnAccept.Location = new Point(523, 328);
            btnAccept.Name = "btnAccept";
            btnAccept.Size = new Size(75, 73);
            btnAccept.TabIndex = 4;
            btnAccept.Text = "✓";
            btnAccept.UseVisualStyleBackColor = true;
            btnAccept.Click += btnAccept_Click;
            // 
            // btnReject
            // 
            btnReject.Anchor = AnchorStyles.None;
            btnReject.FlatAppearance.BorderSize = 0;
            btnReject.FlatStyle = FlatStyle.Flat;
            btnReject.Font = new Font("Berlin Sans FB", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnReject.ForeColor = Color.Red;
            btnReject.Location = new Point(523, 420);
            btnReject.Name = "btnReject";
            btnReject.Size = new Size(75, 73);
            btnReject.TabIndex = 5;
            btnReject.Text = "X";
            btnReject.UseVisualStyleBackColor = true;
            btnReject.Click += btnReject_Click;
            // 
            // clLeave_Id
            // 
            clLeave_Id.Frozen = true;
            clLeave_Id.HeaderText = "Leave_ID";
            clLeave_Id.Name = "clLeave_Id";
            clLeave_Id.ReadOnly = true;
            // 
            // clUserID
            // 
            clUserID.Frozen = true;
            clUserID.HeaderText = "User_ID";
            clUserID.Name = "clUserID";
            clUserID.ReadOnly = true;
            // 
            // clName
            // 
            clName.Frozen = true;
            clName.HeaderText = "Tên";
            clName.Name = "clName";
            clName.ReadOnly = true;
            clName.Width = 180;
            // 
            // clRoleName
            // 
            clRoleName.Frozen = true;
            clRoleName.HeaderText = "Vai trò";
            clRoleName.Name = "clRoleName";
            clRoleName.ReadOnly = true;
            // 
            // clStartDate
            // 
            clStartDate.Frozen = true;
            clStartDate.HeaderText = "Ngày bắt đầu";
            clStartDate.Name = "clStartDate";
            // 
            // clEndDate
            // 
            clEndDate.Frozen = true;
            clEndDate.HeaderText = "Ngày kết thúc";
            clEndDate.Name = "clEndDate";
            clEndDate.ReadOnly = true;
            // 
            // clShift
            // 
            clShift.Frozen = true;
            clShift.HeaderText = "Ca";
            clShift.Name = "clShift";
            clShift.ReadOnly = true;
            // 
            // clDetail
            // 
            clDetail.Frozen = true;
            clDetail.HeaderText = "Chi tiết";
            clDetail.Name = "clDetail";
            clDetail.ReadOnly = true;
            // 
            // LeaveRequestAdminForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.White;
            ClientSize = new Size(902, 505);
            Controls.Add(btnReject);
            Controls.Add(btnAccept);
            Controls.Add(pnlDetail);
            Controls.Add(dataGridView1);
            Controls.Add(comboBox1);
            MaximizeBox = false;
            Name = "LeaveRequestAdminForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LeaveManagerAdminForm";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            pnlDetail.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ComboBox comboBox1;
        private DataGridView dataGridView1;
        private Panel pnlDetail;
        private Button btnAccept;
        private Button btnReject;
        private Label lblReason;
        private Label lblName;
        private DataGridViewTextBoxColumn clLeave_Id;
        private DataGridViewTextBoxColumn clUserID;
        private DataGridViewTextBoxColumn clName;
        private DataGridViewTextBoxColumn clRoleName;
        private DataGridViewTextBoxColumn clStartDate;
        private DataGridViewTextBoxColumn clEndDate;
        private DataGridViewTextBoxColumn clShift;
        private DataGridViewTextBoxColumn clDetail;
    }
}