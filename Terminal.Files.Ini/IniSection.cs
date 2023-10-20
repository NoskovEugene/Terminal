namespace Terminal.Files.Ini;

public class IniSection
{
    public string Name { get; set; }

    public string Decorator { get; set; }

    public IniSection? Parent { get; set; }

    public Dictionary<string, IniValue> Pairs { get; } = new();

    public string Path => Parent != null ? $"{Parent.Path}.{Name}" : Name;

    public void UpdateOrCreatePair(string key, string value)
    {
        if (Pairs.TryGetValue(key, out var iniValue))
        {
            iniValue.Value = value;
        }
        else
        {
            Pairs.Add(key, new IniValue { Value = value });
        }
    }

    public void TryUpdateValue(string key, string value)
    {
        if (Pairs.TryGetValue(key, out var iniValue))
        {
            iniValue.Value = value;
        }
    }
}