using Serilog;
using Serilog.Events;

namespace Terminal.Logging;

public class Logger<T> : ILogger<T>
    where T : class
{
    private readonly ILogger _internalLogger;
    
    public Logger(LoggerConfiguration config)
    {
        _internalLogger = config.Enrich.WithProperty("Class", typeof(T).Name).CreateLogger();
    }
    
    public void Write(LogEvent logEvent)
    {
        _internalLogger.Write(logEvent);
    }
}