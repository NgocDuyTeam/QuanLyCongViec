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
       
        public List<KhoaPhongModel> SelectAll()
        {
            using (var uow = new UnitOfWork())
            {
                var lstKhoa = uow.Repository<KhoaPhong>().Query().OrderBy(x => x.OrderBy(y => y.Ten)).Get();
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
