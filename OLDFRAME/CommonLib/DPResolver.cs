﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;

namespace MPAPI.CommonLib
{
    public static class DPResolver
    {
        internal static Autofac.IContainer Container;

        public static T Resolver<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
