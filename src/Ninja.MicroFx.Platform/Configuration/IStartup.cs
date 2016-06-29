using System.Web.Http;
using Owin;

namespace Ninja.MicroFx.Platform.Configuration
{
    public interface IStartup
    {
        void Configuration(IAppBuilder app, HttpConfiguration config);
    }
}