using Do_an_OOP.Controler;
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
    public partial class LoginNv : Form
    {
        Ctrl_NhanVien ctrlNhanVien = new Ctrl_NhanVien();
        List<NHANVIEN> dsNV = new List<NHANVIEN>(); 

        public LoginNv()
        {
            InitializeComponent();
        }

        private void guna2GradientTileButton1_Click(object sender, EventArgs e)
        {
            HomeNV home = new HomeNV();
            string tk = txtTenDangNhap.Text.Trim();
            string mk = txtMatKhau.Text.Trim();
            NHANVIEN nhanVien = ctrlNhanVien.Login(tk, mk);

            if (nhanVien != null)
            {
                MessageBox.Show("Đăng nhập thành công!");
                FChonQuyen fcq = new FChonQuyen();
                this.Hide();
                home.Show();
            }
            else
            {
                MessageBox.Show("Đăng nhập không thành công!");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            FChonQuyen fcq = new FChonQuyen();
            this.Hide();
            fcq.Show();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
