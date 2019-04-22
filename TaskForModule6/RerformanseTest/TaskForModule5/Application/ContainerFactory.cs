using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using TaskForModule5.FileBroker;
using TaskForModule5.LocaleSelectors;
using TaskForModule5.Localization;
using TaskForModule5.Logging;
using TaskForModule5.Settings;
using Unity;

namespace TaskForModule5.Application
{
    public class ContainerFactory : IContainerFactory
    {
        public IUnityContainer ConfigureApplicationContainer()
        {
            IUnityContainer unityContainer = new UnityContainer();
            unityContainer.RegisterType<ICustomStringLocalizer, CustomStringLocalizer>();
            unityContainer.RegisterType<ILogger, ConsoleLogger>();
            unityContainer.RegisterType<ILocaleSelector, ConsoleLocaleSelector>();
            unityContainer.RegisterType<IConfiguration, ConfigurationStub>();
            unityContainer.RegisterType<ISettings, Settings.Settings>();
            unityContainer.RegisterType<Application>();
            unityContainer.RegisterType<IStringLocalizer, StringLocalizerStub>();
            unityContainer.RegisterType<IFileBroker, FileBroker.FileBroker>();
            return unityContainer;
        }
    }
}