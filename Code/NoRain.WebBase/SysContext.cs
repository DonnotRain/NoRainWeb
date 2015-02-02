using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace NoRain.Business.WebBase
{
    public class SysContext
    {

        public static string UserId
        {
            get
            {
                return (string)HttpContext.Current.Items["UserId"];
            }
            set
            {
                HttpContext.Current.Items["UserId"] = value;
            }
        }

        public static string UserName
        {
            get
            {
                return (string)HttpContext.Current.Items["UserName"];
            }
            set
            {
                HttpContext.Current.Items["UserName"] = value;
            }
        }


        public static int MobileUserId
        {
            get
            {
                return (int)HttpContext.Current.Items["MobileUserId"];
            }
            set
            {
                HttpContext.Current.Items["MobileUserId"] = value;
            }
        }

    }
}
