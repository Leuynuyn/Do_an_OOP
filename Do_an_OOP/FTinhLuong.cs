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
    public partial class FTinhLuong : Form
    {
        Ctrl_NhanVien ctrl_NhanVien = new Ctrl_NhanVien();
        Ctrl_CaLamViec ctrl_CaLamViec = new Ctrl_CaLamViec();
        Ctrl_Luong ctrl_Luong = new Ctrl_Luong();
        Ctrl_LoaiCaLam ctrl_LoaiCaLam = new Ctrl_LoaiCaLam();
        List<NHANVIEN> dsnhanvien = null;
        List<CALAMVIEC> dscalamviec = null;
        List<TINHLUONG> dsLuong = null;
        List<LOAICALAM> dsloaicalam = null;
        NHANVIEN nhanvien;
        CALAMVIEC calamviec;
        TINHLUONG tinhluong;
        LOAICALAM loaicalam;

        public FTinhLuong()
        {
            InitializeComponent();
        }

        public void load_TenNV()
        {
            dataGridViewNV.ClearSelection();
            var list = from nv in dsnhanvien
                       join clv in CUtils.db.CALAMVIECs on nv.IDNhanVien equals clv.IDNhanVien
                       join lcl in CUtils.db.LOAICALAMs on clv.IDLoaiCa equals lcl.IDLoaiCa
                       orderby clv.NgayLam descending
                       select new
                       {
                           nv.IDNhanVien,
                           nv.TenNhanVien,
                           nv.GioiTinh,
                           clv.IDCaLamViec,
                           NgayLam = clv.NgayLam.ToString("dd/MM/yyyy"),
                           lcl.TenCa,
                           lcl.GioBatDau,
                           lcl.GioKetThuc,
                           lcl.TongGioCa
                       };
            dataGridViewNV.DataSource = list.ToList();

            dataGridViewNV.Columns["IDNhanVien"].HeaderText = "IDNV";
            dataGridViewNV.Columns["IDCaLamViec"].HeaderText = "IDCa";
            dataGridViewNV.Columns["IDNhanVien"].HeaderText = "IDNV";
            dataGridViewNV.Columns["TongGioCa"].HeaderText = "TongGio";


            dataGridViewNV.Columns["IDNhanVien"].Width = 60;
            dataGridViewNV.Columns["IDCaLamViec"].Width = 60;
            dataGridViewNV.Columns["tenCa"].Width = 60;
            dataGridViewNV.Columns["TongGioCa"].Width = 70;
            dataGridViewNV.Columns["GioiTinh"].Width = 70;
            dataGridViewNV.Columns["TenNhanVien"].Width = 148;

        }

        public void load_LuongNV()
        {
            var list = from clv in dscalamviec
                       join loaicalam in dsloaicalam on clv.IDLoaiCa equals loaicalam.IDLoaiCa
                       join nv in dsnhanvien on clv.IDNhanVien equals nv.IDNhanVien
                       group new { clv, loaicalam, nv } by new
                       {
                           clv.IDNhanVien,
                           nv.TenNhanVien,
                           Thang = clv.NgayLam.Month,
                           Nam = clv.NgayLam.Year
                       } into grouped
                       select new
                       {
                           IDNhanVien = grouped.Key.IDNhanVien,
                           TenNhanVien = grouped.Key.TenNhanVien,
                           Thang = grouped.Key.Thang,
                           Nam = grouped.Key.Nam,
                           TongGio = grouped.Sum(g => g.loaicalam.TongGioCa),
                           LuongTheoGio = grouped.FirstOrDefault().loaicalam.LuongTheoGio,
                           TongLuong = grouped.Sum(g => g.loaicalam.TongGioCa * g.loaicalam.LuongTheoGio)
                       };
            dataGridViewLuong.DataSource = list.ToList();

            dataGridViewLuong.Columns["IDNhanVien"].Width = 100;
            dataGridViewLuong.Columns["TenNhanVien"].Width = 160;
            dataGridViewLuong.Columns["LuongTheoGio"].Width = 120;

        }


        private void FTinhLuong_Load(object sender, EventArgs e)
        {
            try
            {
                dsnhanvien = ctrl_NhanVien.findAll();
                dscalamviec = ctrl_CaLamViec.findAll();
                dsLuong = ctrl_Luong.findAll();
                dsloaicalam = ctrl_LoaiCaLam.findAll();
                load_LuongNV();
                load_TenNV();
                load_ComboBoxDsCaLamViec();
                LoadComboBoxThang();
                dataGridViewNV.ClearSelection();

                btnXacNhanThem.Visible = false;
                btnXacNhanSua.Visible = false;
                btnXacNhanXoa.Visible = false;



            }
            catch
            {

            }
        }

        //ham load du liệu vào combobox
        private void load_ComboBoxDsCaLamViec()
        {
            cbboxLoaiCaLam.Items.Clear();
            foreach (var loaicalam in dsloaicalam)
            {
                cbboxLoaiCaLam.Items.Add(loaicalam.TenCa);
            }
        }

        private void LoadComboBoxThang()
        {
            cbThang.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                cbThang.Items.Add(i.ToString()); 
            }
            cbThang.SelectedIndex = -1;
        }


        private void dataGridViewNV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewNV.Rows[e.RowIndex];
                string idNhanVien = row.Cells["IDNhanVien"].Value.ToString();
                string tenNhanVien = row.Cells["TenNhanVien"].Value.ToString();
                string idCaLamViec = row.Cells["IDCaLamViec"].Value.ToString();
                string ngayLam = row.Cells["NgayLam"].Value.ToString();
                string calam = row.Cells["TenCa"].Value.ToString();
                string gioBatDau = row.Cells["GioBatDau"].Value.ToString();
                string gioKetThuc = row.Cells["GioKetThuc"].Value.ToString();
                txtIDNhanVien.Text = idNhanVien;
                txtTenNv.Text = tenNhanVien;
                txtIDCaLamViec.Text = idCaLamViec;
                cbboxLoaiCaLam.Text = calam;
                dateTimeNgayLam.Text = ngayLam;
                if (DateTime.TryParse(gioBatDau, out DateTime dtBatDau))
                {
                    DateTimeBatDau.Value = dtBatDau;
                }

                if (DateTime.TryParse(gioKetThuc, out DateTime dtKetThuc))
                {
                    DateTimeKetThuc.Value = dtKetThuc;
                }
                calamviec = dscalamviec.FirstOrDefault(c => c.IDCaLamViec == idCaLamViec);
            }
            txtIDNhanVien.ReadOnly = true;
            txtIDCaLamViec.ReadOnly = true;
            txtTenNv.ReadOnly = true;
            dateTimeNgayLam.Enabled = false;
            DateTimeBatDau.Enabled = false;
            DateTimeKetThuc.Enabled = false;
            cbboxLoaiCaLam.Enabled = false;
        }

        private void btnThemSan_Click(object sender, EventArgs e)
        {
            txtIDCaLamViec.Clear();
            txtIDNhanVien.Clear();
            txtTenNv.Clear();
            DateTimeBatDau.ResetText();
            DateTimeKetThuc.ResetText();
            dateTimeNgayLam.ResetText();
            btnXacNhanThem.Visible = true;

            txtIDNhanVien.ReadOnly = false;
            txtIDCaLamViec.ReadOnly = false;
            txtTenNv.ReadOnly = false;
            dateTimeNgayLam.Enabled = true;
            DateTimeBatDau.Enabled = true;
            DateTimeKetThuc.Enabled = true;
            cbboxLoaiCaLam.Enabled = true;
            cbboxLoaiCaLam.SelectedIndex = -1;
        }

        private void btnXacNhanThem_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(dateTimeNgayLam.Text) ||
                    string.IsNullOrWhiteSpace(txtIDCaLamViec.Text) ||
                    string.IsNullOrWhiteSpace(cbboxLoaiCaLam.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                    return;
                }
                var CaLamMoiNew = new CALAMVIEC();
                CaLamMoiNew.IDCaLamViec = txtIDCaLamViec.Text;
                CaLamMoiNew.NgayLam = dateTimeNgayLam.Value;
                CaLamMoiNew.IDNhanVien = txtIDNhanVien.Text;

                string tenCaSelected = cbboxLoaiCaLam.Text;
                var selectedLoaiCa = dsloaicalam.FirstOrDefault(lcl => lcl.TenCa == tenCaSelected);
                CaLamMoiNew.IDLoaiCa = selectedLoaiCa.IDLoaiCa;

                var counter = dscalamviec.Where(clv => clv.IDCaLamViec == txtIDCaLamViec.Text).ToList().Count();
                if (counter > 0)
                {
                    MessageBox.Show("Mã ca làm việc bị trùng!");
                    return;
                }
                else
                {
                    dscalamviec.Add(CaLamMoiNew);
                    ctrl_CaLamViec.add(CaLamMoiNew);
                    MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK);
                    load_TenNV();

                    txtIDCaLamViec.Clear();
                    txtIDNhanVien.Clear();
                    txtTenNv.Clear();
                    dateTimeNgayLam.ResetText();
                    DateTimeKetThuc.ResetText();
                    dateTimeNgayLam.ResetText();

                    btnXacNhanThem.Visible = false;

                }
            }
            catch
            {

            }
        }

        private void btnSuaSan_Click_1(object sender, EventArgs e)
        {
            btnXacNhanSua.Visible = true;
            txtIDNhanVien.ReadOnly = false;
            txtIDCaLamViec.ReadOnly = false;
            txtTenNv.ReadOnly = false;
            dateTimeNgayLam.Enabled = true;
            DateTimeBatDau.Enabled = true;
            DateTimeKetThuc.Enabled = true;
            cbboxLoaiCaLam.Enabled = true;

        }

        private void btnXacNhanSua_Click(object sender, EventArgs e)
        {
            if (calamviec != null)
            {
                if (cbboxLoaiCaLam.SelectedIndex == -1 || string.IsNullOrEmpty(dateTimeNgayLam.Text) ||
                    string.IsNullOrWhiteSpace(txtIDCaLamViec.Text) || string.IsNullOrWhiteSpace(txtIDNhanVien.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                    return;
                }
                calamviec.IDCaLamViec = txtIDCaLamViec.Text;
                calamviec.NgayLam = dateTimeNgayLam.Value;
                calamviec.IDNhanVien = txtIDNhanVien.Text;
                string tenCaSelected = cbboxLoaiCaLam.Text;
                var selectedLoaiCa = dsloaicalam.FirstOrDefault(lcl => lcl.TenCa == tenCaSelected);
                calamviec.IDLoaiCa = selectedLoaiCa.IDLoaiCa;

                ctrl_CaLamViec.update(calamviec);
                MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK);
                load_TenNV();

                txtIDCaLamViec.Clear();
                txtIDNhanVien.Clear();
                txtTenNv.Clear();
                dateTimeNgayLam.ResetText();
                DateTimeKetThuc.ResetText();
                dateTimeNgayLam.ResetText();
                cbboxLoaiCaLam.SelectedIndex = -1;
                btnXacNhanSua.Visible = false;
            }
            else
            {
                MessageBox.Show("Chưa chọn nhân viên để cập nhật");
            }

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            btnXacNhanXoa.Visible = true;
        }


        private void btnXacNhanXoa_Click_1(object sender, EventArgs e)
        {
            if (calamviec == null)
            {
                MessageBox.Show("Không có ca làm việc được chọn để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                dscalamviec.Remove(calamviec);
                ctrl_CaLamViec.remove(calamviec);

                MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK);

                load_TenNV();

                cbboxLoaiCaLam.SelectedIndex = -1;
                dateTimeNgayLam.ResetText();
                btnXacNhanThem.Visible = false;
            }
        }
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            txtIDCaLamViec.Clear();
            txtIDNhanVien.Clear();
            txtTenNv.Clear();
            cbboxLoaiCaLam.SelectedIndex = -1;
            dateTimeNgayLam.ResetText();
            DateTimeKetThuc.ResetText();
            dateTimeNgayLam.ResetText();
            btnXacNhanThem.Visible = false;
            btnXacNhanSua.Visible = false;
            btnXacNhanXoa.Visible = false;
        }

        private void btnHomeAd_Click_1(object sender, EventArgs e)
        {
            HomeAdmin me = new HomeAdmin();
            this.Hide();
            me.ShowDialog();
            this.Show();
            Close();

        }

        private void btnQLNV_Click_1(object sender, EventArgs e)
        {
            FQLNhanVien me = new FQLNhanVien();
            this.Hide();
            me.ShowDialog();
            this.Show();
            Close();
        }

        private void btnQLSan_Click_1(object sender, EventArgs e)
        {
            FQLSan menu = new FQLSan();
            this.Hide();
            menu.ShowDialog();
            this.Show();
            Close();

        }

        private void btnQLSuKien_Click_1(object sender, EventArgs e)
        {
            FSuKienAD en = new FSuKienAD();
            this.Hide();
            en.ShowDialog();
            this.Show();
            Close();


        }

        private void btnLuong_Click_1(object sender, EventArgs e)
        {
            FTinhLuong me = new FTinhLuong();
            this.Hide();
            me.ShowDialog();
            this.Show();
            Close();

        }

        private void ClosePage_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            FChonQuyen me = new FChonQuyen();
            this.Hide();
            me.ShowDialog();
            this.Show();
        }

        private void cbboxLoaiCaLam_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedTenCa = cbboxLoaiCaLam.SelectedItem?.ToString();

                if (!string.IsNullOrEmpty(selectedTenCa))
                {
                    var selectedLoaiCa = dsloaicalam.FirstOrDefault(lcl => lcl.TenCa == selectedTenCa);

                    if (selectedLoaiCa != null)
                    {
                        DateTimeBatDau.Value = DateTime.Today.Add(selectedLoaiCa.GioBatDau);
                        DateTimeKetThuc.Value = DateTime.Today.Add(selectedLoaiCa.GioKetThuc);
                    }
                }
            }
            catch
            {
            }
        }

        private void txtTenNv_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtIDNhanVien_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string idNhanVien = txtIDNhanVien.Text.Trim();
                var nhanVien = dsnhanvien.FirstOrDefault(nv => nv.IDNhanVien == idNhanVien);

                if (nhanVien != null)
                {
                    txtTenNv.Text = nhanVien.TenNhanVien;
                }
                else
                {
                    txtTenNv.Clear();
                }
            }
            catch
            {
            }
        }

        private void btnLoadForm_Click(object sender, EventArgs e)
        {
            RefreshForm();
        }

        public void RefreshForm()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox) textBox.Clear();
                else if (control is DataGridView dataGridView) dataGridView.DataSource = null;
            }
            load_LuongNV();
        }

        private void IDNVLuong_TextChanged(object sender, EventArgs e)
        {
            string idNhanVien = IDNVLuong.Text.Trim();
            var nhanVien = dsnhanvien.FirstOrDefault(nv => nv.IDNhanVien == idNhanVien);

            if (nhanVien != null)
            {
                txtTenNVLuong.Text = nhanVien.TenNhanVien;
            }
            else
            {
                txtTenNVLuong.Clear();
            }

            string IDNhanVien = txtIDNhanVien.Text.Trim();
            if (!string.IsNullOrEmpty(idNhanVien)) 
            {
                var filteredList = from clv in dscalamviec
                                   join loaicalam in dsloaicalam on clv.IDLoaiCa equals loaicalam.IDLoaiCa
                                   join nv in dsnhanvien on clv.IDNhanVien equals nv.IDNhanVien
                                   where nv.IDNhanVien == idNhanVien
                                   group new { clv, loaicalam, nv } by new
                                   {
                                       clv.IDNhanVien,
                                       nv.TenNhanVien,
                                       Thang = clv.NgayLam.Month,
                                       Nam = clv.NgayLam.Year
                                   } into grouped
                                   select new
                                   {
                                       IDNhanVien = grouped.Key.IDNhanVien,
                                       TenNhanVien = grouped.Key.TenNhanVien,
                                       Thang = grouped.Key.Thang,
                                       Nam = grouped.Key.Nam,
                                       TongGio = grouped.Sum(g => g.loaicalam.TongGioCa),
                                       LuongTheoGio = grouped.FirstOrDefault().loaicalam.LuongTheoGio,
                                       TongLuong = grouped.Sum(g => g.loaicalam.TongGioCa * g.loaicalam.LuongTheoGio)
                                   };

                dataGridViewLuong.DataSource = filteredList.ToList();
            }
            else
            {
                load_LuongNV();
            }

        }

        private void cbThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Lấy giá trị tháng từ ComboBox
                if (int.TryParse(cbThang.SelectedItem?.ToString(), out int selectedMonth))
                {
                    // Lọc danh sách lương theo tháng được chọn
                    var filteredList = from clv in dscalamviec
                                       join loaicalam in dsloaicalam on clv.IDLoaiCa equals loaicalam.IDLoaiCa
                                       join nv in dsnhanvien on clv.IDNhanVien equals nv.IDNhanVien
                                       where clv.NgayLam.Month == selectedMonth
                                       group new { clv, loaicalam, nv } by new
                                       {
                                           clv.IDNhanVien,
                                           nv.TenNhanVien,
                                           Thang = clv.NgayLam.Month,
                                           Nam = clv.NgayLam.Year
                                       } into grouped
                                       select new
                                       {
                                           IDNhanVien = grouped.Key.IDNhanVien,
                                           TenNhanVien = grouped.Key.TenNhanVien,
                                           Thang = grouped.Key.Thang,
                                           Nam = grouped.Key.Nam,
                                           TongGio = grouped.Sum(g => g.loaicalam.TongGioCa),
                                           LuongTheoGio = grouped.FirstOrDefault().loaicalam.LuongTheoGio,
                                           TongLuong = grouped.Sum(g => g.loaicalam.TongGioCa * g.loaicalam.LuongTheoGio)
                                       };

                    // Hiển thị dữ liệu đã lọc lên DataGridView
                    dataGridViewLuong.DataSource = filteredList.ToList();

                }
            }
            catch { }
        }

        private void txtTenNVLuong_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTimKiemIDcalam_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    //lấy keyword trong txt
            //    string keyword = txtTimKiemIDcalam.Text;
            //    //gọi phương thức tìm kiếm
            //    var dsTimkiem = ctrl_CaLamViec.search(keyword);
            //    //hiển thị kq
            //    var list = from nv in dsTimkiem
            //               join clv in CUtils.db.CALAMVIECs on nv.IDNhanVien equals clv.IDNhanVien
            //               join lcl in CUtils.db.LOAICALAMs on clv.IDLoaiCa equals lcl.IDLoaiCa
            //               orderby clv.NgayLam descending
            //               select new
            //               {
            //                   nv.IDNhanVien,
            //                   TenNhanVien = nv.TenNhanVien,
            //                   nv.GioiTinh,
            //                   clv.IDCaLamViec,
            //                   NgayLam = clv.NgayLam.ToString("dd/MM/yyyy"),
            //                   lcl.TenCa,
            //                   lcl.GioBatDau,
            //                   lcl.GioKetThuc,
            //                   lcl.TongGioCa
            //               };

            //    dataGridViewNV.DataSource = list.ToList();

            //    dataGridViewNV.Columns["IDNhanVien"].HeaderText = "IDNV";
            //    dataGridViewNV.Columns["IDCaLamViec"].HeaderText = "IDCa";
            //    dataGridViewNV.Columns["IDNhanVien"].HeaderText = "IDNV";
            //    dataGridViewNV.Columns["TongGioCa"].HeaderText = "TongGio";


            //    dataGridViewNV.Columns["IDNhanVien"].Width = 60;
            //    dataGridViewNV.Columns["IDCaLamViec"].Width = 60;
            //    dataGridViewNV.Columns["tenCa"].Width = 60;
            //    dataGridViewNV.Columns["TongGioCa"].Width = 70;
            //    dataGridViewNV.Columns["GioiTinh"].Width = 70;
            //    dataGridViewNV.Columns["TenNhanVien"].Width = 148;

            //}
            //catch
            //{

            //}

        }
    }
}
