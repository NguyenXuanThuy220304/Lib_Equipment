using Lib_Equipment.BLL;
using Lib_Equipment.Database;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Lib_Equipment
{
    public partial class FrmQuanLySach : Form
    {
        private string selectedBookID = "";
        private bool isSetupMode = false;

        public FrmQuanLySach()
        {
            InitializeComponent();
        }

        private void FrmQuanLySach_Load(object sender, EventArgs e)
        {
            // KHÓA Ô MÃ SÁCH LẠI - CHỈ ĐỂ HIỂN THỊ MÃ DỰ KIẾN
            txtMaSach.Enabled = false;

            LoadComboboxTheLoai();
            if (cboLoaiSach.Items.Count > 0) cboLoaiSach.SelectedIndex = 0;
            LoadData();
        }

        private void LoadData()
        {
            dgvSach.DataSource = SachBLL.Instance.LayDanhSachSach();

            dgvSach.Columns["BookID"].HeaderText = "Mã Sách";
            dgvSach.Columns["Title"].HeaderText = "Tên sách";

            if (dgvSach.Columns.Contains("Price"))
            {
                dgvSach.Columns["Price"].HeaderText = "Giá tiền";
                dgvSach.Columns["Price"].DefaultCellStyle.Format = "N0";
            }
            if (dgvSach.Columns.Contains("Rarity")) dgvSach.Columns["Rarity"].HeaderText = "Loại sách";

            if (dgvSach.Columns.Contains("CabinetLocation"))
            {
                dgvSach.Columns["CabinetLocation"].HeaderText = "Vị trí kệ sách";
                dgvSach.Columns["CabinetLocation"].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                dgvSach.Columns["CabinetLocation"].DefaultCellStyle.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            }
        }

        // =======================================================================
        // COMBOBOX & TỰ ĐỘNG SINH MÃ ĐẦU SÁCH DỰ KIẾN
        // =======================================================================

        private void LoadComboboxTheLoai()
        {
            isSetupMode = true;
            try
            {
                // Truy vấn trực tiếp vào bảng BookCategory (Bạn đã chạy lệnh tạo cột bằng SSMS)
                string sql = "SELECT CategoryID, CategoryName, ISNULL(CategoryPrefix, '') AS CategoryPrefix FROM BookCategory WHERE IsDeleted = 0 OR IsDeleted IS NULL";
                DataTable dt = DataProvider.Instance.ExecuteQuery(sql);

                // Thêm mục "Khác" lên đầu tiên
                DataRow row = dt.NewRow();
                row["CategoryID"] = -1;
                row["CategoryName"] = "➕ Khác (Thêm thể loại mới)...";
                row["CategoryPrefix"] = "";
                dt.Rows.InsertAt(row, 0);

                cboTheLoai.DataSource = dt;
                cboTheLoai.DisplayMember = "CategoryName";
                cboTheLoai.ValueMember = "CategoryID";

                // Gán sự kiện
                cboTheLoai.SelectedIndexChanged -= CboTheLoai_SelectedIndexChanged;
                cboTheLoai.SelectedIndexChanged += CboTheLoai_SelectedIndexChanged;

                if (cboTheLoai.Items.Count > 1) cboTheLoai.SelectedIndex = 1; // Mặc định chọn mục thứ 2
            }
            catch (Exception ex) { MessageBox.Show("Lỗi load thể loại: " + ex.Message); }
            isSetupMode = false;
            GenerateBookID();
        }

        private void CboTheLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isSetupMode || cboTheLoai.SelectedValue == null) return;

            // Nếu người dùng nhấp vào "➕ Khác (Thêm thể loại mới)..."
            if (cboTheLoai.SelectedValue.ToString() == "-1")
            {
                ShowAddCategoryForm();
            }
            else
            {
                // Nếu chọn thể loại bình thường -> Tự động sinh mã sách dự kiến
                GenerateBookID();
            }
        }

        private void GenerateBookID()
        {
            if (isSetupMode || cboTheLoai.SelectedItem == null) return;

            // Nếu đang trong chế độ sửa (đã chọn 1 sách trên lưới) thì không đổi mã dự kiến
            if (!string.IsNullOrEmpty(selectedBookID)) return;

            DataRowView drv = cboTheLoai.SelectedItem as DataRowView;
            if (drv == null || drv["CategoryID"].ToString() == "-1") return;

            string prefix = drv["CategoryPrefix"].ToString().Trim();
            if (string.IsNullOrEmpty(prefix))
            {
                txtMaSach.Clear();
                return;
            }

            try
            {
                // Lấy mã lớn nhất hiện tại có tiền tố này để tự động tăng
                string sql = $"SELECT ISNULL(MAX(BookID), '') FROM Book WHERE BookID LIKE '{prefix}%' AND ISNUMERIC(SUBSTRING(BookID, LEN('{prefix}') + 1, LEN(BookID))) = 1";
                string maxId = DataProvider.Instance.ExecuteScalar(sql)?.ToString();

                if (string.IsNullOrEmpty(maxId))
                {
                    txtMaSach.Text = prefix + "001";
                }
                else
                {
                    string numberPart = maxId.Substring(prefix.Length);
                    if (int.TryParse(numberPart, out int num))
                    {
                        txtMaSach.Text = prefix + (num + 1).ToString("D3"); // Format đủ 3 số
                    }
                    else
                    {
                        txtMaSach.Text = prefix + "001";
                    }
                }
            }
            catch { }
        }

        // FORM NỔI (POPUP) ĐỂ THÊM THỂ LOẠI VÀ MÃ VIẾT TẮT MỚI
        private void ShowAddCategoryForm()
        {
            Form frm = new Form
            {
                Text = "Thêm Thể Loại Sách Mới",
                Size = new Size(400, 230),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.White
            };

            Label lblName = new Label { Text = "Tên thể loại (VD: Khoa học, Tiểu thuyết):", Location = new Point(20, 20), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox txtName = new TextBox { Location = new Point(20, 45), Width = 340, Font = new Font("Segoe UI", 11) };

            Label lblPrefix = new Label { Text = "Mã viết tắt đầu sách (VD: SCI, NOVEL):", Location = new Point(20, 85), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox txtPrefix = new TextBox { Location = new Point(20, 110), Width = 340, Font = new Font("Segoe UI", 11), CharacterCasing = CharacterCasing.Upper };

            Button btnSave = new Button { Text = "LƯU VÀ ĐÓNG", Location = new Point(120, 150), Width = 150, Height = 35, BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold) };

            btnSave.Click += (s, e) =>
            {
                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPrefix.Text)) { MessageBox.Show("Vui lòng nhập đủ tên và mã viết tắt!"); return; }
                try
                {
                    string sql = $"INSERT INTO BookCategory (CategoryName, CategoryPrefix, IsDeleted) VALUES (N'{txtName.Text.Trim()}', '{txtPrefix.Text.Trim()}', 0)";
                    DataProvider.Instance.ExecuteNonQuery(sql, null);

                    MessageBox.Show("Đã thêm thể loại mới thành công!", "Hoàn tất");
                    frm.Close();

                    LoadComboboxTheLoai();
                    cboTheLoai.SelectedIndex = cboTheLoai.Items.Count - 1;
                }
                catch (Exception ex) { MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message); }
            };

            frm.Controls.Add(lblName); frm.Controls.Add(txtName);
            frm.Controls.Add(lblPrefix); frm.Controls.Add(txtPrefix);
            frm.Controls.Add(btnSave);
            frm.ShowDialog();

            if (cboTheLoai.SelectedIndex == 0 && cboTheLoai.Items.Count > 1) cboTheLoai.SelectedIndex = 1;
        }

        // =======================================================================
        // XỬ LÝ CÁC NÚT BẤM (THÊM, SỬA, XÓA, SCAN) VÀ KIỂM TRA TRÙNG LẶP
        // =======================================================================

        private void dgvSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSach.Rows[e.RowIndex];
                selectedBookID = row.Cells["BookID"].Value.ToString();

                txtMaSach.Text = selectedBookID;
                txtTenSach.Text = row.Cells["Title"].Value.ToString();
                txtTacGia.Text = row.Cells["Author"].Value.ToString();
                txtNhaXuatBan.Text = row.Cells["Publisher"].Value.ToString();
                txtNamXuatBan.Text = row.Cells["PublishYear"].Value.ToString();

                isSetupMode = true;
                cboTheLoai.SelectedValue = row.Cells["CategoryID"].Value.ToString();
                isSetupMode = false;

                if (dgvSach.Columns.Contains("Price") && row.Cells["Price"].Value != null)
                    txtGiaSach.Text = row.Cells["Price"].Value.ToString();

                if (dgvSach.Columns.Contains("BookType") && row.Cells["BookType"].Value != null)
                    cboLoaiSach.Text = row.Cells["BookType"].Value.ToString();

                if (dgvSach.Columns.Contains("PageCount") && row.Cells["PageCount"].Value != null && txtSoTrang != null)
                    txtSoTrang.Text = row.Cells["PageCount"].Value.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cboTheLoai.SelectedValue == null || cboTheLoai.SelectedValue.ToString() == "-1")
            {
                MessageBox.Show("Vui lòng chọn Thể loại sách hợp lệ (hoặc thêm mới)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenSach = txtTenSach.Text.Trim();
            string tacGia = txtTacGia.Text.Trim();
            string namXB = txtNamXuatBan.Text.Trim();

            // LÔGIC KIỂM TRA TRÙNG LẶP (Cùng Tên + Tác Giả + Năm XB)
            DataTable dt = (DataTable)dgvSach.DataSource;
            if (dt != null)
            {
                // Dùng Replace("'", "''") để tránh lỗi khi tên sách có chứa dấu nháy đơn
                string filter = string.Format("Title = '{0}' AND Author = '{1}' AND PublishYear = '{2}'",
                                                tenSach.Replace("'", "''"),
                                                tacGia.Replace("'", "''"),
                                                namXB.Replace("'", "''"));
                DataRow[] duplicateRows = dt.Select(filter);

                if (duplicateRows.Length > 0)
                {
                    MessageBox.Show("Đầu sách này đã tồn tại (Bị trùng Tên sách, Tác giả và Năm xuất bản)!\nNếu đây là lô sách mới, vui lòng cập nhật 'Số lượng' thay vì thêm mới.", "Cảnh báo trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            string categoryId = cboTheLoai.SelectedValue.ToString();
            string viTriMoi = Lib_Equipment.Helpers.LocationHelper.GenerateNewBookLocation(categoryId, tenSach);

            if (SachBLL.Instance.ThemSach(txtMaSach.Text.Trim(), tenSach, tacGia, txtNhaXuatBan.Text.Trim(), namXB, categoryId, txtGiaSach.Text.Trim(), cboLoaiSach.Text, txtSoTrang.Text.Trim(), viTriMoi, out string msg))
            {
                MessageBox.Show($"{msg}\nVị trí: {viTriMoi}");
                LoadData();
                btnLamMoi_Click(null, null); // Reset lại để sinh mã dự kiến cho cuốn tiếp theo
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedBookID))
            {
                MessageBox.Show("Vui lòng chọn Đầu sách cần cập nhật!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboTheLoai.SelectedValue == null || cboTheLoai.SelectedValue.ToString() == "-1")
            {
                MessageBox.Show("Vui lòng chọn Thể loại sách hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (SachBLL.Instance.SuaSach(txtMaSach.Text.Trim(), txtTenSach.Text.Trim(), txtTacGia.Text.Trim(), txtNhaXuatBan.Text.Trim(), txtNamXuatBan.Text.Trim(), cboTheLoai.SelectedValue.ToString(), txtGiaSach.Text.Trim(), cboLoaiSach.Text, txtSoTrang.Text.Trim()))
            {
                MessageBox.Show("Cập nhật Đầu sách thành công!", "Thông báo");
                LoadData();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedBookID))
            {
                MessageBox.Show("Vui lòng chọn đầu sách cần xóa!", "Thông báo");
                return;
            }

            DialogResult dr = MessageBox.Show($"CẢNH BÁO: Bạn đang xóa VĨNH VIỄN đầu sách '{selectedBookID}' và TOÀN BỘ bản sao liên quan.\nThao tác này không thể hoàn tác. Bạn có chắc chắn không?", "Xác nhận xóa vĩnh viễn", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                if (SachBLL.Instance.XoaSach(selectedBookID))
                {
                    MessageBox.Show("Đã xóa vĩnh viễn đầu sách và giải phóng vị trí kệ!", "Thành công");
                    LoadData();
                    btnLamMoi_Click(null, null);
                }
            }
        }

        private void btnInPhieu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedBookID))
            {
                MessageBox.Show("Vui lòng chọn một đầu sách dưới lưới để in phiếu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable dtExport = SachBLL.Instance.LayDanhSachInPhieu(selectedBookID);
            if (dtExport == null || dtExport.Rows.Count == 0) return;

            SaveFileDialog sfd = new SaveFileDialog { Filter = "Excel Workbook (*.xlsx)|*.xlsx", FileName = $"PhieuDan_Sach_{selectedBookID}_{DateTime.Now:ddMMyy}.xlsx" };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Excel.Application xlApp = new Excel.Application();
                try
                {
                    Excel.Workbook xlWorkbook = xlApp.Workbooks.Add(Type.Missing);
                    Excel.Worksheet xlWorksheet = (Excel.Worksheet)xlWorkbook.ActiveSheet;

                    for (int j = 0; j < dtExport.Columns.Count; j++)
                    {
                        xlWorksheet.Cells[1, j + 1].Value = dtExport.Columns[j].ColumnName;
                        xlWorksheet.Cells[1, j + 1].Font.Bold = true;
                        xlWorksheet.Cells[1, j + 1].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                        xlWorksheet.Cells[1, j + 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    }
                    xlWorksheet.Rows[1].RowHeight = 35;

                    for (int i = 0; i < dtExport.Rows.Count; i++)
                    {
                        for (int j = 0; j < dtExport.Columns.Count; j++)
                        {
                            var cell = xlWorksheet.Cells[i + 2, j + 1];
                            if (j == 3)
                            {
                                cell.Value = $"*{dtExport.Rows[i][j]}*";
                                cell.Font.Name = "IDAutomationHC39M Free Version";
                                cell.Font.Size = 12;
                            }
                            else
                            {
                                cell.Value = dtExport.Rows[i][j].ToString();
                                cell.Font.Name = "Arial";
                                cell.Font.Size = 11;
                            }
                            cell.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        }
                        xlWorksheet.Rows[i + 2].RowHeight = 78;
                    }

                    xlWorksheet.Columns[1].ColumnWidth = 12;
                    xlWorksheet.Columns[2].ColumnWidth = 20;
                    xlWorksheet.Columns[3].ColumnWidth = 16;
                    xlWorksheet.Columns[4].ColumnWidth = 40;

                    xlWorkbook.SaveAs(sfd.FileName);
                    xlWorkbook.Close();
                    xlApp.Quit();

                    if (MessageBox.Show("Đã tạo file phiếu dán thành công! Bạn có muốn mở Excel lên xem ngay không?", "Hoàn tất", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xuất file: \n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    xlApp.Quit();
                }
            }
        }

        // =======================================================================
        // LIVE SEARCH SIÊU TỐC VÀ LÀM MỚI
        // =======================================================================

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            foreach (Control c in pnlControls.Controls)
                if (c.Name != "pnlSearch" && c.Name != "btnTimKiem" && c.Name != "btnLamMoi" && c.Name != "btnThem" && c.Name != "btnSua" && c.Name != "btnXoa" && c.Name != "btnInPhieu")
                    c.Visible = false;

            pnlSearch.Visible = true;
            pnlSearch.BringToFront();
            txtSearch.Focus();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string key = txtSearch.Text.Trim();
            DataTable dt = (DataTable)dgvSach.DataSource;
            if (dt != null)
            {
                try { dt.DefaultView.RowFilter = string.Format("Title LIKE '%{0}%' OR CategoryName LIKE '%{0}%'", key); } catch { }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            pnlSearch.Visible = false;
            txtSearch.Clear();
            foreach (Control c in pnlControls.Controls) if (c.Name != "pnlSearch") c.Visible = true;

            DataTable dt = (DataTable)dgvSach.DataSource;
            if (dt != null) dt.DefaultView.RowFilter = "";
            txtMaSach.Clear();
            selectedBookID = "";
            txtTenSach.Clear(); txtTacGia.Clear();
            txtNhaXuatBan.Clear(); txtNamXuatBan.Clear(); txtGiaSach.Clear();
            if (txtSoTrang != null) txtSoTrang.Clear();

            LoadData();
            if (cboTheLoai.Items.Count > 1) cboTheLoai.SelectedIndex = 1;
            GenerateBookID();
        }
    }
}