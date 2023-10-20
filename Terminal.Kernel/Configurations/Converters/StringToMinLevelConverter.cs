using Serilog.Events;
using Terminal.Common.Basic.CastHelper;

namespace Terminal.Kernel.Configurations.Converters;

public class StringToMinLevelConverter : IConverter
{
    public object Convert(object source, Type targetType, object parameter)
    {
        if (!targetType.IsEnum) return LogEventLevel.Debug;
        return Enum.TryParse(targetType, source.ToString(), out var result) ? result : LogEventLevel.Debug;
    }
}