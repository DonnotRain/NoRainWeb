using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using log4net;
using System.Net;
using System.Text;
using System.Web.Http;
using NoRain.Business.WebBase;

namespace WQTWeb.Filters
{
    /// <summary>
    /// 系统异常处理过滤器
    /// </summary>
    public class WebApiExceptionFilter : ExceptionFilterAttribute
    {
        public WebApiExceptionFilter()
        {
        }
        public override void OnException(HttpActionExecutedContext context)
        {
            HttpResponseMessage message = null;
            //先过滤自定义异常
            if (context.Exception.GetType() == typeof(ApiException))
            {
                var exception = context.Exception as ApiException;

                var messageStr = "";
                if (exception.Parameters.LongLength > 0)
                {
                    messageStr = string.Format(EnumHelper.GetEnumDescription(exception.ResponseCode) + (string.IsNullOrEmpty(exception.Addition) ? "" : (":" + exception.Addition)), exception.Parameters);
                }
                else
                {
                    messageStr = EnumHelper.GetEnumDescription(exception.ResponseCode) + (string.IsNullOrEmpty(exception.Addition) ? "" : (":" + exception.Addition));
                }
                message = new System.Net.Http.HttpResponseMessage(EnumHelper.GetResponseCode(exception.ResponseCode))
                {
                    Content = new StringContent(messageStr, System.Text.Encoding.UTF8),
                    ReasonPhrase = ((int)exception.ResponseCode).ToString() // + exception.Addition
                };
            }
            //再过滤直接抛出的Http异常
            else if (context.Exception.GetType() == typeof(HttpResponseException))
            {
                var exception = context.Exception as HttpResponseException;
                throw exception;
            }
            //其他情况返回500内部错误
            else
            {
                var messageContent = "内部处理错误，错误信息：" + context.Exception.Message + "\n具体信息可查看日志记录";
                message = new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(messageContent, System.Text.Encoding.UTF8) };
                LoggerHelper.Logger.Error(context.Exception.Message + "\n" + context.Exception.StackTrace);
            }
            throw new HttpResponseException(message);
        }
    }

}