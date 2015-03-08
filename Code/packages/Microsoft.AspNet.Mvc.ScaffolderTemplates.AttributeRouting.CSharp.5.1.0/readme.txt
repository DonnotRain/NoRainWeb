In order to enable attribute routing for ASP.NET MVC, make sure that the following
line is present at the beginning of the RegisterRoutes method in App_Start\RouteConfig.cs:
   routes.MapMvcAttributeRoutes();

For more information on attribute routing in ASP.NET MVC,
see http://go.microsoft.com/fwlink/?LinkID=324821
