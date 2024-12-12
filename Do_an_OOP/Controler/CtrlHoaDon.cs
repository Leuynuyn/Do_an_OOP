using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Do_an_OOP.Controller
{
    internal class CtrlHoaDon
    {
        public List<HDDATSAN> findAll()
        {
            return CUtils.db.HDDATSANs.ToList();
        }

        public void addHoaDon(HDDATSAN hd)
        {
            if (hd == null)
                throw new ArgumentNullException(nameof(hd), "Hóa đơn không thể null.");
            if (string.IsNullOrWhiteSpace(hd.IDHDDatSan))
                throw new ArgumentException("ID hóa đơn không thể trống.", nameof(hd.IDHDDatSan));
            if (hd.TienCoc < 0)
                throw new ArgumentException("Tiền cọc không thể âm.", nameof(hd.TienCoc));

            CUtils.db.HDDATSANs.Add(hd);
            CUtils.db.SaveChanges();
         
        }
        public string taoMaDonHang()
        {
            string maDonHangMax = this.findAll()
                .Select(dh => dh.IDHDDatSan)
                .OrderByDescending(id => id)
                .FirstOrDefault();
            if (string.IsNullOrEmpty(maDonHangMax))
            {
                return "HD001";
            }
            int soLonNhat = int.Parse(maDonHangMax.Substring(2));
            int maMoi = soLonNhat + 1;
            return "HD" + maMoi.ToString("D3");
        }
        public void update(HDDATSAN hd)
        {
            CUtils.db.SaveChanges();
        }


        public void updateTrangThaiHoaDon(string idHoaDon, string trangThaiHoaDon)
        {
            var hoaDon = CUtils.db.HDDATSANs.FirstOrDefault(hd => hd.IDHDDatSan == idHoaDon);
            if (hoaDon != null)
            {
                hoaDon.TrangThai = trangThaiHoaDon;
                CUtils.db.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Hóa đơn không tồn tại.", nameof(idHoaDon));
            }
        }

    }
}
