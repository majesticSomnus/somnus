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
    public class UserInfoController : BaseController
    {
        //
        // GET: /UserInfo/

        public ActionResult Index(int id = 1)
        {
            try
            {
                List<UserInfo> list = UserInfoServant.Select(null, false) as List<UserInfo>;
                PagedList<UserInfo> _list = new PagedList<UserInfo>(list, id, WebSetting.PageCount);
                if (Request.IsAjaxRequest())
                {
                    return PartialView("UCJqUserInfoList", _list);
                }
                return View(_list);
            }
            catch (Exception ex)
            {
                LogHelper.WriteErr(ex);
                throw ex;
            }
        }

        public ActionResult AddUserInfo()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(UserInfo user)
        {
            string newPassword = Request.Form["newPassword"];
            if (string.IsNullOrEmpty(newPassword))
            {
                ModelState.AddModelError("EmptyNewpassword", "新密码不能为空");
                return View();
            }
            UserInfo userInfo = Session["UserInfo"] as UserInfo;
            if (userInfo != null)
            {
                userInfo.Password = Md5.ToMD5(newPassword);
                try
                {
                    UserInfoServant.Update(userInfo);
                    ViewData["msg"] = "修改密码成功";
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("error", ex.Message);
                    LogHelper.WriteErr(ex);
                }
            }
            else
            {
                ModelState.AddModelError("ErrorUserLogin", "用户状态已失效,请退出,重新登录");
            }
            return View();
        }

        public ActionResult EditUserInfo(int id)
        {
            try
            {
                UserInfo user = UserInfoServant.Get(id, false);
                return View(user);
            }
            catch (Exception ex)
            {
                LogHelper.WriteErr(ex);
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult EditUserInfo(UserInfo user, FormCollection form)
        {
            int userID = user.UserID;
            try
            {
                UserInfo userInfo = UserInfoServant.Get(userID, false);
                string password = form["Password"];
                if (string.IsNullOrEmpty(password) == false)
                {
                    userInfo.Password = Md5.ToMD5(password);
                }
                userInfo.RealName = user.RealName;
                userInfo.Sex = int.Parse(form["Sex"]);
                userInfo.Phone = user.Phone;
                userInfo.Address = user.Address;
                UserInfoServant.Update(userInfo);
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                LogHelper.WriteErr(ex);
                throw ex;
            }
        }


        [HttpPost]
        public ActionResult AddUserInfo(UserInfo user)
        {
            user.CreateDate = DateTime.Now;
            try
            {
                user.Password = Md5.ToMD5(user.Password);
                int userID = UserInfoServant.Insert(user);
                if (userID > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                LogHelper.WriteErr(ex);
            }
            return View();
        }

        /// <summary>
        /// 验证用户名
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public ActionResult Verify(string userName)
        {
            try
            {
                Hashtable param = new Hashtable();
                param.Add("UserName", userName);
                int count = UserInfoServant.Count(param);
                return Json(count == 0, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper.WriteErr(ex);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <returns></returns>
        public ActionResult CheckPassword(string oldPassword)
        {
            bool flag = false;
            if (user.Password.Equals(Md5.ToMD5(oldPassword)))
            {
                flag = true;
            }
            return Json(flag);
        }

        /// <summary>
        /// 禁用用户
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ForbidUser(int userID, int forbidValue)
        {
            bool flag = false;
            try
            {
                Hashtable param = new Hashtable();
                param.Add("UserID", userID);
                param.Add("ForbidValue", forbidValue);
                UserInfoServant.ForbidUser(param);
                flag = true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteErr(ex);
            }
            return Json(flag);
        }

        /// <summary>
        /// 逻辑删除用户
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteUser(int userID)
        {
            bool flag = false;
            try
            {
                UserInfoServant.Delete(userID);
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
