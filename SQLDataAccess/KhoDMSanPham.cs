//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SQLDataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class KhoDMSanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhoDMSanPham()
        {
            this.KhoGiaoDichChiTiets = new HashSet<KhoGiaoDichChiTiet>();
            this.KhoTonKhoes = new HashSet<KhoTonKho>();
        }
    
        public System.Guid Id { get; set; }
        public string Ma { get; set; }
        public string TenSanPham { get; set; }
        public System.Guid IdDonVi { get; set; }
        public string GhiChu { get; set; }
    
        public virtual TuDien TuDien { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhoGiaoDichChiTiet> KhoGiaoDichChiTiets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhoTonKho> KhoTonKhoes { get; set; }
    }
}
