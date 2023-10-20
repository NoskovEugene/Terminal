using System.Reflection;

using Terminal.Common.Basic.CastHelper;

namespace Terminal.Files.Ini.Extensions;

public static class IniFileExtensions
{
    public static T Get<T>(this IniFile file, string sectionName) where T : new()
    {
        var props = typeof(T).GetProperties(BindingFlags.Public |
                                            BindingFlags.Instance |
                                            BindingFlags.SetProperty |
                                            BindingFlags.GetProperty);
        var obj = new T();
        var section = file.GetOrAddSectionByName(sectionName);
        foreach (var propertyInfo in props)
        {
            if (!section.Pairs.TryGetValue(propertyInfo.Name, out var pair)) continue;
            
            if (propertyInfo.PropertyType == typeof(string))
            {
                propertyInfo.SetValue(obj, pair.Value);
                continue;
            }
            
            var attribute = propertyInfo.GetCustomAttribute<ConverterAttribute>();
            object typed;
            if (attribute != null && typeof(IConverter).IsAssignableFrom(attribute.ConverterType))
            {
                var converter = (IConverter)Activator.CreateInstance(attribute.ConverterType)!;
                typed = converter.Convert(typeof(string), propertyInfo.PropertyType, pair.Value);
            }
            else
            {
                typed = Convert.ChangeType(pair.Value, propertyInfo.PropertyType);
            }
            
            propertyInfo.SetValue(obj, typed);
        }

        return obj;
    }

    public static T Get<T>(this IniFile file) where T : new()
    {
        return Get<T>(file, typeof(T).Name);
    }

    public static void Update(this IniFile file, object obj, string sectionName, bool addIfNotExist = true)
    {
        var props = obj.GetType().GetProperties(BindingFlags.Public |
                                                BindingFlags.Instance |
                                                BindingFlags.SetProperty |
                                                BindingFlags.GetProperty);
        var section = file.GetOrAddSectionByName(sectionName);
        foreach (var propertyInfo in props)
        {
            var value = propertyInfo.GetValue(obj)?.ToString() ?? string.Empty;
            if (addIfNotExist)
            {
                section.UpdateOrCreatePair(propertyInfo.Name, value);
            }
            else
            {
                section.TryUpdateValue(propertyInfo.Name, value);
            }
        }
    }

    public static void Update(this IniFile file, object obj, bool addIfNotExist = true)
    {
        Update(file, obj, obj.GetType().Name, addIfNotExist);
    }

    public static IniFile Load(this IniFile file, string filePath)
    {
        file.FilePath = filePath;
        IniFileParser.Parse(file);
        return file;
    }

    public static IniFile Save(this IniFile file)
    {
        File.WriteAllText(file.FilePath, file.GetFileText());
        return file;
    }

    public static IniFile Save(this IniFile file, string path)
    {
        file.FilePath = path;
        return Save(file);
    }
}