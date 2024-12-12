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
    public partial class FQLNhanVien : Form
    {
        Ctrl_NhanVien ctrl_NhanVien =  new Ctrl_NhanVien();
        List<NHANVIEN> dsNhanVien = null;
        NHANVIEN nhanvien;
        public FQLNhanVien()
        {
            InitializeComponent();
        }

        public void load_NhanVien()
        {
            var list = from n in dsNhanVien
                       select new {n.IDNhanVien, n.TenNhanVien, n.TenTaiKhoan, n.MatKhau, n.SDT, n.Email, n.DiaChi, n.GioiTinh, n.NgaySinh, n.VaiTro, n.NgayVaoLam };
            dataGridViewTTNV.DataSource = list.ToList();

            //Đổi tên cột
            dataGridViewTTNV.Columns["IDNhanVien"].HeaderText = "IDNV";
            dataGridViewTTNV.Columns["TenTaiKhoan"].HeaderText = "TenTK";
            //điều chỉnh kích thước các cột
            dataGridViewTTNV.Columns["IDNhanVien"].Width = 45;
            dataGridViewTTNV.Columns["TenTaiKhoan"].Width = 60;
            dataGridViewTTNV.Columns["MatKhau"].Width = 60;
            dataGridViewTTNV.Columns["NgaySinh"].Width = 70;
            dataGridViewTTNV.Columns["GioiTinh"].Width = 50;
            dataGridViewTTNV.Columns["SDT"].Width = 70;
            dataGridViewTTNV.Columns["DiaChi"].Width = 70;
            dataGridViewTTNV.Columns["Email"].Width = 80;
            dataGridViewTTNV.Columns["NgayVaoLam"].Width = 80;

        }

        private void FQLNhanVien_Load(object sender, EventArgs e)
        {
            //Load thông tin nhân viên khi form dc khởi chạy
            try
            {
                //Tải dữ liệu từ Ctrl và hiển thị lên datagridview
                dsNhanVien = ctrl_NhanVien.findAll();
                load_NhanVien();

                //nút xác nhận ẩn
                btnXacNhanThem.Visible = false;
                btnXacNhanSua.Visible = false;
                btnXacNhanXoa.Visible = false;  
            }
            catch
            {

            }
        }

        private void txtTimKiemNV_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //lấy keyword trong txt
                string keyword = txtTimKiemNV.Text;
                //gọi phương thức tìm kiếm
                var dsTimkiem = ctrl_NhanVien.search(keyword);
                //hiển thị kq
                dataGridViewTTNV.DataSource = dsTimkiem;
            }catch
            {

            }
        }

        private void dataDateNV_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime selectedDate = dataDateNV.Value.Date;
                var dsTimKiemDay = ctrl_NhanVien.searchDateWork(selectedDate);
                dataGridViewTTNV.DataSource = dsTimKiemDay;

            }catch { }
        }


        private void dataGridViewTTNV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridViewTTNV.CurrentRow;
            int index = row.Index;
            if (index >= 0)
            {
                nhanvien = dsNhanVien[index];
                txtIDNV.Text = nhanvien.IDNhanVien;
                txtTenNV.Text = nhanvien.TenNhanVien;
                txtTenTK.Text = nhanvien.TenTaiKhoan;
                txtMatKhau.Text = nhanvien.MatKhau;
                txtSDT.Text = nhanvien.SDT;
                comboxVaiTro.Text = nhanvien.VaiTro;
                txtDiaChi.Text = nhanvien.DiaChi;
                txtEmail.Text = nhanvien.Email;
                dateNgaySinh.Text = nhanvien.NgaySinh + "";
                dateNgayVaoLam.Text = nhanvien.NgayVaoLam + "";

                //gán giá trị giới tính
                if(nhanvien.GioiTinh == "Nữ")
                {
                    rdbtnNam.Checked = false;
                    rdbtnNu.Checked = true;
                }
                else if (nhanvien.GioiTinh == "Nam")
                {
                    rdbtnNu.Checked = false;
                    rdbtnNam.Checked = true;
                }
                else
                {
                    rdbtnNu.Checked = false;
                    rdbtnNam.Checked = false;
                }
                txtIDNV.ReadOnly = true;
                txtTenNV.ReadOnly = true;
                txtTenTK.ReadOnly = true;  
                txtMatKhau.ReadOnly = true;
                txtSDT.ReadOnly = true;
                comboxVaiTro.Enabled = false;
                txtDiaChi.ReadOnly = true;
                txtEmail.ReadOnly = true;
                dateNgaySinh.Enabled = false;
                dateNgayVaoLam.Enabled = false;
                rdbtnNam.Enabled = false;
                rdbtnNu.Enabled = false;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnXacNhanThem.Visible = true;
            btnXacNhanThem.Cursor = Cursors.Hand;
            txtIDNV.ReadOnly = false;
            txtTenNV.ReadOnly = false;
            txtTenTK.ReadOnly = false;
            txtMatKhau.ReadOnly = false;
            txtSDT.ReadOnly = false;
            comboxVaiTro.Enabled = true;
            txtDiaChi.ReadOnly = false;
            txtEmail.ReadOnly = false;
            dateNgaySinh.Enabled = true;
            dateNgayVaoLam.Enabled = true;
            rdbtnNam.Enabled = true;
            rdbtnNu.Enabled = true;
            if(rdbtnNam.Checked)
            {
                rdbtnNu.Enabled = false ;
            }
            else
            {
                rdbtnNu.Enabled = true ;
            }
            txtIDNV.Clear();
            txtTenNV.Clear();
            txtTenTK.Clear();
            txtMatKhau.Clear();
            txtSDT.Clear();
            comboxVaiTro.ResetText();
            txtDiaChi.Clear();
            txtEmail.Clear();
            dateNgaySinh.ResetText();
            dateNgayVaoLam.ResetText();

        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnXacNhanThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDNV.Text) ||
                string.IsNullOrWhiteSpace(txtTenNV.Text) ||
                string.IsNullOrWhiteSpace(txtTenTK.Text) ||
                string.IsNullOrWhiteSpace(txtMatKhau.Text) ||
                string.IsNullOrWhiteSpace(txtSDT.Text) ||
                string.IsNullOrWhiteSpace(comboxVaiTro.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
             string gioiTinh = "Nam";
            if(rdbtnNu.Checked )
            {
                gioiTinh = "Nữ";
            }
            //xác nhận them ttnv mới
            var TTNVMoi = new NHANVIEN();
            TTNVMoi.IDNhanVien = txtIDNV.Text;
            TTNVMoi.TenNhanVien = txtTenNV.Text;
            TTNVMoi.TenTaiKhoan = txtTenTK.Text;
            TTNVMoi.MatKhau = txtMatKhau.Text;
            TTNVMoi.GioiTinh = gioiTinh;
            if(txtSDT.Text.Length < 11 )
            {
                MessageBox.Show("Số điện thoại không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            TTNVMoi.SDT = txtSDT.Text;
            TTNVMoi.VaiTro = comboxVaiTro.Text;
            TTNVMoi.Email = txtEmail.Text;
            if(txtEmail.Text.EndsWith("@gmail.com"))
            {
                MessageBox.Show("Địa chỉ Email không đúng định dạng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            TTNVMoi.DiaChi = txtDiaChi.Text;
            TTNVMoi.NgaySinh = dateNgaySinh.Value;
            TTNVMoi.NgayVaoLam = dateNgayVaoLam.Value;
            //kiemtra trùng mã nv
            var counter = dsNhanVien.Where(manv => manv.IDNhanVien == txtIDNV.Text).ToList().Count();
            if (counter > 0)
            {
                MessageBox.Show("Mã nhân viên bị trùng!");
                return;
            }
            else
            {
                dsNhanVien.Add(TTNVMoi);
                //gọi ctrl
                ctrl_NhanVien.add(TTNVMoi);
                MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK);
                //load lai dữ liệu
                dataGridViewTTNV.DataSource = null;
                load_NhanVien();
            }
            txtIDNV.Clear();
            txtTenNV.Clear();
            txtTenTK.Clear();
            txtMatKhau.Clear();
            txtSDT.Clear();
            comboxVaiTro.ResetText();
            txtDiaChi.Clear();
            txtEmail.Clear();
            dateNgaySinh.ResetText();
            dateNgayVaoLam.ResetText();
            rdbtnNam.Checked = false;
            rdbtnNu.Checked = false;

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnXacNhanSua.Visible = true;
            btnXacNhanSua.Cursor = Cursors.Hand;
            txtIDNV.ReadOnly = true;
            txtTenNV.ReadOnly = false;
            txtTenTK.ReadOnly = false;
            txtMatKhau.ReadOnly = false;
            txtSDT.ReadOnly = false;
            comboxVaiTro.Enabled = true;
            txtDiaChi.ReadOnly = false;
            txtEmail.ReadOnly = false;
            dateNgaySinh.Enabled = true;
            dateNgayVaoLam.Enabled = true;
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
            if(nhanvien != null)
            {
                if(string.IsNullOrWhiteSpace(txtIDNV.Text) ||
                   string.IsNullOrWhiteSpace(txtTenNV.Text) ||
                   string.IsNullOrWhiteSpace(txtTenTK.Text) ||
                   string.IsNullOrWhiteSpace(txtMatKhau.Text) ||
                   string.IsNullOrWhiteSpace(txtSDT.Text) ||
                   string.IsNullOrWhiteSpace(comboxVaiTro.Text) ||
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
                nhanvien.IDNhanVien = txtIDNV.Text;
                nhanvien.TenNhanVien = txtTenNV.Text;
                nhanvien.TenTaiKhoan = txtTenTK.Text;
                nhanvien.MatKhau = txtMatKhau.Text;
                nhanvien.SDT = txtSDT.Text;
                if (txtSDT.Text.Length < 11)
                {
                    MessageBox.Show("Số điện thoại không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                nhanvien.Email = txtEmail.Text;
                nhanvien.DiaChi = txtDiaChi.Text;
                nhanvien.VaiTro = comboxVaiTro.Text;
                nhanvien.NgaySinh = dateNgaySinh.Value;
                nhanvien.NgayVaoLam = dateNgayVaoLam.Value;
                nhanvien.GioiTinh = gioiTinh;
                //kiemtra trùng mã nv
                var counter = dsNhanVien.Where(manv => manv.IDNhanVien == txtIDNV.Text && manv != nhanvien).ToList().Count();
                if (counter > 0)
                {
                    MessageBox.Show("Mã nhân viên bị trùng!");
                    return;
                }
                else
                {
                    ctrl_NhanVien.upDate(nhanvien);
                    MessageBox.Show("Sửa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK);
                    dataGridViewTTNV.DataSource = null;
                    load_NhanVien();

                    txtIDNV.Clear();
                    txtTenNV.Clear();
                    txtTenTK.Clear();
                    txtMatKhau.Clear();
                    txtSDT.Clear();
                    comboxVaiTro.ResetText();
                    txtDiaChi.Clear();
                    txtEmail.Clear();
                    dateNgaySinh.ResetText();
                    dateNgayVaoLam.ResetText();
                    rdbtnNam.Checked = false;
                    rdbtnNu.Checked = false;
                }
            }
            else
            {
                MessageBox.Show("Chưa chọn loại sách để cập nhật");
            }

        }

        private void btnXacNhanXoa_Click(object sender, EventArgs e)
        {
            if(nhanvien != null)
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    dsNhanVien.Remove(nhanvien);
                    ctrl_NhanVien.remove(nhanvien);
                    MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK);
                    dataGridViewTTNV.DataSource = null;
                    load_NhanVien();
                    nhanvien = null;
                    txtIDNV.Clear();
                    txtTenNV.Clear();
                    txtTenTK.Clear();
                    txtMatKhau.Clear();
                    txtSDT.Clear();
                    comboxVaiTro.ResetText();
                    txtDiaChi.Clear();
                    txtEmail.Clear();
                    dateNgaySinh.ResetText();
                    dateNgayVaoLam.ResetText();
                }
                else
                {
                    MessageBox.Show("Xóa nhân viên không thành công!", "Thông báo", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            btnXacNhanXoa.Visible = true;
            btnXacNhanXoa.Cursor = Cursors.Hand;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            btnXacNhanSua.Visible = false;
            btnXacNhanThem.Visible = false;
            btnXacNhanXoa.Visible = false; 
            txtIDNV.Clear();
            txtTenNV.Clear();
            txtTenTK.Clear();
            txtMatKhau.Clear();
            txtSDT.Clear();
            comboxVaiTro.SelectedIndex = -1;
            txtDiaChi.Clear();
            txtEmail.Clear();
            dateNgaySinh.ResetText();
            dateNgayVaoLam.ResetText();
            rdbtnNam.Checked = false;
            rdbtnNu.Checked = false;   
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

        private void btnQLSan_Click(object sender, EventArgs e)
        {
            FQLSan menu = new FQLSan();
            this.Hide();
            menu.ShowDialog();
            this.Show();
            Close();
        }

        private void btnHomeAd_Click(object sender, EventArgs e)
        {
            HomeAdmin me = new HomeAdmin();
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

        private void btnQLKH_Click(object sender, EventArgs e)
        {
            FQLKhachHang me = new FQLKhachHang();
            this.Hide();
            me.ShowDialog();
            this.Show();
            Close();
        }
    }
}
