using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
namespace MPAPI.CommonLib
{
    public class LoggerHelper
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(LoggerHelper));
        public static ILog Logger { get { return logger; } }
    }
}
