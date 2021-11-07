using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class CanBoModel
    {
        public Guid Id { get; set; }
        public string Ma { get; set; }
        public string HoVaTen { get; set; }
        public Guid? IdKhoa { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Role { get; set; }
        public string TenKhoa { get; set; }
    }
}
