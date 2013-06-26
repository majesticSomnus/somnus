using System;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Somnus.Common
{
    public  class EncryptString
    {
        private static byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        private static string Key = "1qaz2ws!!";
        
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="ClearText">要加密的串</param>
        /// <returns>加密后的串</returns>
        private static string _Encrypt(string ClearText)
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(Key.Substring(0, 8));
            byte[] rgbIV = IV;
            byte[] clearTextArray = Encoding.UTF8.GetBytes(ClearText);

            DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(clearTextArray, 0, clearTextArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }


        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="ClearText">要加密的串</param>
        /// <returns>加密后的串</returns>
        public static String Encrypt(String ClearText)
        {
            string en_string = string.Empty;
            try
            {
                en_string = _Encrypt(ClearText);
                //en_string = _Encrypt(en_string);
                //en_string = _Encrypt(en_string);

            }
            catch 
            {
                ;
            }
            return en_string;
        }


        /// <summary>
        /// 解密串
        /// </summary>
        /// <param name="DecryptText">加密后的串</param>
        /// <returns></returns>
        private static String _Decrypt(string DecryptText)
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(Key.Substring(0, 8));
            byte[] rgbIV = IV;
            byte[] decryptArray = Convert.FromBase64String(DecryptText);
            DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(decryptArray, 0, decryptArray.Length);
            cStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(mStream.ToArray());
        }

        /// <summary>
        /// 解密串
        /// </summary>
        /// <param name="DecryptText">加密后的串</param>
        /// <returns></returns>
        public static String Decrypt(String DecryptText)
        {
            string de_str = string.Empty;
            try
            {
                de_str = _Decrypt(DecryptText);
                //de_str = _Decrypt(de_str);
                //de_str = _Decrypt(de_str);
            }
            catch
            {
                ;
            }
            return de_str;
        }


        public static string UrlEncrypt(string encryptText)
        {
            return HttpContext.Current.Server.UrlEncode(Encrypt(encryptText));
        }
        /// <summary>
        ///  注意,在Request["param"]时候不能调用此方法. 因为Request["param"]的时候,已经UrlDecode一次了
        /// </summary>
        /// <param name="decryptText"></param>
        /// <returns></returns>
        public static string UrlDecrypt(string decryptText)
        {
            return Encrypt(HttpContext.Current.Server.UrlDecode(decryptText));
        }
    }
}
