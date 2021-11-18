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
    
    public partial class PhieuDeNghi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuDeNghi()
        {
            this.BienBanNghiemThus = new HashSet<BienBanNghiemThu>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid IdKhoa { get; set; }
        public System.DateTime NgayTao { get; set; }
        public string TrangThai { get; set; }
        public string NoiDung { get; set; }
        public System.Guid IdCanBoDeNghi { get; set; }
        public Nullable<System.Guid> IdCanBoThucHien { get; set; }
        public System.Guid IdCongViec { get; set; }
    
        public virtual CanBo CanBoDeNghi { get; set; }
        public virtual CanBo CanBoYeuCau { get; set; }
        public virtual DanhMucCongViec DanhMucCongViec { get; set; }
        public virtual KhoaPhong KhoaPhong { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BienBanNghiemThu> BienBanNghiemThus { get; set; }
    }
}
