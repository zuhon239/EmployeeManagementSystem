using EmployeeManagementSystem.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Controller
{
    public class AttendanceController
    {
        private readonly EmployeeManagementContext _context;
        private readonly List<string> _allowedBSSIDs;

        public AttendanceController(EmployeeManagementContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _allowedBSSIDs = new List<string>
            {
                "24:0B:2A:9B:E9:15"
            };
        }

        // Lấy BSSID WiFi
        public string GetCurrentWiFiBSSID()
        {
            try
            {
                var process = new System.Diagnostics.Process();
                process.StartInfo.FileName = "netsh";
                process.StartInfo.Arguments = "wlan show interfaces";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;

                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                var lines = output.Split('\n');
                foreach (var line in lines)
                {
                    if (line.Trim().StartsWith("BSSID"))
                    {
                        int colonIndex = line.IndexOf(':');
                        if (colonIndex != -1 && colonIndex < line.Length - 1)
                        {
                            string bssid = line.Substring(colonIndex + 1);
                            bssid = System.Text.RegularExpressions.Regex.Replace(bssid.Trim(), @"\s+", "");

                            if (bssid.Length == 12)
                            {
                                bssid = string.Format("{0}:{1}:{2}:{3}:{4}:{5}",
                                    bssid.Substring(0, 2),
                                    bssid.Substring(2, 2),
                                    bssid.Substring(4, 2),
                                    bssid.Substring(6, 2),
                                    bssid.Substring(8, 2),
                                    bssid.Substring(10, 2));
                            }

                            return bssid.ToUpper();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi WiFi: {ex.Message}");
            }
            return null;
        }

        // Kiểm tra WiFi hợp lệ
        public bool IsValidCompanyWiFi(string bssid)
        {
            if (string.IsNullOrEmpty(bssid)) return false;

            foreach (var allowedBssid in _allowedBSSIDs)
            {
                if (string.Equals(bssid, allowedBssid, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        // ✅ Check In - Logic mới cho từng ca riêng biệt
        public async Task<string> CheckInAsync(int userId)
        {
            try
            {
                // Kiểm tra WiFi
                string currentBSSID = GetCurrentWiFiBSSID();
                if (!IsValidCompanyWiFi(currentBSSID))
                    return "Phải kết nối WiFi công ty!";

                // Thời gian test: 7h00 ca sáng
                DateTime now = new DateTime(2025, 5, 27, 7, 0, 0);
                DateTime today = now.Date;

                // ✅ Xác định ca hiện tại
                string currentShift;
                string status;

                if (now.Hour >= 7 && now.Hour <= 12)
                {
                    currentShift = "Sáng";
                    status = (now.Hour == 7 && now.Minute <= 30) ? "Đúng giờ" : "Đi trễ";
                }
                else if (now.Hour >= 13 && now.Hour <= 17)
                {
                    currentShift = "Chiều";
                    status = (now.Hour == 13 && now.Minute <= 30) ? "Đúng giờ" : "Đi trễ";
                }
                else
                {
                    return "Ngoài giờ làm việc!";
                }

                // ✅ Kiểm tra đã check in ca này chưa
                var existingShiftAttendance = await _context.Attendances
                    .FirstOrDefaultAsync(a => a.UserId == userId &&
                                           a.Date.Date == today &&
                                           a.Shift == currentShift);

                if (existingShiftAttendance?.ClockIn != null)
                    return $"Đã check in ca {currentShift} hôm nay!";

                // ✅ Tạo record mới cho ca hiện tại
                if (existingShiftAttendance == null)
                {
                    var newAttendance = new Attendance(userId, today, now, null, status, currentShift);
                    _context.Attendances.Add(newAttendance);
                }
                else
                {
                    existingShiftAttendance.ClockIn = now;
                    existingShiftAttendance.UpdateStatus(status);
                }

                await _context.SaveChangesAsync();
                return $"Check in ca {currentShift} thành công! {status} - {now:HH:mm:ss}";
            }
            catch (Exception ex)
            {
                return $"Lỗi: {ex.Message}";
            }
        }

        // ✅ Check Out - Logic mới cho từng ca riêng biệt
        public async Task<string> CheckOutAsync(int userId)
        {
            try
            {
                // Kiểm tra WiFi
                string currentBSSID = GetCurrentWiFiBSSID();
                if (!IsValidCompanyWiFi(currentBSSID))
                    return "Phải kết nối WiFi công ty!";

                // Thời gian test: 7h35 ca sáng
                DateTime now = new DateTime(2025, 5, 27, 7, 35, 0);
                DateTime today = now.Date;

                // ✅ Xác định ca hiện tại
                string currentShift;
                if (now.Hour >= 7 && now.Hour <= 12)
                {
                    currentShift = "Sáng";
                }
                else if (now.Hour >= 13 && now.Hour <= 17)
                {
                    currentShift = "Chiều";
                }
                else
                {
                    return "Ngoài giờ làm việc!";
                }

                // ✅ Tìm record attendance của ca hiện tại
                var existingShiftAttendance = await _context.Attendances
                    .FirstOrDefaultAsync(a => a.UserId == userId &&
                                           a.Date.Date == today &&
                                           a.Shift == currentShift);

                if (existingShiftAttendance == null || existingShiftAttendance.ClockIn == null)
                    return $"Chưa check in ca {currentShift}!";

                if (existingShiftAttendance.ClockOut != null)
                    return $"Đã check out ca {currentShift} rồi!";

                // ✅ Cập nhật check out cho ca hiện tại
                existingShiftAttendance.ClockOut = now;

                // ✅ Cập nhật status dựa trên giờ check out của ca
                string newStatus = existingShiftAttendance.Status;

                if (currentShift == "Sáng" && now.Hour < 12)
                {
                    // Check out sớm ca sáng (trước 12h)
                    newStatus = existingShiftAttendance.Status == "Đi trễ" ? "Đi trễ và về sớm" : "Về sớm";
                }
                else if (currentShift == "Chiều" && now.Hour < 17)
                {
                    // Check out sớm ca chiều (trước 17h)
                    newStatus = existingShiftAttendance.Status == "Đi trễ" ? "Đi trễ và về sớm" : "Về sớm";
                }

                existingShiftAttendance.UpdateStatus(newStatus);
                await _context.SaveChangesAsync();

                return $"Check out ca {currentShift} thành công! {newStatus} - {now:HH:mm:ss}";
            }
            catch (Exception ex)
            {
                return $"Lỗi: {ex.Message}";
            }
        }

        // ✅ Lấy thông tin attendance hôm nay (cả 2 ca)
        public async Task<List<Attendance>> GetTodayAttendancesAsync(int userId)
        {
            var today = new DateTime(2025, 5, 27).Date;
            return await _context.Attendances
                .Where(a => a.UserId == userId && a.Date.Date == today)
                .OrderBy(a => a.Shift)
                .ToListAsync();
        }

        // ✅ Thêm method filter theo ngày cụ thể
        public async Task<List<Attendance>> GetAttendanceByDateAsync(int userId, DateTime filterDate)
        {
            try
            {
                return await _context.Attendances
                    .Where(a => a.UserId == userId && a.Date.Date == filterDate.Date)
                    .OrderBy(a => a.Shift) // Sắp xếp theo ca: Sáng trước, Chiều sau
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi GetAttendanceByDateAsync: {ex.Message}");
                return new List<Attendance>();
            }
        }

        // ✅ Giữ nguyên method lấy tất cả lịch sử
        public async Task<List<Attendance>> GetAttendanceHistoryAsync(int userId)
        {
            try
            {
                return await _context.Attendances
                    .Where(a => a.UserId == userId)
                    .OrderByDescending(a => a.Date)
                    .ThenBy(a => a.Shift) // Sắp xếp theo ca: Sáng trước, Chiều sau
                    .Take(60) // Lấy 60 records (30 ngày x 2 ca)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi GetAttendanceHistoryAsync: {ex.Message}");
                return new List<Attendance>();
            }
        }

    }
}
