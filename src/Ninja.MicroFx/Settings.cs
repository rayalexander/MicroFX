using Ninja.MicroFx.Platform;

namespace Ninja.MicroFx
{
    public class Settings : IMicroserviceSettings
    {
        public Settings(int? port, string serviceName, string description)
        {
            Port = port;
            ServiceName = serviceName;
            Description = description;
        }

        public int? Port { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
    }
}