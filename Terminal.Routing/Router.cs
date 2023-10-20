using System.Reflection;
using Terminal.SharedModels.Parsing;
using Terminal.SharedModels.Routing;
using Terminal.SharedModels.Routing.Attributes;

namespace Terminal.Routing;

public class Router
{
    private readonly List<Route> _routes = new();

    public Router()
    {
    }

    public bool Add(Type type)
    {
        AppendIfNotExist(new [] { type });
        return true;
    }

    private void AppendIfNotExist(IEnumerable<Type> types)
    {
        var routes = types
            .Select(x => new { attribute = x.GetCustomAttribute<CmdletAttribute>(), type = x })
            .Where(x => x.attribute != null && !_routes.Any(t => t.Name == x.attribute.CmdletName && t.Namespace == x.attribute.CmdletNamespace))
            .Select(x =>
                new Route()
                {
                    Name = x.attribute.CmdletName,
                    Namespace = x.attribute.CmdletNamespace,
                    CmdletType = x.type,
                    Parameters = x.type.GetProperties()
                        .Select(y => new { att = y.GetCustomAttribute<CmdletParamAttribute>(), prop = y })
                        .Where(y => y.att != null)
                        .Select(y => new Parameter() { Index = y.att.ParameterIndex, PropertyInfo = y.prop })
                        .ToList()
                })
            .ToList();
        _routes.AddRange(routes);
    }

    public IEnumerable<Route> Find(ParsedCmdlet parsedCmdlet)
    {
        var routes = _routes
            .Where(x => x.Namespace == parsedCmdlet.Namespace & x.Name == parsedCmdlet.Name)
            .Where(x => x.Parameters.Count == parsedCmdlet.ParsedParameters.Count);
        return routes;
    }
}
