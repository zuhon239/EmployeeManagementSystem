using EmployeeManagementSystem.Controller;
using EmployeeManagementSystem.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagementSystem
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var context = new EmployeeManagementContext()) 
            { var users = context.Users.ToList(); 
                foreach (var user in users) 
                { if (string.IsNullOrEmpty(user.HashedPassword) && !string.IsNullOrEmpty(user.Password)) 
                  { user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password); } 
                } context.SaveChanges(); 
                Console.WriteLine("Mật khẩu đã được mã hóa và lưu vào cột HashedPassword!"); }
        
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var serviceProvider = new ServiceCollection()
           .AddDbContext<EmployeeManagementContext>()
           .AddScoped<LeaveRequestController>()
           .AddScoped<AttendanceController>()
           .AddScoped<LoginController>()
           .AddScoped<LoginForm>()
           .BuildServiceProvider();

            var loginForm = serviceProvider.GetService<LoginForm>();
            Application.Run(loginForm);
        }
    }
}