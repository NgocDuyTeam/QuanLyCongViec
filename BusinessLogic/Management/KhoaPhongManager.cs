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
    public class KhoaPhongManager
    {
        #region Singleton
        private static KhoaPhongManager _instance;
        private KhoaPhongManager() { }
        public static KhoaPhongManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new KhoaPhongManager();
                }
                return _instance;
            }
        }
        #endregion
        #region  public
        public KhoaPhongModel SelectById(Guid Id)
        {
            using (var uow = new UnitOfWork())
            {
                var khoa = uow.Repository<KhoaPhong>().Query().Filter(x => x.Id == Id).FirstOrDefault();
                if (khoa != null)
                {
                    return khoa.CopyAs<KhoaPhongModel>();
                }
                return null;
            }
        }

        #endregion


    }
}
