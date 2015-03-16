using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;

namespace NoRain.Business.WebBase
{
    public static class DPResolver
    {
        internal static Autofac.IContainer _container;

        public static T Resolver<T>()
        {
            return _container.Resolve<T>();
        }

        //公开容器
        public static Autofac.IContainer Container { get { return _container; } }
    }
}
