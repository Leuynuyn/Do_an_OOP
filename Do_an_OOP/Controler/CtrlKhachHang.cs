using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Do_an_OOP.Controller
{
    internal class CtrlKhachHang
    {
        public DateTime? NgaySinh { get; set; }

        public List<KHACHHANG> findAll()
        {
            return CUtils.db.KHACHHANGs.ToList();
        }

        public KHACHHANG timKhachHang(string ten, string sdt)
        {
            List<KHACHHANG> danhSachKH = this.findAll();

            return danhSachKH.FirstOrDefault(kh => kh.TenKhachHang == ten && kh.SDT == sdt);
        }

        public void addKH(string idKhachHang,string tenKhachHang, string sdt )
        {
            if (string.IsNullOrWhiteSpace(idKhachHang) ||string.IsNullOrWhiteSpace(tenKhachHang) ||string.IsNullOrWhiteSpace(sdt))
            {
                throw new ArgumentException("ID khách hàng, tên khách hàng và số điện thoại không được để trống.");
            }

            KHACHHANG kh = new KHACHHANG
            {
                IDKhachHang = idKhachHang,
                TenKhachHang = tenKhachHang,
                SDT = sdt,
                TenTaiKhoan = "TaiKhoanDefault",
                MatKhau = "1234567", 
                NgaySinh = null, 
                GioiTinh = "Nam", 
                Email = "email@default.com", 
                DiaChi = "Địa chỉ mặc định",
                VaiTro = "Khách hàng",
                NgayTaoTK = DateTime.Now 
            };

            CUtils.db.KHACHHANGs.Add(kh);
            CUtils.db.SaveChanges();


        }

        public string LayKHDauTien()
        {
            KHACHHANG firstKH = this.findAll().FirstOrDefault();
            return firstKH != null ? firstKH.IDKhachHang.ToString() : null;
        }

        public string taoMaKhachHangMoi()
        {
            string maKhachHangLonNhat = this.findAll()
                .Select(kh => kh.IDKhachHang)
                .OrderByDescending(id => id)
                .FirstOrDefault();
            if (string.IsNullOrEmpty(maKhachHangLonNhat))
            {
                return "KH001";
            }
            int soLonNhat = int.Parse(maKhachHangLonNhat.Substring(2));
            int maMoi = soLonNhat + 1;
            return "KH" + maMoi.ToString("D3"); 
        }

        public KHACHHANG login(string tendangnhap, string matkhau)
        {
            return CUtils.db.KHACHHANGs.FirstOrDefault(kh => kh.TenTaiKhoan == tendangnhap && kh.MatKhau == matkhau);
        }

        public KHACHHANG findByID(string idKH)
        {
            return CUtils.db.KHACHHANGs.FirstOrDefault(kh => kh.IDKhachHang == idKH);
        }

        public string XacDinhKhachHang(string tenKhachHang, string sdt)
        {
            KHACHHANG kh = timKhachHang(tenKhachHang, sdt);
            if (kh != null)
            {
                return kh.IDKhachHang;
            }
            else
            {
                string newID = taoMaKhachHangMoi();
                addKH(newID, tenKhachHang, sdt); // Lưu khách hàng mới vào cơ sở dữ liệu
                return newID; // Trả về ID mới
            }
        }

        //Uyen===================

        //tìm kiếm
        public List<KHACHHANG> search(string keyword)
        {
            if(string.IsNullOrEmpty(keyword))
                return findAll();
            return CUtils.db.KHACHHANGs.Where(kh => kh.IDKhachHang == (keyword) || kh.TenKhachHang == (keyword) || kh.TenTaiKhoan == (keyword) 
            || kh.DiaChi == (keyword) || kh.GioiTinh == (keyword) || kh.Email == (keyword) || kh.SDT == (keyword)).ToList();
        }

        public List<KHACHHANG> searchDate(DateTime date)
        {
            return CUtils.db.KHACHHANGs.Where(kh => kh.NgayTaoTK == (date) || kh.NgaySinh == (date)).ToList();
        }

        public void add(KHACHHANG KhachHang)
        {
            CUtils.db.KHACHHANGs.Add(KhachHang);
            CUtils.db.SaveChanges();
        }

        public void remove(KHACHHANG KhachHang)
        {
            CUtils.db.KHACHHANGs.Remove(KhachHang);
            CUtils.db.SaveChanges();
        }

        public void upDate(KHACHHANG KhachHang)
        {
            CUtils.db.SaveChanges();
        }
    }

}
