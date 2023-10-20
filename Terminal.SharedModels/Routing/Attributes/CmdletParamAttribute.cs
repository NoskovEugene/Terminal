namespace Terminal.SharedModels.Routing.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class CmdletParamAttribute : Attribute
{
    public CmdletParamAttribute(byte parameterIndex)
    {
        ParameterIndex = parameterIndex;
    }

    public byte ParameterIndex { get; }
    
}