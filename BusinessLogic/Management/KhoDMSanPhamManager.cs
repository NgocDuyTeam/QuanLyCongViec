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
    public class KhoDMSanPhamManager
    {
        #region Singleton
        private static KhoDMSanPhamManager _instance;
        private KhoDMSanPhamManager() { }
        public static KhoDMSanPhamManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new KhoDMSanPhamManager();
                }
                return _instance;
            }
        }
        #endregion

        #region  public

        public List<KhoDMSanPhamModel> SelectAll(string sSearch, int iPageIndex, int iPageSize, out int iTotal)
        {
            using (var uow = new UnitOfWork())
            {
                IEnumerable<KhoDMSanPham> lstSP = null;
                var query = uow.Repository<KhoDMSanPham>().Query().OrderBy(x => x.OrderBy(y => y.Ma));
                if (sSearch.IsNotNullOrEmpty())
                {
                    query = query.Filter(x => x.TenSanPham.ToLower().Contains(sSearch.ToLower()) || x.Ma.ToLower().Contains(sSearch.ToLower()));
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
                    var item = x.CopyAs<KhoDMSanPhamModel>();
                    item.TenDonVi = x.TuDien.TenTuDien;
                    return item;
                }).ToList();
            }
        }

        public void AddOrUpdate(KhoDMSanPhamModel value)
        {
            using (var uow = new UnitOfWork())
            {
                if (value.Id.IsNotNull())
                {
                    var sp = value.CopyAs<KhoDMSanPham>();
                    sp.State = EDataState.Modified;
                    uow.Repository<KhoDMSanPham>().InsertOrUpdate(sp);
                }
                else
                {
                    var sp = value.CopyAs<KhoDMSanPham>();
                    sp.Ma = BangMaManager.Instance.GenMa("MaSanPham", "SP", 8, true, true);
                    sp.State = EDataState.Added;
                    sp.Id = Guid.NewGuid();
                    uow.Repository<KhoDMSanPham>().InsertOrUpdate(sp);
                }
                uow.Save();
            }
        }
        public void DeleteById(Guid idSanPham)
        {
            using (var uow = new UnitOfWork())
            {
                var sp = uow.Repository<KhoDMSanPham>().Query().Filter(x => x.Id == idSanPham).FirstOrDefault();
                if (sp != null)
                {
                    if (sp.KhoGiaoDichChiTiets.Count() != 0)
                    {
                        throw new Exception("Thao tác không thành công. Đã có giao dịch được tạo ra.");
                    }
                    if (sp.KhoTonKhoes.Count() != 0)
                    {
                        throw new Exception("Thao tác không thành công. Đã có số lượng tồn trong kho.");
                    }
                    sp.State = EDataState.Deleted;
                    uow.Repository<KhoDMSanPham>().Delete(sp);
                    uow.Save();
                }
            }
        }
        public KhoDMSanPhamModel SelectById(Guid IdSanPham)
        {
            using (var uow = new UnitOfWork())
            {
                var sp = uow.Repository<KhoDMSanPham>().Query().Filter(x => x.Id == IdSanPham).FirstOrDefault();
                if (sp != null)
                {
                    var item = sp.CopyAs<KhoDMSanPhamModel>();
                    item.TenDonVi = sp.TuDien.TenTuDien;
                    return item;
                }
            }
            return null;
        }
        #endregion

        #region private
        #endregion

    }
}
