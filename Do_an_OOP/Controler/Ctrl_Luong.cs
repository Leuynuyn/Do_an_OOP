using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Do_an_OOP.Controler
{
    internal class Ctrl_Luong
    {
        public List<TINHLUONG> findAll()
        {
            return CUtils.db.TINHLUONGs.ToList();
        }

        public void add(TINHLUONG luong)
        {
            CUtils.db.TINHLUONGs.Add(luong);
            CUtils.db.SaveChanges();
        }

    }
}
