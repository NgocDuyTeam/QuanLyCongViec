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
    public class DanhMucKhoaPhongManager
    {
        #region Singleton
        private static DanhMucKhoaPhongManager _instance;
        private DanhMucKhoaPhongManager() { }
        public static DanhMucKhoaPhongManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DanhMucKhoaPhongManager();
                }
                return _instance;
            }
        }
        #endregion

        #region  public

        public List<KhoaPhongModel> SelectAll(int iPageIndex, int iPageSize, out int iTotal)
        {
            using (var uow = new UnitOfWork())
            {
                IEnumerable<KhoaPhong> lstKhoa = null;
                var query = uow.Repository<KhoaPhong>().Query().OrderBy(x => x.OrderBy(y => y.Ten));
                if (iPageIndex == -1)
                {
                    lstKhoa = query.Get();
                    iTotal = lstKhoa.Count();
                }
                else
                {
                    lstKhoa = query.GetPage(iPageIndex, iPageSize, out iTotal);
                }

                return lstKhoa.Select(x =>
                {
                    return x.CopyAs<KhoaPhongModel>();
                }).ToList();
            }
        }

        public void AddOrUpdateKhoaPhong(KhoaPhongModel value)
        {
            using (var uow = new UnitOfWork())
            {
                if (value.Id.IsNotNull())
                {
                    var KP = uow.Repository<KhoaPhong>().Query().Filter(x => x.Id == value.Id).FirstOrDefault();
                    KP.Id = value.Id;
                    KP.Ma = value.Ma;
                    KP.Ten = value.Ten;
                    KP.State = EDataState.Modified;
                    uow.Repository<KhoaPhong>().InsertOrUpdate(KP);
                }
                else
                {
                    var KhoaPhong = value.CopyAs<KhoaPhong>();
                    KhoaPhong.State = EDataState.Added;
                    KhoaPhong.Id = Guid.NewGuid();
                    uow.Repository<KhoaPhong>().InsertOrUpdate(KhoaPhong);
                }
                uow.Save();
            }
        }
        public void DeleteById(Guid IdKhoa)
        {
            using (var uow = new UnitOfWork())
            {
                var KhoaPhong = uow.Repository<KhoaPhong>().Query().Filter(x => x.Id == IdKhoa).FirstOrDefault();
                //var CanBo = uow.Repository<CanBo>().Query().Filter(y => y.IdKhoa == IdKhoa).FirstOrDefault();
                if (KhoaPhong != null)
                {
                    KhoaPhong.State = EDataState.Deleted;
                    uow.Repository<KhoaPhong>().Delete(IdKhoa);
                    uow.Save();
                }
            }
        }
        #endregion

        #region private
        #endregion

    }
}
