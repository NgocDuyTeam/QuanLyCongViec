using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QL.View
{
    public class RouteConfig
    {
        //public static void RegisterRoutes(RouteCollection routes)
        //{
        //    routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

        //    routes.MapRoute(
        //        name: "DangNhap",
        //        url: "dang-nhap",
        //        defaults: new { controller = "DangNhap", action = "Index", id = UrlParameter.Optional, loai_menu_cn = 1 }
        //    );
        //    routes.MapRoute(
        //        name: "DangXuat",
        //        url: "dang-xuat",
        //        defaults: new { controller = "DangNhap", action = "DoLogout", id = UrlParameter.Optional, loai_menu_cn = 1 }
        //    );
        //}
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "DangNhap", action = "SC100_dang_nhap", id = UrlParameter.Optional }
            );
        }
    }
}
