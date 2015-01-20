using BusinessWeb.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WQTWeb.Areas.Back.Controllers
{
    [BackAdminAuthorize]
    public class PlatformController : Controller
    {
        // GET: Back/Platform
        public ActionResult Dashboard()
        {
            return View();
        }

    }
}