using Serilog;

namespace Terminal.Logging;

public interface ILogger<T> : ILogger
    where T: class
{
    
}