using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Owin;

namespace Ninja.MicroFx.Platform.Configuration
{
    public static class IocConfig
    {
        public static void ConfigureIoC(this IAppBuilder app, HttpConfiguration config)
        {
            var callingAssembly = Assembly.GetCallingAssembly();

            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(callingAssembly).InstancePerRequest();

            builder.RegisterAssemblyTypes(callingAssembly)
                .Where(t => typeof (IResource).IsAssignableFrom(t) && !t.IsInterface)
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder.RegisterAssemblyModules(callingAssembly);

           var container = builder.Build();
            
            var dependencyResolver = new AutofacWebApiDependencyResolver(container);
            config.DependencyResolver = dependencyResolver;

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
        }
    }
}
