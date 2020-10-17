﻿using System;
using System.Threading.Tasks;
using lights.api.Proxy;
using lights.common.ArgumentParser;
using lights.common.Configuration;
using lights.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace lights
{
    class Program
    {
        static async Task Main(string[] args)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            var services = ConfigureServices(args);
            var serviceProvider = services.BuildServiceProvider();

            var parsedArguments = ArgumentParserBuilder
                .Create()
                .AddArgument<int?>(Constants.Arguments.Brightness, null)
                .AddArgument<int?>(Constants.Arguments.Temperature, null)
                .AddArgument<int?>(Constants.Arguments.TransitionTime, null)
                .AddFlag(Constants.Arguments.Interactive, false)
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
