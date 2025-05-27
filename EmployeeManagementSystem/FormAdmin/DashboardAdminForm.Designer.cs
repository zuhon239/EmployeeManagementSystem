namespace EmployeeManagementSystem.FormAdmin
{
    partial class DashboardAdminForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea17 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend17 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series17 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea18 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend18 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series18 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title9 = new System.Windows.Forms.DataVisualization.Charting.Title();
            panel6 = new Panel();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            panel5 = new Panel();
            chartSalary = new System.Windows.Forms.DataVisualization.Charting.Chart();
            cbxDepartments = new ComboBox();
            panel4 = new Panel();
            label2 = new Label();
            lblTotalSalary = new Label();
            panel3 = new Panel();
            lblPendingAmount = new Label();
            lblLeaveRequestPending = new Label();
            panel2 = new Panel();
            lblAmountAttendance = new Label();
            lblAttendance = new Label();
            panel1 = new Panel();
            label1 = new Label();
            lblActiveEmployee = new Label();
            pnlTotalEmployee = new Panel();
            lblTotalAmount = new Label();
            lblTotalemployee = new Label();
            btnLast7days = new Button();
            btnThisMonth = new Button();
            btnLast30days = new Button();
            btnToday = new Button();
            dtpTo = new DateTimePicker();
            dtpFrom = new DateTimePicker();
            lblHeader = new Label();
            comboBox1 = new ComboBox();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartSalary).BeginInit();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            pnlTotalEmployee.SuspendLayout();
            SuspendLayout();
            // 
            // panel6
            // 
            panel6.BorderStyle = BorderStyle.FixedSingle;
            panel6.Controls.Add(chart1);
            panel6.Location = new Point(517, 201);
            panel6.Name = "panel6";
            panel6.Size = new Size(383, 311);
            panel6.TabIndex = 31;
            // 
            // chart1
            // 
            chartArea17.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea17);
            legend17.Name = "Legend1";
            chart1.Legends.Add(legend17);
            chart1.Location = new Point(7, 6);
            chart1.Name = "chart1";
            chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series17.ChartArea = "ChartArea1";
            series17.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series17.Legend = "Legend1";
            series17.Name = "Series1";
            chart1.Series.Add(series17);
            chart1.Size = new Size(350, 294);
            chart1.TabIndex = 12;
            chart1.Text = "Salary";
            // 
            // panel5
            // 
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel5.Controls.Add(chartSalary);
            panel5.Location = new Point(12, 171);
            panel5.Name = "panel5";
            panel5.Size = new Size(499, 264);
            panel5.TabIndex = 30;
            // 
            // chartSalary
            // 
            chartSalary.BorderlineColor = Color.Black;
            chartSalary.BorderSkin.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.LeftRight;
            chartArea18.Name = "ChartArea1";
            chartSalary.ChartAreas.Add(chartArea18);
            legend18.Name = "Legend1";
            chartSalary.Legends.Add(legend18);
            chartSalary.Location = new Point(3, 8);
            chartSalary.Name = "chartSalary";
            chartSalary.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series18.ChartArea = "ChartArea1";
            series18.Legend = "Legend1";
            series18.Name = "Series1";
            chartSalary.Series.Add(series18);
            chartSalary.Size = new Size(491, 257);
            chartSalary.TabIndex = 11;
            chartSalary.Text = "Salary";
            title9.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            title9.Name = "Salary";
            chartSalary.Titles.Add(title9);
            // 
            // cbxDepartments
            // 
            cbxDepartments.FormattingEnabled = true;
            cbxDepartments.Location = new Point(517, 171);
            cbxDepartments.Name = "cbxDepartments";
            cbxDepartments.Size = new Size(179, 27);
            cbxDepartments.TabIndex = 29;
            // 
            // panel4
            // 
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(label2);
            panel4.Controls.Add(lblTotalSalary);
            panel4.Location = new Point(12, 443);
            panel4.Name = "panel4";
            panel4.Size = new Size(421, 59);
            panel4.TabIndex = 28;
            // 
            // label2
            // 
            label2.Font = new Font("Dubai", 20.2499981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(15, 19);
            label2.Name = "label2";
            label2.Size = new Size(350, 38);
            label2.TabIndex = 2;
            label2.Text = "60";
            // 
            // lblTotalSalary
            // 
            lblTotalSalary.AutoSize = true;
            lblTotalSalary.ForeColor = Color.Gray;
            lblTotalSalary.Location = new Point(14, 0);
            lblTotalSalary.Name = "lblTotalSalary";
            lblTotalSalary.Size = new Size(74, 19);
            lblTotalSalary.TabIndex = 1;
            lblTotalSalary.Text = "TotalSalary";
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(lblPendingAmount);
            panel3.Controls.Add(lblLeaveRequestPending);
            panel3.Location = new Point(647, 90);
            panel3.Name = "panel3";
            panel3.Size = new Size(268, 68);
            panel3.TabIndex = 27;
            // 
            // lblPendingAmount
            // 
            lblPendingAmount.AutoSize = true;
            lblPendingAmount.Font = new Font("Dubai", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPendingAmount.Location = new Point(22, 19);
            lblPendingAmount.Name = "lblPendingAmount";
            lblPendingAmount.Size = new Size(54, 49);
            lblPendingAmount.TabIndex = 2;
            lblPendingAmount.Text = "60";
            // 
            // lblLeaveRequestPending
            // 
            lblLeaveRequestPending.AutoSize = true;
            lblLeaveRequestPending.ForeColor = Color.Gray;
            lblLeaveRequestPending.Location = new Point(-1, 0);
            lblLeaveRequestPending.Name = "lblLeaveRequestPending";
            lblLeaveRequestPending.Size = new Size(205, 19);
            lblLeaveRequestPending.TabIndex = 1;
            lblLeaveRequestPending.Text = "Leave request pending approval";
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(lblAmountAttendance);
            panel2.Controls.Add(lblAttendance);
            panel2.Location = new Point(434, 90);
            panel2.Name = "panel2";
            panel2.Size = new Size(186, 68);
            panel2.TabIndex = 26;
            // 
            // lblAmountAttendance
            // 
            lblAmountAttendance.AutoSize = true;
            lblAmountAttendance.Font = new Font("Dubai", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAmountAttendance.Location = new Point(22, 19);
            lblAmountAttendance.Name = "lblAmountAttendance";
            lblAmountAttendance.Size = new Size(54, 49);
            lblAmountAttendance.TabIndex = 2;
            lblAmountAttendance.Text = "60";
            // 
            // lblAttendance
            // 
            lblAttendance.AutoSize = true;
            lblAttendance.ForeColor = Color.Gray;
            lblAttendance.Location = new Point(13, 0);
            lblAttendance.Name = "lblAttendance";
            lblAttendance.Size = new Size(79, 19);
            lblAttendance.TabIndex = 1;
            lblAttendance.Text = "Attendance";
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(lblActiveEmployee);
            panel1.Location = new Point(224, 90);
            panel1.Name = "panel1";
            panel1.Size = new Size(186, 68);
            panel1.TabIndex = 24;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Dubai", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(22, 19);
            label1.Name = "label1";
            label1.Size = new Size(54, 49);
            label1.TabIndex = 2;
            label1.Text = "60";
            // 
            // lblActiveEmployee
            // 
            lblActiveEmployee.AutoSize = true;
            lblActiveEmployee.ForeColor = Color.Gray;
            lblActiveEmployee.Location = new Point(13, 0);
            lblActiveEmployee.Name = "lblActiveEmployee";
            lblActiveEmployee.Size = new Size(109, 19);
            lblActiveEmployee.TabIndex = 1;
            lblActiveEmployee.Text = "Active Employee";
            // 
            // pnlTotalEmployee
            // 
            pnlTotalEmployee.BorderStyle = BorderStyle.FixedSingle;
            pnlTotalEmployee.Controls.Add(lblTotalAmount);
            pnlTotalEmployee.Controls.Add(lblTotalemployee);
            pnlTotalEmployee.Location = new Point(12, 90);
            pnlTotalEmployee.Name = "pnlTotalEmployee";
            pnlTotalEmployee.Size = new Size(187, 68);
            pnlTotalEmployee.TabIndex = 23;
            // 
            // lblTotalAmount
            // 
            lblTotalAmount.AutoSize = true;
            lblTotalAmount.Font = new Font("Dubai", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalAmount.Location = new Point(15, 17);
            lblTotalAmount.Name = "lblTotalAmount";
            lblTotalAmount.Size = new Size(54, 49);
            lblTotalAmount.TabIndex = 1;
            lblTotalAmount.Text = "60";
            // 
            // lblTotalemployee
            // 
            lblTotalemployee.AutoSize = true;
            lblTotalemployee.ForeColor = Color.Gray;
            lblTotalemployee.Location = new Point(15, 0);
            lblTotalemployee.Name = "lblTotalemployee";
            lblTotalemployee.Size = new Size(101, 19);
            lblTotalemployee.TabIndex = 0;
            lblTotalemployee.Text = "Total Employee";
            // 
            // btnLast7days
            // 
            btnLast7days.FlatAppearance.BorderColor = Color.Gainsboro;
            btnLast7days.FlatStyle = FlatStyle.Flat;
            btnLast7days.Location = new Point(608, 16);
            btnLast7days.Name = "btnLast7days";
            btnLast7days.Size = new Size(103, 32);
            btnLast7days.TabIndex = 22;
            btnLast7days.Text = "Last 7 days";
            btnLast7days.UseVisualStyleBackColor = true;
            //btnLast7days.Click += this.btnLast7days_Click;
            // 
            // btnThisMonth
            // 
            btnThisMonth.FlatAppearance.BorderColor = Color.Gainsboro;
            btnThisMonth.FlatStyle = FlatStyle.Flat;
            btnThisMonth.Location = new Point(812, 16);
            btnThisMonth.Name = "btnThisMonth";
            btnThisMonth.Size = new Size(103, 32);
            btnThisMonth.TabIndex = 21;
            btnThisMonth.Text = "This month";
            btnThisMonth.UseVisualStyleBackColor = true;
            // 
            // btnLast30days
            // 
            btnLast30days.FlatAppearance.BorderColor = Color.Gainsboro;
            btnLast30days.FlatStyle = FlatStyle.Flat;
            btnLast30days.Location = new Point(710, 16);
            btnLast30days.Name = "btnLast30days";
            btnLast30days.Size = new Size(103, 32);
            btnLast30days.TabIndex = 20;
            btnLast30days.Text = "Last 30 days";
            btnLast30days.UseVisualStyleBackColor = true;
            //btnLast30days.Click += this.btnLast30days_Click;
            // 
            // btnToday
            // 
            btnToday.FlatAppearance.BorderColor = Color.Gainsboro;
            btnToday.FlatStyle = FlatStyle.Flat;
            btnToday.Location = new Point(506, 16);
            btnToday.Name = "btnToday";
            btnToday.Size = new Size(103, 32);
            btnToday.TabIndex = 19;
            btnToday.Text = "Today";
            btnToday.UseVisualStyleBackColor = true;
           // btnToday.Click += this.btnToday_Click;
            // 
            // dtpTo
            // 
            dtpTo.CustomFormat = "MMM dd, yyyy";
            dtpTo.Format = DateTimePickerFormat.Custom;
            dtpTo.Location = new Point(321, 19);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(129, 26);
            dtpTo.TabIndex = 18;
            //dtpTo.ValueChanged += this.dtpTo_ValueChanged;
            // 
            // dtpFrom
            // 
            dtpFrom.CustomFormat = "MMM dd, yyyy";
            dtpFrom.Format = DateTimePickerFormat.Custom;
            dtpFrom.Location = new Point(166, 19);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Size = new Size(135, 26);
            dtpFrom.TabIndex = 17;
            //dtpFrom.ValueChanged += this.dtpFrom_ValueChanged;
            // 
            // lblHeader
            // 
            lblHeader.AutoSize = true;
            lblHeader.Font = new Font("Segoe UI Historic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblHeader.Location = new Point(9, 12);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(138, 32);
            lblHeader.TabIndex = 16;
            lblHeader.Text = "Dashboard";
            //lblHeader.Click += this.lblHeader_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(12, 51);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(217, 27);
            comboBox1.TabIndex = 32;
           // comboBox1.SelectedIndexChanged += this.comboBox1_SelectedIndexChanged;
            // 
            // DashboardAdminForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(942, 546);
            Controls.Add(comboBox1);
            Controls.Add(panel6);
            Controls.Add(panel5);
            Controls.Add(cbxDepartments);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(pnlTotalEmployee);
            Controls.Add(btnLast7days);
            Controls.Add(btnThisMonth);
            Controls.Add(btnLast30days);
            Controls.Add(btnToday);
            Controls.Add(dtpTo);
            Controls.Add(dtpFrom);
            Controls.Add(lblHeader);
            Name = "DashboardAdminForm";
            Text = "DashboardAdminForm";
            //Load += this.DashboardAdminForm_Load;
            panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chartSalary).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            pnlTotalEmployee.ResumeLayout(false);
            pnlTotalEmployee.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel6;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private Panel panel5;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSalary;
        private ComboBox cbxDepartments;
        private Panel panel4;
        private Label label2;
        private Label lblTotalSalary;
        private Panel panel3;
        private Label lblPendingAmount;
        private Label lblLeaveRequestPending;
        private Panel panel2;
        private Label lblAmountAttendance;
        private Label lblAttendance;
        private Panel panel1;
        private Label label1;
        private Label lblActiveEmployee;
        private Panel pnlTotalEmployee;
        private Label lblTotalAmount;
        private Label lblTotalemployee;
        private Button btnLast7days;
        private Button btnThisMonth;
        private Button btnLast30days;
        private Button btnToday;
        private DateTimePicker dtpTo;
        private DateTimePicker dtpFrom;
        private Label lblHeader;
        private ComboBox comboBox1;
    }
}