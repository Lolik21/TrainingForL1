using System;
using System.Diagnostics;
using ContainerLibrary;
using TaskForModule5.Application;
using Unity;

namespace TaskForModule5
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine("Unity container:");
            stopwatch.Start();
            IUnityContainer unityContainer = new ContainerFactory().ConfigureApplicationContainer();
            Console.WriteLine($"Register: {stopwatch.Elapsed}");
            stopwatch.Restart();
            Application.Application application1 = unityContainer.Resolve<Application.Application>();
            Console.WriteLine($"Resolve: {stopwatch.Elapsed}");

            Console.WriteLine("Custom:");
            stopwatch.Restart();
            IContainer container = new CustomContainerFactory().ConfigureApplicationContainer();
            Console.WriteLine($"Register: {stopwatch.Elapsed}");
            stopwatch.Restart();
            Application.Application application2 = container.Resolve<Application.Application>();
            Console.WriteLine($"Resolve: {stopwatch.Elapsed}");

            Console.ReadLine();
        }
    }
}
