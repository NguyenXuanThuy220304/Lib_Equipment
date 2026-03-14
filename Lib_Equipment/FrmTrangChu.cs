using System;
using System.Windows.Forms;

namespace Lib_Equipment
{
    public partial class FrmTrangChu : Form
    {
        public FrmTrangChu()
        {
            InitializeComponent();
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
    }
}