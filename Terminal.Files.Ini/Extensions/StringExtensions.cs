namespace Terminal.Files.Ini.Extensions;

internal static class StringExtensions
{
    public static bool HasDecorator(this string line)
    {
        return line.StartsWith('@');
    }

    public static bool IsSection(this string line)
    {
        return line.StartsWith('[');
    }

    public static bool IsRelative(this string line)
    {
        return line.StartsWith('.');
    }

    public static bool IsComment(this string line)
    {
        return line.StartsWith(';') || line.StartsWith('#');
    }
}