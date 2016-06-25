using System;
using Microsoft.Owin.Hosting;
using Ninja.MicroFx.Properties;

namespace Ninja.MicroFx
{
    public class Service
    {
        private IDisposable app;
        public void Start()
        {
            app = WebApp.Start(string.Format("http://{0}:{1}/", Settings.Default.Host, Settings.Default.Port));
            //Console.WriteLine("web server running on port: " + port);
        }

        public void Stop()
        {
            app.Dispose();
        }
    }
}