namespace Terminal.SharedModels.Routing;

public class Route
{
    public string Namespace { get; init; }
    
    public string Name { get; init; }
    
    public Type CmdletType { get; init; }
    
    public List<Parameter> Parameters { get; init; }
}