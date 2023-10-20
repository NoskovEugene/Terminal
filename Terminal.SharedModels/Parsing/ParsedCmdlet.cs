namespace Terminal.SharedModels.Parsing;

public class ParsedCmdlet
{
    public string Namespace { get; set; }
    
    public string Name { get; set; }
    
    public List<ParsedParameter> ParsedParameters { get; set; }
}