using Microsoft.VisualBasic;
using Terminal.Files.Ini.Extensions;

namespace Terminal.Files.Ini;

public static class IniFileParser
{
    public static IniFile Parse(IniFile file)
    {
        var lines = File.ReadAllLines(file.FilePath);

        var last = file.UnnamedSection;
        foreach (var line in lines)
        {
            if (line.IsSection())
            {
                var s = line.Trim('[', ']');
                if (TryParseSection(s, out var section, file)
                    && !file.Sections.Contains(section))
                {
                    file.Add(section);
                    last = section;                    
                }
                continue;
            }

            if (line.IsComment()) continue;

            if (TryParseValue(line, out var value))
            {
                last.Pairs.Add(value.key, value.value);
            }
        }

        return file;
    }

    public static IniFile Parse(string filePath)
    {
        return Parse(new IniFile(filePath));
    }
    
    private static bool TryParseValue(string input, out (string key, string value) valueTuple)
    {
        valueTuple = new ValueTuple<string, string>(string.Empty, string.Empty);
        var arr = input.Split('=');
        if (arr.Length != 2) return false;
        valueTuple.key = arr[0];
        valueTuple.value = arr[1];
        return true;
    }

    private static bool TryParseSection(string input, out IniSection section, IniFile file)
    {
        section = new IniSection();

        if (input.HasDecorator() && TryGetDecorator(input, out var result))
        {
            section.Decorator = result.decorator;
            input = result.substring;
        }

        if (input.IsRelative())
        {
            var sectionName = input.Trim('.');
            section.Name = sectionName;
            if(file.Sections.Count > 0)
                section.Parent = file.Sections[^1];
            return true;
        }

        if (input.Contains('.'))
        {
            var pointIndex = input.LastIndexOf('.');
            var sectionName = input[pointIndex..].TrimStart('.');
            var beforePoint = input[..pointIndex];
            if (beforePoint.Contains('.'))
            {
                var path = beforePoint;
                var parent = file.Sections.FirstOrDefault(x => x.Path.Equals(path));
                if (parent is null) return false;
                section.Parent = parent;
            }
            else
            {
                var parent = file.Sections.FirstOrDefault(x => x.Name.Equals(beforePoint));
                if (parent is null) return false;
                section.Parent = parent;
            }

            section.Name = sectionName;
            return true;
        }

        section.Name = input;
        return true;
    }


    private static bool TryGetDecorator(string input, out (string decorator, string substring) value)
    {
        value = default;
        var arr = input.Split(';');
        if (arr.Length != 2) return false;
        value.decorator = arr[0].TrimStart('@');
        value.substring = arr[1];
        return true;
    }
}