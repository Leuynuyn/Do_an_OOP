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

    public partial class HoaDonNV : Form
    {
        Ctrl_San ctrlSan = new Ctrl_San();
        List<SAN> dsSan = new List<SAN>();
        Ctrl_LoaiSan CtrlLoaiSan = new Ctrl_LoaiSan();
        List<LOAISAN> dsLoaiSan = new List<LOAISAN>();
        LOAISAN loaisan;
        CtrlHoaDon ctrlhoadon = new CtrlHoaDon();
        List<HDDATSAN> dshoadon = new List<HDDATSAN>();
        CtrlKhachHang CtrlKhachHang = new CtrlKhachHang();
        List<KHACHHANG> dsKH = new List<KHACHHANG>();
        Ctrl_CTHDSan ctrlcthd = new Ctrl_CTHDSan();
        List<CTHDDATSAN> dsCTHD = new List<CTHDDATSAN>();
        CTHDDATSAN ct;
        int index;
        SAN san;
        HDDATSAN hoadon;
        KHACHHANG kh;
        string idLoaiSan = "";
        string idKH = "";
        string idHD = "";
        string idCTHD = "";
        string tenLoaiSan = "";
        public HoaDonNV()
        {
            InitializeComponent();
        }

        public void load_LoaiSan()
        {
            var list = from a in dsLoaiSan
                       select a.TenLoaiSan;
            cbbSan.DataSource = list.ToList();
            if (dsLoaiSan.Count > 0)
            {
                Load_CTHD(tenLoaiSan);
            }
        }


        private void HoaDonNV_Load(object sender, EventArgs e)
        {
            dsLoaiSan = CtrlLoaiSan.findAll();
            dsCTHD = ctrlcthd.findAll();
            load_LoaiSan();
            if (dsLoaiSan.Count > 0)
            {
                loaisan = dsLoaiSan[0];
            }
        }

        public void Load_CTHD(string tenSan)
        {
            if (tenLoaiSan != null)
            {
                dsCTHD = ctrlcthd.findByTenLoaiSan(tenLoaiSan);
                var list = from ct in dsCTHD
                           orderby ct.IDCTHDDatSan descending
                           select new { ct.IDCTHDDatSan, ct.SAN.TenSan, ct.NgayDat, ct.GioBatDau, ct.GioKetThuc, ct.DonGia, ct.HDDATSAN.TrangThai, ct.HDDATSAN.KHACHHANG.TenKhachHang };
                dtgvHoaDon.DataSource = list.ToList();
                dtgvHoaDon.Columns[0].HeaderText = "Mã CTHD";
                dtgvHoaDon.Columns[1].HeaderText = "Tên Sân";
                dtgvHoaDon.Columns[2].HeaderText = "Ngày Đặt";
                dtgvHoaDon.Columns[3].HeaderText = "Giờ bắt đầu";
                dtgvHoaDon.Columns[4].HeaderText = "Giờ kết thúc";
                dtgvHoaDon.Columns[5].HeaderText = "Đơn giá";
                dtgvHoaDon.Columns[6].HeaderText = "Trạng thái";
                dtgvHoaDon.Columns[7].HeaderText = "Ten khach hang";
                int chieudai = dtgvHoaDon.Width;
                dtgvHoaDon.Columns[0].Width = (int)(chieudai * 0.1);
                dtgvHoaDon.Columns[1].Width = (int)(chieudai * 0.15);
                dtgvHoaDon.Columns[2].Width = (int)(chieudai * 0.1);
                dtgvHoaDon.Columns[3].Width = (int)(chieudai * 0.15);
                dtgvHoaDon.Columns[4].Width = (int)(chieudai * 0.15);
                dtgvHoaDon.Columns[5].Width = (int)(chieudai * 0.1);
                dtgvHoaDon.Columns[6].Width = (int)(chieudai * 0.1);
                dtgvHoaDon.Columns[7].Width = (int)(chieudai * 0.1);
            }
        }

        private void dtgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            DataGridViewRow row = dtgvHoaDon.CurrentRow;
            index = row.Index;
            if (e.RowIndex < 0 || e.RowIndex >= dsCTHD.Count) return;
            ct = dsCTHD[index];
            txtDonGia.Text = ct.DonGia.ToString();
            txtMaCTHD.Text = ct.IDCTHDDatSan.ToString();
            txtTenSan.Text = ct.SAN.TenSan.ToString();
            txtThanhTien.Text = ct.HDDATSAN.TongTien.ToString();
            
            txtTienCoc.Text = ct.HDDATSAN.TienCoc.ToString();
            decimal tongTien = Convert.ToDecimal(ct.HDDATSAN.TongTien);
            decimal tienCoc = Convert.ToDecimal(ct.HDDATSAN.TienCoc);
            decimal tienCanTra = tongTien - tienCoc;

            txtTienCanTra.Text = tienCanTra.ToString("N2");
            dtpkNgayDat.Value = Convert.ToDateTime(row.Cells["NgayDat"].Value);
            dtpkBatDau.Value = Convert.ToDateTime(row.Cells["GioBatDau"].Value);
            dtpkKetThuc.Value = Convert.ToDateTime(row.Cells["GioKetThuc"].Value);
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            HomeNV nv = new HomeNV();
            nv.Show();
            this.Hide();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtgvSan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (index < 0 || index >= dsCTHD.Count)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần thanh toán!");
                return;
            }
            CTHDDATSAN ct = dsCTHD[index];
            hoadon = ct.HDDATSAN;
            hoadon.TrangThai = "Đã thanh toán";
            san = ct.SAN;
            san.TrangThai = "Còn trống";
            ctrlSan.upDateSan(san);
            ctrlhoadon.update(hoadon);
            ctrlcthd.upDate(ct);

            MessageBox.Show("Thanh toán thành công!");
            Load_CTHD(tenLoaiSan);
        }

        private void cbbLoaiSan_SelectedIndexChanged(object sender, EventArgs e)
        {
            index = cbbSan.SelectedIndex;
            if (index >= 0 && index < dsLoaiSan.Count)
            {
                loaisan = dsLoaiSan[index];
                tenLoaiSan = loaisan.TenLoaiSan;

                Load_CTHD(tenLoaiSan);

            }
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
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

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                var ketQua = ctrlcthd.TimKiemHD(tuKhoa);
                var list = from ct in ketQua
                           select new
                           {
                               ct.IDCTHDDatSan,
                               ct.SAN.TenSan,
                               ct.NgayDat,
                               ct.GioBatDau,
                               ct.GioKetThuc,
                               ct.DonGia,
                               ct.HDDATSAN.TrangThai,
                               ct.HDDATSAN.KHACHHANG.TenKhachHang
                           };

                dtgvHoaDon.DataSource = list.ToList();
            }
            else
            {
                MessageBox.Show("Không tìm thấy kết quả nào!!!!!");
                dtgvHoaDon.DataSource = null;
            }
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
