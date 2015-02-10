﻿using BusinessWeb.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MainWeb.Controllers
{
    [AdminAuthorize]
    public class ParametersController : Controller
    {
        // GET: Attendence
        public ActionResult Index()
        {
            return View();
        }
    }
}