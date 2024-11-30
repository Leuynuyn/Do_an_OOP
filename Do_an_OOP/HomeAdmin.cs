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
    public partial class HomeAdmin : Form
    {
        public HomeAdmin()
        {
            InitializeComponent();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnQLNV_Click(object sender, EventArgs e)
        {
            FQLNhanVien menu = new FQLNhanVien();
            this.Hide();
            menu.ShowDialog();
            this.Show();
        }
    }
}
