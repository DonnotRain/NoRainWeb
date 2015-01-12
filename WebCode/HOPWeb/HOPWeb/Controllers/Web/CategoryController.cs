using BusinessWeb.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WQTWeb.Controllers
{
    [AdminAuthorize]
    public class CategoryController : Controller
    {
        //
        // GET: /Back/Category/
        public ActionResult Index()
        {
            return View();
        }
    }
}