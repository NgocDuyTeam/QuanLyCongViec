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
    public partial class MauPhieuIn : BaseData
    {
    }
    public partial class CongViecTheoQuyetDinh : BaseData
    {
    }
    public partial class BienBanNghiemThu : BaseData
    {
    }
    public partial class KhoDMSanPham : BaseData
    {
    }
    public partial class KhoGiaoDich : BaseData
    {
    }
    public partial class KhoGiaoDichChiTiet : BaseData
    {
    }
    public partial class KhoTonKho : BaseData
    {
    }
    public partial class TuDien : BaseData
    {
    }
    public partial class TuDienLoai : BaseData
    {
    }
    public partial class BangMa : BaseData
    {
    }
}
