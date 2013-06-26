using System;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Caching;
using Somnus.Common;
using Somnus.Common.Helper;


namespace SomnusLogisticWeb.Handlers
{
    /// <summary>
    /// VerifyCode 的摘要说明
    /// </summary>
    public class VerifyCode : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpResponse response = context.Response;
            response.ContentType = "image/gif";
            response.AppendHeader("Pragma", "no-cache");
            response.AppendHeader("Cache-Control", "private, no-cache, no-cache=\"Set-Cookie\", proxy-revalidate");
            response.AppendHeader("Expires", "-1");
            this.CreateCheckCodeImage(GenerateCheckCode(context), context);
        }

        private string GenerateCheckCode(HttpContext context)
        {

            string verifyCode = RadomCode.GenerateNumberCode(4);
            HttpResponse response = context.Response;
            CookieHelper.Write(CommonSettings.VCODE_KEY, verifyCode, 5, CommonSettings.COOKIE_DOMAIN);
            return verifyCode;
        }

        private void CreateCheckCodeImage(string checkCode, HttpContext context)
        {
            HttpResponse response = context.Response;
            if (checkCode == null || checkCode.Trim() == String.Empty)
                return;

            Bitmap image = new Bitmap((int)Math.Ceiling((checkCode.Length * 12.5)), 22);
            Graphics g = Graphics.FromImage(image);

            try
            {
                //生成随机生成器
                Random random = new Random();

                //清空图片背景色
                g.Clear(Color.White);

                //画图片的背景噪音线
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);

                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }

                Font font = new Font("Arial", 12, (FontStyle.Bold | FontStyle.Italic));
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(checkCode, font, brush, 2, 2);

                //画图片的前景噪音点
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);

                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                MemoryStream ms = new MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                response.ClearContent();
                //response.ContentType = "image/gif";
                response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}