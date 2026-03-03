using Lib_Equipment.Database;
using Lib_Equipment.Helpers;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Lib_Equipment
{
    public partial class FrmPhanQuyen : Form
    {
        private string selectedRoleID = "";

        public FrmPhanQuyen()
        {
            InitializeComponent();
            // Đăng ký sự kiện Check Item nếu bạn chưa đăng ký trong Designer
            this.clbMenu.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbMenu_ItemCheck);
        }

        private void FrmPhanQuyen_Load(object sender, EventArgs e)
        {
            LoadDanhSachRole();
            LoadDanhSachMenu();
        }

        private void LoadDanhSachRole()
        {
            string query = "SELECT RoleID, RoleName FROM Role";
            DataTable dtRole = DataProvider.Instance.ExecuteQuery(query);
            dgvRole.DataSource = dtRole;

            dgvRole.Columns["RoleID"].HeaderText = "Mã Quyền";
            dgvRole.Columns["RoleID"].Width = 100;
            dgvRole.Columns["RoleName"].HeaderText = "Tên Nhóm Quyền";
        }

        // =======================================================
        // CẬP NHẬT: TẢI DANH SÁCH MENU THEO THỨ TỰ LOGIC
        // =======================================================
        private void LoadDanhSachMenu()
        {
            // Sử dụng CASE WHEN trong SQL để ép thứ tự: Nhóm Hệ thống -> Thư viện -> Thiết bị -> Báo cáo
            string query = @"
                SELECT MenuID, MenuName 
                FROM SysMenu 
                ORDER BY 
                    CASE 
                        WHEN MenuID = 'btnTrangChu' THEN 1
                        WHEN MenuID LIKE 'btnHeThong%' OR MenuID LIKE 'btnSub%TaiKhoan' OR MenuID LIKE 'btnSub%PhanQuyen' OR MenuID LIKE 'btnSub%SaoLuu' THEN 2
                        WHEN MenuID LIKE 'btnThuVien%' OR MenuID LIKE 'btnSub%Sach' OR MenuID LIKE 'btnSub%DocGia' OR MenuID LIKE 'btnSub%MuonTra' THEN 3
                        WHEN MenuID LIKE 'btnThietBi%' OR MenuID LIKE 'btnSub%DanhMucTB' OR MenuID LIKE 'btnSub%LuanChuyen' OR MenuID LIKE 'btnSub%BaoTri' THEN 4
                        WHEN MenuID LIKE 'btnBaoCao%' OR MenuID LIKE 'btnSub%BCThuVien' OR MenuID LIKE 'btnSub%BCThietBi' THEN 5
                        ELSE 6 
                    END,
                    CASE WHEN MenuName NOT LIKE '%---%' THEN 0 ELSE 1 END, -- Mục cha hiện trước mục con
                    MenuName ASC";

            DataTable dtMenu = DataProvider.Instance.ExecuteQuery(query);

            // Xóa DataSource cũ trước khi nạp mới để tránh xung đột
            ((ListBox)clbMenu).DataSource = null;
            ((ListBox)clbMenu).DataSource = dtMenu;
            ((ListBox)clbMenu).DisplayMember = "MenuName";
            ((ListBox)clbMenu).ValueMember = "MenuID";
        }

        private void dgvRole_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedRoleID = dgvRole.Rows[e.RowIndex].Cells["RoleID"].Value.ToString();

                // Hủy tick toàn bộ trước khi load quyền của Role mới
                for (int i = 0; i < clbMenu.Items.Count; i++)
                {
                    clbMenu.SetItemChecked(i, false);
                }

                string query = "SELECT MenuID FROM RolePermission WHERE RoleID = @roleId";
                SqlParameter[] param = { new SqlParameter("@roleId", selectedRoleID) };
                DataTable dtPermissions = DataProvider.Instance.ExecuteQuery(query, param);

                foreach (DataRow row in dtPermissions.Rows)
                {
                    string allowedMenuID = row["MenuID"].ToString();
                    for (int i = 0; i < clbMenu.Items.Count; i++)
                    {
                        DataRowView item = (DataRowView)clbMenu.Items[i];
                        if (item["MenuID"].ToString() == allowedMenuID)
                        {
                            clbMenu.SetItemChecked(i, true);
                            break;
                        }
                    }
                }
            }
        }

        // =======================================================
        // CẬP NHẬT: LOGIC TÍCH MỤC CHA TỰ ĐỘNG TÍCH MỤC CON
        // =======================================================
        private void clbMenu_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Sử dụng BeginInvoke để thực thi sau khi giá trị Check thực sự thay đổi
            this.BeginInvoke(new MethodInvoker(() => {
                DataRowView currentItem = (DataRowView)clbMenu.Items[e.Index];
                string menuName = currentItem["MenuName"].ToString();

                // Kiểm tra nếu là mục CHA (Không chứa dấu "---")
                if (!menuName.Contains("---"))
                {
                    bool isChecked = (e.NewValue == CheckState.Checked);

                    // Duyệt các mục phía dưới mục cha này cho đến khi gặp mục cha tiếp theo
                    for (int i = e.Index + 1; i < clbMenu.Items.Count; i++)
                    {
                        DataRowView nextItem = (DataRowView)clbMenu.Items[i];
                        string nextMenuName = nextItem["MenuName"].ToString();

                        if (nextMenuName.Contains("---"))
                        {
                            clbMenu.SetItemChecked(i, isChecked);
                        }
                        else
                        {
                            // Đã chạm đến mục cha của nhóm khác
                            break;
                        }
                    }
                }
            }));
        }

        private void btnLuuQuyen_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedRoleID))
            {
                MessageBox.Show("Vui lòng chọn một quyền bên trái!", "Thông báo");
                return;
            }

            try
            {
                // Bước 1: Xóa các bản ghi cũ
                string deleteQuery = "DELETE FROM RolePermission WHERE RoleID = @role";
                DataProvider.Instance.ExecuteNonQuery(deleteQuery, new SqlParameter[] { new SqlParameter("@role", selectedRoleID) });

                // Bước 2: Lưu tất cả mục được tích
                foreach (object itemChecked in clbMenu.CheckedItems)
                {
                    DataRowView item = (DataRowView)itemChecked;
                    string menuID = item["MenuID"].ToString();

                    string insertQuery = "INSERT INTO RolePermission (RoleID, MenuID) VALUES (@role, @menu)";
                    SqlParameter[] p = {
                        new SqlParameter("@role", selectedRoleID),
                        new SqlParameter("@menu", menuID)
                    };
                    DataProvider.Instance.ExecuteNonQuery(insertQuery, p);
                }

                MessageBox.Show($"Đã cập nhật phân quyền chi tiết cho nhóm: {selectedRoleID}", "Thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu quyền: " + ex.Message, "Lỗi");
            }
        }
    }
}