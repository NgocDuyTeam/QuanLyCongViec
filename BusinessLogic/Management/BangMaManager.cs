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
    public class BangMaManager
    {
        #region Singleton
        private static BangMaManager _instance;
        private BangMaManager() { }
        public static BangMaManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BangMaManager();
                }
                return _instance;
            }
        }
        #endregion

        #region  public

        public string GenMa(string ma, string tienTo, int doDai
             , bool tangSo, bool HienThiTienTo = false)
        {
            var date = DateTime.Now;

            using (var uow = new UnitOfWork())
            {
                var maHienTai = uow.Repository<BangMa>().Query()
                    .Filter(x => x.Ma == ma).FirstOrDefault();
                if (tienTo.IsNullOrEmpty()) { 
                    tienTo = ""; 
                }
                if (maHienTai == null)
                {
                    maHienTai = new BangMa()
                    {
                        Id = Guid.NewGuid(),
                        Ma = ma,
                        TienTo = tienTo,
                        Ten = ma,
                        TienToHT = "",
                        SoHT = 0,
                        DoDai = doDai,
                        HienThiTienTo = HienThiTienTo,
                        State = EDataState.Added,
                    };
                }
                else
                {
                    maHienTai.State = EDataState.Modified;
                }
                var vtienTo = maHienTai.TienTo ?? "";
                var soLuong = maHienTai.SoHT + 1;

                // thêm thông tin ngày tháng nếu có
                if (vtienTo.Contains("yyyy")) vtienTo = vtienTo.Replace("yyyy", date.ToString("yyyy"));
                if (vtienTo.Contains("MM")) vtienTo = vtienTo.Replace("MM", date.ToString("MM"));
                if (vtienTo.Contains("dd")) vtienTo = vtienTo.Replace("dd", date.ToString("dd"));
                if (vtienTo.Contains("yy")) vtienTo = vtienTo.Replace("yy", date.ToString("yy"));

                if (vtienTo != maHienTai.TienToHT || (doDai > 0 && soLuong.ToString().Length == doDai + 1)) soLuong = 1;

                var strMoi = "";
                var vdoDai = maHienTai.DoDai;
                if (vdoDai > 0)
                {
                    if (maHienTai.HienThiTienTo)
                    {
                        if (vtienTo.IsNotNullOrEmpty())
                        {
                            vdoDai = vdoDai - vtienTo.Length;
                        }
                        strMoi = vtienTo + string.Format("{0:D" + vdoDai + "}", int.Parse(soLuong.ToString()));
                    }
                    else
                    {
                        // Không thêm tiền tố vào đầu của mã
                        strMoi = string.Format("{0:D" + vdoDai + "}", int.Parse(soLuong.ToString()));
                    }
                }
                else
                {
                    // Không sử dụng tiền tố, hậu tố
                    strMoi = soLuong.ToString();
                }

                if (tangSo)
                {
                    // Trả
                    maHienTai.SoHT = soLuong;
                    maHienTai.TienToHT = vtienTo;
                    // Tăng số thì mới lưu
                    uow.Repository<BangMa>().InsertOrUpdate(maHienTai);
                    uow.Save();
                }

                return strMoi;
            }
        }
        public string GenMa(UnitOfWork uow,string ma, string tienTo, int doDai
             , bool tangSo, bool HienThiTienTo = false)
        {
            var date = DateTime.Now;
                var maHienTai = uow.Repository<BangMa>().Query()
                    .Filter(x => x.Ma == ma).FirstOrDefault();
                if (tienTo.IsNullOrEmpty())
                {
                    tienTo = "";
                }
                if (maHienTai == null)
                {
                    maHienTai = new BangMa()
                    {
                        Id = Guid.NewGuid(),
                        Ma = ma,
                        TienTo = tienTo,
                        Ten = ma,
                        TienToHT = "",
                        SoHT = 0,
                        DoDai = doDai,
                        HienThiTienTo = HienThiTienTo,
                        State = EDataState.Added,
                    };
                }
                else
                {
                    maHienTai.State = EDataState.Modified;
                }
                var vtienTo = maHienTai.TienTo ?? "";
                var soLuong = maHienTai.SoHT + 1;

                // thêm thông tin ngày tháng nếu có
                if (vtienTo.Contains("yyyy")) vtienTo = vtienTo.Replace("yyyy", date.ToString("yyyy"));
                if (vtienTo.Contains("MM")) vtienTo = vtienTo.Replace("MM", date.ToString("MM"));
                if (vtienTo.Contains("dd")) vtienTo = vtienTo.Replace("dd", date.ToString("dd"));
                if (vtienTo.Contains("yy")) vtienTo = vtienTo.Replace("yy", date.ToString("yy"));

                if (vtienTo != maHienTai.TienToHT || (doDai > 0 && soLuong.ToString().Length == doDai + 1)) soLuong = 1;

                var strMoi = "";
                var vdoDai = maHienTai.DoDai;
                if (vdoDai > 0)
                {
                    if (maHienTai.HienThiTienTo)
                    {
                        if (vtienTo.IsNotNullOrEmpty())
                        {
                            vdoDai = vdoDai - vtienTo.Length;
                        }
                        strMoi = vtienTo + string.Format("{0:D" + vdoDai + "}", int.Parse(soLuong.ToString()));
                    }
                    else
                    {
                        // Không thêm tiền tố vào đầu của mã
                        strMoi = string.Format("{0:D" + vdoDai + "}", int.Parse(soLuong.ToString()));
                    }
                }
                else
                {
                    // Không sử dụng tiền tố, hậu tố
                    strMoi = soLuong.ToString();
                }

                if (tangSo)
                {
                    // Trả
                    maHienTai.SoHT = soLuong;
                    maHienTai.TienToHT = vtienTo;
                    // Tăng số thì mới lưu
                    uow.Repository<BangMa>().InsertOrUpdate(maHienTai);
                }

                return strMoi;
        }
        #endregion

        #region private
        #endregion

    }
}
