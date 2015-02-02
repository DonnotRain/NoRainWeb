using NoRain.Business.CommonToolkit;
using NoRain.Business.WebBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace MainWeb.Filters
{
    /// <summary>
    /// 用于移动端的请求验证,移动设备请求数据时，需要通过此过滤器验证
    /// </summary>
    public class MobileValidateAttribute : System.Web.Http.AuthorizeAttribute
    {
        public string[] CategoryStr { get; set; }
        public String Type { get; set; }
        public MobileValidateAttribute()
        {
        }
        public MobileValidateAttribute(params string[] category)
        {
            if (category == null) throw new NullReferenceException("Category");
            CategoryStr = category;
        }

        /// <summary>
        /// 执行前验证
        /// </summary>
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            var context = System.Web.HttpContext.Current;
            //验证请求参数
            if (!CheckValidate(context.Request))
            {
                HandleUnauthorizedRequest(actionContext);
            }
            try
            {
               
                //获取人员名称，兼容userName和username
                if (context.Request.QueryString.AllKeys.Contains("userName"))
                {
                    SysContext.UserName = context.Request.QueryString["userName"];
                }
                else if (context.Request.Form.AllKeys.Contains("userName"))
                {
                    SysContext.UserName = context.Request.Form["userName"];
                }
                else if (context.Request.Cookies.AllKeys.Contains("userName"))
                {
                    SysContext.UserName = context.Request.Cookies["userName"].Value;
                }
                else if (context.Request.QueryString.AllKeys.Contains("username"))
                {
                    SysContext.UserName = context.Request.QueryString["username"];
                }
                else if (context.Request.Form.AllKeys.Contains("username"))
                {
                    SysContext.UserName = context.Request.Form["username"];
                }
                else if (context.Request.Cookies.AllKeys.Contains("username"))
                {
                    SysContext.UserName = context.Request.Cookies["username"].Value;
                }


            }
            catch (Exception ex)
            {
                LoggerHelper.Logger.Error(ex.Message);
                HandleUnauthorizedRequest(actionContext);
            }
        }

        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var challengeMessage = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent("未通过验证，请检查请求参数")
                };
            // challengeMessage.Headers.Add("WWW-Authenticate", "Basic");
            throw new System.Web.Http.HttpResponseException(challengeMessage);
        }

        private bool CheckValidate(HttpRequest request)
        {
            //不进行验证
            return true;

            //RequestBase requestBase = new RequestBase(request.QueryString);
            //var b = string.Format("{0}{1}{2}", requestBase.Timestamp,
            //                 requestBase.Token, ApiConfig.SecretKey);
            //string mySig = NoRain.Business.CommonToolkit.CommonToolkit.GetMD5(b);
            //if (ApiConfig.VerificationOpen && !mySig.Equals(requestBase.Sig))
            //    return false;
            //return true;
        }
    }
}