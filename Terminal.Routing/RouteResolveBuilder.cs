using System.IO.Compression;
using Terminal.SharedModels.Parsing;
using Terminal.SharedModels.Routing;

namespace Terminal.Routing;

public class RouteResolveBuilder
{
    private object obj;
    
    public RouteResolveBuilder(object obj)
    {
        this.obj = obj;
    }

    public RouteResolveBuilder FillParameters(Route route, ParsedCmdlet parsedCmdlet)
    {
        var zip = route.Parameters.Zip(parsedCmdlet.ParsedParameters,
            (parameter, parsedParameter) => new Tuple<Parameter, ParsedParameter>(parameter, parsedParameter))
            .ToList();
        zip.ForEach(x =>
        {
            
        });

        return this;
    }

    private bool ChangeType(string value, Type targetType, out object? result)
    {
        try
        {
            result = Convert.ChangeType(value, targetType);
            return true;
        }
        catch (Exception e)
        {
            result = null;
            return false;
        }
    }
}