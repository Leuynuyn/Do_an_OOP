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
    public partial class HomeNV : Form
    {
        Ctrl_San CtrlSan = new Ctrl_San();
        List<SAN> dsSan = new List<SAN>();
        Ctrl_LoaiSan CtrlLoaiSan = new Ctrl_LoaiSan();
        List<LOAISAN> dsLoaiSan = new List<LOAISAN>();
        LOAISAN loaisan;
        CtrlHoaDon ctrlhoadon = new CtrlHoaDon();
        List<HDDATSAN> dshoadon = new List<HDDATSAN>();
        CtrlKhachHang CtrlKhachHang = new CtrlKhachHang();
        List<KHACHHANG> dsKH = new List<KHACHHANG>();
        Ctrl_CTHDSan ctrlcthd = new Ctrl_CTHDSan();
        List<CTHDDATSAN> dsctHD = new List<CTHDDATSAN>();
        CTHDDATSAN ct;
        int index;
        SAN san;
        HDDATSAN hoadon;
        KHACHHANG kh;
        string idLoaiSan = "";
        string idKH = "";
        string idHD = "";
        string idCTHD = "";

        public HomeNV()
        {
            InitializeComponent();
        }

        string taoIDHD()
        {
            return "HD" + DateTime.Now.ToString("mss");
        }

        private void HomeNV_Load(object sender, EventArgs e)
        {
            dsLoaiSan = CtrlLoaiSan.findAll();
            dsSan = CtrlSan.findAll();
            Load_LoaiSan();


            if (dsLoaiSan.Count > 0)
            {
                loaisan = dsLoaiSan[0];
            }

        }

        public void Load_San(string idLoaiSan)
        {
            if (idLoaiSan != null)
            {
                dsSan = CtrlSan.findByLoaiSan(idLoaiSan);
                var list = from san in dsSan
                           select new {san.IDSan,san.TenSan,san.MoTa,san.DonGia, san.TrangThai, san.TinhTrangHienTai};
                dtgvSan.DataSource = list.ToList();
                dtgvSan.Columns[0].HeaderText = "Mã Sân";
                dtgvSan.Columns[1].HeaderText = "Tên Sân";
                dtgvSan.Columns[2].HeaderText = "Mô Tả";
                dtgvSan.Columns[3].HeaderText = "Đơn Giá";
                dtgvSan.Columns[4].HeaderText = "Tình Trạng";
                dtgvSan.Columns[5].HeaderText = "Trạng thái";
                int chieudai = dtgvSan.Width;
                dtgvSan.Columns[0].Width = (int)(chieudai * 0.1);
                dtgvSan.Columns[1].Width = (int)(chieudai * 0.2);
                dtgvSan.Columns[2].Width = (int)(chieudai * 0.3);
                dtgvSan.Columns[3].Width = (int)(chieudai * 0.1);
                dtgvSan.Columns[4].Width = (int)(chieudai * 0.15);
                dtgvSan.Columns[5].Width = (int)(chieudai * 0.15);
            }
        }

        public void Load_HoaDon()
        {
            //List<HDDATSAN> dshoadon = ctrlhoadon.findAll(); // Lấy danh sách hóa đơn từ database
            //dtgvHoaDon.DataSource = dshoadon;
        }

        public void Load_LoaiSan()
        {
            var list1 = from b in dsLoaiSan
                        select b.TenLoaiSan;
            cbbLoaiSan.DataSource = list1.ToList();

            if(dsLoaiSan.Count > 0)
            {
                Load_San(idLoaiSan);
            }
            else { }
        }



        private void cbbLoaiSan_SelectedIndexChanged(object sender, EventArgs e)
        {
            index = cbbLoaiSan.SelectedIndex;
            if (index >= 0 && index < dsLoaiSan.Count)
            {
                loaisan = dsLoaiSan[index];
                idLoaiSan = loaisan.IDLoaiSan;
                Load_San(idLoaiSan);
            }
        }

        private void dtgvSan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dtgvSan.CurrentRow;
            index = row.Index;
            if (e.RowIndex < 0 || e.RowIndex >= dsSan.Count) return;
            san = dsSan[index];
            txtDatTenSan.Text = san.TenSan.ToString();
            txtDatMaSan.Text = san.IDSan.ToString();
            txtDatDonGia.Text = san.DonGia.ToString("N2");
        }



        private void dtgvSan_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnDatSan_Click(object sender, EventArgs e)
        {
            if (san.TinhTrangHienTai == "Đã đặt")
            {
                MessageBox.Show("Sân đã được đặt, không thể thực hiện đặt lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtDatTenKH.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtDatSDTKH.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại khách hàng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!decimal.TryParse(txtTienCoc.Text, out decimal tienCoc))
            {
                MessageBox.Show("Tiền cọc phải là một số hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime bd = dtpkBatDau.Value;
            DateTime kt = dtpkKetThuc.Value;
            TimeSpan tgbd = new TimeSpan(bd.Hour, bd.Minute, 0);
            TimeSpan tgkt = new TimeSpan(kt.Hour, kt.Minute, 0);
            TimeSpan tongTG = tgkt - tgbd;
            double soGio = tongTG.TotalHours;

            double donGia = double.Parse(txtDatDonGia.Text);

            double TinhTongTien = donGia * soGio;

            kh = new KHACHHANG();
            string IDKH = CtrlKhachHang.XacDinhKhachHang(txtDatTenKH.Text.Trim(), txtDatSDTKH.Text.Trim());

            if (!string.IsNullOrEmpty(IDKH))
            {
                // Khách hàng đã tồn tại
                idKH = IDKH;
            }
            else
            {
                // Khách hàng mới, tạo ID mới và thêm vào cơ sở dữ liệu
                string newIDKH = CtrlKhachHang.taoMaKhachHangMoi();
                string tenKH = txtDatTenKH.Text.Trim();
                string sdtKH = txtDatSDTKH.Text.Trim();

                // Thêm khách hàng mới vào cơ sở dữ liệu
                CtrlKhachHang.addKH(newIDKH, tenKH, sdtKH);
                idKH = newIDKH; // Gán ID mới cho biến idKH
            }
            idHD = ctrlhoadon.taoMaDonHang();
            string sdt = txtDatSDTKH.Text.Trim();
            hoadon = new HDDATSAN();
            hoadon.IDKhachHang = IDKH;
            hoadon.IDHDDatSan = idHD;
            hoadon.NgayLapHD = DateTime.Now;
            hoadon.IDHDDatSan = taoIDHD();
            hoadon.TienCoc = decimal.Parse(txtTienCoc.Text);
            hoadon.TrangThai = "Chưa thanh toán";
            hoadon.TongTien = decimal.Parse(TinhTongTien + "");
            ctrlhoadon.addHoaDon(hoadon);
            if (hoadon.IDHDDatSan == null)
            {
                MessageBox.Show("Lỗi: Hóa đơn không được thêm thành công.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //MessageBox.Show("Hóa đơn đã được thêm thành công!");
            if (san != null)
            {
                san.TinhTrangHienTai = "Đã đặt";
                CtrlSan.upDateTrangThaiSan(san.IDSan, san.TinhTrangHienTai);
                CtrlSan.upDateSan(san);
                dtgvSan.DataSource = null;
                Load_San(loaisan.IDLoaiSan);
            }
            idCTHD = ctrlcthd.TaoMaDonHang();
            ct = new CTHDDATSAN();
            ct.IDCTHDDatSan = idCTHD;
            ct.IDSan = san.IDSan;
            ct.IDHDDatSan = hoadon.IDHDDatSan;
            ct.IDSan = txtDatMaSan.Text;
            ct.NgayDat = DateTime.Now;
            ct.GioBatDau = dtpkBatDau.Value;
            ct.GioKetThuc = dtpkKetThuc.Value;
            ct.DonGia = decimal.Parse(donGia + "");
            ctrlcthd.addCTHD(ct);
            MessageBox.Show("Hóa đơn đã được thêm thành công!");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnHome_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dtgvSan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2PictureBox2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dtgvLoaiSan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }


        private void btnTimSan_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

       

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtDatIDKhachHang_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void guna2HtmlLabel6_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel12_Click(object sender, EventArgs e)
        {

        }

        private void txtTienCoc_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpkKetThuc_ValueChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel11_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel7_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpkBatDau_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnHuySan_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel10_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel9_Click(object sender, EventArgs e)
        {

        }

        private void txtDatMaSan_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel5_Click(object sender, EventArgs e)
        {

        }

        private void txtDatSDTKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDatTenKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel8_Click(object sender, EventArgs e)
        {

        }

        private void txtDatDonGia_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void txtDatTenSan_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void txtTimSan_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblLoaiSan_Click(object sender, EventArgs e)
        {

        }

        private void lblTimKiemSan_Click(object sender, EventArgs e)
        {

        }

        private void lblDsSan_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            HoaDonNV hd = new HoaDonNV();
            hd.Show();
            this.Hide();
        }

        private void btnTimSan_Click_1(object sender, EventArgs e)
        {

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
    }
}
