//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Do_an_OOP
{
    using System;
    using System.Collections.Generic;
    
    public partial class TINHLUONG
    {
        public string IDLuong { get; set; }
        public string IDNhanVien { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public Nullable<int> TongSoGio { get; set; }
        public Nullable<decimal> TongLuong { get; set; }
    
        public virtual NHANVIEN NHANVIEN { get; set; }
    }
}
