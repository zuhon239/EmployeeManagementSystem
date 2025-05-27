using EmployeeManagementSystem.Controller;
using EmployeeManagementSystem.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagementSystem.FormAdmin
{
    public partial class EmployeeAdminForm : Form
    {
        private int? _selectedEmployeeId;
        private readonly EmployeeManagementContext _context;
        private readonly LeaveRequestController _controller;
        private readonly EmployeeController _employeecontroller;
        private readonly int _currentUserId;
        public EmployeeAdminForm(int currentUserId, EmployeeManagementContext context)
        {
            InitializeComponent();
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _employeecontroller = new EmployeeController(context);
            _currentUserId = currentUserId;
            comboBox1.SelectedIndex = 0;
            LoadEmployee();
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            dataGridView1.CellClick += DataGridView1_CellClick;
            dataGridView1.CellContentClick += DataGridView1_CellContentClick;
            dataGridView1.CellMouseEnter += DataGridView1_CellMouseEnter; 
            dataGridView1.CellMouseLeave += DataGridView1_CellMouseLeave;
            cbGender.Items.AddRange(new[] { "Male", "Female" });
            btnThem.Click += BtnThem_Click;
            button1.Click += BtnSua_Click;
            btnSaThai.Click += BtnSaThai_Click;
         
        }
        private void LoadEmployee()
        {         
            try
            {
                var selectedItem = comboBox1.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(selectedItem))
                {
                    MessageBox.Show("Please select a valid option.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                dataGridView1.Rows.Clear();
                if (selectedItem == "Manager")
                {
                    var justmanager = _context.Users
                    .OfType<Employee>()
                    .Where(lr => lr.RoleId == 2 && lr.Status)
                    .Select(lr => new
                    {
                        lr.UserId,
                        lr.Name,
                        lr.Gender,
                        lr.DateOfBirth,
                        lr.Phone,
                        lr.Position,
                        lr.HireDate,
                        Promotion = "Promotion"
                    })
                    .ToList();

                    foreach (var request in justmanager)
                    {
                        dataGridView1.Rows.Add(
                            request.UserId,
                            request.Name,
                            request.Gender,
                            request.DateOfBirth.ToString("dd-MM-yyyy"),
                            request.Phone,
                            request.Position,
                            request.HireDate.ToString("dd-MM-yyyy"),
                            request.Promotion
                        );
                    }
                }
                else                 
                {
                    var department = _context.Departments
                        .FirstOrDefault(d => d.Name == selectedItem);

                    if (department == null)
                    {
                        MessageBox.Show("Selected department not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var employeesInDepartment = _context.Employees
                       .Where(e => e.DepartmentId == department.DepartmentId && e.RoleId == 1)
                       .Select(e => e.UserId)
                       .ToList();
                    if (!employeesInDepartment.Any())
                    {
                        MessageBox.Show("No employees found in this department.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
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
                            lr.HireDate,
                            Promotion = "Promotion"
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
                            request.HireDate.ToString("dd-MM-yyyy"),
                            request.Promotion
                        );
                    }
                }
                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading leave requests: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEmployee(); 
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
                    cbGender.SelectedItem = employee.Gender;
                    if (employee.DateOfBirth != default(DateTime))
                    {
                        dateTimePicker1.Value = employee.DateOfBirth;
                        dateTimePicker1.Checked = true; 
                    }
                    else
                    {
                        dateTimePicker1.Checked = false; 
                    }
                    txtPhone.Text = employee.Phone;
                    txtUserName.Text = employee.Username;
                    txtPosition.Text = employee.Position;
                    txtEmail.Text = employee.Email;
                }
            }
        }
        private void DataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["clUpdateRole"].Index && e.RowIndex >= 0)
            {
                dataGridView1.Cursor = Cursors.Hand;
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.Blue;
            }
        }
        private void DataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["clUpdateRole"].Index && e.RowIndex >= 0)
            {
                dataGridView1.Cursor = Cursors.Default;
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.Black;
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
                        int currentIndex = comboBox1.SelectedIndex;
                        comboBox1.SelectedIndex = currentIndex;
                        LoadEmployee();
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
                    var gender = cbGender.SelectedItem?.ToString();
                    var dateOfBirth = dateTimePicker1.Value;
                    var phone = txtPhone.Text.Trim();
                    var email = string.IsNullOrWhiteSpace(txtEmail.Text.Trim()) ? null : txtEmail.Text.Trim();
                    var position = txtPosition.Text.Trim();

                    if (_employeecontroller.UpdateEmployee(_selectedEmployeeId.Value, name, gender, dateOfBirth, phone, email, position))
                    {
                        MessageBox.Show("Cập nhật nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        int currentIndex = comboBox1.SelectedIndex;
                        comboBox1.SelectedIndex = currentIndex;
                        LoadEmployee(); 
                        
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
                        int currentIndex = comboBox1.SelectedIndex;
                        comboBox1.SelectedIndex = currentIndex;
                        LoadEmployee();
                        ClearInputs();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi sa thải nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["clUpdateRole"].Index)
            {
                var selectedEmployeeId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["clUserId"].Value);
                var result = MessageBox.Show(
                    "Xác nhận thăng chức nhân viên này thành Manager?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        if (_employeecontroller.PromoteEmployee(selectedEmployeeId))
                        {
                            MessageBox.Show("Thăng chức nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            int currentIndex = comboBox1.SelectedIndex;
                            comboBox1.SelectedIndex = currentIndex; // Làm mới danh sách
                            LoadEmployee();
                            ClearInputs();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi thăng chức nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void ClearInputs()
        {
            txtID.Text = "";
            txtName.Text = "";
            cbGender.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
            txtPhone.Text = "";
            txtUserName.Text = "";
            txtEmail.Text = "";
            txtPosition.Text = "";
            _selectedEmployeeId = null;
        }

    }
}
