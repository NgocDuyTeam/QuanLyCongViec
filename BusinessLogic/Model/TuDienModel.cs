using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class TuDienModel
    {
        public Guid Id { get; set; }
        public string MaTuDien { get; set; }
        public string TenTuDien { get; set; }
        public Guid IdLoaiTuDien { get; set; }
        public bool Active { get; set; }
        ///
        public string TenLoaiTuDien { get; set; }
    }
}
