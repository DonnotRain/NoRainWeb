using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Autofac;
using System.Reflection;

namespace HuaweiSoftware.WQT.WebBase
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
            //  LogManager.GetLogger(Assembly.Load(new AssemblyName("ZiYinZhanWebServer")), "ZiYinZhanWebServer.Models.LoggerHelper").Debug("程序启动"); ;
         
            //bool openDB = DBManage.DB != null;
          //  openDB = DBManage.SecurityDB != null;
        }
        public void RegisterTypeAfterInit(Type srcType, Type targetType)
        {
            _bulider.RegisterGeneric(srcType).As(targetType);
        }

        ///// <summary>
        ///// 注册Controller
        ///// </summary>
        ///// <param name="assemblies"></param>
        //private void RegisterControllers(params  System.Reflection.Assembly[] assemblies)
        //{

        //    _bulider.RegisterControllers(assemblies);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblies"></param>
        private void RegisterAssebbly(params  System.Reflection.Assembly[] assemblies)
        {
            _bulider.RegisterAssemblyTypes(assemblies)
                .AsImplementedInterfaces()
                .SingleInstance();

        }
    }
}
