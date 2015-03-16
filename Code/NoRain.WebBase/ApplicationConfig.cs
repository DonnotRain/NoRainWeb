using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Autofac;
using Autofac.Integration.WebApi;
using Autofac.Integration.Mvc;
using System.Reflection;
using System.Web.Http;

namespace NoRain.Business.WebBase
{
    public class ApplicationConfig
    {
        private static ApplicationConfig _config;

        static ApplicationConfig()
        {
            _config = new ApplicationConfig();
        }

        public static ApplicationConfig Intance
        {
            get { return _config; }
        }

        private ContainerBuilder _builder;
        public void Init(Action<ContainerBuilder> func, params  System.Reflection.Assembly[] assemblies)
        {
            if (this._builder != null)
            {
                throw new Exception("已经初始化，不能再初始化！");
            }
            _builder = new ContainerBuilder();

            _builder.RegisterInstance(this);

            this.RegisterAssebbly(assemblies);

            func(this._builder);

            //_bulider.RegisterType<ServiceInterceptor>().As<IInterceptor>();

            DPResolver._container = this._builder.Build();

        }

        public void RegisterTypeAfterInit(Type srcType, Type targetType)
        {

            _builder.RegisterGeneric(srcType).As(targetType);
        }

        /// <summary>
        /// 注册Controller
        /// </summary>
        /// <param name="assemblies"></param>
        public void RegisterMVCControllers(params  System.Reflection.Assembly[] assemblies)
        {
            _builder.RegisterControllers(assemblies);
        }

        /// <summary>
        /// 注册Controller
        /// </summary>
        /// <param name="assemblies"></param>
        public void RegisterAPIControllers(params  System.Reflection.Assembly[] assemblies)
        {
            _builder.RegisterAssemblyTypes(assemblies)
          .Where(t =>
             !t.IsAbstract && typeof(ApiController).IsAssignableFrom(t))
        .InstancePerMatchingLifetimeScope(
           AutofacWebApiDependencyResolver.ApiRequestTag);
            _builder.RegisterApiControllers(assemblies);
        }

        private void RegisterAssebbly(params  System.Reflection.Assembly[] assemblies)
        {
            _builder.RegisterAssemblyTypes(assemblies)
                .AsImplementedInterfaces()
                .SingleInstance();

        }
    }
}
