using BusinessWeb.Filters;
using HuaweiSoftware.WQT.IBll;
using HuaweiSoftware.WQT.WebBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WQTWeb.Controllers
{
    [AdminAuthorize]
    public class HomeController : Controller
    {
        private IParameterBll m_parameterBll;
     
        public HomeController()
        {
            m_parameterBll = DPResolver.Resolver<IParameterBll>();
        }

        [AdminAuthorize]
        public ActionResult Index(string redirect)
        {
            ViewBag.Title = "主面板";

            return View();
        }

        [AdminAuthorize]
        public ActionResult Settings()
        {
            //获取系统名称参数
            var sysName = m_parameterBll.GetSysName("系统名称").Value;
            var upTime = m_parameterBll.GetSysName("上班时间").Value;
            var downTime = m_parameterBll.GetSysName("下班时间").Value;
            var distance = m_parameterBll.GetSysName("允许的签到误差距离").Value;

            ViewBag.AttendDistance = distance;

            ViewBag.SysName = sysName;
            ViewBag.UpTime = upTime;
            ViewBag.DownTime = downTime;

            return View();
        }

        [AdminAuthorize]
        [HttpPost]
        public ActionResult Settings(string sysName, string upTime, string downTime, string attendDistance)
        {
            try
            {
                //获取系统名称参数
                var systemName = m_parameterBll.SetSysName(sysName, "系统名称").Value;
                var upTimeParam = m_parameterBll.SetSysName(upTime, "上班时间").Value;
                var downTimeParam = m_parameterBll.SetSysName(downTime, "下班时间").Value;
                var distance = m_parameterBll.SetSysName(attendDistance, "允许的签到误差距离").Value;
                ViewBag.SysName = systemName;
                ViewBag.UpTime = upTimeParam;
                ViewBag.DownTime = downTimeParam;
                ViewBag.AttendDistance = distance;

                ViewBag.Success = true;
            }
            catch
            {
                ViewBag.Success = false;
            }

            return View();
        }
    }
}
