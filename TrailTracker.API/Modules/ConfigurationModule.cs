using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

namespace TrailTracker.API.Modules
{
    public class ConfigurationModule : Module
    {
        private string ConfigFilePath { get; }

        public ConfigurationModule(string configPath)
        {
            ConfigFilePath = configPath;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var services = new ServiceCollection();
            var config = LoadConfiguration();

            services.AddOptions()
                .AddLogging(logbuilder => { logbuilder.AddSerilog(); })
                .Configure<LoggerConfiguration>(config.GetSection("logging"));

            builder.Populate(services);
        }

        private IConfiguration LoadConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile(ConfigFilePath, optional: false, reloadOnChange: true)
                .Build();

            return config;
        }
    }
}
