using System;
using System.Linq;
using System.Reflection;
using Owin;

namespace Ninja.MicroFx.Platform.Configuration
{
    public static class AutoMapperConfig
    {
        public static void ConfigureAutoMapper(this IAppBuilder app)
        {
            var modules = Assembly.GetCallingAssembly().GetTypes()
                .Where(t => typeof (IAutoMapperModule).IsAssignableFrom(t) && !t.IsInterface)
                .Select(Activator.CreateInstance)
                .ToList();

            modules.ForEach(t => ((IAutoMapperModule) t).Load());
        }
    }
}
