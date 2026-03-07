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
            dtpHanTra.Value = DateTime.Now.AddDays(14);
            SetupGridReturnButton();

            // GỌI HÀM NÀY LÚC MỞ FORM ĐỂ HIỆN TẤT CẢ PHIẾU CHƯA TRẢ
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
        // HÀM THÔNG MINH: TẢI DANH SÁCH SÁCH ĐANG MƯỢN
        // ==========================================================
        private void RefreshGrid()
        {
            string query = "";
            DataTable dt = new DataTable();

            // Nếu chưa nhập sinh viên -> Hiển thị TẤT CẢ sách đang mượn của thư viện
            if (string.IsNullOrEmpty(txtMaDG.Text.Trim()))
            {
                query = @"
                    SELECT br.RecordID AS [Mã Phiếu], 
                           r.FullName AS [Người Mượn], -- Hiện thêm tên người mượn
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
            // Nếu đã nhập mã sinh viên -> Lọc riêng sách của sinh viên đó
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

            // Đẩy nút TRẢ SÁCH xuống cột cuối cùng
            if (dgvDangMuon.Columns.Contains("btnReturn"))
            {
                dgvDangMuon.Columns["btnReturn"].DisplayIndex = dgvDangMuon.Columns.Count - 1;
            }
        }

        // ==========================================================
        // TÌM ĐỘC GIẢ
        // ==========================================================
        private void txtMaDG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtMaDG.Text))
                {
                    txtTenDG.Clear();
                    RefreshGrid(); // Nếu xóa trắng mã ĐG và ấn Enter -> Load lại toàn bộ
                    return;
                }

                string query = "SELECT FullName, Status FROM Reader WHERE ReaderID = @id AND (IsDeleted = 0 OR IsDeleted IS NULL)";
                SqlParameter[] param = { new SqlParameter("@id", txtMaDG.Text.Trim()) };

                DataTable dt = DataProvider.Instance.ExecuteQuery(query, param);
                if (dt.Rows.Count > 0)
                {
                    txtTenDG.Text = dt.Rows[0]["FullName"].ToString();
                    string statusValue = dt.Rows[0]["Status"].ToString().Trim().ToLower();
                    if (statusValue != "1" && statusValue != "true")
                    {
                        MessageBox.Show("Thẻ độc giả này đã bị khóa hoặc hết hạn!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnChoMuon.Enabled = false;
                        dgvDangMuon.DataSource = null;
                        return;
                    }

                    RefreshGrid(); // Load riêng sách của Độc giả này
                    txtMaBanSao.Focus();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy Độc giả này trong hệ thống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenDG.Clear();
                    RefreshGrid(); // Load lại toàn bộ
                }
            }
        }

        // ==========================================================
        // QUÉT MÃ SÁCH
        // ==========================================================
        private void txtMaBanSao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtMaBanSao.Text)) return;

                string query = @"
                    SELECT b.Title, bc.Status, bc.Location 
                    FROM BookCopy bc
                    JOIN Book b ON bc.BookID = b.BookID
                    WHERE bc.CopyID = @id AND (bc.IsDeleted = 0 OR bc.IsDeleted IS NULL)";

                SqlParameter[] param = { new SqlParameter("@id", txtMaBanSao.Text.Trim()) };
                DataTable dt = DataProvider.Instance.ExecuteQuery(query, param);

                if (dt.Rows.Count > 0)
                {
                    txtTenSachMuon.Text = dt.Rows[0]["Title"].ToString();
                    string status = dt.Rows[0]["Status"].ToString();
                    string location = dt.Rows[0]["Location"].ToString();

                    if (status != "Có sẵn")
                    {
                        MessageBox.Show("Sách này hiện đang được mượn hoặc không có sẵn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnChoMuon.Enabled = false;
                        txtTenSachMuon.ForeColor = Color.DimGray;
                    }
                    else if (!location.Contains("Kho mượn"))
                    {
                        MessageBox.Show("Đây là sách đọc tại chỗ! Không được phép mượn mang về.", "Cảnh báo Kho", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        btnChoMuon.Enabled = false;
                        txtTenSachMuon.ForeColor = Color.Red;
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
        // XÁC NHẬN CHO MƯỢN
        // ==========================================================
        private void btnChoMuon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenDG.Text))
            {
                MessageBox.Show("Vui lòng xác nhận thông tin Độc giả trước!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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

                txtMaBanSao.Clear();
                txtTenSachMuon.Text = "Chưa xác định...";
                txtTenSachMuon.ForeColor = Color.DimGray;
                btnChoMuon.Enabled = false;

                RefreshGrid();
                txtMaBanSao.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống khi tạo phiếu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================================================
        // XÁC NHẬN TRẢ SÁCH (POPUP)
        // ==========================================================
        private void dgvDangMuon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvDangMuon.Columns["btnReturn"].Index && e.RowIndex >= 0)
            {
                string recordId = dgvDangMuon.Rows[e.RowIndex].Cells["Mã Phiếu"].Value.ToString();
                string copyId = dgvDangMuon.Rows[e.RowIndex].Cells["Mã Bản Sao"].Value.ToString();
                string tenSach = dgvDangMuon.Rows[e.RowIndex].Cells["Tên Sách"].Value.ToString();

                Form frmTra = new Form();
                frmTra.Text = "Xác nhận tình trạng sách";
                frmTra.Size = new Size(380, 230);
                frmTra.StartPosition = FormStartPosition.CenterParent;
                frmTra.FormBorderStyle = FormBorderStyle.FixedDialog;
                frmTra.MaximizeBox = false;
                frmTra.MinimizeBox = false;
                frmTra.BackColor = Color.White;

                Label lblTinhTrang = new Label() { Text = "Tình trạng:", Left = 20, Top = 25, Width = 110, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                ComboBox cboTinhTrang = new ComboBox() { Left = 130, Top = 20, Width = 190, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };
                cboTinhTrang.Items.AddRange(new string[] { "Tốt", "Hỏng", "Mất" });
                cboTinhTrang.SelectedIndex = 0;

                Label lblPhat = new Label() { Text = "Tiền phạt:", Left = 20, Top = 75, Width = 110, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                TextBox txtPhat = new TextBox() { Left = 130, Top = 70, Width = 190, Text = "0", Font = new Font("Segoe UI", 10) };

                cboTinhTrang.SelectedIndexChanged += (s, ev) => {
                    if (cboTinhTrang.Text == "Tốt") txtPhat.Text = "0";
                    else if (cboTinhTrang.Text == "Hỏng") txtPhat.Text = "50000";
                    else if (cboTinhTrang.Text == "Mất") txtPhat.Text = "150000";
                };

                Button btnXacNhan = new Button() { Text = "HOÀN TẤT TRẢ", Left = 130, Top = 120, Width = 190, Height = 45, BackColor = Color.FromArgb(40, 167, 69), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold) };

                frmTra.Controls.Add(lblTinhTrang); frmTra.Controls.Add(cboTinhTrang);
                frmTra.Controls.Add(lblPhat); frmTra.Controls.Add(txtPhat);
                frmTra.Controls.Add(btnXacNhan);

                btnXacNhan.Click += (s, ev) => {
                    decimal tienPhat = 0;
                    decimal.TryParse(txtPhat.Text, out tienPhat);

                    string sqlReturn = @"
                        BEGIN TRAN;
                        BEGIN TRY
                            UPDATE BorrowDetail 
                            SET ReturnDate = GETDATE(), ReturnCondition = @cond, FineAmount = @fine 
                            WHERE RecordID = @rec AND CopyID = @copy;

                            IF @cond = N'Mất'
                                UPDATE BookCopy SET Status = N'Mất' WHERE CopyID = @copy;
                            ELSE IF @cond = N'Hỏng'
                                UPDATE BookCopy SET Status = N'Hỏng' WHERE CopyID = @copy;
                            ELSE
                                UPDATE BookCopy SET Status = N'Có sẵn' WHERE CopyID = @copy;

                            COMMIT TRAN;
                        END TRY
                        BEGIN CATCH
                            ROLLBACK TRAN;
                            THROW;
                        END CATCH;";

                    SqlParameter[] paramReturn = {
                        new SqlParameter("@rec", recordId),
                        new SqlParameter("@copy", copyId),
                        new SqlParameter("@cond", cboTinhTrang.Text),
                        new SqlParameter("@fine", tienPhat)
                    };

                    try
                    {
                        DataProvider.Instance.ExecuteNonQuery(sqlReturn, paramReturn);
                        MessageBox.Show("Nhận trả sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmTra.DialogResult = DialogResult.OK;
                        frmTra.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Lỗi SQL");
                    }
                };

                if (frmTra.ShowDialog() == DialogResult.OK)
                {
                    RefreshGrid();
                }
            }
        }

        // ==========================================================
        // HIGHLIGHT SÁCH QUÁ HẠN 
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