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
    /// web请求的过滤接口
    /// </summary>
    public class ServiceValidateAttribute : System.Web.Http.AuthorizeAttribute
    {
        public string[] Category { get; set; }
        public String Type { get; set; }
        public ServiceValidateAttribute()
        {
        }
        public ServiceValidateAttribute(params string[] category)
        {
            if (category == null) throw new NullReferenceException("Category");
            Category = category;
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
                  
            if (context.Request.QueryString.AllKeys.Contains("UserId"))
            {
                SysContext.UserId =context.Request.QueryString["UserId"];
            }
            else if (context.Request.Cookies.AllKeys.Contains("UserId"))
                SysContext.UserId = context.Request.Cookies["UserId"].Value;

            //  LoggerHelper.Logger.Debug(actionContext.ActionDescriptor.ControllerDescriptor);
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