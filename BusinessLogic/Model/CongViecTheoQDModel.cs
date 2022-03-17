using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class CongViecTheoQDModel
    {
        public Guid Id { get; set; }
        public string MoTaCongViec { get; set; }
        public string DanhSachKhoa { get; set; }
        public string TenCongViec { get; set; }
        public string TrangThai { get; set; }
        public Guid IdCanBo { get; set; }
        public string TenCanBo { get; set; }
        public DateTime NgayTao { get; set; }
        public decimal? SoTien { get; set; }
        public Guid IdTienDo { get; set; }
        /// <summary>
        /// /ex
        /// </summary>
        public string sDanhSachKhoa { get; set; }
        public List<BienBanNghiemThuModel> lstBienBan { get; set; }
        public string sTienDo { get; set; }
        public List<TienDoCongViecModel> lstTienDo { get; set; }
        public string GhiChuTienDo { get; set; }
    }
    public class TienDoCongViecModel
    {
        public int STT { get; set; }
        public bool TrangThai { get; set; }
        public string TienDo { get; set; }
        public Guid Id { get; set; }
    }
}
