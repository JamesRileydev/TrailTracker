using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace TrailTracker.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
           Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseConfiguration(new ConfigurationBuilder()
                       .AddJsonFile("appsettings.json")
                       .Build());

                    webBuilder.UseStartup<Startup>();
                }).Build().Run();
        }
    }
}
