using BusinessWeb.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WQTWeb.Controllers
{
    [AdminAuthorize]
    public class SellUserController : Controller
    {
        // GET: Attendence
        public ActionResult Index()
        {
            return View();
        }
    }
}