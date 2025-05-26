using EmployeeManagementSystem.Controller;
using EmployeeManagementSystem.Model;
using System.Data;

namespace EmployeeManagementSystem.FormManager
{
    public partial class EmployeeManagerForm : Form
    {
        private int? _selectedEmployeeId;
        private readonly EmployeeManagementContext _context;
        private readonly LeaveRequestController _controller;
        private readonly EmployeeController _employeecontroller;
        private readonly int _currentUserId;
        public EmployeeManagerForm(int currentUserId, EmployeeManagementContext context)
        {
            InitializeComponent();
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _employeecontroller = new EmployeeController(context);
            _currentUserId = currentUserId;
            LoadEmployee(_currentUserId);
            dataGridView1.CellClick += DataGridView1_CellClick;

            // Gắn sự kiện cho các button
            btnThem.Click += BtnThem_Click;
            button1.Click += BtnSua_Click;
            btnSaThai.Click += BtnSaThai_Click;
            comboBox1.Items.AddRange(new[] { "Male", "Female" });
            dateTimePicker1.Checked = false;

        }
        private void LoadEmployee(int _currentUserId)
        {
            try
            {
                var manager = _context.Users
                .OfType<Employee>()
                .FirstOrDefault(e => e.UserId == _currentUserId);

                if (manager == null)
                {
                    MessageBox.Show("No manager found with the given UserId.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var managedDepartmentId = manager.DepartmentId;

                if (managedDepartmentId == null)
                {
                    MessageBox.Show("No department found for this manager.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var employeesInDepartment = _context.Employees
                    .Where(e => e.DepartmentId == managedDepartmentId && e.RoleId == 1)
                    .Select(e => e.UserId)
                    .ToList();

                var employee = _context.Users
                .OfType<Employee>()
                .Where(lr => employeesInDepartment.Contains(lr.UserId) && lr.RoleId == 1 && lr.Status)
                    .Select(lr => new
                    {
                        lr.UserId,
                        lr.Name,
                        lr.Gender,
                        lr.DateOfBirth,
                        lr.Phone,
                        lr.Position,
                        lr.HireDate
                    })
                    .ToList();

                dataGridView1.Rows.Clear();

                foreach (var request in employee)
                {
                    dataGridView1.Rows.Add(
                        request.UserId,
                        request.Name,
                        request.Gender,
                        request.DateOfBirth.ToString("dd-MM-yyyy"),
                        request.Phone,
                        request.Position,
                        request.HireDate.ToString("dd-MM-yyyy")
                    );
                }

                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading leave requests: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                _selectedEmployeeId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["clUserId"].Value);
                var employee = _employeecontroller.GetEmployeeById(_selectedEmployeeId.Value);
                if (employee != null)
                {
                    txtID.Text = employee.UserId.ToString();
                    txtName.Text = employee.Name;
                    comboBox1.SelectedItem = employee.Gender;
                    if (employee.DateOfBirth != default(DateTime))
                    {
                        dateTimePicker1.Value = employee.DateOfBirth;
                        dateTimePicker1.Checked = true; // Hiển thị ngày khi chọn nhân viên
                    }
                    else
                    {
                        dateTimePicker1.Checked = false; // Không có giá trị
                    }
                    txtPhone.Text = employee.Phone;
                    txtUserName.Text = employee.Username;
                    txtPosition.Text = employee.Position;
                    txtEmail.Text = employee.Email;
                    // Gán email vào TextBox (thay thế label3 và textBox3 nếu cần)
                }
            }
        }
        private void BtnThem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                   "Xác nhận thêm nhân viên?",
                   "Xác nhận",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Question);
            if (result == DialogResult.Yes) 
            {
                try
                {
                    var manager = _context.Employees.FirstOrDefault(e => e.UserId == _currentUserId);
                    if (manager == null)
                        throw new Exception("Không tìm thấy thông tin quản lý.");

                    var username = txtUserName.Text;
                    var name = txtName.Text;
                    var gender = comboBox1.SelectedItem?.ToString();
                    var dateOfBirth = dateTimePicker1.Value;
                    var email = txtEmail.Text;
                    var phone = txtPhone.Text;
                    var position = txtPosition.Text;

                    if (_employeecontroller.AddEmployee(username, name, gender, dateOfBirth, phone, email, manager.DepartmentId, position))
                    {
                        MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadEmployee(_currentUserId);
                        ClearInputs();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi thêm nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }
        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (!_selectedEmployeeId.HasValue)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var result = MessageBox.Show(
                   "Xác nhận sửa nhân viên?",
                   "Xác nhận",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Question);
            if (result == DialogResult.Yes) 
            {
                try
                {
                    var name = txtName.Text.Trim();
                    var gender = comboBox1.SelectedItem?.ToString();
                    var dateOfBirth = dateTimePicker1.Value;
                    var phone = txtPhone.Text.Trim();
                    var email = string.IsNullOrWhiteSpace(txtEmail.Text.Trim()) ? null : txtEmail.Text.Trim();
                    var position = txtPosition.Text.Trim();

                    if (_employeecontroller.UpdateEmployee(_selectedEmployeeId.Value, name, gender, dateOfBirth, phone, email, position))
                    {
                        MessageBox.Show("Cập nhật nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadEmployee(_currentUserId);
                        ClearInputs();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi sửa nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
           
        }
        private void BtnSaThai_Click(object sender, EventArgs e)
        {

            if (!_selectedEmployeeId.HasValue)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để sa thải.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var result = MessageBox.Show(
                   "Xác nhận sa thải nhân viên?",
                   "Xác nhận",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    if (_employeecontroller.TerminateEmployee(_selectedEmployeeId.Value))
                    {
                        MessageBox.Show("Sa thải nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadEmployee(_currentUserId);
                        ClearInputs();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi sa thải nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }
        private void ClearInputs()
        {
            txtID.Text = "";
            txtName.Text = "";
            comboBox1.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
            txtPhone.Text = "";
            txtUserName.Text = "";
            _selectedEmployeeId = null;
        }

    }
}
