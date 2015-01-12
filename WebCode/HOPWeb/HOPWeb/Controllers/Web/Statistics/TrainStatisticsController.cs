using BusinessWeb.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WQTWeb.Controllers.Web.Statistics
{
    [AdminAuthorize]
    public class TrainStatisticsController : Controller
    {
        /// <summary>
        /// 销售员培训统计
        /// </summary>
        /// <returns></returns>
        public ActionResult SalerTrainStatistic()
        {
            return View();
        }

    }
}