using Do_an_OOP.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Do_an_OOP
{
    public partial class FHomeKH : Form
    {
        KHACHHANG kh = null;    
        public FHomeKH()
        {
            InitializeComponent();
        }

        private void FHomeKH_Load(object sender, EventArgs e)
        {
            kh = MyGereral.KhachHang;
            if (kh == null)
            {
                lblHienTen.Text = "CHƯA ĐĂNG NHẬP";
            }
            else
            {
                lblHienTen.Text = "Chào " + kh.TenKhachHang;
            }
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            FChonQuyen me = new FChonQuyen();
            this.Hide();
            me.ShowDialog();
            this.Show();

        }

        private void txtHienTen_Click(object sender, EventArgs e)
        {

        }

        private void btnDatSan_Click(object sender, EventArgs e)
        {
            FDatSanBR me = new FDatSanBR(); 
            this.Hide();
            me.ShowDialog();
            this.Show();
            Close();
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            FTKKH me = new FTKKH();
            this.Hide();
            me.ShowDialog();
            this.Show();
            Close();
        }
    }
}
