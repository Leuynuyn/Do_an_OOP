using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Do_an_OOP.Controler
{
    internal class Ctrl_QTV
    {
        public List<QUANTRIVIEN> findAll()
        {
            return CUtils.db.QUANTRIVIENs.ToList();
        }

        public QUANTRIVIEN Login(string tenDangNhap, string matKhau)
        {
            return CUtils.db.QUANTRIVIENs.FirstOrDefault(qtv => qtv.TenTaiKhoan == tenDangNhap && qtv.MatKhau == matKhau);
        }

        //public QUANTRIVIEN findByTenTaiKhoan(string tenTaiKhoan)
        //{
        //    var quanTriVien = (from qtv in CUtils.db.QUANTRIVIENs
        //                       where qtv.TenTaiKhoan == tenTaiKhoan
        //                       select qtv).FirstOrDefault();
        //    return quanTriVien;
        //}
    }
}
