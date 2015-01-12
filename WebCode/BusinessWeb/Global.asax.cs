using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebBase;

namespace BusinessWeb
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //依赖注入
            ApplicationConfig.Intance.Init(o => { },
                               new Assembly[]
                                               {
                                                   Assembly.Load("iBangTeam.Business.Bll"), Assembly.Load("iBangTeam.Business.IBll"),  Assembly.Load("iBangTeam.Business.IDal"),Assembly.Load("iBangTeam.Business.Dal"),Assembly.Load("BusinessWeb")
                                               });

        }
    }
}
