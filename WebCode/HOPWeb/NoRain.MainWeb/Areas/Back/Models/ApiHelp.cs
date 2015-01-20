using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Http.Description;
using WQTWeb.Filters;

namespace WQTWeb.Areas.Back.Models
{
    public class ApiHelp
    {
        public static Collection<ApiDescription> FilterApiDescriptions(Collection<ApiDescription> apis)
        {
            for (int i = apis.Count - 1; i >= 0; i--)
            {
                var type = apis[i].ActionDescriptor.ControllerDescriptor.ControllerType;
                var arrtbutes = type.GetCustomAttributes(false);

                if (!arrtbutes.Any(m => m.GetType() == typeof(MobileServiceMarkAttribute)))
                {
                    apis.RemoveAt(i);
                }
            }
            return apis;
        }
    }
}