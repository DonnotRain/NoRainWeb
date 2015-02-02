using System.Web.Mvc;

namespace MainWeb.Areas.Back
{
    public class BackAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Back";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Back_default",
                "Back/{controller}/{action}/{id}",
                new { action = "Dashboard", controller = "Platform", id = UrlParameter.Optional}
            );
        }
    }
}