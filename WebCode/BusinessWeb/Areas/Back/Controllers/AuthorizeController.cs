using WebBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebBase.Models;

namespace BusinessWeb.Areas.Back.Controllers
{
    public class AuthorizeController : Controller
    {

        public ActionResult Login(string url)
        {
            ViewBag.url = url;
            return View();
        }

        [HttpPost]
        public JsonResult AjaxLogin(string username, string password, string checkNumber)
        {
            AjaxResult result = new AjaxResult();
            var queryResult = new { };
            //if (queryResult.Count() == 0)
            //{
            //    result.Successed = false;
            //    result.Message = "未找到相关的管理员信息,请检查用户名和密码后重试!";
            //}
            //else
            //{
            //    SystemContext.CurrentAdmin = queryResult.ToList()[0];
            //    result.Data = new
            //    {
            //        username = SystemContext.CurrentAdmin.name,
            //        userid = SystemContext.CurrentAdmin.id
            //    };
            //}
            result.Data = new
            {
                username = "Operator",
                userid = "Operator"
            };
            return Json(result);
        }


        public ActionResult Logout()
        {
            //   Response.Cookies.Clear();
            return RedirectToAction("Login");
        }
    }
}
