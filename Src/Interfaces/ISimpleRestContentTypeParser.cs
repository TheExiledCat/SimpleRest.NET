namespace SimpleRest.Api;
public interface ISimpleRestContentTypeParser
{
    Dictionary<Type, string> ContentTypes { get; }

    /// <summary>
    /// Converts a .NET or custom type into an http content type
    /// </summary>
    /// <typeparam name="T">The type of the object to convert</typeparam>
    /// <returns>a string containing the http content type per MDN standard</returns>
    public string GetType<T>();

}
