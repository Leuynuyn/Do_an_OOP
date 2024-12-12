using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Do_an_OOP.Controler
{
    internal class Ctrl_CTHDSan
    {
        //public List<CTHDDATSAN> findAll()
        //{
        //    return CUtils.db.CTHDDATSANs.ToList();
        //}
        public List<CTHDDATSAN> TimKiemHD(string tuKhoa)
        {
            return CUtils.db.CTHDDATSANs.Where(ct => ct.IDHDDatSan.Contains(tuKhoa) || ct.HDDATSAN.KHACHHANG.TenKhachHang.Contains(tuKhoa) || ct.SAN.TenSan.Contains(tuKhoa)).ToList();
        }

        public List<CTHDDATSAN> findAll()
        {
            return CUtils.db.CTHDDATSANs.ToList();
        }

        public void addCTHD(CTHDDATSAN ct)
        {
            CUtils.db.CTHDDATSANs.Add(ct);
            CUtils.db.SaveChanges();
        }

        public List<CTHDDATSAN> findByTenSan(string TenSan)
        {
            return CUtils.db.CTHDDATSANs.Where(s => s.SAN.TenSan == TenSan).ToList();
        }
        public List<CTHDDATSAN> findByTenLoaiSan(string TenLoaiSan)
        {
            return CUtils.db.CTHDDATSANs.Where(s => s.SAN.LOAISAN.TenLoaiSan == TenLoaiSan).ToList();
        }

        public void upDate(CTHDDATSAN ct)
        {
            CUtils.db.SaveChanges();
        }
        public string TaoMaDonHang()
        {
            string maDonHangMax = findAll()
                .Select(cthd => cthd.IDCTHDDatSan)
                .OrderByDescending(id => id)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(maDonHangMax))
            {
                return "CT001";
            }

            int soLonNhat = int.Parse(maDonHangMax.Substring(3));
            int maMoi = soLonNhat + 1;
            return "CT" + maMoi.ToString("D3");
        }


    }
}
