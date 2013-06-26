using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Xml;
using SomnusLogistic.Model;


namespace WebView.Core
{
    public class Common
    {
        /// <summary>
        /// 默认
        /// </summary>
        /// <param name="list"></param>
        /// <param name="showID"></param>
        /// <returns></returns>
        public static string GenStatXml(List<CostStat> list, int operDepartmentID, string showID)
        {
            if (list == null || list.Count == 0)
            {
                return string.Empty;
            }
            OperDepartment dpt = null;
            if (operDepartmentID != 0)
            {
                List<OperDepartment> operDepartmentList = Common.OperDepartmentList;
                dpt = operDepartmentList.Where(o => o.ID == operDepartmentID).FirstOrDefault();
                if (dpt != null)
                {
                    list.ForEach(ct =>
                    {
                        ct.OperDepartment = dpt;
                    });
                }
            }
            StringBuilder sbCategories = new StringBuilder();
            StringBuilder sbMoney = new StringBuilder();
            for (int i =list.Count-1; i >=0; i--)
            {
                CostStat stat = list[i];
                if (stat.Money == 0)
                {
                    continue;
                }
                sbCategories.Append(" <category label='" + stat.Date + "'/>");
                sbMoney.Append(" <set value='" + stat.Money + "'/>");
            }
            string show = "日";
            if (showID.Equals("1"))
            {
                show = "月";
            }
            if (showID.Equals("2"))
            {
                show = "年";
            }
            string title = string.Format("{1}费用按{0}统计汇总", show, dpt == null ? string.Empty : dpt.Name + "--");
            StringBuilder xmlData = new StringBuilder();
            xmlData.Append("<chart palette='1' caption='" + title + "' shownames='1' sYAxisValuesDecimals='2'  showvalues='0'  numberPrefix='￥'  showSum='1' decimals='0' connectNullData='0'  numDivLines='4' formatNumberScale='0'>");
            xmlData.Append("<categories>" + sbCategories.ToString() + "</categories>");
            xmlData.Append("<dataset seriesName='费用'  color='AFD8F8'  showValues='0'>" + sbMoney + "</dataset>");

            xmlData.Append("<styles><definition>");
            xmlData.Append("<style name='myCaptionFont' type='font' font='Arial' size='16' color='666666' bold='1' underline='1'/>");
            xmlData.Append("<style name='myShadow' type='Shadow' color='999999' angle='45'/>");
            xmlData.Append("</definition>");
            xmlData.Append("<application>");
            xmlData.Append("<apply toObject='Caption' styles='myCaptionFont,myShadow' />");
            xmlData.Append("</application></styles>");

            xmlData.Append("</chart>");
            return xmlData.ToString();
        }

        public static string GenStatXml(List<CostStat> list,int operDepartmentID,string showID, bool isShowExecutor)
        {
            if (list == null || list.Count == 0)
            {
                return string.Empty;
            }
            OperDepartment dpt = null;
            if (operDepartmentID != 0)
            {
                List<OperDepartment> operDepartmentList = Common.OperDepartmentList;
                dpt = operDepartmentList.Where(o => o.ID == operDepartmentID).FirstOrDefault();
                if (dpt != null)
                {
                    list.ForEach(ct =>
                    {
                        ct.OperDepartment = dpt;
                    });
                }
            }

            //日期集合
            List<string> dateList = list.GroupBy(ct => ct.Date).Select(ct => ct.Key).ToList<string>();
            dateList.Reverse();
            //执行人集合
            List<UserInfo> executorList = new List<UserInfo>();
            if (isShowExecutor)
            {
                List<int> executorIDList = list.GroupBy(ct => ct.ExecutorInfo.UserID)
                       .Select(c => c.Key).ToList<int>();
                executorIDList.ForEach(delegate(int id)
                {
                    int executorID = id;
                    CostStat costStat = list.Where(ct => ct.ExecutorInfo.UserID == executorID).FirstOrDefault();
                    executorList.Add(costStat.ExecutorInfo);
                });
            }
            //http://www.computerhope.com/cgi-bin/htmlcolor.pl
            string[] arrColor = { "AFD8F8", "F6BD0F", "A66EDD", "8BBA00", "EAC117", "D4A017", "F9B7FF", "153E7E" };
            StringBuilder sbCategories = new StringBuilder();
            for (int i = 0,count=dateList.Count; i <count; i++)
            {
                sbCategories.Append(" <category label='" + dateList[i] + "'/>");
            }
            string show = "日";
            if (showID.Equals("1"))
            {
                show = "月";
            }
            if (showID.Equals("2"))
            {
                show = "年";
            }
            string title = string.Format("{1}费用按{0}统计汇总",show,dpt==null?string.Empty:dpt.Name+"--");
            StringBuilder xmlData = new StringBuilder();
            xmlData.Append("<chart palette='1' caption='" + title + "' shownames='1' sYAxisValuesDecimals='2'    showvalues='0' connectNullData='0'  numDivLines='4' formatNumberScale='0'>");
            xmlData.Append("<categories>" + sbCategories.ToString() + "</categories>");

            string strFmt = "<dataset seriesName='{0}'  color='{1}'  showValues='0'>{2}</dataset>";
            for (int i = 0, count = executorList.Count; i < count; i++)
            {
                UserInfo executorInfo = executorList[i];
                StringBuilder sbRise = new StringBuilder();
                dateList.ForEach(delegate(string _date)
                {
                    string date = _date;
                    CostStat stat = list.Where(ct => ct.Date.Equals(date) && ct.ExecutorInfo.UserID == executorInfo.UserID).FirstOrDefault();
                    decimal money = 0;
                    if (stat != null)
                    {
                        money = stat.Money;
                    }
                    string toolText = string.Format("{0},{1},{2}", date, executorInfo.RealName, money);
                    sbRise.Append(" <set value='" + money + "' toolText='" + toolText + "'/>");
                });
                xmlData.AppendFormat(strFmt, executorInfo.RealName, arrColor[i], sbRise);
            }

            xmlData.Append("<styles><definition>");
            xmlData.Append("<style name='myCaptionFont' type='font' font='Arial' size='16' color='666666' bold='1' underline='1'/>");
            xmlData.Append("<style name='myShadow' type='Shadow' color='999999' angle='45'/>");
            xmlData.Append("</definition>");
            xmlData.Append("<application>");
            xmlData.Append("<apply toObject='Caption' styles='myCaptionFont,myShadow' />");
            xmlData.Append("</application></styles>");

            xmlData.Append("</chart>");
          return xmlData.ToString();
        }

        /// <summary>
        /// 作业部集合
        /// </summary>
        public static List<OperDepartment> OperDepartmentList
        {
            get
            {
                List<OperDepartment> list = new List<OperDepartment>();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(HttpContext.Current.Server.MapPath("~/OperDepartmentList.xml"));
                XmlNode xn = xmlDoc.SelectSingleNode("OperDepartmentList");
                XmlNodeList xnl = xn.ChildNodes;
                foreach (XmlNode xnf in xnl)
                {
                    XmlElement xe = (XmlElement)xnf;
                    //Console.WriteLine(xe.GetAttribute("genre"));//显示属性值
                    //Console.WriteLine(xe.GetAttribute("ISBN"));
                    XmlNodeList xnf1 = xe.ChildNodes;
                    OperDepartment odm = new OperDepartment();
                    foreach (XmlNode xn2 in xnf1)
                    {
                        XmlElement xe2 = (XmlElement)xn2;
                        if (xe2.Name == "ID")
                        {
                            odm.ID = int.Parse(xe2.InnerText);
                        }
                        else if (xe2.Name == "Name")
                        {
                            odm.Name = xe2.InnerText;
                        }
                        else if (xe2.Name == "Sort")
                        {
                            odm.Sort = int.Parse(xe2.InnerText);
                        }
                    }
                    list.Add(odm);
                }
                list = list.OrderBy(o => o.Sort).ToList<OperDepartment>();
                return list;
            }


            #region 废弃
            //get {
            //    List<OperDepartment> list = new List<OperDepartment>();
            //    list.Add(new OperDepartment { 
            //        ID=1,
            //        Name = "宿迁作业部",
            //        Sort=0
            //    });
            //    list.Add(new OperDepartment
            //    {
            //        ID = 2,
            //        Name = "宝鸡作业部",
            //        Sort = 0
            //    });
            //    list.Add(new OperDepartment
            //    {
            //        ID = 3,
            //        Name = "望都作业部",
            //        Sort = 0
            //    });
            //    list.Add(new OperDepartment
            //    {
            //        ID = 4,
            //        Name = "通辽作业部",
            //        Sort = 0
            //    });
            //    return list;
            //}
            #endregion
        }
    }

}