﻿using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Webdiyer.WebControls.Mvc
{
    public static class MvcCaptchaHelper
    {
        private static MvcHtmlString MvcCaptcha(this HtmlHelper helper, string actionName, string controllerName, MvcCaptchaOptions options)
        {
            if(options==null)
                options=new MvcCaptchaOptions();
            var image = new MvcCaptchaImage(options);
            HttpContext.Current.Session.Add(
                image.UniqueId,
                image);
            var url = new UrlHelper(helper.ViewContext.RequestContext);
            var sb = new StringBuilder(1500);
            const string copyrightText = "\r\n<!--MvcCaptcha 1.2 @Webdiyer (http://www.webdiyer.com)-->\r\n";
            sb.Append(copyrightText);
            sb.Append("<input type=\"hidden\" name=\"_mvcCaptchaGuid\" id=\"_mvcCaptchaGuid\"");

            if (options.DelayLoad)
            {
                sb.Append("/><script language=\"javascript\" type=\"text/javascript\">if (typeof (jQuery) == \"undefined\") { alert(\"jQuery脚本库未加载，请检查！\"); }");
                sb.Append("var _mvcCaptchaPrevGuid = null,_mvcCaptchaImgLoaded = false;function _loadMvcCaptchaImage(){");
                sb.Append("if(!_mvcCaptchaImgLoaded){$.ajax({type:'GET',url:'");
                sb.Append(url.Action("MvcCaptchaLoader", "_MvcCaptcha",new RouteValueDictionary{{"area",null}}));
                sb.Append("?'+_mvcCaptchaPrevGuid,global:false,success:function(data){_mvcCaptchaImgLoaded=true;");
                sb.Append("$(\"#_mvcCaptchaGuid\").val(data);_mvcCaptchaPrevGuid=data;$(\"#");
                sb.Append(options.CaptchaImageContainerId).Append("\").html('");
                sb.Append(CreateImgTag(url.Action(actionName, controllerName, new RouteValueDictionary { { "area", null } }) + "?'+data+'", options, null));
                sb.Append("');}});} };function _reloadMvcCaptchaImage(){_mvcCaptchaImgLoaded=false;_loadMvcCaptchaImage();};$(function(){");
                sb.Append("if($(\"#").Append(options.ValidationInputBoxId).Append("\").length==0){alert(\"未能找到验证码输入文本框，请检查ValidationInputBoxId属性是否设置正确！\");}");
                sb.Append("if($(\"#").Append(options.CaptchaImageContainerId).Append("\").length==0){alert(\"未能找到验证码图片父容器，请检查CaptchaImageContainerId属性是否设置正确！\");}");
                sb.Append("$(\"#").Append(options.ValidationInputBoxId);
                sb.Append("\").bind(\"focus\",_loadMvcCaptchaImage)});</script>");
            }
            else
            {
                sb.AppendFormat(" value=\"{0}\" />",image.UniqueId);
                sb.Append(CreateImgTag(url.Action(actionName, controllerName, new RouteValueDictionary { { "area", null } }) + "?" + image.UniqueId, options, image.UniqueId));
                sb.Append("<script language=\"javascript\" type=\"text/javascript\">function _reloadMvcCaptchaImage(){var ci=document.getElementById(\"");
                sb.Append(image.UniqueId);
                sb.Append("\");var sl=ci.src.length;if(ci.src.indexOf(\"&\")>-1)sl=ci.src.indexOf(\"&\");ci.src=ci.src.substr(0,sl)+\"&\"+(new Date().valueOf());}</script>");
            }
            sb.Append(copyrightText);
            return MvcHtmlString.Create(sb.ToString());
        }

        static string CreateImgTag(string url, MvcCaptchaOptions options, string id)
        {
            var sb = new StringBuilder("<a href=\"javascript:_reloadMvcCaptchaImage()\"><img src=\"");
            sb.Append(url);
            sb.Append("\" alt=\"do4best\" title=\"刷新图片\" width=\"");
            sb.Append(options.Width);
            sb.Append("\" height=\"");
            sb.Append(options.Height);
            if (!string.IsNullOrEmpty(id))
                sb.Append("\" id=\"").Append(id);
            sb.Append("\" border=\"0\"/></a><a href=\"javascript:_reloadMvcCaptchaImage()\">").Append(options.ReloadLinkText).Append("</a>");
            return sb.ToString();
        }

        public static MvcHtmlString MvcCaptcha(this HtmlHelper helper)
        {
            return MvcCaptcha(helper, new MvcCaptchaOptions());
        }

        public static MvcHtmlString MvcCaptcha(this HtmlHelper helper, MvcCaptchaOptions options)
        {
            return MvcCaptcha(helper, "MvcCaptchaImage", "_MvcCaptcha", options);
        }
    }
}
