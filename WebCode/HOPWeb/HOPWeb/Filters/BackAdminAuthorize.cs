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
    /// <summary>
    /// 平台的后台管理权限验证过滤器
    /// </summary>
    public class BackAdminAuthorizeAttribute : AuthorizeAttribute
    {
        private ICommonSecurityBLL m_securityBll = DPResolver.Resolver<ICommonSecurityBLL>();

        public BackAdminAuthorizeAttribute()
        {
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var request = filterContext.RequestContext.HttpContext.Request;
            try
            {
                //var identity = HuaweiSoftware.Common.CGYLoginControl.SecurityHelper.Identity;
                //var userName = SecurityHelper.SecurityInst.VerifyUserByIdentity(identity);
                SysContext.UserName = request.Cookies["BackUserName"].Value;
                SysContext.UserId = int.Parse(request.Cookies["BackUserId"].Value);
                //保存登录信息

                //   var user = m_securityBll.Find<User>(m => m.Name == userName);

            }
            catch (Exception ex)
            {
                filterContext.HttpContext.Response.Redirect(string.Format("~/Back/BackAuthorize/Login?url={0}", filterContext.HttpContext.Request.Url));
                return;
            }
            //请求的Cookies中不包含adminname，则跳转到登录页
            if (!request.Cookies.Keys.Cast<string>().Contains("BackUserName"))
            {
                filterContext.HttpContext.Response.Redirect(string.Format("~/Back/BackAuthorize/Login?url={0}", filterContext.HttpContext.Request.Url));
                return;
            }
        }
    }
}