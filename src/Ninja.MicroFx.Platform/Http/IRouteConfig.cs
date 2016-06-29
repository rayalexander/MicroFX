using System.Web.Http;

namespace Ninja.MicroFx.Platform.Http
{
    public interface IRouteConfig
    {
        void Configure(HttpConfiguration config);
    }
}