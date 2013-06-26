
using System.Security.Cryptography;
using System.Text;
using System;

namespace Somnus.Common
{
    /// <summary>
    /// MD5操作
    /// </summary>
    public class Md5
    {
        /// <summary>
        /// MD5加密操作,用在产品venus中
        /// </summary>
        /// <param name="str_pwd">加密串</param>
        /// <returns>MD5值</returns>
        public static string ToMD5(string strPwd)
        {
            MD5 m = new MD5CryptoServiceProvider();
            byte[] arr = m.ComputeHash(UnicodeEncoding.UTF8.GetBytes(strPwd));
            return BitConverter.ToString(arr).ToLower().Replace("-", "");
        }

    }
}
