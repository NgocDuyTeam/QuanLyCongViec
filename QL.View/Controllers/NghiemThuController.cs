using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL.View.Controllers
{
    public class NghiemThuController : BaseController
    {
        public ActionResult SC300_BienBanNghiemThu()
        {
            ViewBag.Title = "Bệnh viện K - Hệ thống quản lý phiếu đề nghị";
            ViewBag.TitleUrl = " / Biên bản nghiệm thu ";
            return View();
        }
    }
}