using Lib_Equipment.Database;
using Lib_Equipment.Helpers;
using System;
using System.Data;
using System.Data.SqlClient;
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
            txtMaTB.ReadOnly = true;
            txtTenTB.ReadOnly = true;
            txtMaPhieu.Enabled = false;

            cboHanhDong.Items.Clear();
            cboHanhDong.Items.Add("Đang bảo trì");
            cboHanhDong.Items.Add("Đã thanh lý");
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

            SqlParameter[] p = {
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
                DataProvider.Instance.ExecuteNonQuery(sql, p);
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
    }
}