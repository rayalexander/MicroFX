using System;
using Owin;

namespace Ninja.MicroFx.Platform.AutoMapper
{
    public static class AutoMapperConfig
    {
        public static void ConfigureAutoMapper(this IAppBuilder app)
        {
            var modules = AssemblyHelper.GetTypes<IMapperModule>();

            modules.ForEach(t => t.Load());

            Console.WriteLine("Config automapper .....is run");
        }

        
    }
}
