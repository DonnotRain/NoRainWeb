using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BusinessWeb.Filters
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        public AdminAuthorizeAttribute()
        {
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var request = filterContext.RequestContext.HttpContext.Request;

            //请求的Cookies中不包含adminname，则跳转到登录页
            if (!request.Cookies.Keys.Cast<string>().Contains("adminname"))
            {
                filterContext.HttpContext.Response.Redirect(string.Format("~/Back/Authorize/Login?url={0}", filterContext.HttpContext.Request.Url));
                return;
            }
            //保存登录信息

            //  filterContext.HttpContext.Request.Cookies
        }
    }
}