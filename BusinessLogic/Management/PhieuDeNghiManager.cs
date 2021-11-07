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

        #endregion


    }
}
