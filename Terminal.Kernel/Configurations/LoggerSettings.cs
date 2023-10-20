using Serilog.Events;
using Terminal.Common.Basic.CastHelper;
using Terminal.Kernel.Configurations.Converters;

namespace Terminal.Kernel.Configurations;

public class LoggerSettings
{
    [Converter(typeof(StringToMinLevelConverter))]
    public LogEventLevel MinimumLevel { get; set; }
    
    public string Template { get; set; }
}