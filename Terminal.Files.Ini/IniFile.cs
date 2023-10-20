using System.Text;
using Terminal.Extensions.Basic;

namespace Terminal.Files.Ini;

public class IniFile
{
    private string _filePath;

    public string FilePath
    {
        get => _filePath;
        set => _filePath = value;
    }

    public IReadOnlyList<IniSection> Sections => _sections;

    public IniSection UnnamedSection { get; } = new() { Name = "Unnamed" };
    
    private List<IniSection> _sections;

    public IniFile(string filePath)
    {
        _filePath = filePath;
        _sections = new();
    }

    public IniFile()
    {
        _filePath = string.Empty;
        _sections = new();
    }

    public void Clear()
    {
        _sections.Clear();
    }

    public void Add(IniSection section)
    {
        if (Sections.Any(x => x.Name == section.Name)) return;
        _sections.Add(section);
    }

    public IniValue this[string key]
    {
        get => UnnamedSection.Pairs[key];
        set => UnnamedSection.Pairs[key] = value; 
    }

    public string GetFileText()
    {
        var sb = new StringBuilder();
        foreach (var section in _sections)
        {
            var decorator = !section.Decorator.IsNullOrEmpty() ? $"@{section.Decorator};" : string.Empty;
            sb.AppendLine($"[{decorator}{section.Path}]");
            foreach (var keyValuePair in section.Pairs)
            {
                var builder = new StringBuilder();
                if (!keyValuePair.Value.Decorator.IsNullOrEmpty())
                {
                    builder.Append($"@{keyValuePair.Value.Decorator};");
                }
                builder.Append(keyValuePair.Key);
                builder.Append('=');
                builder.Append(keyValuePair.Value.Value);

                sb.AppendLine(builder.ToString());
            }
        }
        
        return sb.ToString();
    }

    public IniSection GetOrAddSectionByName(string name)
    {
        var section = GetSectionByName(name);
        if (section is not null) return section;
        var newSection = new IniSection { Name = name };
        _sections.Add(newSection);
        return newSection;
    }

    public IniSection? GetSectionByName(string name)
    {
        return _sections.FirstOrDefault(x=> x.Name.Equals(name));
    }
}