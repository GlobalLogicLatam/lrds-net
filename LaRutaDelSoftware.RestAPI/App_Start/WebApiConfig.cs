using LaRutaDelSoftware.RestAPI.Filters;
using System.Web.Http;

namespace LaRutaDelSoftware.RestAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Filters.Add(new ValidateModelStateFilter());
            config.Filters.Add(new ExceptionHandlingAttribute());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // add UoW action filter globally
            config.Filters.Add(new UnitOfWorkActionFilter());
        }
    }
}
