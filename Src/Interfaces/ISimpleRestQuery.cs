using System.Collections.ObjectModel;

namespace SimpleRest.Api;
public interface ISimpleRestQuery
{
    public ReadOnlyDictionary<string, object?> Query { get; }
    public string[] Keys { get; }
    public object?[] Values { get; }
    public object? this[string key] { get; }
}
