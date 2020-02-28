using Autofac;
using Serilog;

namespace TrailTracker.API.Modules
{
    public class LoggingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ILogger>()
                .SingleInstance();
        }
    }
}
