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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
       private void InitializeComponent()
        {
            lblStartDate = new Label { Text = "Ngày bắt đầu:", Location = new System.Drawing.Point(20, 20), Size = new System.Drawing.Size(100, 20) };
            dtpStartDate = new DateTimePicker { Location = new System.Drawing.Point(120, 20), Width = 200 };
            lblEndDate = new Label { Text = "Ngày kết thúc:", Location = new System.Drawing.Point(20, 50), Size = new System.Drawing.Size(100, 20) };
            dtpEndDate = new DateTimePicker { Location = new System.Drawing.Point(120, 50), Width = 200 };
            lblReason = new Label { Text = "Lý do:", Location = new System.Drawing.Point(20, 80), Size = new System.Drawing.Size(100, 20) };
            txtReason = new TextBox { Location = new System.Drawing.Point(120, 80), Size = new System.Drawing.Size(200, 60), Multiline = true };
            lblShift = new Label { Text = "Ca nghỉ:", Location = new System.Drawing.Point(20, 150), Size = new System.Drawing.Size(100, 20) };
            cmbShift = new ComboBox { Location = new System.Drawing.Point(120, 150), Width = 200 };
            btnSubmit = new Button { Text = "Gửi yêu cầu", Location = new System.Drawing.Point(120, 200), Width = 100 };
            btnCancel = new Button { Text = "Hủy", Location = new System.Drawing.Point(230, 200), Width = 100 };

            pnlShift = new Panel { Location = new System.Drawing.Point(20, 150), Size = new System.Drawing.Size(300, 30) };
            pnlShift.Controls.Add(lblShift);
            pnlShift.Controls.Add(cmbShift);

            Controls.AddRange(new Control[] { lblStartDate, dtpStartDate, lblEndDate, dtpEndDate, lblReason, txtReason, pnlShift, btnSubmit, btnCancel });

            dtpStartDate.ValueChanged += (s, e) => UpdateShiftVisibility();
            dtpEndDate.ValueChanged += (s, e) => UpdateShiftVisibility();
            btnSubmit.Click += BtnSubmit_Click;
            btnCancel.Click += (s, e) => Close();

            Text = "Xin Nghỉ Phép";
            Size = new System.Drawing.Size(400, 300);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }

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
        #endregion
    }
}