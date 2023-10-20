using Microsoft.VisualBasic;

namespace Terminal.Files.Ini.Extensions;

public static class IniSectionExtensions
{
    public static void UpdateOrCreatePair(this IniSection section, string key, object value)
    {
        UpdateOrCreatePair(section, key, value, o => o.ToString() ?? string.Empty);
    }

    public static void UpdateOrCreatePair(this IniSection section,
        string key,
        object value,
        Func<object, string> toStringAction)
    {
        var strValue = toStringAction(value);
        section.UpdateOrCreatePair(key, strValue);
    }

    public static void TryUpdateValue(this IniSection section,
        string key,
        object value,
        Func<object, string> toStringAction)
    {
        var strValue = toStringAction(value);
        section.TryUpdateValue(key, strValue);
    }

    public static void TryUpdateValue(this IniSection section,
        string key,
        object value)
    {
        TryUpdateValue(section, key, value, o => o.ToString() ?? string.Empty);
    }
}