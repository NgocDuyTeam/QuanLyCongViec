using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL.View.Controllers
{
    public class DanhMucController : BaseController
    {
        // GET: DanhMucKhoaPhong
        public ActionResult SC200_DanhMucKhoaPhong()
        {
            return View();
        }
        public ActionResult SC201_DanhMucCongViec()
        {
            return View();
        }
        public ActionResult SC202_DanhMucCanBo()
        {
            return View();
        }
    }
}