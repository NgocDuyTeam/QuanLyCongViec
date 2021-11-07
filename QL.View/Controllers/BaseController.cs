using QL.View.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QL.View.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            try
            {
                try
                {
                    // Add thêm thời gian khi vẫn còn hoạt động, Tránh timeout;
                    var checkCookie = false;
                    foreach (var cookey in requestContext.HttpContext.Request.Cookies.AllKeys)
                    {
                        if (cookey == FormsAuthentication.FormsCookieName || cookey.ToLower() == "asp.net_sessionid")
                        {
                            var reqCookie = requestContext.HttpContext.Request.Cookies[cookey];

                            if (reqCookie != null)
                            {
                                System.Web.HttpCookie respCookie = new System.Web.HttpCookie(reqCookie.Name, reqCookie.Value);
                                respCookie.Expires = DateTime.Now.AddMinutes(360);

                                requestContext.HttpContext.Response.Cookies.Set(respCookie);
                                checkCookie = true;
                            }
                            break;
                        }
                    }
                    if (!checkCookie)
                    {
                        System.Web.HttpCookie respCookie = new System.Web.HttpCookie(FormsAuthentication.FormsCookieName);
                        respCookie.Expires = DateTime.Now.AddMinutes(360);
                        requestContext.HttpContext.Response.Cookies.Set(respCookie);
                        checkCookie = true;
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }

                if (!requestContext.HttpContext.Request.IsAuthenticated)
                {
                    requestContext.HttpContext.Response.Redirect(GetLoginUrl(requestContext));
                }
                var controller = requestContext.RouteData.Values["controller"].ToString().Trim().ToUpper();
                var action = requestContext.RouteData.Values["action"].ToString().Trim().ToUpper();

                var currentAcc = Global.TaiKhoan_Login;
                if (currentAcc != null)
                {

                    //Phân quyền nếu sử dụng

                }
                else if (controller == "DANGNHAP")
                {
                    requestContext.HttpContext.Response.Redirect(GetLoginUrl(requestContext));
                }
                base.Initialize(requestContext);
            }
            catch (Exception ex)
            {
                requestContext.HttpContext.Response.Redirect("/DangNhap/SC100_dang_nhap");
            }
        }
        private string GetLoginUrl(System.Web.Routing.RequestContext requestContext)
        {
            var redirectUrl = requestContext.HttpContext.Server.UrlEncode(requestContext.HttpContext.Request.Url.PathAndQuery);
            return string.Format("/DangNhap/SC100_dang_nhap");
        }
    }
}