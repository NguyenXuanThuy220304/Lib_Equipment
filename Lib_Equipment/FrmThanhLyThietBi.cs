using Lib_Equipment.Database;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Rectangle = System.Drawing.Rectangle;

namespace Lib_Equipment
{
    public partial class FrmThanhLyThietBi : Form
    {
        public FrmThanhLyThietBi()
        {
            InitializeComponent();
        }

        private void FrmThanhLyThietBi_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadDanhSachChoThanhLy();
            LoadNextLiquidationID();
            dtpNgayThanhLy.Value = DateTime.Now;
        }

        // 1. CẤU HÌNH LƯỚI (LUXURY SETUP)
        private void SetupDataGridView()
        {
            dgvThietBi.Columns.Clear();
            dgvThietBi.AllowUserToAddRows = false;
            dgvThietBi.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 11F);
            dgvThietBi.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 12F, FontStyle.Bold);

            // Cột Checkbox để chọn máy cần bán
            DataGridViewCheckBoxColumn chkCol = new DataGridViewCheckBoxColumn
            {
                Name = "chkSelect",
                HeaderText = "Chọn",
                Width = 60,
                FlatStyle = FlatStyle.Flat
            };
            dgvThietBi.Columns.Add(chkCol);

            dgvThietBi.Columns.Add(new DataGridViewTextBoxColumn { Name = "EquipmentID", HeaderText = "Mã TB", Width = 120, ReadOnly = true });
            dgvThietBi.Columns.Add(new DataGridViewTextBoxColumn { Name = "EquipmentName", HeaderText = "Tên Thiết Bị", Width = 280, ReadOnly = true });
            dgvThietBi.Columns.Add(new DataGridViewTextBoxColumn { Name = "PurchasePrice", HeaderText = "Nguyên giá (VNĐ)", Width = 150, ReadOnly = true });
            dgvThietBi.Columns.Add(new DataGridViewTextBoxColumn { Name = "Condition", HeaderText = "Hiện trạng", Width = 150, ReadOnly = true });

            // Format cột tiền tệ
            dgvThietBi.Columns["PurchasePrice"].DefaultCellStyle.Format = "N0";
        }

        // 2. TẢI DANH SÁCH CÁC MÁY "CHỜ THANH LÝ" HOẶC "HỎNG HOÀN TOÀN"
        private void LoadDanhSachChoThanhLy()
        {
            string sql = @"SELECT EquipmentID, EquipmentName, PurchasePrice, Condition 
                           FROM Equipment 
                           WHERE IsDeleted = 0 
                           AND Condition IN (N'Đề xuất thanh lý', N'Hỏng hoàn toàn')";

            DataTable dt = DataProvider.Instance.ExecuteQuery(sql);
            dgvThietBi.Rows.Clear();
            foreach (DataRow r in dt.Rows)
            {
                dgvThietBi.Rows.Add(false, r["EquipmentID"], r["EquipmentName"], r["PurchasePrice"], r["Condition"]);
            }
        }

        // 3. TẠO MÃ PHIẾU TỰ ĐỘNG
        private void LoadNextLiquidationID()
        {
            try
            {
                string sql = "SELECT ISNULL(MAX(LiquidationID), 0) + 1 FROM LiquidationRecord";
                txtMaPhieu.Text = "TL_" + DateTime.Now.ToString("yyyyMMdd") + "_" + Convert.ToInt32(DataProvider.Instance.ExecuteScalar(sql)).ToString("D3");
            }
            catch { txtMaPhieu.Text = "TL_" + DateTime.Now.ToString("yyyyMMdd") + "_001"; }
        }

        // 4. XỬ LÝ THANH LÝ HÀNG LOẠT
        private void btnThanhLy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNguoiMua.Text))
            {
                MessageBox.Show("Vui lòng nhập tên Đơn vị thu mua / Người mua!"); return;
            }

            int count = 0;
            foreach (DataGridViewRow row in dgvThietBi.Rows)
                if (Convert.ToBoolean(row.Cells["chkSelect"].Value)) count++;

            if (count == 0) { MessageBox.Show("Vui lòng chọn ít nhất 1 thiết bị để thanh lý!"); return; }

            string msg = $"Xác nhận thanh lý lô tài sản gồm {count} thiết bị?\nHành động này sẽ cập nhật thiết bị thành trạng thái 'Đã thanh lý' và không thể hoàn tác.";
            if (MessageBox.Show(msg, "Duyệt Thanh Lý", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    // Ép kiểu an toàn số tiền thu hồi
                    double.TryParse(txtTongTienThuHoi.Text.Replace(",", ""), out double totalValue);

                    // BƯỚC 1: Lưu Phiếu tổng
                    string sqlRec = @"INSERT INTO LiquidationRecord (LiquidationCode, LiquidationDate, BuyerName, TotalRecoveryValue, Reason, CreatedBy) 
                                      VALUES (@code, @date, @buyer, @val, @reason, @user); 
                                      SELECT SCOPE_IDENTITY();";

                    SqlParameter[] p1 = {
                        new SqlParameter("@code", txtMaPhieu.Text),
                        new SqlParameter("@date", dtpNgayThanhLy.Value),
                        new SqlParameter("@buyer", txtNguoiMua.Text),
                        new SqlParameter("@val", totalValue),
                        new SqlParameter("@reason", txtLyDo.Text),
                        new SqlParameter("@user", "ADMIN")
                    };
                    int newId = Convert.ToInt32(DataProvider.Instance.ExecuteScalar(sqlRec, p1));

                    // BƯỚC 2: Lưu Chi tiết & Xóa sổ khỏi kho
                    foreach (DataGridViewRow row in dgvThietBi.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells["chkSelect"].Value))
                        {
                            string eid = row.Cells["EquipmentID"].Value.ToString();
                            string condition = row.Cells["Condition"].Value.ToString();

                            string sqlDet = $@"
                                -- Thêm vào chi tiết thanh lý
                                INSERT INTO LiquidationDetail (LiquidationID, EquipmentID, Notes) 
                                VALUES ({newId}, '{eid}', N'{condition}');
                                
                                -- Xóa sổ thiết bị (Vẫn giữ record nhưng ẩn đi và đánh dấu Đã thanh lý)
                                UPDATE Equipment 
                                SET Condition = N'Đã thanh lý', IsDeleted = 1, UpdatedAt = GETDATE() 
                                WHERE EquipmentID = '{eid}';";

                            DataProvider.Instance.ExecuteNonQuery(sqlDet, null);
                        }
                    }

                    MessageBox.Show("Thanh lý tài sản thành công! Bắt đầu xuất Biên bản PDF...", "Hoàn tất");
                    XuatBienBanThanhLyPDF();
                    LoadDanhSachChoThanhLy();
                    LoadNextLiquidationID();
                    txtLyDo.Clear();
                    txtTongTienThuHoi.Clear();
                    txtNguoiMua.Clear();
                    
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        // 5. XUẤT BIÊN BẢN HỘI ĐỒNG THANH LÝ TÀI SẢN (PDF)
        private void XuatBienBanThanhLyPDF()
        {
            SaveFileDialog sfd = new SaveFileDialog { Filter = "PDF Files|*.pdf", FileName = $"Bien_Ban_Thanh_Ly_{txtMaPhieu.Text}.pdf" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                    {
                        Document doc = new Document(PageSize.A4, 40, 40, 50, 40);
                        PdfWriter.GetInstance(doc, fs);
                        doc.Open();

                        // Font tiếng Việt
                        string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "Arial.ttf");
                        BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                        iTextSharp.text.Font fTitle = new iTextSharp.text.Font(bf, 16, iTextSharp.text.Font.BOLD);
                        iTextSharp.text.Font fBold = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.BOLD);
                        iTextSharp.text.Font fNormal = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.NORMAL);

                        // Header
                        Paragraph header = new Paragraph("TRƯỜNG ĐẠI HỌC KINH TẾ - KỸ THUẬT CÔNG NGHIỆP (UNETI)\nCỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM\nĐộc lập - Tự do - Hạnh phúc", fBold);
                        header.Alignment = Element.ALIGN_CENTER;
                        doc.Add(header);
                        doc.Add(new Paragraph("-------------------------------------------------------", fNormal) { Alignment = Element.ALIGN_CENTER });

                        // Title
                        Paragraph pTitle = new Paragraph("\nBIÊN BẢN HỘI ĐỒNG THANH LÝ TÀI SẢN", fTitle);
                        pTitle.Alignment = Element.ALIGN_CENTER;
                        doc.Add(pTitle);

                        // Thông tin chung
                        doc.Add(new Paragraph($"\nMã phiếu: {txtMaPhieu.Text}   -   Ngày lập: {dtpNgayThanhLy.Value:dd/MM/yyyy}", new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.ITALIC)));
                        doc.Add(new Paragraph($"\nĐơn vị / Người thu mua: {txtNguoiMua.Text}", fBold));
                        doc.Add(new Paragraph($"Lý do thanh lý: {txtLyDo.Text}", fNormal));
                        double.TryParse(txtTongTienThuHoi.Text.Replace(",", ""), out double tongTien);
                        doc.Add(new Paragraph($"Tổng giá trị thu hồi: {tongTien:N0} VNĐ", fBold));

                        // Bảng Thiết bị
                        PdfPTable table = new PdfPTable(4);
                        table.WidthPercentage = 100;
                        table.SetWidths(new float[] { 8f, 22f, 45f, 25f });
                        table.SpacingBefore = 15f;

                        string[] headers = { "STT", "Mã Thiết Bị", "Tên Thiết Bị", "Nguyên giá (VNĐ)" };
                        foreach (string h in headers)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(h, fBold)) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = BaseColor.LIGHT_GRAY, Padding = 6 };
                            table.AddCell(cell);
                        }

                        int stt = 1;
                        foreach (DataGridViewRow row in dgvThietBi.Rows)
                        {
                            if (Convert.ToBoolean(row.Cells["chkSelect"].Value))
                            {
                                table.AddCell(new PdfPCell(new Phrase(stt++.ToString(), fNormal)) { HorizontalAlignment = Element.ALIGN_CENTER });
                                table.AddCell(new PdfPCell(new Phrase(row.Cells["EquipmentID"].Value.ToString(), fNormal)));
                                table.AddCell(new PdfPCell(new Phrase(row.Cells["EquipmentName"].Value.ToString(), fNormal)));

                                // Format tiền tệ cho cột Nguyên giá trong PDF
                                double.TryParse(row.Cells["PurchasePrice"].Value.ToString(), out double ngGia);
                                table.AddCell(new PdfPCell(new Phrase(ngGia.ToString("N0"), fNormal)) { HorizontalAlignment = Element.ALIGN_RIGHT });
                            }
                        }
                        doc.Add(table);

                        // Lời kết & Chữ ký
                        doc.Add(new Paragraph("\nTài sản trên đã được bàn giao cho đơn vị thu mua. Kế toán tiến hành ghi giảm tài sản trên sổ sách theo đúng quy định hiện hành.", fNormal));

                        PdfPTable sign = new PdfPTable(3);
                        sign.WidthPercentage = 100;
                        sign.SpacingBefore = 40f;
                        sign.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                        sign.AddCell(new PdfPCell(new Phrase("ĐẠI DIỆN TRƯỜNG\n(Ký và ghi rõ họ tên)", fBold)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                        sign.AddCell(new PdfPCell(new Phrase("KẾ TOÁN TRƯỞNG\n(Ký và ghi rõ họ tên)", fBold)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                        sign.AddCell(new PdfPCell(new Phrase("ĐƠN VỊ THU MUA\n(Ký và ghi rõ họ tên)", fBold)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                        doc.Add(sign);

                        doc.Close();
                    }
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
                }
                catch (Exception ex) { MessageBox.Show("Lỗi tạo PDF: " + ex.Message); }
            }
        }

        private void dgvThietBi_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string id = dgvThietBi.Rows[e.RowIndex].Cells["EquipmentID"].Value.ToString();
                string name = dgvThietBi.Rows[e.RowIndex].Cells["EquipmentName"].Value.ToString();
                FrmHoSoThietBi frm = new FrmHoSoThietBi(id, name);
                frm.ShowDialog();
            }
        }

        private void btnTuChoi_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow row in dgvThietBi.Rows)
                if (Convert.ToBoolean(row.Cells["chkSelect"].Value)) count++;

            if (count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 thiết bị để từ chối thanh lý!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string msg = $"Xác nhận TỪ CHỐI thanh lý lô {count} thiết bị này?\n\nHành động này sẽ:\n1. Đổi trạng thái máy thành 'Tốt'.\n2. Điều chuyển thiết bị về Kho chung và lưu vết lịch sử.";

            if (MessageBox.Show(msg, "Từ chối thanh lý", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    // BƯỚC 1: Tự động tìm ID của "Kho" trong hệ thống
                    string findKhoSql = "SELECT TOP 1 DepartmentID FROM Department WHERE DepartmentName LIKE N'%Kho%' AND IsDeleted = 0";
                    object khoObj = DataProvider.Instance.ExecuteScalar(findKhoSql);

                    if (khoObj == null)
                    {
                        MessageBox.Show("Không tìm thấy 'Kho' trong danh mục Khoa/Phòng. Vui lòng thiết lập Kho trước!", "Lỗi cấu hình", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string khoID = khoObj.ToString();

                    // BƯỚC 2: Sinh ra 1 Phiếu Luân Chuyển Ảo để lưu vết (Audit Trail)
                    string sqlRec = @"INSERT INTO TransferRecord (FromDepartmentID, ToDepartmentID, CreatedBy, TransferDate, Reason, IsDeleted) 
                              VALUES (@kho, @kho, @user, GETDATE(), N'Hệ thống tự động: Từ chối thanh lý, thu hồi tài sản về Kho', 0); 
                              SELECT SCOPE_IDENTITY();";

                    SqlParameter[] paramRec = {
                new SqlParameter("@kho", khoID),
                new SqlParameter("@user", "ADMIN") // Thay bằng AppSession.Username của bạn nếu có
            };
                    int newTransferId = Convert.ToInt32(DataProvider.Instance.ExecuteScalar(sqlRec, paramRec));

                    // BƯỚC 3: Vòng lặp xử lý từng thiết bị
                    foreach (DataGridViewRow row in dgvThietBi.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells["chkSelect"].Value))
                        {
                            string eid = row.Cells["EquipmentID"].Value.ToString();

                            // Cập nhật Thiết bị VÀ Insert Chi tiết Luân chuyển cùng lúc
                            string sqlUpdateAndTrace = $@"
                        -- 1. Lưu vết vào lịch sử luân chuyển với trạng thái 'Tốt'
                        INSERT INTO TransferDetail (TransferID, EquipmentID, ConditionAtTransfer) 
                        VALUES ({newTransferId}, '{eid}', N'Tốt');

                        -- 2. Đổi trạng thái thiết bị thành 'Tốt' và đẩy về Kho
                        UPDATE Equipment 
                        SET Condition = N'Tốt', DepartmentID = '{khoID}', UpdatedAt = GETDATE() 
                        WHERE EquipmentID = '{eid}';";

                            DataProvider.Instance.ExecuteNonQuery(sqlUpdateAndTrace, null);
                        }
                    }

                    MessageBox.Show("Đã từ chối thanh lý và điều chuyển thiết bị về Kho thành công!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Làm mới lại lưới danh sách
                    LoadDanhSachChoThanhLy();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xử lý từ chối thanh lý: " + ex.Message, "Lỗi CSDL");
                }
            }
        }
    }
}