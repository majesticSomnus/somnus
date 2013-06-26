using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SomnusLogistic.Model;

namespace WebView.Core
{

    public class ValidateLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        { 
            //在Action执行前执行
            //filterContext.HttpContext.Response.Write(@"<br />Before Action execute" + "\t " + Message);
            UserInfo user = filterContext.HttpContext.Session["UserInfo"] as UserInfo;
            if (user == null)
            {
                string redirectOnSuccess = filterContext.HttpContext.Request.RawUrl;
                string redirectUrl = string.Format("?toUrl={0}", redirectOnSuccess);
                string loginUrl = "~/login/" + redirectUrl;
                filterContext.HttpContext.Response.Redirect(loginUrl, true);
                return;
            }
            
            base.OnActionExecuting(filterContext);
        }
    }
}