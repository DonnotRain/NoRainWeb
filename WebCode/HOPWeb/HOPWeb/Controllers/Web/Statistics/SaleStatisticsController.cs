using BusinessWeb.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WQTWeb.Controllers.Web.Statistics
{
    [AdminAuthorize]
    public class SaleStatisticsController : Controller
    {
        /// <summary>
        /// 商品实时销售汇总金额
        /// </summary>
        /// <returns></returns>
        public ActionResult RealTimeAmount()
        {
            return View();
        }

        /// <summary>
        /// 商品年，月，日销量对照表
        /// </summary>
        /// <returns></returns>
        public ActionResult ItemCount()
        {
            return View();
        }

        /// <summary>
        /// 销售员业绩统计
        /// </summary>
        /// <returns></returns>
        public ActionResult SalersPerformance()
        {
            return View();
        }
    }
}