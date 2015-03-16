﻿using Autofac;
using NoRain.Business.WebBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;

namespace NoRain.WebMain
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles); var path = Server.MapPath("~/log4net.xml");
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(path));

            //依赖注入
            ApplicationConfig.Intance.Init(o => { },
                               new Assembly[]
                                               {
                                                   Assembly.Load("NoRain.SF.Service"),
                                                   Assembly.Load("NoRain.SF.Data"),
                                                   Assembly.Load("NoRain.SF.Entrance")
                                               });
            //API依赖注入
            var configuration = GlobalConfiguration.Configuration;
            ApplicationConfig.Intance.RegisterAPIControllers(Assembly.GetExecutingAssembly());
            var container = DPResolver.Container;
            var resolver = new AutofacWebApiDependencyResolver(container);
            configuration.DependencyResolver = resolver;          


            //MVC依赖注入
            ApplicationConfig.Intance.RegisterMVCControllers(Assembly.GetExecutingAssembly());
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));    
         
        }

    }
}