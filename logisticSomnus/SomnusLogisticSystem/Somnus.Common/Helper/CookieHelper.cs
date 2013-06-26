using System;
using System.Web;


namespace Somnus.Common.Helper
{
    /// <summary>
    /// COOKIE操作
    /// </summary>
    public class CookieHelper
    {
        /// <summary>
        /// 写COOKIE操作
        /// </summary>
        /// <param name="key">Cookie键</param>
        /// <param name="value">Cookie值</param>
        /// <param name="ms">过期时间(分钟数)</param>
        /// <param name="domain">域</param>
        public static void Write(string key, string values, int ms, string domain)
        {
            HttpResponse response = HttpContext.Current.Response;
            HttpRequest request = HttpContext.Current.Request;
            //先看有没有
            HttpCookie cookie = request.Cookies[key];

            if (cookie == null)
            {
                cookie = new HttpCookie(key);
            }

            cookie.Value =HttpContext.Current.Server.UrlEncode(EncryptString.Encrypt(values));

            

            cookie.Path = "/";

            if (!string.IsNullOrEmpty(domain))
                cookie.Domain = domain;
            
            if (ms != 0)
            {
                DateTime expir = DateTime.Now.AddMinutes(ms);
                cookie.Expires = expir;
            }
            response.AppendCookie(cookie);
        }

        /// <summary>
        /// 读取一个COOKIE
        /// </summary>
        /// <param name="key">COOKIE键</param>
        /// <returns></returns>
        public static string Read(string key)
        {
            HttpRequest request = HttpContext.Current.Request;

            HttpCookie cookie = request.Cookies[key];
            string result = string.Empty;
            if (cookie != null)
            {

                result = EncryptString.Decrypt(HttpContext.Current.Server.UrlDecode(cookie.Value));

           

            }
            return result;
        }


        /// <summary>
        /// 删除Cookie
        /// </summary>
        /// <param name="key"></param>
        /// <param name="domain"></param>
        /// <param name="ms"></param>
        public static void RemoveCookie(string key, string domain, int ms)
        {
            HttpCookie MyCo;
            if (domain != null && domain != "")
            {
                MyCo = HttpContext.Current.Request.Cookies[key];
                if (System.Web.HttpContext.Current.Request.ServerVariables.ToString().IndexOf(domain) >= 0 && MyCo != null)
                {
                    MyCo.Domain = domain;
                    MyCo.Expires = DateTime.Now.AddHours(-ms);
                    HttpContext.Current.Response.Cookies.Add(MyCo);
                }
            }
            else
            {
                MyCo = HttpContext.Current.Request.Cookies[key];
                if (MyCo != null)
                {
                    MyCo.Expires = DateTime.Now.AddHours(-ms);
                    HttpContext.Current.Response.Cookies.Add(MyCo);
                }

            }
        }
    }
}
