using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Autofac;
using Autofac.Integration.WebApi;
using Autofac.Integration.Mvc;
using System.Reflection;

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

        private ContainerBuilder _bulider;
        public void Init(Action<ContainerBuilder> func, params  System.Reflection.Assembly[] assemblies)
        {
            if (this._bulider != null)
            {
                throw new Exception("已经初始化，不能再初始化！");
            }
            _bulider = new ContainerBuilder();

            _bulider.RegisterInstance(this);

            this.RegisterAssebbly(assemblies);

            func(this._bulider);

            //_bulider.RegisterType<ServiceInterceptor>().As<IInterceptor>();

            DPResolver.Container = this._bulider.Build();

        }

        public void RegisterTypeAfterInit(Type srcType, Type targetType)
        {
            _bulider.RegisterGeneric(srcType).As(targetType);
        }

        /// <summary>
        /// 注册Controller
        /// </summary>
        /// <param name="assemblies"></param>
        public void RegisterMVCControllers(params  System.Reflection.Assembly[] assemblies)
        {
            _bulider.RegisterControllers(assemblies);
        }

        /// <summary>
        /// 注册Controller
        /// </summary>
        /// <param name="assemblies"></param>
        public void RegisterAPIControllers(params  System.Reflection.Assembly[] assemblies)
        {
            _bulider.RegisterApiControllers(assemblies);
        }

        private void RegisterAssebbly(params  System.Reflection.Assembly[] assemblies)
        {
            _bulider.RegisterAssemblyTypes(assemblies)
                .AsImplementedInterfaces()
                .SingleInstance();

        }
    }
}
