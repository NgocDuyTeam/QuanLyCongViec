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
    public class TuDienManager
    {
        #region Singleton
        private static TuDienManager _instance;
        private TuDienManager() { }
        public static TuDienManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TuDienManager();
                }
                return _instance;
            }
        }
        #endregion

        #region  public

        public List<TuDienModel> SelectTuDienByLoai(string sLoaiTuDien)
        {
            using (var uow = new UnitOfWork())
            {
                var loaitudien = uow.Repository<TuDienLoai>().Query().Filter(x => x.LoaiTuDien == sLoaiTuDien).FirstOrDefault();
                if (loaitudien != null)
                {
                    return loaitudien.TuDiens.Select(x =>
                    {
                        var item = x.CopyAs<TuDienModel>();
                        item.TenLoaiTuDien = x.TuDienLoai.TenLoai;
                        return item;
                    }).OrderBy(x => x.TenTuDien).ToList();
                }
            }
            return null;
        }
        public TuDienModel SelectByMaAndLoai(string sMa, string sLoaiTuDien)
        {
            using (var uow = new UnitOfWork())
            {
                var loaitudien = uow.Repository<TuDienLoai>().Query().Filter(x => x.LoaiTuDien == sLoaiTuDien).FirstOrDefault(); var loaitudien = uow.Repository<TuDienLoai>().Query().Filter(x => x.LoaiTuDien == sLoaiTuDien).FirstOrDefault();
                if (loaitudien != null)
                {
                    var tudien = uow.Repository<TuDien>().Query().Filter(x => x.IdLoaiTuDien == loaitudien.Id
                    && x.MaTuDien == sMa).FirstOrDefault();
                    return tudien.CopyAs<TuDienModel>();
                }
            }
            return null;
        }

        #endregion

        #region private
        #endregion

    }
}
