using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL.View.Controllers
{
    public class PhieuDeNghiController : BaseController
    {
        public ActionResult SC100_PhieuDeNghi()
        {
            ViewBag.Title = "Bệnh viện K - Hệ thống quản lý phiếu đề nghị";
            ViewBag.TitleUrl = " / Tạo phiếu đề nghị";
            return View();
        }
        public ActionResult SC101_DSPhieuDeNghi()
        {
            ViewBag.Title = "Bệnh viện K - Hệ thống quản lý phiếu đề nghị";
            ViewBag.TitleUrl = " / Danh sách phiếu đề nghị";
            return View();
        }
        public ActionResult SC102_QLPhieuDeNghi()
        {
            ViewBag.Title = "Bệnh viện K - Hệ thống quản lý phiếu đề nghị";
            ViewBag.TitleUrl = " / Danh sách phiếu đề nghị";
            return View();
        }
        public ActionResult SC103_NVPhieDeNghi()
        {

            ViewBag.Title = "Bệnh viện K - Hệ thống quản lý phiếu đề nghị";
            ViewBag.TitleUrl = " / Danh sách phiếu đề nghị";
            return View();
        }
        public ActionResult SC104_DSPhieuDeNghiTuChoi()
        {

            ViewBag.Title = "Bệnh viện K - Hệ thống quản lý phiếu đề nghị";
            ViewBag.TitleUrl = " / Danh sách phiếu đề nghị bị từ chối";
            return View();

        }
    }
}
