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

namespace Do_an_OOP.View
{
    public partial class FHDDatSan : Form
    {
        List<CTHDDATSAN> dscthddatsan = new List<CTHDDATSAN>();
        Ctrl_San ctrlDatSan = new Ctrl_San();
        Ctrl_SuKien ctrlSK = new Ctrl_SuKien();
        CTHDDATSAN cthdds = new CTHDDATSAN();
        HDDATSAN hdds = new HDDATSAN();
        public FHDDatSan()
        {
            InitializeComponent();
        }

        private void FHDDatSan_Load(object sender, EventArgs e)
        {

        }

        private void btnHoanTatDatSan_Click(object sender, EventArgs e)
        {
            FHomeKH me = new FHomeKH();
            this.Hide();
            me.ShowDialog();
            this.Show();
            Close();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
