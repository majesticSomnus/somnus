using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Somnus.Common
{
   public class CalcData
    {
        /// <summary>
        /// 四舍五入方法
        /// 描述:适合数据统计【正负数都四舍五入】
        /// </summary>
        /// <param name="value">要四舍五入的数</param>
        /// <param name="decimals">要保留的小数点后位数</param>
        /// <returns></returns>
        public static float Round_Data(double value, int decimals)
        {
            if (decimals < 0)
                decimals = 0;
            if (value >= 0)
            {
                value += 5 * Math.Pow(10, -(decimals + 1));
            }
            else
            {
                value += -5 * Math.Pow(10, -(decimals + 1));
            }
            string str = value.ToString();
            string[] strs = str.Split('.');
            int idot = str.IndexOf('.');
            if (idot == -1)//没有小数点时就直接返回值[如运行Round_Data(1955.50,0)]，避免出错
                return float.Parse(str);
            string prestr = strs[0];
            string poststr = strs[1];
            if (poststr.Length > decimals)
            {
                poststr = str.Substring(idot + 1, decimals);
            }
            string strd = prestr + "." + poststr;
            value = float.Parse(strd);
            return (float)value;
        }
    }
}
