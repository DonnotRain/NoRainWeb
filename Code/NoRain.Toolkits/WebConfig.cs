using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace NoRain.Business.CommonToolkit
{
    public class SysConfigs
    {
        private SysConfigs()
        {
        }

        private static SysConfigs instance=null;

        public static  SysConfigs Instance
        {
            get
            {
                if (instance == null)
                    instance = new SysConfigs();
                return instance;
            }
        }

        public static string SecretKey
        {
            get { return ConfigurationManager.AppSettings["SecretKey"].ToString(); }
        }
        public static bool VerificationOpen
        {
            get { return bool.Parse(ConfigurationManager.AppSettings["VerificationOpen"].ToString()); }
        }

        public static string UploadPath
        {
            get { return ConfigurationManager.AppSettings["UploadPath"].ToString(); }
        }

        public static string AndroidPath
        {
            get
            {
                return ConfigurationManager.AppSettings["AndroidApk"].ToString();
            }
        }

        public static string iOSPath
        {
            get
            {
                return ConfigurationManager.AppSettings["iOSIpa"].ToString();
            }
        }

        public static string AndroidVersion
        {
            get
            {
                return ConfigurationManager.AppSettings["AndroidVersion"].ToString();
            }
        }

        public static string iOSVersion
        {
            get
            {
                return ConfigurationManager.AppSettings["iOSVersion"].ToString();
            }
        }

        public string this[string key]
        {
            get { return ConfigurationManager.AppSettings[key].ToString(); }
        }
    }
}
