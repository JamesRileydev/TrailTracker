using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace TrailTracker.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
           //Log.Logger = new LoggerConfiguration()
           //     .Enrich.FromLogContext()
           //     .WriteTo.Console()
           //     .CreateLogger();

           // Log.Information("Starting up application");

           Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                                .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration)
                    .Enrich.FromLogContext()
                    .WriteTo.Debug()
                    .WriteTo.Console(
                        outputTemplate: "[{Timestamp:HH:mm:ss}] {Message:l}{NewLine}{Exception}"))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseConfiguration(new ConfigurationBuilder()
                       .AddJsonFile("appsettings.json")
                       .Build());

                    webBuilder.UseStartup<Startup>();
                })

                .Build().Run();
        }
    }
}
