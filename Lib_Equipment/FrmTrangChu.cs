using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lib_Equipment
{
    public partial class FrmTrangChu : Form
    {
        // 1. Khai báo danh sách chứa các bức ảnh và vị trí ảnh hiện tại
        private Image[] danhSachAnh;
        private int viTriHienTai = 0;

        public FrmTrangChu()
        {
            InitializeComponent();
        }

        private void FrmTrangChu_Load(object sender, EventArgs e)
        {
            try
            {
                // 2. Nạp ảnh từ Resources vào danh sách
                // LƯU Ý: Bạn cần nạp 3 tấm ảnh vào Properties -> Resources và đặt tên là slide1, slide2, slide3 nhé
                danhSachAnh = new Image[]
                {
                    Properties.Resources.uneti_sl1,
                    Properties.Resources.uneti_sl2,
                    Properties.Resources.uneti_sl3,
                    Properties.Resources.uneti_sl4
                };

                // Hiển thị bức ảnh đầu tiên ngay khi mở Form
                picSlider.Image = danhSachAnh[0];

                // Bắt đầu đếm giờ chuyển ảnh
                timerSlider.Start();
            }
            catch
            {
                // Tạm thời bỏ qua nếu chưa có ảnh để tránh lỗi sập chương trình
            }
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            // Cập nhật giờ phút giây
            lblClock.Text = DateTime.Now.ToString("HH:mm:ss");

            // Cập nhật thứ ngày tháng bằng tiếng Việt
            string[] thuTiengViet = { "Chủ Nhật", "Thứ Hai", "Thứ Ba", "Thứ Tư", "Thứ Năm", "Thứ Sáu", "Thứ Bảy" };
            string thu = thuTiengViet[(int)DateTime.Now.DayOfWeek];
            lblDate.Text = $"{thu}, ngày {DateTime.Now.ToString("dd/MM/yyyy")}";
        }

        // 3. Sự kiện Tick của Timer Slider (Hành động xảy ra mỗi khi hết thời gian set)
        private void timerSlider_Tick(object sender, EventArgs e)
        {
            // Kiểm tra nếu chưa có ảnh thì không làm gì cả
            if (danhSachAnh == null || danhSachAnh.Length == 0) return;

            // Tăng vị trí ảnh lên 1
            viTriHienTai++;

            // Nếu chạy đến bức ảnh cuối cùng thì vòng lại ảnh số 0
            if (viTriHienTai >= danhSachAnh.Length)
            {
                viTriHienTai = 0;
            }

            // Gán ảnh mới cho PictureBox
            picSlider.Image = danhSachAnh[viTriHienTai];
        }
    }
}