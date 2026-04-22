using Lib_Equipment.Database;
using Lib_Equipment.Helpers;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Lib_Equipment
{
    public partial class FrmBaoTriThietBi : Form
    {
        public FrmBaoTriThietBi()
        {
            InitializeComponent();
        }

        private void FrmBaoTriThietBi_Load(object sender, EventArgs e)
        {
            LoadEquipment();
            LoadNextID();
            txtTenTB.ReadOnly = true;
            txtMaPhieu.Enabled = false;
            txtTenTB.BackColor = Color.WhiteSmoke;
            txtMaPhieu.Text = "BT_" + DateTime.Now.ToString("yyyyMMdd_HHmm");
            txtMaPhieu.ReadOnly = true;
            txtMaTB.Focus();
            cboHanhDong.Items.Clear();
            cboHanhDong.Items.Add("Đang bảo trì");
            cboHanhDong.Items.Add("Đề xuất thanh lý");
            cboHanhDong.StartIndex = 0;
        }

        private void LoadNextID()
        {
            try
            {
                string q = "SELECT ISNULL(MAX(MaintenanceID), 0) + 1 FROM MaintenanceRecord";
                txtMaPhieu.Text = "BT_" + Convert.ToInt32(DataProvider.Instance.ExecuteQuery(q).Rows[0][0]).ToString("D3");
            }
            catch { txtMaPhieu.Text = "BT_001"; }
        }

        private void LoadEquipment()
        {
            string query = "SELECT EquipmentID, EquipmentName, Condition FROM Equipment WHERE IsDeleted = 0";
            dgvThietBi.DataSource = DataProvider.Instance.ExecuteQuery(query);
            dgvThietBi.Columns["EquipmentID"].HeaderText = "Mã TB";
            dgvThietBi.Columns["EquipmentName"].HeaderText = "Tên TB";
            dgvThietBi.Columns["Condition"].HeaderText = "Tình trạng";
        }
        // Thêm sự kiện này để khi máy sửa xong, Double Click là nó về trạng thái "Tốt"
        private void dgvThietBi_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string maTB = dgvThietBi.Rows[e.RowIndex].Cells["EquipmentID"].Value.ToString();
                string tinhTrang = dgvThietBi.Rows[e.RowIndex].Cells["Condition"].Value.ToString();

                if (tinhTrang == "Đang bảo trì")
                {
                    DialogResult dr = MessageBox.Show($"Xác nhận thiết bị {maTB} đã sửa xong và hoạt động tốt?", "Nghiệm thu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        string sql = "UPDATE Equipment SET Condition = N'Tốt', UpdatedAt = GETDATE() WHERE EquipmentID = @id";
                        DataProvider.Instance.ExecuteNonQuery(sql, new SqlParameter[] { new SqlParameter("@id", maTB) });

                        MessageBox.Show("Đã đưa thiết bị về trạng thái sẵn sàng sử dụng!");
                        LoadEquipment(); // Load lại lưới
                    }
                }
            }
        }
        private void dgvThietBi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMaTB.Text = dgvThietBi.Rows[e.RowIndex].Cells["EquipmentID"].Value.ToString();
                txtTenTB.Text = dgvThietBi.Rows[e.RowIndex].Cells["EquipmentName"].Value.ToString();
            }
        }

        private void btnThucHien_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaTB.Text))
            {
                MessageBox.Show("Vui lòng chọn thiết bị từ danh sách bên phải!");
                return;
            }

            decimal cost = 0;
            decimal.TryParse(txtCost.Text, out cost);

            // CÂU LỆNH SQL CHUẨN (KHÔNG CÓ MESSAGEBOX BÊN TRONG)
            string sql = @"
                BEGIN TRAN;
                BEGIN TRY
                    INSERT INTO MaintenanceRecord (EquipmentID, CreatedBy, MaintenanceDate, Description, Cost, Vendor, IsDeleted)
                    VALUES (@eid, @user, @date, @desc, @cost, @vendor, 0);

                    UPDATE Equipment SET Condition = @status WHERE EquipmentID = @eid;

                    COMMIT TRAN;
                END TRY
                BEGIN CATCH
                    ROLLBACK TRAN;
                    THROW;
                END CATCH;";

            SqlParameter[] parameters = {
                new SqlParameter("@eid", txtMaTB.Text),
                new SqlParameter("@user", AppSession.Username ?? "ADMIN"),
                new SqlParameter("@date", dtpNgayBT.Value),
                new SqlParameter("@desc", txtDescription.Text),
                new SqlParameter("@cost", cost),
                new SqlParameter("@vendor", txtVendor.Text),
                new SqlParameter("@status", cboHanhDong.Text)
            };

            try
            {
                DataProvider.Instance.ExecuteNonQuery(sql, parameters);
                MessageBox.Show("Thực hiện nghiệp vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadEquipment();
                LoadNextID();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi");
            }
        }

        private void ClearForm()
        {
            txtMaTB.Clear();
            txtTenTB.Clear();
            txtVendor.Clear();
            txtCost.Clear();
            txtDescription.Clear();
        }
        double tempGiaNhap = 0;
        DateTime tempNgayNhap = DateTime.Now; // Biến lưu ngày nhập thiết bị

        // 1. HÀM TÍNH GIÁ THANH LÝ (Theo đúng công thức mới)
        private double TinhGiaThanhLy(double giaGoc, DateTime ngayNhap, double chiPhiSua)
        {
            int soNam = DateTime.Now.Year - ngayNhap.Year;
            if (soNam < 0) soNam = 0;

            double tiLeThuHoi = 0;

            // 0-5 năm: 5% | 5-10 năm: 10% | > 10 năm: 15%
            if (soNam <= 5) tiLeThuHoi = 0.05;
            else if (soNam <= 10) tiLeThuHoi = 0.10;
            else tiLeThuHoi = 0.15;

            // Giá thanh lý = (Tỷ lệ * Giá nhập) + 10% phí bảo trì
            double giaThanhLy = (giaGoc * tiLeThuHoi) + (chiPhiSua * 0.25);

            // Đảm bảo giá thu hồi không bị âm (nếu phí sửa quá cao thì cùng lắm là cho không đồng nát)
            return giaThanhLy > 0 ? giaThanhLy : 0;
        }

        // 2. LOGIC KIỂM TRA THỜI GIAN NHẬP LÂU KHI BẮN MÃ MÁY
        private void txtMaTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                string maTB = txtMaTB.Text.Trim();
                if (string.IsNullOrEmpty(maTB)) return;

                string query = "SELECT EquipmentName, Condition, PurchasePrice, ImportDate FROM Equipment WHERE EquipmentID = @id";
                try
                {
                    DataTable dt = DataProvider.Instance.ExecuteQuery(query, new SqlParameter[] { new SqlParameter("@id", maTB) });

                    if (dt.Rows.Count > 0)
                    {
                        txtTenTB.Text = dt.Rows[0]["EquipmentName"].ToString();

                        double.TryParse(dt.Rows[0]["PurchasePrice"].ToString(), out tempGiaNhap);
                        tempNgayNhap = Convert.ToDateTime(dt.Rows[0]["ImportDate"]);

                        int soNamSuDung = DateTime.Now.Year - tempNgayNhap.Year;
                        string condition = dt.Rows[0]["Condition"].ToString();

                        // ĐIỀU KIỆN 1: Nếu thời gian nhập quá lâu (ví dụ > 10 năm) -> Tự động đề xuất thanh lý
                        if (soNamSuDung >= 10)
                        {
                            cboHanhDong.Text = "Đề xuất thanh lý";
                            txtDescription.Text = $"Máy đã sử dụng {soNamSuDung} năm. Đạt niên hạn thanh lý.";
                            txtDescription.ForeColor = Color.DarkOrange;
                        }
                        else if (condition == "Hỏng" || condition == "Cần bảo trì")
                        {
                            cboHanhDong.Text = "Đang bảo trì";
                            txtDescription.Clear();
                        }

                        txtCost.Focus();
                        txtCost.Clear();
                    }
                    else { MessageBox.Show("Mã thiết bị không tồn tại!"); }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        // 3. LOGIC KIỂM TRA CHI PHÍ BẢO TRÌ QUÁ 50%
        private void txtCost_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtCost.Text, out double chiPhiSua))
            {
                // ĐIỀU KIỆN 2: Chi phí bảo trì >= 50% giá trị sản phẩm
                if (tempGiaNhap > 0 && chiPhiSua >= (tempGiaNhap / 2))
                {
                    // Sửa chữ "Đã thanh lý" thành "Đề xuất thanh lý"
                    cboHanhDong.Text = "Đề xuất thanh lý";

                    double giaThanhLy = TinhGiaThanhLy(tempGiaNhap, tempNgayNhap, chiPhiSua);
                    int soNam = DateTime.Now.Year - tempNgayNhap.Year;

                    txtDescription.Text = $"--- ĐỀ XUẤT THANH LÝ ---\r\n" +
                                         $"- Lý do: Phí sửa chữa quá 50% nguyên giá.\r\n" +
                                         $"- Tuổi thọ: {soNam} năm\r\n" +
                                         $"- Giá trị đề xuất bán: {giaThanhLy:N0} VNĐ";
                    txtDescription.ForeColor = Color.Red;
                }
                else
                {
                    // Nếu người dùng xóa bớt số 0 đi (tiền sửa lại rẻ) thì quay về trạng thái bảo trì
                    if (cboHanhDong.Text == "Đề xuất thanh lý" && txtDescription.Text.Contains("Phí sửa chữa quá 50%"))
                    {
                        cboHanhDong.Text = "Đang bảo trì";
                        txtDescription.Text = "";
                        txtDescription.ForeColor = Color.Black;
                    }
                }
            }
        }
    }
}