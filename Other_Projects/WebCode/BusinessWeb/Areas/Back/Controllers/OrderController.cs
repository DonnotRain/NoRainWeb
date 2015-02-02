using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using  Security_ConnectionString;
using BusinessWeb.Filters;

namespace BusinessWeb.Areas.Back.Controllers
{
    public class OrderController : Controller
    {
        //
        // GET: /BaseSystem/Order/
        [AdminAuthorize]
        public ActionResult Index()
        {
            return null;
        }


        [HttpPost]
        public ActionResult AjaxPager()
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        //
        // GET: /BaseSystem/Order/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /BaseSystem/Order/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /BaseSystem/Order/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /BaseSystem/Order/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /BaseSystem/Order/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /BaseSystem/Order/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /BaseSystem/Order/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
