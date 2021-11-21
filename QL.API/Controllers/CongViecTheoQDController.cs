using BusinessLogic.Management;
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
    [RoutePrefix("api/congviectheoqd")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CongViecTheoQDController : QLApiControlle
    {
        [Route("saveCongViec")]
        [HttpPost]
        public HttpResponseMessage Save(CongViecTheoQDModel value)
        {
            try
            {
                CongViecTheoQDManager.Instance.AddOrUpdate(value);
                return HttpOk("");
            }
            catch (Exception ex)
            {
                return HttpInternalServerError(ex.Message);
            }
        }
        [Route("getById")]
        [HttpPost]
        public HttpResponseMessage GetById(Guid Id)
        {
            try
            {
                var result = CongViecTheoQDManager.Instance.SelectById(Id);
                return HttpOk("");
            }
            catch (Exception ex)
            {
                return HttpInternalServerError(ex.Message);
            }
        }
        [Route("getByPage")]
        [HttpGet]
        public HttpResponseMessage GetByPage(DateTime TuNgay, DateTime DenNgay, Guid? IdKhoa, int iPageIndex, int iPageSize)
        {
            try
            {
                int iTotal = 0;
                var result = new ListSelect();
                result.List = CongViecTheoQDManager.Instance.GetPhieuDeNghiByPage(TuNgay, DenNgay.AddDays(1), IdKhoa, iPageIndex, iPageSize, out iTotal);
                result.iTotal = iTotal;
                return HttpOk(result);
            }
            catch (Exception ex)
            {
                return HttpInternalServerError(ex.Message);
            }
        }
    }
}