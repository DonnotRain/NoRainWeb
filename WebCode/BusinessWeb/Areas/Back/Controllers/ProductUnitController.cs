﻿using BusinessWeb.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessWeb.Areas.Back.Controllers
{
    [AdminAuthorize]
    public class ProductUnitController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
