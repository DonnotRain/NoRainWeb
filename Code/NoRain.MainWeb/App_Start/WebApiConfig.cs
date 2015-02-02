using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MainWeb.Filters;

namespace MainWeb
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //config.Filters.Add(new ServiceValidateAttribute());

            config.Filters.Add(new WebApiExceptionFilter());
        }
    }
}
