namespace EmployeeManagementSystem.FormManager
{
    partial class LeaveRequestManager
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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            dataGridView1 = new DataGridView();
            clLeaveId = new DataGridViewTextBoxColumn();
            clUserid = new DataGridViewTextBoxColumn();
            clName = new DataGridViewTextBoxColumn();
            clStartdate = new DataGridViewTextBoxColumn();
            clEnddate = new DataGridViewTextBoxColumn();
            clShift = new DataGridViewTextBoxColumn();
            clStatus = new DataGridViewTextBoxColumn();
            clDetail = new DataGridViewTextBoxColumn();
            panel1 = new Panel();
            lblReason = new Label();
            lblHoTen = new Label();
            btnAccept = new Button();
            btnDeny = new Button();
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
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new Padding(2, 2, 3, 3);
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { clLeaveId, clUserid, clName, clStartdate, clEnddate, clShift, clStatus, clDetail });
            dataGridView1.GridColor = Color.White;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new Size(930, 280);
            dataGridView1.TabIndex = 0;
            // 
            // clLeaveId
            // 
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            clLeaveId.DefaultCellStyle = dataGridViewCellStyle2;
            clLeaveId.FillWeight = 200F;
            clLeaveId.Frozen = true;
            clLeaveId.HeaderText = "Leave_id";
            clLeaveId.Name = "clLeaveId";
            clLeaveId.ReadOnly = true;
            clLeaveId.Width = 95;
            // 
            // clUserid
            // 
            dataGridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            clUserid.DefaultCellStyle = dataGridViewCellStyle3;
            clUserid.Frozen = true;
            clUserid.HeaderText = "User_id";
            clUserid.Name = "clUserid";
            clUserid.ReadOnly = true;
            clUserid.Width = 95;
            // 
            // clName
            // 
            clName.Frozen = true;
            clName.HeaderText = "Name";
            clName.Name = "clName";
            clName.ReadOnly = true;
            clName.Width = 210;
            // 
            // clStartdate
            // 
            clStartdate.Frozen = true;
            clStartdate.HeaderText = "Start Date";
            clStartdate.Name = "clStartdate";
            clStartdate.ReadOnly = true;
            clStartdate.Width = 90;
            // 
            // clEnddate
            // 
            clEnddate.Frozen = true;
            clEnddate.HeaderText = "End Date";
            clEnddate.Name = "clEnddate";
            clEnddate.ReadOnly = true;
            clEnddate.Width = 90;
            // 
            // clShift
            // 
            clShift.Frozen = true;
            clShift.HeaderText = "Shift";
            clShift.Name = "clShift";
            clShift.ReadOnly = true;
            clShift.Width = 90;
            // 
            // clStatus
            // 
            clStatus.Frozen = true;
            clStatus.HeaderText = "Status";
            clStatus.Name = "clStatus";
            clStatus.ReadOnly = true;
            // 
            // clDetail
            // 
            dataGridViewCellStyle4.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            clDetail.DefaultCellStyle = dataGridViewCellStyle4;
            clDetail.Frozen = true;
            clDetail.HeaderText = "See Detail";
            clDetail.Name = "clDetail";
            clDetail.ReadOnly = true;
            clDetail.Width = 120;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(lblReason);
            panel1.Controls.Add(lblHoTen);
            panel1.Location = new Point(12, 286);
            panel1.Name = "panel1";
            panel1.Size = new Size(570, 171);
            panel1.TabIndex = 1;
            // 
            // lblReason
            // 
            lblReason.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblReason.Location = new Point(24, 70);
            lblReason.Name = "lblReason";
            lblReason.Size = new Size(525, 89);
            lblReason.TabIndex = 1;
            // 
            // lblHoTen
            // 
            lblHoTen.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblHoTen.Location = new Point(24, 32);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.RightToLeft = RightToLeft.No;
            lblHoTen.Size = new Size(338, 23);
            lblHoTen.TabIndex = 0;
            // 
            // btnAccept
            // 
            btnAccept.FlatAppearance.BorderSize = 0;
            btnAccept.FlatStyle = FlatStyle.Flat;
            btnAccept.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAccept.ForeColor = Color.FromArgb(0, 192, 0);
            btnAccept.Location = new Point(604, 303);
            btnAccept.Name = "btnAccept";
            btnAccept.Size = new Size(68, 61);
            btnAccept.TabIndex = 2;
            btnAccept.Text = "✓";
            btnAccept.UseVisualStyleBackColor = true;
            btnAccept.Click += btnAccept_Click;
            // 
            // btnDeny
            // 
            btnDeny.FlatAppearance.BorderSize = 0;
            btnDeny.FlatStyle = FlatStyle.Flat;
            btnDeny.Font = new Font("Berlin Sans FB", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDeny.ForeColor = Color.Red;
            btnDeny.Location = new Point(604, 385);
            btnDeny.Name = "btnDeny";
            btnDeny.Size = new Size(68, 61);
            btnDeny.TabIndex = 3;
            btnDeny.Text = "X";
            btnDeny.UseVisualStyleBackColor = true;
            btnDeny.Click += btnDeny_Click_1;
            // 
            // LeaveRequestManager
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(930, 469);
            Controls.Add(btnDeny);
            Controls.Add(btnAccept);
            Controls.Add(panel1);
            Controls.Add(dataGridView1);
            Name = "LeaveRequestManager";
            Text = "LeaveRequestManager";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private Panel panel1;
        private Button btnAccept;
        private Button btnDeny;
        private Label lblReason;
        private Label lblHoTen;
        private DataGridViewTextBoxColumn clLeaveId;
        private DataGridViewTextBoxColumn clUserid;
        private DataGridViewTextBoxColumn clName;
        private DataGridViewTextBoxColumn clStartdate;
        private DataGridViewTextBoxColumn clEnddate;
        private DataGridViewTextBoxColumn clShift;
        private DataGridViewTextBoxColumn clStatus;
        private DataGridViewTextBoxColumn clDetail;
    }
}