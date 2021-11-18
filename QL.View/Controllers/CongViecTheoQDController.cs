using BusinessLogic.Helper;
using BusinessLogic.Management;
using BusinessLogic.Model;
using BusinessLogic.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QL.View.Controllers
{
    public class CongViecTheoQDController : Controller
    {
        [HttpGet]
        public ActionResult SC400_CongViec()
        {
            ViewBag.Title = "Công việc theo quyết định";
            ViewBag.TitleUrl = " / Quản lý công việc theo quyết định";
            return View();
        }
      
    }
}