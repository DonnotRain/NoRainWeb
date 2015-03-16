using BusinessWeb.Filters;
using NoRain.Business.IService;
using NoRain.Business.WebBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MainWeb.Controllers
{
    [AdminAuthorize]
    public class HomeController : Controller
    {
        private IParameterService m_parameterService;
     
        public HomeController()
        {
            m_parameterService = DPResolver.Resolver<IParameterService>();
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
            var sysName = m_parameterService.GetSysName("系统名称").ValueContent;
            var upTime = m_parameterService.GetSysName("上班时间").ValueContent;
            var downTime = m_parameterService.GetSysName("下班时间").ValueContent;
            var distance = m_parameterService.GetSysName("允许的签到误差距离").ValueContent;

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
                var systemName = m_parameterService.SetSysName(sysName, "系统名称").ValueContent;
                var upTimeParam = m_parameterService.SetSysName(upTime, "上班时间").ValueContent;
                var downTimeParam = m_parameterService.SetSysName(downTime, "下班时间").ValueContent;
                var distance = m_parameterService.SetSysName(attendDistance, "允许的签到误差距离").ValueContent;
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
