using BusinessWeb.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessWeb.Areas.Back.Controllers
{
    public class BackHomeController : Controller
    {
        //
        // GET: /Back/BackHome/

        [AdminAuthorize]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Blank()
        {
            return View();
        }


    }
}
