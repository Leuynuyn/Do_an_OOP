using Do_an_OOP.Controler;
using Do_an_OOP.Controller;
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

namespace Do_an_OOP.View
{
    public partial class FDatSanBR : Form
    {
        List<SAN> dssan = new List<SAN>();
        List<LOAISAN> dslsan = new List<LOAISAN>();
        List<KHACHHANG> dskh = new List<KHACHHANG>();
        List<CTHDDATSAN> dscthddatsan = new List<CTHDDATSAN>();
        List<LOAISAN> dsls = new List<LOAISAN>();
        List<SUKIENKHUYENMAI> dsskkm = new List<SUKIENKHUYENMAI>();
        Ctrl_San ctrlDatSan = new Ctrl_San();
        Ctrl_SuKien ctrlSK = new Ctrl_SuKien();
        CTHDDATSAN cthdds = new CTHDDATSAN();
        HDDATSAN hdds = new HDDATSAN();

        public FDatSanBR()
        {
            InitializeComponent();
            loadTTSanDaDat();
            loadTenSan();
            loadTinhTrangHienTai();
            loadLoaiSan();
            LoadGiobatDau();
            LoadGioKetThuc();
            loadTTKM();
        }

        private void ctrlb_Click(object sender, EventArgs e)
        {
            Close();
        }


        public void loadTTSanDaDat()
        {
            dssan = ctrlDatSan.findall();
            dscthddatsan = ctrlDatSan.findCTHDSan();
            dtgvTTSanDaDat.ClearSelection();
            // Kiểm tra dsSan và dsCTHDSan không null
            if (dssan == null || dscthddatsan == null)
            {
                MessageBox.Show("Danh sách sân hoặc chi tiết hóa đơn đặt sân chưa được nạp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DateTime ngayKiemTra = dtpKTraNgayDaDat.Value.Date;
            var list = from san in dssan
                       let cthd = dscthddatsan.FirstOrDefault(ct => ct.IDSan == san.IDSan && ct.NgayDat.Date == ngayKiemTra)
                       select new
                       {
                           san.IDSan,
                           san.TenSan,
                           san.DonGia,
                           san.MoTa,
                           san.TrangThai,
                           TinhTrangHienTai = cthd != null ? "Đã đặt" : "Trống",
                           NgayDat = cthd != null ? cthd.NgayDat.ToString("dd/MM/yyyy") : "Chưa đặt",
                           GioBatDau = cthd != null ? cthd.GioBatDau.ToString("HH:mm") : "Chưa đặt",
                           GioKetThuc = cthd != null ? cthd.GioKetThuc.ToString("HH:mm") : "Chưa đặt"
                       };
            dtgvTTSanDaDat.DataSource = list.ToList();
            dtgvTTSanDaDat.Columns["TenSan"].HeaderText = "Tên sân";
            dtgvTTSanDaDat.Columns["DonGia"].HeaderText = "Đơn giá";
            dtgvTTSanDaDat.Columns["MoTa"].HeaderText = "Mô tả";
            dtgvTTSanDaDat.Columns["TrangThai"].HeaderText = "Trạng thái";
            dtgvTTSanDaDat.Columns["TinhTrangHienTai"].HeaderText = "Tình trạng";
            dtgvTTSanDaDat.Columns["NgayDat"].HeaderText = "Ngày Đặt";
            dtgvTTSanDaDat.Columns["GioBatDau"].HeaderText = "Giờ bắt đầu";
            dtgvTTSanDaDat.Columns["GioKetThuc"].HeaderText = "Giờ kết thúc";

            //điều chỉnh kích thước các cột
            dtgvTTSanDaDat.Columns["IDSan"].Width = 55;
            dtgvTTSanDaDat.Columns["TenSan"].Width = 120;
            dtgvTTSanDaDat.Columns["DonGia"].Width = 100;
            dtgvTTSanDaDat.Columns["MoTa"].Width = 180;
            dtgvTTSanDaDat.Columns["TrangThai"].Width = 110;
            dtgvTTSanDaDat.Columns["TinhTrangHienTai"].Width = 110;
            dtgvTTSanDaDat.Columns["NgayDat"].Width = 110;
            dtgvTTSanDaDat.Columns["GioBatDau"].Width = 100;
            dtgvTTSanDaDat.Columns["GioKetThuc"].Width = 100;
        }

        public void dtpKTraNgayDaDat_ValueChanged(object sender, EventArgs e)
        {

            loadTTSanDaDat();
        }

        public void btnTimKiem_Click(object sender, EventArgs e)
        {
            //Lấy giá trị từ cmb
            string tenSan = cmbTenSan.SelectedItem?.ToString();
            string tinhTrangHT = cmbTTHienTai.SelectedItem?.ToString();

            //Lọc danh sách sân
            var filterList = dssan.Select(san =>
            {
                var cthd = dscthddatsan.FirstOrDefault(ct => ct.IDSan == san.IDSan);
                return new
                {
                    san.IDSan,
                    san.TenSan,
                    san.DonGia,
                    san.MoTa,
                    san.TrangThai,
                    TinhTrangHienTai = cthd != null ? "Đã đặt" : "Trống",
                    NgayDat = cthd?.NgayDat.ToString("dd/MM/yyyy") ?? "Chưa đặt",
                    GioBatDau = cthd?.GioBatDau.ToString("HH:mm") ?? "Chưa đặt",
                    GioKetThuc = cthd?.GioKetThuc.ToString("HH:mm") ?? "Chưa đặt"
                };
            }).ToList();

            //Lọc giá trị của combobox
            if (!string.IsNullOrEmpty(tenSan))
            {
                filterList = filterList.Where(s => s.TenSan == tenSan).ToList();
            }

            if (!string.IsNullOrEmpty(tinhTrangHT))
            {
                if (tinhTrangHT == "Trống")
                {
                    filterList = filterList.Where(ct => ct.TinhTrangHienTai == "Trống").ToList();
                }

                else if (tinhTrangHT == "Đã đặt")
                {
                    filterList = filterList.Where(ct => ct.TinhTrangHienTai == "Đã đặt").ToList();
                }
            }

            //Cập nhật dtgr
            dtgvTTSanDaDat.DataSource = filterList;
        }

        public void loadTenSan()
        {
            cmbTenSan.Items.Clear(); //xóa các phần tử đã hiện trước đó
            foreach (var san in dssan)
            {
                cmbTenSan.Items.Add(san.TenSan);
                cmbChonTenSan.Items.Add(san.TenSan);
            }
        }

        public void loadTinhTrangHienTai()
        {
            cmbTTHienTai.Items.Clear();
            cmbTTHienTai.Items.Add("Đã đặt");
            cmbTTHienTai.Items.Add("Trống");
        }

        public void btnReset_Click(object sender, EventArgs e)
        {
            var allList = dssan.Select(san =>
            {
                var cthd = dscthddatsan.FirstOrDefault(ct => ct.IDSan == san.IDSan);
                return new
                {
                    san.IDSan,
                    san.TenSan,
                    san.DonGia,
                    san.MoTa,
                    san.TrangThai,
                    TinhTrangHT = cthd != null ? "Đã đặt" : "Trống",
                    NgayDat = cthd?.NgayDat.ToString("dd/MM/yyyy"),
                    GioBatDau = cthd?.GioBatDau.ToString("HH:mm"),
                    GioKetThuc = cthd?.GioKetThuc.ToString("HH:mm")
                };
            }).ToList();

            dtgvTTSanDaDat.DataSource = allList;

            //bỏ chọn trong cmb
            cmbTenSan.SelectedItem = null;
            cmbTTHienTai.SelectedItem = null;
        }

        public void dtgvTTSanDaDat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dtgvTTSanDaDat.CurrentRow;

            int index = row?.Index ?? -1;

            if (index >= 0)
            {
                var san = dssan[index];

                txtDoiChieuMaSan.Text = san.IDSan;
                txtDoiChieuTenSan.Text = san.TenSan;

                var cthd = dscthddatsan.FirstOrDefault(ct => ct.IDSan == san.IDSan);

                txtDoiChieuTTHT.Text = cthd != null ? "Đã đặt" : "Trống";

                txtDoiChieuNgayDat.Text = cthd?.NgayDat.ToString("dd/MM/yyyy") ?? "Chưa đặt";
                txtDoiChieuTGBD.Text = cthd?.GioBatDau.ToString("HH:mm") ?? "Chưa đặt";
                txtDoiChieuTGKT.Text = cthd?.GioKetThuc.ToString("HH:mm") ?? "Chưa đặt";
            }

            //Chuyển thành chế chỉ được phép đọc
            txtDoiChieuMaSan.ReadOnly = true;
            txtDoiChieuTenSan.ReadOnly = true;
            txtDoiChieuTTHT.ReadOnly = true;
            txtDoiChieuNgayDat.ReadOnly = true;
            txtDoiChieuTGBD.ReadOnly = true;
            txtDoiChieuTGKT.ReadOnly = true;
        }

        public void loadLoaiSan()
        {
            dsls = ctrlDatSan.Findall();
            cmbChonLoaiSan.Items.Clear();
            foreach (var ls in dsls)
            {
                cmbChonLoaiSan.Items.Add(ls.TenLoaiSan);
            }
        }

        public void cmbChonLoaiSan_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu không có mục nào được chọn
            if (cmbChonLoaiSan.SelectedItem == null)
            {
                return;
            }

            try
            {
                // Lấy tên loại sân đã chọn từ ComboBox
                string selectedLoaiSan = cmbChonLoaiSan.SelectedItem.ToString();

                // Tìm IDLoaiSan từ tên loại sân đã chọn
                var loaiSan = dsls.FirstOrDefault(ls => ls.TenLoaiSan == selectedLoaiSan);

                if (loaiSan != null)
                {
                    // Lọc danh sách các sân theo IDLoaiSan của loại sân đã chọn
                    var filteredTenSan = dssan.Where(s => s.IDLoaiSan == loaiSan.IDLoaiSan)
                                              .Select(s => s.TenSan)
                                              .ToList();

                    // Cập nhật ComboBox tên sân
                    cmbChonTenSan.Items.Clear();
                    cmbChonTenSan.Items.AddRange(filteredTenSan.ToArray());

                    // Bỏ chọn mặc định
                    cmbChonTenSan.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadGiobatDau()
        {
            cmbChonGioBD.Items.Clear();
            for (int i = 7; i <= 22; i++)
            {
                cmbChonGioBD.Items.Add(i.ToString("D2") + ":00");
            }
            cmbChonGioBD.SelectedIndex = 0; // Chọn mặc định giờ bắt đầu là 7:00
        }

        public void LoadGioKetThuc()
        {
            for (int i = 8; i <= 22; i++)
            {
                cmbChonGioKT.Items.Add(i.ToString("D2") + ":00"); // Tương tự như giờ bắt đầu
            }
            cmbChonGioKT.SelectedIndex = 0; // Chọn mặc định giờ 8h
        }

        public void cmbChonGioBD_SelectedIndexChanged(object sender, EventArgs e)
        {
            int gioBatDau = int.Parse(cmbChonGioBD.SelectedItem.ToString().Substring(0, 2)); // Lấy giờ từ giờ bắt đầu

            // Xóa tất cả các mục trong combo box giờ kết thúc
            cmbChonGioKT.Items.Clear();

            // Thêm giờ kết thúc từ giờ bắt đầu + 1 trở lên
            for (int i = gioBatDau + 1; i <= 22; i++)
            {
                cmbChonGioKT.Items.Add(i.ToString("D2") + ":00");
            }

            // Chọn giờ kết thúc mặc định là giờ bắt đầu + 1
            cmbChonGioKT.SelectedIndex = 0;

            TinhTienTheoGio();
        }

        public void cmbChonGioKT_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu không có giá trị được chọn
            if (cmbChonGioBD.SelectedItem == null || cmbChonGioKT.SelectedItem == null)
            {
                return; // Nếu chưa chọn giờ bắt đầu hoặc giờ kết thúc thì không làm gì
            }

            // Lấy giờ từ giờ bắt đầu và giờ kết thúc
            int gioBatDau = int.Parse(cmbChonGioBD.SelectedItem.ToString().Substring(0, 2)); // Lấy giờ từ giờ bắt đầu
            int gioKetThuc = int.Parse(cmbChonGioKT.SelectedItem.ToString().Substring(0, 2)); // Lấy giờ từ giờ kết thúc

            // Kiểm tra nếu giờ kết thúc bé hơn giờ bắt đầu
            if (gioKetThuc <= gioBatDau)
            {
                // Hiển thị thông báo lỗi
                MessageBox.Show("Giờ kết thúc không thể bé hơn hoặc bằng giờ bắt đầu. \nVui lòng chọn lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Đưa ComboBox giờ kết thúc về lại giờ hợp lệ (lớn hơn giờ bắt đầu)
                cmbChonGioKT.SelectedIndex = -1; // Deselect current selection
                cmbChonGioKT.SelectedIndex = gioBatDau; // Set selected to next valid hour
            }
            TinhTienTheoGio();
        }

        public void loadTTKM()
        {
            // Lấy danh sách sự kiện khuyến mãi
            dsskkm = ctrlSK.findAll();

            // Thêm tên sự kiện vào ComboBox
            cmbMaKM.Items.Clear();
            foreach (var suKien in dsskkm)
            {
                // Kiểm tra sự kiện còn trong thời gian sử dụng hay không
                if (suKien.NgayKetThucSK >= DateTime.Now)
                {
                    //string tenSuKien = Encoding.UTF8.GetString(Encoding.Default.GetBytes(suKien.TenSuKien));
                    cmbMaKM.Items.Add(suKien.TenSuKien);
                }
            }
        }
        public void cmbMaKM_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu không có sự kiện được chọn
            if (cmbMaKM.SelectedItem == null)
            {
                return; // Thoát nếu không có sự kiện
            }

            // Lấy tên sự kiện đã chọn
            string tenSuKien = cmbMaKM.SelectedItem.ToString();

            // Tìm sự kiện khuyến mãi tương ứng từ danh sách
            var suKien = dsskkm.FirstOrDefault(s => s.TenSuKien == tenSuKien);

            if (suKien != null)
            {
                // Áp dụng giảm giá vào tổng tiền và tiền cọc sân
                decimal giaTriGiam = suKien.GiaTriGiam;

                // Cập nhật tổng tiền và tiền cọc
                CapNhatTinhTienMaKM(giaTriGiam);
            }
        }
        public void TinhTienTheoGio()
        {
            // Kiểm tra nếu ComboBox chưa được chọn
            if (cmbChonGioBD.SelectedItem == null || cmbChonGioKT.SelectedItem == null || cmbChonTenSan.SelectedItem == null)
            {
                return; // Không làm gì cả nếu chưa chọn đủ
            }

            try
            {
                // Lấy giờ bắt đầu và giờ kết thúc từ ComboBox
                int gioBatDau = int.Parse(cmbChonGioBD.SelectedItem.ToString().Substring(0, 2));
                int gioKetThuc = int.Parse(cmbChonGioKT.SelectedItem.ToString().Substring(0, 2));

                // Lấy sân được chọn từ ComboBox
                string tenSan = cmbChonTenSan.SelectedItem.ToString();

                // Tìm đơn giá của sân được chọn từ danh sách dssan
                var sanDuocChon = dssan.FirstOrDefault(s => s.TenSan == tenSan);
                if (sanDuocChon == null)
                {
                    MessageBox.Show("Không tìm thấy sân được chọn trong danh sách!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tính tổng tiền và tiền cọc sân
                int soGio = gioKetThuc - gioBatDau;
                decimal tongTien = soGio * sanDuocChon.DonGia;
                decimal tienCocSan = tongTien / 3;

                // Hiển thị kết quả lên TextBox
                txtTongTien.Text = tongTien.ToString("N0") + " VNĐ"; // Hiển thị dạng số có dấu phân cách
                txtTienCocSan.Text = tienCocSan.ToString("N0") + " VNĐ";
            }
            catch (Exception ex)
            {
                // Bắt lỗi không xác định
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            txtTongTien.ReadOnly = true;
            txtTienCocSan.ReadOnly = true;
        }

        public void CapNhatTinhTienMaKM(decimal giaTriGiam)
        {
            // Kiểm tra nếu ComboBox chưa được chọn
            if (cmbChonGioBD.SelectedItem == null || cmbChonGioKT.SelectedItem == null || cmbChonTenSan.SelectedItem == null)
            {
                return; // Không làm gì cả nếu chưa chọn đủ
            }

            try
            {
                // Lấy giờ bắt đầu và giờ kết thúc từ ComboBox
                int gioBatDau = int.Parse(cmbChonGioBD.SelectedItem.ToString().Substring(0, 2));
                int gioKetThuc = int.Parse(cmbChonGioKT.SelectedItem.ToString().Substring(0, 2));

                // Lấy sân được chọn từ ComboBox
                string tenSan = cmbChonTenSan.SelectedItem.ToString();

                // Tìm đơn giá của sân được chọn từ danh sách dssan
                var sanDuocChon = dssan.FirstOrDefault(s => s.TenSan == tenSan);
                if (sanDuocChon == null)
                {
                    MessageBox.Show("Không tìm thấy sân được chọn trong danh sách!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tính tổng tiền và tiền cọc sân
                int soGio = gioKetThuc - gioBatDau;
                decimal tongTien = soGio * sanDuocChon.DonGia;
                decimal tienCocSan = tongTien / 3;

                // Áp dụng giảm giá vào tổng tiền và tiền cọc sân
                tongTien -= giaTriGiam;
                tienCocSan -= giaTriGiam;

                // Cập nhật lại vào TextBox
                txtTongTien.Text = tongTien.ToString("N0") + " VNĐ";
                txtTienCocSan.Text = tienCocSan.ToString("N0") + " VNĐ";
            }
            catch (Exception ex)
            {
                // Bắt lỗi không xác định
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            txtTongTien.ReadOnly = true;
            txtTienCocSan.ReadOnly = true;
        }

        public void btnResetChonSan_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạm thời tắt sự kiện để tránh kích hoạt không mong muốn
                cmbChonLoaiSan.SelectedIndexChanged -= cmbChonLoaiSan_SelectedIndexChanged;
                cmbChonTenSan.SelectedIndexChanged -= cmbChonTenSan_SelectedIndexChanged;

                // Xóa giá trị đã chọn
                cmbChonLoaiSan.SelectedIndex = -1;
                cmbChonTenSan.SelectedIndex = -1;

                // Cập nhật lại danh sách đầy đủ cho các ComboBox
                cmbChonLoaiSan.Items.Clear();
                cmbChonLoaiSan.Items.AddRange(dsls.Select(ls => ls.TenLoaiSan).ToArray());

                cmbChonTenSan.Items.Clear();
                cmbChonTenSan.Items.AddRange(dssan.Select(s => s.TenSan).ToArray());

                // Làm sạch các ô nhập liệu
                txtTongTien.Clear();
                txtTienCocSan.Clear();

                // Tính lại tiền sau khi reset (Gọi hàm tính tiền theo giờ)
                TinhTienTheoGio();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi, nếu có
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Bật lại sự kiện sau khi hoàn tất reset
                cmbChonLoaiSan.SelectedIndexChanged += cmbChonLoaiSan_SelectedIndexChanged;
                cmbChonTenSan.SelectedIndexChanged += cmbChonTenSan_SelectedIndexChanged;
            }
        }

        public void cmbChonTenSan_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu không có sân được chọn
            if (cmbChonTenSan.SelectedItem == null)
            {
                return;
            }

            try
            {
                // Lấy tên sân đã chọn
                string tenSan = cmbChonTenSan.SelectedItem.ToString();

                // Lấy sân tương ứng với tên sân đã chọn
                var sanDuocChon = dssan.FirstOrDefault(s => s.TenSan == tenSan);
                if (sanDuocChon != null)
                {
                    // Cập nhật loại sân dựa trên tên sân
                    var loaiSan = dsls.FirstOrDefault(ls => ls.IDLoaiSan == sanDuocChon.IDLoaiSan);
                    if (loaiSan != null)
                    {
                        cmbChonLoaiSan.SelectedItem = loaiSan.TenLoaiSan;
                    }
                }

                // Tính lại tổng tiền và tiền cọc
                TinhTienTheoGio();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void btnXacNhanDatSan_Click(object sender, EventArgs e)
        {
            // Lấy thông tin sân và thời gian đã chọn
            string tenSan = cmbChonTenSan.SelectedItem.ToString();
            DateTime ngayDat = DateTime.Parse(dtpChonNgayDat.Value.ToString("yyyy-MM-dd"));
            int gioBD = int.Parse(cmbChonGioBD.SelectedItem.ToString().Substring(0, 2));
            int gioKT = int.Parse(cmbChonGioKT.SelectedItem.ToString().Substring(0, 2));

            // Kiểm tra xem có đơn đặt sân nào trùng với thời gian và sân đã chọn không
            var datSanTrung = dscthddatsan.FirstOrDefault(ds => ds.NgayDat.Date == ngayDat.Date // So sánh ngày
                                               && ds.GioBatDau.Hour < gioKT // So sánh giờ bắt đầu của sân đã đặt với giờ kết thúc
                                               && ds.GioKetThuc.Hour > gioBD);

            if (datSanTrung != null)
            {
                // Nếu có trùng lịch
                MessageBox.Show("Sân đã được đặt trong khoảng thời gian này. Vui lòng chọn lại khung giờ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Nếu không trùng, hiển thị thông báo xác nhận
                DialogResult dialogResult = MessageBox.Show("\t\tKhông có trùng lịch \nBạn cần tiến hành thanh toán tiền cọc để hoàn tất đặt sân?",
                                                            "Xác nhận",
                                                            MessageBoxButtons.YesNo,
                                                            MessageBoxIcon.Information);

                if (dialogResult == DialogResult.Yes)
                {
                    // Mở form Thanh Toán
                    FHDDatSan fHD = new FHDDatSan();
                    this.Hide(); // Ẩn form hiện tại nếu cần
                    fHD.Show();
                    Close();
                }
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            FHomeKH me = new FHomeKH();
            this.Hide();
            me.ShowDialog();
            this.Show();
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            FTKKH me = new FTKKH();
            this.Hide();
            me.ShowDialog();
            this.Show();
        }
    }
}
