namespace SimpleRest.Api;

public class SimpleRestContentTypeParser : ISimpleRestContentTypeParser
{
    Dictionary<Type, string> contentTypes = new Dictionary<Type, string>
    {
        { typeof(string), "text/plain" },
        { typeof(int), "text/plain" },
        { typeof(float), "text/plain" },
        { typeof(double), "text/plain" },
        { typeof(decimal), "text/plain" },
        { typeof(bool), "text/plain" },
        { typeof(byte), "application/octet-stream" },
        { typeof(byte[]), "application/octet-stream" },
        { typeof(DateTime), "application/json" },    // Serialize dates as JSON
        { typeof(Guid), "application/json" },         // Serialize GUIDs as JSON
        { typeof(object), "application/json" },         // Serialize custom objects as JSON
    };
    public Dictionary<Type, string> ContentTypes
    {
        get => contentTypes;
    }

    public string GetType<T>()
    {
        if (contentTypes.ContainsKey(typeof(T)))
            return contentTypes[typeof(T)];
        else
        {
            return contentTypes[typeof(string)];
        }
    }
}