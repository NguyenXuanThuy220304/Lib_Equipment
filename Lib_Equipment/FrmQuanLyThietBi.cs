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
        private Label lblMaintenanceAlert;

        // BÍ QUYẾT ĐỂ LƯỚI GRID VIEW VÀ FORM HỒ SƠ ĐỒNG BỘ 100%
        // Ép SQL tự tính toán Ngày bảo trì tiếp theo y hệt code C#
        private readonly string sqlDynamicMaintenanceDate = @"
            CAST(DATEADD(month, 
                CASE 
                    WHEN ISNULL(e.PurchasePrice, 0) < 10000000 THEN 5 
                    WHEN ISNULL(e.PurchasePrice, 0) < 25000000 THEN 8 
                    ELSE 12 
                END, 
                ISNULL((SELECT TOP 1 MaintenanceDate FROM MaintenanceRecord WHERE EquipmentID = e.EquipmentID ORDER BY MaintenanceDate DESC), e.ImportDate)
            ) AS DATE)";

        public FrmQuanLyThietBi()
        {
            InitializeComponent();
            dgvThietBi.DataError += (s, e) => { e.ThrowException = false; };

            searchTimer = new Timer();
            searchTimer.Interval = 300;
            searchTimer.Tick += SearchTimer_Tick;

            InitializeMaintenanceAlertLabel();
            dgvThietBi.CellFormatting += DgvThietBi_CellFormatting;
        }

        private void InitializeMaintenanceAlertLabel()
        {
            lblMaintenanceAlert = new Label
            {
                Text = "",
                ForeColor = Color.White,
                BackColor = Color.FromArgb(220, 53, 69),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = true,
                Visible = false,
                Padding = new Padding(5),
                // Bổ sung lệnh ghim chặt vào góc Trên - Phải
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            this.Controls.Add(lblMaintenanceAlert);
            lblMaintenanceAlert.BringToFront();

            lblMaintenanceAlert.Click += LblMaintenanceAlert_Click;
            lblMaintenanceAlert.Cursor = Cursors.Hand;
        }

        private void FrmQuanLyThietBi_Load(object sender, EventArgs e)
        {
            txtMaTB.Enabled = false;

            LoadComboboxLoaiTB();
            LoadComboboxKhoaPhong();

            TuDongCapNhatTrangThaiBaoTri();

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
        // THUẬT TOÁN SINH MÃ VÀ ĐẦU MÃ
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
            return categoryId + (maxNum + 1).ToString("D3");
        }

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
            string cleanName = LoaiBoDauTiengViet(tenLoai).ToUpper().Trim();
            if (string.IsNullOrEmpty(cleanName)) return "";

            string[] words = cleanName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string basePrefix = "";

            if (words.Length == 1) basePrefix = words[0].Substring(0, Math.Min(3, words[0].Length));
            else
            {
                foreach (string w in words)
                {
                    basePrefix += w[0];
                    if (basePrefix.Length == 4) break;
                }
            }

            string finalPrefix = basePrefix;
            int counter = 1;

            while (true)
            {
                string query = "SELECT COUNT(*) FROM EquipmentCategory WHERE CategoryID = @id";
                int count = (int)DataProvider.Instance.ExecuteScalar(query, new SqlParameter[] { new SqlParameter("@id", finalPrefix) });
                if (count == 0) return finalPrefix;
                finalPrefix = basePrefix + counter.ToString();
                counter++;
            }
        }

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
            if (string.IsNullOrEmpty(selectedEquipmentID)) txtMaTB.Text = SinhMaThietBiTuDong(catId);
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

            txtName.TextChanged += (s, ev) =>
            {
                string input = txtName.Text.Trim();
                if (input.Length > 0) txtPrefix.Text = TaoDauMaKhoaHoc(input);
                else txtPrefix.Clear();
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
                        new SqlParameter("@id", newPrefix), new SqlParameter("@name", newCatName)
                    });
                    MessageBox.Show("Thêm danh mục thiết bị thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadComboboxLoaiTB(); cboLoaiTB.SelectedValue = newPrefix;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đầu mã đã tồn tại hoặc có lỗi:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadComboboxLoaiTB();
                }
            }
            else LoadComboboxLoaiTB();
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

            if (dgvThietBi.Columns.Contains("ImportDate"))
            {
                dgvThietBi.Columns["ImportDate"].HeaderText = "Ngày Nhập";
                dgvThietBi.Columns["ImportDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            if (dgvThietBi.Columns.Contains("PurchasePrice"))
            {
                dgvThietBi.Columns["PurchasePrice"].HeaderText = "Giá Nhập";
                dgvThietBi.Columns["PurchasePrice"].DefaultCellStyle.Format = "N0";
            }

            if (dgvThietBi.Columns.Contains("Condition")) dgvThietBi.Columns["Condition"].HeaderText = "Tình Trạng";

            if (dgvThietBi.Columns.Contains("NgayBaoTriDinhKy"))
            {
                dgvThietBi.Columns["NgayBaoTriDinhKy"].HeaderText = "Hạn Bảo Trì";
                dgvThietBi.Columns["NgayBaoTriDinhKy"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            if (dgvThietBi.Columns.Contains("CategoryID")) dgvThietBi.Columns["CategoryID"].Visible = false;
            if (dgvThietBi.Columns.Contains("DepartmentID")) dgvThietBi.Columns["DepartmentID"].Visible = false;
        }

        private void LoadData(bool showOnlyOverdue = false)
        {
            // Thay vì dùng cột có sẵn có thể lỗi, ta ép SQL gọi thẳng công thức tính chuẩn xác
            string query = $@"
                SELECT e.EquipmentID, e.EquipmentName, c.CategoryName, d.DepartmentName, 
                       CAST(e.ImportDate AS DATE) AS ImportDate, 
                       e.PurchasePrice, e.Condition, 
                       {sqlDynamicMaintenanceDate} AS NgayBaoTriDinhKy,
                       e.CategoryID, e.DepartmentID 
                FROM Equipment e
                LEFT JOIN EquipmentCategory c ON e.CategoryID = c.CategoryID
                LEFT JOIN Department d ON e.DepartmentID = d.DepartmentID
                WHERE (e.IsDeleted = 0 OR e.IsDeleted IS NULL)";

            if (showOnlyOverdue)
            {
                query += $" AND {sqlDynamicMaintenanceDate} <= CAST(GETDATE() AS DATE) AND e.Condition != N'Đề xuất thanh lý'";
            }

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

            string query = $@"
                SELECT e.EquipmentID, e.EquipmentName, c.CategoryName, d.DepartmentName, 
                       CAST(e.ImportDate AS DATE) AS ImportDate, 
                       e.PurchasePrice, e.Condition, 
                       {sqlDynamicMaintenanceDate} AS NgayBaoTriDinhKy,
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
            CheckMaintenanceAlert();
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

                LoadData();
                CheckMaintenanceAlert();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaTB.Text) || string.IsNullOrEmpty(txtTenTB.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ Mã và Tên thiết bị!"); return;
            }
            decimal price = 0; decimal.TryParse(txtGiaTien.Text, out price);

            // Bổ sung xử lý tính NgayBaoTriDinhKy ban đầu (chuẩn theo logic giá tiền)
            int chuKyThang = price < 10000000 ? 5 : (price < 25000000 ? 8 : 12);
            DateTime importDate = dtpNgayNhap.Value;
            DateTime nextMaintenanceDate = importDate.AddMonths(chuKyThang);

            string query = @"INSERT INTO Equipment (EquipmentID, EquipmentName, CategoryID, DepartmentID, ImportDate, PurchasePrice, Condition, NgayBaoTriDinhKy, UpdatedAt, IsDeleted) 
                             VALUES (@id, @name, @cat, @dept, @date, @price, @condition, @nextMaint, GETDATE(), 0)";

            SqlParameter[] p = {
                new SqlParameter("@id", txtMaTB.Text), new SqlParameter("@name", txtTenTB.Text),
                new SqlParameter("@cat", cboLoaiTB.SelectedValue), new SqlParameter("@dept", cboKhoaPhong.SelectedValue),
                new SqlParameter("@date", importDate), new SqlParameter("@price", price),
                new SqlParameter("@condition", cboTinhTrang.Text), new SqlParameter("@nextMaint", nextMaintenanceDate)
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
                string q = $@"
                    SELECT COUNT(*) 
                    FROM Equipment e 
                    WHERE {sqlDynamicMaintenanceDate} <= CAST(GETDATE() AS DATE) 
                      AND e.Condition != N'Đề xuất thanh lý' 
                      AND (e.IsDeleted = 0 OR e.IsDeleted IS NULL)";

                int count = (int)DataProvider.Instance.ExecuteScalar(q);

                if (count > 0)
                {
                    lblMaintenanceAlert.Text = $"⚠️ Có {count} thiết bị quá hạn bảo trì! (click)";

                    // LỆNH TỰ TÍNH TOÁN ĐẨY LABEL SÁT VÀO GÓC TRÊN BÊN PHẢI (Cách mép phải 30px, mép trên 15px)
                    lblMaintenanceAlert.Left = this.ClientSize.Width - lblMaintenanceAlert.Width - 30;
                    lblMaintenanceAlert.Top = 15;

                    lblMaintenanceAlert.Visible = true;
                }
                else
                {
                    lblMaintenanceAlert.Visible = false;
                }
            }
            catch (Exception)
            {
                lblMaintenanceAlert.Visible = false;
            }
        }

        private void LblMaintenanceAlert_Click(object sender, EventArgs e)
        {
            LoadData(showOnlyOverdue: true);
        }

        private void DgvThietBi_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvThietBi.Rows.Count) return;

            if (dgvThietBi.Columns.Contains("NgayBaoTriDinhKy"))
            {
                var cellValue = dgvThietBi.Rows[e.RowIndex].Cells["NgayBaoTriDinhKy"].Value;
                string condition = dgvThietBi.Rows[e.RowIndex].Cells["Condition"].Value?.ToString();

                if (cellValue != null && cellValue != DBNull.Value && condition != "Đề xuất thanh lý")
                {
                    DateTime maintDate = Convert.ToDateTime(cellValue);
                    if (maintDate <= DateTime.Now)
                    {
                        dgvThietBi.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 204, 204);
                        dgvThietBi.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.DarkRed;
                    }
                }
            }
        }

        // =========================================================
        // HÀM CHẠY NGẦM: TỰ ĐỘNG CHUYỂN TRẠNG THÁI THIẾT BỊ QUÁ HẠN
        // =========================================================
        private void TuDongCapNhatTrangThaiBaoTri()
        {
            try
            {
                string query = $@"
                    UPDATE e 
                    SET e.Condition = N'Cần bảo trì' 
                    FROM Equipment e
                    WHERE {sqlDynamicMaintenanceDate} <= CAST(GETDATE() AS DATE) 
                      AND e.Condition NOT IN (N'Cần bảo trì', N'Đang bảo trì', N'Đề xuất thanh lý', N'Đã thanh lý') 
                      AND (e.IsDeleted = 0 OR e.IsDeleted IS NULL)";

                DataProvider.Instance.ExecuteNonQuery(query, null);
            }
            catch (Exception)
            {
                // Bỏ qua lỗi ngầm để không làm crash form
            }
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