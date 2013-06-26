using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace WebView.Core
{
    public class WebSetting
    {
        public static int PageCount
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["PageCount"]);
            }
        }
    }
}