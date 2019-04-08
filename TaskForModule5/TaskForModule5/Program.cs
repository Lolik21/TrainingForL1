using System;
using TaskForModule5.Application;
using TaskForModule5.Localization;
using Unity;

namespace TaskForModule5
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            IContainerFactory containerFactory = new ContainerFactory();
            IUnityContainer unityContainer = containerFactory.ConfigureApplicationContainer();
            IApplication application = unityContainer.Resolve<IApplication>();
            ICustomStringLocalizer customStringLocalizer = unityContainer.Resolve<ICustomStringLocalizer>();
            application.Run();
            Console.WriteLine(customStringLocalizer["PressEnterKey"]);
            Console.ReadLine();
        }
    }
}
