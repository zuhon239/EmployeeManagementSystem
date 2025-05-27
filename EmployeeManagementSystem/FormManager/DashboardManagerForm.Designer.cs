namespace EmployeeManagementSystem.FormManager
{
    partial class DashboardManagerForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            lblHeader = new Label();
            dtpFrom = new DateTimePicker();
            dtpTo = new DateTimePicker();
            btnToday = new Button();
            btnLast30days = new Button();
            btnThisMonth = new Button();
            btnLast7days = new Button();
            pnlTotalEmployee = new Panel();
            lblTotalAmount = new Label();
            lblTotalemployee = new Label();
            lblDepartmentName = new Label();
            panel1 = new Panel();
            label1 = new Label();
            lblActiveEmployee = new Label();
            panel2 = new Panel();
            lblAmountAttendance = new Label();
            lblAttendance = new Label();
            panel3 = new Panel();
            lblPendingAmount = new Label();
            lblLeaveRequestPending = new Label();
            chartSalary = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            panel4 = new Panel();
            label2 = new Label();
            lblTotalSalary = new Label();
            pnlTotalEmployee.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartSalary).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // lblHeader
            // 
            lblHeader.AutoSize = true;
            lblHeader.Font = new Font("Segoe UI Historic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblHeader.Location = new Point(12, 9);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(138, 32);
            lblHeader.TabIndex = 0;
            lblHeader.Text = "Dashboard";
            // 
            // dtpFrom
            // 
            dtpFrom.CustomFormat = "MMM dd, yyyy";
            dtpFrom.Format = DateTimePickerFormat.Custom;
            dtpFrom.Location = new Point(169, 16);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Size = new Size(135, 26);
            dtpFrom.TabIndex = 1;
            // 
            // dtpTo
            // 
            dtpTo.CustomFormat = "MMM dd, yyyy";
            dtpTo.Format = DateTimePickerFormat.Custom;
            dtpTo.Location = new Point(324, 16);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(129, 26);
            dtpTo.TabIndex = 2;
            // 
            // btnToday
            // 
            btnToday.Location = new Point(486, 9);
            btnToday.Name = "btnToday";
            btnToday.Size = new Size(103, 34);
            btnToday.TabIndex = 3;
            btnToday.Text = "Today";
            btnToday.UseVisualStyleBackColor = true;
            // 
            // btnLast30days
            // 
            btnLast30days.Location = new Point(706, 9);
            btnLast30days.Name = "btnLast30days";
            btnLast30days.Size = new Size(103, 33);
            btnLast30days.TabIndex = 4;
            btnLast30days.Text = "Last 30 days";
            btnLast30days.UseVisualStyleBackColor = true;
            // 
            // btnThisMonth
            // 
            btnThisMonth.Location = new Point(815, 9);
            btnThisMonth.Name = "btnThisMonth";
            btnThisMonth.Size = new Size(103, 32);
            btnThisMonth.TabIndex = 5;
            btnThisMonth.Text = "This month";
            btnThisMonth.UseVisualStyleBackColor = true;
            // 
            // btnLast7days
            // 
            btnLast7days.Location = new Point(595, 9);
            btnLast7days.Name = "btnLast7days";
            btnLast7days.Size = new Size(103, 33);
            btnLast7days.TabIndex = 6;
            btnLast7days.Text = "Last 7 days";
            btnLast7days.UseVisualStyleBackColor = true;
            // 
            // pnlTotalEmployee
            // 
            pnlTotalEmployee.BorderStyle = BorderStyle.FixedSingle;
            pnlTotalEmployee.Controls.Add(lblTotalAmount);
            pnlTotalEmployee.Controls.Add(lblTotalemployee);
            pnlTotalEmployee.Location = new Point(15, 72);
            pnlTotalEmployee.Name = "pnlTotalEmployee";
            pnlTotalEmployee.Size = new Size(187, 68);
            pnlTotalEmployee.TabIndex = 7;
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
            lblTotalemployee.Location = new Point(15, 0);
            lblTotalemployee.Name = "lblTotalemployee";
            lblTotalemployee.Size = new Size(101, 19);
            lblTotalemployee.TabIndex = 0;
            lblTotalemployee.Text = "Total Employee";
            // 
            // lblDepartmentName
            // 
            lblDepartmentName.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDepartmentName.Location = new Point(15, 45);
            lblDepartmentName.Name = "lblDepartmentName";
            lblDepartmentName.Size = new Size(421, 23);
            lblDepartmentName.TabIndex = 8;
            lblDepartmentName.Text = "DepartmentName";
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(lblActiveEmployee);
            panel1.Location = new Point(227, 72);
            panel1.Name = "panel1";
            panel1.Size = new Size(186, 68);
            panel1.TabIndex = 8;
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
            lblActiveEmployee.Location = new Point(13, 0);
            lblActiveEmployee.Name = "lblActiveEmployee";
            lblActiveEmployee.Size = new Size(109, 19);
            lblActiveEmployee.TabIndex = 1;
            lblActiveEmployee.Text = "Active Employee";
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(lblAmountAttendance);
            panel2.Controls.Add(lblAttendance);
            panel2.Location = new Point(437, 72);
            panel2.Name = "panel2";
            panel2.Size = new Size(186, 68);
            panel2.TabIndex = 9;
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
            lblAttendance.Location = new Point(13, 0);
            lblAttendance.Name = "lblAttendance";
            lblAttendance.Size = new Size(79, 19);
            lblAttendance.TabIndex = 1;
            lblAttendance.Text = "Attendance";
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(lblPendingAmount);
            panel3.Controls.Add(lblLeaveRequestPending);
            panel3.Location = new Point(650, 72);
            panel3.Name = "panel3";
            panel3.Size = new Size(268, 68);
            panel3.TabIndex = 10;
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
            lblLeaveRequestPending.Location = new Point(-1, 0);
            lblLeaveRequestPending.Name = "lblLeaveRequestPending";
            lblLeaveRequestPending.Size = new Size(205, 19);
            lblLeaveRequestPending.TabIndex = 1;
            lblLeaveRequestPending.Text = "Leave request pending approval";
            // 
            // chartSalary
            // 
            chartSalary.BorderlineColor = Color.Black;
            chartSalary.BorderSkin.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.LeftRight;
            chartArea1.Name = "ChartArea1";
            chartSalary.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chartSalary.Legends.Add(legend1);
            chartSalary.Location = new Point(-2, 163);
            chartSalary.Name = "chartSalary";
            chartSalary.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chartSalary.Series.Add(series1);
            chartSalary.Size = new Size(561, 239);
            chartSalary.TabIndex = 11;
            chartSalary.Text = "Salary";
            title1.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            title1.Name = "Salary";
            chartSalary.Titles.Add(title1);
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            chart1.Legends.Add(legend2);
            chart1.Location = new Point(565, 163);
            chart1.Name = "chart1";
            chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            chart1.Series.Add(series2);
            chart1.Size = new Size(358, 327);
            chart1.TabIndex = 12;
            chart1.Text = "Salary";
            // 
            // panel4
            // 
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(label2);
            panel4.Controls.Add(lblTotalSalary);
            panel4.Location = new Point(15, 408);
            panel4.Name = "panel4";
            panel4.Size = new Size(421, 68);
            panel4.TabIndex = 11;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Dubai", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(22, 19);
            label2.Name = "label2";
            label2.Size = new Size(54, 49);
            label2.TabIndex = 2;
            label2.Text = "60";
            // 
            // lblTotalSalary
            // 
            lblTotalSalary.AutoSize = true;
            lblTotalSalary.Location = new Point(15, 0);
            lblTotalSalary.Name = "lblTotalSalary";
            lblTotalSalary.Size = new Size(74, 19);
            lblTotalSalary.TabIndex = 1;
            lblTotalSalary.Text = "TotalSalary";
            lblTotalSalary.Click += lblTotalSalary_Click;
            // 
            // DashboardManagerForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(935, 518);
            Controls.Add(panel4);
            Controls.Add(chart1);
            Controls.Add(chartSalary);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(lblDepartmentName);
            Controls.Add(pnlTotalEmployee);
            Controls.Add(btnLast7days);
            Controls.Add(btnThisMonth);
            Controls.Add(btnLast30days);
            Controls.Add(btnToday);
            Controls.Add(dtpTo);
            Controls.Add(dtpFrom);
            Controls.Add(lblHeader);
            Name = "DashboardManagerForm";
            Text = "DashboardManagerForm";
            pnlTotalEmployee.ResumeLayout(false);
            pnlTotalEmployee.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chartSalary).EndInit();
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblHeader;
        private DateTimePicker dtpFrom;
        private DateTimePicker dtpTo;
        private Button btnToday;
        private Button btnLastWeek;
        private Button btnThisMonth;
        private Button btnLast30days;
        private Button btnLast7days;
        private Panel pnlTotalEmployee;
        private Label lblTotalemployee;
        private Label lblDepartmentName;
        private Panel panel1;
        private Label lblActiveEmployee;
        private Label lblTotalAmount;
        private Label label1;
        private Panel panel2;
        private Label lblAmountAttendance;
        private Label lblAttendance;
        private Panel panel3;
        private Label lblPendingAmount;
        private Label lblLeaveRequestPending;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSalary;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private Panel panel4;
        private Label label2;
        private Label lblTotalSalary;
    }
}