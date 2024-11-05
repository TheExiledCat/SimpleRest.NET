using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using SimpleRest.Extensions;
namespace SimpleRest.Api;
internal class SimpleRestRequest
{
    public string Endpoint { get; private set; } = "";
    public object[]? Path { get; private set; }
    public SimpleRestQuery Query { get; private set; } = new SimpleRestQuery();
    public Dictionary<string, object?> Params { get; set; } = [];

    public SimpleRestRequestBody Body { get; private set; }
    public Dictionary<string, string>? Headers { get; private set; }
    public string? ContentType { get; private set; }
    public long ContentLength { get; private set; }
    public SimpleRestMethod Method { get; private set; }
    public string? UserAgent { get; private set; }

    public object? this[string key]
    {
        get => Params[key];
    }
    public static SimpleRestRequest FromHttpListenerContext(HttpListenerContext listener, ISimpleRestEndpointFormatter? endpointFormatter = null)
    {
        HttpListenerRequest contextRequest = listener.Request;

        SimpleRestRequest request = new SimpleRestRequest();
        request.Query = SimpleRestQuery.FromDictionary(contextRequest.QueryString.AllKeys.ToDictionary(k => k, k => contextRequest.QueryString[k].SafeDeserialize(contextRequest.QueryString[k])));
        request.Body = new SimpleRestRequestBody(contextRequest);
        request.Headers = contextRequest.Headers?.AllKeys.ToDictionary(k => k, k => contextRequest.Headers[k]);
        request.ContentType = contextRequest.ContentType;
        request.ContentLength = contextRequest.ContentLength64;
        string pathAndQuery = contextRequest.Url?.PathAndQuery ?? "/";
        request.Endpoint = (pathAndQuery.Contains('?') ? pathAndQuery?.Split("?")[0] : pathAndQuery) ?? "/";
        Console.WriteLine("Endpoint:" + request.Endpoint);

        request.Endpoint = endpointFormatter?.GetEndpoint(request.Endpoint) ?? request.Endpoint;
        Console.WriteLine("Endpoint:" + request.Endpoint);

        request.Method = Enum.Parse<SimpleRestMethod>(contextRequest.HttpMethod);
        // Get the raw URL and extract the path
        string? rawUrl = contextRequest.RawUrl;
        string? path = rawUrl?.Split('?')[0]; // Exclude any query string
        string[]? pathSegments = path?.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries); // Split by slash
        request.Path = pathSegments.Length <= 0 ? ["/"] : pathSegments;

        return request;
    }
    public class SimpleRestRequestBody
    {
        public SimpleRestRequestBody(HttpListenerRequest request)
        {
            using (var memoryStream = new MemoryStream())
            {
                request.InputStream.CopyTo(memoryStream);
                Bytes = memoryStream.ToArray();
            }
            Content = Encoding.UTF8.GetString(Bytes);
        }
        public TBody? GetContent<TBody>()
        {
            return JsonConvert.DeserializeObject<TBody>(Content);
        }
        public string Content { get; private set; }
        public byte[] Bytes { get; private set; }

        public T? As<T>() where T : class
        {

            return GetContent<T>();
        }
    }
}
internal class SimpleRestQuery : ISimpleRestQuery
{
    public static SimpleRestQuery FromDictionary(Dictionary<string, object?>? keyValuePairs)
    {
        SimpleRestQuery query = new SimpleRestQuery()
        {
            Query = new ReadOnlyDictionary<string, object?>(keyValuePairs ?? [])
        };
        return query;
    }
    public object? this[string key]
    {

        get
        {
            return Query.TryGetValue(key, out object val) ? val : null;
        }

    }

    public string[] Keys => Query.Keys.ToArray();

    public object?[] Values => Query.Values.ToArray();





    public ReadOnlyDictionary<string, object?> Query { get; private set; } = ReadOnlyDictionary<string, object?>.Empty;
}
