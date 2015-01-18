using NoRain.Business.IBll;
using NoRain.Business.WebBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoRainRights;


namespace BusinessWeb.Filters
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        private ICommonSecurityBLL m_securityBll = DPResolver.Resolver<ICommonSecurityBLL>();

        public AdminAuthorizeAttribute()
        {
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var request = filterContext.RequestContext.HttpContext.Request;
            try
            {
                //保存登录信息
                SysContext.UserName = request.Cookies["UserName"].Value;
                SysContext.UserId = request.Cookies["UserId"].Value;

                //还要验证已登录的身份信息是否有访问此页面的权限
            }
            catch (Exception ex)
            {
                // filterContext.HttpContext.Response.
                if (filterContext.HttpContext.Response.StatusCode != 302)
                {

                    filterContext.HttpContext.Response.Redirect(string.Format("~/Authorize/Login?url={0}", filterContext.HttpContext.Request.Url));
                    filterContext.HttpContext.Response.End();
                }
             //   else throw ex;
            }
        }
    }
}