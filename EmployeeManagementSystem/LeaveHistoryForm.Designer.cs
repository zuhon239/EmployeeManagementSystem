namespace EmployeeManagementSystem
{
    partial class LeaveHistoryForm
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
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            dataGridView1 = new DataGridView();
            clleaveID = new DataGridViewTextBoxColumn();
            clStartDate = new DataGridViewTextBoxColumn();
            clEndDate = new DataGridViewTextBoxColumn();
            clShift = new DataGridViewTextBoxColumn();
            clStatus = new DataGridViewTextBoxColumn();
            clApprovalName = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { clleaveID, clStartDate, clEndDate, clShift, clStatus, clApprovalName });
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = SystemColors.Window;
            dataGridViewCellStyle8.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle8.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle8;
            dataGridView1.GridColor = Color.White;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Size = new Size(696, 474);
            dataGridView1.TabIndex = 0;
            // 
            // clleaveID
            // 
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            clleaveID.DefaultCellStyle = dataGridViewCellStyle2;
            clleaveID.Frozen = true;
            clleaveID.HeaderText = "Leave_ID";
            clleaveID.Name = "clleaveID";
            clleaveID.ReadOnly = true;
            // 
            // clStartDate
            // 
            dataGridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            clStartDate.DefaultCellStyle = dataGridViewCellStyle3;
            clStartDate.Frozen = true;
            clStartDate.HeaderText = "Start Date";
            clStartDate.Name = "clStartDate";
            clStartDate.ReadOnly = true;
            // 
            // clEndDate
            // 
            dataGridViewCellStyle4.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            clEndDate.DefaultCellStyle = dataGridViewCellStyle4;
            clEndDate.Frozen = true;
            clEndDate.HeaderText = "End Date";
            clEndDate.Name = "clEndDate";
            clEndDate.ReadOnly = true;
            // 
            // clShift
            // 
            dataGridViewCellStyle5.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            clShift.DefaultCellStyle = dataGridViewCellStyle5;
            clShift.Frozen = true;
            clShift.HeaderText = "Shift";
            clShift.Name = "clShift";
            clShift.ReadOnly = true;
            // 
            // clStatus
            // 
            dataGridViewCellStyle6.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            clStatus.DefaultCellStyle = dataGridViewCellStyle6;
            clStatus.Frozen = true;
            clStatus.HeaderText = "Status";
            clStatus.Name = "clStatus";
            clStatus.ReadOnly = true;
            // 
            // clApprovalName
            // 
            dataGridViewCellStyle7.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            clApprovalName.DefaultCellStyle = dataGridViewCellStyle7;
            clApprovalName.Frozen = true;
            clApprovalName.HeaderText = "ApprovalName";
            clApprovalName.Name = "clApprovalName";
            clApprovalName.ReadOnly = true;
            clApprovalName.Width = 200;
            // 
            // LeaveHistoryForm
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(600, 474);
            Controls.Add(dataGridView1);
            Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            MaximizeBox = false;
            Name = "LeaveHistoryForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LeaveHistoryForm";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn clleaveID;
        private DataGridViewTextBoxColumn clStartDate;
        private DataGridViewTextBoxColumn clEndDate;
        private DataGridViewTextBoxColumn clShift;
        private DataGridViewTextBoxColumn clStatus;
        private DataGridViewTextBoxColumn clApprovalName;
    }
}