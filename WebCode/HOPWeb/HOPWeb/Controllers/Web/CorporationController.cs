using BusinessWeb.Filters;
using DefaultConnection;
using HuaweiSoftware.WQT.IBll;
using HuaweiSoftware.WQT.WebBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WQTWeb.Controllers
{
    public class CorporationController : Controller
    {
        ICorpBll m_Bll;
        public CorporationController()
        {
            m_Bll = DPResolver.Resolver<ICorpBll>();
        }

        public ActionResult Index()
        {
            return View();
        }


        // GET: Attendence
        public ActionResult Register()
        {
            return View(new DefaultConnection.Corporation());
        }

        [HttpPost]
        public ActionResult Register(FormCollection collection, Corporation corp)
        {
            try
            {
                m_Bll.Register(corp, collection["RealName"], collection["AdminName"], collection["AdminPassword"]);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.RealName = collection["RealName"];
                ViewBag.AdminName = collection["AdminName"];
                ViewBag.Password = collection["AdminPassword"];

                return View(corp);
            }

            ViewBag.IsSuccess = true;
            ViewBag.Code = corp.Code;
            return View(corp);

        }
    }
}