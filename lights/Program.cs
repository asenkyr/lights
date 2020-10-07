using System;
using System.Threading.Tasks;
using lights.api.Proxy;
using lights.common.ArgumentParser;
using lights.common.Configuration;
using lights.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace lights
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var services = ConfigureServices(args);
            var serviceProvider = services.BuildServiceProvider();

            var parsedArguments = ArgumentParserBuilder
                .Create()
                .AddFlag("i", false)
                .AddArgument<int?>(Constants.Arguments.Brightness, null)
                .AddArgument<int?>(Constants.Arguments.Temperature, null)
                .AddArgument<int?>(Constants.Arguments.TransitionTime, null)
                .Build()
                .Parse(args);

            await serviceProvider.GetService<RootController>().ProcessAsync(parsedArguments);
        }

        private static IServiceCollection ConfigureServices(string[] args)
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
