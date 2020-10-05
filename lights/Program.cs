using System.Threading.Tasks;
using lights.api.Proxy;
using lights.common.Configuration;
using lights.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace lights
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            await serviceProvider.GetService<RootController>().ProcessAsync(args);
        }

        private static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            // configuration
            services.AddSingleton(ConfigurationFactory.Load());

            // controllers
            services.AddSingleton<RootController>();
            services.AddSingleton<LightsController>();
            services.AddSingleton<SensorsController>();
            services.AddSingleton<ScenesController>();
            services.AddSingleton<GroupsController>();
            services.AddSingleton<RulesController>();

            // proxies
            services.AddSingleton<LightsProxy>();
            services.AddSingleton<SensorsProxy>();
            services.AddSingleton<ScenesProxy>();
            services.AddSingleton<GroupsProxy>();
            services.AddSingleton<RulesProxy>();

            return services;
        }
    }
}
