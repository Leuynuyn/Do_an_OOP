using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Do_an_OOP.Controler
{
    internal class Ctrl_LoaiSan
    {
        //truy vấn tát cả dữ liệu
        public List<LOAISAN> findAll()
        {
            return CUtils.db.LOAISANs.ToList();
        }

        public List<LOAISAN> search(string keyword)
        {
            //trống thì trả về toàn bộ ds
            if (string.IsNullOrEmpty(keyword))
                return findAll();
            return CUtils.db.LOAISANs.Where(ls => ls.IDLoaiSan == (keyword) || ls.TenLoaiSan == (keyword) || ls.MoTa == (keyword)).ToList();
        }

        public void add(LOAISAN LoaiSan)
        {
            CUtils.db.LOAISANs.Add(LoaiSan);
            CUtils.db.SaveChanges();
        }
        
        public void update(LOAISAN LoaiSan)
        {
            CUtils.db.SaveChanges();
        }

        public void remove(LOAISAN LoaiSan)
        {
            CUtils.db.LOAISANs.Remove(LoaiSan);
            CUtils.db.SaveChanges();
        }
    }
}
