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
    
    public partial class KhoGiaoDich
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhoGiaoDich()
        {
            this.KhoGiaoDichChiTiets = new HashSet<KhoGiaoDichChiTiet>();
        }
    
        public System.Guid Id { get; set; }
        public int LoaiGiaoDich { get; set; }
        public System.DateTime NgayTao { get; set; }
        public System.Guid IdNguoiTao { get; set; }
        public bool Active { get; set; }
        public string GhiChu { get; set; }
        public string MaGiaoDich { get; set; }
    
        public virtual CanBo CanBo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhoGiaoDichChiTiet> KhoGiaoDichChiTiets { get; set; }
    }
}
