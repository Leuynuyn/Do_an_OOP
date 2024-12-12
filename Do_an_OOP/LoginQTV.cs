using Do_an_OOP.Controler;
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
    public partial class LoginQTV : Form
    {
        Ctrl_QTV ctrl_QTV = new Ctrl_QTV();
        List<QUANTRIVIEN> dsqtv = new List<QUANTRIVIEN>();
        public LoginQTV()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tk = txtTenDangNhap.Text.Trim();
            string mk = txtMatKhau.Text.Trim();
            QUANTRIVIEN qtv = ctrl_QTV.Login(tk, mk);

            if (qtv != null)
            {
                MessageBox.Show("Đăng nhập thành công!");
                MyGereral.QuanTriVien = qtv;
                HomeAdmin home = new HomeAdmin();
                this.Hide();
                home.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Đăng nhập không thành công!");
            }

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtTenDangNhap_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
