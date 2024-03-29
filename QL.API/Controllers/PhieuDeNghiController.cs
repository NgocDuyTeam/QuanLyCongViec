﻿using BusinessLogic.Management;
using BusinessLogic.Model;
using QL.API.Http;
using QL.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace QL.API.Controllers
{
    [RoutePrefix("api/phieudenghi")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PhieuDeNghiController : QLApiControlle
    {
        // GET api/<controller>
        [Route("getDemo")]
        [HttpGet]
        public HttpResponseMessage DemoAPI(string sDemo)
        {
            try
            {
                return HttpOk("");
            }
            catch (Exception ex)
            {
                return HttpInternalServerError(ex.Message);
            }
        }
        [Route("savePhieuDeNghi")]
        [HttpPost]
        public HttpResponseMessage SavePhieuDeNghi(PhieuDeNghiModel value)
        {
            try
            {
                PhieuDeNghiManager.Instance.AddOrUpdatePhieu(value);
                return HttpOk("");
            }
            catch (Exception ex)
            {
                return HttpInternalServerError(ex.Message);
            }
        }
        [Route("SaveTrangThaiHT")]
        [HttpPost]
        public HttpResponseMessage SaveTrangThaiHT(PhieuDeNghiModel value)
        {
            try
            {
                PhieuDeNghiManager.Instance.SaveTrangThaiHT(value);
                return HttpOk("");
            }
            catch (Exception ex)
            {
                return HttpInternalServerError(ex.Message);
            }
        }
        [Route("savePhanCongPhieuDeNghi")]
        [HttpPost]
        public HttpResponseMessage SavePhanCongPhieuDeNghi(PhieuDeNghiModel value)
        {
            try
            {
                PhieuDeNghiManager.Instance.UpdatePhanCongPhieu(value);
                return HttpOk("");
            }
            catch (Exception ex)
            {
                return HttpInternalServerError(ex.Message);
            }
        }
        [Route("getPhieuDeNghiByPage")]
        [HttpGet]
        public HttpResponseMessage GetPhieuDeNghiByPage(Guid? IdKhoa, DateTime TuNgay, DateTime DenNgay, string sTrangThai
            , Guid? IdCanBo, int iPageIndex, int iPageSize, bool IsTuChoi)
        {
            try
            {
                int iTotal = 0;
                var result = new ListSelect();
                result.List = PhieuDeNghiManager.Instance.GetPhieuDeNghiByPage(IdKhoa, TuNgay, DenNgay.AddDays(1), sTrangThai, IdCanBo, iPageIndex, iPageSize, IsTuChoi, out iTotal);
                result.iTotal = iTotal;
                return HttpOk(result);
            }
            catch (Exception ex)
            {
                return HttpInternalServerError(ex.Message);
            }
        }
        [Route("getPhieuDeNghiByPagenv")]
        [HttpGet]
        public HttpResponseMessage GetPhieuDeNghiByPageNV(Guid? IdKhoa, DateTime TuNgay, DateTime DenNgay, string sTrangThai
           , Guid? IdCanBo, int iPageIndex, int iPageSize)
        {
            try
            {
                int iTotal = 0;
                var result = new ListSelect();
                result.List = PhieuDeNghiManager.Instance.GetPhieuDeNghiByPage(IdKhoa, TuNgay, DenNgay.AddDays(1), sTrangThai, IdCanBo, iPageIndex, iPageSize, false, out iTotal);
                result.iTotal = iTotal;
                return HttpOk(result);
            }
            catch (Exception ex)
            {
                return HttpInternalServerError(ex.Message);
            }
        }
        [Route("getPhieuDeId")]
        [HttpGet]
        public HttpResponseMessage GetPhieuDeId(Guid IdPhieu)
        {
            try
            {
                var result = PhieuDeNghiManager.Instance.SelectById(IdPhieu);
                return HttpOk(result);
            }
            catch (Exception ex)
            {
                return HttpInternalServerError(ex.Message);
            }
        }
        [Route("deletePhieuDeNghiById")]
        [HttpPost]
        public HttpResponseMessage DeletePhieuDeNghiById(Guid IdPhieu)
        {
            try
            {
                PhieuDeNghiManager.Instance.DeleteById(IdPhieu);
                return HttpOk("");
            }
            catch (Exception ex)
            {
                return HttpInternalServerError(ex.Message);
            }
        }
        [Route("tuChoiDeNghi")]
        [HttpPost]
        public HttpResponseMessage TuChoiDeNghi(Guid IdPhieu, string sNoiDung)
        {
            try
            {
                PhieuDeNghiManager.Instance.TuChoiDeNghi(IdPhieu, sNoiDung);
                return HttpOk("");
            }
            catch (Exception ex)
            {
                return HttpInternalServerError(ex.Message);
            }
        }
    }
}