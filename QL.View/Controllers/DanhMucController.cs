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
        public ActionResult SC200a_AddDanhMucKhoaPhong()
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
        public ActionResult SC203_DanhMucSanPham()
        {
            ViewBag.Title = "Bệnh viện K - Hệ thống quản lý phiếu đề nghị";
            ViewBag.TitleUrl = " / Danh mục sản phẩm";
            return View();
        }
    }
}