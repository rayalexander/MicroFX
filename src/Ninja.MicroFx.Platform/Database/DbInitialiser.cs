using System;

namespace Ninja.MicroFx.Platform.Database
{
    public class DbInitialiser:IInitialiseModule
    {
        public void Initialise(IMicroserviceSettings settings)
        {
           Console.WriteLine("Db Init ...has run");
        }
    }
}
