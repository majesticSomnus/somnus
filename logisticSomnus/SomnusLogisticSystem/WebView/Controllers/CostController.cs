using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SomnusLogistic.Model;
using SomnusLogistic.Service;
using Somnus.Common;
using Somnus.Common.Helper;
using Webdiyer.WebControls.Mvc;
using WebView.Core;

namespace WebView.Controllers
{
    //[ValidateLogin]
    public class CostController : BaseController
    {
        private IList<UserInfo> userInfo = UserInfoServant.Select(null, false);
        private List<OperDepartment> operDepartmentList = Common.OperDepartmentList;
        //
        // GET: /Cost/
        public ActionResult Index(string beginDate, string endDate, int operDepartmentID = 0, int id = 1)
        {
            try
            {
                ViewBag.OperDepartmentID = new SelectList(operDepartmentList, "ID", "Name");
                Hashtable param = new Hashtable();
                if (string.IsNullOrEmpty(beginDate) == false)
                {
                    param.Add("BeginDate", beginDate);
                }
                if (string.IsNullOrEmpty(endDate) == false)
                {
                    param.Add("EndDate", endDate);
                }
                if (operDepartmentID != 0)
                {
                    param.Add("OperDepartmentID", operDepartmentID);
                }
                List<Cost> list = CostServant.Select(param, true) as List<Cost>;
                list.ForEach(ct =>
                {
                    OperDepartment odm = operDepartmentList.Where(o => o.ID == ct.OperDepartmentID).FirstOrDefault();
                    if (odm != null)
                    {
                        ct.OperDepartment = odm;
                    }
                });
                PagedList<Cost> _list = new PagedList<Cost>(list, id, WebSetting.PageCount);
                if (Request.IsAjaxRequest())
                {
                    return PartialView("UCJqCostList", _list);
                }
                return View(_list);
            }
            catch (Exception ex)
            {
                LogHelper.WriteErr(ex);
                throw ex;
            }
        }

        //
        // GET: /Cost/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Cost/Create

        public ActionResult Create()
        {
            ViewBag.RatifyID = new SelectList(userInfo, "UserID", "RealName");
            ViewBag.ExecutorID = new SelectList(userInfo, "UserID", "RealName");
            ViewBag.OperDepartmentID = new SelectList(operDepartmentList, "ID", "Name");
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "无发票", Value = "0" });
            items.Add(new SelectListItem { Text = "有发票", Value = "1" });
            ViewBag.HasInvoice = items;
            return View();
        }

        //
        // POST: /Cost/Create

        [HttpPost]
        public ActionResult Create(Cost cost)
        {
            try
            {
                cost.IsHasInvoice = cost.HasInvoice == 1 ? true : false;
                cost.UserID = user.UserID;
                cost.CreateDate = DateTime.Now;
                CostServant.Insert(cost);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogHelper.WriteErr(ex);
                throw ex;
            }
        }

        //
        // GET: /Cost/Edit/5

        public ActionResult Edit(int id)
        {
            try
            {
                Cost cost = CostServant.Get(id, false);
                ViewBag.RatifyID = new SelectList(userInfo, "UserID", "RealName", cost.RatifyID);
                ViewBag.ExecutorID = new SelectList(userInfo, "UserID", "RealName", cost.ExecutorID);
                ViewBag.OperDepartmentID = new SelectList(operDepartmentList, "ID", "Name", cost.OperDepartmentID);
                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "无发票", Value = "0", Selected = cost.IsHasInvoice });
                items.Add(new SelectListItem { Text = "有发票", Value = "1", Selected = cost.IsHasInvoice });
                ViewBag.HasInvoice = items;
                return View(cost);
            }
            catch (Exception ex)
            {
                LogHelper.WriteErr(ex);
                throw ex;
            }

        }

        //
        // POST: /Cost/Edit/5

        [HttpPost]
        public ActionResult Edit(Cost cost, FormCollection collection)
        {
            try
            {
                cost.IsHasInvoice = cost.HasInvoice == 1 ? true : false;
                CostServant.Update(cost);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogHelper.WriteErr(ex);
                throw ex;
            }
        }

        //
        // GET: /Cost/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Cost/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            bool flag = false;
            try
            {
                CostServant.Delete(id);
                flag = true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteErr(ex);
                throw ex;
            }
            return Json(flag);
        }


        public ActionResult Chart()
        {
            ViewBag.OperDepartmentID = new SelectList(operDepartmentList, "ID", "Name");
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "天", Value = "0", Selected = true });
            items.Add(new SelectListItem { Text = "月", Value = "1" });
            items.Add(new SelectListItem { Text = "年", Value = "2" });
            ViewBag.ShowID = items;
            List<SelectListItem> showExecutorItems = new List<SelectListItem>();
            showExecutorItems.Add(new SelectListItem { Text = "不显示", Value = "0", Selected = true });
            showExecutorItems.Add(new SelectListItem { Text = "显示", Value = "1" });
            ViewBag.showExecutor = showExecutorItems;
            Hashtable param = new Hashtable();
            //todo:这里需要改下
             //param.Add("Top", 30);
            //数据库中最近30条记录
            List<Cost> costList = CostServant.Select(param, true) as List<Cost>;
            //找到他们的日期集合
            List<DateTime> dateList = costList.OrderByDescending(c => c.AddDate)
                .GroupBy(c => c.AddDate)
                .Select(c => c.Key).ToList<DateTime>();
            List<CostStat> dayMoneyList = new List<CostStat>();
            foreach (DateTime date in dateList)
            {
                decimal money = costList.Where(c => c.AddDate == date).Sum(c => c.Money);
                dayMoneyList.Add(new CostStat
                {
                    Date = date.ToString("yyyy-MM-dd"),
                    Money = money
                });
            }
            decimal totalDateMoney = dayMoneyList.Sum(ct => ct.Money);
            ViewBag.TotalDateMoney = totalDateMoney;

            ViewBag.StatList = dayMoneyList;
            ViewBag.XML = Common.GenStatXml(dayMoneyList,0, "0");
            return View();
        }

        [HttpPost]
        public ActionResult Chart(FormCollection collection)
        {
            int dptID = string.IsNullOrEmpty(collection["OperDepartmentID"]) ? 0 : int.Parse(collection["OperDepartmentID"]);
            string showID = string.IsNullOrEmpty(collection["ShowID"]) ? string.Empty : collection["ShowID"];
            string showExcetor = string.IsNullOrEmpty(collection["ShowExecutor"]) ? string.Empty : collection["ShowExecutor"];
            string beginDate = string.IsNullOrEmpty(collection["beginDate"]) ? string.Empty : collection["beginDate"];
            string endDate = string.IsNullOrEmpty(collection["endDate"]) ? string.Empty : collection["endDate"];
            ViewBag.OperDepartmentID = new SelectList(operDepartmentList, "ID", "Name", dptID);
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "天", Value = "0", Selected = showID.Equals("0") });
            items.Add(new SelectListItem { Text = "月", Value = "1", Selected = showID.Equals("1") });
            items.Add(new SelectListItem { Text = "年", Value = "2", Selected = showID.Equals("2") });
            ViewBag.ShowID = items;
            List<SelectListItem> showExecutorItems = new List<SelectListItem>();
            showExecutorItems.Add(new SelectListItem { Text = "不显示", Value = "0", Selected = showExcetor.Equals("0") });
            showExecutorItems.Add(new SelectListItem { Text = "显示", Value = "1", Selected = showExcetor.Equals("1") });
            ViewBag.showExecutor = showExecutorItems;
            ViewBag.BeginDate = beginDate;
            ViewBag.EndDate = endDate;

            Hashtable param = new Hashtable();
            if (dptID != 0)
            {
                param.Add("OperDepartmentID", dptID);
            }
            if (string.IsNullOrEmpty(beginDate) == false)
            {
                param.Add("BeginDate", DateTime.Parse(beginDate));
            }
            if (string.IsNullOrEmpty(endDate) == false)
            {
                param.Add("EndDate", DateTime.Parse(endDate));
            }
            List<Cost> costList = CostServant.Select(param, true) as List<Cost>;

            //执行人集合 
            List<UserInfo> executorList = new List<UserInfo>();
            if (showExcetor.Equals("1"))
            {
                List<int> executorIDList = costList.GroupBy(c => c.ExecutorID)
                    .Select(c => c.Key).ToList<int>();
                executorIDList.ForEach(delegate(int id)
                {
                    int executorID = id;
                    Cost cost = costList.Where(c => c.ExecutorID == executorID).FirstOrDefault();
                    executorList.Add(cost.ExecutorInfo);
                });
            }
            List<CostStat> moneyList = new List<CostStat>();
            //天
            List<DateTime> dayDateList = costList.OrderByDescending(c => c.AddDate)
                .GroupBy(c => c.AddDate)
                .Select(c => c.Key).ToList<DateTime>();
            //月
            List<DateTime> monthDateList = new List<DateTime>();
            //年
            List<DateTime> yearDateList = new List<DateTime>();
            dayDateList.ForEach(delegate(DateTime model)
            {
                DateTime date = model;
                DateTime month = new DateTime(date.Year, date.Month, 1);
                DateTime year = new DateTime(date.Year, 1, 1);
                if (monthDateList.Count == 0)
                {
                    monthDateList.Add(month);
                }
                else
                {
                    if (monthDateList.Contains(month) == false)
                    {
                        monthDateList.Add(month);
                    }
                }
                if (yearDateList.Count == 0)
                {
                    yearDateList.Add(year);
                }
                else
                {
                    if (yearDateList.Contains(year) == false)
                    {
                        yearDateList.Add(year);
                    }
                }
            });
            //天月年
            switch (showID)
            {
                case "0"://天
                    if (executorList.Count == 0)
                    {//不显示执行人
                        foreach (DateTime date in dayDateList)
                        {
                            decimal money = costList.Where(c => c.AddDate == date)
                                .Sum(c => c.Money);
                            moneyList.Add(new CostStat
                            {
                                Date = date.ToString("yyyy-MM-dd"),
                                Money = money
                            });
                        }
                    }
                    else
                    { //显示执行人 
                        foreach (DateTime date in dayDateList)
                        {
                            executorList.ForEach(delegate(UserInfo uInfo)
                              {
                                  UserInfo model = uInfo;
                                  decimal money = costList.Where(c => c.AddDate == date
                                      && c.ExecutorID == model.UserID)
                                      .Sum(c => c.Money);
                                  if (money > 0)
                                  {
                                      moneyList.Add(new CostStat
                                      {
                                          Date = date.ToString("yyyy-MM-dd"),
                                          ExecutorInfo = model,
                                          Money = money
                                      });
                                  }
                              });
                        }
                    }
                    break;
                case "1"://月
                    if (executorList.Count == 0)
                    {//不显示执行人
                        foreach (DateTime monthDate in monthDateList)
                        {
                            decimal money = costList.Where(c => c.AddDate.Year == monthDate.Year
                                && c.AddDate.Month == monthDate.Month)
                                .Sum(c => c.Money);
                            moneyList.Add(new CostStat
                            {
                                Date = string.Format("{0}年{1}月", monthDate.Year, monthDate.Month),
                                Money = money
                            });
                        }
                    }
                    else
                    {
                        foreach (DateTime monthDate in monthDateList)
                        {
                            executorList.ForEach(delegate(UserInfo uInfo)
                                   {
                                       UserInfo model = uInfo;
                                       decimal money = costList.Where(c => c.AddDate.Year == monthDate.Year
                                           && c.AddDate.Month == monthDate.Month
                                           && c.ExecutorID == model.UserID)
                                           .Sum(c => c.Money);
                                       if (money > 0)
                                       {
                                           moneyList.Add(new CostStat
                                           {
                                               Date = string.Format("{0}年{1}月", monthDate.Year, monthDate.Month),
                                               ExecutorInfo = model,
                                               Money = money
                                           });
                                       }
                                   });
                        }
                    }
                    break;
                case "2"://年
                    if (executorList.Count == 0)
                    {//不显示执行人
                        foreach (DateTime yearDate in yearDateList)
                        {
                            decimal money = costList.Where(c => c.AddDate.Year == yearDate.Year)
                                .Sum(c => c.Money);
                            moneyList.Add(new CostStat
                            {
                                Date = string.Format("{0}年", yearDate.Year),
                                Money = money
                            });
                        }
                    }
                    else
                    {
                        foreach (DateTime yearDate in yearDateList)
                        {
                            executorList.ForEach(delegate(UserInfo uInfo)
                                          {
                                              UserInfo model = uInfo;
                                              decimal money = costList.Where(c => c.AddDate.Year == yearDate.Year
                                                                                  && c.ExecutorID == model.UserID)
                                                  .Sum(c => c.Money);
                                              if (money > 0)
                                              {
                                                  moneyList.Add(new CostStat
                                                  {
                                                      Date = string.Format("{0}年", yearDate.Year),
                                                      ExecutorInfo = model,
                                                      Money = money
                                                  });
                                              }
                                          });
                        }
                    }
                    break;
            }

            decimal totalDateMoney = moneyList.Sum(ct => ct.Money);
            ViewBag.TotalDateMoney = totalDateMoney;
            ViewBag.StatList = moneyList;
            bool isShowExecutor = showExcetor.Equals("1") ? true : false;
            ViewBag.IsShowExecutor = isShowExecutor;
            ViewBag.DptID = dptID;
            if (isShowExecutor)
            {
                ViewBag.XML = Common.GenStatXml(moneyList,dptID, showID, true);
            }
            else
            {
                ViewBag.XML = Common.GenStatXml(moneyList,dptID, showID);
            }
            return View();
        }
    }
}
