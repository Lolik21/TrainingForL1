using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeWebApplication.Configurations
{
    public interface ICustomConfiguration
    {
        void ConfigureServices(IServiceCollection services);
        void ConfigureAppBuilder(IApplicationBuilder app);
    }
}
