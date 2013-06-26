using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.IO;
/// <summary>
///LogHelp 的摘要说明
/// </summary>
namespace Somnus.Common.Helper
{
    public class LogHelper
    {
        /**/
        /// <summary>  
        /// LogHelper的摘要说明。  
        /// </summary>  



        public static log4net.ILog logInfo = log4net.LogManager.GetLogger("logInfo");   //选择<logger name="logInfo">的配置

        public static log4net.ILog logError = log4net.LogManager.GetLogger("logError");   //选择<logger name="logError">的配置

        public static void SetConfig()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static void SetConfig(FileInfo configFile)
        {
            log4net.Config.XmlConfigurator.Configure(configFile);
        }

        public static void WriteLog(object info)
        {
            logInfo.Info(info);
        }

        public static void WriteErr(object error)
        {
            logError.Error(error);
        }

    }
}