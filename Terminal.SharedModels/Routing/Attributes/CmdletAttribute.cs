namespace Terminal.SharedModels.Routing.Attributes;


[AttributeUsage(AttributeTargets.Class)]
public class CmdletAttribute : Attribute
{
    public string CmdletName { get; }
    
    public string CmdletNamespace { get; }

    public CmdletAttribute(string cmdletNamespace, string cmdletName)
    {
        CmdletName = cmdletName;
        CmdletNamespace = cmdletNamespace;
    }
    
}