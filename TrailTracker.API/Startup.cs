using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using TrailTracker.API.Models;
using TrailTracker.API.Services;

namespace TrailTracker.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<TrailTrackerDatabaseSettings>(
                Configuration.GetSection(nameof(TrailTrackerDatabaseSettings)));

            services.AddSingleton<ITrailTrackerDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<TrailTrackerDatabaseSettings>>().Value);

            services.AddSingleton<TrailService>();

            services.AddControllers();

            services.Add(new ServiceDescriptor(typeof(TrailsContext),
                new TrailsContext(Configuration.GetConnectionString("DefaultConnection"))));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
