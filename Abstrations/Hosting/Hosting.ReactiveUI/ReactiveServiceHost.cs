using Hosting.CommonObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReactiveUI;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;
using System;

namespace Hosting.ReactiveUI
{
    public static class ReactiveServiceHost
    {
        /// <summary>
        /// Configures the services in the host, including the DI container
        /// </summary>
        /// <param name="bootstrapContainer"></param>
        /// <returns></returns>
        public static IServiceProvider Create(Action<HostBuilderContext, IServiceCollection> bootstrapContainer)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

            var host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(cb => ConfiguratureAppConfiguration(cb))
                .ConfigureServices((cx, services) =>
                {
                    services.UseMicrosoftDependencyResolver();
                    var resolver = Locator.CurrentMutable;
                    resolver.InitializeSplat();
                    resolver.InitializeReactiveUI();

                    services.Configure<ClientConfiguration>(cx.Configuration.GetSection(ClientConfiguration.OptionsName));

                    ConfigureServices(cx, services, bootstrapContainer);

                }).Build();

            return host.Services;
        }

        /// <summary>
        /// Configures the application configuration file in the host
        /// </summary>
        /// <param name="config"></param>
        public static void ConfiguratureAppConfiguration(
            IConfigurationBuilder config)
        {
            config.Sources.Clear();
            config.AddJsonFile("appSettings.json");
        }

        /// <summary>
        /// Configures the services in the host, including the DI container
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="services"></param>
        /// <param name="bootstrapContainer"></param>
        public static void ConfigureServices(
            HostBuilderContext cx,
            IServiceCollection services,
            Action<HostBuilderContext, IServiceCollection> bootstrapContainer) =>
                bootstrapContainer(cx, services);
    }
}