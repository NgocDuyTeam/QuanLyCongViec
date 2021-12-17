using BusinessLogic.Management;
using BusinessLogic.Model;
using Framework.Extensions;
using QL.API.Http;
using QL.API.Models;
using SQLDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Cors;

namespace QL.API.Controllers
{
    [RoutePrefix("api/kho")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class KhoController : QLApiControlle
    {
        // GET api/<controller>
        [Route("createGiaoDichKho")]
        [HttpPost]
        public HttpResponseMessage CreateGiaoDichKho(KhoGiaoDichModel value)
        {
            try
            {
                using (var uow = new UnitOfWork())
                {
                    KhoManager.Instance.CreateGiaoDichKho(uow, value);
                    uow.Save();
                }
                return HttpOk("");
            }
            catch (Exception ex)
            {
                return HttpInternalServerError(ex.Message);
            }
        }
        [Route("getTonKho")]
        [HttpGet]
        public HttpResponseMessage GetDanhSach(string sSearch, int iPageIndex, int iPageSize)
        {
            try
            {
                int iTotal = 0;
                var result = new ListSelect();
                result.List = KhoManager.Instance.SelectTonKhoAll(sSearch, iPageIndex, iPageSize, out iTotal);
                result.iTotal = iTotal;
                return HttpOk(result);
            }
            catch (Exception ex)
            {
                return HttpInternalServerError(ex);
            }
        }

        [Route("getDanhSachGiaoDich")]
        [HttpGet]
        public HttpResponseMessage GetDanhSachGiaoDich(DateTime TuNgay, DateTime DenNgay, int iPageIndex, int iPageSize, string iStatus = "")
        {
            try
            {
                int iTotal = 0;
                var result = new ListSelect();
                result.List = KhoManager.Instance.SelectGiaoDichByPage(TuNgay, DenNgay.Date.AddDays(1), iStatus, iPageIndex, iPageSize, out iTotal);
                result.iTotal = iTotal;
                return HttpOk(result);
            }
            catch (Exception ex)
            {
                return HttpInternalServerError(ex);
            }
        }
        [Route("getGiaoDichChitiet")]
        [HttpGet]
        public HttpResponseMessage GetGiaoDichChiTiet(Guid IdGiaoDich)
        {
            try
            {
                var result = KhoManager.Instance.GetGiaoDichChiTiet(IdGiaoDich);
                return HttpOk(result);
            }
            catch (Exception ex)
            {
                return HttpInternalServerError(ex);
            }
        }
        [Route("bcXuatNhapTon")]
        [HttpGet]
        public HttpResponseMessage BaoCaoXuatNhapTon(string sSearch, DateTime TuNgay, DateTime DenNgay)
        {
            try
            {
                int iTotal = 0;
                var result = new ListSelect();
                result.List = KhoManager.Instance.BaoCaoXuatNhapTon(sSearch, TuNgay, DenNgay.Date.AddDays(1));
                result.iTotal = iTotal;
                return HttpOk(result);
            }
            catch (Exception ex)
            {
                return HttpInternalServerError(ex);
            }
        }
        [Route("bcXuatKhoaPhong")]
        [HttpGet]
        public HttpResponseMessage BCXuatKhoaPhong(string sSearch, DateTime TuNgay, DateTime DenNgay, Guid IdKhoa)
        {
            try
            {
                int iTotal = 0;
                var result = new ListSelect();
                result.List = KhoManager.Instance.BCXuatKhoaPhong(sSearch, TuNgay, DenNgay.Date.AddDays(1), IdKhoa);
                result.iTotal = iTotal;
                return HttpOk(result);
            }
            catch (Exception ex)
            {
                return HttpInternalServerError(ex);
            }
        }
        [Route("taoPhieuVTByPhieuDeNghi")]
        [HttpPost]
        public HttpResponseMessage TaoPhieuVTByPhieuDeNghi(KhoGiaoDichModel value)
        {
            try
            {
                if (value.DanhSachKhoa.IsNotNullOrEmpty())
                {
                    using (var uow = new UnitOfWork())
                    {
                        var lstKhoa = Regex.Split(value.DanhSachKhoa, ";");
                        foreach (var item in lstKhoa)
                        {
                            Guid idkhoa = Guid.Empty;
                            if (Guid.TryParse(item, out idkhoa))
                            {
                                value.IdKhoa = idkhoa;
                                KhoManager.Instance.CreateGiaoDichKho(uow, value);
                            }
                        }
                        uow.Save();
                    }
                }
                else
                {
                    using (var uow = new UnitOfWork())
                    {
                        KhoManager.Instance.CreateGiaoDichKho(uow, value);
                        uow.Save();
                    }
                }
                return HttpOk("");
            }
            catch (Exception ex)
            {
                return HttpInternalServerError(ex.Message);
            }
        }
        [Route("getGiaoDichByCongViec")]
        [HttpGet]
        public HttpResponseMessage GetGiaoDichByCongViec(int iPageIndex, int iPageSize, Guid IdCongViec)
        {
            try
            {
                int iTotal = 0;
                var result = new ListSelect();
                result.List = KhoManager.Instance.GetGiaoDichByCongViec(IdCongViec, iPageIndex, iPageSize, out iTotal);
                result.iTotal = iTotal;
                return HttpOk(result);
            }
            catch (Exception ex)
            {
                return HttpInternalServerError(ex);
            }
        }
    }
}