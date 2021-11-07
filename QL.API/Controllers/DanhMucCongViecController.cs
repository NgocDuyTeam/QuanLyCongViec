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
    [RoutePrefix("api/dmcongviec")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DanhMucCongViecController : QLApiControlle
    {
        // GET api/<controller>
        [Route("getDanhSach")]
        [HttpGet]
        public HttpResponseMessage GetDanhSach()
        {
            try
            {
                var result = new ListSelect();
                result.List = DanhMucCongViecManager.Instance.SelectAll();
                return HttpOk(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}