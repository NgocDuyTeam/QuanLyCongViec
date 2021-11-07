using BusinessLogic.Management;
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
    [RoutePrefix("phieudenghi")]
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
                CanBoManager
                return HttpOk("");
            }
            catch (Exception ex)
            {
                return HttpInternalServerError(ex.Message);
            }
        }
    }
}