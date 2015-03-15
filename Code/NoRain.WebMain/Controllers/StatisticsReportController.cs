using BusinessWeb.Filters;
using NoRain.Business.IBll;
using NoRain.Business.WebBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MainWeb.Controllers
{
    [AdminAuthorize]
    public class StatisticsReportController : Controller
    {

        [AdminAuthorize]
        public ActionResult Index(string redirect)
        {
            return View();
        }
    }
}
