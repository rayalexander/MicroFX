using System;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Owin;
using Module = Autofac.Module;

namespace Ninja.MicroFx.Platform.Container
{
    public static class IocConfig
    {
        public static void ConfigureIoC(this IAppBuilder app, HttpConfiguration config)
        {
            var callingAssembly = Assembly.GetEntryAssembly();

            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(callingAssembly).InstancePerRequest();

            builder.RegisterAssemblyTypes(callingAssembly)
                .Where(t => typeof (IResource).IsAssignableFrom(t) && !t.IsInterface)
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder.RegisterType<MicroService>()
                .AsSelf()
                .InstancePerRequest();

            var modules = AssemblyHelper.GetTypes<Module>();
            modules.ForEach(m => builder.RegisterModule(m));

            var container = builder.Build();
            
            var dependencyResolver = new AutofacWebApiDependencyResolver(container);
            config.DependencyResolver = dependencyResolver;

            app.UseAutofacMiddleware(container);


            IoC.Initialize(container);

            app.UseAutofacWebApi(config);


            Console.WriteLine("Config container .....is run");
        }
    }
}
