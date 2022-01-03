using BusinessLogic.Data;
using BusinessLogic.Helper;
using BusinessLogic.Model;
using Framework.Extensions;
using SQLDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Management
{
    public class KhoManager
    {
        #region Singleton
        private static KhoManager _instance;
        private KhoManager() { }
        public static KhoManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new KhoManager();
                }
                return _instance;
            }
        }
        #endregion

        #region  public

        public List<KhoTonKhoModel> SelectTonKhoAll(string sSearch, int iPageIndex, int iPageSize, out int iTotal)
        {
            using (var uow = new UnitOfWork())
            {
                IEnumerable<KhoTonKho> lstSP = null;
                var query = uow.Repository<KhoTonKho>().Query().OrderBy(x => x.OrderBy(y => y.KhoDMSanPham.Ma));
                if (sSearch.IsNotNullOrEmpty())
                {
                    query = query.Filter(x => x.KhoDMSanPham.TenSanPham.ToLower().Contains(sSearch.ToLower())
                    || x.KhoDMSanPham.Ma.ToLower().Contains(sSearch.ToLower()));
                }
                if (iPageIndex == -1)
                {
                    lstSP = query.Get();
                    iTotal = lstSP.Count();
                }
                else
                {
                    lstSP = query.GetPage(iPageIndex, iPageSize, out iTotal);
                }

                return lstSP.Select(x =>
                {
                    var item = x.CopyAs<KhoTonKhoModel>();
                    item.TenSanPham = x.KhoDMSanPham.TenSanPham;
                    item.MaSanPham = x.KhoDMSanPham.Ma;
                    item.TenDonVi = x.KhoDMSanPham.TuDien.TenTuDien;
                    return item;
                }).ToList();
            }
        }
        public List<KhoGiaoDichModel> SelectGiaoDichByPage(DateTime TuNgay, DateTime DenNgay, string iStatus, int iPageIndex, int iPageSize, out int iTotal)
        {
            using (var uow = new UnitOfWork())
            {
                IEnumerable<KhoGiaoDich> lstSP = null;
                var query = uow.Repository<KhoGiaoDich>().Query().Filter(x => x.NgayTao >= TuNgay && x.NgayTao < DenNgay)
                    .OrderBy(x => x.OrderBy(y => y.NgayTao));
                if (iStatus == "1")
                {
                    query = query.Filter(x => x.LoaiGiaoDich == 1);
                }
                else if (iStatus == "0")
                {
                    query = query.Filter(x => x.LoaiGiaoDich == 0);
                }
                if (iPageIndex == -1)
                {
                    lstSP = query.Get();
                    iTotal = lstSP.Count();
                }
                else
                {
                    lstSP = query.GetPage(iPageIndex, iPageSize, out iTotal);
                }
                return lstSP.Select(x =>
                {
                    var item = x.CopyAs<KhoGiaoDichModel>();
                    item.TenCanBo = x.CanBo.HoVaTen;
                    item.TenKhoa = x.KhoaPhong.Ten;
                    return item;
                }).ToList();
            }
        }
        public void CreateGiaoDichKho(UnitOfWork uow, KhoGiaoDichModel value)
        {
            var MaGiaoDich = BangMaManager.Instance.GenMa(uow, CBangMa.MaGiaoDichKho, "GDK", 8, true, true);
            if (value.LoaiGiaoDich == 1)
            {
                KhoGiaoDich gd = new KhoGiaoDich()
                {
                    Id = Guid.NewGuid(),
                    Active = true,
                    NgayTao = DateTime.Now,
                    LoaiGiaoDich = 1,
                    IdNguoiTao = value.IdNguoiTao,
                    GhiChu = value.GhiChu,
                    MaGiaoDich = MaGiaoDich,
                    IdKhoa = value.IdKhoa,
                    IdPhieuDeNghi = value.IdPhieuDeNghi,
                    IdCongViec = value.IdCongViec,
                    State = EDataState.Added
                };
                uow.Repository<KhoGiaoDich>().InsertOrUpdate(gd);
                foreach (var item in value.ChiTiet)
                {
                    var ton = uow.Repository<KhoTonKho>().Query().Filter(x => x.IdSanPham == item.IdSanPham).FirstOrDefault();
                    if (ton != null)
                    {
                        ton.SoLuong += item.SoLuong;
                        ton.State = EDataState.Modified;
                    }
                    else
                    {
                        ton = new KhoTonKho()
                        {
                            Id = Guid.NewGuid(),
                            IdSanPham = item.IdSanPham,
                            State = EDataState.Added,
                            SoLuong = item.SoLuong
                        };
                    }
                    uow.Repository<KhoTonKho>().InsertOrUpdate(ton);
                    KhoGiaoDichChiTiet chitiet = new KhoGiaoDichChiTiet()
                    {
                        Id = Guid.NewGuid(),
                        IdGiaoDich = gd.Id,
                        IdSanPham = item.IdSanPham,
                        SoLuong = item.SoLuong,
                        IdTonKho = ton.Id,
                        State = EDataState.Added
                    };
                    uow.Repository<KhoGiaoDichChiTiet>().InsertOrUpdate(chitiet);
                }
            }
            else
            {
                KhoGiaoDich gd = new KhoGiaoDich()
                {
                    Id = Guid.NewGuid(),
                    MaGiaoDich = MaGiaoDich,
                    Active = true,
                    NgayTao = DateTime.Now,
                    LoaiGiaoDich = 0,
                    IdNguoiTao = value.IdNguoiTao,
                    GhiChu = value.GhiChu,
                    IdKhoa = value.IdKhoa,
                    IdPhieuDeNghi = value.IdPhieuDeNghi,
                    IdCongViec = value.IdCongViec,
                    State = EDataState.Added,
                };
                uow.Repository<KhoGiaoDich>().InsertOrUpdate(gd);
                foreach (var item in value.ChiTiet)
                {
                    var ton = uow.Repository<KhoTonKho>().Query().Filter(x => x.IdSanPham == item.IdSanPham
                    && x.SoLuong >= item.SoLuong).FirstOrDefault();
                    if (ton != null)
                    {
                        ton.SoLuong -= item.SoLuong;
                        ton.State = EDataState.Modified;
                        if (ton.SoLuong <= 0)
                        {
                            throw new Exception("Kho không đủ sản phẩm để trừ kho. Giao dịch thất bại.");
                        }
                    }
                    else
                    {
                        throw new Exception("Kho không đủ sản phẩm để trừ kho. Giao dịch thất bại.");
                    }
                    uow.Repository<KhoTonKho>().InsertOrUpdate(ton);
                    KhoGiaoDichChiTiet chitiet = new KhoGiaoDichChiTiet()
                    {
                        Id = Guid.NewGuid(),
                        IdGiaoDich = gd.Id,
                        IdSanPham = item.IdSanPham,
                        SoLuong = item.SoLuong,
                        IdTonKho = ton.Id,
                        State = EDataState.Added
                    };
                    uow.Repository<KhoGiaoDichChiTiet>().InsertOrUpdate(chitiet);
                }
            }
        }
        public KhoGiaoDichModel GetGiaoDichChiTiet(Guid IdGiaoDich)
        {
            using (var uow = new UnitOfWork())
            {
                var gd = uow.Repository<KhoGiaoDich>().Query().Filter(x => x.Id == IdGiaoDich).FirstOrDefault();
                if (gd != null)
                {
                    var result = gd.CopyAs<KhoGiaoDichModel>();
                    result.ChiTiet = gd.KhoGiaoDichChiTiets.Select(x =>
                    {
                        var item = x.CopyAs<KhoGiaoDichChiTietModel>();
                        item.TenDonVi = x.KhoDMSanPham.TuDien.TenTuDien;
                        item.TenSanPham = x.KhoDMSanPham.TenSanPham;
                        item.MaSanPham = x.KhoDMSanPham.Ma;
                        return item;
                    }).ToList();
                    return result;
                }
            }
            return null;
        }
        public DataTable BaoCaoXuatNhapTon(string sSearch, DateTime TuNgay, DateTime DenNgay)
        {
            if (sSearch == null)
            {
                sSearch = "";
            }
            ReportData data = new ReportData();
            data.AddSqlParameter("@search", sSearch);
            data.AddSqlParameter("@tungay", TuNgay);
            data.AddSqlParameter("@denngay", DenNgay);
            DataSet ds = data.GetDataSet(CProcName.proc_BCKho_Xuat_Nhap_Ton);
            return ds.Tables[0];
        }
        public DataTable BCXuatKhoaPhong(string sSearch, DateTime TuNgay, DateTime DenNgay, Guid IdKhoa)
        {
            if (sSearch == null)
            {
                sSearch = "";
            }
            ReportData data = new ReportData();
            data.AddSqlParameter("@search", sSearch);
            data.AddSqlParameter("@tungay", TuNgay);
            data.AddSqlParameter("@denngay", DenNgay);
            data.AddSqlParameter("@idkhoa", IdKhoa);
            DataSet ds = data.GetDataSet(CProcName.proc_BCKhoXuatKhoaPhong);
            return ds.Tables[0];
        }
        public List<KhoGiaoDichModel> GetGiaoDichByCongViec(Guid IdCongViec, int iPageIndex, int iPageSize, out int iTotal)
        {
            using (var uow = new UnitOfWork())
            {
                IEnumerable<KhoGiaoDich> lstSP = null;
                var query = uow.Repository<KhoGiaoDich>().Query().Filter(x => x.IdCongViec == IdCongViec)
                    .OrderBy(x => x.OrderBy(y => y.MaGiaoDich));

                if (iPageIndex == -1)
                {
                    lstSP = query.Get();
                    iTotal = lstSP.Count();
                }
                else
                {
                    lstSP = query.GetPage(iPageIndex, iPageSize, out iTotal);
                }
                return lstSP.Select(x =>
                {
                    var item = x.CopyAs<KhoGiaoDichModel>();
                    item.TenCanBo = x.CanBo.HoVaTen;
                    item.TenKhoa = x.KhoaPhong.Ten;
                    item.ChiTiet = x.KhoGiaoDichChiTiets.Select(y =>
                    {
                        var ct = new KhoGiaoDichChiTietModel()
                        {
                            Id = y.Id,
                            GhiChu = y.GhiChu ?? "",
                            MaSanPham = y.KhoDMSanPham.Ma,
                            TenSanPham = y.KhoDMSanPham.TenSanPham,
                            TenDonVi = y.KhoDMSanPham.TuDien.TenTuDien,
                            SoLuong = y.SoLuong,
                        };
                        return ct;
                    }).ToList();
                    return item;
                }).ToList();
            }
        }
        #endregion

        #region private
        #endregion

    }
}
