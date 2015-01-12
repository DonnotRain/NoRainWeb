using BusinessWeb.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WQTWeb.Controllers.Web.Statistics
{
    [AdminAuthorize]
    public class SaleAttendController : Controller
    {
        /// <summary>
        /// 请假统计
        /// </summary>
        /// <returns></returns>
        public ActionResult SaleLeave()
        {
            return View();
        }

        /// <summary>
        /// 签到考勤记录
        /// </summary>
        /// <returns></returns>
        public ActionResult SaleAttend()
        {
            return View();
        }
    }
}