﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QuanLyContainer : DbContext
    {
        public QuanLyContainer()
            : base("name=QuanLyContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CanBo> CanBoes { get; set; }
        public virtual DbSet<KhoaPhong> KhoaPhongs { get; set; }
        public virtual DbSet<DanhMucCongViec> DanhMucCongViecs { get; set; }
        public virtual DbSet<PhieuDeNghi> PhieuDeNghis { get; set; }
        public virtual DbSet<MauPhieuIn> MauPhieuIns { get; set; }
        public virtual DbSet<BienBanNghiemThu> BienBanNghiemThus { get; set; }
        public virtual DbSet<CongViecTheoQuyetDinh> CongViecTheoQuyetDinhs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}
