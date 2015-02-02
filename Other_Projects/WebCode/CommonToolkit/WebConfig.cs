using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace CommonToolkit
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

      public static string  UploadPath
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
    }
}
