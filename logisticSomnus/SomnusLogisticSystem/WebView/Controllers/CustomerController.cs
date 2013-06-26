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
    public class CustomerController : BaseController
    {
        private List<OperDepartment> operDepartmentList = Common.OperDepartmentList;

        // GET: /Customer/
        public ActionResult Index(int id = 1, int operDepartmentID = 0)
        {
            try
            {
                ViewBag.OperDepartmentID = new SelectList(operDepartmentList, "ID", "Name");
                Hashtable param = new Hashtable();
                if (operDepartmentID != 0)
                {
                    param.Add("OperDepartmentID", operDepartmentID);
                }
                List<Customer> list = CustomerServant.Select(param, false) as List<Customer>;
                list.ForEach(c =>
                {
                    OperDepartment odm = operDepartmentList.Where(o => o.ID == c.OperDepartmentID).FirstOrDefault();
                    if (odm != null)
                    {
                        c.OperDepartment = odm;
                    }
                });
                PagedList<Customer> _list = new PagedList<Customer>(list, id, WebSetting.PageCount);
                if (Request.IsAjaxRequest())
                {
                    return PartialView("UCJqCustomerList", _list);
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
        // GET: /Customer/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Customer/Create

        public ActionResult Create()
        {
            ViewBag.OperDepartmentID = new SelectList(operDepartmentList, "ID", "Name");
            return View();
        }

        //
        // POST: /Customer/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection, Customer customer)
        {
            try
            {
                customer.UserID = base.user.UserID;
                customer.CreateDate = DateTime.Now;
                CustomerServant.Insert(customer);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                LogHelper.WriteErr(ex);
                return View();
            }
        }

        //
        // GET: /Customer/Edit/5

        public ActionResult Edit(int id)
        {
            Customer customer = CustomerServant.Get(id, false);
            ViewBag.OperDepartmentID = new SelectList(operDepartmentList, "ID", "Name", customer.OperDepartmentID);
            return View(customer);
        }

        //
        // POST: /Customer/Edit/5

        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            try
            {
                CustomerServant.Update(customer);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogHelper.WriteErr(ex);
                throw ex;
            }
        }

        //
        // GET: /Customer/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Customer/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// 逻辑删除客户
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteCustomer(int customerID)
        {
            bool flag = false;
            try
            {
                CustomerServant.Delete(customerID);
                flag = true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteErr(ex);
            }
            return Json(flag);
        }
    }
}
