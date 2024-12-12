using Do_an_OOP.Controler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Do_an_OOP
{
    public partial class FQLSan : Form
    {
        Ctrl_LoaiSan ctrl_LoaiSan = new Ctrl_LoaiSan();
        List<LOAISAN> dsLoaiSan = null;
        LOAISAN loaisan;
        Ctrl_CTHDSan ctrl_cthdSan = new Ctrl_CTHDSan();
        List<CTHDDATSAN> dsCTHDSan = null;

        Ctrl_San ctrl_San = new Ctrl_San();
        List<SAN> dsSan = null;
        SAN san;
        public FQLSan()
        {
            InitializeComponent();
        }

        private void ClosePage_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblQLSAN_Click(object sender, EventArgs e)
        {

        }

        public void load_LoaiSan()
        {
            var list = from ls in dsLoaiSan
                       select new { ls.IDLoaiSan, ls.TenLoaiSan, ls.MoTa,
                           TongSanHoatDong = dsSan.Count(s => s.IDLoaiSan == ls.IDLoaiSan && s.TrangThai == "Hoạt động"),
                           TongSanNgung = dsSan.Count(s => s.IDLoaiSan == ls.IDLoaiSan && s.TrangThai == "Ngừng"),
                           TongSoSan = dsSan.Count(s => s.IDLoaiSan == ls.IDLoaiSan) 
                       };
            dataGridViewLoaiSan.DataSource = list.ToList();

            dataGridViewLoaiSan.Columns["IDLoaiSan"].HeaderText = "ID";
            dataGridViewLoaiSan.Columns["TenLoaiSan"].HeaderText = "Tên loại sân";
            dataGridViewLoaiSan.Columns["MoTa"].HeaderText = "Mô tả";
            dataGridViewLoaiSan.Columns["TongSoSan"].HeaderText = "Tổng sân";
            dataGridViewLoaiSan.Columns["TongSanHoatDong"].HeaderText = "Hoạt động";
            dataGridViewLoaiSan.Columns["TongSanNgung"].HeaderText = "Ngừng";

            dataGridViewLoaiSan.Columns["IDLoaiSan"].Width = 55;
            dataGridViewLoaiSan.Columns["TenLoaiSan"].Width = 120;
            dataGridViewLoaiSan.Columns["MoTa"].Width = 330;
            dataGridViewLoaiSan.Columns["TongSanHoatDong"].Width = 120;
            dataGridViewLoaiSan.Columns["TongSanNgung"].Width = 80;
            dataGridViewLoaiSan.Columns["TongSoSan"].Width = 95;


        }

        private void FQLSan_Load(object sender, EventArgs e)
        {
            dsLoaiSan = ctrl_LoaiSan.findAll();
            dsSan = ctrl_San.findAll();
            dsCTHDSan = ctrl_San.findCTHDSan();

            load_LoaiSan();
            load_SanTheThao();

            load_ComboBoxLoaiSan();

            btnXacNhanThem.Visible = false;
            btnXacNhanSuaLoaiSan.Visible = false;
            btnXacNhanXoaLoaiSan.Visible = false;

            btnXacNhanThemSan.Visible = false ;
            btXacNhanSuaSan.Visible = false ;

            txtIDLoaiSan.ReadOnly = true;
            txtTenLoaiSan.ReadOnly = true;
            txtMoTaLoaiSan.ReadOnly = true;
            
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //lấy keyword trong txt
                string keyword = txtTimKiem.Text;
                //gọi phương thức tìm kiếm
                var dsTimkiem = ctrl_LoaiSan.search(keyword);

                var list = from ls in dsTimkiem
                           select new{ls.IDLoaiSan,ls.TenLoaiSan,ls.MoTa,
                               TongSanHoatDong = dsSan.Count(s => s.IDLoaiSan == ls.IDLoaiSan && s.TrangThai == "Hoạt động"),
                               TongSanNgung = dsSan.Count(s => s.IDLoaiSan == ls.IDLoaiSan && s.TrangThai == "Ngừng"),
                               TongSoSan = dsSan.Count(s => s.IDLoaiSan == ls.IDLoaiSan)};
                //hiển thị kq
                dataGridViewLoaiSan.DataSource = list.ToList();

                dataGridViewLoaiSan.Columns["IDLoaiSan"].HeaderText = "ID";
                dataGridViewLoaiSan.Columns["TenLoaiSan"].HeaderText = "Tên loại sân";
                dataGridViewLoaiSan.Columns["MoTa"].HeaderText = "Mô tả";
                dataGridViewLoaiSan.Columns["TongSoSan"].HeaderText = "Tổng sân";
                dataGridViewLoaiSan.Columns["TongSanHoatDong"].HeaderText = "Hoạt động";
                dataGridViewLoaiSan.Columns["TongSanNgung"].HeaderText = "Ngừng";


                dataGridViewLoaiSan.Columns["IDLoaiSan"].Width = 55;
                dataGridViewLoaiSan.Columns["TenLoaiSan"].Width = 120;
                dataGridViewLoaiSan.Columns["MoTa"].Width = 330;
                dataGridViewLoaiSan.Columns["TongSanHoatDong"].Width = 120;
                dataGridViewLoaiSan.Columns["TongSanNgung"].Width = 80;
                dataGridViewLoaiSan.Columns["TongSoSan"].Width = 95;

            }
            catch
            {

            }

        }

        private void dataGridViewLoaiSan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridViewLoaiSan.CurrentRow;
            int index = row.Index;
            if(index >= 0)
            {
                loaisan = dsLoaiSan[index];
                txtIDLoaiSan.Text = loaisan.IDLoaiSan;
                txtTenLoaiSan.Text = loaisan.TenLoaiSan;
                txtMoTaLoaiSan.Text = loaisan.MoTa;
            }

            txtIDLoaiSan.ReadOnly = true;
            txtTenLoaiSan.ReadOnly = true;
            txtMoTaLoaiSan.ReadOnly = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnXacNhanThem.Visible = true;

            txtIDLoaiSan.ReadOnly = false;
            txtTenLoaiSan.ReadOnly = false;
            txtMoTaLoaiSan.ReadOnly = false;

            txtIDLoaiSan.Clear();
            txtTenLoaiSan.Clear();
            txtMoTaLoaiSan.Clear();

        }

        private void btnXacNhanThem_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtIDLoaiSan.Text) ||
               string.IsNullOrEmpty(txtTenLoaiSan.Text) ||
               string.IsNullOrEmpty(txtMoTaLoaiSan.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var TTLoaiSannew = new LOAISAN();
            TTLoaiSannew.IDLoaiSan = txtIDLoaiSan.Text;
            TTLoaiSannew.TenLoaiSan = txtTenLoaiSan.Text;
            TTLoaiSannew.MoTa = txtMoTaLoaiSan.Text;

            var counter = dsLoaiSan.Where(maloaisan => maloaisan.IDLoaiSan == txtIDLoaiSan.Text).ToList().Count();
            if(counter > 0)
            {
                MessageBox.Show("Mã loại sân bị trùng!!!!");
                return;
            }
            else
            {
                dsLoaiSan.Add(TTLoaiSannew);
                ctrl_LoaiSan.add(TTLoaiSannew);
                cbLoaiSan.Items.Add(TTLoaiSannew.TenLoaiSan);
                MessageBox.Show("Thêm loại sân thành công!", "Thông báo", MessageBoxButtons.OK);
                load_LoaiSan();
            }
            txtIDLoaiSan.Clear();
            txtTenLoaiSan.Clear();
            txtMoTaLoaiSan.Clear();

            btnXacNhanThem.Visible = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnXacNhanSuaLoaiSan.Visible = true;

            txtIDLoaiSan.ReadOnly = false;
            txtTenLoaiSan.ReadOnly = false;
            txtMoTaLoaiSan.ReadOnly = false;

        }

        private void btnXacNhanSuaLoaiSan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIDLoaiSan.Text) ||
                string.IsNullOrEmpty(txtTenLoaiSan.Text) ||
                string.IsNullOrEmpty(txtMoTaLoaiSan.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
                return;
            }
            loaisan.IDLoaiSan = txtIDLoaiSan.Text;
            loaisan.TenLoaiSan = txtTenLoaiSan.Text;
            loaisan.MoTa = txtMoTaLoaiSan.Text;
            var counter = dsLoaiSan.Where(maloaisan => maloaisan.IDLoaiSan == txtIDLoaiSan.Text && maloaisan != loaisan).ToList().Count();
            if(counter > 0)
            {
                MessageBox.Show("Mã loại bị trùng!");
                return;
            }
            else
            {
                ctrl_LoaiSan.update(loaisan);
                MessageBox.Show("Sửa loại sân thành công!", "Thông báo", MessageBoxButtons.OK);
                load_LoaiSan();

            }
            txtIDLoaiSan.Clear();
            txtTenLoaiSan.Clear();
            txtMoTaLoaiSan.Clear();

            btnXacNhanSuaLoaiSan.Visible = false;

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            btnXacNhanXoaLoaiSan.Visible = true;
        }

        private void btnXacNhanXoaLoaiSan_Click(object sender, EventArgs e)
        {
            if (loaisan != null)
            {
                // Đếm số sân ngừng
                int soSanNgung = dsSan.Count(s => s.IDLoaiSan == loaisan.IDLoaiSan && s.TrangThai  == "Ngừng");
                int tongSoSan = dsSan.Count(s => s.IDLoaiSan == loaisan.IDLoaiSan);
                if (soSanNgung == 0 && tongSoSan > 0)
                {
                    MessageBox.Show($"Không thể xóa loại sân \"{loaisan.TenLoaiSan}\". Tất cả sân trong loại sân này phải tạm ngừng hoạt động.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (tongSoSan == 0)
                {
                    var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa loại sân \"{loaisan.TenLoaiSan}\" không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        dsLoaiSan.Remove(loaisan);
                        ctrl_LoaiSan.remove(loaisan);
                        MessageBox.Show("Xóa loại sân thành công!", "Thông báo", MessageBoxButtons.OK);
                        load_LoaiSan();
                        loaisan = null;
                        txtIDLoaiSan.Clear();
                        txtTenLoaiSan.Clear();
                        txtMoTaLoaiSan.Clear();
                        btnXacNhanXoaLoaiSan.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show($"Không thể xóa loại sân \"{loaisan.TenLoaiSan}\" vì có sân đang hoạt động!","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn loại sân cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            btnXacNhanThem.Visible = false;
            btnXacNhanSuaLoaiSan.Visible=false;
            btnXacNhanXoaLoaiSan.Visible=false ;

            txtIDLoaiSan.Clear();
            txtTenLoaiSan.Clear();
            txtMoTaLoaiSan.Clear();

        }

        //Hàm load dữ liệu vào combobox loaisan
        private void load_ComboBoxLoaiSan()
        {
            cbLoaiSan.Items.Clear();

            foreach (var loaiSan in dsLoaiSan)
            {
                cbLoaiSan.Items.Add(loaiSan.TenLoaiSan);
                cbChonLoaiSan.Items.Add(loaiSan.TenLoaiSan);
            }
        }

        public void load_SanTheThao()
        {
            dataGridViewSan.ClearSelection();
            // Kiểm tra dsSan và dsCTHDSan không null
            if (dsSan == null || dsCTHDSan == null)
            {
                MessageBox.Show("Danh sách sân hoặc chi tiết hóa đơn đặt sân chưa được nạp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DateTime ngayKiemTra = datetimePickerNgayKiemTra.Value.Date;
            var list = from n in dsSan
                       let cthd = dsCTHDSan.FirstOrDefault(ct => ct.IDSan == n.IDSan && ct.NgayDat.Date == ngayKiemTra)
                       select new { n.IDSan, n.TenSan, n.DonGia, n.MoTa, n.TrangThai,
                           TinhTrangHienTai = cthd != null ? "Đã đặt" : "Trống",
                           NgayDat = cthd != null ? cthd.NgayDat.ToString("dd/MM/yyyy") : "Chưa đặt",
                           //GioBatDau = cthd != null ? cthd.GioBatDau.ToString("HH:mm") : "Chưa đặt", 
                           //GioKetThuc = cthd != null ? cthd.GioKetThuc.ToString("HH:mm") : "Chưa đặt"
                       };
            dataGridViewSan.DataSource = list.ToList();
            dataGridViewSan.Columns["TenSan"].HeaderText = "Tên sân";
            dataGridViewSan.Columns["DonGia"].HeaderText = "Đơn giá";
            dataGridViewSan.Columns["MoTa"].HeaderText = "Mô tả";
            dataGridViewSan.Columns["TrangThai"].HeaderText = "Trạng thái";
            dataGridViewSan.Columns["TinhTrangHienTai"].HeaderText = "Tình trạng";
            dataGridViewSan.Columns["NgayDat"].HeaderText = "Ngày Đặt";

            //điều chỉnh kích thước các cột
            dataGridViewSan.Columns["IDSan"].Width = 55;
            dataGridViewSan.Columns["TenSan"].Width = 120;
            dataGridViewSan.Columns["DonGia"].Width = 100;
            dataGridViewSan.Columns["MoTa"].Width = 180;
            dataGridViewSan.Columns["TrangThai"].Width = 110;
            dataGridViewSan.Columns["TinhTrangHienTai"].Width = 110;
            dataGridViewSan.Columns["NgayDat"].Width = 110;

        }

        private void cbLoaiSan_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewSan.ClearSelection();
            if (cbLoaiSan.SelectedIndex >= 0)
            {
                //lấy tên loại sân
                string tenLoaiSan = cbLoaiSan.SelectedItem.ToString();

                var loaiSan = dsLoaiSan.FirstOrDefault(ls => ls.TenLoaiSan == tenLoaiSan); //lấy id dựa tên
                if (loaiSan != null)
                {
                    //lấy ngày kiểm tra 
                    DateTime ngayKiemTra = datetimePickerNgayKiemTra.Value.Date;
                    var danhSachSan = dsSan.Where(s => s.IDLoaiSan == loaiSan.IDLoaiSan)
                                           .Select(s => new{s.IDSan,s.TenSan,s.DonGia,s.MoTa,s.TrangThai,
                                               TinhTrangHienTai = dsCTHDSan.Any(ct => ct.IDSan == s.IDSan && ct.NgayDat.Date == ngayKiemTra)
                                                    ? "Đã đặt"
                                                    : "Trống",
                                               NgayDat = dsCTHDSan.FirstOrDefault(ct => ct.IDSan == s.IDSan && ct.NgayDat.Date == ngayKiemTra)?.NgayDat.ToString("dd/MM/yyyy") ?? "Chưa đặt"
                                           }).ToList();

                    // Hiển thị danh sách sân trong DataGridView
                    dataGridViewSan.DataSource = danhSachSan;

                    dataGridViewSan.Columns["TenSan"].HeaderText = "Tên sân";
                    dataGridViewSan.Columns["DonGia"].HeaderText = "Đơn giá";
                    dataGridViewSan.Columns["MoTa"].HeaderText = "Mô tả";
                    dataGridViewSan.Columns["TrangThai"].HeaderText = "Trạng thái";
                    dataGridViewSan.Columns["TinhTrangHienTai"].HeaderText = "Tình trạng";
                    dataGridViewSan.Columns["NgayDat"].HeaderText = "Ngày Đặt";

                    //điều chỉnh kích thước các cột
                    dataGridViewSan.Columns["IDSan"].Width = 55;
                    dataGridViewSan.Columns["TenSan"].Width = 120;
                    dataGridViewSan.Columns["DonGia"].Width = 100;
                    dataGridViewSan.Columns["MoTa"].Width = 180;
                    dataGridViewSan.Columns["TrangThai"].Width = 110;
                    dataGridViewSan.Columns["TinhTrangHienTai"].Width = 110;
                    dataGridViewSan.Columns["NgayDat"].Width = 110;

                }
            }

        }


        private void txtTimKiemSan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //lấy keyword trong txt
                string keyword = txtTimKiemSan.Text;
                //gọi phương thức tìm kiếm
                var dsTimKiemSan = ctrl_San.search(keyword);
                DateTime ngayKiemTra = datetimePickerNgayKiemTra.Value.Date;
                //hiển thị kq
                var list = from san in dsTimKiemSan
                           let cthd = dsCTHDSan.FirstOrDefault(ct => ct.IDSan == san.IDSan  && ct.NgayDat.Date == ngayKiemTra)
                           select new
                           {
                               san.IDSan,
                               san.TenSan,
                               san.DonGia,
                               san.MoTa,
                               san.TrangThai,
                               TinhTrangHienTai = cthd != null ? "Đã đặt" : "Trống",
                               NgayDat = cthd?.NgayDat.ToString("dd/MM/yyyy") ?? "Chưa đặt"
                           };
                    //hiển thị kq
                dataGridViewSan.DataSource = list.ToList();
                dataGridViewSan.Columns["TenSan"].HeaderText = "Tên sân";
                dataGridViewSan.Columns["DonGia"].HeaderText = "Đơn giá";
                dataGridViewSan.Columns["MoTa"].HeaderText = "Mô tả";
                dataGridViewSan.Columns["TrangThai"].HeaderText = "Trạng thái";
                dataGridViewSan.Columns["TinhTrangHienTai"].HeaderText = "Tình trạng";
                dataGridViewSan.Columns["NgayDat"].HeaderText = "Ngày Đặt";

                //điều chỉnh kích thước các cột
                dataGridViewSan.Columns["IDSan"].Width = 55;
                dataGridViewSan.Columns["TenSan"].Width = 120;
                dataGridViewSan.Columns["DonGia"].Width = 100;
                dataGridViewSan.Columns["MoTa"].Width = 180;
                dataGridViewSan.Columns["TrangThai"].Width = 110;
                dataGridViewSan.Columns["TinhTrangHienTai"].Width = 110;
                dataGridViewSan.Columns["NgayDat"].Width = 110;

            }
            catch
            {

            }

        }

        private void datetimePickerNgayKiemTra_ValueChanged(object sender, EventArgs e)
        {
            load_SanTheThao();
        }

        private void dataGridViewSan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridViewSan.CurrentRow;
            int index = row.Index;
            if(index >= 0)
            {
                san = dsSan[index];
                txtIDSan.Text = san.IDSan;
                txtTenSan.Text = san.TenSan;
                txtDonGia.Text = san.DonGia + "";
                txtMoTa.Text = san.MoTa;
                cbTinhTrang.Text = san.TrangThai;

                var loaiSan = dsLoaiSan.FirstOrDefault(ls => ls.IDLoaiSan == san.IDLoaiSan);
                if (loaiSan != null)
                {
                    cbChonLoaiSan.Text = loaiSan.TenLoaiSan;
                }
            }
            txtIDSan.ReadOnly = true;
            txtTenSan.ReadOnly = true;
            txtDonGia.ReadOnly = true;
            txtMoTa.ReadOnly = true;
            cbTinhTrang.Enabled = false;
            cbChonLoaiSan.Enabled = false;
        }

        private void cbChonLoaiSan_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btnThemSan_Click(object sender, EventArgs e)
        {
            btnXacNhanThemSan.Visible = true;

            txtIDSan.ReadOnly = false;
            txtTenSan.ReadOnly = false;
            txtDonGia.ReadOnly = false;
            txtMoTa.ReadOnly = false;
            cbTinhTrang.Enabled = true;
            cbChonLoaiSan.Enabled = true;

            txtIDSan.Clear();
            txtTenSan.Clear();
            txtDonGia.Clear();
            txtMoTa.Clear();
            cbChonLoaiSan.SelectedIndex = -1;
            cbTinhTrang.SelectedIndex = -1;

        }

        private void btnXacNhanThemSan_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtIDSan.Text) ||
                string.IsNullOrWhiteSpace(txtTenSan.Text) ||
                string.IsNullOrWhiteSpace(txtDonGia.Text) ||
                string.IsNullOrWhiteSpace(txtMoTa.Text) ||
                string.IsNullOrWhiteSpace(cbTinhTrang.Text) ||
                string.IsNullOrWhiteSpace(cbChonLoaiSan.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var SanNew = new SAN();
            SanNew.IDSan = txtIDSan.Text;
            SanNew.TenSan = txtTenSan.Text;
            SanNew.DonGia = decimal.Parse(txtDonGia.Text);
            SanNew.MoTa = txtMoTa.Text;
            SanNew.TrangThai = cbTinhTrang.Text;

            string tenLoaiSan = cbChonLoaiSan.Text;
            var loaiSan = dsLoaiSan.FirstOrDefault(ls => ls.TenLoaiSan == tenLoaiSan);
            if (loaiSan != null)
            {
                SanNew.IDLoaiSan = loaiSan.IDLoaiSan; 
            }
            else
            {
                MessageBox.Show("Loại sân không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var counter = dsSan.Where(masan => masan.IDSan == txtIDSan.Text).ToList().Count();
            if(counter > 0)
            {
                MessageBox.Show("Mã sân thể thao bị trùng!");
                return;
            }
            else
            {
                dsSan.Add(SanNew);
                ctrl_San.add(SanNew);
                MessageBox.Show("Thêm sân thể thao thành công!", "Thông báo", MessageBoxButtons.OK);
                load_SanTheThao();
                txtIDSan.ReadOnly = true;
                txtTenSan.ReadOnly = true;
                txtDonGia.ReadOnly = true;
                txtMoTa.ReadOnly = true;
                cbTinhTrang.Enabled = false;
                cbChonLoaiSan.Enabled = false;

                txtIDSan.Clear();
                txtTenSan.Clear();
                txtDonGia.Clear();
                txtMoTa.Clear();
                cbChonLoaiSan.SelectedIndex = -1;
                cbTinhTrang.SelectedIndex = -1;
            }
        }
        private void btnSuaSan_Click(object sender, EventArgs e)
        {
            btXacNhanSuaSan.Visible = true;

            txtIDSan.ReadOnly = false;
            txtTenSan.ReadOnly = false;
            txtDonGia.ReadOnly = false;
            txtMoTa.ReadOnly = false;
            cbTinhTrang.Enabled = true;
            cbChonLoaiSan.Enabled = true;

        }

        private void btXacNhanSuaSan_Click(object sender, EventArgs e)
        {
            if( san != null )
            {
                if (string.IsNullOrWhiteSpace(txtIDSan.Text) ||
                    string.IsNullOrWhiteSpace(txtTenSan.Text) ||
                    string.IsNullOrWhiteSpace(txtDonGia.Text) ||
                    string.IsNullOrWhiteSpace(txtMoTa.Text) ||
                    string.IsNullOrWhiteSpace(cbTinhTrang.Text) ||
                    string.IsNullOrWhiteSpace(cbChonLoaiSan.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
                    return;
                }
                san.IDSan = txtIDSan.Text;
                san.TenSan = txtTenSan.Text;
                san.DonGia = decimal.Parse(txtDonGia.Text);
                san.TrangThai = cbTinhTrang.Text;
                string tenLoaiSan = cbChonLoaiSan.Text;
                var loaiSan = dsLoaiSan.FirstOrDefault(ls => ls.TenLoaiSan == tenLoaiSan);
                if (loaiSan != null)
                {
                    san.IDLoaiSan = loaiSan.IDLoaiSan;
                }
                else
                {
                    MessageBox.Show("Loại sân không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var counter = dsSan.Where(masan => masan.IDSan == txtIDSan.Text && masan != san).ToList().Count();
                if (counter > 0)
                {
                    MessageBox.Show("Mã sân thể thao bị trùng!");
                    return;
                }
                else
                {
                    ctrl_San.update(san);
                    MessageBox.Show("Sửa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK);
                    load_SanTheThao();
                }
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            txtIDSan.ReadOnly = true;
            txtTenSan.ReadOnly = true;
            txtDonGia.ReadOnly = true;
            txtMoTa.ReadOnly = true;
            cbTinhTrang.Enabled = false;
            cbChonLoaiSan.Enabled = false;

            txtIDSan.Clear();
            txtTenSan.Clear();
            txtDonGia.Clear();
            txtMoTa.Clear();
            cbChonLoaiSan.SelectedIndex = -1;
            cbTinhTrang.SelectedIndex = -1;

            btnXacNhanThemSan.Visible = false;
            btXacNhanSuaSan.Visible= false;
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

        private void btnQLKH_Click(object sender, EventArgs e)
        {
            FQLKhachHang menu = new FQLKhachHang();
            this.Hide();
            menu.ShowDialog();
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            FChonQuyen me = new FChonQuyen();
            this.Hide();
            me.ShowDialog();
            this.Show();
        }
    }
}
