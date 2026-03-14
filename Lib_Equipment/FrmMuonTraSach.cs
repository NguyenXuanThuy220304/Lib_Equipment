using Lib_Equipment.Helpers;
using Lib_Equipment.Database;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Lib_Equipment
{
    public partial class FrmMuonTraSach : Form
    {
        public FrmMuonTraSach()
        {
            InitializeComponent();
        }

        private void FrmMuonTraSach_Load(object sender, EventArgs e)
        {
            dtpHanTra.Value = DateTime.Now.AddDays(14); // Mặc định cho mượn 14 ngày
            SetupGridReturnButton();
            RefreshGrid();
        }

        private void SetupGridReturnButton()
        {
            if (dgvDangMuon.Columns.Contains("btnReturn")) return;
            DataGridViewButtonColumn btnReturn = new DataGridViewButtonColumn();
            btnReturn.Name = "btnReturn";
            btnReturn.HeaderText = "Thao tác";
            btnReturn.Text = "XÁC NHẬN TRẢ";
            btnReturn.UseColumnTextForButtonValue = true;
            btnReturn.FlatStyle = FlatStyle.Flat;
            btnReturn.DefaultCellStyle.BackColor = Color.FromArgb(220, 53, 69);
            btnReturn.DefaultCellStyle.ForeColor = Color.White;
            btnReturn.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 35, 51);
            btnReturn.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgvDangMuon.Columns.Add(btnReturn);
        }

        // ==========================================================
        // 1. LÀM MỚI DANH SÁCH (TẤT CẢ / HOẶC 1 ĐỘC GIẢ)
        // ==========================================================
        private void RefreshGrid()
        {
            string query = "";
            DataTable dt = new DataTable();

            if (string.IsNullOrEmpty(txtMaDG.Text.Trim()))
            {
                query = @"
                    SELECT br.RecordID AS [Mã Phiếu], 
                           r.FullName AS [Người Mượn],
                           bd.CopyID AS [Mã Bản Sao], 
                           b.Title AS [Tên Sách], 
                           br.BorrowDate AS [Ngày Mượn], 
                           br.DueDate AS [Hạn Trả]
                    FROM BorrowRecord br
                    JOIN Reader r ON br.ReaderID = r.ReaderID
                    JOIN BorrowDetail bd ON br.RecordID = bd.RecordID
                    JOIN BookCopy bc ON bd.CopyID = bc.CopyID
                    JOIN Book b ON bc.BookID = b.BookID
                    WHERE bd.ReturnDate IS NULL";

                dt = DataProvider.Instance.ExecuteQuery(query);
            }
            else
            {
                query = @"
                    SELECT br.RecordID AS [Mã Phiếu], 
                           bd.CopyID AS [Mã Bản Sao], 
                           b.Title AS [Tên Sách], 
                           br.BorrowDate AS [Ngày Mượn], 
                           br.DueDate AS [Hạn Trả]
                    FROM BorrowRecord br
                    JOIN BorrowDetail bd ON br.RecordID = bd.RecordID
                    JOIN BookCopy bc ON bd.CopyID = bc.CopyID
                    JOIN Book b ON bc.BookID = b.BookID
                    WHERE br.ReaderID = @id AND bd.ReturnDate IS NULL";

                SqlParameter[] param = { new SqlParameter("@id", txtMaDG.Text.Trim()) };
                dt = DataProvider.Instance.ExecuteQuery(query, param);
            }

            dgvDangMuon.DataSource = dt;
            if (dgvDangMuon.Columns.Contains("btnReturn"))
            {
                dgvDangMuon.Columns["btnReturn"].DisplayIndex = dgvDangMuon.Columns.Count - 1;
            }
        }

        // ==========================================================
        // 2. QUÉT THẺ ĐỘC GIẢ (CHECK QUÁ HẠN & KHÓA TÀI KHOẢN)
        // ==========================================================
        private void txtMaDG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtMaDG.Text))
                {
                    txtTenDG.Clear();
                    RefreshGrid();
                    return;
                }

                string query = "SELECT FullName, Status FROM Reader WHERE ReaderID = @id AND (IsDeleted = 0 OR IsDeleted IS NULL)";
                SqlParameter[] param = { new SqlParameter("@id", txtMaDG.Text.Trim()) };

                DataTable dt = DataProvider.Instance.ExecuteQuery(query, param);
                if (dt.Rows.Count > 0)
                {
                    txtTenDG.Text = dt.Rows[0]["FullName"].ToString();

                    // Check trạng thái thẻ
                    string statusValue = dt.Rows[0]["Status"].ToString().Trim().ToLower();
                    if (statusValue != "1" && statusValue != "true")
                    {
                        MessageBox.Show("Thẻ độc giả này đã bị khóa hệ thống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnChoMuon.Enabled = false; return;
                    }

                    // CHECK LUẬT BÁCH KHOA: CÓ NỢ SÁCH QUÁ HẠN KHÔNG?
                    string checkOverdue = @"
                        SELECT COUNT(*) FROM BorrowRecord br 
                        JOIN BorrowDetail bd ON br.RecordID = bd.RecordID 
                        WHERE br.ReaderID = @id AND bd.ReturnDate IS NULL AND br.DueDate < CAST(GETDATE() AS DATE)";

                    // ĐÃ FIX LỖI MẢNG THAM SỐ Ở ĐÂY
                    SqlParameter[] paramCheck = { new SqlParameter("@id", txtMaDG.Text.Trim()) };
                    int overdueCount = (int)DataProvider.Instance.ExecuteScalar(checkOverdue, paramCheck);

                    RefreshGrid();

                    if (overdueCount > 0)
                    {
                        MessageBox.Show($"CẢNH BÁO: Độc giả này đang có {overdueCount} cuốn sách QUÁ HẠN!\n\nHệ thống tự động TẠM KHÓA quyền mượn mới. Vui lòng trả sách cũ và nộp phạt để mở khóa.", "Chế tài kỷ luật", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        btnChoMuon.Enabled = false;
                        txtMaBanSao.Enabled = false;
                    }
                    else
                    {
                        txtMaBanSao.Enabled = true;
                        txtMaBanSao.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy Độc giả này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenDG.Clear(); RefreshGrid();
                }
            }
        }

        // ==========================================================
        // 3. QUÉT SÁCH (SÁCH NÀO CŨNG MƯỢN ĐƯỢC MIỄN LÀ "CÓ SẴN")
        // ==========================================================
        private void txtMaBanSao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtMaBanSao.Text)) return;

                string query = @"
                    SELECT b.Title, bc.Status 
                    FROM BookCopy bc
                    JOIN Book b ON bc.BookID = b.BookID
                    WHERE bc.CopyID = @id AND (bc.IsDeleted = 0 OR bc.IsDeleted IS NULL)";

                SqlParameter[] param = { new SqlParameter("@id", txtMaBanSao.Text.Trim()) };
                DataTable dt = DataProvider.Instance.ExecuteQuery(query, param);

                if (dt.Rows.Count > 0)
                {
                    txtTenSachMuon.Text = dt.Rows[0]["Title"].ToString();
                    string status = dt.Rows[0]["Status"].ToString();

                    // Sách phòng đọc hay kho mượn đều ok, chỉ cần chưa bị mượn/mất
                    if (status != "Có sẵn")
                    {
                        MessageBox.Show("Sách này hiện đang được mượn hoặc không có sẵn (Hỏng/Mất)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnChoMuon.Enabled = false;
                        txtTenSachMuon.ForeColor = Color.DimGray;
                    }
                    else
                    {
                        btnChoMuon.Enabled = true;
                        txtTenSachMuon.ForeColor = Color.FromArgb(40, 167, 69);
                        btnChoMuon.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Mã bản sao sách không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenSachMuon.Text = "Không tìm thấy sách!";
                    txtTenSachMuon.ForeColor = Color.Red;
                    btnChoMuon.Enabled = false;
                }
            }
        }

        // ==========================================================
        // 4. LẬP PHIẾU MƯỢN (DÙNG TRANSACTION)
        // ==========================================================
        private void btnChoMuon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenDG.Text)) { MessageBox.Show("Vui lòng xác nhận thông tin Độc giả trước!", "Lỗi"); return; }

            string sqlBorrow = @"
                BEGIN TRAN;
                BEGIN TRY
                    INSERT INTO BorrowRecord (ReaderID, BorrowDate, DueDate, CreatedBy)
                    VALUES (@reader, GETDATE(), @due, @user);

                    DECLARE @newRecordID INT = SCOPE_IDENTITY();

                    INSERT INTO BorrowDetail (RecordID, CopyID)
                    VALUES (@newRecordID, @copy);

                    UPDATE BookCopy SET Status = N'Đang mượn' WHERE CopyID = @copy;
                    
                    COMMIT TRAN;
                END TRY
                BEGIN CATCH
                    ROLLBACK TRAN;
                    THROW;
                END CATCH;";

            SqlParameter[] paramBorrow = {
                new SqlParameter("@reader", txtMaDG.Text.Trim()),
                new SqlParameter("@copy", txtMaBanSao.Text.Trim()),
                new SqlParameter("@due", dtpHanTra.Value),
                new SqlParameter("@user", AppSession.Username ?? "ADMIN")
            };

            try
            {
                DataProvider.Instance.ExecuteNonQuery(sqlBorrow, paramBorrow);
                MessageBox.Show("Cho mượn sách thành công!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtMaBanSao.Clear(); txtTenSachMuon.Text = "Chưa xác định..."; txtTenSachMuon.ForeColor = Color.DimGray; btnChoMuon.Enabled = false;
                RefreshGrid(); txtMaBanSao.Focus();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi"); }
        }

        // ==========================================================
        // 5. TRẢ SÁCH (TÍNH TIỀN PHẠT SIÊU CHUẨN)
        // ==========================================================
        private void dgvDangMuon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvDangMuon.Columns["btnReturn"].Index && e.RowIndex >= 0)
            {
                string recordId = dgvDangMuon.Rows[e.RowIndex].Cells["Mã Phiếu"].Value.ToString();
                string copyId = dgvDangMuon.Rows[e.RowIndex].Cells["Mã Bản Sao"].Value.ToString();
                DateTime dueDate = Convert.ToDateTime(dgvDangMuon.Rows[e.RowIndex].Cells["Hạn Trả"].Value);

                // TÍNH TOÁN NGÀY TRỄ HẠN (1.000đ / ngày)
                int lateDays = (DateTime.Now.Date - dueDate.Date).Days;
                lateDays = lateDays > 0 ? lateDays : 0;
                decimal lateFine = lateDays * 1000;

                // GIAO DIỆN POPUP 
                Form frmTra = new Form();
                frmTra.Text = "Nghiệp vụ Trả sách & Thu phạt";
                frmTra.Size = new Size(450, 360);
                frmTra.StartPosition = FormStartPosition.CenterParent;
                frmTra.FormBorderStyle = FormBorderStyle.FixedDialog;
                frmTra.MaximizeBox = false; frmTra.MinimizeBox = false; frmTra.BackColor = Color.White;

                Label lblTre = new Label() { Text = $"Trả trễ: {lateDays} ngày (Phạt {lateFine:N0}đ)", Left = 20, Top = 20, Width = 380, Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = lateDays > 0 ? Color.Red : Color.Green };

                Label lblTinhTrang = new Label() { Text = "Tình trạng sách:", Left = 20, Top = 65, Width = 130, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                ComboBox cboTinhTrang = new ComboBox() { Left = 150, Top = 60, Width = 250, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };
                cboTinhTrang.Items.AddRange(new string[] {
                    "Bình thường",
                    "Làm hỏng (Phạt 50k)",
                    "Mất - Mua đền sách mới",
                    "Mất - Sách hiếm (Đền x3)",
                    "Mất - TL nội bộ (Tiếng Việt)",
                    "Mất - TL nội bộ (Tiếng Ngoại)"
                });
                cboTinhTrang.SelectedIndex = 0;

                Label lblGhiChu = new Label() { Text = "", Left = 20, Top = 105, Width = 130, Font = new Font("Segoe UI", 10) };
                TextBox txtInput = new TextBox() { Left = 150, Top = 100, Width = 250, Font = new Font("Segoe UI", 10), Visible = false };

                Label lblTongPhat = new Label() { Text = "TỔNG PHẠT (VNĐ):", Left = 20, Top = 155, Width = 130, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                TextBox txtTongPhat = new TextBox() { Left = 150, Top = 150, Width = 250, Text = lateFine.ToString(), Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.Red, ReadOnly = true };

                Label lblAction = new Label() { Text = "(Hoàn trả sách bình thường)", Left = 20, Top = 190, Width = 400, Font = new Font("Segoe UI", 9, FontStyle.Italic), ForeColor = Color.Blue };

                // LOGIC TÍNH PHẠT ĐỘNG
                Action calculateFine = () => {
                    decimal total = lateFine;
                    decimal inputVal = 0;
                    decimal.TryParse(txtInput.Text, out inputVal);

                    if (cboTinhTrang.Text.Contains("Làm hỏng")) { total += 50000; lblAction.Text = "(Sách chuyển trạng thái Hỏng)"; }
                    else if (cboTinhTrang.Text.Contains("Mua đền sách mới")) { total += 20000; lblAction.Text = "(Thu 1 sách mới + 20k phí mã vạch. Chuyển trạng thái Mất)"; }
                    else if (cboTinhTrang.Text.Contains("Sách hiếm")) { total += (inputVal * 3); lblAction.Text = "(Phạt gấp 3 lần giá bìa. Chuyển trạng thái Mất)"; }
                    else if (cboTinhTrang.Text.Contains("Tiếng Việt")) { total += (inputVal * 1000); lblAction.Text = "(Phạt 1.000đ x Số trang. Chuyển trạng thái Mất)"; }
                    else if (cboTinhTrang.Text.Contains("Tiếng Ngoại")) { total += (inputVal * 10000); lblAction.Text = "(Phạt 10.000đ x Số trang. Chuyển trạng thái Mất)"; }
                    else { lblAction.Text = "(Hoàn trả sách vào kho bình thường)"; }

                    txtTongPhat.Text = total.ToString("N0");
                };

                cboTinhTrang.SelectedIndexChanged += (s, ev) => {
                    txtInput.Visible = false; lblGhiChu.Text = ""; txtInput.Text = "0";
                    if (cboTinhTrang.Text.Contains("Sách hiếm")) { lblGhiChu.Text = "Nhập giá bìa (đ):"; txtInput.Visible = true; txtInput.Focus(); }
                    else if (cboTinhTrang.Text.Contains("nội bộ")) { lblGhiChu.Text = "Nhập số trang:"; txtInput.Visible = true; txtInput.Focus(); }
                    calculateFine();
                };
                txtInput.TextChanged += (s, ev) => calculateFine();

                Button btnXacNhan = new Button() { Text = "THU TIỀN VÀ TRẢ SÁCH", Left = 150, Top = 230, Width = 250, Height = 50, BackColor = Color.FromArgb(40, 167, 69), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 11, FontStyle.Bold) };
                frmTra.Controls.AddRange(new Control[] { lblTre, lblTinhTrang, cboTinhTrang, lblGhiChu, txtInput, lblTongPhat, txtTongPhat, lblAction, btnXacNhan });

                // XỬ LÝ LƯU DATABASE
                btnXacNhan.Click += (s, ev) => {
                    decimal finalFine = 0;
                    decimal.TryParse(txtTongPhat.Text.Replace(",", "").Replace(".", ""), out finalFine);

                    string newStatus = "Có sẵn"; string cond = "Tốt";
                    if (cboTinhTrang.Text.Contains("Hỏng")) { newStatus = "Hỏng"; cond = "Hỏng"; }
                    else if (cboTinhTrang.Text.Contains("Mất")) { newStatus = "Mất"; cond = "Mất"; }

                    string sqlReturn = @"
                        BEGIN TRAN;
                        BEGIN TRY
                            UPDATE BorrowDetail SET ReturnDate = GETDATE(), ReturnCondition = @cond, FineAmount = @fine WHERE RecordID = @rec AND CopyID = @copy;
                            UPDATE BookCopy SET Status = @status WHERE CopyID = @copy;
                            COMMIT TRAN;
                        END TRY
                        BEGIN CATCH ROLLBACK TRAN; THROW; END CATCH;";

                    SqlParameter[] paramReturn = {
                        new SqlParameter("@rec", recordId), new SqlParameter("@copy", copyId),
                        new SqlParameter("@cond", cond), new SqlParameter("@fine", finalFine),
                        new SqlParameter("@status", newStatus)
                    };

                    DataProvider.Instance.ExecuteNonQuery(sqlReturn, paramReturn);
                    MessageBox.Show("Đã hoàn tất quy trình trả sách và cập nhật trạng thái kho!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmTra.DialogResult = DialogResult.OK; frmTra.Close();
                };

                if (frmTra.ShowDialog() == DialogResult.OK) { RefreshGrid(); }
            }
        }

        // ==========================================================
        // 6. BÔI ĐỎ SÁCH QUÁ HẠN 
        // ==========================================================
        private void dgvDangMuon_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvDangMuon.Columns[e.ColumnIndex].Name == "Hạn Trả" && e.Value != null)
            {
                DateTime dueDate;
                if (DateTime.TryParse(e.Value.ToString(), out dueDate))
                {
                    if (dueDate < DateTime.Now.Date)
                    {
                        dgvDangMuon.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
                        dgvDangMuon.Rows[e.RowIndex].DefaultCellStyle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
                    }
                }
            }
        }
    }
}