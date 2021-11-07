using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogic.Utils;

namespace BusinessLogic.Helper
{
    public class CookieHelper
    {
        //SET COOKIE FUNCTIONS *****************************************************
        //SetTripleDESEncryptedCookie - key & value only
        public static void SetTripleDESEncryptedCookie(string key, string value)
        {
            //Convert parts
            key = CryptoUtils.EncryptTripleDES(key);
            value = CryptoUtils.EncryptTripleDES(value);
            SetCookie(key, value);
        }
        //SetTripleDESEncryptedCookie - overloaded method with expires parameter
        public static void SetTripleDESEncryptedCookie(string key, string value, System.DateTime expires, string domain = null)
        {
            //Convert parts
            key = CryptoUtils.EncryptTripleDES(key);
            value = CryptoUtils.EncryptTripleDES(value);
            SetCookie(key, value, expires, domain);
        }
        //SetEncryptedCookie - key & value only
        public static void SetEncryptedCookie(string key, string value)
        {
            //Convert parts
            key = CryptoUtils.Encrypt(key);
            value = CryptoUtils.Encrypt(value);
            SetCookie(key, value);
        }
        //SetEncryptedCookie - overloaded method with expires parameter
        public static void SetEncryptedCookie(string key, string value, System.DateTime expires)
        {
            //Convert parts
            key = CryptoUtils.Encrypt(key);
            value = CryptoUtils.Encrypt(value);
            SetCookie(key, value, expires);
        }
        //SetCookie - key & value only
        public static void SetCookie(string key, string value)
        {
            //Encode Part
            key = HttpContext.Current.Server.UrlEncode(key);
            value = HttpContext.Current.Server.UrlEncode(value);
            HttpCookie cookie = default(HttpCookie);
            cookie = new HttpCookie(key, value);
            SetCookie(cookie);
        }
        //SetCookie - overloaded with expires parameter
        public static void SetCookie(string key, string value, System.DateTime expires, string domain = null)
        {
            //Encode Parts
            key = HttpContext.Current.Server.UrlEncode(key);
            value = HttpContext.Current.Server.UrlEncode(value);
            HttpCookie cookie = default(HttpCookie);
            cookie = new HttpCookie(key, value);
            if (domain != null)
            {
                cookie.Domain = domain;
            }
            cookie.Expires = expires;
            SetCookie(cookie);
        }
        //SetCookie - HttpCookie only
        //final step to set the cookie to the response clause
        public static void SetCookie(HttpCookie cookie)
        {
            HttpContext.Current.Response.Cookies.Set(cookie);
        }
        //GET COOKIE FUNCTIONS *****************************************************
        public static string GetTripleDESEncryptedCookieValue(string key)
        {
            //encrypt key only - encoding done in GetCookieValue
            key = CryptoUtils.EncryptTripleDES(key);
            //get value 
            string value = null;
            value = GetCookieValue(key);
            //decrypt value
            value = CryptoUtils.DecryptTripleDES(value);
            return value;
        }

        public static HttpCookie GetTripleDESEncryptedCookieObject(string key)
        {
            //encrypt key only - encoding done in GetCookieValue
            key = CryptoUtils.EncryptTripleDES(key);
            key = HttpContext.Current.Server.UrlEncode(key);
            //get value 
            return HttpContext.Current.Request.Cookies.Get(key);
        }

        public static string GetEncryptedCookieValue(string key)
        {
            //encrypt key only - encoding done in GetCookieValue
            key = CryptoUtils.Encrypt(key);
            //get value 
            string value = null;
            value = GetCookieValue(key);
            //decrypt value
            value = CryptoUtils.Decrypt(value);
            return value;
        }
        public static HttpCookie GetCookie(string key)
        {
            //encode key for retrieval
            key = HttpContext.Current.Server.UrlEncode(key);
            return HttpContext.Current.Request.Cookies.Get(key);
        }
        public static string GetCookieValue(string key)
        {
            try
            {
                //don't encode key for retrieval here
                //done in the GetCookie function
                //get value 
                string value = GetCookie(key).Value;
                //decode stored value
                value = HttpContext.Current.Server.UrlDecode(value);
                return value;
            }
            catch
            {
            }
            return string.Empty;
        }
    }
}