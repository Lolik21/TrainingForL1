using Unity;

namespace MongoDB
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            IContainerFactory containerFactory = new ConsoleContainerFactory();
            IUnityContainer unityContainer = containerFactory.ConfigureContainer();
            IApplication application = unityContainer.Resolve<IApplication>();
            application.Start();
        }
    }
}
