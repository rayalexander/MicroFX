using System.Web.Http;
using System.Web.Http.Routing;
using Owin;

namespace Ninja.MicroFx.Platform.Configuration
{
    public static class WebApiConfig
    {
        public static void ConfigureWebApi(this IAppBuilder app,HttpConfiguration config)
        {
            //config.SuppressDefaultHostAuthentication();
           // config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //   name: "DefaultApi",
            //   routeTemplate: "v1/{controller}/{id}",
            //   defaults: new { id = RouteParameter.Optional }
            //   );


            config.Routes.MapHttpRoute(
                name: "Default", // Route name
                routeTemplate: "{version}/{controller}/{action}/{id}", // URL with parameters
                defaults: new {version="v1", controller = "Home", action = "Index", id = RouteParameter.Optional } // Parameter defaults
                );
           
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.Add(config.Formatters.JsonFormatter);
            //config.MessageHandlers.Add();

            app.UseWebApi(config);
        }
        
    }
}
