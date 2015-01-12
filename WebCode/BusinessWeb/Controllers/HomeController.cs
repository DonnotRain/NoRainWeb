using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPAPI.Models;

namespace BusinessWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string url)
        {
            ViewBag.Url = url;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (!string.IsNullOrEmpty(model.Url))
            {
                return Redirect(model.Url);
            }
            return RedirectToAction("Index");
        }

        public ActionResult SearchResult()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            return View();
        }

    }
}
