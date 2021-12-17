using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class KhoGiaoDichModel
    {
        public Guid Id { get; set; }
        public int LoaiGiaoDich { get; set; }
        public string MaGiaoDich { get; set; }
        public DateTime NgayTao { get; set; }
        public Guid IdNguoiTao { get; set; }
        public bool Active { get; set; }
        public string GhiChu { get; set; }
        public Guid IdKhoa { get; set; }
        public Guid? IdPhieuDeNghi { get; set; }
        public Guid? IdCongViec { get; set; }
        public List<KhoGiaoDichChiTietModel> ChiTiet { get; set; }

        public string TenCanBo { get; set; }
        public string TenKhoa { get; set; }
        public string DanhSachKhoa { get; set; }
    }

}
