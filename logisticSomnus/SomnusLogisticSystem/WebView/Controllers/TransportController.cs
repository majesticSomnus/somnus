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
    public class TransportController : BaseController
    {
        private IList<Customer> customerList = CustomerServant.Select(null, false);
        private List<OperDepartment> operDepartmentList = Common.OperDepartmentList;
        // GET: /Transport/
        public ActionResult Index(int operDepartmentID = 0, int customerID = 0)
        {
            try
            {
                List<Customer> cList = new List<Customer>();
                if (operDepartmentID != 0)
                {
                    cList = customerList.Where(c => c.OperDepartmentID == operDepartmentID).ToList<Customer>();
                    ViewBag.OperDepartmentID = new SelectList(operDepartmentList, "ID", "Name", operDepartmentID);
                    if (customerID != 0)
                    {
                        ViewBag.CustomerID = new SelectList(cList, "CustomerID", "CustomerName", customerID);
                    }
                    else
                    {
                        ViewBag.CustomerID = new SelectList(cList, "CustomerID", "CustomerName");
                    }
                }
                else
                {
                    ViewBag.OperDepartmentID = new SelectList(operDepartmentList, "ID", "Name");
                    ViewBag.CustomerID = new SelectList(new List<Customer>(), "CustomerID", "CustomerName");
                    customerID = 0;
                }
                Hashtable param = new Hashtable();
                if (operDepartmentID != 0)
                {
                    param.Add("OperDepartmentID", operDepartmentID);
                }
                if (customerID != 0)
                {
                    param.Add("CustomerID", customerID);
                }
                List<TransPort> transList = TransportServant.Select(param, true) as List<TransPort>;
                transList.ForEach(ts =>
                {
                    OperDepartment odm = operDepartmentList.Where(o => o.ID == ts.Customer.OperDepartmentID).FirstOrDefault();
                    if (odm != null)
                    {
                        ts.Customer.OperDepartment = odm;
                    }
                });
                return View(transList);
            }
            catch (Exception ex)
            {
                LogHelper.WriteErr(ex);
                throw;
            }
        }

        //
        // GET: /Transport/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Transport/Create

        public ActionResult Create()
        {
            ViewBag.OperDepartmentID = new SelectList(operDepartmentList, "ID", "Name");
            ViewBag.CustomerID = new SelectList(new List<Customer>(), "CustomerID", "CustomerName");
            return View();
        }

        // POST: /Transport/Create

        [HttpPost]
        public ActionResult Create(TransPort trans)
        {
            try
            {
                trans.UserID = user.UserID;
                trans.CreateDate = DateTime.Now;
                TransportServant.Insert(trans);
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                LogHelper.WriteErr(ex);
                throw ex;

            }
        }

        //
        // GET: /Transport/Edit/5

        public ActionResult Edit(int id)
        {
            try
            {
                TransPort trans = TransportServant.Get(id, true);
                ViewBag.OperDepartmentID = new SelectList(operDepartmentList, "ID", "Name", trans.Customer.OperDepartmentID);
                List<Customer> cList = customerList.Where(c => c.OperDepartmentID == trans.Customer.OperDepartmentID).ToList<Customer>();
                ViewBag.CustomerID = new SelectList(cList, "CustomerID", "CustomerName", trans.CustomerID);
                if (trans.ArriveDate == new DateTime())
                {
                    trans.ArriveDate = DateTime.Now.Date;
                }
                return View(trans);
            }
            catch (Exception ex)
            {
                LogHelper.WriteErr(ex);
                throw ex;
            }
        }

        //
        // POST: /Transport/Edit/5

        [HttpPost]
        public ActionResult Edit(TransPort trans)
        {
            try
            {
                TransportServant.Update(trans);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogHelper.WriteErr(ex);
                throw ex;
            }
        }

        //
        // GET: /Transport/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Transport/Delete/5

        [HttpPost]
        public ActionResult Delete(int id,FormCollection collection)
        {
            bool flag = false;
            try
            {
                TransportServant.Delete(id);
                flag = true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteErr(ex);
            }
            return Json(flag);
        }

        [HttpPost]
        public ActionResult SelectCustomer(int dptID)
        {
            List<Customer> cList = customerList.Where(c=>c.OperDepartmentID==dptID).ToList<Customer>();
            return Json(cList);
        }
    }
}
