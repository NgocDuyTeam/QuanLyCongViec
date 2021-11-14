using BusinessLogic.Model;
using Framework.Extensions;
using SQLDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Management
{
    public class PhieuDeNghiManager
    {
        #region Singleton
        private static PhieuDeNghiManager _instance;
        private PhieuDeNghiManager() { }
        public static PhieuDeNghiManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PhieuDeNghiManager();
                }
                return _instance;
            }
        }
        #endregion
        #region  public
        public void AddOrUpdatePhieu(PhieuDeNghiModel value)
        {
            using (var uow = new UnitOfWork())
            {
                if (value.Id.IsNotNull())
                {
                    var phieu = uow.Repository<PhieuDeNghi>().Query().Filter(x => x.Id == value.Id).FirstOrDefault();
                    phieu.NoiDung = value.NoiDung;
                    phieu.IdCongViec = value.IdCongViec;
                    phieu.State = EDataState.Modified;
                    uow.Repository<PhieuDeNghi>().InsertOrUpdate(phieu);
                }
                else
                {
                    var phieu = value.CopyAs<PhieuDeNghi>();
                    phieu.State = EDataState.Added;
                    phieu.NgayTao = DateTime.Now;
                    phieu.Id = Guid.NewGuid();
                    uow.Repository<PhieuDeNghi>().InsertOrUpdate(phieu);
                }
                uow.Save();
            }
        }
        public void UpdatePhanCongPhieu(PhieuDeNghiModel value)
        {
            using (var uow = new UnitOfWork())
            {
                if (value.Id.IsNotNull())
                {
                    var phieu = uow.Repository<PhieuDeNghi>().Query().Filter(x => x.Id == value.Id).FirstOrDefault();
                    phieu.IdCanBoThucHien = value.IdCanBoThucHien;
                    phieu.TrangThai = "DaPhanViec";
                    phieu.State = EDataState.Modified;
                    uow.Repository<PhieuDeNghi>().InsertOrUpdate(phieu);
                }
                uow.Save();
            }
        }
        public PhieuDeNghiModel SelectById(Guid IdPhieu)
        {
            using (var uow = new UnitOfWork())
            {
                var phieu = uow.Repository<PhieuDeNghi>().Query().Filter(x => x.Id == IdPhieu).FirstOrDefault();
                if (phieu != null)
                {
                    var result = phieu.CopyAs<PhieuDeNghiModel>();
                    if (phieu.KhoaPhong != null)
                    {
                        result.TenKhoa = phieu.KhoaPhong.Ten;
                    }
                    if (phieu.CanBoDeNghi != null)
                    {
                        result.TenCBThucHien = phieu.CanBoDeNghi.HoVaTen;
                    }
                    if (phieu.DanhMucCongViec != null)
                    {
                        result.TenCongViec = phieu.DanhMucCongViec.TenCongViec;
                    }
                    if (phieu.TrangThai == "GuiYeuCau")
                    {
                        result.sTrangThai = "Gửi yêu cầu";
                    }
                    return result;
                }
            }
            return null;
        }
        public void DeleteById(Guid IdPhieu)
        {
            using (var uow = new UnitOfWork())
            {
                var phieu = uow.Repository<PhieuDeNghi>().Query().Filter(x => x.Id == IdPhieu).FirstOrDefault();
                if (phieu != null)
                {
                    phieu.State = EDataState.Deleted;
                    uow.Repository<PhieuDeNghi>().Delete(phieu);
                    uow.Save();
                }
            }
        }
        public List<PhieuDeNghiModel> GetPhieuDeNghiByPage(Guid? IdKhoa, DateTime TuNgay, DateTime DenNgay, string sTrangThai
            , Guid? IdCanBo, int iPageIndex, int iPageSize, out int iTotal)
        {
            using (var uow = new UnitOfWork())
            {
                IEnumerable<PhieuDeNghi> lstPhieu = null;
                var query = uow.Repository<PhieuDeNghi>().Query().Filter(x => x.NgayTao >= TuNgay && x.NgayTao < DenNgay);
                if (IdKhoa.HasValue)
                {
                    query = query.Filter(x => x.IdKhoa == IdKhoa);
                }
                if (sTrangThai.IsNotNullOrEmpty())
                {
                    query = query.Filter(x => sTrangThai.ToLower().Contains(x.TrangThai.ToLower()));
                }
                if (IdCanBo.IsNotNull())
                {
                    query = query.Filter(x => x.IdCanBoThucHien == IdCanBo);
                }
                if (iPageIndex != -1)
                {
                    lstPhieu = query.OrderBy(x => x.OrderByDescending(y => y.NgayTao)).GetPage(iPageIndex, iPageSize, out iTotal);
                }
                else
                {
                    lstPhieu = query.OrderBy(x => x.OrderByDescending(y => y.NgayTao)).Get();
                    iTotal = lstPhieu.Count();
                }
                return lstPhieu.Select(x =>
                {
                    var phieu = x.CopyAs<PhieuDeNghiModel>();
                    if (x.KhoaPhong != null)
                    {
                        phieu.TenKhoa = x.KhoaPhong.Ten;
                    }
                    if (x.CanBoDeNghi != null)
                    {
                        phieu.TenCBThucHien = x.CanBoDeNghi.HoVaTen;
                    }
                    if (x.DanhMucCongViec != null)
                    {
                        phieu.TenCongViec = x.DanhMucCongViec.TenCongViec;
                    }
                    if (x.TrangThai == "GuiYeuCau")
                    {
                        phieu.sTrangThai = "Gửi yêu cầu";
                    }
                    else if (x.TrangThai == "DaPhanViec")
                    {
                        phieu.sTrangThai = "Đã phân công";
                    }
                    else if (x.TrangThai == "DaThucHien")
                    {
                        phieu.sTrangThai = "Đã thực hiện";
                    }
                    return phieu;
                }).ToList();
            }
        }

        #endregion


    }
}
