using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class PhieuDeNghiModel
    {
        public Guid Id { get; set; }
        public Guid IdKhoa { get; set; }
        public DateTime NgayTao { get; set; }
        public string TrangThai { get; set; }
        public string NoiDung { get; set; }
        public Guid IdCanBoDeNghi { get; set; }
        public Guid? IdCanBoThucHien { get; set; }
        public Guid IdCongViec { get; set; }
    }
}
