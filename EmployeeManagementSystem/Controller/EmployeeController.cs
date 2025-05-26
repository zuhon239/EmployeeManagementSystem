using EmployeeManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Controller
{
    public class EmployeeController
    {
        private readonly EmployeeManagementContext _context;

        public EmployeeController(EmployeeManagementContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Tạo ID tự động dựa trên DepartmentId
        private string GenerateEmployeeId(int departmentId)
        {
            var lastEmployee = _context.Employees
                .Where(e => e.DepartmentId == departmentId)
                .OrderByDescending(e => e.UserId)
                .FirstOrDefault();

            int sequence = lastEmployee != null ? int.Parse(lastEmployee.UserId.ToString().Substring(2)) + 1 : 1;
            return $"{departmentId}{sequence.ToString("D3")}";
        }

        // Tạo mật khẩu ngẫu nhiên
        private string GenerateRandomPassword(int length = 8)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // Thêm nhân viên mới
       // Thêm nhân viên mới
    public bool AddEmployee(string username, string name, string gender, DateTime dateOfBirth, string phone, string email, int departmentId, string position)
    {
        try
        {
            // Validate dữ liệu
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username không được để trống.");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tên không được để trống.");
            if (string.IsNullOrWhiteSpace(gender))
                throw new ArgumentException("Giới tính không được để trống.");
            if (string.IsNullOrWhiteSpace(position))
                throw new ArgumentException("Vị trí không được để trống.");

            // Kiểm tra username trùng lặp
            if (User.UsernameExists(_context, username))
                throw new ArgumentException("Username đã tồn tại.");

            // Kiểm tra email nếu được cung cấp
            if (!string.IsNullOrWhiteSpace(email) && User.EmailExists(_context, email))
                throw new ArgumentException("Email đã tồn tại.");

            // Tạo mật khẩu ngẫu nhiên
            var password = GenerateRandomPassword();

            // Tạo ID tự động
            var employeeId = int.Parse(GenerateEmployeeId(departmentId));

            // Lấy thời điểm hiện tại làm HireDate
            var hireDate = DateTime.Now;

            var employee = new Employee(
                username: username,
                password: password,
                email: email,
                roleId: 1, // Role mặc định là Employee
                status: true,
                name: name,
                gender: gender,
                dateOfBirth: dateOfBirth,
                phone: phone,
                departmentId: departmentId,
                position: position,
                hireDate: hireDate
            );

            _context.Employees.Add(employee);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Lỗi khi thêm nhân viên: {ex.Message}");
        }
    }
        // Sửa thông tin nhân viên
        public bool UpdateEmployee(int userId, string name, string gender, DateTime dateOfBirth, string phone, string email, string position)
        {
            try
            {
                var employee = _context.Employees.FirstOrDefault(e => e.UserId == userId);
                if (employee == null)
                    throw new ArgumentException("Nhân viên không tồn tại.");

                // Validate dữ liệu
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentException("Tên không được để trống.");
                if (string.IsNullOrWhiteSpace(gender))
                    throw new ArgumentException("Giới tính không được để trống.");
                if (string.IsNullOrWhiteSpace(position))
                    throw new ArgumentException("Vị trí không được để trống.");

                if (!string.IsNullOrWhiteSpace(email) && User.EmailExists(_context, email, userId))
                    throw new ArgumentException("Email đã tồn tại.");

                // Cập nhật thông tin
                employee.UpdateName(name);
                employee.UpdateGender(gender);
                employee.UpdateDateOfBirth(dateOfBirth);
                employee.UpdatePhone(phone);
                employee.UpdateEmail(string.IsNullOrWhiteSpace(email) ? null : email); // Chấp nhận null nếu email rỗng
                employee.UpdatePosition(position);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi sửa nhân viên: {ex.Message}");
            }
        }

        // Sa thải nhân viên (chuyển Status thành false)
        public bool TerminateEmployee(int userId)
        {
            try
            {
                var employee = _context.Employees.FirstOrDefault(e => e.UserId == userId);
                if (employee == null)
                    throw new ArgumentException("Nhân viên không tồn tại.");

                employee.Status = false;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi sa thải nhân viên: {ex.Message}");
            }
        }

        // Lấy thông tin nhân viên theo UserId
        public Employee GetEmployeeById(int userId)
        {
            return _context.Employees.FirstOrDefault(e => e.UserId == userId);
        }
    }
}
