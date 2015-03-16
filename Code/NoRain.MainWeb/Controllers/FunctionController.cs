﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessWeb.Filters;

namespace BusinessWeb.Areas.Back.Controllers
{
     [AdminAuthorize]
    public class FunctionController : Controller
    {
        //
        // GET: /Back/Plug/
        public ActionResult Index()
        {
            return View();
        }
	}
}