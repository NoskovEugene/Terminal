using System.Reflection;

namespace Terminal.SharedModels.Routing;

public class Parameter
{
    public int Index { get; init; }
    
    public PropertyInfo PropertyInfo { get; init; }

    public Type TargetType => PropertyInfo.PropertyType;
}