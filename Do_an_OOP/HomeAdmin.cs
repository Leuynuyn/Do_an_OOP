using Do_an_OOP.Controler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Do_an_OOP
{
    public partial class HomeAdmin : Form
    {
        Ctrl_San ctrl_San = new Ctrl_San();
        List<SAN> dsSan = null;
        Ctrl_QTV dsQTV = null;
        QUANTRIVIEN qtv = null;
        public HomeAdmin()
        {
            InitializeComponent();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnQLNV_Click(object sender, EventArgs e)
        {
            FQLNhanVien menu = new FQLNhanVien();
            this.Hide();
            menu.ShowDialog();
            this.Show();
            Close();
        }

        private void HomeAdmin_Load(object sender, EventArgs e)
        {
            dsSan = ctrl_San.findAll();
            load_SanBongDa();

            qtv = MyGereral.QuanTriVien;
            if (qtv == null)
            {
                lblhienthiten.Text = "CHƯA ĐĂNG NHẬP";
            }
            else
            {
                lblhienthiten.Text = "Chào " + qtv.TenAdmin;
            }
            
        }
        public void load_SanBongDa()
        {
            var dsSanBongDa = ctrl_San.findByLoaiSan("LS002");
            //dataGridView1.DataSource = dsSanBongDa.Select(sbd => new { sbd.IDSan, sbd.TenSan, sbd.DonGia, sbd.MoTa, sbd.TinhTrang }).ToList();
        }

        private void btnQLSan_Click(object sender, EventArgs e)
        {
            FQLSan menu = new FQLSan();
            this.Hide();
            menu.ShowDialog();
            this.Show();
            Close();
        }

        private void btnQLSuKien_Click(object sender, EventArgs e)
        {
            FSuKienAD en = new FSuKienAD();
            this.Hide();
            en.ShowDialog();
            this.Show();
            Close();

        }

        private void btnLuong_Click(object sender, EventArgs e)
        {
        }
        private void btnHomeAd_Click(object sender, EventArgs e)
        {
            HomeAdmin me = new HomeAdmin();
            this.Hide();
            me.ShowDialog();
            this.Show();
            Close();
        }

        private void btnLuong_Click_1(object sender, EventArgs e)
        {
            FTinhLuong me = new FTinhLuong();
            this.Hide();
            me.ShowDialog();
            this.Show();
            Close();

        }

        private void btnHomeAd_Click_1(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            FChonQuyen me = new FChonQuyen();
            this.Hide();
            me.ShowDialog();
            this.Show();
        }


        private void hienthiten_Click(object sender, EventArgs e)
        {

        }

        private void btnQLKH_Click(object sender, EventArgs e)
        {
            FQLKhachHang me = new FQLKhachHang();
            this.Hide();
            me.ShowDialog();
            this.Show();
            Close();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            FTestTT me = new FTestTT();
            this.Hide();
            me.ShowDialog();
            this.Show();
            Close();
        }
    }
}
