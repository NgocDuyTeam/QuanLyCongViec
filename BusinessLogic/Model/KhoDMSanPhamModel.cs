using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class KhoDMSanPhamModel
    {
        public Guid Id { get; set; }
        public string Ma { get; set; }
        public string TenSanPham { get; set; }
        public Guid IdDonVi { get; set; }
        public string GhiChu { get; set; }
        ///
        public string TenDonVi { get; set; }
    }
}
