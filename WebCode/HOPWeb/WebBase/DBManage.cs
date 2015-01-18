using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace NoRain.Business.WebBase
{
    public class DBManage
    {
        public static Database DB
        {

            get
            {
                if (HttpContext.Current.Items["CurrentDb"] == null)
                {
                    var retval = new DefaultConnection.DefaultConnectionDB();
                    HttpContext.Current.Items["CurrentDb"] = retval;
                    return retval;
                }
                return (Database)HttpContext.Current.Items["CurrentDb"];
            }
        }
      
        public static Database SecurityDB
        {
            get
            {
                if (HttpContext.Current.Items["CurrentSecurityDB"] == null)
                {
                    var retval = new NoRainRights.NoRainRightsDB();
                    HttpContext.Current.Items["CurrentSecurityDB"] = retval;
                    return retval;
                }
                return (Database)HttpContext.Current.Items["CurrentSecurityDB"];
            }
        }
    }
}
