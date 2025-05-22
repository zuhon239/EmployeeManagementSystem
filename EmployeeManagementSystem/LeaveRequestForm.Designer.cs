namespace EmployeeManagementSystem
{
    partial class LeaveRequestForm
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

        private Label lblStartDate;
        private DateTimePicker dtpStartDate;
        private Label lblEndDate;
        private DateTimePicker dtpEndDate;
        private Label lblReason;
        private TextBox txtReason;
        private Label lblShift;
        private ComboBox cmbShift;
        private Panel pnlShift;
        private Button btnSubmit;
        private Button btnCancel;
        private Panel headerPanel;
        private Label lblHeader;

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            headerPanel = new Panel();
            lblHeader = new Label();
            lblStartDate = new Label();
            dtpStartDate = new DateTimePicker();
            lblEndDate = new Label();
            dtpEndDate = new DateTimePicker();
            lblReason = new Label();
            txtReason = new TextBox();
            lblShift = new Label();
            cmbShift = new ComboBox();
            pnlShift = new Panel();
            btnSubmit = new Button();
            btnCancel = new Button();
            SuspendLayout();

            // 
            // headerPanel
            // 
            headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            headerPanel.Controls.Add(lblHeader);
            headerPanel.Location = new System.Drawing.Point(12, 12);
            headerPanel.Name = "headerPanel";
            headerPanel.Size = new System.Drawing.Size(476, 44);
            headerPanel.TabIndex = 0;

            // 
            // lblHeader
            // 
            lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            lblHeader.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            lblHeader.ForeColor = System.Drawing.Color.White;
            lblHeader.Location = new System.Drawing.Point(0, 0);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new System.Drawing.Size(476, 44);
            lblHeader.TabIndex = 0;
            lblHeader.Text = "Yêu Cầu Nghỉ Phép";
            lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // lblStartDate
            // 
            lblStartDate.AutoSize = true;
            lblStartDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblStartDate.Location = new System.Drawing.Point(30, 70);
            lblStartDate.Name = "lblStartDate";
            lblStartDate.Size = new System.Drawing.Size(114, 23);
            lblStartDate.TabIndex = 1;
            lblStartDate.Text = "Ngày bắt đầu:";

            // 
            // dtpStartDate
            // 
            dtpStartDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            dtpStartDate.Location = new System.Drawing.Point(150, 67);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new System.Drawing.Size(250, 30);
            dtpStartDate.TabIndex = 2;
            dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            // 
            // lblEndDate
            // 
            lblEndDate.AutoSize = true;
            lblEndDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblEndDate.Location = new System.Drawing.Point(30, 110);
            lblEndDate.Name = "lblEndDate";
            lblEndDate.Size = new System.Drawing.Size(114, 23);
            lblEndDate.TabIndex = 3;
            lblEndDate.Text = "Ngày kết thúc:";

            // 
            // dtpEndDate
            // 
            dtpEndDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            dtpEndDate.Location = new System.Drawing.Point(150, 107);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Size = new System.Drawing.Size(250, 30);
            dtpEndDate.TabIndex = 4;
            dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            // 
            // lblReason
            // 
            lblReason.AutoSize = true;
            lblReason.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblReason.Location = new System.Drawing.Point(30, 150);
            lblReason.Name = "lblReason";
            lblReason.Size = new System.Drawing.Size(54, 23);
            lblReason.TabIndex = 5;
            lblReason.Text = "Lý do:";

            // 
            // txtReason
            // 
            txtReason.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtReason.Location = new System.Drawing.Point(150, 147);
            txtReason.Multiline = true;
            txtReason.Name = "txtReason";
            txtReason.Size = new System.Drawing.Size(250, 60);
            txtReason.TabIndex = 6;

            // 
            // lblShift
            // 
            lblShift.AutoSize = true;
            lblShift.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblShift.Location = new System.Drawing.Point(0, 5);
            lblShift.Name = "lblShift";
            lblShift.Size = new System.Drawing.Size(70, 23);
            lblShift.TabIndex = 0;
            lblShift.Text = "Ca nghỉ:";

            // 
            // cmbShift
            // 
            cmbShift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbShift.Font = new System.Drawing.Font("Segoe UI", 10F);
            cmbShift.Location = new System.Drawing.Point(100, 2);
            cmbShift.Name = "cmbShift";
            cmbShift.Size = new System.Drawing.Size(150, 31);
            cmbShift.TabIndex = 1;

            // 
            // pnlShift
            // 
            pnlShift.Controls.Add(lblShift);
            pnlShift.Controls.Add(cmbShift);
            pnlShift.Location = new System.Drawing.Point(150, 220);
            pnlShift.Name = "pnlShift";
            pnlShift.Size = new System.Drawing.Size(250, 34);
            pnlShift.TabIndex = 7;

            // 
            // btnSubmit
            // 
            btnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSubmit.Font = new System.Drawing.Font("Segoe UI", 10F);
            btnSubmit.ForeColor = System.Drawing.Color.White;
            btnSubmit.Location = new System.Drawing.Point(150, 270);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new System.Drawing.Size(120, 35);
            btnSubmit.TabIndex = 8;
            btnSubmit.Text = "Gửi Yêu Cầu";
            btnSubmit.UseVisualStyleBackColor = false;

            // 
            // btnCancel
            // 
            btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            btnCancel.ForeColor = System.Drawing.Color.White;
            btnCancel.Location = new System.Drawing.Point(280, 270);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(120, 35);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = false;

            // 
            // LeaveRequestForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(500, 400);
            Controls.Add(headerPanel);
            Controls.Add(lblStartDate);
            Controls.Add(dtpStartDate);
            Controls.Add(lblEndDate);
            Controls.Add(dtpEndDate);
            Controls.Add(lblReason);
            Controls.Add(txtReason);
            Controls.Add(pnlShift);
            Controls.Add(btnSubmit);
            Controls.Add(btnCancel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "LeaveRequestForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Xin Nghỉ Phép";
            BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            btnSubmit.Click += BtnSubmit_Click;
            btnCancel.Click += (s, e) => Close();
            dtpStartDate.ValueChanged += (s, e) => UpdateShiftVisibility();
            dtpEndDate.ValueChanged += (s, e) => UpdateShiftVisibility();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}