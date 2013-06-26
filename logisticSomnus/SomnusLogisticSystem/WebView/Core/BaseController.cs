using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SomnusLogistic.Model;

namespace WebView.Core
{
    public class BaseController : Controller
    {
        public UserInfo user = null;
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
             user = requestContext.HttpContext.Session["UserInfo"] as UserInfo;
            if (user == null)
            {
                string redirectOnSuccess = requestContext.HttpContext.Request.RawUrl;
                string redirectUrl = string.Format("?toUrl={0}", redirectOnSuccess);
               // string loginUrl = requestContext.HttpContext.Request.ApplicationPath+"login/" + redirectUrl;
                string loginUrl = "~/login/" + redirectUrl;
                requestContext.HttpContext.Response.Redirect(loginUrl, true);
                return;
            }
            else
            {
                ViewBag.RealName = user.RealName;
            }
            base.Initialize(requestContext);
        }
    }
}