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
    
    public partial class CongViecTheoQuyetDinh
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CongViecTheoQuyetDinh()
        {
            this.BienBanNghiemThus = new HashSet<BienBanNghiemThu>();
        }
    
        public System.Guid Id { get; set; }
        public string MoTaCongViec { get; set; }
        public string DanhSachKhoa { get; set; }
        public string TenCongViec { get; set; }
        public string TrangThai { get; set; }
        public Nullable<System.Guid> IdCanBo { get; set; }
        public System.DateTime NgayTao { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BienBanNghiemThu> BienBanNghiemThus { get; set; }
        public virtual CanBo CanBo { get; set; }
    }
}