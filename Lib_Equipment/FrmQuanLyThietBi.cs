using Lib_Equipment.Database;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Lib_Equipment
{
    public partial class FrmQuanLyThietBi : Form
    {
        private string selectedEquipmentID = "";
        private Timer searchTimer;

        public FrmQuanLyThietBi()
        {
            InitializeComponent();
            dgvThietBi.DataError += (s, e) => { e.ThrowException = false; };

            searchTimer = new Timer();
            searchTimer.Interval = 300;
            searchTimer.Tick += SearchTimer_Tick;
        }

        private void FrmQuanLyThietBi_Load(object sender, EventArgs e)
        {
            txtMaTB.Enabled = false;

            LoadComboboxLoaiTB();
            LoadComboboxKhoaPhong();
            LoadData();
            AdjustSearchLayout();

            pnlSearch.Visible = false;
            pnlControls.Visible = true;
            CheckMaintenanceAlert();

            cboLoaiTB.SelectedIndexChanged += cboLoaiTB_SelectedIndexChanged;
            btnLamMoi_Click(null, null);
        }

        private void FrmQuanLyThietBi_Resize(object sender, EventArgs e)
        {
            AdjustSearchLayout();
        }

        // =======================================================
        // THUẬT TOÁN SINH MÃ CHO TỪNG THIẾT BỊ 
        // =======================================================
        private string SinhMaThietBiTuDong(string categoryId)
        {
            if (string.IsNullOrEmpty(categoryId)) return "";

            string query = "SELECT EquipmentID FROM Equipment WHERE CategoryID = @catID";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query, new SqlParameter[] { new SqlParameter("@catID", categoryId) });

            int maxNum = 0;
            foreach (DataRow row in dt.Rows)
            {
                string id = row["EquipmentID"].ToString();
                if (id.Length > categoryId.Length)
                {
                    string numPart = id.Substring(categoryId.Length);
                    if (int.TryParse(numPart, out int num))
                    {
                        if (num > maxNum) maxNum = num;
                    }
                }
            }
            int newNum = maxNum + 1;
            return categoryId + newNum.ToString("D3");
        }

        // =======================================================
        // THUẬT TOÁN KHOA HỌC: TỰ ĐỘNG GỢI Ý ĐẦU MÃ DANH MỤC MỚI
        // =======================================================
        private string LoaiBoDauTiengViet(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
            "đ", "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ", "í","ì","ỉ","ĩ","ị",
            "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
            "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự", "ý","ỳ","ỷ","ỹ","ỵ"};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
            "d", "e","e","e","e","e","e","e","e","e","e","e", "i","i","i","i","i",
            "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
            "u","u","u","u","u","u","u","u","u","u","u", "y","y","y","y","y"};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }

        private string TaoDauMaKhoaHoc(string tenLoai)
        {
            // 1. Xóa dấu, in hoa, xóa khoảng trắng thừa
            string cleanName = LoaiBoDauTiengViet(tenLoai).ToUpper().Trim();
            if (string.IsNullOrEmpty(cleanName)) return "";

            string[] words = cleanName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string basePrefix = "";

            // 2. Lấy các chữ cái đầu tiên (Khoa học)
            if (words.Length == 1)
            {
                // Nếu chỉ có 1 từ (VD: QUẠT), lấy tối đa 3 chữ cái đầu -> QUA
                basePrefix = words[0].Substring(0, Math.Min(3, words[0].Length));
            }
            else
            {
                // Nếu có nhiều từ (VD: Máy In Màu), lấy chữ cái đầu mỗi từ -> MIM
                foreach (string w in words)
                {
                    basePrefix += w[0];
                    if (basePrefix.Length == 4) break; // Chỉ lấy tối đa 4 ký tự cho gọn
                }
            }

            // 3. Quét Database chống trùng lặp
            string finalPrefix = basePrefix;
            int counter = 1;

            while (true)
            {
                string query = "SELECT COUNT(*) FROM EquipmentCategory WHERE CategoryID = @id";
                int count = (int)DataProvider.Instance.ExecuteScalar(query, new SqlParameter[] { new SqlParameter("@id", finalPrefix) });

                // Nếu chưa ai dùng mã này -> OK lấy luôn
                if (count == 0) return finalPrefix;

                // Nếu bị trùng (VD: MI đã có rồi), tự động thêm số thành MI1, MI2...
                finalPrefix = basePrefix + counter.ToString();
                counter++;
            }
        }

        // =======================================================

        private void LoadComboboxLoaiTB()
        {
            cboLoaiTB.SelectedIndexChanged -= cboLoaiTB_SelectedIndexChanged;

            string query = "SELECT CategoryID, CategoryName FROM EquipmentCategory WHERE IsDeleted = 0 OR IsDeleted IS NULL";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            DataRow newRow = dt.NewRow();
            newRow["CategoryID"] = "NEW_CAT";
            newRow["CategoryName"] = "➕THÊM LOẠI THIẾT BỊ MỚI";
            dt.Rows.Add(newRow);

            cboLoaiTB.DataSource = dt;
            cboLoaiTB.DisplayMember = "CategoryName";
            cboLoaiTB.ValueMember = "CategoryID";

            cboLoaiTB.SelectedIndexChanged += cboLoaiTB_SelectedIndexChanged;
        }

        private void cboLoaiTB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLoaiTB.SelectedValue == null) return;
            string catId = cboLoaiTB.SelectedValue.ToString();
            if (catId == "System.Data.DataRowView") return;

            if (catId == "NEW_CAT")
            {
                HienThiCuaSoThemLoaiMoi();
                return;
            }

            if (string.IsNullOrEmpty(selectedEquipmentID))
            {
                txtMaTB.Text = SinhMaThietBiTuDong(catId);
            }
        }

        private void HienThiCuaSoThemLoaiMoi()
        {
            Form prompt = new Form()
            {
                Width = 370,
                Height = 250,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Thêm Danh Mục Thiết Bị",
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label lblName = new Label() { Left = 20, Top = 20, Text = "Tên loại (Gõ để tự sinh mã):", Width = 300 };
            TextBox txtName = new TextBox() { Left = 20, Top = 45, Width = 310 };

            Label lblPrefix = new Label() { Left = 20, Top = 80, Text = "Đầu mã (Có thể sửa lại nếu muốn):", Width = 300 };
            TextBox txtPrefix = new TextBox() { Left = 20, Top = 105, Width = 310, CharacterCasing = CharacterCasing.Upper };

            Button btnSave = new Button() { Text = "Lưu", Left = 150, Top = 160, Width = 80, DialogResult = DialogResult.OK };
            Button btnCancel = new Button() { Text = "Hủy", Left = 250, Top = 160, Width = 80, DialogResult = DialogResult.Cancel };

            prompt.Controls.Add(lblName); prompt.Controls.Add(txtName);
            prompt.Controls.Add(lblPrefix); prompt.Controls.Add(txtPrefix);
            prompt.Controls.Add(btnSave); prompt.Controls.Add(btnCancel);
            prompt.AcceptButton = btnSave; prompt.CancelButton = btnCancel;

            // Sự kiện Real-time: Ngay khi người dùng gõ Tên, Đầu mã tự động nhảy theo
            txtName.TextChanged += (s, ev) =>
            {
                string input = txtName.Text.Trim();
                if (input.Length > 0)
                {
                    txtPrefix.Text = TaoDauMaKhoaHoc(input);
                }
                else
                {
                    txtPrefix.Clear();
                }
            };

            if (prompt.ShowDialog() == DialogResult.OK)
            {
                string newCatName = txtName.Text.Trim();
                string newPrefix = txtPrefix.Text.Trim();

                if (string.IsNullOrEmpty(newCatName) || string.IsNullOrEmpty(newPrefix))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ Tên loại và Đầu mã!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LoadComboboxLoaiTB(); return;
                }

                try
                {
                    string query = "INSERT INTO EquipmentCategory (CategoryID, CategoryName, IsDeleted) VALUES (@id, @name, 0)";
                    DataProvider.Instance.ExecuteNonQuery(query, new SqlParameter[] {
                        new SqlParameter("@id", newPrefix),
                        new SqlParameter("@name", newCatName)
                    });

                    MessageBox.Show("Thêm danh mục thiết bị thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadComboboxLoaiTB();
                    cboLoaiTB.SelectedValue = newPrefix;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đầu mã đã tồn tại hoặc có lỗi:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadComboboxLoaiTB();
                }
            }
            else
            {
                LoadComboboxLoaiTB();
            }
        }

        private void LoadComboboxKhoaPhong()
        {
            string query = "SELECT DepartmentID, DepartmentName FROM Department WHERE IsDeleted = 0 OR IsDeleted IS NULL";
            cboKhoaPhong.DataSource = DataProvider.Instance.ExecuteQuery(query);
            cboKhoaPhong.DisplayMember = "DepartmentName";
            cboKhoaPhong.ValueMember = "DepartmentID";
        }

        private void FormatGrid()
        {
            if (dgvThietBi.Columns.Contains("EquipmentID")) dgvThietBi.Columns["EquipmentID"].HeaderText = "Mã TB";
            if (dgvThietBi.Columns.Contains("EquipmentName")) dgvThietBi.Columns["EquipmentName"].HeaderText = "Tên Thiết Bị";
            if (dgvThietBi.Columns.Contains("CategoryName")) dgvThietBi.Columns["CategoryName"].HeaderText = "Phân Loại";
            if (dgvThietBi.Columns.Contains("DepartmentName")) dgvThietBi.Columns["DepartmentName"].HeaderText = "Khoa/Phòng";
            if (dgvThietBi.Columns.Contains("ImportDate")) dgvThietBi.Columns["ImportDate"].HeaderText = "Ngày Nhập";
            if (dgvThietBi.Columns.Contains("PurchasePrice"))
            {
                dgvThietBi.Columns["PurchasePrice"].HeaderText = "Giá Nhập";
                dgvThietBi.Columns["PurchasePrice"].DefaultCellStyle.Format = "N0";
            }
            if (dgvThietBi.Columns.Contains("Condition")) dgvThietBi.Columns["Condition"].HeaderText = "Tình Trạng";

            if (dgvThietBi.Columns.Contains("CategoryID")) dgvThietBi.Columns["CategoryID"].Visible = false;
            if (dgvThietBi.Columns.Contains("DepartmentID")) dgvThietBi.Columns["DepartmentID"].Visible = false;
        }

        private void LoadData()
        {
            string query = @"
                SELECT e.EquipmentID, e.EquipmentName, c.CategoryName, d.DepartmentName, 
                       e.ImportDate, e.PurchasePrice, e.Condition, 
                       e.CategoryID, e.DepartmentID 
                FROM Equipment e
                LEFT JOIN EquipmentCategory c ON e.CategoryID = c.CategoryID
                LEFT JOIN Department d ON e.DepartmentID = d.DepartmentID
                WHERE e.IsDeleted = 0 OR e.IsDeleted IS NULL";

            dgvThietBi.DataSource = DataProvider.Instance.ExecuteQuery(query);
            FormatGrid();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            pnlControls.Visible = false;
            pnlSearch.Visible = true;
            txtSearch.Focus();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            searchTimer.Stop();
            searchTimer.Start();
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();

            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadData();
                return;
            }

            string fuzzyKey = "%";
            foreach (char c in keyword) { fuzzyKey += c + "%"; }

            string query = @"
                SELECT e.EquipmentID, e.EquipmentName, c.CategoryName, d.DepartmentName, 
                       e.ImportDate, e.PurchasePrice, e.Condition,
                       e.CategoryID, e.DepartmentID 
                FROM Equipment e
                LEFT JOIN EquipmentCategory c ON e.CategoryID = c.CategoryID
                LEFT JOIN Department d ON e.DepartmentID = d.DepartmentID
                WHERE (e.IsDeleted = 0 OR e.IsDeleted IS NULL)
                AND (
                    (e.EquipmentName + ' ' + ISNULL(c.CategoryName, '') + ' ' + ISNULL(d.DepartmentName, '')) LIKE @fuzzy
                    OR e.EquipmentID LIKE @normal
                )";

            SqlParameter[] param = {
                new SqlParameter("@fuzzy", fuzzyKey),
                new SqlParameter("@normal", "%" + keyword + "%")
            };

            dgvThietBi.DataSource = DataProvider.Instance.ExecuteQuery(query, param);
            FormatGrid();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            pnlSearch.Visible = false;
            pnlControls.Visible = true;
            txtSearch.Clear();

            selectedEquipmentID = "";
            txtTenTB.Clear(); txtGiaTien.Clear();

            if (cboKhoaPhong.Items.Count > 0) cboKhoaPhong.SelectedIndex = 0;
            if (cboTinhTrang.Items.Count > 0) cboTinhTrang.SelectedIndex = 0;

            if (cboLoaiTB.Items.Count > 0)
            {
                cboLoaiTB.SelectedIndex = 0;
                string catId = cboLoaiTB.SelectedValue?.ToString();
                if (catId != null && catId != "System.Data.DataRowView" && catId != "NEW_CAT")
                {
                    txtMaTB.Text = SinhMaThietBiTuDong(catId);
                }
            }

            LoadData();
        }

        private void AdjustSearchLayout()
        {
            int startX = (pnlSearch.Width - txtSearch.Width) / 2;
            txtSearch.Location = new Point(startX, (pnlSearch.Height - txtSearch.Height) / 2);
        }

        private void btnBarcode_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedEquipmentID))
            {
                MessageBox.Show("Vui lòng chọn một thiết bị từ danh sách để xem mã vạch!", "Thông báo");
                return;
            }
            FrmBarcodeViewer barcodeForm = new FrmBarcodeViewer(selectedEquipmentID, txtTenTB.Text);
            barcodeForm.ShowDialog();
        }

        private void dgvThietBi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvThietBi.Rows[e.RowIndex];
                selectedEquipmentID = row.Cells["EquipmentID"].Value.ToString();

                txtMaTB.Text = selectedEquipmentID;
                txtTenTB.Text = row.Cells["EquipmentName"].Value.ToString();

                if (dgvThietBi.Columns.Contains("CategoryID")) cboLoaiTB.SelectedValue = row.Cells["CategoryID"].Value;
                if (dgvThietBi.Columns.Contains("DepartmentID")) cboKhoaPhong.SelectedValue = row.Cells["DepartmentID"].Value;

                txtGiaTien.Text = row.Cells["PurchasePrice"].Value.ToString();
                cboTinhTrang.Text = row.Cells["Condition"].Value.ToString();
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaTB.Text) || string.IsNullOrEmpty(txtTenTB.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ Mã và Tên thiết bị!"); return;
            }
            decimal price = 0; decimal.TryParse(txtGiaTien.Text, out price);

            string query = @"INSERT INTO Equipment (EquipmentID, EquipmentName, CategoryID, DepartmentID, ImportDate, PurchasePrice, Condition, UpdatedAt, IsDeleted) 
                             VALUES (@id, @name, @cat, @dept, @date, @price, @condition, GETDATE(), 0)";

            SqlParameter[] p = {
                new SqlParameter("@id", txtMaTB.Text), new SqlParameter("@name", txtTenTB.Text),
                new SqlParameter("@cat", cboLoaiTB.SelectedValue), new SqlParameter("@dept", cboKhoaPhong.SelectedValue),
                new SqlParameter("@date", dtpNgayNhap.Value), new SqlParameter("@price", price),
                new SqlParameter("@condition", cboTinhTrang.Text)
            };
            DataProvider.Instance.ExecuteNonQuery(query, p);
            MessageBox.Show("Thêm mới thành công!");
            btnLamMoi_Click(null, null);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedEquipmentID)) { MessageBox.Show("Chọn thiết bị cần sửa!"); return; }
            decimal price = 0; decimal.TryParse(txtGiaTien.Text, out price);

            string query = @"UPDATE Equipment SET EquipmentName=@n, CategoryID=@c, DepartmentID=@d, 
                             ImportDate=@dt, PurchasePrice=@p, Condition=@cond, UpdatedAt=GETDATE() WHERE EquipmentID=@id";
            SqlParameter[] p = {
                new SqlParameter("@n", txtTenTB.Text), new SqlParameter("@c", cboLoaiTB.SelectedValue),
                new SqlParameter("@d", cboKhoaPhong.SelectedValue), new SqlParameter("@dt", dtpNgayNhap.Value),
                new SqlParameter("@p", price), new SqlParameter("@cond", cboTinhTrang.Text), new SqlParameter("@id", selectedEquipmentID)
            };
            DataProvider.Instance.ExecuteNonQuery(query, p);
            MessageBox.Show("Cập nhật thành công!");
            btnLamMoi_Click(null, null);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedEquipmentID)) return;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string query = "UPDATE Equipment SET IsDeleted = 1 WHERE EquipmentID = @id";
                DataProvider.Instance.ExecuteNonQuery(query, new SqlParameter[] { new SqlParameter("@id", selectedEquipmentID) });
                MessageBox.Show("Đã xóa thiết bị!");
                btnLamMoi_Click(null, null);
            }
        }

        private void CheckMaintenanceAlert()
        {
            try
            {
                string q = "SELECT COUNT(*) FROM Equipment WHERE NgayBaoTriDinhKy <= GETDATE() AND Condition != N'Đề xuất thanh lý' AND IsDeleted = 0";
                int count = (int)DataProvider.Instance.ExecuteScalar(q);
                if (count > 0) MessageBox.Show($"Hệ thống: Có {count} thiết bị cần bảo trì định kỳ!", "Cảnh báo bảo trì", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch { }
        }

        private void btnInQR_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedEquipmentID))
            {
                MessageBox.Show("Vui lòng chọn một thiết bị từ danh sách!", "Thông báo");
                return;
            }
            new FrmQRThietBi(selectedEquipmentID, txtTenTB.Text).ShowDialog();
        }
    }
}