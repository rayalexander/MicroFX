using System.Web.Http;
using Ninja.MicroFx.Platform.Configuration;
using Owin;

namespace Ninja.MicroFx
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            
            app.ConfigureWebApi(config);
            app.ConfigureIoC(config);

            app.ConfigureAutoMapper();

            SwaggerConfig.Register();

        }

    }
}

