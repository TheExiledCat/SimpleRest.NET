using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using SimpleRest.Extensions;

namespace SimpleRest.Api;

/// <summary>
/// The main class for all incoming request data. This is a wrapper for the HttpListenerRequest class.
/// Contains one static Factory function to create requests from httplistenercontexts. Cannot be instantiated
/// <example>
/// <code>
/// Example: How the default SimpleRestApi creates the request
/// HttpListenerContext context = await listener.GetContextAsync();
/// SimpleRestRequest request = SimpleRestRequest.FromHttpListenerContext(context)
/// </code>
/// </example>
/// </summary>

public class SimpleRestRequest : ISimpleRestHttpObject
{
    public string Endpoint { get; private set; } = "";
    public object[]? Path { get; private set; }
    public SimpleRestQuery Query { get; private set; } = new SimpleRestQuery();
    public Dictionary<string, object?> Params { get; set; } = new Dictionary<string, object?>();

    public SimpleRestBody Body { get; private set; }
    public WebHeaderCollection Headers { get; private set; } = new WebHeaderCollection();
    public string? ContentType { get; private set; }
    public long ContentLength { get; private set; }
    public SimpleRestMethod Method { get; private set; }
    public string? UserAgent { get; private set; }

    private SimpleRestRequest() { }

    /// <param name="key">The url parameter from the UriTemplate</param>

    /// <returns> The URI param from the request if it exists</returns>
    public object? this[string key]
    {
        get => Params[key];
    }

    /// <summary>
    /// Factory function creating a Request object with all http request info stored on it
    /// </summary>
    /// <param name="listenerContext">The HttpListenerContext instance to generate the request from</param>
    /// <param name="endpointFormatter">An optional injectable Endpoint formatter class that can apply transformations to the url</param>
    /// <returns>The SimplerRestRequest generated from the <paramref name="listenerContext"/></returns>
    public static SimpleRestRequest FromHttpListenerContext(
        HttpListenerContext listenerContext,
        ISimpleRestEndpointFormatter? endpointFormatter = null
    )
    {
        HttpListenerRequest contextRequest = listenerContext.Request;

        SimpleRestRequest request = new SimpleRestRequest();
        if (contextRequest.QueryString.Count > 0)
        {
            request.Query = SimpleRestQuery.FromDictionary(
                contextRequest.QueryString.AllKeys.ToDictionary(
                    k => k ?? "",
                    k =>
                        contextRequest.QueryString[k].SafeDeserialize(contextRequest.QueryString[k])
                )
            );
        }
        else
        {
            request.Query = SimpleRestQuery.FromDictionary(new Dictionary<string, object?>());
        }

        request.Body = new SimpleRestBody(contextRequest);
        request.Headers = contextRequest
            .Headers?.AllKeys.ToDictionary(k => k, k => contextRequest.Headers[k])
            .ToWebHeaderCollection();
        request.ContentType = contextRequest.ContentType;
        request.ContentLength = contextRequest.ContentLength64;
        string pathAndQuery = contextRequest.Url?.PathAndQuery ?? "/";
        request.Endpoint =
            (pathAndQuery.Contains('?') ? pathAndQuery?.Split("?")[0] : pathAndQuery) ?? "/";
        Console.WriteLine("Endpoint:" + request.Endpoint);

        request.Endpoint = endpointFormatter?.GetEndpoint(request.Endpoint) ?? request.Endpoint;
        Console.WriteLine("Endpoint:" + request.Endpoint);

        request.Method = Enum.Parse<SimpleRestMethod>(contextRequest.HttpMethod);
        // Get the raw URL and extract the path
        string? rawUrl = contextRequest.RawUrl;
        string? path = rawUrl?.Split('?')[0]; // Exclude any query string
        string[]? pathSegments = path?.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries); // Split by slash
        request.Path = pathSegments.Length <= 0 ? new string[] { "/" } : pathSegments;

        return request;
    }
}

//TODO seperate SimpleRestRequestBody from SimpleRestRequest in a smart way
/// <summary>
/// The body of a <see cref="SimpleRest.Api.SimpleRestRequest"/>.
/// This class encapsulates the content of an HTTP request body, providing access to both raw byte data and
/// deserialized content.
/// </summary>
public class SimpleRestBody
{
    public SimpleRestBody(string contents)
    {
        Bytes = Encoding.UTF8.GetBytes(contents);

        Content = contents;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleRestBody"/> class using the specified <see cref="HttpListenerRequest"/>.
    /// </summary>
    /// <param name="request">The <see cref="HttpListenerRequest"/> from which to read the body content.</param>
    public SimpleRestBody(HttpListenerRequest request)
    {
        using (var memoryStream = new MemoryStream())
        {
            request.InputStream.CopyTo(memoryStream);
            Bytes = memoryStream.ToArray();
        }
        Content = Encoding.UTF8.GetString(Bytes);
    }

    /// <summary>
    /// Gets the content of the request body as a UTF-8 encoded string.
    /// </summary>
    public string Content { get; private set; }

    /// <summary>
    /// Gets the raw content of the request body as a byte array.
    /// </summary>
    public byte[] Bytes { get; private set; }

    /// <summary>
    /// The function <c>As&lt;T&gt;</c> returns the content as type <typeparamref name="T"/> if <typeparamref name="T"/> is a reference type.
    /// </summary>
    /// <typeparam name="T">The type to which the content should be deserialized.</typeparam>
    /// <returns>
    /// An instance of type <typeparamref name="T"/>, or <c>null</c> if the conversion fails.
    /// </returns>
    public T? As<T>()
    {
        return GetContent<T>();
    }

    /// <summary>
    /// The function <c>GetContent&lt;TBody&gt;</c> deserializes the <see cref="Content"/> property into an object of type
    /// <typeparamref name="TBody"/>.
    /// </summary>
    /// <typeparam name="TBody">The type to which the content should be deserialized.</typeparam>
    /// <returns>
    /// An instance of type <typeparamref name="TBody"/>, or <c>null</c> if the deserialization fails.
    /// </returns>
    public TBody? GetContent<TBody>()
    {
        return JsonConvert.DeserializeObject<TBody>(Content);
    }
}

public class SimpleRestQuery : ISimpleRestQuery
{
    public static SimpleRestQuery FromDictionary(Dictionary<string, object?>? keyValuePairs)
    {
        SimpleRestQuery query = new SimpleRestQuery()
        {
            Query = new ReadOnlyDictionary<string, object?>(
                keyValuePairs ?? new Dictionary<string, object?>()
            ),
        };
        return query;
    }

    public object? this[string key]
    {
        get { return Query.TryGetValue(key, out object val) ? val : null; }
    }

    public string[] Keys => Query.Keys.ToArray();

    public object?[] Values => Query.Values.ToArray();

    public ReadOnlyDictionary<string, object?> Query { get; private set; } =
        new ReadOnlyDictionary<string, object?>(new Dictionary<string, object?>());
}
