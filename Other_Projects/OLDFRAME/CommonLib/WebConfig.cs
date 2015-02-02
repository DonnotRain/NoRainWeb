using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace MPAPI.CommonLib
{
    public class ApiConfig
    {
        public static string SecretKey
        {
            get { return ConfigurationManager.AppSettings["SecretKey"].ToString(); }
        }
        public static bool VerificationOpen
        {
            get { return  bool.Parse(ConfigurationManager.AppSettings["VerificationOpen"].ToString()); }
        }

    }
}
