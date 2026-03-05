using Lib_Equipment.Database;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace Lib_Equipment
{
    public partial class FrmSaoLuuPhucHoi : Form
    {
        // Tên cơ sở dữ liệu của bạn - BẮT BUỘC PHẢI CHUẨN XÁC
        private string dbName = "Lib_EquipmentDB";

        public FrmSaoLuuPhucHoi()
        {
            InitializeComponent();
        }

        // =======================================================
        // 1. CHỨC NĂNG SAO LƯU (BACKUP)
        // =======================================================
        private void btnBrowseBackup_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Backup Files (*.bak)|*.bak";
            sfd.Title = "Chọn nơi lưu file Sao lưu";
            // Tự động tạo tên file có gắn ngày tháng để không bị trùng
            sfd.FileName = $"Backup_UNETI_Lib_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.bak";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                txtBackupPath.Text = sfd.FileName;
            }
        }

        private void btnThucHienBackup_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBackupPath.Text))
            {
                MessageBox.Show("Vui lòng chọn đường dẫn để lưu file Backup!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Thay vì dùng DataProvider.Instance có thể bị timeout, ta tạo một truy vấn thẳng
                string backupSQL = $"BACKUP DATABASE [{dbName}] TO DISK = '{txtBackupPath.Text}'";

                DataProvider.Instance.ExecuteNonQuery(backupSQL);

                MessageBox.Show("Đã sao lưu cơ sở dữ liệu thành công!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBackupPath.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi trong quá trình sao lưu: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =======================================================
        // 2. CHỨC NĂNG PHỤC HỒI (RESTORE)
        // =======================================================
        private void btnBrowseRestore_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Backup Files (*.bak)|*.bak";
            ofd.Title = "Chọn file dữ liệu để phục hồi";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtRestorePath.Text = ofd.FileName;
            }
        }

        private void btnThucHienRestore_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRestorePath.Text))
            {
                MessageBox.Show("Vui lòng chọn file .bak để phục hồi dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!File.Exists(txtRestorePath.Text))
            {
                MessageBox.Show("File không tồn tại. Vui lòng kiểm tra lại đường dẫn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult dr = MessageBox.Show(
                "CẢNH BÁO: Việc phục hồi sẽ ghi đè và xóa bỏ toàn bộ dữ liệu hiện tại của hệ thống.\nBạn có chắc chắn muốn thực hiện không?",
                "Cảnh báo rủi ro", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    /* Thuật toán Restore an toàn:
                     * 1. Chuyển ngữ cảnh sang master (bắt buộc)
                     * 2. Tắt các kết nối khác (SINGLE_USER)
                     * 3. Thực hiện RESTORE
                     * 4. Bật lại MULTI_USER
                     */
                    string restoreSQL = $@"
                        USE master;
                        ALTER DATABASE [{dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                        RESTORE DATABASE [{dbName}] FROM DISK = '{txtRestorePath.Text}' WITH REPLACE;
                        ALTER DATABASE [{dbName}] SET MULTI_USER;
                    ";

                    DataProvider.Instance.ExecuteNonQuery(restoreSQL);

                    MessageBox.Show("Đã phục hồi dữ liệu thành công! Phần mềm cần khởi động lại để áp dụng dữ liệu mới.", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Khởi động lại ứng dụng để tránh lỗi kết nối ngầm
                    Application.Restart();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi trong quá trình phục hồi (Lưu ý: Dịch vụ SQL Server cần có quyền truy cập vào file này): \n" + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}