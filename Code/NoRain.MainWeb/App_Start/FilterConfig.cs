using System.Web;
using System.Web.Mvc;
using MainWeb.Filters;

namespace MainWeb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
          
        }
    }
}
