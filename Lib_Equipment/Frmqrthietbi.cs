using iTextSharp.text.pdf.qrcode;
using Lib_Equipment.Database;
using QRCoder;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Lib_Equipment
{
    public partial class FrmQRThietBi : Form
    {
        private readonly string _equipmentID;
        private readonly string _equipmentName;
        private Bitmap _qrBitmap;

        public FrmQRThietBi(string equipmentID, string equipmentName)
        {
            InitializeComponent();
            _equipmentID = equipmentID;
            _equipmentName = equipmentName;
            this.Text = $"QR Code - {equipmentName}";
        }

        private void FrmQRThietBi_Load(object sender, EventArgs e)
        {
            LoadAndGenerateQR();
        }

        // ═══════════════════════════════════════
        // LẤY DỮ LIỆU & TẠO NỘI DUNG QR
        // ═══════════════════════════════════════
        private void LoadAndGenerateQR()
        {
            try
            {
                // 1. Lấy thông tin thiết bị
                var dtInfo = DataProvider.Instance.ExecuteQuery(@"
                    SELECT e.EquipmentID, e.EquipmentName, c.CategoryName, d.DepartmentName,
                           e.ImportDate, e.PurchasePrice, e.Condition, e.NgayBaoTriDinhKy, e.MaVach
                    FROM Equipment e
                    LEFT JOIN EquipmentCategory c ON e.CategoryID = c.CategoryID
                    LEFT JOIN Department d ON e.DepartmentID = d.DepartmentID
                    WHERE e.EquipmentID = @id",
                    new SqlParameter[] { new SqlParameter("@id", _equipmentID) });

                if (dtInfo.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy thông tin thiết bị!");
                    this.Close();
                    return;
                }

                // 2. Lấy lịch sử bảo trì
                var dtMaintenance = DataProvider.Instance.ExecuteQuery(@"
                    SELECT MaintenanceDate, Description, Cost 
                    FROM MaintenanceRecord
                    WHERE EquipmentID = @id
                    ORDER BY MaintenanceDate DESC",
                    new SqlParameter[] { new SqlParameter("@id", _equipmentID) });

                // =========================================================
                // 🚀 TỰ ĐỘNG TÍNH NGÀY BẢO TRÌ TIẾP THEO (Tương tự form Hồ sơ)
                // =========================================================
                DateTime ngayMoc = DateTime.Now;
                if (dtInfo.Rows[0]["ImportDate"] != DBNull.Value)
                    ngayMoc = Convert.ToDateTime(dtInfo.Rows[0]["ImportDate"]);

                // Nếu đã từng bảo trì, lấy ngày bảo trì gần nhất làm mốc
                if (dtMaintenance.Rows.Count > 0)
                    ngayMoc = Convert.ToDateTime(dtMaintenance.Rows[0]["MaintenanceDate"]);

                double giaTri = 0;
                if (dtInfo.Rows[0]["PurchasePrice"] != DBNull.Value)
                    double.TryParse(dtInfo.Rows[0]["PurchasePrice"].ToString(), out giaTri);

                int chuKyThang = 5; // Dưới 10tr: 5 tháng
                if (giaTri >= 25000000) chuKyThang = 12; // Trên 25tr: 12 tháng
                else if (giaTri >= 10000000) chuKyThang = 8; // 10tr - 25tr: 8 tháng

                DateTime ngayBT_TiepTheo = ngayMoc.AddMonths(chuKyThang);

                // Gắn ngược lại vào DataRow để cả UI và mã QR đều hiển thị đúng
                dtInfo.Columns["NgayBaoTriDinhKy"].ReadOnly = false;
                dtInfo.Rows[0]["NgayBaoTriDinhKy"] = ngayBT_TiepTheo;
                // =========================================================

                // 3. Xây dựng nội dung QR
                string qrContent = BuildQRContent(dtInfo.Rows[0], dtMaintenance);

                // 4. Hiển thị thông tin lên form
                DisplayInfo(dtInfo.Rows[0], dtMaintenance);

                // 5. Tạo QR Code
                GenerateQR(qrContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private string BuildQRContent(DataRow info, DataTable maintenance)
        {
            var sb = new StringBuilder();
            sb.AppendLine("=== THIET BI UNETI ===");
            sb.AppendLine($"Ma TB: {info["EquipmentID"]}");
            sb.AppendLine($"Ten: {info["EquipmentName"]}");
            sb.AppendLine($"Phan loai: {info["CategoryName"]}");
            sb.AppendLine($"Khoa/Phong: {info["DepartmentName"]}");
            sb.AppendLine($"Ngay nhap: {FormatDate(info["ImportDate"])}");
            sb.AppendLine($"Gia tri: {FormatMoney(info["PurchasePrice"])} VND");
            sb.AppendLine($"Tinh trang: {info["Condition"]}");
            sb.AppendLine($"Bao tri dinh ky: {FormatDate(info["NgayBaoTriDinhKy"])}");
            sb.AppendLine($"Ma vach: {info["MaVach"]}");

            if (maintenance.Rows.Count > 0)
            {
                sb.AppendLine("--- LICH SU BAO TRI ---");
                int count = Math.Min(maintenance.Rows.Count, 5); // Tối đa 5 dòng gần nhất
                for (int i = 0; i < count; i++)
                {
                    var r = maintenance.Rows[i];
                    // ĐÃ SỬA: Không gọi cột MaintenanceType bị thiếu nữa, đưa thẳng Chi phí và Mô tả vào
                    sb.AppendLine($"[{FormatDate(r["MaintenanceDate"])}] Chi phí: {FormatMoney(r["Cost"])}đ - {r["Description"]}");
                }
                if (maintenance.Rows.Count > 5)
                    sb.AppendLine($"...va {maintenance.Rows.Count - 5} lan bao tri khac");
            }
            else
            {
                sb.AppendLine("--- Chua co lich su bao tri ---");
            }

            sb.AppendLine($"Xuat: {DateTime.Now:dd/MM/yyyy HH:mm}");
            return sb.ToString();
        }

        private void DisplayInfo(DataRow info, DataTable maintenance)
        {
            // Cập nhật labels thông tin chính
            lblMaTB.Text = info["EquipmentID"].ToString();
            lblTenTB.Text = info["EquipmentName"].ToString();
            lblPhanLoai.Text = info["CategoryName"].ToString();
            lblKhoaPhong.Text = info["DepartmentName"].ToString();
            lblNgayNhap.Text = FormatDate(info["ImportDate"]);
            lblGiaNhap.Text = FormatMoney(info["PurchasePrice"]) + " VNĐ";
            lblTinhTrang.Text = info["Condition"].ToString();
            lblBaoTri.Text = FormatDate(info["NgayBaoTriDinhKy"]);

            // Tô màu cảnh báo bảo trì
            if (info["NgayBaoTriDinhKy"] != DBNull.Value)
            {
                DateTime ngayBT = Convert.ToDateTime(info["NgayBaoTriDinhKy"]);
                int soNgay = (ngayBT.Date - DateTime.Now.Date).Days;
                if (soNgay < 0)
                    lblBaoTri.ForeColor = Color.Red;
                else if (soNgay <= 15) // Sắp đến hạn <= 15 ngày
                    lblBaoTri.ForeColor = Color.DarkOrange;
                else
                    lblBaoTri.ForeColor = Color.Green;
            }

            // Hiển thị lịch sử bảo trì vào DataGridView
            dgvLichSu.DataSource = maintenance;
            FormatMaintenanceGrid();
        }

        private void FormatMaintenanceGrid()
        {
            // ĐÃ SỬA: Loại bỏ các cột không tồn tại, chỉ format những cột lấy về từ DB
            if (dgvLichSu.Columns.Contains("MaintenanceDate")) dgvLichSu.Columns["MaintenanceDate"].HeaderText = "Ngày BT";
            if (dgvLichSu.Columns.Contains("Description")) dgvLichSu.Columns["Description"].HeaderText = "Mô tả";
            if (dgvLichSu.Columns.Contains("Cost"))
            {
                dgvLichSu.Columns["Cost"].HeaderText = "Chi phí";
                dgvLichSu.Columns["Cost"].DefaultCellStyle.Format = "N0";
            }
        }

        private void GenerateQR(string content)
        {
            try
            {
                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                {
                    QRCodeData qrData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.M);

                    using (PngByteQRCode qrCode = new PngByteQRCode(qrData))
                    {
                        // Khai báo màu dưới dạng mảng byte { R, G, B, Alpha } (255 = không trong suốt)
                        byte[] darkColor = new byte[] { 26, 75, 132, 255 };    // Màu Xanh Navy UNETI
                        byte[] lightColor = new byte[] { 255, 255, 255, 255 }; // Màu Trắng nền

                        // Truyền thẳng giá trị theo thứ tự để tránh lỗi sai tên tham số của các phiên bản cũ
                        // Thứ tự: (kích thước pixel, màu QR, màu nền, có viền trắng không)
                        byte[] qrBytes = qrCode.GetGraphic(6, darkColor, lightColor, true);

                        // Chuyển mảng byte thành Bitmap
                        using (MemoryStream ms = new MemoryStream(qrBytes))
                        {
                            _qrBitmap = new Bitmap(ms);
                        }

                        picQR.Image = _qrBitmap;
                        picQR.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
            }
            catch (Exception ex)
            {
                lblQRStatus.Text = "Lỗi tạo QR: " + ex.Message;
                lblQRStatus.ForeColor = Color.Red;
            }
        }

        // ═══════════════════════════════════════
        // NÚT IN QR
        // ═══════════════════════════════════════
        private void btnInQR_Click(object sender, EventArgs e)
        {
            if (_qrBitmap == null) { MessageBox.Show("Chưa có QR để in!"); return; }

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += (s, ev) =>
            {
                Graphics g = ev.Graphics;
                int pageW = ev.PageBounds.Width;

                // Tiêu đề
                Font titleFont = new Font("Segoe UI", 16, FontStyle.Bold);
                Font infoFont = new Font("Segoe UI", 10);
                Font smallFont = new Font("Segoe UI", 8);
                SolidBrush blueBrush = new SolidBrush(Color.FromArgb(26, 75, 132));
                SolidBrush blackBrush = new SolidBrush(Color.Black);
                SolidBrush grayBrush = new SolidBrush(Color.Gray);

                int y = 20;

                // Header
                g.DrawString("TRƯỜNG ĐH KINH TẾ - KỸ THUẬT CÔNG NGHIỆP", infoFont, blueBrush,
                    new RectangleF(0, y, pageW, 25), new StringFormat { Alignment = StringAlignment.Center });
                y += 22;
                g.DrawString("PHIẾU THÔNG TIN THIẾT BỊ", titleFont, blueBrush,
                    new RectangleF(0, y, pageW, 35), new StringFormat { Alignment = StringAlignment.Center });
                y += 40;

                // Đường kẻ
                g.DrawLine(new Pen(Color.FromArgb(26, 75, 132), 2), 20, y, pageW - 20, y);
                y += 15;

                // QR Code
                int qrSize = 220;
                int qrX = (pageW - qrSize) / 2;
                g.DrawImage(_qrBitmap, new Rectangle(qrX, y, qrSize, qrSize));
                y += qrSize + 10;

                // Tên thiết bị dưới QR
                Font nameFontBig = new Font("Segoe UI", 13, FontStyle.Bold);
                g.DrawString(lblTenTB.Text, nameFontBig, blackBrush,
                    new RectangleF(0, y, pageW, 30), new StringFormat { Alignment = StringAlignment.Center });
                y += 35;

                // Thông tin chi tiết (2 cột)
                Font boldFont = new Font("Segoe UI", 9, FontStyle.Bold);
                int col1X = 30, col2X = pageW / 2 + 10;
                int colW = pageW / 2 - 40;

                void DrawRow(string label, string value, int x, ref int ry)
                {
                    g.DrawString(label, boldFont, blueBrush, x, ry);
                    g.DrawString(value, infoFont, blackBrush, x + 100, ry);
                    ry += 20;
                }

                int y1 = y, y2 = y;
                DrawRow("Mã TB:", lblMaTB.Text, col1X, ref y1);
                DrawRow("Phân loại:", lblPhanLoai.Text, col1X, ref y1);
                DrawRow("Ngày nhập:", lblNgayNhap.Text, col1X, ref y1);
                DrawRow("Tình trạng:", lblTinhTrang.Text, col1X, ref y1);
                DrawRow("Khoa/Phòng:", lblKhoaPhong.Text, col2X, ref y2);
                DrawRow("Giá nhập:", lblGiaNhap.Text, col2X, ref y2);
                DrawRow("Bảo trì ĐK:", lblBaoTri.Text, col2X, ref y2);

                y = Math.Max(y1, y2) + 10;

                // Đường kẻ
                g.DrawLine(new Pen(Color.LightGray, 1), 20, y, pageW - 20, y);
                y += 10;

                // Footer
                g.DrawString($"In ngày: {DateTime.Now:dd/MM/yyyy HH:mm}  |  Quét QR để xem lịch sử bảo trì đầy đủ",
                    smallFont, grayBrush,
                    new RectangleF(0, y, pageW, 20), new StringFormat { Alignment = StringAlignment.Center });
            };

            PrintPreviewDialog preview = new PrintPreviewDialog();
            preview.Document = pd;
            preview.Width = 800;
            preview.Height = 700;
            preview.ShowDialog();
        }

        // ═══════════════════════════════════════
        // NÚT LƯU QR RA FILE
        // ═══════════════════════════════════════
        private void btnLuuQR_Click(object sender, EventArgs e)
        {
            if (_qrBitmap == null) return;
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "PNG Image|*.png|JPEG Image|*.jpg";
                dlg.FileName = $"QR_{_equipmentID}_{DateTime.Now:yyyyMMdd}";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ImageFormat fmt = dlg.FilterIndex == 1 ? ImageFormat.Png : ImageFormat.Jpeg;
                    _qrBitmap.Save(dlg.FileName, fmt);
                    MessageBox.Show($"Đã lưu QR tại:\n{dlg.FileName}", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // ═══════════════════════════════════════
        // HELPERS
        // ═══════════════════════════════════════
        private string FormatDate(object val)
        {
            if (val == DBNull.Value || val == null) return "---";
            return Convert.ToDateTime(val).ToString("dd/MM/yyyy");
        }

        private string FormatMoney(object val)
        {
            if (val == DBNull.Value || val == null) return "---";
            return Convert.ToDecimal(val).ToString("N0");
        }
    }
}