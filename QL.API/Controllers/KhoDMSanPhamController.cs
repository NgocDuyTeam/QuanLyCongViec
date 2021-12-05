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
    [RoutePrefix("api/dmsanpham")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class KhoDMSanPhamController : QLApiControlle
    {
        // GET api/<controller>
        [Route("getDanhSach")]
        [HttpGet]
        public HttpResponseMessage GetDanhSach(string sSearch, int iPageIndex, int iPageSize)
        {
            try
            {
                int iTotal = 0;
                var result = new ListSelect();
                result.List = KhoDMSanPhamManager.Instance.SelectAll(sSearch, iPageIndex, iPageSize, out iTotal);
                result.iTotal = iTotal;
                return HttpOk(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("addOrUpdate")]
        [HttpPost]
        public HttpResponseMessage AddOrUpdate(KhoDMSanPhamModel value)
        {
            try
            {
                KhoDMSanPhamManager.Instance.AddOrUpdate(value);
                return HttpOk("");
            }
            catch (Exception ex)
            {
                return HttpInternalServerError(ex.Message);
            }
        }
        [Route("deleteById")]
        [HttpPost]
        public HttpResponseMessage DeleteById(Guid IdSanPham)
        {
            try
            {
                KhoDMSanPhamManager.Instance.DeleteById(IdSanPham);
                return HttpOk("");
            }
            catch (Exception ex)
            {
                return HttpInternalServerError(ex.Message);
            }
        }
        [Route("getById")]
        [HttpGet]
        public HttpResponseMessage GetById(Guid IdSanPham)
        {
            try
            {
                var result = KhoDMSanPhamManager.Instance.SelectById(IdSanPham);
                return HttpOk(result);
            }
            catch (Exception ex)
            {
                return HttpInternalServerError(ex.Message);
            }
        }


    }
}