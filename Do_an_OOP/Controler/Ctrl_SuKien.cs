using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Do_an_OOP.Controler
{
    internal class Ctrl_SuKien
    {
        public List<SUKIENKHUYENMAI> findAll()
        {
            return CUtils.db.SUKIENKHUYENMAIs.ToList();
        }

        public void add(SUKIENKHUYENMAI sukien)
        {
            CUtils.db.SUKIENKHUYENMAIs.Add(sukien);
            CUtils.db.SaveChanges();
        }

    }
}
