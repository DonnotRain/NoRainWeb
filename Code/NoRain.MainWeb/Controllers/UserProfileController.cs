using BusinessWeb.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MainWeb.Controllers
{
    public class UserProfileController : Controller
    {
        public ActionResult Index(int? id)
        {
            return View();
        }

        public ActionResult Account(int? id)
        {
            return View();
        }

        public ActionResult Tasks(int? id)
        {
            return View();
        }

        public ActionResult Helps()
        {
            return View();
        }
    }
}