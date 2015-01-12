using BusinessWeb.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WQTWeb.Controllers.Web
{
    [AdminAuthorize]
    public class ScoreController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}