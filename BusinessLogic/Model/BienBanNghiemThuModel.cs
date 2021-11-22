using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class BienBanNghiemThuModel
    {
        public Guid Id { get; set; }
        public Guid? IdCongViec { get; set; }
        public Guid? IdPhieuDeNghi { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public Guid? IdCanBo { get; set; }
        public string DanhSachKhoa { get; set; }
        public string DauViec { get; set; }
        public string NhaThau { get; set; }
        public string PhongQuanTri { get; set; }
        public string GoiThau { get; set; }
        public string DoiTuongNghiemThu { get; set; }
        public string HopDongKinhTe { get; set; }
        public ObjPhongQuanTri ObjPhongQuanTri { get; set; }
        public ObjNhaThau ObjNhaThau { get; set; }
        public List<ObjCongViec> LstCongViec { get; set; }
        public string sDanhSachKhoa { get; set; }
    }
    public class ObjPhongQuanTri
    {
        public string HoVaTen { get; set; }
        public string ChucVu { get; set; }
    }
    public class ObjNhaThau
    {
        public string TenNhaThau { get; set; }
        public string HoVaTen { get; set; }
        public string ChucVu { get; set; }
    }
    public class ObjCongViec
    {
        public string STT { get; set; }
        public string NoiDung { get; set; }
        public string DonVi { get; set; }
        public string KhoiLuong { get; set; }
        public string GhiChu { get; set; }
    }
}
