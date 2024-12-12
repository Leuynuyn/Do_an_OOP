using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Do_an_OOP.Controler
{
    internal class Ctrl_CaLamViec
    {
        public List<CALAMVIEC> findAll()
        {
            return CUtils.db.CALAMVIECs.ToList();
        }

        public void add(CALAMVIEC canlamviec)
        {
            CUtils.db.CALAMVIECs.Add(canlamviec);
            CUtils.db.SaveChanges();
        }

        public void remove(CALAMVIEC calamviec)
        {
            CUtils.db.CALAMVIECs.Remove(calamviec);
            CUtils.db.SaveChanges();
        }
        public void update(CALAMVIEC calamviec)
        {
            CUtils.db.SaveChanges();
        }

        public List<CALAMVIEC> search(string key)
        {
            if (string.IsNullOrEmpty(key))
                return findAll();
            return CUtils.db.CALAMVIECs.Where(ca => ca.IDCaLamViec == (key)).ToList();
        }
    }
}
