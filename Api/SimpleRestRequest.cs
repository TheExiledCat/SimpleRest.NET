using System.Net;
using System.Text;
using Newtonsoft.Json;
using SimpleRest.Extensions;
namespace SimpleRest.Api;
public class SimpleRestRequest
{
    public string Endpoint { get; private set; } = "";
    public object[]? Path { get; private set; }
    public Dictionary<string, object?> Query { get; private set; } = [];
    public Dictionary<string, object?> Params { get; private set; } = [];
    public SimpleRestRequestBody? Body { get; private set; }
    public Dictionary<string, string>? Headers { get; private set; }
    public string? ContentType { get; private set; }
    public long ContentLength { get; private set; }
    public SimpleRestMethod Method { get; private set; }
    public string? UserAgent { get; private set; }


    public static SimpleRestRequest FromHttpListenerContext(HttpListenerContext listener)
    {
        HttpListenerRequest contextRequest = listener.Request;

        SimpleRestRequest request = new SimpleRestRequest();
        request.Query = contextRequest.QueryString.AllKeys.ToDictionary(k => k, k => contextRequest.QueryString[k].SafeDeserialize(contextRequest.QueryString[k]));
        request.Body = new SimpleRestRequestBody(contextRequest);
        request.Headers = contextRequest.Headers?.AllKeys.ToDictionary(k => k, k => contextRequest.Headers[k]);
        request.ContentType = contextRequest.ContentType;
        request.ContentLength = contextRequest.ContentLength64;
        request.Endpoint = contextRequest.Url?.AbsolutePath;
        request.Method = Enum.Parse<SimpleRestMethod>(contextRequest.HttpMethod);
        // Get the raw URL and extract the path
        string? rawUrl = contextRequest.RawUrl;
        string? path = rawUrl?.Split('?')[0]; // Exclude any query string
        string[]? pathSegments = path?.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries); // Split by slash
        request.Path = pathSegments;

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


    }
}
