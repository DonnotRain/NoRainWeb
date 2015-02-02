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
using WebBase;

namespace BusinessWeb.Filters
{

    public class WebApiExceptionFilter : ExceptionFilterAttribute
    {
        public WebApiExceptionFilter()
        {
        }
        public override void OnException(HttpActionExecutedContext context)
        {
            HttpResponseMessage message = null;
            if (context.Exception.GetType() == typeof(ApiException))
            {
                var exception = context.Exception as ApiException;
                message = new System.Net.Http.HttpResponseMessage(EnumHelper.GetResponseCode(exception.ResponseCode))
                {
                    Content = new StringContent(EnumHelper.GetEnumDescription(exception.ResponseCode) + exception.Addition, System.Text.Encoding.UTF8),
                    ReasonPhrase = ((int)exception.ResponseCode).ToString() // + exception.Addition
                };
            }
            else
            {
                var messageContent = context.Exception.Message;
                message = new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(messageContent, System.Text.Encoding.UTF8) };
            }
            LoggerHelper.Logger.Error(context.Exception);
            throw new HttpResponseException(message);
        }
    }

}