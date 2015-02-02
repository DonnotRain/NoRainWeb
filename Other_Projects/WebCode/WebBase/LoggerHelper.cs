using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Reflection;
namespace WebBase
{
    public class LoggerHelper
    {
        private static readonly ILog logger = LogManager.GetLogger(Assembly.Load(new AssemblyName("BusinessWeb")), "LogFileAppender");
        public static ILog Logger { get { return logger; } }
    }
}
