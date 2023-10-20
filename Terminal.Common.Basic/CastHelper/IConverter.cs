namespace Terminal.Common.Basic.CastHelper;

public interface IConverter
{
    object Convert(object source, Type targetType, object parameter);
}