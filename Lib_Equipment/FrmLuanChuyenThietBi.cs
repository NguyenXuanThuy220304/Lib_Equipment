using Lib_Equipment.Database;
using Lib_Equipment.Helpers;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Lib_Equipment
{
    public partial class FrmLuanChuyenThietBi : Form
    {
        public FrmLuanChuyenThietBi()
        {
            InitializeComponent();
        }

        private void FrmLuanChuyenThietBi_Load(object sender, EventArgs e)
        {
            LoadKhoaPhong();
            SetupDataGridView();

            LoadNextTransferID();
            txtMaPhieu.Enabled = false; // Vẫn khóa lại để bảo vệ dữ liệu
        }

        // =======================================================
        // 1. TẢI COMBOBOX & CẤU HÌNH LƯỚI CHỌN
        // =======================================================
        private void LoadKhoaPhong()
        {
            string query = "SELECT DepartmentID, DepartmentName FROM Department WHERE IsDeleted = 0 OR IsDeleted IS NULL";
            DataTable dtTuKhoa = DataProvider.Instance.ExecuteQuery(query);
            DataTable dtDenKhoa = dtTuKhoa.Copy();

            cboTuKhoa.DataSource = dtTuKhoa;
            cboTuKhoa.DisplayMember = "DepartmentName";
            cboTuKhoa.ValueMember = "DepartmentID";

            cboDenKhoa.DataSource = dtDenKhoa;
            cboDenKhoa.DisplayMember = "DepartmentName";
            cboDenKhoa.ValueMember = "DepartmentID";
        }

        private void SetupDataGridView()
        {
            DataGridViewCheckBoxColumn chkCol = new DataGridViewCheckBoxColumn();
            chkCol.Name = "chkSelect";
            chkCol.HeaderText = "Chọn";
            chkCol.Width = 60;
            chkCol.ReadOnly = false;
            dgvThietBi.Columns.Add(chkCol);
        }

        // =======================================================
        // 2. HIỂN THỊ CÁC MÁY CỦA KHOA XUẤT
        // =======================================================
        private void cboTuKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTuKhoa.SelectedValue != null && cboTuKhoa.SelectedValue is string)
            {
                string deptId = cboTuKhoa.SelectedValue.ToString();

                string query = @"
                    SELECT EquipmentID, EquipmentName, Condition 
                    FROM Equipment 
                    WHERE DepartmentID = @deptId AND (IsDeleted = 0 OR IsDeleted IS NULL)";

                DataTable dt = DataProvider.Instance.ExecuteQuery(query, new SqlParameter[] { new SqlParameter("@deptId", deptId) });

                dgvThietBi.DataSource = dt;

                dgvThietBi.Columns["EquipmentID"].HeaderText = "Mã TB";
                dgvThietBi.Columns["EquipmentID"].ReadOnly = true;

                dgvThietBi.Columns["EquipmentName"].HeaderText = "Tên Thiết Bị";
                dgvThietBi.Columns["EquipmentName"].ReadOnly = true;
                dgvThietBi.Columns["EquipmentName"].Width = 200;

                dgvThietBi.Columns["Condition"].HeaderText = "Tình trạng";
                dgvThietBi.Columns["Condition"].ReadOnly = true;
            }
        }

        // =======================================================
        // 3. THUẬT TOÁN TRANSACTION LUÂN CHUYỂN (ĐÃ FIX LỖI IDENTITY)
        // =======================================================
        private void btnThucHien_Click(object sender, EventArgs e)
        {
            if (cboTuKhoa.SelectedValue.ToString() == cboDenKhoa.SelectedValue.ToString())
            {
                MessageBox.Show("Khoa nhận không được trùng với Khoa xuất!", "Lỗi logic", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int checkedCount = 0;
            foreach (DataGridViewRow row in dgvThietBi.Rows)
            {
                if (Convert.ToBoolean(row.Cells["chkSelect"].Value) == true) checkedCount++;
            }

            if (checkedCount == 0)
            {
                MessageBox.Show("Bạn chưa tick chọn thiết bị nào để luân chuyển!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show($"Bạn sắp luân chuyển {checkedCount} thiết bị sang {cboDenKhoa.Text}.\nXác nhận thực hiện?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr != DialogResult.Yes) return;

            string currentUser = AppSession.Username ?? "ADMIN";

            // SỬA Ở ĐÂY: KHÔNG chèn TransferID, và dùng SCOPE_IDENTITY() để lấy ID vừa sinh ra
            string batchSql = @"
                BEGIN TRY
                    BEGIN TRAN;

                    -- 1. Lưu thông tin chung (bỏ cột TransferID vì nó tự tăng)
                    INSERT INTO TransferRecord (FromDepartmentID, ToDepartmentID, CreatedBy, TransferDate, Reason, IsDeleted)
                    VALUES (@fromDept, @toDept, @user, @date, @reason, 0);

                    -- 2. Lấy cái ID tự động vừa tạo ra lưu vào biến @newId
                    DECLARE @newId INT = SCOPE_IDENTITY();
            ";

            // 3. Loop qua lưới, dùng @newId để Insert Detail
            foreach (DataGridViewRow row in dgvThietBi.Rows)
            {
                if (Convert.ToBoolean(row.Cells["chkSelect"].Value) == true)
                {
                    string eqId = row.Cells["EquipmentID"].Value.ToString();
                    string cond = row.Cells["Condition"].Value.ToString();

                    batchSql += $@"
                        INSERT INTO TransferDetail (TransferID, EquipmentID, ConditionAtTransfer) 
                        VALUES (@newId, '{eqId}', N'{cond}');
                        
                        UPDATE Equipment 
                        SET DepartmentID = @toDept, UpdatedAt = GETDATE() 
                        WHERE EquipmentID = '{eqId}';
                    ";
                }
            }

            batchSql += @"
                    COMMIT TRAN;
                END TRY
                BEGIN CATCH
                    ROLLBACK TRAN;
                    THROW;
                END CATCH;
            ";

            // Truyền tham số (đã xóa tham số @transferId cũ đi)
            SqlParameter[] param = {
                new SqlParameter("@fromDept", cboTuKhoa.SelectedValue),
                new SqlParameter("@toDept", cboDenKhoa.SelectedValue),
                new SqlParameter("@user", currentUser),
                new SqlParameter("@date", dtpNgayChuyen.Value),
                new SqlParameter("@reason", txtLyDo.Text.Trim())
            };

            try
            {
                DataProvider.Instance.ExecuteNonQuery(batchSql, param);

                MessageBox.Show("Luân chuyển thành công! Dữ liệu đã được lưu vết vào hệ thống.", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reload lại danh sách thiết bị
                cboTuKhoa_SelectedIndexChanged(null, null);
                txtLyDo.Clear();
                LoadNextTransferID();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi giao dịch SQL: \n" + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Hàm tự động lấy ID tiếp theo và trang trí thành chuẩn LC_001
        private void LoadNextTransferID()
        {
            try
            {
                string query = "SELECT ISNULL(MAX(TransferID), 0) + 1 FROM TransferRecord";
                DataTable dt = DataProvider.Instance.ExecuteQuery(query);

                if (dt.Rows.Count > 0)
                {
                    int nextId = Convert.ToInt32(dt.Rows[0][0]);
                    // .ToString("D3") sẽ tự động chèn số 0 vào trước (VD: số 1 -> 001, số 15 -> 015)
                    txtMaPhieu.Text = "LC_" + nextId.ToString("D3");
                }
            }
            catch
            {
                txtMaPhieu.Text = "LC_001";
            }
        }
    }
}