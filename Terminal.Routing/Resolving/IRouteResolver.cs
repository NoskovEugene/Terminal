namespace Terminal.Routing.Resolving;

public interface IRouteResolver
{
    void Register(Type t);

    bool CanResolve(Type t);
    
    RouteResolveResult Resolve(Type t);
}