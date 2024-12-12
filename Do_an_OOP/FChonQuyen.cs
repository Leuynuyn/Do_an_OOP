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
    public partial class FChonQuyen : Form
    {
        public FChonQuyen()
        {
            InitializeComponent();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            LoginNv FLogin = new LoginNv();
            this.Hide();
            FLogin.Show();

        }

        private void FChonQuyen_Load(object sender, EventArgs e)
        {

        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            FLoginKH me = new FLoginKH();
            this.Hide();
            me.Show();
        }

        private void btnQTV_Click(object sender, EventArgs e)
        {
            LoginQTV FLogin = new LoginQTV();
            this.Hide();
            FLogin.Show();
        }
    }
}
