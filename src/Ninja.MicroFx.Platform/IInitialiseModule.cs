namespace Ninja.MicroFx.Platform
{
    public interface IInitialiseModule
    {
        void Initialise(IMicroserviceSettings settings);
    }
}