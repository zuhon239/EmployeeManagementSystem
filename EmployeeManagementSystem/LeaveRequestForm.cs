using EmployeeManagementSystem.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagementSystem
{
    public partial class LeaveRequestForm : Form
    {
        private readonly int _userId; 
        private readonly LeaveRequestController _leaveRequestController;
        public LeaveRequestForm(int userId, LeaveRequestController leaveRequestController)
        {
            InitializeComponent();
            _userId = userId;
            _leaveRequestController = leaveRequestController ?? throw new ArgumentNullException(nameof(leaveRequestController));

            // Thiết lập ComboBox cho Shift
            cmbShift.Items.AddRange(new[] { "Morning", "Afternoon", "FullDay" });
            UpdateShiftVisibility();
        }
        private void UpdateShiftVisibility()
        {
            bool isSingleDay = dtpStartDate.Value.Date == dtpEndDate.Value.Date;
            // Giữ pnlShift luôn hiển thị, chỉ vô hiệu hóa cmbShift
            cmbShift.Enabled = isSingleDay;
            if (!isSingleDay)
                cmbShift.SelectedIndex = -1;


            pnlShift.Refresh();
            System.Diagnostics.Debug.WriteLine($"UpdateShiftVisibility: isSingleDay={isSingleDay}, cmbShift.Enabled={cmbShift.Enabled}");
        }
        private async void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtReason.Text))
                {
                    MessageBox.Show("Vui lòng nhập lý do.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool isSingleDay = dtpStartDate.Value.Date == dtpEndDate.Value.Date;
                if (isSingleDay && cmbShift.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn ca nghỉ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                System.Diagnostics.Debug.WriteLine($"Gửi yêu cầu nghỉ phép: UserId={_userId}, " +
                $"StartDate={dtpStartDate.Value}, EndDate={dtpEndDate.Value}," +
                $" Shift={(isSingleDay ? cmbShift.SelectedItem?.ToString() : null)}, Reason={txtReason.Text}");
                await _leaveRequestController.CreateLeaveRequestAsync(
                    _userId,
                    dtpStartDate.Value,
                    dtpEndDate.Value,
                    txtReason.Text,
                    isSingleDay ? cmbShift.SelectedItem?.ToString() : null
                );

                MessageBox.Show("Yêu cầu nghỉ phép đã được gửi.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
