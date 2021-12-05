using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL.View.Controllers
{
    public class KhoController : BaseController
    {
        public ActionResult SC501NhapKho()
        {
            ViewBag.Title = "Bệnh viện K - Hệ thống quản lý phiếu đề nghị";
            ViewBag.TitleUrl = " / Nhập kho ";
            return View();
        }
        public ActionResult SC502XuatKho()
        {
            ViewBag.Title = "Bệnh viện K - Hệ thống quản lý phiếu đề nghị";
            ViewBag.TitleUrl = " / Xuất kho ";
            return View();
        }
        public ActionResult SC503TonKho()
        {
            ViewBag.Title = "Bệnh viện K - Hệ thống quản lý phiếu đề nghị";
            ViewBag.TitleUrl = " / Tồn kho ";
            return View();
        }
        public ActionResult SC504GiaoDichKho()
        {
            ViewBag.Title = "Bệnh viện K - Hệ thống quản lý phiếu đề nghị";
            ViewBag.TitleUrl = " / Danh sách giao dịch kho ";
            return View();
        }
    }
}