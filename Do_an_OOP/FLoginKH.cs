using Do_an_OOP.Controller;
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
    public partial class FLoginKH : Form
    {
        CtrlKhachHang ctrl_KhachHang = new CtrlKhachHang();

        public FLoginKH()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tk = txtTenDangNhap.Text.Trim();
            string mk = txtMatKhau.Text.Trim();
            KHACHHANG kh = ctrl_KhachHang.login(tk, mk);
            if (kh != null)
            {
                MessageBox.Show("Đăng nhập thành công!");
                MyGereral.KhachHang = kh;
                FHomeKH me = new FHomeKH();
                this.Hide();
                me.ShowDialog(); 
                this.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Đăng nhập không thành công!");
            }

        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
