using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Lib_Equipment.Database;
using Lib_Equipment.Helpers;

namespace Lib_Equipment
{
    public partial class frmTrangChuDocGia : Form
    {
        private Form currentChildForm; // Dùng để quản lý Form AI đang mở
        private string currentReaderID;
        private string currentFullName;
        private readonly string API_KEY = AppSecrets.GeminiApiKey;
        private DataTable _currentData;
        private bool _isHistoryMode;

        // Lưu ý: Nếu bạn có kéo thả nút btnAI ở giao diện thì cứ giữ nguyên.
        // Trong code này tớ mặc định bạn đã có btnAI.Click nối vào sự kiện bên dưới rồi.

        public frmTrangChuDocGia(string readerID, string fullName)
        {
            InitializeComponent();
            currentReaderID = readerID;
            currentFullName = fullName;

            lblWelcome.Text = $"Xin chào, {currentFullName} | Mã: {currentReaderID}";
            UpdateDebtDisplay();

            this.Load += FrmTrangChuDocGia_Load;
            this.KeyDown += FrmTrangChuDocGia_KeyDown;

            this.Resize += (s, e) => flpBooks.Invalidate();
            flpBooks.Resize += FlpBooks_Resize;

            btnSearch.Click += BtnSearch_Click;
            txtSearch.KeyDown += TxtSearch_KeyDown;
            btnLichSu.Click += BtnLichSu_Click;
            btnPay.Click += BtnThanhToanNo_Click;
            btnAutoBorrow.Click += HandleOpenBorrowForm_Click;

            // Nếu có nút AI trên Designer thì nối sự kiện (Bỏ comment dòng dưới nếu cần)
            // btnAI.Click += btnAI_Click; 
        }

        private void FrmTrangChuDocGia_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            LoadLichSuMuon();
        }

        private void FlpBooks_Resize(object sender, EventArgs e)
        {
            if (_currentData != null)
            {
                flpBooks.SuspendLayout();
                int targetWidth = flpBooks.ClientSize.Width - 25;
                foreach (Control ctrl in flpBooks.Controls)
                {
                    if (ctrl is Guna2Panel card && card.Width != targetWidth)
                    {
                        card.Width = targetWidth;
                    }
                }
                flpBooks.ResumeLayout();
            }
        }

        // ===============================================
        // XỬ LÝ MỞ FORM TRỢ LÝ AI VÀO TRONG PANEL
        // ===============================================
        private void btnAI_Click(object sender, EventArgs e)
        {
            lblSectionTitle.Text = "🤖 Trợ lý AI UNETI - Hỗ trợ thông minh";
            OpenChildForm(new FrmTroLyAI("DocGia")); // Mở Form AI
        }

        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close(); // Đóng form cũ nếu đang mở
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            // TUYỆT CHIÊU: Ẩn danh sách sách đi để nhường chỗ cho Form AI
            flpBooks.Visible = false;

            // Ném form AI thẳng vào pnlContent thay vì flpBooks
            pnlContent.Controls.Add(childForm);
            pnlContent.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void CloseChildForm()
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
                currentChildForm = null;
            }
            // Hiện lại danh sách sách khi thoát AI
            flpBooks.Visible = true;
        }

        // ===============================================
        // 1. NGHIỆP VỤ CÔNG NỢ
        // ===============================================
        private void UpdateDebtDisplay()
        {
            string query = "SELECT ISNULL(AcademicDebt, 0) FROM Reader WHERE ReaderID = @id";
            decimal debt = Convert.ToDecimal(DataProvider.Instance.ExecuteScalar(query, new SqlParameter[] { new SqlParameter("@id", currentReaderID) }));

            if (debt > 0)
            {
                lblCongNo.Text = $"Đang nợ phí: {debt:N0} VNĐ";
                lblCongNo.ForeColor = Color.FromArgb(231, 76, 60);
                btnPay.Visible = true;
            }
            else
            {
                lblCongNo.Text = "Tài khoản sạch (0 VNĐ)";
                lblCongNo.ForeColor = Color.FromArgb(46, 204, 113);
                btnPay.Visible = false;
            }
        }

        private void BtnThanhToanNo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xác nhận bạn đã thanh toán qua cổng Ngân hàng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = "UPDATE Reader SET AcademicDebt = 0 WHERE ReaderID = @id";
                DataProvider.Instance.ExecuteNonQuery(sql, new SqlParameter[] { new SqlParameter("@id", currentReaderID) });
                MessageBox.Show("Thanh toán thành công!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateDebtDisplay();
            }
        }

        // ===============================================
        // 2. NGHIỆP VỤ TÌM KIẾM
        // ===============================================
        private void BtnSearch_Click(object sender, EventArgs e) { TimKiemSachThongMinh(); }
        private void TxtSearch_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; TimKiemSachThongMinh(); } }

        private void TimKiemSachThongMinh()
        {
            CloseChildForm(); // Tắt AI (nếu đang mở) để nhường chỗ cho danh sách sách

            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword)) { LoadLichSuMuon(); return; }

            lblSectionTitle.Text = $"🔍 Kết quả tìm kiếm cho: '{keyword}'";

            string acronym = "%";
            foreach (char c in keyword) if (char.IsLetterOrDigit(c)) acronym += c + "%";
            string[] words = keyword.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            List<SqlParameter> paramList = new List<SqlParameter>();
            string wordConditions = "";
            for (int i = 0; i < words.Length; i++)
            {
                string pName = $"@word{i}";
                wordConditions += $"(b.Title COLLATE SQL_Latin1_General_CP1_CI_AI LIKE {pName} OR b.Author COLLATE SQL_Latin1_General_CP1_CI_AI LIKE {pName})";
                if (i < words.Length - 1) wordConditions += " AND ";
                paramList.Add(new SqlParameter(pName, $"%{words[i]}%"));
            }
            paramList.Add(new SqlParameter("@acronym", acronym));

            string query = $@"
                SELECT 
                    b.CabinetLocation AS [ViTriKe], b.Title AS [TenSach], b.Author AS [TacGia], bcg.CategoryName AS [TheLoai],
                    (SELECT COUNT(*) FROM BookCopy bc WHERE bc.BookID = b.BookID AND bc.Status = N'Có sẵn' AND bc.IsDeleted = 0) AS [SoLuong]
                FROM Book b LEFT JOIN BookCategory bcg ON b.CategoryID = bcg.CategoryID
                WHERE b.IsDeleted = 0 AND (({wordConditions}) OR b.Title LIKE @acronym) ORDER BY b.Title ASC";

            _currentData = DataProvider.Instance.ExecuteQuery(query, paramList.ToArray());
            _isHistoryMode = false;
            PopulateCards();
        }

        // ===============================================
        // 3. NGHIỆP VỤ LỊCH SỬ MƯỢN TRẢ
        // ===============================================
        private void BtnLichSu_Click(object sender, EventArgs e) { txtSearch.Clear(); LoadLichSuMuon(); }

        private void LoadLichSuMuon()
        {
            CloseChildForm(); // Tắt AI (nếu đang mở) để nhường chỗ cho danh sách sách

            lblSectionTitle.Text = "📚 Lịch sử mượn sách của bạn";
            string query = @"
                SELECT 
                    b.CabinetLocation AS [ViTriKe], br.RecordID AS [MaPhieu], bc.CopyID AS [MaBanSao], b.Title AS [TenSach], 
                    br.BorrowDate AS [NgayMuon], br.DueDate AS [HanTra],
                    CASE 
                        WHEN bd.ReturnDate IS NULL THEN N'Đang mượn' 
                        WHEN bc.Status = N'Chờ kiểm duyệt' AND bd.RecordID = (SELECT MAX(RecordID) FROM BorrowDetail WHERE CopyID = bd.CopyID) THEN N'Chờ duyệt'
                        ELSE N'Đã trả' 
                    END AS [TrangThai]
                FROM BorrowRecord br JOIN BorrowDetail bd ON br.RecordID = bd.RecordID JOIN BookCopy bc ON bd.CopyID = bc.CopyID JOIN Book b ON bc.BookID = b.BookID
                WHERE br.ReaderID = @readerId AND br.IsDeleted = 0
                ORDER BY CASE WHEN bd.ReturnDate IS NULL OR bc.Status = N'Chờ kiểm duyệt' THEN 0 ELSE 1 END, br.BorrowDate DESC";

            _currentData = DataProvider.Instance.ExecuteQuery(query, new SqlParameter[] { new SqlParameter("@readerId", currentReaderID) });
            _isHistoryMode = true;
            PopulateCards();
        }

        // ===============================================
        // 4. ENGINE TẠO CARD LIST (1 Item / Hàng)
        // ===============================================
        private void PopulateCards()
        {
            if (_currentData == null) return;

            flpBooks.SuspendLayout();
            flpBooks.Controls.Clear();

            if (_currentData.Rows.Count == 0)
            {
                Label lblEmpty = new Label() { Text = "Không có dữ liệu phù hợp.", Font = new Font("Segoe UI", 12, FontStyle.Italic), AutoSize = true, ForeColor = Color.Gray, Margin = new Padding(20) };
                flpBooks.Controls.Add(lblEmpty);
                flpBooks.ResumeLayout();
                return;
            }

            int cardWidth = flpBooks.ClientSize.Width > 0 ? flpBooks.ClientSize.Width - 25 : 1000;

            foreach (DataRow row in _currentData.Rows)
            {
                Guna2Panel card = new Guna2Panel()
                {
                    Width = cardWidth,
                    Height = 100,
                    BorderRadius = 10,
                    FillColor = Color.White,
                    Margin = new Padding(3, 3, 3, 10),
                    BorderColor = Color.FromArgb(230, 230, 230),
                    BorderThickness = 1
                };

                Label lblTitle = new Label() { Text = row["TenSach"].ToString(), Font = new Font("Segoe UI", 13, FontStyle.Bold), ForeColor = Color.FromArgb(0, 51, 102), Location = new Point(20, 20), Size = new Size(cardWidth - 400, 30), AutoEllipsis = true };
                Label lblVitri = new Label() { Font = new Font("Segoe UI", 10, FontStyle.Bold), AutoSize = false, Width = 300, TextAlign = ContentAlignment.MiddleRight };
                lblVitri.Location = new Point(card.Width - 320, 20);
                lblVitri.Anchor = AnchorStyles.Top | AnchorStyles.Right;

                if (_isHistoryMode)
                {
                    string status = row["TrangThai"].ToString();
                    lblVitri.Text = $"Kệ: {row["ViTriKe"]}  •  {status}";
                    lblVitri.ForeColor = status == "Đang mượn" ? Color.FromArgb(255, 102, 0) : Color.Gray;

                    DateTime borrowDate = Convert.ToDateTime(row["NgayMuon"]);
                    DateTime dueDate = Convert.ToDateTime(row["HanTra"]);
                    Label lblDate = new Label() { Text = $"Ngày mượn: {borrowDate:dd/MM/yyyy}  |  Hạn trả: {dueDate:dd/MM/yyyy}", Font = new Font("Segoe UI", 10), ForeColor = Color.DimGray, Location = new Point(20, 55), AutoSize = true };
                    card.Controls.Add(lblDate);

                    if (status == "Đang mượn")
                    {
                        string recordId = row["MaPhieu"].ToString();
                        string copyId = row["MaBanSao"].ToString();
                        string title = row["TenSach"].ToString();

                        Guna2Button btnTra = new Guna2Button() { Text = "Trả Sách", Size = new Size(110, 35), BorderRadius = 8, FillColor = Color.FromArgb(0, 184, 148), Font = new Font("Segoe UI", 9, FontStyle.Bold), Cursor = Cursors.Hand };
                        btnTra.Location = new Point(card.Width - 130, 50);
                        btnTra.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                        btnTra.Click += (s, e) => ActionTraSach(recordId, copyId, title);
                        card.Controls.Add(btnTra);

                        if ((dueDate.Date - DateTime.Now.Date).Days >= 3)
                        {
                            Guna2Button btnGiaHan = new Guna2Button() { Text = "Gia Hạn", Size = new Size(110, 35), BorderRadius = 8, FillColor = Color.FromArgb(255, 193, 7), ForeColor = Color.Black, Font = new Font("Segoe UI", 9, FontStyle.Bold), Cursor = Cursors.Hand };
                            btnGiaHan.Location = new Point(card.Width - 250, 50);
                            btnGiaHan.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                            btnGiaHan.Click += (s, e) => ActionGiaHan(recordId, copyId, title);
                            card.Controls.Add(btnGiaHan);
                        }
                    }
                }
                else
                {
                    lblVitri.Text = $"Vị trí kệ: {row["ViTriKe"]}";
                    lblVitri.ForeColor = Color.FromArgb(231, 76, 60);

                    Label lblAuthor = new Label() { Text = $"Tác giả: {row["TacGia"]} | Thể loại: {row["TheLoai"]}", Font = new Font("Segoe UI", 10), ForeColor = Color.DimGray, Location = new Point(20, 55), AutoSize = true };
                    card.Controls.Add(lblAuthor);

                    int qty = Convert.ToInt32(row["SoLuong"]);
                    Label lblQty = new Label() { Text = qty > 0 ? $"Kho còn: {qty} cuốn" : "Đã mượn hết", Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = qty > 0 ? Color.FromArgb(46, 204, 113) : Color.Red, AutoSize = false, Width = 200, TextAlign = ContentAlignment.MiddleRight };
                    lblQty.Location = new Point(card.Width - 220, 55);
                    lblQty.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                    card.Controls.Add(lblQty);
                }

                card.Controls.Add(lblVitri);
                card.Controls.Add(lblTitle);
                flpBooks.Controls.Add(card);
            }
            flpBooks.ResumeLayout();
        }

        // ===============================================
        // 5. HÀNH ĐỘNG GIA HẠN / TRẢ SÁCH
        // ===============================================
        private void HandleOpenBorrowForm_Click(object sender, EventArgs e)
        {
            new frmSecondaryBorrow(currentReaderID, currentFullName, API_KEY).ShowDialog();
            LoadLichSuMuon(); UpdateDebtDisplay();
        }

        private void ActionGiaHan(string recordId, string copyId, string tenSach)
        {
            int addDays = 14;
            if (MessageBox.Show($"Xác nhận gia hạn thêm {addDays} ngày cho sách: {tenSach}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (XacNhanBangMaVach("GIA HẠN", tenSach, copyId))
                {
                    DataProvider.Instance.ExecuteNonQuery("UPDATE BorrowRecord SET DueDate = DATEADD(day, @days, DueDate) WHERE RecordID = @id", new SqlParameter[] { new SqlParameter("@days", addDays), new SqlParameter("@id", recordId) });
                    MessageBox.Show("Gia hạn thành công!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information); LoadLichSuMuon();
                }
            }
        }

        private void ActionTraSach(string recordId, string copyId, string tenSach)
        {
            if (MessageBox.Show($"Bạn chuẩn bị trả sách: {tenSach}?\n\nSách sẽ được chuyển sang trạng thái 'Chờ kiểm duyệt' trước khi hoàn tất thủ tục.", "Xác nhận trả", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (XacNhanBangMaVach("TỰ TRẢ SÁCH", tenSach, copyId))
                {
                    string sql = @"DECLARE @Late INT = DATEDIFF(day, (SELECT DueDate FROM BorrowRecord WHERE RecordID = @rec), GETDATE());
                                   DECLARE @Fine DECIMAL(18,0) = CASE WHEN @Late > 0 THEN @Late * 2000 ELSE 0 END;
                                   UPDATE BorrowDetail SET ReturnDate = GETDATE(), FineAmount = @Fine WHERE RecordID = @rec AND CopyID = @copy;
                                   UPDATE BookCopy SET Status = N'Chờ kiểm duyệt' WHERE CopyID = @copy;";
                    DataProvider.Instance.ExecuteNonQuery(sql, new SqlParameter[] { new SqlParameter("@copy", copyId), new SqlParameter("@rec", recordId) });
                    MessageBox.Show("Đã ghi nhận! Vui lòng bỏ sách vào Tủ trả sách.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information); LoadLichSuMuon();
                }
            }
        }

        private bool XacNhanBangMaVach(string hanhDong, string tenSach, string maBanSaoGoc)
        {
            bool isMatched = false;
            Form scanForm = new Form() { Width = 450, Height = 200, Text = $"Quét mã - {hanhDong}", StartPosition = FormStartPosition.CenterParent, BackColor = Color.White, MaximizeBox = false, MinimizeBox = false };
            Label lbl = new Label() { Left = 20, Top = 20, Width = 400, Text = $"Mã vạch: {tenSach}", Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox txt = new TextBox() { Left = 20, Top = 60, Width = 390, Font = new Font("Segoe UI", 12) };
            txt.KeyDown += (s, ev) => {
                if (ev.KeyCode == Keys.Enter)
                {
                    if (txt.Text.Trim().ToUpper() == maBanSaoGoc.ToUpper()) { isMatched = true; scanForm.Close(); }
                    else { MessageBox.Show("Mã vạch sai!", "Lỗi"); txt.Clear(); }
                }
            };
            scanForm.Controls.Add(lbl); scanForm.Controls.Add(txt); scanForm.Shown += (s, ev) => txt.Focus(); scanForm.ShowDialog();
            return isMatched;
        }

        private void FrmTrangChuDocGia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12) { if (MessageBox.Show("Đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes) { this.Hide(); new FrmLogin().ShowDialog(); this.Close(); } }
        }
    }
}