using BusinessLogic.Management;
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
    [RoutePrefix("api/mauphieuin")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MauPhieuInController : QLApiControlle
    {
        [Route("getByMa")]
        [HttpGet]
        public HttpResponseMessage GetByMa(string sMa)
        {
            try
            {
                var result = MauPhieuInManager.Instance.SelectByMa(sMa);
                return HttpOk(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
