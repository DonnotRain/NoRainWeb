using HuaweiSoftware.WQT.IBll;
using HuaweiSoftware.WQT.WebBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WQTRights;


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
                SysContext.UserId = int.Parse(request.Cookies["UserId"].Value);
                SysContext.CorpCode = request.Cookies["CorpCode"].Value;

                //还要验证已登录的身份信息是否有访问此页面的权限
            }
            catch (Exception ex)
            {
                filterContext.HttpContext.Response.Redirect(string.Format("~/Authorize/Index?url={0}", filterContext.HttpContext.Request.Url));
                return;
            }
        }
    }
}