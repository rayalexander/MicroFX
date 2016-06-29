using System;
using System.Collections.Generic;
using Microsoft.Owin.Hosting;

namespace Ninja.MicroFx.Platform
{
    public class MicroService
    {
        private readonly IMicroserviceSettings settings;
        private IDisposable app;

        private List<Func<IInitialiseModule>> initialiseTasks = new List<Func<IInitialiseModule>>();

        public MicroService WithDbInitialise(Func<IInitialiseModule> dbModule)
        {
            initialiseTasks.Add(dbModule);
            return this;
        }

        public MicroService(IMicroserviceSettings settings)
        {
            this.settings = settings;
        }

        public void Start()
        {
            var options = new StartOptions
            {
                Port = settings.Port
            };

            app = WebApp.Start(options);

            initialiseTasks.ForEach(task =>
            {
                var init = task();
                init.Initialise(settings);
            });

            Console.WriteLine("Service start .....is run");
        }

        public void Stop()
        {
            app.Dispose();
        }
    }
}