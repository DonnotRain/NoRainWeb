using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessWeb.Controllers
{
    public class ProductsController : Controller
    {
        //
        // GET: /Products/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Category()
        {
            return View();
        }
    }
}