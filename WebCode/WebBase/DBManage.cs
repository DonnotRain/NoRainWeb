using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WebBase
{
    public class DBManage
    {
        public static Database DB
        {

            get
            {
                if (HttpContext.Current.Items["CurrentDb"] == null)
                {
                    var retval = DefaultConnection.DefaultConnectionDB.GetInstance();
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
                    var retval = Security_ConnectionString.Security_ConnectionStringDB.GetInstance();
                    HttpContext.Current.Items["CurrentSecurityDB"] = retval;
                    return retval;
                }
                return (Database)HttpContext.Current.Items["CurrentSecurityDB"];
            }
        }
    }
}
