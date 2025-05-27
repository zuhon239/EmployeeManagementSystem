namespace EmployeeManagementSystem.FormManager
{
    partial class SalaryManagerForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblManagerInfo;
        private Label lblEmployeeLabel;
        private ComboBox cmbEmployee;
        private Label lblBaseSalaryLabel;
        private TextBox txtBaseSalary;
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
            lblManagerInfo = new Label();
            lblEmployeeLabel = new Label();
            cmbEmployee = new ComboBox();
            lblBaseSalaryLabel = new Label();
            txtBaseSalary = new TextBox();
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
            lblTotalSalary = new Label();
            dgvDetails = new DataGridView();
            lblWarning = new Label();

            ((System.ComponentModel.ISupportInitialize)dgvDetails).BeginInit();
            gbResult.SuspendLayout();
            SuspendLayout();

            // Form
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 800);
            Name = "SalaryManagerForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản Lý Lương Nhân Viên";
            Load += SalaryManagerForm_Load;

            // lblManagerInfo
            lblManagerInfo.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold);
            lblManagerInfo.Location = new Point(20, 20);
            lblManagerInfo.Size = new Size(800, 30);
            lblManagerInfo.Text = "Thông tin Manager";

            // lblEmployeeLabel
            lblEmployeeLabel.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            lblEmployeeLabel.Location = new Point(20, 70);
            lblEmployeeLabel.Size = new Size(100, 25);
            lblEmployeeLabel.Text = "Nhân viên:";

            // cmbEmployee
            cmbEmployee.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEmployee.Location = new Point(130, 70);
            cmbEmployee.Size = new Size(250, 27);

            // lblBaseSalaryLabel
            lblBaseSalaryLabel.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            lblBaseSalaryLabel.Location = new Point(400, 70);
            lblBaseSalaryLabel.Size = new Size(100, 25);
            lblBaseSalaryLabel.Text = "Lương cơ bản:";

            // txtBaseSalary
            txtBaseSalary.Location = new Point(510, 70);
            txtBaseSalary.Size = new Size(120, 26);
            txtBaseSalary.Text = "3000000";

            // lblMonthLabel
            lblMonthLabel.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            lblMonthLabel.Location = new Point(650, 70);
            lblMonthLabel.Size = new Size(50, 25);
            lblMonthLabel.Text = "Tháng:";

            // dtpMonth
            dtpMonth.Format = DateTimePickerFormat.Custom;
            dtpMonth.CustomFormat = "MM/yyyy";
            dtpMonth.Location = new Point(710, 70);
            dtpMonth.Size = new Size(100, 26);
            dtpMonth.Value = DateTime.Now;

            // btnCalculate
            btnCalculate.BackColor = Color.FromArgb(0, 123, 255);
            btnCalculate.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            btnCalculate.ForeColor = Color.White;
            btnCalculate.Location = new Point(830, 68);
            btnCalculate.Size = new Size(100, 30);
            btnCalculate.Text = "Tính Lương";
            btnCalculate.UseVisualStyleBackColor = false;
            btnCalculate.Click += btnCalculate_Click;

            // btnSave
            btnSave.BackColor = Color.FromArgb(40, 167, 69);
            btnSave.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(950, 68);
            btnSave.Size = new Size(100, 30);
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;

            // btnReset
            btnReset.BackColor = Color.FromArgb(220, 53, 69);
            btnReset.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            btnReset.ForeColor = Color.White;
            btnReset.Location = new Point(1070, 68);
            btnReset.Size = new Size(100, 30);
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = false;
            btnReset.Click += btnReset_Click;

            // lblWarning
            lblWarning.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Italic);
            lblWarning.ForeColor = Color.Red;
            lblWarning.Location = new Point(20, 110);
            lblWarning.Size = new Size(800, 40);
            lblWarning.Text = "⚠️ Cảnh báo: Theo pháp luật Việt Nam, việc trừ lương do đi trễ/về sớm là bị cấm và có thể bị phạt 20-80 triệu đồng.\nCode này chỉ để demo kỹ thuật.";

            // gbResult
            gbResult.Text = "Kết Quả Tính Lương";
            gbResult.Location = new Point(20, 160);
            gbResult.Size = new Size(1160, 150);

            // lblEmployeeName
            lblEmployeeName.Location = new Point(20, 25);
            lblEmployeeName.Size = new Size(250, 20);
            lblEmployeeName.Text = "Nhân viên: ";
            lblEmployeeName.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);

            // lblMonth
            lblMonth.Location = new Point(300, 25);
            lblMonth.Size = new Size(150, 20);
            lblMonth.Text = "Tháng: ";
            lblMonth.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);

            // lblBaseSalary
            lblBaseSalary.Location = new Point(20, 50);
            lblBaseSalary.Size = new Size(200, 20);
            lblBaseSalary.Text = "Lương cơ bản: ";
            lblBaseSalary.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);

            // lblDailySalary
            lblDailySalary.Location = new Point(250, 50);
            lblDailySalary.Size = new Size(200, 20);
            lblDailySalary.Text = "Lương ngày: ";
            lblDailySalary.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);

            // lblTotalDeduction
            lblTotalDeduction.Location = new Point(20, 75);
            lblTotalDeduction.Size = new Size(200, 20);
            lblTotalDeduction.Text = "Tổng khấu trừ: ";
            lblTotalDeduction.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            lblTotalDeduction.ForeColor = Color.Red;

            // lblTotalSalary
            lblTotalSalary.Location = new Point(250, 75);
            lblTotalSalary.Size = new Size(200, 20);
            lblTotalSalary.Text = "Lương thực nhận: ";
            lblTotalSalary.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            lblTotalSalary.ForeColor = Color.Green;

            gbResult.Controls.AddRange(new Control[] {
                lblEmployeeName, lblMonth, lblBaseSalary, lblDailySalary,
                lblTotalDeduction, lblTotalSalary
            });

            // dgvDetails
            dgvDetails.AllowUserToAddRows = false;
            dgvDetails.AllowUserToDeleteRows = false;
            dgvDetails.Location = new Point(20, 330);
            dgvDetails.ReadOnly = true;
            dgvDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetails.Size = new Size(1160, 450);
            dgvDetails.ColumnHeadersHeight = 30;
            dgvDetails.ScrollBars = ScrollBars.Both;

            // Add controls to form
            Controls.AddRange(new Control[] {
                lblManagerInfo, lblEmployeeLabel, cmbEmployee,
                lblBaseSalaryLabel, txtBaseSalary, lblMonthLabel, dtpMonth,
                btnCalculate, btnSave, btnReset, lblWarning,
                gbResult, dgvDetails
            });

            ((System.ComponentModel.ISupportInitialize)dgvDetails).EndInit();
            gbResult.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
