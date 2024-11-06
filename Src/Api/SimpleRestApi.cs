using System.Net;
using Dumpify;
using Newtonsoft.Json;
using SimpleRest.Extensions;
using UriTemplate.Core;
using Uri = UriTemplate.Core;
namespace SimpleRest.Api;
public delegate Task ApiMiddleWare(SimpleRestRequest request, SimpleRestResponse response);
public class SimpleRestApi
{

    ISimpleRestLogger m_Logger;
    ISimpleRestContentTypeParser m_ResponseTypeParser;
    ISimpleRestUriTemplateFormatter m_UriTemplateFormatter;
    ISimpleRestEndpointFormatter m_EndpointFormatter;
    HttpListener m_Listener;
    List<SimpleRestMap> m_Middleware = [];
    Type m_DefaultIntType;

    int m_Port;
    public SimpleRestApi(int port, ISimpleRestLogger? logger = null, ISimpleRestContentTypeParser? responseParser = null, ISimpleRestUriTemplateFormatter? uriFormatter = null, ISimpleRestEndpointFormatter? endpointFormatter = null, JsonSerializerSettings? jsonSerializerSettings = null, Type? defaultIntType = null)
    {
        m_Logger = logger ?? new SimpleRestLogger();
        m_ResponseTypeParser = responseParser ?? new SimpleRestContentTypeParser();
        m_Listener = new HttpListener();
        m_UriTemplateFormatter = uriFormatter ?? new SimpleRestUriTemplateHandler();
        m_Port = port;
        m_Listener.Prefixes.Add("http://*:" + port + "/");
        JsonConvert.DefaultSettings = () => jsonSerializerSettings ?? new JsonSerializerSettings();
        m_EndpointFormatter = endpointFormatter ?? new SimpleRestEndpointFormatter();
        m_DefaultIntType = defaultIntType ?? typeof(int);

    }

    void AddMiddleware(string endpoint, SimpleRestMethod method, ApiMiddleWare middleWare)
    {
        m_Middleware.Add(new SimpleRestMap(endpoint, method, middleWare, m_UriTemplateFormatter));

    }

    public void Map(string endpoint, ApiMiddleWare middleWare)
    {
        AddMiddleware(endpoint, SimpleRestMethod.ANY, middleWare);
    }



    public void Get(string endpoint, ApiMiddleWare middleWare)
    {
        AddMiddleware(endpoint, SimpleRestMethod.GET, middleWare);


    }
    public void Post(string endpoint, ApiMiddleWare middleWare)
    {
        AddMiddleware(endpoint, SimpleRestMethod.POST, middleWare);



    }
    public void Put(string endpoint, ApiMiddleWare middleWare)
    {
        AddMiddleware(endpoint, SimpleRestMethod.PUT, middleWare);



    }
    public void Patch(string endpoint, ApiMiddleWare middleWare)
    {
        AddMiddleware(endpoint, SimpleRestMethod.PATCH, middleWare);



    }
    public void Delete(string endpoint, ApiMiddleWare middleWare)
    {
        AddMiddleware(endpoint, SimpleRestMethod.DELETE, middleWare);



    }
    public void Head(string endpoint, ApiMiddleWare middleWare)
    {
        AddMiddleware(endpoint, SimpleRestMethod.HEAD, middleWare);



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
                    SimpleRestRequest request = SimpleRestRequest.FromHttpListenerContext(context, m_EndpointFormatter);
                    SimpleRestResponse response = new SimpleRestResponse(context.Response, m_ResponseTypeParser);
                    m_Logger.Log(request);
                    await RunMiddleWare(request, response);

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
    async Task RunMiddleWare(SimpleRestRequest request, SimpleRestResponse response)
    {
        Dictionary<UriTemplateMatch, SimpleRestMap> matches = m_Middleware
        .Where(
            m => m.Pattern.Match(new System.Uri(request.Endpoint, UriKind.Relative)) != null
            )
        .ToDictionary(
            m => m.Pattern.Match(new System.Uri(request.Endpoint, UriKind.Relative)), m => m
            );
        matches.Values.ToList().Dump();

        request.Dump();

        foreach (KeyValuePair<UriTemplateMatch, SimpleRestMap> match in matches)
        {
            if (response.HasCompleted)
                return;
            SimpleRestMap map = match.Value;
            UriTemplateMatch uriTemplateMatch = match.Key;

            if (map.Method == SimpleRestMethod.ANY || map.Method == request.Method)
            {
                Console.WriteLine($"Running middleware for route {map.Method.ToString()} {map.Endpoint}");

                ApplyUriParams(uriTemplateMatch, request);
                await map.Middleware.Invoke(request, response);
                //TODO until manual route switching is added, stop on the first matched path.



            }

        }

    }
    void ApplyUriParams(UriTemplateMatch match, SimpleRestRequest request)
    {

        request.Params.NonDistructiveUnion(match.Bindings.Select(b => new KeyValuePair<string, object?>(b.Key, ApplyIntType(JsonConvert.DeserializeObject(b.Value.Value?.ToString() ?? "null")))).ToDictionary(b => b.Key, b => (object?)b.Value));


    }
    object? ApplyIntType(object? value)
    {
        if (value != null && value is short
            || value is ushort
            || value is int
            || value is uint
            || value is long
            || value is ulong)
        {
            return Convert.ChangeType(value, m_DefaultIntType);
        }
        return value;

    }
    public void Stop()
    {
        m_Listener.Close();
    }

}
