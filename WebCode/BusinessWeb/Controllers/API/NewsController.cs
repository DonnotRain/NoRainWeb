using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessWeb.Controllers.API
{
    public class NewsController : Controller
    {
        //
        // GET: /News/
        public ActionResult Index()
        {
            return View();
        }
        // GET: /News/
        public ActionResult Detail()
        {
            return View();
        }
    }
}