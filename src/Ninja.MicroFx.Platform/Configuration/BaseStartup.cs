using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Ninja.MicroFx.Platform.AutoMapper;
using Ninja.MicroFx.Platform.Container;
using Ninja.MicroFx.Platform.Http;
using Owin;

namespace Ninja.MicroFx.Platform.Configuration
{
    public abstract class BaseStartup
    {
        private readonly HttpConfiguration config;

        protected BaseStartup(HttpConfiguration config)
        {
            this.config = config;
        }

        public void Configuration(IAppBuilder app)
        {
            app.ConfigureWebApi(config);
            app.ConfigureIoC(config);
            app.ConfigureAutoMapper();

            RunCustomStartups(app);
        }

        private void RunCustomStartups(IAppBuilder app)
        {
            var modules = Assembly.GetCallingAssembly().GetTypes()
               .Where(t => typeof(IStartup).IsAssignableFrom(t) && !t.IsInterface)
               .Select(Activator.CreateInstance)
               .ToList();

            modules.ForEach(t => ((IStartup)t).Configuration(app, config));
        }
    }
}