using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Do_an_OOP.Controler
{
    internal class Ctrl_San
    {
        //truy vấn tất cả thông tin
        public List<SAN> findAll()
        {
            return CUtils.db.SANs.ToList();
        }

        //truy vấn theo loại sân
        public List<SAN> findByLoaiSan(string loaisan)
        {
            return CUtils.db.SANs.Where(s => s.IDLoaiSan == loaisan).ToList();
        }

        //tìm kiếm san bằng thông tin
        public List<SAN> search(string keyword)
        {
            //trống thì trả về toàn bộ ds
            if (string.IsNullOrEmpty(keyword))
                return findAll();
            return CUtils.db.SANs.Where(s => s.IDSan == (keyword) || s.TenSan == (keyword) || s.DonGia.ToString() == (keyword) || s.MoTa == (keyword) || s.TrangThai == (keyword)).ToList();
        }

        //them thông tin nv mới
        public void add(SAN san)
        {
            CUtils.db.SANs.Add(san);
            CUtils.db.SaveChanges();
        }

        //sua 
        public void update(SAN san)
        {
            CUtils.db.SaveChanges();
        }

        public List<CTHDDATSAN> findCTHDSan()
        {
            return CUtils.db.CTHDDATSANs.ToList();
        }

        public void upDateTrangThaiSan(string idSan, string trangthaiSan)
        {
            var san = CUtils.db.SANs.FirstOrDefault(s => s.IDSan == idSan);
            if (san != null)
            {
                san.TinhTrangHienTai = trangthaiSan;
            }
        }

        public void upDateSan(SAN san)
        {
            CUtils.db.SaveChanges();

        }

        public List<SAN> findall()
        {
            return CUtils.db.SANs.ToList();
        }

        public List<SAN> timKiemSan(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return findall();
            }
            return CUtils.db.SANs.Where(s => s.IDSan.Contains(keyword) || s.TenSan.Contains(keyword)).ToList();
        }

        public List<LOAISAN> getAllLoaiSan()
        {
            return CUtils.db.LOAISANs.ToList();
        }

        public List<LOAISAN> getSanByLoaiSan(string idLoaiSan)
        {
            return CUtils.db.LOAISANs.Where(s => s.IDLoaiSan == idLoaiSan).ToList();
        }
        public List<LOAISAN> Findall()
        {
            return CUtils.db.LOAISANs.ToList();
        }


    }
}
