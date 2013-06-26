using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Somnus.Common.Helper
{
    public class TextHelper
    {
        #region elem
        /// <summary>
        /// 指示正则表达式在一次匹配时，最多匹配4000个项
        /// </summary>
        public const int MAX_MATCH_COUNT = 4000;
        #endregion

        #region ReplaceP 过滤html开头的<p>和结尾的</p>标签
        /// <summary>
        /// 过滤html开头的<p>和结尾的</p>标签
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string ReplaceP(string html)
        {
            if (string.IsNullOrEmpty(html))
            {
                return null;
            }
            Regex reg = new Regex("^<p>|</p>$", RegexOptions.IgnoreCase);
            string _html = reg.Replace(html, string.Empty);
            return Regex.Replace(_html, @"<br />", string.Empty);
        }
        #endregion

        #region FormattingSingelQuote 单引号转义
        /// <summary>
        /// 单引号转义
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string FormattingSingelQuote(string html)
        {
            if (string.IsNullOrEmpty(html))
            {
                return null;
            }

            Regex reg = new Regex("'", RegexOptions.Multiline);
            string _html = reg.Replace(html, "\\'");
            return _html;
        }
        #endregion

        #region FormattingQuote 双引号转义
        /// <summary>
        /// 双引号转义
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string FormattingQuote(string html)
        {
            if (string.IsNullOrEmpty(html))
            {
                return null;
            }

            Regex reg = new Regex("\"", RegexOptions.Multiline);
            string _html = reg.Replace(html, "\\\"");
            return _html;
        }
        #endregion

        #region 过滤 iframe,js,style,div,a标签内容
        /// <summary>
        /// 清除html文档中的js代码和样式 iframe
        /// 过滤链接,去掉注释 去掉<div >和</div>
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string ClearScriptStyle(string html)
        {
            if (html == null)
                return null;

            Regex regex = new Regex("<(script|style|iframe).*?>.*?</(script|style|iframe).*?>|(<a.*?>|</a>)|(<!--.*?-->)|(<div.*?>|</div>)|(<span.*?>|</span>)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            return regex.Replace(html, string.Empty);
        }
        #endregion


        #region GetSubstring
        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="text"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string GetSubstring(string text, int len)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            len = len > text.Length ? text.Length : len;
            return text.Substring(0, len);
        }
        #endregion

        #region ConvertStr  将字符串从from编码转到to编码
        /// <summary>
        /// 将字符串从from编码转到to编码
        /// </summary>
        /// <param name="str">源串</param>
        /// <param name="from">源编码</param>
        /// <param name="to">目标编码</param>
        /// <returns></returns>
        public static string ConvertStr(string str, Encoding from, Encoding to)
        {
            string result = to.GetString(from.GetBytes(str));
            return result;
        }
        #endregion

        #region IndexCode 取拼音
        /// <summary>
        /// 取拼音
        /// </summary>
        /// <param name="IndexTxt"></param>
        /// <returns></returns>
        public static String IndexCode(String IndexTxt)
        {
            String _Temp = null;
            try
            {
                for (int i = 0; i < IndexTxt.Length; i++)
                {
                    _Temp = _Temp + GetOneIndex(IndexTxt.Substring(i, 1));
                }
            }
            catch
            {
            }
            return _Temp;
        }
        #endregion

        #region GetOneIndex 得到单个字符的首字母
        /// <summary>
        /// 得到单个字符的首字母
        /// </summary>
        /// <param name="OneIndexTxt"></param>
        /// <returns></returns>
        private static String GetOneIndex(String OneIndexTxt)
        {
            if (Convert.ToChar(OneIndexTxt) >= 0 && Convert.ToChar(OneIndexTxt) < 256)
                return OneIndexTxt;
            else
            {
                Encoding gb2312 = Encoding.GetEncoding("gb2312");
                byte[] unicodeBytes = Encoding.Unicode.GetBytes(OneIndexTxt);
                byte[] gb2312Bytes = Encoding.Convert(Encoding.Unicode, gb2312, unicodeBytes);
                return GetX(Convert.ToInt32(
                                String.Format("{0:D2}", Convert.ToInt16(gb2312Bytes[0]) - 160)
                                + String.Format("{0:D2}", Convert.ToInt16(gb2312Bytes[1]) - 160)
                                ));
            }

        }
        #endregion

        #region  GetX 根据区位得到首字母
        /// <summary>
        /// 根据区位得到首字母
        /// </summary>
        /// <param name="GBCode"></param>
        /// <returns></returns>
        private static String GetX(int GBCode)
        {
            if (GBCode >= 1601 && GBCode < 1637) return "A";
            if (GBCode >= 1637 && GBCode < 1833) return "B";
            if (GBCode >= 1833 && GBCode < 2078) return "C";
            if (GBCode >= 2078 && GBCode < 2274) return "D";
            if (GBCode >= 2274 && GBCode < 2302) return "E";
            if (GBCode >= 2302 && GBCode < 2433) return "F";
            if (GBCode >= 2433 && GBCode < 2594) return "G";
            if (GBCode >= 2594 && GBCode < 2787) return "H";
            if (GBCode >= 2787 && GBCode < 3106) return "J";
            if (GBCode >= 3106 && GBCode < 3212) return "K";
            if (GBCode >= 3212 && GBCode < 3472) return "L";
            if (GBCode >= 3472 && GBCode < 3635) return "M";
            if (GBCode >= 3635 && GBCode < 3722) return "N";
            if (GBCode >= 3722 && GBCode < 3730) return "O";
            if (GBCode >= 3730 && GBCode < 3858) return "P";
            if (GBCode >= 3858 && GBCode < 4027) return "Q";
            if (GBCode >= 4027 && GBCode < 4086) return "R";
            if (GBCode >= 4086 && GBCode < 4390) return "S";
            if (GBCode >= 4390 && GBCode < 4558) return "T";
            if (GBCode >= 4558 && GBCode < 4684) return "W";
            if (GBCode >= 4684 && GBCode < 4925) return "X";
            if (GBCode >= 4925 && GBCode < 5249) return "Y";
            if (GBCode >= 5249 && GBCode <= 5589) return "Z";
            if (GBCode >= 5601 && GBCode <= 8794)
            {
                String CodeData = @"cjwgnspgcenegypbtwxzdxykygtpjnmjqmbsgzscyjsyyfpggbzgydywjkgaljswkbjqhyjwpdzlsgmr
ybywwccgznkydgttngjeyekzydcjnmcylqlypyqbqrpzslwbdgkjfyxjwcltbncxjjjjcxdtqsqzycdxxhgckbphffss
pybgmxjbbyglbhlssmzmpjhsojnghdzcdklgjhsgqzhxqgkezzwymcscjnyetxadzpmdssmzjjqjyzcjjfwqjbdzbjgd
nzcbwhgxhqkmwfbpbqdtjjzkqhylcgxfptyjyyzpsjlfchmqshgmmxsxjpkdcmbbqbefsjwhwwgckpylqbgldlcctnma
eddksjngkcsgxlhzaybdbtsdkdylhgymylcxpycjndqjwxqxfyyfjlejbzrwccqhqcsbzkymgplbmcrqcflnymyqmsqt
rbcjthztqfrxchxmcjcjlxqgjmshzkbswxemdlckfsydsglycjjssjnqbjctyhbftdcyjdgwyghqfrxwckqkxebpdjpx
jqsrmebwgjlbjslyysmdxlclqkxlhtjrjjmbjhxhwywcbhtrxxglhjhfbmgykldyxzpplggpmtcbbajjzyljtyanjgbj
flqgdzyqcaxbkclecjsznslyzhlxlzcghbxzhznytdsbcjkdlzayffydlabbgqszkggldndnyskjshdlxxbcghxyggdj
mmzngmmccgwzszxsjbznmlzdthcqydbdllscddnlkjyhjsycjlkohqasdhnhcsgaehdaashtcplcpqybsdmpjlpcjaql
cdhjjasprchngjnlhlyyqyhwzpnccgwwmzffjqqqqxxaclbhkdjxdgmmydjxzllsygxgkjrywzwyclzmcsjzldbndcfc
xyhlschycjqppqagmnyxpfrkssbjlyxyjjglnscmhcwwmnzjjlhmhchsyppttxrycsxbyhcsmxjsxnbwgpxxtaybgajc
xlypdccwqocwkccsbnhcpdyznbcyytyckskybsqkkytqqxfcwchcwkelcqbsqyjqcclmthsywhmktlkjlychwheqjhtj
hppqpqscfymmcmgbmhglgsllysdllljpchmjhwljcyhzjxhdxjlhxrswlwzjcbxmhzqxsdzpmgfcsglsdymjshxpjxom
yqknmyblrthbcftpmgyxlchlhlzylxgsssscclsldclepbhshxyyfhbmgdfycnjqwlqhjjcywjztejjdhfblqxtqkwhd
chqxagtlxljxmsljhdzkzjecxjcjnmbbjcsfywkbjzghysdcpqyrsljpclpwxsdwejbjcbcnaytmgmbapclyqbclzxcb
nmsggfnzjjbzsfqyndxhpcqkzczwalsbccjxpozgwkybsgxfcfcdkhjbstlqfsgdslqwzkxtmhsbgzhjcrglyjbpmljs
xlcjqqhzmjczydjwbmjklddpmjegxyhylxhlqyqhkycwcjmyhxnatjhyccxzpcqlbzwwwtwbqcmlbmynjcccxbbsnzzl
jpljxyztzlgcldcklyrzzgqtgjhhgjljaxfgfjzslcfdqzlclgjdjcsnclljpjqdcclcjxmyzftsxgcgsbrzxjqqcczh
gyjdjqqlzxjyldlbcyamcstylbdjbyregklzdzhldszchznwczcllwjqjjjkdgjcolbbzppglghtgzcygezmycnqcycy
hbhgxkamtxyxnbskyzzgjzlqjdfcjxdygjqjjpmgwgjjjpkjsbgbmmcjssclpqpdxcdyykypcjddyygywchjrtgcnyql
dkljczzgzccjgdyksgpzmdlcphnjafyzdjcnmwescsglbtzcgmsdllyxqsxsbljsbbsgghfjlwpmzjnlyywdqshzxtyy
whmcyhywdbxbtlmswyyfsbjcbdxxlhjhfpsxzqhfzmqcztqcxzxrdkdjhnnyzqqfnqdmmgnydxmjgdhcdycbffallztd
ltfkmxqzdngeqdbdczjdxbzgsqqddjcmbkxffxmkdmcsychzcmljdjynhprsjmkmpcklgdbqtfzswtfgglyplljzhgjj
gypzltcsmcnbtjbhfkdhbyzgkpbbymtdlsxsbnpdkleycjnycdykzddhqgsdzsctarlltkzlgecllkjljjaqnbdggghf
jtzqjsecshalqfmmgjnlyjbbtmlycxdcjpldlpcqdhsycbzsckbzmsljflhrbjsnbrgjhxpdgdjybzgdlgcsezgxlblg
yxtwmabchecmwyjyzlljjshlgndjlslygkdzpzxjyyzlpcxszfgwyydlyhcljscmbjhblyjlycblydpdqysxktbytdkd
xjypcnrjmfdjgklccjbctbjddbblblcdqrppxjcglzcshltoljnmdddlngkaqakgjgyhheznmshrphqqjchgmfprxcjg
dychghlyrzqlcngjnzsqdkqjymszswlcfqjqxgbggxmdjwlmcrnfkkfsyyljbmqammmycctbshcptxxzzsmphfshmclm
ldjfyqxsdyjdjjzzhqpdszglssjbckbxyqzjsgpsxjzqznqtbdkwxjkhhgflbcsmdldgdzdblzkycqnncsybzbfglzzx
swmsccmqnjqsbdqsjtxxmbldxcclzshzcxrqjgjylxzfjphymzqqydfqjjlcznzjcdgzygcdxmzysctlkphtxhtlbjxj
lxscdqccbbqjfqzfsltjbtkqbsxjjljchczdbzjdczjccprnlqcgpfczlclcxzdmxmphgsgzgszzqjxlwtjpfsyaslcj
btckwcwmytcsjjljcqlwzmalbxyfbpnlschtgjwejjxxglljstgshjqlzfkcgnndszfdeqfhbsaqdgylbxmmygszldyd
jmjjrgbjgkgdhgkblgkbdmbylxwcxyttybkmrjjzxqjbhlmhmjjzmqasldcyxyqdlqcafywyxqhz";
                String _gbcode = GBCode.ToString();
                int pos = (Convert.ToInt16(_gbcode.Substring(0, 2)) - 56) * 94 + Convert.ToInt16(_gbcode.Substring(_gbcode.Length - 2, 2));
                return CodeData.Substring(pos - 1, 1);
            }
            return "";
        }

        #endregion

        #region IsMobileNumber 是否是手机号
        /// <summary>
        /// 是否是手机号
        /// </summary>
        /// <param name="num">手机号</param>
        /// <returns></returns>
        public static bool IsMobileNumber(string num)
        {
            if (string.IsNullOrEmpty(num))
            {
                return false;
            }
            string phone = Regex.Match(num, @"\d{11}$").Value;
            if (Regex.IsMatch(phone, @"\d{11}"))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region FormatStr 过滤掉 \r \t \n &nbsp;
        /// <summary>
        /// 过滤掉 \r \t \n &nbsp;
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FormatStr(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            return str.Replace("\n", string.Empty).Replace("\t", string.Empty).Replace("\r", string.Empty).Replace("&nbsp;", string.Empty);
        }
        #endregion

        #region FormatCRLF
        /// <summary>
        /// 规范化模板和即将要比较的内容，将windows换行符\r\n转换为统一的unix格式的\n
        /// 在模板存入库之前需要全部处理为这种格式的
        /// </summary>
        /// <param name="strtoformat"></param>
        /// <returns></returns>
        public static string FormatCRLF(string strtoformat)
        {
            if (strtoformat == null)
                return strtoformat;


            return strtoformat.Replace("\r\n", "\n");
        }
        #endregion

        #region UnFormatCRLF
        /// <summary>
        /// 将unix格式的字符串，转换为windows格式的，主要征对模板从数据库读出需要编辑的时候
        /// 还源字符串在编辑器中的格式
        /// </summary>
        /// <param name="strtoformat"></param>
        /// <returns></returns>
        public static string UnFormatCRLF(string strtoformat)
        {
            if (strtoformat == null)
                return strtoformat;

            //必须先全部转换为unix格式的，再转换为windows格式的
            return strtoformat.Replace("\r\n", "\n").Replace("\n", "\r\n");
        }
        #endregion

        #region MatchedGroup
        /// <summary>
        /// 返回符合条件的字符串
        /// </summary>
        /// <param name="strToMatch"></param>
        /// <param name="regex"></param>
        /// <returns>匹配的字符串，如果不匹配，则返回null</returns>
        public static string MatchedGroup(string strToMatch, string regex)
        {
            if (strToMatch == null || regex == null)
            {
                return null;
            }
            Match match = Regex.Match(strToMatch, regex, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return match.Value;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region MatchedGroup
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strToMatch"></param>
        /// <param name="regex"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public static string MatchedGroup(string strToMatch, string regex, string groupName)
        {
            if (strToMatch == null || regex == null || groupName == null)
            {
                return null;
            }
            Match match = Regex.Match(strToMatch, regex, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            if (match.Success)
            {
                if (match.Groups[groupName] != null)
                {
                    return match.Groups[groupName].Value;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region StrBetween
        /// <summary>
        /// 找出一个字符串中由start和end包围的一个值
        /// 如果开始和结速字符用 $$包含起来，表示需要把开始和结束标记也放进匹配的内容中
        /// 如果开始的左侧由%开始,表示开始标签和第一个匹配的标签匹配(默许)
        /// 如果开始的右侧由%开始,表示要开始标签和结束标签的左边匹配
        /// 如果结束的左侧由%开始,表示结束标签和最靠近开始标签的匹配
        /// 如果结束右侧由%开始，表示结束标签和离开始标签最远的标签匹配
        /// %只有出现在整个字符串的最初或最必才有效
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string StrBetween(string text, string start, string end)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
                return null;

            bool rightAligh = false;
            bool startLeft = true;
            if (start[0] == '%')
            {
                startLeft = true;
                start = start.Substring(1, start.Length - 1);
            }

            if (start[start.Length - 1] == '%')
            {
                startLeft = false;
                start = start.Substring(0, start.Length - 1);
            }

            if (end[end.Length - 1] == '%')
            {
                rightAligh = true;
                end = end.Substring(0, end.Length - 1);
            }

            if (end[0] == '%')
            {
                rightAligh = false;
                end = end.Substring(1, end.Length - 1);
            }

            bool startInclude = start[0] == '$' && start[start.Length - 1] == '$';
            bool endInclude = end[0] == '$' && end[end.Length - 1] == '$';
            if (startInclude)
            {
                if (start.Length <= 2)
                {
                    return null;
                }
                start = start.Substring(1, start.Length - 2);
            }

            if (endInclude)
            {
                if (end.Length <= 2)
                {
                    return null;
                }
                end = end.Substring(1, end.Length - 2);
            }

            if (start.Length + end.Length >= text.Length)
            {
                return null;
            }

            int startIndex = -1;
            int endIndex = -1;
            int len = 0;

            if (startLeft)
            {
                startIndex = text.IndexOf(start);
                if (startIndex < 0)
                {
                    return null;
                }
                startIndex = startIndex + start.Length;


                if (rightAligh) //右对齐
                {
                    endIndex = text.LastIndexOf(end);
                }
                else
                {
                    endIndex = text.IndexOf(end, startIndex);
                }

                if (endIndex < 0 || endIndex <= startIndex)
                {
                    return null;
                }

                len = endIndex - startIndex;
            }
            else
            {
                if (rightAligh) //右对齐
                {
                    endIndex = text.LastIndexOf(end);
                }
                else
                {
                    endIndex = text.IndexOf(end);
                }

                if (endIndex < 0)
                {
                    return null;
                }

                string temptext = text.Substring(0, endIndex);
                startIndex = temptext.LastIndexOf(start);
                if (startIndex == -1)
                {
                    return null;
                }
                startIndex = startIndex + start.Length;
                len = temptext.Length - startIndex;
            }

            if (len <= 0)
            {
                return null;
            }
            string result = text.Substring(startIndex, len);
            if (startInclude)
                result = start + result;

            if (endInclude)
                result = result + end;

            return result;
        }

        #endregion

        #region StrBetweenList
        /// <summary>
        /// 找出text中，由开始和结束标记指定的所有数据项，并放到List中返回
        ///  如果开始和结速字符用 $$包含起来，表示需要把开始和结束标记也放进匹配的内容中
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static IList<string> StrBetweenList(string text, string start, string end)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
                return null;

            bool startInclude = start[0] == '$' && start[start.Length - 1] == '$';
            bool endInclude = end[0] == '$' && end[end.Length - 1] == '$';
            if (startInclude)
            {
                if (start.Length <= 2)
                {
                    return null;
                }
                start = start.Substring(1, start.Length - 2);
            }

            if (endInclude)
            {
                if (end.Length <= 2)
                {
                    return null;
                }
                end = end.Substring(1, end.Length - 2);
            }

            if (start.Length + end.Length >= text.Length)
            {
                return null;
            }

            IList<string> strList = new List<string>();
            int currentIndex = 0;
            while (currentIndex < text.Length - 1)
            {
                int startIndex = text.IndexOf(start, currentIndex);
                if (startIndex < 0 || (startIndex + end.Length >= text.Length))
                {
                    break;
                }

                int endIndex = text.IndexOf(end, startIndex + start.Length);
                if (endIndex < 0 || (endIndex - startIndex) == 0)
                {
                    break;
                }
                startIndex = startIndex + start.Length;
                string item = text.Substring(startIndex, endIndex - startIndex);
                if (!String.IsNullOrEmpty(item) && item.Trim().Length > 0)
                {
                    strList.Add(item.Trim());
                }
                currentIndex = endIndex + end.Length;
            }

            if (strList.Count > 0)
            {
                return strList;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region SubStr 按字节码截取
        /// <summary>
        ///  按字节码截取
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length">汉字长度</param>
        /// <returns></returns>
        public static String SubStr(object str, int length)
        {
            if (str != null)
            {
                return bSubstring(str.ToString(), length);
            }
            else
            {

                return String.Empty;
            }
        }
        #endregion

        #region bSubstring 根据字节数截取字符串 当最后一个字符是字母或数字，则保留该字符，如果是汉字，说明这个汉字被截了一半，则去掉这个汉字。
        /// <summary>
        ///  根据字节数截取字符串 当最后一个字符是字母或数字，则保留该字符，如果是汉字，说明这个汉字被截了一半，则去掉这个汉字。
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private static String bSubstring(string s, int length)
        {
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(s);
            int n = 0;  //  表示当前的字节数
            int i = 0;  //  要截取的字节数
            for (; i < bytes.GetLength(0) && n < (2 * length); i++)
            {

                //  偶数位置，如0、2、4等，为UCS2编码中两个字节的第一个字节

                if (i % 2 == 0)
                {
                    n++;      //  在UCS2第一个字节时n加1
                }
                else
                {

                    //  当UCS2编码的第二个字节大于0时，该UCS2字符为汉字，一个汉字算两个字节

                    if (bytes[i] > 0)
                    {
                        n++;
                    }
                }

            }

            //  如果i为奇数时，处理成偶数
            if (i % 2 == 1)
            {

                //  该UCS2字符是汉字时，去掉这个截一半的汉字
                if (bytes[i] > 0)

                    i = i - 1;

                 //  该UCS2字符是字母或数字，则保留该字符

                else
                    i = i + 1;
            }
            return System.Text.Encoding.Unicode.GetString(bytes, 0, i);
        }
        #endregion

        #region IsNum 把字符中为数字的部分按先后顺序组合起来返回。包括“,”
        /// <summary>
        ///  把字符中为数字的部分按先后顺序组合起来返回。包括“,”
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string PickUpNum(string str)
        {
            StringBuilder str2 = new StringBuilder(String.Empty);
            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsNumber(str, i))
                {
                    str2.Append(str.Substring(i, 1));
                }
                else if (str.Substring(i, 1) == ",")
                {
                    str2.Append(str.Substring(i, 1));
                }
            }

            return str2.ToString();
        }
        #endregion

        #region m_blnIsNUmber
        /// <summary>
        /// m_blnIsNUmber
        /// </summary>
        /// <param name="p_strVaule"></param>
        /// <returns></returns>
        public static bool m_blnIsNUmber(string p_strVaule)
        {
            if (p_strVaule == "")
            {
                return false;
            }
            Regex regex = new Regex(@"^[0-9]{4}-(((0[13578]|(10|12))-(0[1-9]|[1-2][0-9]|3[0-1]))|(02-(0[1-9]|[1-2][0-9]))|((0[469]|11)-(0[1-9]|[1-2][0-9]|30)))($|\s([0-1]\d|[2][0-3])\:[0-5]\d\:[0-5]\d)");
            return regex.IsMatch(p_strVaule);
        }
        #endregion

        #region m_numCode
        /// <summary>
        /// 判断字符是不是数字
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static bool m_numCode(string num)
        {
            if (String.IsNullOrEmpty(num))
            {
                return false;
            }
            Regex regex = new Regex(@"^[1-9]\d*$");
            return regex.IsMatch(num);
        }
        #endregion

        #region  m_stockCode 判断字符是不是合法的股票代码或者名字
        /// <summary>
        ///  判断字符是不是合法的股票代码或者名字
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool m_stockCode(string code)
        {
            if (code == "")
            {
                return false;
            }
            Regex regex = new Regex("^([a-z]{2})?[0-9]{6}$");
            return regex.IsMatch(code);
        }
        #endregion

        #region IsNumeric判断是否为数字,包括正小数
        /// <summary>
        /// 判断是否为数字,包括正小数
        /// author: wyf
        /// create: 2010.12.27
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumeric(string str)
        {
            if (String.IsNullOrEmpty(str)) return false;

            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            byte[] bytestr = ascii.GetBytes(str);
            foreach (byte c in bytestr)
            {
                if ((c < 48 && c != 46) || c > 57)
                {
                    return false;
                }

            }

            return true;

        }
        #endregion

    }

}
