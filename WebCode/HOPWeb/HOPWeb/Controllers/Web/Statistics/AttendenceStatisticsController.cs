using BusinessWeb.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WQTWeb.Controllers.Web.Statistics
{
    [AdminAuthorize]
    public class AttendenceStatisticsController : Controller
    {
        /// <summary>
        /// 考勤统计
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}