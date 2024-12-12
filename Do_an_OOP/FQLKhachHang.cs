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
    public partial class FQLKhachHang : Form
    {
        CtrlKhachHang ctrl_KhachHang = new CtrlKhachHang();
        List<KHACHHANG> dsKhachHang = null;
        KHACHHANG khachhang;
        public FQLKhachHang()
        {
            InitializeComponent();
        }
        public void load_KhachHang()
        {
            var list = from kh in dsKhachHang
                       select new { kh.IDKhachHang, kh.TenKhachHang, kh.TenTaiKhoan,kh.MatKhau, kh.NgaySinh, kh.GioiTinh, kh.SDT, kh.Email, kh.DiaChi, kh.NgayTaoTK };
            dataGridViewTTKH.DataSource = list.ToList();

            dataGridViewTTKH.Columns["IDKhachHang"].HeaderText = "IDKH";
            dataGridViewTTKH.Columns["TenTaiKhoan"].HeaderText = "TenTK";

            dataGridViewTTKH.Columns["IDKhachHang"].Width = 50;
            dataGridViewTTKH.Columns["MatKhau"].Width = 60;
            dataGridViewTTKH.Columns["TenTaiKhoan"].Width = 90;
            dataGridViewTTKH.Columns["TenKhachHang"].Width = 110;
            dataGridViewTTKH.Columns["NgaySinh"].Width = 70;
            dataGridViewTTKH.Columns["GioiTinh"].Width = 55;
            dataGridViewTTKH.Columns["SDT"].Width = 75;
            dataGridViewTTKH.Columns["DiaChi"].Width = 80;
            dataGridViewTTKH.Columns["Email"].Width = 120;
            dataGridViewTTKH.Columns["NgayTaoTK"].Width = 80;

        }

        private void FQLKhachHang_Load(object sender, EventArgs e)
        {
            dsKhachHang = ctrl_KhachHang.findAll();
            load_KhachHang();

            btnXacNhanThem.Visible = false;
            btnXacNhanSua.Visible = false;
        }

        private void txtTimKiemKH_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //lấy keyword trong txt
                string keyword = txtTimKiemKH.Text;
                //gọi phương thức tìm kiếm
                var dsTimkiem = ctrl_KhachHang.search(keyword);
                //hiển thị kq
                dataGridViewTTKH.DataSource = dsTimkiem;
            }
            catch
            {

            }

        }

        private void dataDateKH_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime selectedDate = dataDateKH.Value.Date;
                var dsTimKiemDay = ctrl_KhachHang.searchDate(selectedDate);
                dataGridViewTTKH.DataSource = dsTimKiemDay;

            }
            catch { }

        }

        private void dataGridViewTTKH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridViewTTKH.CurrentRow;
            int index = row.Index;
            if (index >= 0)
            {
                khachhang = dsKhachHang[index];
                txtIDKH.Text = khachhang.IDKhachHang;
                txtTenKH.Text = khachhang.TenKhachHang;
                txtTenTK.Text = khachhang.TenTaiKhoan;
                txtMatKhau.Text = khachhang.MatKhau;
                txtSDT.Text = khachhang.SDT;
                txtDiaChi.Text = khachhang.DiaChi;
                txtEmail.Text = khachhang.Email;
                dateNgaySinh.Text = khachhang.NgaySinh + "";
                dateNgayTaoTK.Text = khachhang.NgayTaoTK+ "";

                //gán giá trị giới tính
                if (khachhang.GioiTinh == "Nữ")
                {
                    rdbtnNam.Checked = false;
                    rdbtnNu.Checked = true;
                }
                else if (khachhang.GioiTinh == "Nam")
                {
                    rdbtnNu.Checked = false;
                    rdbtnNam.Checked = true;
                }
                else
                {
                    rdbtnNu.Checked = false;
                    rdbtnNam.Checked = false;
                }
                txtIDKH.ReadOnly = true;
                txtTenKH.ReadOnly = true;
                txtTenTK.ReadOnly = true;
                txtMatKhau.ReadOnly = true;
                txtSDT.ReadOnly = true;
                txtDiaChi.ReadOnly = true;
                txtEmail.ReadOnly = true;
                dateNgaySinh.Enabled = false;
                dateNgayTaoTK.Enabled = false;
                rdbtnNam.Enabled = false;
                rdbtnNu.Enabled = false;
            }

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnXacNhanThem.Visible = true;
            btnXacNhanThem.Cursor = Cursors.Hand;
            txtIDKH.ReadOnly = false;
            txtTenKH.ReadOnly = false;
            txtTenTK.ReadOnly = false;
            txtMatKhau.ReadOnly = false;
            txtSDT.ReadOnly = false;
            txtDiaChi.ReadOnly = false;
            txtEmail.ReadOnly = false;
            dateNgaySinh.Enabled = true;
            dateNgayTaoTK.Enabled = true;
            rdbtnNam.Enabled = true;
            rdbtnNu.Enabled = true;
            if (rdbtnNam.Checked)
            {
                rdbtnNu.Enabled = false;
            }
            else
            {
                rdbtnNu.Enabled = true;
            }
            txtIDKH.Clear();
            txtTenKH.Clear();
            txtTenTK.Clear();
            txtMatKhau.Clear();
            txtSDT.Clear();
            txtDiaChi.Clear();
            txtEmail.Clear();
            dateNgaySinh.ResetText();
            dateNgayTaoTK.ResetText();

        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnXacNhanThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDKH.Text) ||
                string.IsNullOrWhiteSpace(txtTenKH.Text) ||
                string.IsNullOrWhiteSpace(txtTenTK.Text) ||
                string.IsNullOrWhiteSpace(txtMatKhau.Text) ||
                string.IsNullOrWhiteSpace(txtSDT.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string gioiTinh = "Nam";
            if (rdbtnNu.Checked)
            {
                gioiTinh = "Nữ";
            }
            //xác nhận them ttnv mới
            var TTKHMoi = new KHACHHANG();
            TTKHMoi.IDKhachHang = txtIDKH.Text;
            TTKHMoi.TenKhachHang = txtTenKH.Text;
            TTKHMoi.TenTaiKhoan = txtTenTK.Text;
            TTKHMoi.MatKhau = txtMatKhau.Text;
            TTKHMoi.GioiTinh = gioiTinh;
            if (txtSDT.Text.Length < 11)
            {
                MessageBox.Show("Số điện thoại không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            TTKHMoi.SDT = txtSDT.Text;
            TTKHMoi.Email = txtEmail.Text;
            if (txtEmail.Text.EndsWith("@gmail.com"))
            {
                MessageBox.Show("Địa chỉ Email không đúng định dạng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            TTKHMoi.DiaChi = txtDiaChi.Text;
            TTKHMoi.NgaySinh = dateNgaySinh.Value;
            TTKHMoi.NgayTaoTK = dateNgayTaoTK.Value;
            //kiemtra trùng mã nv
            var counter = dsKhachHang.Where(makh => makh.IDKhachHang == txtIDKH.Text).ToList().Count();
            if (counter > 0)
            {
                MessageBox.Show("Mã nhân viên bị trùng!");
                return;
            }
            else
            {
                dsKhachHang.Add(TTKHMoi);
                //gọi ctrl
                ctrl_KhachHang.add(TTKHMoi);
                MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK);
                //load lai dữ liệu
                dataGridViewTTKH.DataSource = null;
                load_KhachHang();
            }
            txtIDKH.Clear();
            txtTenKH.Clear();
            txtTenTK.Clear();
            txtMatKhau.Clear();
            txtSDT.Clear();
            txtDiaChi.Clear();
            txtEmail.Clear();
            dateNgaySinh.ResetText();
            dateNgayTaoTK.ResetText();
            rdbtnNam.Checked = false;
            rdbtnNu.Checked = false;

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnXacNhanSua.Visible = true;
            btnXacNhanSua.Cursor = Cursors.Hand;
            txtIDKH.ReadOnly = true;
            txtTenKH.ReadOnly = false;
            txtTenTK.ReadOnly = false;
            txtMatKhau.ReadOnly = false;
            txtSDT.ReadOnly = false;
            txtDiaChi.ReadOnly = false;
            txtEmail.ReadOnly = false;
            dateNgaySinh.Enabled = true;
            dateNgayTaoTK.Enabled = true;
            rdbtnNam.Enabled = true;
            rdbtnNu.Enabled = true;
            if (rdbtnNam.Checked)
            {
                rdbtnNu.Enabled = false;
            }
            else
            {
                rdbtnNu.Enabled = true;
            }

        }

        private void btnXacNhanSua_Click(object sender, EventArgs e)
        {
            if (khachhang != null)
            {
                if (string.IsNullOrWhiteSpace(txtIDKH.Text) ||
                   string.IsNullOrWhiteSpace(txtTenKH.Text) ||
                   string.IsNullOrWhiteSpace(txtTenTK.Text) ||
                   string.IsNullOrWhiteSpace(txtMatKhau.Text) ||
                   string.IsNullOrWhiteSpace(txtSDT.Text) ||
                   string.IsNullOrWhiteSpace(txtEmail.Text) ||
                   string.IsNullOrWhiteSpace(txtDiaChi.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
                    return;
                }
                string gioiTinh = "Nam";
                if (rdbtnNu.Checked)
                {
                    gioiTinh = "Nữ";
                }
                khachhang.IDKhachHang = txtIDKH.Text;
                khachhang.TenKhachHang = txtTenKH.Text;
                khachhang.TenTaiKhoan = txtTenTK.Text;
                khachhang.MatKhau = txtMatKhau.Text;
                khachhang.SDT = txtSDT.Text;
                if (txtSDT.Text.Length < 11)
                {
                    MessageBox.Show("Số điện thoại không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                khachhang.Email = txtEmail.Text;
                khachhang.DiaChi = txtDiaChi.Text;
                khachhang.NgaySinh = dateNgaySinh.Value;
                khachhang.NgayTaoTK = dateNgayTaoTK.Value;
                khachhang.GioiTinh = gioiTinh;
                //kiemtra trùng mã nv
                var counter = dsKhachHang.Where(manv => manv.IDKhachHang == txtIDKH.Text && manv != khachhang).ToList().Count();
                if (counter > 0)
                {
                    MessageBox.Show("Mã nhân viên bị trùng!");
                    return;
                }
                else
                {
                    ctrl_KhachHang.upDate(khachhang);
                    MessageBox.Show("Sửa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK);
                    dataGridViewTTKH.DataSource = null;
                    load_KhachHang();

                    txtIDKH.Clear();
                    txtTenKH.Clear();
                    txtTenTK.Clear();
                    txtMatKhau.Clear();
                    txtSDT.Clear();
                    txtDiaChi.Clear();
                    txtEmail.Clear();
                    dateNgaySinh.ResetText();
                    dateNgayTaoTK.ResetText();
                    rdbtnNam.Checked = false;
                    rdbtnNu.Checked = false;
                }
            }
            else
            {
                MessageBox.Show("Chưa chọn loại sách để cập nhật");
            }
        }

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
            if (txtSDT.Text.Length >= 11 && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            btnXacNhanSua.Visible = false;
            btnXacNhanThem.Visible = false;
            txtIDKH.Clear();
            txtTenKH.Clear();
            txtTenTK.Clear();
            txtMatKhau.Clear();
            txtSDT.Clear();
            txtDiaChi.Clear();
            txtEmail.Clear();
            dateNgaySinh.ResetText();
            dateNgayTaoTK.ResetText();
            rdbtnNam.Checked = false;
            rdbtnNu.Checked = false;

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

        private void btnExit_Click(object sender, EventArgs e)
        {
            FChonQuyen me = new FChonQuyen();
            this.Hide();
            me.ShowDialog();
            this.Show();

        }
    }
}
