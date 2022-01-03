using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class BangMaModel
    {
        public Guid Id { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }
        public string TienTo { get; set; }
        public int DoDai { get; set; }
        public int SoHT { get; set; }
        public string TienToHT { get; set; }
    }
}
