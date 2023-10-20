namespace Terminal.Common.Basic.CastHelper;

[AttributeUsage(AttributeTargets.Property)]
public class ConverterAttribute : Attribute
{
    public ConverterAttribute(Type converterType)
    {
        ConverterType = converterType;
    }

    public Type ConverterType { get; }
}