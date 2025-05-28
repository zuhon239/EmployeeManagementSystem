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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
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
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartSalary).BeginInit();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            pnlTotalEmployee.SuspendLayout();
            SuspendLayout();
            // 
            // panel6
            // 
            panel6.Anchor = AnchorStyles.None;
            panel6.BorderStyle = BorderStyle.FixedSingle;
            panel6.Controls.Add(chart1);
            panel6.Location = new Point(545, 171);
            panel6.Name = "panel6";
            panel6.Size = new Size(398, 341);
            panel6.TabIndex = 31;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);
            chart1.Location = new Point(7, 6);
            chart1.Name = "chart1";
            chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chart1.Series.Add(series1);
            chart1.Size = new Size(350, 294);
            chart1.TabIndex = 12;
            chart1.Text = "Salary";
            // 
            // panel5
            // 
            panel5.Anchor = AnchorStyles.None;
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel5.Controls.Add(chartSalary);
            panel5.Location = new Point(40, 171);
            panel5.Name = "panel5";
            panel5.Size = new Size(499, 341);
            panel5.TabIndex = 30;
            // 
            // chartSalary
            // 
            chartSalary.BorderlineColor = Color.Black;
            chartSalary.BorderSkin.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.LeftRight;
            chartArea2.Name = "ChartArea1";
            chartSalary.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            chartSalary.Legends.Add(legend2);
            chartSalary.Location = new Point(3, 8);
            chartSalary.Name = "chartSalary";
            chartSalary.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            chartSalary.Series.Add(series2);
            chartSalary.Size = new Size(491, 322);
            chartSalary.TabIndex = 11;
            chartSalary.Text = "Salary";
            title1.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            title1.Name = "Salary";
            chartSalary.Titles.Add(title1);
            // 
            // cbxDepartments
            // 
            cbxDepartments.Anchor = AnchorStyles.None;
            cbxDepartments.FormattingEnabled = true;
            cbxDepartments.Location = new Point(40, 57);
            cbxDepartments.Name = "cbxDepartments";
            cbxDepartments.Size = new Size(179, 27);
            cbxDepartments.TabIndex = 29;
            // 
            // panel4
            // 
            panel4.Anchor = AnchorStyles.None;
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(label2);
            panel4.Controls.Add(lblTotalSalary);
            panel4.Location = new Point(683, 90);
            panel4.Name = "panel4";
            panel4.Size = new Size(275, 70);
            panel4.TabIndex = 28;
            // 
            // label2
            // 
            label2.Font = new Font("Dubai", 20.2499981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(14, 23);
            label2.Name = "label2";
            label2.Size = new Size(256, 38);
            label2.TabIndex = 2;
            label2.Text = "60";
            // 
            // lblTotalSalary
            // 
            lblTotalSalary.AutoSize = true;
            lblTotalSalary.ForeColor = Color.Gray;
            lblTotalSalary.Location = new Point(14, 3);
            lblTotalSalary.Name = "lblTotalSalary";
            lblTotalSalary.Size = new Size(79, 19);
            lblTotalSalary.TabIndex = 1;
            lblTotalSalary.Text = "Tổng lương";
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.None;
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(lblPendingAmount);
            panel3.Controls.Add(lblLeaveRequestPending);
            panel3.Location = new Point(456, 91);
            panel3.Name = "panel3";
            panel3.Size = new Size(211, 68);
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
            lblLeaveRequestPending.Size = new Size(166, 19);
            lblLeaveRequestPending.TabIndex = 1;
            lblLeaveRequestPending.Text = "Xin nghỉ phép chưa duyệt";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.None;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(lblActiveEmployee);
            panel1.Location = new Point(252, 90);
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
            lblActiveEmployee.Size = new Size(102, 19);
            lblActiveEmployee.TabIndex = 1;
            lblActiveEmployee.Text = "Còn hoạt động";
            // 
            // pnlTotalEmployee
            // 
            pnlTotalEmployee.Anchor = AnchorStyles.None;
            pnlTotalEmployee.BorderStyle = BorderStyle.FixedSingle;
            pnlTotalEmployee.Controls.Add(lblTotalAmount);
            pnlTotalEmployee.Controls.Add(lblTotalemployee);
            pnlTotalEmployee.Location = new Point(40, 90);
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
            lblTotalemployee.Size = new Size(104, 19);
            lblTotalemployee.TabIndex = 0;
            lblTotalemployee.Text = "Tổng nhân viên";
            // 
            // btnLast7days
            // 
            btnLast7days.Anchor = AnchorStyles.None;
            btnLast7days.FlatAppearance.BorderColor = Color.Gainsboro;
            btnLast7days.FlatStyle = FlatStyle.Flat;
            btnLast7days.Location = new Point(636, 16);
            btnLast7days.Name = "btnLast7days";
            btnLast7days.Size = new Size(103, 32);
            btnLast7days.TabIndex = 22;
            btnLast7days.Text = "Last 7 days";
            btnLast7days.UseVisualStyleBackColor = true;
            // 
            // btnThisMonth
            // 
            btnThisMonth.Anchor = AnchorStyles.None;
            btnThisMonth.FlatAppearance.BorderColor = Color.Gainsboro;
            btnThisMonth.FlatStyle = FlatStyle.Flat;
            btnThisMonth.Location = new Point(840, 16);
            btnThisMonth.Name = "btnThisMonth";
            btnThisMonth.Size = new Size(103, 32);
            btnThisMonth.TabIndex = 21;
            btnThisMonth.Text = "This month";
            btnThisMonth.UseVisualStyleBackColor = true;
            // 
            // btnLast30days
            // 
            btnLast30days.Anchor = AnchorStyles.None;
            btnLast30days.FlatAppearance.BorderColor = Color.Gainsboro;
            btnLast30days.FlatStyle = FlatStyle.Flat;
            btnLast30days.Location = new Point(738, 16);
            btnLast30days.Name = "btnLast30days";
            btnLast30days.Size = new Size(103, 32);
            btnLast30days.TabIndex = 20;
            btnLast30days.Text = "Last 30 days";
            btnLast30days.UseVisualStyleBackColor = true;
            // 
            // btnToday
            // 
            btnToday.Anchor = AnchorStyles.None;
            btnToday.FlatAppearance.BorderColor = Color.Gainsboro;
            btnToday.FlatStyle = FlatStyle.Flat;
            btnToday.Location = new Point(534, 16);
            btnToday.Name = "btnToday";
            btnToday.Size = new Size(103, 32);
            btnToday.TabIndex = 19;
            btnToday.Text = "Today";
            btnToday.UseVisualStyleBackColor = true;
            // 
            // dtpTo
            // 
            dtpTo.Anchor = AnchorStyles.None;
            dtpTo.CustomFormat = "MMM dd, yyyy";
            dtpTo.Format = DateTimePickerFormat.Custom;
            dtpTo.Location = new Point(349, 19);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(129, 26);
            dtpTo.TabIndex = 18;
            // 
            // dtpFrom
            // 
            dtpFrom.Anchor = AnchorStyles.None;
            dtpFrom.CustomFormat = "MMM dd, yyyy";
            dtpFrom.Format = DateTimePickerFormat.Custom;
            dtpFrom.Location = new Point(194, 19);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Size = new Size(135, 26);
            dtpFrom.TabIndex = 17;
            // 
            // lblHeader
            // 
            lblHeader.Anchor = AnchorStyles.None;
            lblHeader.AutoSize = true;
            lblHeader.Font = new Font("Segoe UI Historic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblHeader.Location = new Point(37, 12);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(138, 32);
            lblHeader.TabIndex = 16;
            lblHeader.Text = "Dashboard";
            // 
            // DashboardAdminForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(979, 546);
            Controls.Add(panel6);
            Controls.Add(panel5);
            Controls.Add(cbxDepartments);
            Controls.Add(panel4);
            Controls.Add(panel3);
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
            panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chartSalary).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
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
    }
}