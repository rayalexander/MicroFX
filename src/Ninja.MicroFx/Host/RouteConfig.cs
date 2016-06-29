using System;
using System.Web.Http;
using Ninja.MicroFx.Platform.Http;

namespace Ninja.MicroFx.Host
{
    public class RouteConfig: IRouteConfig
    {
        public void Configure(HttpConfiguration config)
        {
            Console.WriteLine("custom route module ...has run");
        }
    }
}
