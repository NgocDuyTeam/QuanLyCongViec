using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLDataAccess
{
    public abstract class BaseData
    {
        public EDataState State { get; set; }
    }

    public partial class CanBo : BaseData
    {
    }
    public partial class PhieuDeNghi : BaseData
    {
    }
    public partial class KhoaPhong : BaseData
    {
    }
    public partial class DanhMucCongViec : BaseData
    {
    }
}
