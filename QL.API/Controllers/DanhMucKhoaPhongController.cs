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
    [RoutePrefix("api/dmkhoaphong")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DanhMucKhoaPhongController : QLApiControlle
    {
        // GET api/<controller>
        [Route("getDanhSach")]
        [HttpGet]
        public HttpResponseMessage GetDanhSach(int iPageIndex, int iPageSize)
        {
            try
            {
                int iTotal = 0;
                var result = new ListSelect();
                result.List = DanhMucKhoaPhongManager.Instance.SelectAll(iPageIndex, iPageSize, out iTotal);
                result.iTotal = iTotal;
                return HttpOk(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("saveKhoaPhong")]
        [HttpPost]
        public HttpResponseMessage SaveKhoaPhong(KhoaPhongModel value)
        {
            try
            {
                DanhMucKhoaPhongManager.Instance.AddOrUpdateKhoaPhong(value);
                return HttpOk("");
            }
            catch (Exception ex)
            {
                return HttpInternalServerError(ex.Message);
            }
        }
        [Route("deletePhieuDeNghiById")]
        [HttpPost]
        public HttpResponseMessage DeleteKhoaPhong(Guid IdKhoa)
        {
            try
            {
                DanhMucKhoaPhongManager.Instance.DeleteById(IdKhoa);
                return HttpOk("");
            }
            catch (Exception ex)
            {
                return HttpInternalServerError(ex.Message);
            }
        }



    }
}