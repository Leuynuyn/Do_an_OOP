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
    
    public partial class LSPFOOD
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LSPFOOD()
        {
            this.SPFOODs = new HashSet<SPFOOD>();
        }
    
        public string IDLoaiSP { get; set; }
        public string TenLoaiSP { get; set; }
        public string MoTa { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPFOOD> SPFOODs { get; set; }
    }
}
