using BusinessLogic.Management;
using BusinessLogic.Model;
using QL.API.Http;
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
    }
}