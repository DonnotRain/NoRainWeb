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
        private static DBManage _instance;
        public static DBManage Instance
        {
            get
            {
                if (_instance == null) _instance = new DBManage();
                return _instance;
            }
        }

        public Database this[string dbName]
        {
            get
            {
                if (HttpContext.Current.Items[dbName] == null)
                {
                    var retval = new Database(dbName);
                    HttpContext.Current.Items[dbName] = retval;
                    return retval;
                }
                return (Database)HttpContext.Current.Items[dbName];
            }
        }
    }
}
