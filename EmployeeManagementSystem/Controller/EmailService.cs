using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace EmployeeManagementSystem.Controller
{
    public class EmailService
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;
        private readonly string _fromName;

        public EmailService()
        {
            // Cấu hình SMTP 
            _smtpHost = "smtp.gmail.com";
            _smtpPort = 587;
            _smtpUser = "tgddkhachhang72@gmail.com"; 
            _smtpPass = "gefm npao xljb exta\r\n"; 
            _fromName = "Employee Management System"; 
        }

        public bool SendPasswordResetEmail(string toEmail, string resetToken, string username, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                using (var client = new SmtpClient(_smtpHost, _smtpPort))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(_smtpUser, _smtpPass);

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_smtpUser, _fromName),
                        Subject = "Đặt lại mật khẩu - Employee Management System",
                        Body = $@"
                            <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto;'>
                                <h2 style='color: #2c3e50; text-align: center;'>Đặt lại mật khẩu</h2>
                                <div style='background-color: #f8f9fa; padding: 20px; border-radius: 5px; margin: 20px 0;'>
                                    <p><strong>Chào {username},</strong></p>
                                    <p>Bạn đã yêu cầu đặt lại mật khẩu cho tài khoản của mình trong hệ thống Employee Management System.</p>
                                    <p><strong>Thông tin tài khoản:</strong></p>
                                    <ul>
                                        <li>Email: {toEmail}</li>
                                        <li>Username: {username}</li>
                                    </ul>
                                    <div style='background-color: #e3f2fd; padding: 15px; border-left: 4px solid #2196f3; margin: 15px 0;'>
                                        <p><strong>Mã token của bạn là:</strong></p>
                                        <h3 style='color: #1976d2; font-family: monospace; letter-spacing: 2px;'>{resetToken}</h3>
                                    </div>
                                    <p><strong>Lưu ý quan trọng:</strong></p>
                                    <ul>
                                        <li>Mã này có hiệu lực trong <strong>15 phút</strong></li>
                                        <li>Chỉ sử dụng mã này một lần duy nhất</li>
                                        <li>Không chia sẻ mã này với bất kỳ ai</li>
                                    </ul>
                                    <p>Nếu bạn không yêu cầu đặt lại mật khẩu, vui lòng bỏ qua email này và liên hệ với quản trị viên nếu cần thiết.</p>
                                </div>
                                <div style='border-top: 1px solid #dee2e6; padding-top: 15px; text-align: center; color: #6c757d;'>
                                    <p>Trân trọng,<br/>
                                    <strong>Employee Management System</strong></p>
                                    <small>Email này được gửi tự động, vui lòng không trả lời.</small>
                                </div>
                            </div>",
                        IsBodyHtml = true
                    };
                    mailMessage.To.Add(toEmail);

                    client.Send(mailMessage);
                    return true;
                }
            }
            catch (SmtpException ex)
            {
                errorMessage = $"Lỗi gửi email: Kết nối SMTP thất bại. Chi tiết: {ex.Message}";
                return false;
            }
            catch (ArgumentException ex)
            {
                errorMessage = $"Lỗi định dạng email: {ex.Message}";
                return false;
            }
            catch (Exception ex)
            {
                errorMessage = $"Lỗi gửi email: {ex.Message}";
                return false;
            }
        }

        public bool SendPasswordChangeNotification(string toEmail, string username, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                using (var client = new SmtpClient(_smtpHost, _smtpPort))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(_smtpUser, _smtpPass);

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_smtpUser, _fromName),
                        Subject = "Mật khẩu đã được thay đổi - Employee Management System",
                        Body = $@"
                            <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto;'>
                                <h2 style='color: #28a745; text-align: center;'>Mật khẩu đã được thay đổi thành công</h2>
                                <div style='background-color: #f8f9fa; padding: 20px; border-radius: 5px; margin: 20px 0;'>
                                    <p><strong>Chào {username},</strong></p>
                                    <p>Mật khẩu cho tài khoản của bạn đã được thay đổi thành công.</p>
                                    <p><strong>Thông tin tài khoản:</strong></p>
                                    <ul>
                                        <li>Email: {toEmail}</li>
                                        <li>Username: {username}</li>
                                        <li>Thời gian thay đổi: {DateTime.Now:dd/MM/yyyy HH:mm:ss}</li>
                                    </ul>
                                    <div style='background-color: #fff3cd; padding: 15px; border-left: 4px solid #ffc107; margin: 15px 0;'>
                                        <p><strong>Nếu bạn không thực hiện thay đổi này:</strong></p>
                                        <p>Vui lòng liên hệ ngay với quản trị viên hệ thống để được hỗ trợ bảo mật tài khoản.</p>
                                    </div>
                                </div>
                                <div style='border-top: 1px solid #dee2e6; padding-top: 15px; text-align: center; color: #6c757d;'>
                                    <p>Trân trọng,<br/>
                                    <strong>Employee Management System</strong></p>
                                    <small>Email này được gửi tự động, vui lòng không trả lời.</small>
                                </div>
                            </div>",
                        IsBodyHtml = true
                    };
                    mailMessage.To.Add(toEmail);

                    client.Send(mailMessage);
                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Lỗi gửi email thông báo: {ex.Message}";
                return false;
            }
        }
    }



}

