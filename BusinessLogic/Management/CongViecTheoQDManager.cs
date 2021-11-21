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
    public class CongViecTheoQDManager
    {
        #region Singleton
        private static CongViecTheoQDManager _instance;
        private CongViecTheoQDManager() { }
        public static CongViecTheoQDManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CongViecTheoQDManager();
                }
                return _instance;
            }
        }
        #endregion
        #region  public
        public void AddOrUpdate(CongViecTheoQDModel value)
        {
            using (var uow = new UnitOfWork())
            {
                if (value.Id.IsNotNull())
                {
                    var cv = uow.Repository<CongViecTheoQuyetDinh>().Query().Filter(x => x.Id == value.Id).FirstOrDefault();
                    cv.DanhSachKhoa = value.DanhSachKhoa;
                    cv.TenCongViec = value.TenCongViec;
                    cv.MoTaCongViec = value.MoTaCongViec;
                    cv.State = EDataState.Modified;
                    uow.Repository<CongViecTheoQuyetDinh>().InsertOrUpdate(cv);
                }
                else
                {
                    var cv = value.CopyAs<CongViecTheoQuyetDinh>();
                    cv.State = EDataState.Added;
                    cv.NgayTao = DateTime.Now;
                    cv.Id = Guid.NewGuid();
                    uow.Repository<CongViecTheoQuyetDinh>().InsertOrUpdate(cv);
                }
                uow.Save();
            }
        }
        public CongViecTheoQDModel SelectById(Guid Id)
        {
            using (var uow = new UnitOfWork())
            {
                var phieu = uow.Repository<CongViecTheoQuyetDinh>().Query().Filter(x => x.Id == Id).FirstOrDefault();
                if (phieu != null)
                {
                    var result = phieu.CopyAs<CongViecTheoQDModel>();
                    return result;
                }
            }
            return null;
        }
        public List<CongViecTheoQDModel> GetPhieuDeNghiByPage(DateTime TuNgay, DateTime DenNgay, Guid? IdKhoa, int iPageIndex, int iPageSize, out int iTotal)
        {
            using (var uow = new UnitOfWork())
            {
                IEnumerable<CongViecTheoQuyetDinh> lstPhieu = null;
                var sKhoa = IdKhoa.IsNotNull() ? IdKhoa.Value.ToString() : "";
                var query = uow.Repository<CongViecTheoQuyetDinh>().Query().Filter(x => x.NgayTao >= TuNgay && x.NgayTao < DenNgay
                    && (sKhoa == "" || x.DanhSachKhoa.Contains(sKhoa)));
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
                    var phieu = x.CopyAs<CongViecTheoQDModel>();
                    if (x.CanBo != null)
                    {
                        phieu.TenCanBo = x.CanBo.HoVaTen;
                    }
                    return phieu;
                }).ToList();
            }
        }


        #endregion


    }
}
