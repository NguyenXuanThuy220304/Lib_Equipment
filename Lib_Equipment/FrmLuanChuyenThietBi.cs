using Lib_Equipment.Database;
using System;
using System.Collections.Generic;
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
    public partial class FrmLuanChuyenThietBi : Form
    {
        private CheckBox headerCheckBox = new CheckBox();

        public FrmLuanChuyenThietBi()
        {
            InitializeComponent();
        }

        private void FrmLuanChuyenThietBi_Load(object sender, EventArgs e)
        {
            LoadKhoaPhong();
            SetupDataGridView();
            LoadNextTransferID();
            this.dtpNgayChuyen.Value = DateTime.Now;
        }

        // 1. TẢI DANH SÁCH KHOA PHÒNG
        private void LoadKhoaPhong()
        {
            string query = "SELECT DepartmentID, DepartmentName FROM Department WHERE IsDeleted = 0 OR IsDeleted IS NULL";
            DataTable dtTu = DataProvider.Instance.ExecuteQuery(query);

            cboTuKhoa.DataSource = dtTu;
            cboTuKhoa.DisplayMember = "DepartmentName";
            cboTuKhoa.ValueMember = "DepartmentID";

            DataTable dtDen = dtTu.Copy();
            cboDenKhoa.DataSource = dtDen;
            cboDenKhoa.DisplayMember = "DepartmentName";
            cboDenKhoa.ValueMember = "DepartmentID";
        }

        // 2. CẤU HÌNH LƯỚI CHỌN (KHÓA SỬA, THÊM CỘT XÁC NHẬN)
        // 1. Sửa lại hàm Setup để chữ "Nét" và "Luxury" hơn
        private void SetupDataGridView()
        {
            dgvThietBi.Columns.Clear();
            dgvThietBi.DataError += (s, e) => { e.ThrowException = false; };

            // Tăng kích thước Font cho toàn bộ lưới
            dgvThietBi.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 11F);
            dgvThietBi.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);

            // Cột 0: Checkbox
            DataGridViewCheckBoxColumn chkCol = new DataGridViewCheckBoxColumn
            {
                Name = "chkSelect",
                HeaderText = "",
                Width = 50,
                FlatStyle = FlatStyle.Flat
            };
            dgvThietBi.Columns.Add(chkCol);

            // Cột thông tin (Khóa sửa, chữ đậm)
            dgvThietBi.Columns.Add(new DataGridViewTextBoxColumn { Name = "EquipmentID", HeaderText = "Mã TB", Width = 120, ReadOnly = true });
            dgvThietBi.Columns.Add(new DataGridViewTextBoxColumn { Name = "EquipmentName", HeaderText = "Tên Thiết Bị", Width = 300, ReadOnly = true });
            dgvThietBi.Columns.Add(new DataGridViewTextBoxColumn { Name = "CurrentCondition", HeaderText = "Hiện trạng", Width = 150, ReadOnly = true });

            // Cột xác nhận của Cán bộ (Chữ to rõ)
            DataGridViewComboBoxColumn cmbActual = new DataGridViewComboBoxColumn
            {
                Name = "ActualCondition",
                HeaderText = "Xác nhận của CB",
                Width = 180,
                FlatStyle = FlatStyle.Flat
            };
            cmbActual.Items.AddRange("Tốt", "Cần bảo trì", "Hỏng");
            dgvThietBi.Columns.Add(cmbActual);

            // Căn chỉnh checkbox Header (Select All)
            //dgvThietBi.Controls.Add(headerCheckBox);
            UpdateHeaderCheckBoxLocation();
        }
        private void UpdateHeaderCheckBoxLocation()
        {
            Rectangle rect = dgvThietBi.GetCellDisplayRectangle(0, -1, true);
            headerCheckBox.Location = new Point(rect.X + (rect.Width - headerCheckBox.Width) / 2, rect.Y + (rect.Height - headerCheckBox.Height) / 2);
        }

        // 3. LOAD THIẾT BỊ VÀO LƯỚI
        private void cboTuKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTuKhoa.SelectedValue != null && cboTuKhoa.SelectedValue is string)
            {
                // Thêm điều kiện NOT IN (Đang bảo trì, Đề xuất thanh lý)
                string sql = @"SELECT EquipmentID, EquipmentName, Condition FROM Equipment 
                       WHERE DepartmentID = @dept AND IsDeleted = 0 
                       AND Condition NOT IN (N'Đang bảo trì', N'Đề xuất thanh lý')";

                DataTable dt = DataProvider.Instance.ExecuteQuery(sql, new SqlParameter[] { new SqlParameter("@dept", cboTuKhoa.SelectedValue.ToString()) });

                dgvThietBi.Rows.Clear();
                foreach (DataRow r in dt.Rows)
                {
                    string dbStatus = r["Condition"].ToString();
                    int idx = dgvThietBi.Rows.Add(false, r["EquipmentID"], r["EquipmentName"], dbStatus);

                    // Gán giá trị mặc định cho ComboBox xác nhận
                    DataGridViewComboBoxColumn col = (DataGridViewComboBoxColumn)dgvThietBi.Columns["ActualCondition"];
                    if (col.Items.Contains(dbStatus))
                        dgvThietBi.Rows[idx].Cells["ActualCondition"].Value = dbStatus;
                    else
                        dgvThietBi.Rows[idx].Cells["ActualCondition"].Value = "Tốt";
                }
                headerCheckBox.Checked = false;
            }
        }

        // 4. LOGIC THỰC HIỆN BÀN GIAO CÓ KIỂM TRA
        private void btnThucHien_Click(object sender, EventArgs e)
        {
            if (cboTuKhoa.SelectedValue.ToString() == cboDenKhoa.SelectedValue.ToString())
            {
                MessageBox.Show("Nơi nhận phải khác nơi giao!", "Cảnh báo"); return;
            }

            int count = 0;
            foreach (DataGridViewRow row in dgvThietBi.Rows) if (Convert.ToBoolean(row.Cells["chkSelect"].Value)) count++;
            if (count == 0) { MessageBox.Show("Vui lòng tích chọn thiết bị cần bàn giao!"); return; }

            string msg = $"Xác nhận cán bộ thiết bị đã kiểm tra {count} máy và thực hiện bàn giao?";
            if (MessageBox.Show(msg, "Xác nhận kiểm tra", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    // Bước 1: Lưu phiếu tổng
                    string sqlRec = "INSERT INTO TransferRecord (FromDepartmentID, ToDepartmentID, CreatedBy, TransferDate, Reason, IsDeleted) VALUES (@f, @t, @u, @d, @r, 0); SELECT SCOPE_IDENTITY();";
                    SqlParameter[] p1 = {
                        new SqlParameter("@f", cboTuKhoa.SelectedValue),
                        new SqlParameter("@t", cboDenKhoa.SelectedValue),
                        new SqlParameter("@u", "ADMIN"), // AppSession.Username
                        new SqlParameter("@d", dtpNgayChuyen.Value),
                        new SqlParameter("@r", txtLyDo.Text)
                    };
                    int newId = Convert.ToInt32(DataProvider.Instance.ExecuteScalar(sqlRec, p1));

                    // Bước 2: Lưu chi tiết & Cập nhật tình trạng MỚI sau khi CB thiết bị xác nhận
                    bool isToKho = cboDenKhoa.Text.ToLower().Contains("kho") && !cboDenKhoa.Text.ToLower().Contains("khoa");

                    foreach (DataGridViewRow row in dgvThietBi.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells["chkSelect"].Value))
                        {
                            string eid = row.Cells["EquipmentID"].Value.ToString();
                            string verifiedStatus = row.Cells["ActualCondition"].Value.ToString();

                            string targetStatus = verifiedStatus; // Mặc định là giữ nguyên đánh giá của cán bộ

                            // ======================================================================
                            // LOGIC TỰ ĐỘNG ĐỔI TRẠNG THÁI THÔNG MINH NẾU MÁY KHÔNG HỎNG
                            // ======================================================================
                            if (verifiedStatus == "Tốt" || verifiedStatus == "Đang sử dụng")
                            {
                                if (isToKho)
                                {
                                    targetStatus = "Tốt"; // Nhập kho cất đi -> Trạng thái sẵn sàng "Tốt"
                                }
                                else
                                {
                                    targetStatus = "Đang sử dụng"; // Giao cho Khoa viện -> Bắt đầu "Đang sử dụng"
                                }
                            }
                            // (Nếu verifiedStatus là "Hỏng nhẹ", "Cần bảo trì"... thì targetStatus tự động giữ nguyên lỗi đó)

                            string sqlDet = $@"INSERT INTO TransferDetail (TransferID, EquipmentID, ConditionAtTransfer) VALUES ({newId}, '{eid}', N'{verifiedStatus}');
                           UPDATE Equipment SET DepartmentID = '{cboDenKhoa.SelectedValue}', Condition = N'{targetStatus}', UpdatedAt = GETDATE() WHERE EquipmentID = '{eid}';";

                            DataProvider.Instance.ExecuteNonQuery(sqlDet, null);
                        }
                    }

                    MessageBox.Show("Cán bộ thiết bị đã ký xác nhận và bàn giao thành công!");
                    XuatBienBanSmartPDF(cboTuKhoa.Text, cboDenKhoa.Text);
                    cboTuKhoa_SelectedIndexChanged(null, null);
                    LoadNextTransferID();
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        // 5. PDF 3 BÊN: GIAO - NHẬN - CÁN BỘ THIẾT BỊ XÁC NHẬN
        private void XuatBienBanSmartPDF(string tu, string den)
        {
            bool isReturn = den.ToLower().Contains("kho") && !den.ToLower().Contains("khoa");

            string fileName = isReturn ? "Bien_Ban_Thu_Hoi.pdf" : "Bien_Ban_Ban_Giao.pdf";
            SaveFileDialog sfd = new SaveFileDialog { Filter = "PDF Files|*.pdf", FileName = fileName, OverwritePrompt = false };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // TRƯỜNG HỢP 1: FILE CHƯA TỒN TẠI -> TẠO MỚI HOÀN TOÀN
                    if (!File.Exists(sfd.FileName))
                    {
                        using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                        {
                            Document doc = new Document(PageSize.A4, 40, 40, 40, 40);
                            PdfWriter.GetInstance(doc, fs);
                            doc.Open();
                            DrawPdfContent(doc, tu, den);
                            doc.Close();
                        }
                    }
                    // TRƯỜNG HỢP 2: FILE ĐÃ CÓ -> GHI TIẾP (APPEND) VÀO TRANG MỚI
                    else
                    {
                        string temp = Path.GetTempFileName();
                        using (PdfReader reader = new PdfReader(sfd.FileName))
                        {
                            using (FileStream fs = new FileStream(temp, FileMode.Create))
                            {
                                // Cách dùng PdfCopy chuẩn nhất để không bị lỗi GetDocument()
                                Document doc = new Document(PageSize.A4);
                                PdfCopy copy = new PdfCopy(doc, fs);
                                doc.Open();

                                // 1. Chép lại toàn bộ các trang cũ
                                for (int i = 1; i <= reader.NumberOfPages; i++)
                                {
                                    copy.AddPage(copy.GetImportedPage(reader, i));
                                }

                                // 2. Tạo nội dung trang mới vào MemoryStream
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    Document newDoc = new Document(PageSize.A4, 40, 40, 40, 40);
                                    PdfWriter.GetInstance(newDoc, ms);
                                    newDoc.Open();
                                    DrawPdfContent(newDoc, tu, den);
                                    newDoc.Close();

                                    // 3. Đọc lại từ Memory và chèn vào trang cuối của file gốc
                                    PdfReader newReader = new PdfReader(ms.ToArray());
                                    copy.AddPage(copy.GetImportedPage(newReader, 1));
                                }
                                doc.Close();
                            }
                        }
                        File.Delete(sfd.FileName);
                        File.Move(temp, sfd.FileName);
                    }
                    MessageBox.Show("Biên bản có chữ ký xác nhận 3 bên đã được lưu thành công!", "Hoàn tất");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xử lý PDF: " + ex.Message, "Lỗi hệ thống");
                }
            }
        }

        private void DrawPdfContent(Document doc, string tu, string den)
        {
            // CÀI ĐẶT FONT CHUẨN ĐỂ KHÔNG SAI CHÍNH TẢ TIẾNG VIỆT
            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "Arial.ttf");
            BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            iTextSharp.text.Font fTitle = new iTextSharp.text.Font(bf, 16, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font fBold = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font fNormal = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font fItalic = new iTextSharp.text.Font(bf, 9, iTextSharp.text.Font.ITALIC);

            // 1. Quốc hiệu tiêu ngữ
            Paragraph header = new Paragraph("TRƯỜNG ĐẠI HỌC KINH TẾ - KỸ THUẬT CÔNG NGHIỆP (UNETI)\nCỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM\nĐộc lập - Tự do - Hạnh phúc", fBold);
            header.Alignment = Element.ALIGN_CENTER;
            doc.Add(header);
            doc.Add(new Paragraph("-------------------------------------------------------", fNormal) { Alignment = Element.ALIGN_CENTER });

            // 2. Tiêu đề linh hoạt theo hướng bàn giao
            string titleStr = den.ToLower().Contains("kho") ? "BIÊN BẢN KIỂM TRA & THU HỒI TÀI SẢN" : "BIÊN BẢN KIỂM TRA & BÀN GIAO TÀI SẢN";
            Paragraph pTitle = new Paragraph("\n" + titleStr, fTitle);
            pTitle.Alignment = Element.ALIGN_CENTER;
            doc.Add(pTitle);

            // 3. Thông tin chung
            doc.Add(new Paragraph($"\nThời gian xác nhận: {DateTime.Now:dd/MM/yyyy HH:mm}", fNormal));
            doc.Add(new Paragraph($"Đơn vị bàn giao: {tu}", fNormal));
            doc.Add(new Paragraph($"Đơn vị tiếp nhận: {den}", fNormal));
            doc.Add(new Paragraph($"Lý do bàn giao: {txtLyDo.Text}", fNormal));

            // 4. Bảng danh sách thiết bị (4 cột)
            PdfPTable table = new PdfPTable(4);
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 8f, 20f, 42f, 30f });
            table.SpacingBefore = 15f;

            // Header bảng
            string[] headers = { "STT", "Mã Thiết Bị", "Tên Thiết Bị", "Hiện trạng (CB xác nhận)" };
            foreach (string h in headers)
            {
                PdfPCell cell = new PdfPCell(new Phrase(h, fBold));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell.Padding = 5;
                table.AddCell(cell);
            }

            // Nội dung bảng lấy từ dgvThietBi
            int stt = 1;
            foreach (DataGridViewRow row in dgvThietBi.Rows)
            {
                if (Convert.ToBoolean(row.Cells["chkSelect"].Value))
                {
                    table.AddCell(new PdfPCell(new Phrase(stt++.ToString(), fNormal)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    table.AddCell(new PdfPCell(new Phrase(row.Cells["EquipmentID"].Value.ToString(), fNormal)));
                    table.AddCell(new PdfPCell(new Phrase(row.Cells["EquipmentName"].Value.ToString(), fNormal)));
                    table.AddCell(new PdfPCell(new Phrase(row.Cells["ActualCondition"].Value?.ToString() ?? "Tốt", fNormal)) { HorizontalAlignment = Element.ALIGN_CENTER });
                }
            }
            doc.Add(table);

            // 5. Cam kết trách nhiệm
            doc.Add(new Paragraph("\nCAM KẾT TRÁCH NHIỆM:", fBold));
            string camKet = "- Các bên đã thực hiện kiểm tra thực tế tình trạng thiết bị tại thời điểm bàn giao. Đơn vị nhận có trách nhiệm quản lý, bảo quản đúng quy định. Nếu xảy ra hư hỏng, mất mát do lỗi chủ quan sau thời điểm này, đơn vị nhận cam kết đền bù 100% giá trị tài sản.";
            doc.Add(new Paragraph(camKet, fItalic));

            // 6. Chữ ký 3 bên (Cân đối)
            PdfPTable sign = new PdfPTable(3);
            sign.WidthPercentage = 100;
            sign.SpacingBefore = 40f;
            sign.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

            sign.AddCell(new PdfPCell(new Phrase("ĐẠI DIỆN BÊN GIAO\n(Ký và ghi rõ họ tên)", fBold)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
            sign.AddCell(new PdfPCell(new Phrase("CÁN BỘ THIẾT BỊ\n(Xác nhận thực tế)", fBold)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
            sign.AddCell(new PdfPCell(new Phrase("ĐẠI DIỆN BÊN NHẬN\n(Ký và ghi rõ họ tên)", fBold)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
            doc.Add(sign);
        }

        private void LoadNextTransferID()
        {
            try
            {
                string sql = "SELECT ISNULL(MAX(TransferID), 0) + 1 FROM TransferRecord";
                txtMaPhieu.Text = "LC_" + Convert.ToInt32(DataProvider.Instance.ExecuteScalar(sql)).ToString("D3");
            }
            catch { txtMaPhieu.Text = "LC_001"; }
        }
    }
}