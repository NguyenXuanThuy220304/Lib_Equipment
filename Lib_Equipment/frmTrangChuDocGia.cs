using Lib_Equipment.BLL;
using Lib_Equipment.DAO;
using Lib_Equipment.DTO;
using Lib_Equipment.Helpers;
using Lib_Equipment.Database;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Lib_Equipment
{
    public partial class frmTrangChuDocGia : Form
    {
        private string currentReaderID;
        private string currentFullName;
        private readonly string API_KEY = AppSecrets.GeminiApiKey;

        private Button btnHanhDongGiaHan;
        private Button btnHanhDongTraSach;
        private Button btnThanhToanNo; // Nút thanh toán nợ

        public frmTrangChuDocGia(string readerID, string fullName)
        {
            InitializeComponent();
            currentReaderID = readerID;
            currentFullName = fullName;
            this.KeyPreview = true;

            TaoNutThanhToanNo();
            UpdateDebtDisplay();
            lblWelcome.Text = $"Xin chào, {currentFullName} | Mã: {currentReaderID}";

            pnlBarcodeCard.Cursor = Cursors.Hand;
            lblBarcodeTitle.Cursor = Cursors.Hand;
            lblBarcodeDesc.Cursor = Cursors.Hand;

            pnlBarcodeCard.Click += HandleOpenBorrowForm_Click;
            lblBarcodeTitle.Click += HandleOpenBorrowForm_Click;
            lblBarcodeDesc.Click += HandleOpenBorrowForm_Click;

            TaoNutBanhDongThongMinh();
            dgvMain.SelectionChanged += DgvMain_SelectionChanged;
        }

        private void frmTrangChuDocGia_Load(object sender, EventArgs e) { LoadLichSuMuon(); }

        private void TaoNutThanhToanNo()
        {
            btnThanhToanNo = new Button() { Text = "💳 THANH TOÁN", Size = new Size(130, 35), BackColor = Color.FromArgb(40, 167, 69), ForeColor = Color.White, Font = new Font("Segoe UI", 10, FontStyle.Bold), FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand, Visible = false };
            btnThanhToanNo.Location = new Point(lblCongNo.Right + 20, lblCongNo.Top);
            btnThanhToanNo.Click += BtnThanhToanNo_Click;

            // Add thẳng vào pnlHeader cạnh cái lblCongNo của bạn
            pnlHeader.Controls.Add(btnThanhToanNo);
            btnThanhToanNo.BringToFront();
        }

        private void UpdateDebtDisplay()
        {
            string query = "SELECT ISNULL(AcademicDebt, 0) FROM Reader WHERE ReaderID = @id";
            decimal debt = Convert.ToDecimal(DataProvider.Instance.ExecuteScalar(query, new SqlParameter[] { new SqlParameter("@id", currentReaderID) }));

            if (debt > 0)
            {
                lblCongNo.Text = $"Công nợ: {debt:N0} VNĐ";
                lblCongNo.ForeColor = Color.Orange;
                btnThanhToanNo.Location = new Point(lblCongNo.Right + 20, lblCongNo.Top);
                btnThanhToanNo.Visible = true; // Hiện nút thanh toán khi có nợ
            }
            else
            {
                lblCongNo.Text = "Công nợ: 0 VNĐ";
                lblCongNo.ForeColor = Color.LightGreen;
                btnThanhToanNo.Visible = false;
            }
        }

        private void BtnThanhToanNo_Click(object sender, EventArgs e)
        {
            // Giao diện quét QR Code giả lập
            Form frmPay = new Form() { Text = "CỔNG THANH TOÁN UNETI-PAY", Size = new Size(400, 500), StartPosition = FormStartPosition.CenterParent, BackColor = Color.White };

            Label lbl = new Label() { Text = "QUÉT MÃ ĐỂ THANH TOÁN CÔNG NỢ", Top = 20, Width = 400, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 12, FontStyle.Bold) };

            PictureBox picQR = new PictureBox() { Size = new Size(250, 250), Top = 60, Left = 65, BorderStyle = BorderStyle.FixedSingle, SizeMode = PictureBoxSizeMode.CenterImage };
            // Tạo 1 ảnh QR trắng tượng trưng
            Bitmap bmp = new Bitmap(250, 250);
            using (Graphics g = Graphics.FromImage(bmp)) { g.Clear(Color.LightGray); g.DrawString("MÃ QR NGÂN HÀNG\n(Chèn ảnh thật vào đây)", new Font("Segoe UI", 10), Brushes.Black, new PointF(40, 100)); }
            picQR.Image = bmp;

            Button btnXacNhan = new Button() { Text = "TÔI ĐÃ CHUYỂN KHOẢN XONG", Top = 340, Left = 50, Width = 280, Height = 50, BackColor = Color.FromArgb(40, 167, 69), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold), Cursor = Cursors.Hand };

            btnXacNhan.Click += (s, ev) => {
                if (MessageBox.Show("Hệ thống sẽ đối soát với Ngân hàng. Bạn chắc chắn đã thanh toán?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Xóa nợ trong Database
                    string sql = "UPDATE Reader SET AcademicDebt = 0 WHERE ReaderID = @id";
                    DataProvider.Instance.ExecuteNonQuery(sql, new SqlParameter[] { new SqlParameter("@id", currentReaderID) });

                    MessageBox.Show("Thanh toán thành công! Công nợ của bạn đã về 0.", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmPay.Close();
                    UpdateDebtDisplay();
                }
            };

            frmPay.Controls.AddRange(new Control[] { lbl, picQR, btnXacNhan });
            frmPay.ShowDialog();
        }

        private void HandleOpenBorrowForm_Click(object sender, EventArgs e)
        {
            frmSecondaryBorrow frmPhu = new frmSecondaryBorrow(currentReaderID, currentFullName, API_KEY);
            frmPhu.ShowDialog();
            LoadLichSuMuon();
            UpdateDebtDisplay();
        }

        private void TaoNutBanhDongThongMinh()
        {
            btnHanhDongGiaHan = new Button() { Text = "⏳ GIA HẠN SÁCH NÀY", Size = new Size(180, 40), BackColor = Color.FromArgb(255, 193, 7), Font = new Font("Segoe UI", 10, FontStyle.Bold), FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand, Visible = false };
            btnHanhDongGiaHan.Location = new Point(dgvMain.Right - 380, dgvMain.Top - 50);
            btnHanhDongGiaHan.Click += BtnHanhDongGiaHan_Click;
            this.Controls.Add(btnHanhDongGiaHan);

            btnHanhDongTraSach = new Button() { Text = "📦 TỰ TRẢ SÁCH NÀY", Size = new Size(180, 40), BackColor = Color.FromArgb(40, 167, 69), ForeColor = Color.White, Font = new Font("Segoe UI", 10, FontStyle.Bold), FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand, Visible = false };
            btnHanhDongTraSach.Location = new Point(dgvMain.Right - 180, dgvMain.Top - 50);
            btnHanhDongTraSach.Click += BtnHanhDongTraSach_Click;
            this.Controls.Add(btnHanhDongTraSach);
        }

        private void DgvMain_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMain.CurrentRow == null || dgvMain.CurrentRow.Index < 0 || !dgvMain.Columns.Contains("Trạng Thái"))
            {
                btnHanhDongGiaHan.Visible = btnHanhDongTraSach.Visible = false;
                return;
            }

            DataGridViewRow row = dgvMain.CurrentRow;
            string trangThai = row.Cells["Trạng Thái"].Value?.ToString();

            if (trangThai == "Đang mượn" && dgvMain.Columns.Contains("Hạn Trả"))
            {
                DateTime dueDate = Convert.ToDateTime(row.Cells["Hạn Trả"].Value);
                int daysLeft = (dueDate.Date - DateTime.Now.Date).Days;
                btnHanhDongTraSach.Visible = true;
                btnHanhDongGiaHan.Visible = (daysLeft >= 3);
            }
            else
            {
                btnHanhDongGiaHan.Visible = btnHanhDongTraSach.Visible = false;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e) { TimKiemSachThongMinh(); }
        private void txtSearch_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; TimKiemSachThongMinh(); } }

        private void TimKiemSachThongMinh()
        {
            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword)) { LoadLichSuMuon(); return; }

            lblGridTitle.Text = $"🔍 Tìm kiếm kho sách: '{keyword}'";

            string acronym = "%";
            foreach (char c in keyword) if (char.IsLetterOrDigit(c)) acronym += c + "%";

            string[] words = keyword.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            List<SqlParameter> paramList = new List<SqlParameter>();
            string wordConditions = "";

            for (int i = 0; i < words.Length; i++)
            {
                string pName = $"@word{i}";
                wordConditions += $"(b.Title COLLATE SQL_Latin1_General_CP1_CI_AI LIKE {pName} " +
                                  $"OR b.Author COLLATE SQL_Latin1_General_CP1_CI_AI LIKE {pName} " +
                                  $"OR bcg.CategoryName COLLATE SQL_Latin1_General_CP1_CI_AI LIKE {pName})";

                if (i < words.Length - 1) wordConditions += " AND ";
                paramList.Add(new SqlParameter(pName, $"%{words[i]}%"));
            }

            paramList.Add(new SqlParameter("@acronym", acronym));

            string query = $@"
                SELECT 
                    b.CabinetLocation AS [Vị trí kệ],
                    b.Title AS [Tên Sách], 
                    b.Author AS [Tác Giả], 
                    bcg.CategoryName AS [Thể Loại],
                    (SELECT COUNT(*) FROM BookCopy bc 
                     WHERE bc.BookID = b.BookID AND bc.Status = N'Có sẵn' AND bc.IsDeleted = 0) AS [Số quyển sẵn sàng]
                FROM Book b
                LEFT JOIN BookCategory bcg ON b.CategoryID = bcg.CategoryID
                WHERE b.IsDeleted = 0 
                  AND (
                        ({wordConditions})            
                        OR b.Title LIKE @acronym      
                        OR bcg.CategoryName LIKE @acronym 
                      )
                ORDER BY b.Title ASC";

            dgvMain.DataSource = DataProvider.Instance.ExecuteQuery(query, paramList.ToArray());
            DinhDangCotViTri();
        }

        private void LoadLichSuMuon()
        {
            lblGridTitle.Text = "📚 Lịch sử mượn sách của bạn";
            string query = @"
                SELECT 
                    b.CabinetLocation AS [Vị trí kệ],
                    br.RecordID AS [Mã Phiếu], bc.CopyID AS [Mã Bản Sao], b.Title AS [Tên Sách], 
                    br.BorrowDate AS [Ngày Mượn], br.DueDate AS [Hạn Trả],
                    CASE 
                        WHEN bd.ReturnDate IS NULL THEN N'Đang mượn' 
                        WHEN bc.Status = N'Chờ kiểm duyệt' AND bd.RecordID = (SELECT MAX(RecordID) FROM BorrowDetail WHERE CopyID = bd.CopyID) THEN N'Chờ kiểm duyệt'
                        ELSE N'Đã trả' 
                    END AS [Trạng Thái]
                FROM BorrowRecord br
                JOIN BorrowDetail bd ON br.RecordID = bd.RecordID
                JOIN BookCopy bc ON bd.CopyID = bc.CopyID
                JOIN Book b ON bc.BookID = b.BookID
                WHERE br.ReaderID = @readerId AND br.IsDeleted = 0
                ORDER BY CASE WHEN bd.ReturnDate IS NULL OR (bc.Status = N'Chờ kiểm duyệt' AND bd.RecordID = (SELECT MAX(RecordID) FROM BorrowDetail WHERE CopyID = bd.CopyID)) THEN 0 ELSE 1 END, br.BorrowDate DESC";

            dgvMain.DataSource = DataProvider.Instance.ExecuteQuery(query, new SqlParameter[] { new SqlParameter("@readerId", currentReaderID) });
            DinhDangCotViTri();
        }

        private void DinhDangCotViTri()
        {
            if (dgvMain.Columns.Contains("Vị trí kệ"))
            {
                dgvMain.Columns["Vị trí kệ"].DisplayIndex = 0;
                dgvMain.Columns["Vị trí kệ"].DefaultCellStyle.ForeColor = Color.Red;
                dgvMain.Columns["Vị trí kệ"].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvMain.Columns["Vị trí kệ"].Width = 110;
            }

            if (dgvMain.Columns.Contains("Mã Bản Sao")) dgvMain.Columns["Mã Bản Sao"].Visible = false;
            if (dgvMain.Columns.Contains("Mã Phiếu")) dgvMain.Columns["Mã Phiếu"].Visible = false;
        }

        private void BtnHanhDongGiaHan_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvMain.CurrentRow;
            string recordId = row.Cells["Mã Phiếu"].Value.ToString();
            string copyId = row.Cells["Mã Bản Sao"].Value.ToString();
            string tenSach = row.Cells["Tên Sách"].Value.ToString();

            string typeQuery = "SELECT ReaderType FROM Reader WHERE ReaderID = @id";
            string readerType = DataProvider.Instance.ExecuteScalar(typeQuery, new SqlParameter[] { new SqlParameter("@id", currentReaderID) })?.ToString() ?? "Sinh viên";
            int addDays = readerType.Contains("Giảng viên") ? 21 : 14;

            if (MessageBox.Show($"Xác nhận gia hạn thêm {addDays} ngày cho: {tenSach}?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (XacNhanBangMaVach("GIA HẠN", tenSach, copyId))
                {
                    DataProvider.Instance.ExecuteNonQuery("UPDATE BorrowRecord SET DueDate = DATEADD(day, @days, DueDate) WHERE RecordID = @id", new SqlParameter[] { new SqlParameter("@days", addDays), new SqlParameter("@id", recordId) });
                    MessageBox.Show("Gia hạn thành công!"); LoadLichSuMuon();
                }
            }
        }

        private void BtnHanhDongTraSach_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvMain.CurrentRow;
            string recordId = row.Cells["Mã Phiếu"].Value.ToString();
            string copyId = row.Cells["Mã Bản Sao"].Value.ToString();
            string tenSach = row.Cells["Tên Sách"].Value.ToString();

            if (MessageBox.Show("Bạn chuẩn bị bỏ sách vào Tủ trả tự động?\n\nLƯU Ý: Hệ thống sẽ chốt ngày trả ngay lúc này để không tính thêm phạt trễ hạn. Tuy nhiên, sách sẽ chuyển sang trạng thái 'Chờ kiểm duyệt'. \n\nNếu Thủ thư kiểm tra phát hiện sách bị rách/hỏng, bạn sẽ phải chịu trách nhiệm bồi thường.", "Xác nhận tự trả", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if (XacNhanBangMaVach("TỰ TRẢ SÁCH", tenSach, copyId))
                {
                    string sql = @"
                        DECLARE @LateDays INT = DATEDIFF(day, (SELECT DueDate FROM BorrowRecord WHERE RecordID = @rec), GETDATE());
                        DECLARE @LateFine DECIMAL(18,0) = CASE WHEN @LateDays > 0 THEN @LateDays * 2000 ELSE 0 END;

                        UPDATE BorrowDetail SET ReturnDate = GETDATE(), FineAmount = @LateFine WHERE RecordID = @rec AND CopyID = @copy;
                        UPDATE BookCopy SET Status = N'Chờ kiểm duyệt' WHERE CopyID = @copy;
                    ";

                    DataProvider.Instance.ExecuteNonQuery(sql, new SqlParameter[] {
                        new SqlParameter("@copy", copyId),
                        new SqlParameter("@rec", recordId)
                    });

                    MessageBox.Show("Đã ghi nhận! Vui lòng bỏ sách vào Tủ trả sách.\nNgày trả của bạn đã được hệ thống chốt.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadLichSuMuon();
                }
            }
        }

        private bool XacNhanBangMaVach(string hanhDong, string tenSach, string maBanSaoGoc)
        {
            bool isMatched = false;
            Form scanForm = new Form() { Width = 450, Height = 200, Text = $"Xác thực {hanhDong}", StartPosition = FormStartPosition.CenterParent, BackColor = Color.White };
            Label lbl = new Label() { Left = 20, Top = 20, Width = 400, Text = $"Quét mã vạch: {tenSach}", Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox txt = new TextBox() { Left = 20, Top = 60, Width = 390, Font = new Font("Segoe UI", 12) };
            txt.KeyDown += (s, ev) => {
                if (ev.KeyCode == Keys.Enter)
                {
                    if (txt.Text.Trim().ToUpper() == maBanSaoGoc.ToUpper()) { isMatched = true; scanForm.Close(); }
                    else { MessageBox.Show("Mã vạch không khớp!"); txt.Clear(); }
                }
            };
            scanForm.Controls.Add(lbl); scanForm.Controls.Add(txt); scanForm.ShowDialog();
            return isMatched;
        }

        private void btnLichSu_Click(object sender, EventArgs e) { LoadLichSuMuon(); }

        private void frmTrangChuDocGia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12) { if (MessageBox.Show("Đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes) { this.Hide(); new FrmLogin().ShowDialog(); this.Close(); } }
        }
    }
}