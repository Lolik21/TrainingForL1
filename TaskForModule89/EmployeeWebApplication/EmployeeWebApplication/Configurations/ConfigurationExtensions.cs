using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeWebApplication.Configurations
{
    public static class ConfigurationExtensions
    {
        public static void SetupServices(
            this ICustomConfiguration[] configurations,
            IServiceCollection serviceCollection)
        {
            foreach(var config in configurations)
            {
                config.ConfigureServices(serviceCollection);
            }
        }

        public static void SetupAppBuilder(
            this ICustomConfiguration[] configurations,
            IApplicationBuilder applicationBuilder)
        {
            foreach (var config in configurations)
            {
                config.ConfigureAppBuilder(applicationBuilder);
            }
        }

    }
}
