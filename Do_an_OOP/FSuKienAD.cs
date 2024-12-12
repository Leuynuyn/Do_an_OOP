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
    public partial class FSuKienAD : Form
    {
        Ctrl_SuKien ctrl_SuKien = new Ctrl_SuKien();
        List<SUKIENKHUYENMAI> dsSuKien = null;
        SUKIENKHUYENMAI sukien;
        public FSuKienAD()
        {
            InitializeComponent();
        }

        private void ClosePage_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void load_SuKien()
        {
            DateTime currentDate = DateTime.Now;
            var list = from sk in dsSuKien
                       select new { sk.IDSuKien, sk.TenSuKien, sk.MoTa, sk.NgayBatDauSK, sk.NgayKetThucSK, sk.MaCodeKM,
                           TinhTrang = (currentDate >= sk.NgayBatDauSK && currentDate <= sk.NgayKetThucSK) ? "Đang áp dụng" : "Không áp dụng"
                       };
            dataGridViewSuKien.DataSource = list.ToList();

            dataGridViewSuKien.Columns["IDSuKien"].HeaderText = "Mã sự kiện";
            dataGridViewSuKien.Columns["TenSuKien"].HeaderText = "Tên sự kiện";
            dataGridViewSuKien.Columns["MoTa"].HeaderText = "Mô tả";
            dataGridViewSuKien.Columns["NgayBatDauSK"].HeaderText = "Ngày bắt đầu";
            dataGridViewSuKien.Columns["NgayKetThucSK"].HeaderText = "Ngày kết thúc";

            dataGridViewSuKien.Columns["IDSuKien"].Width = 90;
            dataGridViewSuKien.Columns["TenSuKien"].Width = 130;
            dataGridViewSuKien.Columns["MoTa"].Width = 180;
            dataGridViewSuKien.Columns["MaCodeKM"].Width = 90;

        }
        private void lblQLSuKien_Click(object sender, EventArgs e)
        {

        }

        private void FSuKien_Load(object sender, EventArgs e)
        {
            dsSuKien = ctrl_SuKien.findAll();
            load_SuKien();

            btnXacNhanThem.Visible = false;
        }

        private void dataGridViewSuKien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridViewSuKien.CurrentRow;
            int index = row.Index;
            if (index >= 0)
            {
                sukien = dsSuKien[index];
                txtIDSuKien.Text = sukien.IDSuKien;
                txtTenSK.Text = sukien.TenSuKien;
                txtMota.Text = sukien.MoTa;
                txtMaKM.Text = sukien.MaCodeKM;
                datetimeNgayBatDau.Text = sukien.NgayBatDauSK + "";
                datetimeNgayKetThuc.Text = sukien.NgayKetThucSK + "";

                txtIDSuKien.ReadOnly = true;
                txtTenSK.ReadOnly = true;
                txtMota.ReadOnly = true;
                txtMaKM.ReadOnly = true;
                datetimeNgayBatDau.Enabled = false;
                datetimeNgayKetThuc.Enabled = false;

            }

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnXacNhanThem.Visible = true;

            txtIDSuKien.ReadOnly = false;
            txtTenSK.ReadOnly = false;
            txtMota.ReadOnly = false;
            txtMaKM.ReadOnly = false;
            datetimeNgayBatDau.Enabled = true;
            datetimeNgayKetThuc.Enabled = true;

            txtIDSuKien.Clear();
            txtTenSK.Clear();
            txtMota.Clear();
            txtMaKM.Clear();
            datetimeNgayBatDau.ResetText();
            datetimeNgayKetThuc.ResetText();

        }

        private void btnXacNhanThem_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtIDSuKien.Text)||
                string.IsNullOrWhiteSpace(txtTenSK.Text) ||
                string.IsNullOrWhiteSpace(txtMota.Text) ||
                string.IsNullOrWhiteSpace(txtMaKM.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var TTSK = new SUKIENKHUYENMAI();
            TTSK.IDSuKien = txtIDSuKien.Text;
            TTSK.TenSuKien = txtTenSK.Text;
            TTSK.MoTa = txtMota.Text;
            TTSK.MaCodeKM = txtMaKM.Text;
            TTSK.NgayBatDauSK = datetimeNgayBatDau.Value;
            TTSK.NgayKetThucSK = datetimeNgayKetThuc.Value;

            var counter = dsSuKien.Where(sk => sk.IDSuKien == txtIDSuKien.Text).ToList().Count();
            if (counter > 0)
            {
                MessageBox.Show("Mã sự kiện bị trùng!");
                return;
            }
            else
            {
                dsSuKien.Add(TTSK);
                ctrl_SuKien.add(TTSK);
                MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK);
                load_SuKien();

                txtIDSuKien.Clear();
                txtTenSK.Clear();
                txtMota.Clear();
                txtMaKM.Clear();
                txtIDSuKien.ReadOnly = true;
                txtTenSK.ReadOnly = true;
                txtMota.ReadOnly = true;
                txtMaKM.ReadOnly = true;
                datetimeNgayBatDau.Enabled = false;
                datetimeNgayKetThuc.Enabled = false;

            }

        }

        private void btnHomeAd_Click(object sender, EventArgs e)
        {
            HomeAdmin me = new HomeAdmin();
            this.Hide();
            me.ShowDialog();
            this.Show();
            Close();

        }

        private void btnQLKH_Click(object sender, EventArgs e)
        {
            FQLKhachHang me = new FQLKhachHang();
            this.Hide();
            me.ShowDialog();
            this.Show();
            Close();
        }

        private void btnQLNV_Click(object sender, EventArgs e)
        {
            FQLNhanVien me = new FQLNhanVien();
            this.Hide();
            me.ShowDialog();
            this.Show();
            Close();

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
            FTinhLuong me = new FTinhLuong();
            this.Hide();
            me.ShowDialog();
            this.Show();
            Close();

        }

        private void btnQLFood_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            FChonQuyen me = new FChonQuyen();
            this.Hide();
            me.ShowDialog();
            this.Show();
        }
    }
}
