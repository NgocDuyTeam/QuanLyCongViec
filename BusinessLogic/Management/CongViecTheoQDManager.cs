using BusinessLogic.Helper;
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
    public class CongViecTheoQDManager
    {
        #region Singleton
        private static CongViecTheoQDManager _instance;
        private CongViecTheoQDManager() { }
        public static CongViecTheoQDManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CongViecTheoQDManager();
                }
                return _instance;
            }
        }
        #endregion
        #region  public
        public void AddOrUpdate(CongViecTheoQDModel value)
        {
            using (var uow = new UnitOfWork())
            {
                if (value.Id.IsNotNull())
                {
                    var cv = uow.Repository<CongViecTheoQuyetDinh>().Query().Filter(x => x.Id == value.Id).FirstOrDefault();
                    cv.DanhSachKhoa = value.DanhSachKhoa;
                    cv.TenCongViec = value.TenCongViec;
                    cv.MoTaCongViec = value.MoTaCongViec;
                    cv.SoTien = value.SoTien;
                    cv.IdTienDo = GetStartTienDoByTien(value.SoTien ?? 0);
                    cv.State = EDataState.Modified;
                    uow.Repository<CongViecTheoQuyetDinh>().InsertOrUpdate(cv);
                }
                else
                {
                    var cv = value.CopyAs<CongViecTheoQuyetDinh>();
                    cv.State = EDataState.Added;
                    cv.NgayTao = DateTime.Now;
                    cv.Id = Guid.NewGuid();
                    uow.Repository<CongViecTheoQuyetDinh>().InsertOrUpdate(cv);
                }
                uow.Save();
            }
        }
        public CongViecTheoQDModel SelectById(Guid Id)
        {
            using (var uow = new UnitOfWork())
            {
                var phieu = uow.Repository<CongViecTheoQuyetDinh>().Query().Filter(x => x.Id == Id).FirstOrDefault();
                if (phieu != null)
                {
                    var result = phieu.CopyAs<CongViecTheoQDModel>();
                    return result;
                }
            }
            return null;
        }
        public List<CongViecTheoQDModel> GetPhieuDeNghiByPage(DateTime TuNgay, DateTime DenNgay, Guid? IdKhoa, int iPageIndex, int iPageSize, out int iTotal)
        {
            using (var uow = new UnitOfWork())
            {
                IEnumerable<CongViecTheoQuyetDinh> lstPhieu = null;
                var sKhoa = IdKhoa.IsNotNull() ? IdKhoa.Value.ToString() : "";
                var query = uow.Repository<CongViecTheoQuyetDinh>().Query().Filter(x => x.NgayTao >= TuNgay && x.NgayTao < DenNgay
                    && (sKhoa == "" || x.DanhSachKhoa.Contains(sKhoa)));
                if (iPageIndex != -1)
                {
                    lstPhieu = query.OrderBy(x => x.OrderByDescending(y => y.NgayTao)).GetPage(iPageIndex, iPageSize, out iTotal);
                }
                else
                {
                    lstPhieu = query.OrderBy(x => x.OrderByDescending(y => y.NgayTao)).Get();
                    iTotal = lstPhieu.Count();
                }
                return lstPhieu.Select(x =>
                {
                    var phieu = x.CopyAs<CongViecTheoQDModel>();
                    if (x.CanBo != null)
                    {
                        phieu.TenCanBo = x.CanBo.HoVaTen;
                    }
                    if (x.DanhSachKhoa.IsNotNullOrEmpty())
                    {
                        var lstId = x.DanhSachKhoa.Split(';');
                        Guid idKhoaTemp = Guid.Empty;
                        foreach (var idkhoa in lstId)
                        {
                            Guid.TryParse(idkhoa, out idKhoaTemp);
                            var khoa = DanhMucKhoaPhongManager.Instance.SelectById(idKhoaTemp);
                            if (khoa != null)
                            {
                                phieu.sDanhSachKhoa += khoa.Ten + ", ";
                            }
                        }
                    }
                    phieu.lstBienBan = new List<BienBanNghiemThuModel>();
                    if (x.BienBanNghiemThus.Count() > 0)
                    {
                        foreach (var item in x.BienBanNghiemThus)
                        {
                            if (!IdKhoa.IsNotNull() || (item.DanhSachKhoa.IsNotNullOrEmpty() && item.DanhSachKhoa.Contains(IdKhoa.ToString())))
                            {
                                var bb = item.CopyAs<BienBanNghiemThuModel>();
                                bb.LstCongViec = new List<ObjCongViec>();
                                if (bb.DauViec.IsNotNullOrEmpty())
                                {
                                    var lstCongViec = bb.DauViec.SplitEmbeddedLength();
                                    foreach (var itemCV in lstCongViec)
                                    {
                                        var cv = itemCV.FromJson<ObjCongViec>();
                                        bb.LstCongViec.Add(cv);
                                    }
                                }

                                bb.ObjPhongQuanTri = item.PhongQuanTri.FromJson<ObjPhongQuanTri>();
                                if (item.NhaThau != null)
                                {
                                    bb.ObjNhaThau = item.NhaThau.FromJson<ObjNhaThau>();
                                }
                                phieu.sDanhSachKhoa = "";
                                if (item.DanhSachKhoa.IsNotNullOrEmpty())
                                {
                                    var lstId = item.DanhSachKhoa.Split(';');
                                    Guid idKhoaTemp = Guid.Empty;
                                    foreach (var idkhoa in lstId)
                                    {
                                        Guid.TryParse(idkhoa, out idKhoaTemp);
                                        var khoa = DanhMucKhoaPhongManager.Instance.SelectById(idKhoaTemp);
                                        if (khoa != null)
                                        {
                                            bb.sDanhSachKhoa += khoa.Ten + ", ";
                                        }
                                    }
                                }
                                phieu.lstBienBan.Add(bb);
                            }
                        }
                    }
                    return phieu;
                }).ToList();
            }
        }
        public void DeleteById(Guid IdCongViec)
        {
            using (var uow = new UnitOfWork())
            {
                var cv = uow.Repository<CongViecTheoQuyetDinh>().Query().Filter(x => x.Id == IdCongViec).FirstOrDefault();
                if (cv != null)
                {
                    if (cv.BienBanNghiemThus.Count > 0)
                    {
                        throw new Exception("Đã có biên bản nghiệm thu được tạo.");
                    }
                    if (cv.BienBanNghiemThus.Count() == 0)
                    {
                        cv.State = EDataState.Deleted;
                        uow.Repository<CongViecTheoQuyetDinh>().Delete(cv);
                        uow.Save();
                    }
                }
            }
        }


        #endregion
        private Guid GetStartTienDoByTien(decimal SoTien)
        {
            if (SoTien >= 20000000 && SoTien < 50000000)
            {
                var tudien = TuDienManager.Instance.SelectByMaAndLoai(CGoiThau20_50.DuToan_1, CLoaiTuDien.GoiThau20_50);
                return tudien.Id;
            }
            else if (SoTien >= 50000000 && SoTien < 100000000)
            {
                var tudien = TuDienManager.Instance.SelectByMaAndLoai(CGoiThau50_100.DuToan_1, CLoaiTuDien.GoiThau50_100);
                return tudien.Id;
            }
            else if (SoTien >= 100000000)
            {
                var tudien = TuDienManager.Instance.SelectByMaAndLoai(CGoiThauTren100.DT_BCKTKT_1, CLoaiTuDien.GoiThauTren100);
                return tudien.Id;
            }
            else
            {
                var tudien = TuDienManager.Instance.SelectByMaAndLoai(CGoiThauDuoi20.DuToan_1, CLoaiTuDien.GoiThauDuoi20);
                return tudien.Id;
            }
        }

    }
}
