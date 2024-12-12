using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Do_an_OOP.Controler
{
    internal class Ctrl_NhanVien
    {
        //truy vấn tất cả thông tin nhân viên
        public List<NHANVIEN> findAll()
        {
            return CUtils.db.NHANVIENs.ToList();
        }

        //tìm kiếm nv bằng thông tin
        public List<NHANVIEN> search(string keyword)
        {
            //trống thì trả về toàn bộ ds
            if(string.IsNullOrEmpty(keyword))
                return findAll();
            return CUtils.db.NHANVIENs.Where(nv => nv.IDNhanVien == (keyword) || nv.TenNhanVien == (keyword) || nv.TenTaiKhoan == (keyword) || nv.VaiTro == (keyword)
            || nv.DiaChi == (keyword) || nv.GioiTinh == (keyword) || nv.Email == (keyword) || nv.SDT == (keyword)) .ToList();
        }

        //tim kiem bằng ngày vào làm or ngày sinh
        public List<NHANVIEN> searchDateWork(DateTime date)
        {
            return CUtils.db.NHANVIENs.Where(nv => nv.NgayVaoLam == (date) || nv.NgaySinh == (date)).ToList();
        }

        //them thông tin nv mới
        public void add(NHANVIEN NhanVien)
        {
            CUtils.db.NHANVIENs.Add(NhanVien);
            CUtils.db.SaveChanges();
        }

        //Xoa NV
        public void remove(NHANVIEN NhanVien)
        {
            CUtils.db.NHANVIENs.Remove(NhanVien);
            CUtils.db.SaveChanges();
        }

        //sua
        public void upDate(NHANVIEN NhanVien)
        {
            CUtils.db.SaveChanges();
        }
        public NHANVIEN Login(string tenDangNhap, string matKhau)
        {
            return CUtils.db.NHANVIENs.FirstOrDefault(nv => nv.TenTaiKhoan == tenDangNhap && nv.MatKhau == matKhau);
        }

    }
}
