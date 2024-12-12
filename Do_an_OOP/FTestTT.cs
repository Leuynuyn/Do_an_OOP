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
    public partial class FTestTT : Form
    {
        QUANTRIVIEN qtv = null;
        public FTestTT()
        {
            InitializeComponent();
        }

        private void FTestTT_Load(object sender, EventArgs e)
        {
            qtv = MyGereral.QuanTriVien;
            lblhienthiten.Text = "Chào " + qtv.TenAdmin;
            txtTenAdmin.Text = qtv.TenAdmin;
            txtTenTaiKhoan.Text = qtv.TenTaiKhoan;
            txtGioiTinh.Text = qtv.GioiTinh;
            txtEmail.Text = qtv.Email;
            txtNgaySinh.Text = qtv.NgaySinh+"";
            txtDiaChi.Text = qtv.DiaChi;
            txtSDT.Text = qtv.SDT;
            txtMatKhau.Text = qtv.MatKhau;
        }

        private void btnTTCaNhan_Click(object sender, EventArgs e)
        {

        }
    }
}
