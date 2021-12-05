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
    [RoutePrefix("api/tudien")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TuDienController : QLApiControlle
    {
        // GET api/<controller>
        [Route("getTuDienByLoai")]
        [HttpGet]
        public HttpResponseMessage GetTuDienByLoai(string sLoaiTuDien)
        {
            try
            {
                var result = new ListSelect();
                result.List = TuDienManager.Instance.SelectTuDienByLoai(sLoaiTuDien);
                return HttpOk(result);

            }
            catch (Exception ex)
            {
                return HttpInternalServerError(ex.Message);
            }
        }


    }
}