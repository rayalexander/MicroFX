
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
                c.Service<Service>(service =>
                {
                    ServiceConfigurator<Service> s = service;
                    s.ConstructUsing(() => new Service());
                    s.WhenStarted(a => a.Start());
                    s.WhenStopped(a => a.Stop());
                });

                c.SetServiceName(Settings.Default.ServiceName);
                c.SetDisplayName(Settings.Default.ServiceName);
                c.SetDescription(Settings.Default.Description);

                c.StartAutomatically();
                c.RunAsLocalSystem();

               // c.DependsOnEventLog();
               // c.DependsOnMsSql();
            });

            return (int) exitCode;
        }
    }

}
