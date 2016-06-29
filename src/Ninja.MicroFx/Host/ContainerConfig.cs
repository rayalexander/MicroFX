using System;
using Autofac;
using Ninja.MicroFx.Platform;
using Ninja.MicroFx.Properties;

namespace Ninja.MicroFx.Host
{
    public class ContainerConfig : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterType<Settings>().As<IMicroserviceSettings>()
            .WithParameter("port", Properties.Settings.Default.Port)
            .WithParameter("serviceName", Properties.Settings.Default.ServiceName)
            .WithParameter("description", Properties.Settings.Default.Description);

            Console.WriteLine("custom autofac module ...has run");
        }
    }
}
