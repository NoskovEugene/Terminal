namespace Terminal.Files.Ini;

public class IniSection
{
    public string Name { get; set; }

    public string Decorator { get; set; }

    public IniSection? Parent { get; set; }

    public Dictionary<string, string> Pairs { get; } = new();

    public string Path => Parent != null ? $"{Parent.Path}.{Name}" : Name;

    public void UpdateOrCreatePair(string key, string value)
    {
        Pairs[key] = value;
    }

    public void TryUpdateValue(string key, string value)
    {
        if (Pairs.ContainsKey(key))
        {
            Pairs[key] = value;
        }
    }
}