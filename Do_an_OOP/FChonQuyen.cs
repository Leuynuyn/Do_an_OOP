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
            Login FLogin = new Login();
            this.Hide();
            FLogin.Show();
            
        }
    }
}
