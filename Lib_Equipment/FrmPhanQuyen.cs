using Lib_Equipment.Database;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Lib_Equipment
{
    public partial class FrmPhanQuyen : Form
    {
        private string selectedRoleID = "";

        // 1. THÊM BIẾN CỜ KHÓA ĐỂ CHỐNG LỖI PHẢN ỨNG DÂY CHUYỀN
        private bool isUpdatingCheck = false;

        public FrmPhanQuyen()
        {
            InitializeComponent();
            this.clbMenu.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbMenu_ItemCheck);
        }

        private void FrmPhanQuyen_Load(object sender, EventArgs e)
        {
            clbMenu.CheckOnClick = true; // Bắt buộc để click 1 lần là ăn ngay
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

        private void LoadDanhSachMenu()
        {
            // ĐÃ SỬA SQL Ở ĐÂY: Bổ sung "OR MenuID LIKE '%BanSao'" vào Nhóm 3 (Thư viện)
            string query = @"
                SELECT MenuID, MenuName 
                FROM SysMenu 
                ORDER BY 
                    CASE 
                        WHEN MenuID = 'btnTrangChu' THEN 1
                        WHEN MenuID LIKE 'btnHeThong%' OR MenuID LIKE 'btnSub%TaiKhoan' OR MenuID LIKE 'btnSub%PhanQuyen' OR MenuID LIKE 'btnSub%SaoLuu' THEN 2
                        WHEN MenuID LIKE 'btnThuVien%' OR MenuID LIKE 'btnSub%Sach' OR MenuID LIKE '%BanSao' OR MenuID LIKE 'btnSub%DocGia' OR MenuID LIKE 'btnSub%MuonTra' THEN 3
                        WHEN MenuID LIKE 'btnThietBi%' OR MenuID LIKE 'btnSub%DanhMucTB' OR MenuID LIKE 'btnSub%LuanChuyen' OR MenuID LIKE 'btnSub%BaoTri' THEN 4
                        WHEN MenuID LIKE 'btnBaoCao%' OR MenuID LIKE 'btnSub%BCThuVien' OR MenuID LIKE 'btnSub%BCThietBi' THEN 5
                        ELSE 6 
                    END,
                    CASE WHEN MenuName NOT LIKE '%---%' THEN 0 ELSE 1 END,
                    MenuName ASC";

            DataTable dtMenu = DataProvider.Instance.ExecuteQuery(query);

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

                // Bật cờ khóa trước khi dùng code để load dữ liệu cũ lên
                isUpdatingCheck = true;

                for (int i = 0; i < clbMenu.Items.Count; i++)
                {
                    clbMenu.SetItemChecked(i, false);
                }

                string query = "SELECT MenuID FROM RolePermission WHERE RoleID = @roleId";
                SqlParameter[] param = { new SqlParameter("@roleId", selectedRoleID) };
                DataTable dtPermissions = DataProvider.Instance.ExecuteQuery(query, param);

                foreach (DataRow row in dtPermissions.Rows)
                {
                    string allowedMenuID = row["MenuID"].ToString().Trim();
                    for (int i = 0; i < clbMenu.Items.Count; i++)
                    {
                        DataRowView item = (DataRowView)clbMenu.Items[i];
                        if (item["MenuID"].ToString().Trim() == allowedMenuID)
                        {
                            clbMenu.SetItemChecked(i, true);
                            break;
                        }
                    }
                }

                // Mở cờ khóa sau khi load xong
                isUpdatingCheck = false;
            }
        }

        // =======================================================
        // 2. LOGIC TÍCH MỤC (ĐÃ FIX CHUẨN)
        // =======================================================
        private void clbMenu_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // NGĂN CHẶN LỖI DÂY CHUYỀN
            if (isUpdatingCheck) return;

            this.BeginInvoke(new MethodInvoker(() => {

                isUpdatingCheck = true; // Khóa lại không cho sự kiện khác xen vào

                DataRowView currentItem = (DataRowView)clbMenu.Items[e.Index];
                string menuName = currentItem["MenuName"].ToString();
                bool isChecked = (e.NewValue == CheckState.Checked);

                // NẾU LÀ MỤC CHA
                if (!menuName.Contains("---"))
                {
                    // Tích tất cả mục con bên dưới nó
                    for (int i = e.Index + 1; i < clbMenu.Items.Count; i++)
                    {
                        DataRowView nextItem = (DataRowView)clbMenu.Items[i];
                        if (nextItem["MenuName"].ToString().Contains("---"))
                        {
                            clbMenu.SetItemChecked(i, isChecked);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                // NẾU LÀ MỤC CON
                else
                {
                    // Chỉ khi mục con ĐƯỢC TÍCH, ta mới tự động tích mục cha
                    if (isChecked)
                    {
                        for (int i = e.Index - 1; i >= 0; i--)
                        {
                            DataRowView prevItem = (DataRowView)clbMenu.Items[i];
                            if (!prevItem["MenuName"].ToString().Contains("---"))
                            {
                                clbMenu.SetItemChecked(i, true);
                                break;
                            }
                        }
                    }
                }

                isUpdatingCheck = false; // Xong việc thì mở khóa
            }));
        }

        // =======================================================
        // 3. LƯU QUYỀN VÀO DATABASE
        // =======================================================
        private void btnLuuQuyen_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedRoleID))
            {
                MessageBox.Show("Vui lòng chọn một quyền ở bảng bên trái trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Bước 1: Quét sạch quyền cũ
                string deleteQuery = "DELETE FROM RolePermission WHERE RoleID = @role";
                DataProvider.Instance.ExecuteNonQuery(deleteQuery, new SqlParameter[] { new SqlParameter("@role", selectedRoleID) });

                // Bước 2: Nạp các quyền mới đang được tích
                foreach (object itemChecked in clbMenu.CheckedItems)
                {
                    DataRowView item = (DataRowView)itemChecked;
                    string menuID = item["MenuID"].ToString().Trim();

                    string insertQuery = "INSERT INTO RolePermission (RoleID, MenuID) VALUES (@role, @menu)";
                    SqlParameter[] p = {
                        new SqlParameter("@role", selectedRoleID),
                        new SqlParameter("@menu", menuID)
                    };
                    DataProvider.Instance.ExecuteNonQuery(insertQuery, p);
                }

                MessageBox.Show($"Lưu thành công phân quyền mới cho: {selectedRoleID}\nVui lòng đăng nhập lại để thay đổi có hiệu lực.", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật SQL: " + ex.Message, "Lỗi");
            }
        }
    }
}