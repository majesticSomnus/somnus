using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using SomnusLogistic.Model;
using SomnusLogistic.Service;
using Somnus.Common;
using Somnus.Common.Helper;
using Webdiyer.WebControls.Mvc;


namespace WebView.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index(string toUrl)
        {
            ViewData["toUrl"] = toUrl;
            return View();
        }

        [ValidateMvcCaptcha("MvcCaptchaText")]
        [HttpPost]
        public ActionResult Index(UserInfo user, FormCollection collection)
        {
            string userName = user.UserName;
            string password = user.Password;
            string toUrl = collection["toUrl"];
            if (string.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("userName", "用户名不能为空");
            }
            if (string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("password", "密码不能为空");
            }
            if (ModelState.IsValid == false)
            {
                return View();
            }

            Hashtable param = new Hashtable();
            param.Add("UserName", userName);
            param.Add("Password", Md5.ToMD5(password));
            UserInfo userInfo = UserInfoServant.Select(param, false).FirstOrDefault();
            if (userInfo != null)
            {
                if (userInfo.IsForbidden == false)
                {
                    Session["UserInfo"] = userInfo;
                    if (string.IsNullOrEmpty(toUrl))
                    {
                        return RedirectToAction("index", "userInfo");
                    }
                    else
                    {
                        return Redirect(toUrl);
                    }
                }
                else
                {
                    ViewData["errorMsg"] = "您的账号已经被禁用,请联系管理员";
                }
            }
            else
            {
                ViewData["errorMsg"] = "用户名或密码错误";
            }
            return View();
        }

        public ActionResult Change()
        {
            Session.Abandon();
            Session["UserInfo"] = null;
            return RedirectToAction("index");
        }
        public ActionResult logout()
        {
            Session.Abandon();
            Session["UserInfo"] = null;
            return RedirectToAction("index");
        }

    }
}
