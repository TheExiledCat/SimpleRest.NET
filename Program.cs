using System.Net;
using System.Security.Cryptography;
using System.Text;
namespace OpenChat;

using System.Collections.Specialized;
using System.Diagnostics;
using System.Text.Unicode;
using System.Web;
using Dumpify;
using Newtonsoft.Json;
using Spectre.Console;

class Program
{
    static void Main(string[] args)
    {
        SimpleRestApi api = new SimpleRestApi(3000, new SimpleRestLogger(logLevel: SimpleRestLogLevel.DEBUG));
        Task server = api.Start((int port) =>
        {
            Console.WriteLine("Server started on port " + port);
        });
        server.Wait();
    }
}
interface ISimpleRestLogger
{
    public void Log(string customMessage, SimpleRestLogLevel? logLevel = null);
    public void Log(SimpleRestRequest request, SimpleRestLogLevel? logLevel = null);
}
public enum SimpleRestLogLevel
{
    NONE,
    LOW,
    MEDIUM,
    HIGH,
    LONG,
    DEBUG
}
class SimpleRestLogger : ISimpleRestLogger
{
    SimpleRestLogLevel m_LogLevel;
    public SimpleRestLogger(SimpleRestLogLevel logLevel = SimpleRestLogLevel.NONE)
    {
        m_LogLevel = logLevel;

    }
    public void Log(string customMessage, SimpleRestLogLevel? loglevel = null)
    {
        Console.WriteLine($"[{DateTime.Now.ToShortDateString()}] {customMessage}");
    }

    public void Log(SimpleRestRequest request, SimpleRestLogLevel? loglevel = null)
    {
        SimpleRestLogLevel level = loglevel == null ? m_LogLevel : (SimpleRestLogLevel)loglevel;

        switch (level)
        {
            case SimpleRestLogLevel.LOW:
                Console.WriteLine($"{request.Method.ToString()}: {request.Endpoint}");

                break;
            case SimpleRestLogLevel.MEDIUM:
                Console.WriteLine($"[{DateTime.Now.ToShortDateString()}] {request.Method.ToString()}: {request.Endpoint}");
                break;
            case SimpleRestLogLevel.HIGH:
                Console.WriteLine($"[{DateTime.Now.ToShortDateString()}] {request.Method.ToString()}: {request.Endpoint}{(request.Query?.Count > 0 ? "?" + string.Join("&", request.Query.Select(kvp => kvp.Key + "=" + kvp.Value)) : string.Empty)}");
                break;
            case SimpleRestLogLevel.LONG:
                Console.WriteLine($"[{DateTime.Now.ToLongDateString()}] {request.Method.ToString()}: {request.Endpoint}{(request.Query?.Count > 0 ? "?" + string.Join("&", request.Query.Select(kvp => kvp.Key + "=" + kvp.Value)) : string.Empty)}");
                break;

            case SimpleRestLogLevel.DEBUG:
                Console.WriteLine($@"[{DateTime.Now.ToLongDateString()}] {request.Method.ToString()}: {request.Endpoint}{'\n'}Query:{'\n'}{request.Query.DumpText(
                typeNames: new TypeNamingConfig { ShowTypeNames = false },
                tableConfig: new TableConfig
                {
                    ShowTableHeaders = false,
                },
                members: new MembersConfig { IncludeFields = true }




                )}");
                break;
        }
    }
}
class SimpleRestApi
{
    public delegate object? ApiMiddleWare(SimpleRestRequest request, SimpleRestResponse response);
    // public delegate void ApiMiddleWareNext();
    // public delegate void ApiMiddleWareWithNext(ApiCallback callback, ApiMiddleWareNext next);
    public Dictionary<string, List<ApiMiddleWare>> m_Middleware = [];
    ISimpleRestLogger m_Logger;
    HttpListener m_Listener;
    int m_Port;
    public SimpleRestApi(int port, ISimpleRestLogger logger)
    {
        m_Listener = new HttpListener();
        m_Port = port;
        m_Listener.Prefixes.Add("http://*:" + port + "/");
        m_Logger = logger;


    }
    public void Map(string endpoint, ApiMiddleWare middleware)
    {
        if (m_Middleware[endpoint] == null)
        {
            m_Middleware[endpoint] = [];

        }
        m_Middleware[endpoint].Add(middleware);

    }
    public async Task Start(Action<int>? OnStartup = null)
    {
        try
        {
            m_Listener.Start();
            OnStartup?.Invoke(m_Port);
            while (true)
            {
                try
                {
                    HttpListenerContext context = await m_Listener.GetContextAsync();
                    SimpleRestRequest request = SimpleRestRequest.FromHttpListenerContext(context);
                    SimpleRestResponse response = new SimpleRestResponse();
                    foreach (ApiMiddleWare middleware in m_Middleware[request.Endpoint])
                    {
                        await Task.Run(() => { middleware.Invoke(request, response); });
                    }

                    m_Logger.Log(request);
                    context.Response.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Something went wrong while getting request: " + e.Message + " " + e.StackTrace);
                }
            }

        }
        catch (Exception e)
        {
            Console.WriteLine("Failure to open server: " + e.Message);
        }

    }
    public async Task RunMiddleWare(string endpoint, SimpleRestRequest request, SimpleRestResponse response)
    {
        m_Middleware[endpoint].Dump();
        await Task.Delay(1);
    }
    public void Close()
    {
        m_Listener.Close();
    }

}
public enum SimpleRestMethod
{
    GET,
    POST,
    HEAD,
    PATCH,
    PUT,
    DELETE,
    OPTIONS,
    OTHER

}
class SimpleRestRequest
{
    public string? Endpoint { get; private set; }
    public object[]? Path { get; private set; }
    public Dictionary<string, object?>? Query { get; private set; }
    public Dictionary<string, object?>? Params { get; private set; }
    public SimpleRestRequestBody? Body { get; private set; }
    public Dictionary<string, string>? Headers { get; private set; }
    public string? ContentType { get; private set; }
    public long ContentLength { get; private set; }
    public SimpleRestMethod Method { get; private set; }


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
        string rawUrl = contextRequest.RawUrl;
        string path = rawUrl.Split('?')[0]; // Exclude any query string
        string[] pathSegments = path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries); // Split by slash
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
        public TBody GetContent<TBody>()
        {
            return JsonConvert.DeserializeObject<TBody>(Content);
        }
        public string Content { get; private set; }
        public byte[] Bytes { get; private set; }


    }
}
class SimpleRestResponse
{
    public HttpListenerResponse? Response { get; private set; }
    public string ContentType { get; set; }

    public void Send<T>(T result)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result));
        Response?.OutputStream.Write(buffer, 0, buffer.Length);
        Response?.Close();
    }
}
class SimpleRestContentTypeParser : ISimpleRestContentTypeParser
{
    public Dictionary<Type, string> ContentTypes
    {
        get => new Dictionary<Type, string>()
        {

        }; private set;
    }
}
interface ISimpleRestContentTypeParser
{
    Dictionary<Type, string> contentType { get; }


}