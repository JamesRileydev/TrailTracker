using Serilog.Events;

namespace TrailTracker.API.Configuration
{
    public class LoggingConfig
    {
        public static string SourceContext { get; } = "SourceContext";

        public static string DefaultLogTemplate { get; } = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] [{SourceContext}] [{EventId}] {Message}{NewLine}{Exception}";

        public static LogEventLevel MinLevel { get; } = LogEventLevel.Verbose;

        public static string LogPathTemplate { get; } = "G:\\logs\\log-{Date}.txt";
    }
}
