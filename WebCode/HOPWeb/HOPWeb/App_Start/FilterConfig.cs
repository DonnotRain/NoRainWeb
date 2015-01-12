using System.Web;
using System.Web.Mvc;
using WQTWeb.Filters;

namespace WQTWeb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
          
        }
    }
}
