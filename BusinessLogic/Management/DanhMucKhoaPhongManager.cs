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


        #endregion

        #region private
        #endregion

    }
}
