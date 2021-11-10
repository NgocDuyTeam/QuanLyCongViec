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
    public class DanhMucCongViecManager
    {
        #region Singleton
        private static DanhMucCongViecManager _instance;
        private DanhMucCongViecManager() { }
        public static DanhMucCongViecManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DanhMucCongViecManager();
                }
                return _instance;
            }
        }
        #endregion

        #region  public
       
        public List<DanhMucCongViecModel> SelectAll()
        {
            using (var uow = new UnitOfWork())
            {
                var lstcongViec = uow.Repository<DanhMucCongViec>().Query().OrderBy(x => x.OrderBy(y => y.TenCongViec)).Get();
                return lstcongViec.Select(x =>
                {
                    return x.CopyAs<DanhMucCongViecModel>();
                }).ToList();


     
            }
        }


        #endregion

        #region private
        #endregion

    }
}
