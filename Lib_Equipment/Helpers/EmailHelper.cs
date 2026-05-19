using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Lib_Equipment.Helpers
{
    public static class EmailHelper
    {
        // THAY ĐỔI THÔNG TIN TẠI ĐÂY
        private static string senderEmail = "nxthuy22032004@gmail.com";
        private static string appPassword = "akwd zzjk vawd dckm"; // Mã 16 ký tự App Password của Google

        public static bool SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(senderEmail, appPassword),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(senderEmail, "THƯ VIỆN ĐẠI HỌC UNETI"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8
                };

                mailMessage.To.Add(toEmail);
                smtpClient.Send(mailMessage);
                return true;
            }
            catch { return false; }
        }

        public static bool SendNoticeEmail(string toEmail, string readerName, string readerID, decimal amount, string type)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(senderEmail, appPassword),
                    EnableSsl = true,
                };

                // Định dạng HTML chuyên nghiệp cho Email
                string subject = $"[UNETI] THÔNG BÁO GHI NHẬN CÔNG NỢ THƯ VIỆN - {readerID}";
                string body = $@"
                <div style='font-family: Segoe UI, Arial, sans-serif; max-width: 600px; border: 1px solid #e0e0e0; border-radius: 8px; overflow: hidden; margin: auto;'>
                    <div style='background-color: #1a4b84; padding: 20px; text-align: center;'>
                        <h2 style='color: white; margin: 0;'>THƯ VIỆN ĐẠI HỌC UNETI</h2>
                    </div>
                    <div style='padding: 30px; line-height: 1.6; color: #333;'>
                        <p>Xin chào <b>{readerName}</b>,</p>
                        <p>Hệ thống ghi nhận bạn có phát sinh một khoản <b>Công nợ học thuật</b> mới tại thư viện:</p>
                        <table style='width: 100%; border-collapse: collapse; margin: 20px 0;'>
                            <tr style='background-color: #f8f9fa;'>
                                <td style='padding: 10px; border: 1px solid #ddd; width: 40%;'><b>Mã Độc giả:</b></td>
                                <td style='padding: 10px; border: 1px solid #ddd;'>{readerID}</td>
                            </tr>
                            <tr>
                                <td style='padding: 10px; border: 1px solid #ddd;'><b>Lý do vi phạm:</b></td>
                                <td style='padding: 10px; border: 1px solid #ddd;'>{type}</td>
                            </tr>
                            <tr style='color: #d93025;'>
                                <td style='padding: 10px; border: 1px solid #ddd;'><b>Số tiền ghi nợ:</b></td>
                                <td style='padding: 10px; border: 1px solid #ddd; font-size: 16px;'><b>{amount:N0} VNĐ</b></td>
                            </tr>
                        </table>
                        <p style='background-color: #fff3cd; padding: 12px; border-left: 5px solid #ffc107; color: #856404; font-size: 14px;'>
                            <b>Lưu ý:</b> Khoản nợ này sẽ được chuyển sang bộ phận Đào tạo/Tài chính để xử lý (Chặn xét tốt nghiệp đối với Sinh viên hoặc trừ lương đối với Giảng viên).
                        </p>
                        <p>Vui lòng đăng nhập vào hệ thống UNETI Bank để thanh toán trực tuyến hoặc đến văn phòng Thư viện để giải quyết sớm nhất.</p>
                    </div>
                    <div style='background-color: #f1f1f1; padding: 15px; text-align: center; font-size: 12px; color: #777;'>
                        Đây là thư gửi tự động từ hệ thống quản lý. Vui lòng không trả lời thư này.<br/>
                        &copy; 2026 UNETI Library Management System.
                    </div>
                </div>";

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(senderEmail, "THƯ VIỆN UNETI"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8
                };

                mailMessage.To.Add(toEmail);
                smtpClient.Send(mailMessage);
                return true;
            }
            catch { return false; }
        }
    }
}