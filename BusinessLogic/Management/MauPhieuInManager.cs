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
    public class MauPhieuInManager
    {
        #region Singleton
        private static MauPhieuInManager _instance;
        private MauPhieuInManager() { }
        public static MauPhieuInManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MauPhieuInManager();
                }
                return _instance;
            }
        }
        #endregion
        #region  public
        public MauPhieuInModel SelectByMa(string sMaPhieuIn)
        {
            using (var uow = new UnitOfWork())
            {
                var phieu = uow.Repository<MauPhieuIn>().Query().Filter(x => x.MaPhieuIn == sMaPhieuIn).FirstOrDefault();
                if (phieu != null)
                {
                    var result = phieu.CopyAs<MauPhieuInModel>();
                    return result;
                }
            }
            return null;
        }
        #endregion


    }
}
