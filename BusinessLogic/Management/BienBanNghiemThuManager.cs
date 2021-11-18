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
    public class BienBanNghiemThuManager
    {
        #region Singleton
        private static BienBanNghiemThuManager _instance;
        private BienBanNghiemThuManager() { }
        public static BienBanNghiemThuManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BienBanNghiemThuManager();
                }
                return _instance;
            }
        }
        #endregion
        #region  public
        public void AddOrUpdateBienBan(BienBanNghiemThuModel value)
        {
            using (var uow = new UnitOfWork())
            {
                if (value.Id.IsNotNull())
                {

                }
                else
                {
                    if (value.IdPhieuDeNghi.IsNotNull())
                    {
                        var bienban = uow.Repository<BienBanNghiemThu>().Query().Filter(x => x.IdPhieuDeNghi == value.IdPhieuDeNghi).FirstOrDefault();
                        if (bienban == null)
                        {
                            var objSave = value.CopyAs<BienBanNghiemThu>();
                            objSave.Id = Guid.NewGuid();
                            string[] strDauViec = new string[value.LstCongViec.Count()];
                            for (int i = 0; i < value.LstCongViec.Count(); i++)
                            {
                                strDauViec[i] = value.LstCongViec[i].ToJson();
                            }
                            objSave.DauViec = strDauViec.JoinEmbeddedLength();
                            objSave.PhongQuanTri = value.ObjPhongQuanTri.ToJson();
                            objSave.State = EDataState.Added;
                            uow.Repository<BienBanNghiemThu>().InsertOrUpdate(objSave);
                        }
                    }
                }
                uow.Save();
            }
        }
        public BienBanNghiemThuModel SelectById(Guid id)
        {
            using (var uow = new UnitOfWork())
            {
                var bienban = uow.Repository<BienBanNghiemThu>().Query().Filter(x => x.Id == id).FirstOrDefault();
                if (bienban != null)
                {
                    var result = bienban.CopyAs<BienBanNghiemThuModel>();
                    result.LstCongViec = new List<ObjCongViec>();
                    var lstCongViec = result.DauViec.SplitEmbeddedLength();
                    foreach (var item in lstCongViec)
                    {
                        var cv = item.FromJson<ObjCongViec>();
                        result.LstCongViec.Add(cv);
                    }
                    result.ObjPhongQuanTri = bienban.PhongQuanTri.FromJson<ObjPhongQuanTri>();
                    return result;
                }
                return null;
            }

        }
        #endregion


    }
}
