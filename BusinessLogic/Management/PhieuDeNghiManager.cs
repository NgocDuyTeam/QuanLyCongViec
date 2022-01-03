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
        public void SaveTrangThaiHT(PhieuDeNghiModel value)
        {
            using (var uow = new UnitOfWork())
            {
                if (value.Id.IsNotNull())
                {
                    var phieu = uow.Repository<PhieuDeNghi>().Query().Filter(x => x.Id == value.Id).FirstOrDefault();
                    phieu.TrangThai = CTrangThaiPhieu.DaThucHien;
                    phieu.State = EDataState.Modified;
                }
                uow.Save();
            }
        }
        public void UpdatePhanCongPhieu(PhieuDeNghiModel value)
        {
            using (var uow = new UnitOfWork())
            {
                if (value.Id.IsNotNull())
                {
                    var phieu = uow.Repository<PhieuDeNghi>().Query().Filter(x => x.Id == value.Id).FirstOrDefault();
                    phieu.IdCanBoThucHien = value.IdCanBoThucHien;
                    phieu.TrangThai = CTrangThaiPhieu.DaPhanViec;
                    phieu.State = EDataState.Modified;
                    uow.Repository<PhieuDeNghi>().InsertOrUpdate(phieu);
                }
                uow.Save();
            }
        }
        public PhieuDeNghiModel SelectById(Guid IdPhieu)
        {
            using (var uow = new UnitOfWork())
            {
                var phieu = uow.Repository<PhieuDeNghi>().Query().Filter(x => x.Id == IdPhieu).FirstOrDefault();
                if (phieu != null)
                {
                    var result = phieu.CopyAs<PhieuDeNghiModel>();
                    if (phieu.KhoaPhong != null)
                    {
                        result.TenKhoa = phieu.KhoaPhong.Ten;
                    }
                    if (phieu.CanBoDeNghi != null)
                    {
                        result.TenCBThucHien = phieu.CanBoDeNghi.HoVaTen;
                    }
                    if (phieu.DanhMucCongViec != null)
                    {
                        result.TenCongViec = phieu.DanhMucCongViec.TenCongViec;
                    }
                    if (phieu.TrangThai == CTrangThaiPhieu.GuiYeuCau)
                    {
                        result.sTrangThai = "Gửi yêu cầu";
                    }
                    if (phieu.BienBanNghiemThus.Count() > 0)
                    {
                        result.lstBienBan = new List<BienBanNghiemThuModel>();
                        foreach (var item in phieu.BienBanNghiemThus)
                        {
                            var bb = item.CopyAs<BienBanNghiemThuModel>();
                            bb.LstCongViec = new List<ObjCongViec>();
                            var lstCongViec = bb.DauViec.SplitEmbeddedLength();
                            foreach (var itemCV in lstCongViec)
                            {
                                var cv = itemCV.FromJson<ObjCongViec>();
                                bb.LstCongViec.Add(cv);
                            }
                            bb.ObjPhongQuanTri = item.PhongQuanTri.FromJson<ObjPhongQuanTri>();
                            if (item.NhaThau != null)
                            {
                                bb.ObjNhaThau = item.NhaThau.FromJson<ObjNhaThau>();
                            }
                            result.lstBienBan.Add(bb);
                        }
                    }
                    return result;
                }
            }
            return null;
        }
        public void DeleteById(Guid IdPhieu)
        {
            using (var uow = new UnitOfWork())
            {
                var phieu = uow.Repository<PhieuDeNghi>().Query().Filter(x => x.Id == IdPhieu).FirstOrDefault();
                if (phieu != null)
                {
                    phieu.State = EDataState.Deleted;
                    uow.Repository<PhieuDeNghi>().Delete(phieu);
                    uow.Save();
                }
            }
        }
        public void TuChoiDeNghi(Guid IdPhieu, string sNoiDung)
        {
            using (var uow = new UnitOfWork())
            {
                var phieu = uow.Repository<PhieuDeNghi>().Query().Filter(x => x.Id == IdPhieu).FirstOrDefault();
                if (phieu != null)
                {
                    if (phieu.TrangThai != CTrangThaiPhieu.GuiYeuCau)
                    {
                        throw new Exception("Không thể từ chối phiếu khi đã tiếp nhận.");
                    }
                    phieu.State = EDataState.Modified;
                    phieu.IsTuChoi = true;
                    phieu.LyDoTuChoi = sNoiDung;
                    uow.Repository<PhieuDeNghi>().Delete(phieu);
                    uow.Save();
                }
            }
        }
        public List<PhieuDeNghiModel> GetPhieuDeNghiByPage(Guid? IdKhoa, DateTime TuNgay, DateTime DenNgay, string sTrangThai
            , Guid? IdCanBo, int iPageIndex, int iPageSize, bool IsTuChoi, out int iTotal)
        {
            using (var uow = new UnitOfWork())
            {
                IEnumerable<PhieuDeNghi> lstPhieu = null;
                var query = uow.Repository<PhieuDeNghi>().Query().Filter(x => x.NgayTao >= TuNgay && x.NgayTao < DenNgay
                    && x.IsTuChoi == IsTuChoi);
                if (IdKhoa.HasValue)
                {
                    query = query.Filter(x => x.IdKhoa == IdKhoa);
                }
                if (sTrangThai.IsNotNullOrEmpty())
                {
                    query = query.Filter(x => sTrangThai.ToLower().Contains(x.TrangThai.ToLower()));
                }
                if (IdCanBo.IsNotNull())
                {
                    query = query.Filter(x => x.IdCanBoThucHien == IdCanBo);
                }
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
                    var phieu = x.CopyAs<PhieuDeNghiModel>();
                    if (x.KhoaPhong != null)
                    {
                        phieu.TenKhoa = x.KhoaPhong.Ten;
                    }
                    if (x.CanBoDeNghi != null)
                    {
                        phieu.TenCBThucHien = x.CanBoDeNghi.HoVaTen;
                    }
                    if (x.DanhMucCongViec != null)
                    {
                        phieu.TenCongViec = x.DanhMucCongViec.TenCongViec;
                    }
                    if (x.TrangThai == CTrangThaiPhieu.GuiYeuCau)
                    {
                        phieu.sTrangThai = "Gửi yêu cầu";
                    }
                    else if (x.TrangThai == CTrangThaiPhieu.DaPhanViec)
                    {
                        phieu.sTrangThai = "Đã phân công";
                    }
                    else if (x.TrangThai == CTrangThaiPhieu.DaThucHien)
                    {
                        phieu.sTrangThai = "Đã thực hiện";
                        if (x.BienBanNghiemThus.Count() > 0)
                        {
                            phieu.lstBienBan = new List<BienBanNghiemThuModel>();
                            foreach (var item in x.BienBanNghiemThus)
                            {
                                var bb = item.CopyAs<BienBanNghiemThuModel>();
                                bb.LstCongViec = new List<ObjCongViec>();
                                var lstCongViec = bb.DauViec.SplitEmbeddedLength();
                                foreach (var itemCV in lstCongViec)
                                {
                                    var cv = itemCV.FromJson<ObjCongViec>();
                                    bb.LstCongViec.Add(cv);
                                }
                                bb.ObjPhongQuanTri = item.PhongQuanTri.FromJson<ObjPhongQuanTri>();
                                if (item.NhaThau != null)
                                {
                                    bb.ObjNhaThau = item.NhaThau.FromJson<ObjNhaThau>();
                                }
                                phieu.lstBienBan.Add(bb);
                            }
                        }
                    }
                    if (x.KhoGiaoDiches.Count > 0)
                    {
                        var gd = x.KhoGiaoDiches.FirstOrDefault();
                        phieu.GiaoDichVatTu = gd.CopyAs<KhoGiaoDichModel>();
                        phieu.GiaoDichVatTu.TenKhoa = gd.KhoaPhong.Ten;
                        phieu.GiaoDichVatTu.ChiTiet = gd.KhoGiaoDichChiTiets.Select(y =>
                        {
                            var item = new KhoGiaoDichChiTietModel()
                            {
                                Id = y.Id,
                                GhiChu = y.GhiChu ?? "",
                                MaSanPham = y.KhoDMSanPham.Ma,
                                TenSanPham = y.KhoDMSanPham.TenSanPham,
                                TenDonVi = y.KhoDMSanPham.TuDien.TenTuDien,
                                SoLuong = y.SoLuong,
                            };
                            return item;
                        }).ToList();

                    }
                    return phieu;
                }).ToList();
            }
        }

        #endregion


    }
}
