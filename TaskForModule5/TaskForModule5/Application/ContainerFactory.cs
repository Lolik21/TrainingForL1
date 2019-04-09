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
    public class ContainerFactory : IContainerFactory
    {
        public IUnityContainer ConfigureApplicationContainer()
        {
            IUnityContainer unityContainer = new UnityContainer();

            ICustomStringLocalizer customStringLocalizer = BuildCustomStringLocalizer();
            unityContainer.RegisterInstance(customStringLocalizer);
            unityContainer.RegisterType<ILogger, ConsoleLogger>();
            unityContainer.RegisterType<ILocaleSelector, ConsoleLocaleSelector>();

            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("settings.json", true, true).Build();
            unityContainer.RegisterInstance(configuration);
            unityContainer.RegisterType<ISettings, Settings.Settings>();
            unityContainer.RegisterType<Application>();
            unityContainer.RegisterType<IFileBroker, FileBroker.FileBroker>();
            return unityContainer;
        }

        public ICustomStringLocalizer BuildCustomStringLocalizer()
        {
            ILogger loggerStub = new LoggerStub();
            IStringLocalizer stringLocalizer = new JsonStringLocalizer(
                "Resources", "TaskForModule5", loggerStub);
            return new CustomStringLocalizer(stringLocalizer);
        }
    }
}