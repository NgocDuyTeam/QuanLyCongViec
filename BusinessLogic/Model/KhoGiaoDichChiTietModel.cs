using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class KhoGiaoDichChiTietModel
    {
        public Guid Id { get; set; }
        public Guid IdGiaoDich { get; set; }
        public Guid IdSanPham { get; set; }
        public decimal SoLuong { get; set; }

        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string TenDonVi { get; set; }

    }
}
