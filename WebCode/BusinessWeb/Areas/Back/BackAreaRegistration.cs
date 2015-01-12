using System.Web.Mvc;

namespace BusinessWeb.Areas.Back
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
                new { action = "Index", controller = "BackHome", id = UrlParameter.Optional }
            );
        }
    }
}