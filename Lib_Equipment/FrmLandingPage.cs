using Guna.UI2.WinForms;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Data.SqlClient;
using System.Windows.Forms;
using Lib_Equipment.Database; // Gọi DataProvider để lấy dữ liệu thật

namespace Lib_Equipment
{
    public partial class FrmLandingPage : Form
    {
        private string configPath = Path.Combine(Application.StartupPath, "app_docs_config.ini");

        public FrmLandingPage()
        {
            InitializeComponent();

            this.WindowState = FormWindowState.Maximized;

            this.Resize += (s, e) => {
                pnlCenter.Left = (this.ClientSize.Width - pnlCenter.Width) / 2;
                pnlCenter.Top = (this.ClientSize.Height - pnlCenter.Height) / 2 - 20;
            };

            CreateNewsAndStatsPanel();

            btnDangNhap.Click += BtnDangNhap_Click;
            btnLuatThuVien.Click += BtnLuatThuVien_Click;
            btnHuongDan.Click += BtnHuongDan_Click;
        }

        private void CreateNewsAndStatsPanel()
        {
            // ==========================================
            // 1. VẼ BẢNG TIN TỨC (NEWS)
            // ==========================================
            Label lblNewsTitle = new Label() { Text = "📢 THÔNG BÁO TỪ BAN QUẢN TRỊ", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.FromArgb(0, 51, 102), Location = new Point(25, 25), AutoSize = true, BackColor = Color.Transparent };
            pnlNews.Controls.Add(lblNewsTitle);

            string[] newsItems = {
                $"[{DateTime.Now:dd/MM/yyyy}] HOT: Hệ thống AI Trợ lý thư viện đã sẵn sàng.",
                $"[{DateTime.Now.AddDays(-2):dd/MM/yyyy}] Thư viện mới nhập thêm các đầu sách chuyên ngành.",
                $"[{DateTime.Now.AddDays(-5):dd/MM/yyyy}] Lưu ý: Sinh viên thanh toán nợ trước khi kết thúc kỳ.",
                $"[{DateTime.Now.AddDays(-10):dd/MM/yyyy}] Lịch bảo trì hệ thống định kỳ vào cuối tuần này."
            };

            int yPos = 85;
            foreach (string item in newsItems)
            {
                Label lblItem = new Label() { Text = "• " + item, Font = new Font("Segoe UI", 12), ForeColor = Color.FromArgb(64, 64, 64), Location = new Point(25, yPos), AutoSize = true, BackColor = Color.Transparent };
                pnlNews.Controls.Add(lblItem);
                yPos += 45;
            }

            // ==========================================
            // 2. VẼ BẢNG THỐNG KÊ (DÙNG DATABASE THẬT)
            // ==========================================
            Label lblStatsTitle = new Label() { Text = "📊 THỐNG KÊ HỆ THỐNG", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.FromArgb(0, 51, 102), Location = new Point(25, 25), AutoSize = true, BackColor = Color.Transparent };
            pnlStats.Controls.Add(lblStatsTitle);

            // Bắt đầu chọc vào Database lấy số thật
            string countBooks = "0";
            string countEquips = "0";
            string countReaders = "0";

            try
            {
                // Lấy tổng số Sách (Bảng Book)
                countBooks = DataProvider.Instance.ExecuteScalar("SELECT COUNT(*) FROM Book WHERE IsDeleted = 0")?.ToString() ?? "0";

                // Lấy tổng số Thiết bị (Bảng Equipment)
                countEquips = DataProvider.Instance.ExecuteScalar("SELECT COUNT(*) FROM Equipment WHERE IsDeleted = 0")?.ToString() ?? "0";

                // Lấy tổng số Độc giả (Bảng Reader)
                countReaders = DataProvider.Instance.ExecuteScalar("SELECT COUNT(*) FROM Reader WHERE IsDeleted = 0")?.ToString() ?? "0";
            }
            catch (Exception ex)
            {
                // Nếu DB chưa bật hoặc có lỗi, hiện số 0
                Console.WriteLine("Lỗi DB: " + ex.Message);
            }

            string[,] stats = {
                { "📚", countBooks, "Đầu sách & Giáo trình" },
                { "💻", countEquips, "Thiết bị thực hành" },
                { "👥", countReaders, "Độc giả trong hệ thống" }
            };

            int yStatPos = 90;
            for (int i = 0; i < 3; i++)
            {
                Label lblIcon = new Label() { Text = stats[i, 0], Font = new Font("Segoe UI", 24), Location = new Point(30, yStatPos - 10), AutoSize = true, BackColor = Color.Transparent };

                // Định dạng số có dấu phẩy (VD: 1.500 thay vì 1500)
                int num = int.Parse(stats[i, 1]);
                Label lblNumber = new Label() { Text = num.ToString("N0"), Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.FromArgb(255, 102, 0), Location = new Point(90, yStatPos - 5), AutoSize = true, BackColor = Color.Transparent };

                Label lblDesc = new Label() { Text = stats[i, 2], Font = new Font("Segoe UI", 12), ForeColor = Color.DimGray, Location = new Point(190, yStatPos), AutoSize = true, BackColor = Color.Transparent };

                pnlStats.Controls.Add(lblIcon);
                pnlStats.Controls.Add(lblNumber);
                pnlStats.Controls.Add(lblDesc);

                yStatPos += 70;
            }
        }

        private void BtnDangNhap_Click(object sender, EventArgs e)
        {
            this.Hide();
            new FrmLogin().ShowDialog();
            this.Close();
        }

        // ==========================================
        // TÍNH NĂNG TỰ ĐỘNG TẠO & LƯU & ĐỌC TÀI LIỆU
        // ==========================================
        private void BtnLuatThuVien_Click(object sender, EventArgs e)
        {
            string htmlContent = @"
                <div style='max-width:800px; margin:40px auto; font-family:Arial,sans-serif; color:#333; line-height:1.8; padding:30px; border:1px solid #ddd; border-radius:10px; box-shadow: 0 4px 8px rgba(0,0,0,0.1);'>
                    <h1 style='text-align:center; color:#003366;'>📜 NỘI QUY THƯ VIỆN UNETI</h1>
                    <hr style='border:1px solid #003366; margin-bottom:20px;'>
                    <ul style='font-size:16px;'>
                        <li><b>Điều 1:</b> Xuất trình Thẻ Sinh viên khi mượn trả sách/thiết bị. Không mượn hộ người khác.</li>
                        <li><b>Điều 2:</b> Thời hạn mượn sách tối đa: 14 ngày (Sinh viên) và 21 ngày (Giảng viên).</li>
                        <li><b>Điều 3:</b> Giữ gìn tài sản chung. Làm rách, mất sách hoặc hỏng thiết bị phải bồi thường 200% giá trị.</li>
                        <li><b>Điều 4:</b> Quá hạn trả sách/thiết bị sẽ bị phạt tiền tự động theo quy định của nhà trường.</li>
                        <li><b>Điều 5:</b> Sinh viên phải thanh toán hết công nợ mới được tiếp tục sử dụng các dịch vụ của hệ thống.</li>
                    </ul>
                    <p style='text-align:right; font-style:italic; margin-top:40px;'>Hà Nội, năm 2026<br><b>Bản quyền thuộc hệ thống Admin UNETI</b></p>
                </div>";

            HandleDocument("NoiQuy", "NoiQuyThuVien.html", htmlContent);
        }

        private void BtnHuongDan_Click(object sender, EventArgs e)
        {
            string htmlContent = @"
                <div style='max-width:800px; margin:40px auto; font-family:Arial,sans-serif; color:#333; line-height:1.8; padding:30px; border:1px solid #ddd; border-radius:10px; box-shadow: 0 4px 8px rgba(0,0,0,0.1);'>
                    <h1 style='text-align:center; color:#FF6600;'>💡 HƯỚNG DẪN SỬ DỤNG HỆ THỐNG</h1>
                    <hr style='border:1px solid #FF6600; margin-bottom:20px;'>
                    <h3 style='color:#003366;'>1. Đăng nhập và tra cứu</h3>
                    <p>Sử dụng mã Sinh Viên / Giảng Viên do trường cấp. Tại trang chủ, sử dụng thanh tìm kiếm để tra cứu sách theo tên, tác giả hoặc chuyên ngành.</p>
                    <h3 style='color:#003366;'>2. Quy trình mượn sách thông minh</h3>
                    <p>Bấm vào nút <b>Tự động mượn sách</b> (màu cam), sau đó đưa mã vạch của sách vào máy quét. Hệ thống sẽ tự động đối soát và cập nhật vào lịch sử mượn của bạn.</p>
                    <h3 style='color:#003366;'>3. Trả sách & Gia hạn</h3>
                    <p>Truy cập tab <b>Lịch sử</b>. Bạn có thể nhấn nút Gia hạn nếu sách sắp hết hạn (áp dụng 1 lần). Để trả sách, nhấn nút Trả và bỏ sách vào Tủ trả tự động.</p>
                    <h3 style='color:#003366;'>4. Trợ lý AI</h3>
                    <p>Nếu gặp khó khăn trong việc tìm kiếm dữ liệu, hãy mở tab Hỗ trợ (AI) và đặt câu hỏi. Hệ thống sẽ kết nối trực tiếp vào Database để trả lời chính xác nhất.</p>
                </div>";

            HandleDocument("HuongDan", "HuongDanSuDung.html", htmlContent);
        }

        private void HandleDocument(string key, string defaultFileName, string htmlContent)
        {
            string savedPath = GetSavedPath(key);

            if (string.IsNullOrEmpty(savedPath) || !File.Exists(savedPath))
            {
                MessageBox.Show("Đây là lần đầu mở tài liệu này. Hệ thống sẽ tạo một File văn bản siêu đẹp cho bạn.\n\nVui lòng chọn thư mục bạn muốn lưu File này!", "Thiết lập tài liệu", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Chọn nơi lưu tài liệu";
                sfd.FileName = defaultFileName;
                sfd.Filter = "Web Document (*.html)|*.html";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sfd.FileName, htmlContent);
                    SavePathToConfig(key, sfd.FileName);
                    savedPath = sfd.FileName;
                    MessageBox.Show("Tạo và lưu tài liệu thành công!\nCác lần sau bấm nút sẽ tự động mở file này.", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    return;
                }
            }

            try
            {
                Process.Start(new ProcessStartInfo(savedPath) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở tài liệu: " + ex.Message);
            }
        }

        private string GetSavedPath(string key)
        {
            if (!File.Exists(configPath)) return "";
            string[] lines = File.ReadAllLines(configPath);
            foreach (string line in lines)
            {
                if (line.StartsWith(key + "=")) return line.Substring(key.Length + 1);
            }
            return "";
        }

        private void SavePathToConfig(string key, string path)
        {
            string newContent = "";
            bool keyExists = false;

            if (File.Exists(configPath))
            {
                string[] lines = File.ReadAllLines(configPath);
                foreach (string line in lines)
                {
                    if (line.StartsWith(key + "=")) { newContent += key + "=" + path + "\n"; keyExists = true; }
                    else { newContent += line + "\n"; }
                }
            }
            if (!keyExists) newContent += key + "=" + path + "\n";

            File.WriteAllText(configPath, newContent);
        }
    }
}