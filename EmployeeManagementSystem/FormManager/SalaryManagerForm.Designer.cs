namespace EmployeeManagementSystem.FormManager
{
    partial class SalaryManagerForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblManagerInfo;
        private Label lblEmployeeLabel;
        private ComboBox cmbEmployee;
        private Label lblBonusLabel;
        private TextBox txtBonus;
        private Label lblMonthLabel;
        private DateTimePicker dtpMonth;
        private Button btnCalculate;
        private Button btnSave;
        private Button btnReset;
        private GroupBox gbResult;
        private Label lblEmployeeName;
        private Label lblMonth;
        private Label lblBaseSalary;
        private Label lblDailySalary;
        private Label lblTotalDeduction;
        private Label lblBonus;
        private Label lblTotalSalary;
        private DataGridView dgvDetails;
        private Label lblWarning;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            lblManagerInfo = new Label();
            lblEmployeeLabel = new Label();
            cmbEmployee = new ComboBox();
            lblBonusLabel = new Label();
            txtBonus = new TextBox();
            lblMonthLabel = new Label();
            dtpMonth = new DateTimePicker();
            btnCalculate = new Button();
            btnSave = new Button();
            btnReset = new Button();
            gbResult = new GroupBox();
            lblEmployeeName = new Label();
            lblMonth = new Label();
            lblBaseSalary = new Label();
            lblDailySalary = new Label();
            lblTotalDeduction = new Label();
            lblBonus = new Label();
            lblTotalSalary = new Label();
            dgvDetails = new DataGridView();
            lblWarning = new Label();
            gbResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDetails).BeginInit();
            SuspendLayout();
            // 
            // lblManagerInfo
            // 
            lblManagerInfo.Anchor = AnchorStyles.None;
            lblManagerInfo.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold);
            lblManagerInfo.ForeColor = Color.FromArgb(0, 123, 255);
            lblManagerInfo.Location = new Point(20, 31);
            lblManagerInfo.Name = "lblManagerInfo";
            lblManagerInfo.Size = new Size(800, 30);
            lblManagerInfo.TabIndex = 0;
            lblManagerInfo.Text = "Thông tin Manager";
            lblManagerInfo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblEmployeeLabel
            // 
            lblEmployeeLabel.Anchor = AnchorStyles.None;
            lblEmployeeLabel.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            lblEmployeeLabel.Location = new Point(21, 64);
            lblEmployeeLabel.Name = "lblEmployeeLabel";
            lblEmployeeLabel.Size = new Size(100, 25);
            lblEmployeeLabel.TabIndex = 1;
            lblEmployeeLabel.Text = "Nhân viên:";
            lblEmployeeLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbEmployee
            // 
            cmbEmployee.Anchor = AnchorStyles.None;
            cmbEmployee.BackColor = Color.White;
            cmbEmployee.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEmployee.Font = new Font("Microsoft Sans Serif", 9F);
            cmbEmployee.Location = new Point(131, 64);
            cmbEmployee.Name = "cmbEmployee";
            cmbEmployee.Size = new Size(250, 23);
            cmbEmployee.TabIndex = 2;
            // 
            // lblBonusLabel
            // 
            lblBonusLabel.Anchor = AnchorStyles.None;
            lblBonusLabel.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            lblBonusLabel.Location = new Point(401, 64);
            lblBonusLabel.Name = "lblBonusLabel";
            lblBonusLabel.Size = new Size(60, 25);
            lblBonusLabel.TabIndex = 3;
            lblBonusLabel.Text = "Bonus:";
            lblBonusLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtBonus
            // 
            txtBonus.Anchor = AnchorStyles.None;
            txtBonus.BackColor = Color.White;
            txtBonus.BorderStyle = BorderStyle.FixedSingle;
            txtBonus.Font = new Font("Microsoft Sans Serif", 9F);
            txtBonus.Location = new Point(471, 64);
            txtBonus.Name = "txtBonus";
            txtBonus.Size = new Size(120, 21);
            txtBonus.TabIndex = 4;
            txtBonus.Text = "0";
            txtBonus.TextAlign = HorizontalAlignment.Right;
            // 
            // lblMonthLabel
            // 
            lblMonthLabel.Anchor = AnchorStyles.None;
            lblMonthLabel.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            lblMonthLabel.Location = new Point(597, 63);
            lblMonthLabel.Name = "lblMonthLabel";
            lblMonthLabel.Size = new Size(68, 25);
            lblMonthLabel.TabIndex = 5;
            lblMonthLabel.Text = "Tháng:";
            lblMonthLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dtpMonth
            // 
            dtpMonth.Anchor = AnchorStyles.None;
            dtpMonth.CustomFormat = "MM/yyyy";
            dtpMonth.Font = new Font("Microsoft Sans Serif", 9F);
            dtpMonth.Format = DateTimePickerFormat.Custom;
            dtpMonth.Location = new Point(671, 64);
            dtpMonth.Name = "dtpMonth";
            dtpMonth.Size = new Size(100, 21);
            dtpMonth.TabIndex = 6;
            dtpMonth.Value = new DateTime(2025, 5, 28, 8, 16, 8, 381);
            // 
            // btnCalculate
            // 
            btnCalculate.Anchor = AnchorStyles.None;
            btnCalculate.BackColor = Color.FromArgb(0, 123, 255);
            btnCalculate.Cursor = Cursors.Hand;
            btnCalculate.FlatStyle = FlatStyle.Flat;
            btnCalculate.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            btnCalculate.ForeColor = Color.White;
            btnCalculate.Location = new Point(440, 100);
            btnCalculate.Name = "btnCalculate";
            btnCalculate.Size = new Size(100, 30);
            btnCalculate.TabIndex = 7;
            btnCalculate.Text = "Tính Lương";
            btnCalculate.UseVisualStyleBackColor = false;
            btnCalculate.Click += btnCalculate_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.None;
            btnSave.BackColor = Color.FromArgb(40, 167, 69);
            btnSave.Cursor = Cursors.Hand;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(560, 100);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 30);
            btnSave.TabIndex = 8;
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnReset
            // 
            btnReset.Anchor = AnchorStyles.None;
            btnReset.BackColor = Color.FromArgb(220, 53, 69);
            btnReset.Cursor = Cursors.Hand;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            btnReset.ForeColor = Color.White;
            btnReset.Location = new Point(680, 100);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(100, 30);
            btnReset.TabIndex = 9;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = false;
            btnReset.Click += btnReset_Click;
            // 
            // gbResult
            // 
            gbResult.Anchor = AnchorStyles.None;
            gbResult.BackColor = Color.FromArgb(248, 249, 250);
            gbResult.Controls.Add(lblEmployeeName);
            gbResult.Controls.Add(lblMonth);
            gbResult.Controls.Add(lblBaseSalary);
            gbResult.Controls.Add(lblDailySalary);
            gbResult.Controls.Add(lblTotalDeduction);
            gbResult.Controls.Add(lblBonus);
            gbResult.Controls.Add(lblTotalSalary);
            gbResult.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            gbResult.ForeColor = Color.FromArgb(0, 123, 255);
            gbResult.Location = new Point(21, 133);
            gbResult.Name = "gbResult";
            gbResult.Size = new Size(759, 134);
            gbResult.TabIndex = 11;
            gbResult.TabStop = false;
            gbResult.Text = "Kết Quả Tính Lương";
            gbResult.Enter += gbResult_Enter;
            // 
            // lblEmployeeName
            // 
            lblEmployeeName.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            lblEmployeeName.ForeColor = Color.Black;
            lblEmployeeName.Location = new Point(20, 25);
            lblEmployeeName.Name = "lblEmployeeName";
            lblEmployeeName.Size = new Size(280, 20);
            lblEmployeeName.TabIndex = 0;
            lblEmployeeName.Text = "Nhân viên: ";
            // 
            // lblMonth
            // 
            lblMonth.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            lblMonth.ForeColor = Color.Black;
            lblMonth.Location = new Point(320, 25);
            lblMonth.Name = "lblMonth";
            lblMonth.Size = new Size(150, 20);
            lblMonth.TabIndex = 1;
            lblMonth.Text = "Tháng: ";
            // 
            // lblBaseSalary
            // 
            lblBaseSalary.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            lblBaseSalary.ForeColor = Color.Black;
            lblBaseSalary.Location = new Point(20, 50);
            lblBaseSalary.Name = "lblBaseSalary";
            lblBaseSalary.Size = new Size(280, 20);
            lblBaseSalary.TabIndex = 2;
            lblBaseSalary.Text = "Lương cơ bản: ";
            // 
            // lblDailySalary
            // 
            lblDailySalary.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            lblDailySalary.ForeColor = Color.Black;
            lblDailySalary.Location = new Point(320, 50);
            lblDailySalary.Name = "lblDailySalary";
            lblDailySalary.Size = new Size(280, 20);
            lblDailySalary.TabIndex = 3;
            lblDailySalary.Text = "Lương ngày: ";
            // 
            // lblTotalDeduction
            // 
            lblTotalDeduction.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            lblTotalDeduction.ForeColor = Color.Red;
            lblTotalDeduction.Location = new Point(20, 75);
            lblTotalDeduction.Name = "lblTotalDeduction";
            lblTotalDeduction.Size = new Size(280, 20);
            lblTotalDeduction.TabIndex = 4;
            lblTotalDeduction.Text = "Tổng khấu trừ: ";
            // 
            // lblBonus
            // 
            lblBonus.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            lblBonus.ForeColor = Color.Blue;
            lblBonus.Location = new Point(320, 75);
            lblBonus.Name = "lblBonus";
            lblBonus.Size = new Size(280, 20);
            lblBonus.TabIndex = 5;
            lblBonus.Text = "Bonus: ";
            // 
            // lblTotalSalary
            // 
            lblTotalSalary.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            lblTotalSalary.ForeColor = Color.Green;
            lblTotalSalary.Location = new Point(20, 100);
            lblTotalSalary.Name = "lblTotalSalary";
            lblTotalSalary.Size = new Size(580, 25);
            lblTotalSalary.TabIndex = 6;
            lblTotalSalary.Text = "Lương thực nhận: ";
            // 
            // dgvDetails
            // 
            dgvDetails.AllowUserToAddRows = false;
            dgvDetails.AllowUserToDeleteRows = false;
            dgvDetails.AllowUserToResizeRows = false;
            dgvDetails.Anchor = AnchorStyles.None;
            dgvDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDetails.BackgroundColor = Color.White;
            dgvDetails.BorderStyle = BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(0, 123, 255);
            dataGridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvDetails.ColumnHeadersHeight = 35;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(0, 123, 255);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvDetails.DefaultCellStyle = dataGridViewCellStyle2;
            dgvDetails.EnableHeadersVisualStyles = false;
            dgvDetails.GridColor = Color.LightGray;
            dgvDetails.Location = new Point(21, 273);
            dgvDetails.MultiSelect = false;
            dgvDetails.Name = "dgvDetails";
            dgvDetails.ReadOnly = true;
            dgvDetails.RowHeadersVisible = false;
            dgvDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetails.Size = new Size(759, 249);
            dgvDetails.TabIndex = 12;
            // 
            // lblWarning
            // 
            lblWarning.Anchor = AnchorStyles.None;
            lblWarning.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Italic);
            lblWarning.ForeColor = Color.Red;
            lblWarning.Location = new Point(20, 90);
            lblWarning.Name = "lblWarning";
            lblWarning.Size = new Size(414, 40);
            lblWarning.TabIndex = 10;
            lblWarning.Text = "⚠️ Cảnh báo: Theo pháp luật Việt Nam, việc trừ lương do đi trễ/về sớm là bị cấm và có thể bị phạt 20-80 triệu đồng.\nCode này chỉ để demo kỹ thuật.";
            lblWarning.Click += lblWarning_Click;
            // 
            // SalaryManagerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(892, 540);
            Controls.Add(lblManagerInfo);
            Controls.Add(lblEmployeeLabel);
            Controls.Add(cmbEmployee);
            Controls.Add(lblBonusLabel);
            Controls.Add(txtBonus);
            Controls.Add(lblMonthLabel);
            Controls.Add(dtpMonth);
            Controls.Add(btnCalculate);
            Controls.Add(btnSave);
            Controls.Add(btnReset);
            Controls.Add(lblWarning);
            Controls.Add(gbResult);
            Controls.Add(dgvDetails);
            Font = new Font("Microsoft Sans Serif", 9F);
            Name = "SalaryManagerForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản Lý Lương Nhân Viên";
            Load += SalaryManagerForm_Load;
            gbResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDetails).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
