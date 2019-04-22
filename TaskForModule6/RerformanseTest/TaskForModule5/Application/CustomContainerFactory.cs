using ContainerLibrary;
using LocalizationCultureCore.StringLocalizer;
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
    public class CustomContainerFactory
    {
        public IContainer ConfigureApplicationContainer()
        {
            IContainer container = new Container();
            container.Register<ICustomStringLocalizer, CustomStringLocalizer>();
            container.Register<ILogger, ConsoleLogger>();
            container.Register<ILocaleSelector, ConsoleLocaleSelector>();
            container.Register<IConfiguration, ConfigurationStub>();
            container.Register<ISettings, Settings.Settings>();
            container.Register<Application>();
            container.Register<IStringLocalizer, StringLocalizerStub>();
            container.Register<IUnityContainer, UnityContainer>();
            container.Register<IFileBroker, FileBroker.FileBroker>();
            return container;
        }
    }
}