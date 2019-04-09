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
            IUnityContainer unityContainer = new ContainerFactory().ConfigureApplicationContainer();
            ICustomStringLocalizer customStringLocalizer = unityContainer.Resolve<ICustomStringLocalizer>();
            unityContainer.Resolve<Application.Application>().Run();
            Console.WriteLine(customStringLocalizer["PressEnterKey"]);
            Console.ReadLine();
        }
    }
}
