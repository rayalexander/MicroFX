using System.Web.Http;
using Ninja.MicroFx.Platform.Configuration;

namespace Ninja.MicroFx
{
    public class Startup : BaseStartup
    {
        public Startup() : base(new HttpConfiguration())
        {
        }
    }
}

