using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text; // Đảm bảo đã cài NuGet iTextSharp
using iTextSharp.text.pdf;

namespace Lib_Equipment
{
    public partial class FrmBarcodeViewer : Form
    {
        private string _barcodeText;
        private string _equipmentName;

        public FrmBarcodeViewer(string barcodeText, string equipmentName = "N/A")
        {
            InitializeComponent();
            _barcodeText = barcodeText;
            _equipmentName = equipmentName;
            this.Text = "Hệ thống mã vạch - " + _barcodeText;
        }

        private void FrmBarcodeViewer_Load(object sender, EventArgs e)
        {
            // 1. TÍNH TOÁN KÍCH THƯỚC FORM LINH ĐỘNG
            using (Graphics g = this.CreateGraphics())
            {
                System.Drawing.Font fontName = new System.Drawing.Font("Consolas", 14, FontStyle.Bold);
                SizeF sizeName = g.MeasureString("Tên: " + _equipmentName, fontName);

                // Chiều rộng: Tối thiểu 650px, hoặc rộng hơn nếu tên quá dài
                int calculatedWidth = Math.Max(650, (int)sizeName.Width + 120);

                this.Width = calculatedWidth;

                // CHIỀU CAO (ĐỘ CAO): Tăng lên 550px để nút không đè lên chữ
                this.Height = 550;
            }

            // 2. ĐẶT NÚT Ở VỊ TRÍ CỐ ĐỊNH CÁCH ĐÁY 20PX
            // Đảm bảo nút luôn nằm dưới cùng, không đè lên nội dung vẽ
            btnExportPDF.Location = new Point((this.ClientSize.Width - btnExportPDF.Width) / 2,
                                             this.ClientSize.Height - btnExportPDF.Height - 30);
        }

        // Điều chỉnh lại tọa độ Y trong hàm vẽ để mọi thứ cao lên một chút
        private void DrawBarcodeLogic(Graphics g, int canvasWidth, int canvasHeight)
        {
            g.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;

            // Tiêu đề (Y = 25)
            System.Drawing.Font fontTitle = new System.Drawing.Font("Segoe UI", 22, FontStyle.Bold);
            g.DrawString("THÔNG TIN THIẾT BỊ", fontTitle, Brushes.Black, new RectangleF(0, 25, canvasWidth, 50), sf);

            // Mã vạch (Y = 85, Chiều cao = 120) -> Kết thúc tại Y = 205
            int barWidth = (int)(canvasWidth * 0.75);
            int barHeight = 120;
            int startX = (canvasWidth - barWidth) / 2;
            int barY = 85;

            Random rnd = new Random(_barcodeText.GetHashCode());
            int currentX = startX;
            while (currentX < startX + barWidth)
            {
                int lineWidth = rnd.Next(2, 5);
                g.FillRectangle(Brushes.Black, currentX, barY, lineWidth, barHeight);
                currentX += lineWidth + rnd.Next(1, 4);
            }

            // Thông tin chữ (Bắt đầu từ Y = 230)
            System.Drawing.Font fontInfo = new System.Drawing.Font("Consolas", 15, FontStyle.Bold);

            // Vẽ Mã số (Y = 230)
            g.DrawString($"Mã số: {_barcodeText}", fontInfo, Brushes.Black, new RectangleF(0, 230, canvasWidth, 35), sf);

            // Vẽ Tên (Y = 275)
            g.DrawString($"Tên: {_equipmentName}", fontInfo, Brushes.Black, new RectangleF(0, 275, canvasWidth, 35), sf);

            // Ngày giờ (Y = 325)
            System.Drawing.Font fontDate = new System.Drawing.Font("Arial", 10, FontStyle.Italic);
            g.DrawString($"Ngày xuất: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}", fontDate, Brushes.Gray, new RectangleF(0, 325, canvasWidth, 25), sf);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawBarcodeLogic(e.Graphics, this.ClientSize.Width, this.ClientSize.Height);
        }

        private System.Drawing.Image GetBarcodeImage()
        {
            // Xuất ảnh chất lượng cực cao cho PDF (2000px chiều rộng để in ra không bao giờ bị vỡ)
            Bitmap bmp = new Bitmap(1000, 600);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                DrawBarcodeLogic(g, bmp.Width, bmp.Height);
            }
            return bmp;
        }

        // =======================================================
        // LOGIC XUẤT PDF (SMART APPEND/OVERWRITE)
        // =======================================================
        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF Files (*.pdf)|*.pdf";
            sfd.FileName = "DanhMucMaVach.pdf";
            sfd.OverwritePrompt = false;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (!File.Exists(sfd.FileName)) CreateNewPdf(sfd.FileName);
                    else ProcessSmartUpdate(sfd.FileName);
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        private void CreateNewPdf(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                Document doc = new Document(PageSize.A4);
                PdfWriter.GetInstance(doc, fs);
                doc.Open();
                AddBarcodePage(doc);
                doc.Close();
            }
            MessageBox.Show("Đã tạo file mới và lưu mã vạch thành công!", "Thông báo");
        }

        private void ProcessSmartUpdate(string path)
        {
            string tempFile = Path.GetTempFileName();
            bool isUpdated = false;
            using (PdfReader reader = new PdfReader(path))
            {
                using (FileStream fs = new FileStream(tempFile, FileMode.Create))
                {
                    Document doc = new Document(reader.GetPageSizeWithRotation(1));
                    PdfCopy copy = new PdfCopy(doc, fs);
                    doc.Open();
                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        string pageText = iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, i);
                        // Nếu tìm thấy mã thiết bị này đã tồn tại trong file PDF
                        if (pageText.Contains("ID_REF: " + _barcodeText))
                        {
                            AddBarcodePage(doc); // Ghi đè trang mới nhất vào
                            isUpdated = true;
                        }
                        else copy.AddPage(copy.GetImportedPage(reader, i));
                    }
                    // Nếu chưa có mã này thì ghi tiếp vào cuối
                    if (!isUpdated) AddBarcodePage(doc);
                    doc.Close();
                }
            }
            File.Delete(path); File.Move(tempFile, path);
            MessageBox.Show(isUpdated ? "Mã này đã có, hệ thống đã cập nhật thông tin mới!" : "Đã thêm mã vạch mới vào danh sách!");
        }

        private void AddBarcodePage(Document doc)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                GetBarcodeImage().Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(ms.ToArray());
                img.Alignment = Element.ALIGN_CENTER;
                img.ScaleToFit(500f, 400f);
                img.SpacingBefore = 30f;
                doc.Add(img);

                // Dòng chữ ẩn giúp code nhận diện ID khi quét file PDF để cập nhật
                Paragraph p = new Paragraph("ID_REF: " + _barcodeText);
                p.Font.Color = BaseColor.WHITE;
                doc.Add(p);
            }
        }
    }
}