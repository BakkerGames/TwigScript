namespace TwigScript;

public partial class Twig
{
    private string Get(string key)
    {
        if (_dict.TryGetValue(key, out string? value))
        {
            return value;
        }
        return "";
    }

    private void Set(string key, string value)
    {
        if (_dict.ContainsKey(key))
        {
            _dict[key] = value;
        }
        else
        {
            _dict.Add(key, value);
        }
    }

    private int GetInt(string key)
    {
        var value = Get(key);
        try
        {
            return ConvertToInt(value);
        }
        catch (Exception)
        {
            throw new SystemException($"Value is not numeric: [{key}] {value}");
        }
    }

    private bool GetBool(string key)
    {
        var value = Get(key);
        try
        {
            return ConvertToBool(value);
        }
        catch (Exception)
        {
            throw new SystemException($"Value is not boolean: [{key}] {value}");
        }
    }

    private bool ContainsKey(string key)
    {
        return _dict.ContainsKey(key);
    }

    private Dictionary<string, string> GetByPrefix(string prefix)
    {
        Dictionary<string, string> result = [];
        var keys = _dict.Keys.Where(x => x.StartsWith(prefix));
        foreach (string k in keys)
        {
            result.Add(k, _dict[k]);
        }
        return result;
    }
}