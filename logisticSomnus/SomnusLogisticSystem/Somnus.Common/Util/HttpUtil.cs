using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;

namespace Somnus.Common.Util
{
    public class HttpUtil
    {
        /// <summary>
        /// get Url Page content
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="coding">编码方式</param>
        /// <returns></returns>
        public static string FetchUrl(string url, Encoding coding)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream stm = null;
            StreamReader reader = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                response = (HttpWebResponse)request.GetResponse();
                stm = response.GetResponseStream();
                reader = new StreamReader(stm, coding);
                string content = reader.ReadToEnd();
                return content;
            }
            catch (Exception exp)
            {
                throw new GetRequestError(exp.Message, exp);
            }
            finally
            {
                if (reader != null) reader.Close();
                if (stm != null) stm.Close();
                if (response != null) response.Close();

            }
        }

        public static int GetUrlError(string curl)
        {
            int num = 200;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(curl));
            ServicePointManager.Expect100Continue = false;
            try
            {
                ((HttpWebResponse)request.GetResponse()).Close();
            }
            catch (WebException exception)
            {
                if (exception.Status != WebExceptionStatus.ProtocolError)
                {
                    return num;
                }
                if (exception.Message.IndexOf("500 ") > 0)
                {
                    return 500;
                }
                if (exception.Message.IndexOf("401 ") > 0)
                {
                    return 401;
                }
                if (exception.Message.IndexOf("404") > 0)
                {
                    num = 404;
                }
            }
            return num;

        }

        /// <summary>
        /// 规范化并合并URL
        /// </summary>
        /// <param name="baseUri">基本url</param>
        /// <param name="urlpath">访问路径，或完全的url</param>
        /// <returns></returns>
        public static Uri CombineUrl(Uri baseUri, string urlpath)
        {
            if (string.IsNullOrEmpty(urlpath))
            {
                return null;
            }

            string lowverUrl = urlpath.ToLower();
            try
            {
                if (!lowverUrl.StartsWith("http://") && !lowverUrl.StartsWith("https://"))
                {
                    return new Uri(baseUri, urlpath);
                }
                else
                {
                    return new Uri(urlpath);
                }
            }
            catch
            {
                return null; //无效的url
            }
        }

        /// <summary>
        /// 自动检测内容的charset,通过字符集和html中指定的contentType来获取
        /// 如果无法获得字符集，则返回null
        /// </summary>
        /// <param name="stringcontent"></param>
        /// <returns></returns>
        public static Encoding DetectCharset(QuickWebResponse response, byte[] bytes)
        {

            string regexstr = "(text/html|text/xml).*charset=(?<charset>\\w+\\-*\\d*)";
            Regex regex = new Regex(regexstr, RegexOptions.IgnoreCase);

            string contentType = response.ContentTypeStr;
            if (contentType != null)
            {
                Match match1 = regex.Match(contentType);
                if (match1.Success)
                {
                    string charset = match1.Groups["charset"].Value.ToUpper();
                    Encoding encoder = System.Text.Encoding.GetEncoding(charset);
                    if (encoder != null)
                    {
                        return encoder;
                    }
                }
            }

            string ascii = System.Text.Encoding.ASCII.GetString(bytes);
            //<META http-equiv="Content-Type" content="text/html; charset=GB2312">
            //Content-Type=text/html;

            Match match = regex.Match(ascii);
            if (match.Success)
            {
                string charset = match.Groups["charset"].Value.ToUpper();
                return System.Text.Encoding.GetEncoding(charset);
            }
            else
            {
                return null;
            }
        }

        public static string HtmlEncode(String str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            string _txtString = str;
            _txtString = _txtString.Replace("<", "&lt;")
                .Replace(">", "&rt;")
                .Replace("\"", "&quot;")
                .Replace("'", "&#039;")
                .Replace(" ", "&nbsp;")
                .Replace("\r\n", "<br/>")
                .Replace("\r", "<br/>")
                .Replace("\n", "<br/>");

            return _txtString;
        }

        public static string LeftSub(string str, int len)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            return str.Length > len ? str.Substring(0, len) + "..." : str;
        }

        public static string LeftSubHtmlEncode(string str, int len)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            string subStr = LeftSub(str, len);
            return HttpUtility.HtmlEncode(subStr);
        }
    }
}
