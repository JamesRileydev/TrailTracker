using Autofac;
using Serilog;
using Serilog.Exceptions;
using TrailTracker.API.Configuration;

namespace TrailTracker.API.Modules
{
    public class LoggingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ConfigureLogging)
                .AsSelf()
                .SingleInstance();

            builder.Register(ctx => ctx.Resolve<LoggerConfiguration>().CreateLogger())
                .As<ILogger>()
                .SingleInstance();
        }

        private LoggerConfiguration ConfigureLogging(IComponentContext context)
        {
            return new LoggerConfiguration()
                .Enrich.WithExceptionDetails()
                .Enrich.FromLogContext()
                .MinimumLevel.Is(Serilog.Events.LogEventLevel.Verbose)
                .WriteTo.Console(outputTemplate: LoggingConfig.DefaultLogTemplate)
                .WriteTo.RollingFile(LoggingConfig.LogPathTemplate, outputTemplate: LoggingConfig.DefaultLogTemplate);
                
        }
    }
}
