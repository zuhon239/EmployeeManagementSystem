using EmployeeManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Controller
{
    public class LoginController
    {
        private readonly EmployeeManagementContext _context;
        private readonly LeaveRequestController _leaveRequestController;

        public LoginController(EmployeeManagementContext context, LeaveRequestController leaveRequestController)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _leaveRequestController = leaveRequestController ?? throw new ArgumentNullException(nameof(leaveRequestController));
        }

        public bool Login(string username, string password, out string errorMessage, out Form nextForm)
        {
            errorMessage = string.Empty;
            nextForm = null;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                errorMessage = "Username and password are required.";
                return false;
            }

            var user = User.Authenticate(_context, username, password);
            if (user == null)
            {
                errorMessage = "Invalid username or password, or account is inactive.";
                return false;
            }

            // Navigate based on role
            switch (user.Role.RoleName)
            {
                case "Admin":
                    nextForm = new AdminForm();
                    break;
                case "Manager":
                    nextForm = new ManagerForm(user.UserId, _leaveRequestController, _context);
                    break;
                case "Employee":
                    nextForm = new EmployeeForm(user.UserId, _leaveRequestController, _context);
                    break;
                default:
                    errorMessage = "Unknown role.";
                    return false;
            }
            return true;
        }
    }
}
