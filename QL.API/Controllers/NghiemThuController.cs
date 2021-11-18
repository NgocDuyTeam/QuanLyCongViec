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
    [RoutePrefix("api/nghiemthu")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class NghiemThuController : QLApiControlle
    {
        [Route("saveBienBan")]
        [HttpPost]
        public HttpResponseMessage SaveBienBan(BienBanNghiemThuModel value)
        {
            try
            {
                BienBanNghiemThuManager.Instance.AddOrUpdateBienBan(value);
                return HttpOk("");
            }
            catch (Exception ex)
            {
                return HttpInternalServerError(ex.Message);
            }
        }
        [Route("getBienBanById")]
        [HttpPost]
        public HttpResponseMessage GetBienBanById(Guid Id)
        {
            try
            {
               var result = BienBanNghiemThuManager.Instance.SelectById(Id);
                return HttpOk("");
            }
            catch (Exception ex)
            {
                return HttpInternalServerError(ex.Message);
            }
        }
    }
}