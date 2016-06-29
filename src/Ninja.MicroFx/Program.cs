
using Ninja.MicroFx.Platform;
using Ninja.MicroFx.Platform.Database;
using Ninja.MicroFx.Properties;
using Topshelf;
using Topshelf.ServiceConfigurators;

namespace Ninja.MicroFx
{
    class Program
    {
        private static int Main(string[] args)
        {
            var exitCode = HostFactory.Run(c =>
            {
                c.Service<MicroService>(service =>
                {
                    ServiceConfigurator<MicroService> s = service;
                    
                    s.ConstructUsing(() => new MicroService(new Settings(4000,null,null))
                        .WithDbInitialise(() => new DbInitialiser()));

                    s.WhenStarted(a => a.Start());
                    s.WhenStopped(a => a.Stop());
                });

                c.SetServiceName(Properties.Settings.Default.ServiceName);
                c.SetDisplayName(Properties.Settings.Default.ServiceName);
                c.SetDescription(Properties.Settings.Default.Description);

                c.StartAutomatically();
                c.RunAsLocalSystem();

               // c.DependsOnEventLog();
               // c.DependsOnMsSql();
            });

            return (int) exitCode;
        }
    }
}
