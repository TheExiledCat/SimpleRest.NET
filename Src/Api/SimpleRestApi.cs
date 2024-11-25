using System.Diagnostics;
using System.Net;
using Dumpify;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SimpleRest.Extensions;
using UriTemplate.Core;
using Uri = UriTemplate.Core;

namespace SimpleRest.Api;

public delegate Task ApiMiddleWare(SimpleRestRequest request, SimpleRestResponse response);

public class SimpleRestApi : IDisposable
{
    ISimpleRestLogger m_Logger;
    ISimpleRestContentTypeParser m_ResponseTypeParser;
    ISimpleRestUriTemplateFormatter m_UriTemplateFormatter;
    ISimpleRestEndpointFormatter m_EndpointFormatter;
    List<ISimpleRestApiHandler> m_Handlers = new List<ISimpleRestApiHandler>();
    HttpListener m_Listener;
    int m_Port;

    List<SimpleRestMap> m_Middleware = new List<SimpleRestMap>();
    Type m_DefaultIntType;
    public bool HasStarted { get; private set; } = false;
    public bool Disposed { get; private set; } = false;
    public event Action<SimpleRestApi>? OnServerStart;
    public event Action<SimpleRestApi>? OnBeforeRequestCreate;
    public event Action<SimpleRestApi, SimpleRestRequest>? OnRequestCreate;
    public event Action<SimpleRestApi, SimpleRestRequest>? OnBeforeResponseCreate;
    public event Action<SimpleRestApi, SimpleRestRequest, SimpleRestResponse>? OnResponseCreate;
    public event Action<SimpleRestApi, SimpleRestRequest>? OnLog;
    public event Action<
        SimpleRestApi,
        SimpleRestRequest,
        SimpleRestResponse,
        Dictionary<UriTemplateMatch, SimpleRestMap>
    >? OnHandleRequestStack;
    public event Action<
        SimpleRestApi,
        SimpleRestRequest,
        SimpleRestResponse,
        UriTemplateMatch,
        SimpleRestMap
    >? OnRequestMatch;
    public event Action<
        SimpleRestApi,
        SimpleRestRequest,
        SimpleRestResponse,
        UriTemplateMatch,
        SimpleRestMap
    >? OnApplyUriParams;
    public event Action<
        SimpleRestApi,
        SimpleRestRequest,
        SimpleRestResponse,
        UriTemplateMatch,
        SimpleRestMap
    >? OnBeforeRunMiddleware;
    public event Action<
        SimpleRestApi,
        SimpleRestRequest,
        SimpleRestResponse,
        UriTemplateMatch,
        SimpleRestMap
    >? OnRunMiddleware;
    public event Action<SimpleRestApi, SimpleRestRequest, SimpleRestResponse>? OnBeforeRequestEnd;
    public event Action<SimpleRestApi, SimpleRestRequest, SimpleRestResponse>? OnRequestEnd;

    public SimpleRestApi(
        int port,
        ISimpleRestLogger? logger = null,
        ISimpleRestContentTypeParser? responseParser = null,
        ISimpleRestUriTemplateFormatter? uriFormatter = null,
        ISimpleRestEndpointFormatter? endpointFormatter = null,
        JsonSerializerSettings? jsonSerializerSettings = null,
        Type? defaultIntType = null
    )
    {
        m_Logger = logger ?? new SimpleRestLogger();
        m_ResponseTypeParser = responseParser ?? new SimpleRestContentTypeParser();
        m_Listener = new HttpListener();
        m_UriTemplateFormatter = uriFormatter ?? new SimpleRestUriTemplateHandler();
        m_Port = port;
        m_Listener.Prefixes.Add("http://*:" + port + "/");
        JsonConvert.DefaultSettings = () =>
            jsonSerializerSettings
            ?? new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            };
        m_EndpointFormatter = endpointFormatter ?? new SimpleRestEndpointFormatter();
        m_DefaultIntType = defaultIntType ?? typeof(int);
    }

    void MapApiHandlers()
    {
        foreach (ISimpleRestApiHandler handler in m_Handlers)
        {
            OnServerStart += handler.OnServerStart;
            OnBeforeRequestCreate += handler.OnBeforeRequestCreate;
            OnRequestCreate += handler.OnRequestCreate;
            OnBeforeResponseCreate += handler.OnBeforeResponseCreate;
            OnResponseCreate += handler.OnResponseCreate;
            OnLog += handler.OnLog;
            OnHandleRequestStack += handler.OnHandleRequestStack;
            OnRequestMatch += handler.OnRequestMatch;
            OnApplyUriParams += handler.OnApplyUriParams;
            OnBeforeRunMiddleware += handler.OnBeforeRunMiddleware;
            OnRunMiddleware += handler.OnRunMiddleware;
            OnBeforeRequestEnd += handler.OnBeforeRequestEnd;
            OnRequestEnd += handler.OnRequestEnd;
        }
    }

    // void MapRouteHandlers()
    // {
    //     foreach (SimpleRestMap map in m_Middleware)
    //     {
    //         map.RouteHandlers.ToList()
    //             .ForEach(h =>
    //             {
    //                 OnRequestCreate += h.OnRequest;
    //                 OnResponseCreate += h.OnResponseCreate;
    //                 OnRequestEnd += h.OnResponse;
    //             });
    //     }
    // }

    void AddMiddleware(
        string endpoint,
        SimpleRestMethod method,
        ApiMiddleWare middleWare,
        ISimpleRestRouteHandler[]? routeHandlers = null
    )
    {
        m_Middleware.Add(
            new SimpleRestMap(endpoint, method, middleWare, m_UriTemplateFormatter, routeHandlers)
        );
    }

    public void Use(ISimpleRestApiHandler customHandler)
    {
        m_Handlers.Add(customHandler);
    }

    public void Map(
        string endpoint,
        ApiMiddleWare middleWare,
        params ISimpleRestRouteHandler[] routeHandlers
    )
    {
        AddMiddleware(endpoint, SimpleRestMethod.ANY, middleWare, routeHandlers);
    }

    public void All(ApiMiddleWare middleWare, params ISimpleRestRouteHandler[] routeHandlers)
    {
        AddMiddleware("/*", SimpleRestMethod.ANY, middleWare, routeHandlers);
    }

    public void Options(ApiMiddleWare middleWare, params ISimpleRestRouteHandler[] routeHandlers)
    {
        AddMiddleware("/*", SimpleRestMethod.OPTIONS, middleWare, routeHandlers);
    }

    public void Options(
        string endpoint,
        ApiMiddleWare middleWare,
        params ISimpleRestRouteHandler[] routeHandlers
    )
    {
        AddMiddleware(endpoint, SimpleRestMethod.OPTIONS, middleWare, routeHandlers);
    }

    public void Get(
        string endpoint,
        ApiMiddleWare middleWare,
        params ISimpleRestRouteHandler[] routeHandlers
    )
    {
        AddMiddleware(endpoint, SimpleRestMethod.GET, middleWare, routeHandlers);
    }

    public void Get(ApiMiddleWare middleWare, params ISimpleRestRouteHandler[] routeHandlers)
    {
        AddMiddleware("/*", SimpleRestMethod.GET, middleWare, routeHandlers);
    }

    public void Post(
        string endpoint,
        ApiMiddleWare middleWare,
        params ISimpleRestRouteHandler[] routeHandlers
    )
    {
        AddMiddleware(endpoint, SimpleRestMethod.POST, middleWare, routeHandlers);
    }

    public void Post(ApiMiddleWare middleWare, params ISimpleRestRouteHandler[] routeHandlers)
    {
        AddMiddleware("/*", SimpleRestMethod.POST, middleWare, routeHandlers);
    }

    public void Put(
        string endpoint,
        ApiMiddleWare middleWare,
        params ISimpleRestRouteHandler[] routeHandlers
    )
    {
        AddMiddleware(endpoint, SimpleRestMethod.PUT, middleWare, routeHandlers);
    }

    public void Put(ApiMiddleWare middleWare, params ISimpleRestRouteHandler[] routeHandlers)
    {
        AddMiddleware("/*", SimpleRestMethod.PUT, middleWare, routeHandlers);
    }

    public void Patch(
        string endpoint,
        ApiMiddleWare middleWare,
        params ISimpleRestRouteHandler[] routeHandlers
    )
    {
        AddMiddleware(endpoint, SimpleRestMethod.PATCH, middleWare, routeHandlers);
    }

    public void Patch(ApiMiddleWare middleWare, params ISimpleRestRouteHandler[] routeHandlers)
    {
        AddMiddleware("/*", SimpleRestMethod.PATCH, middleWare, routeHandlers);
    }

    public void Delete(
        string endpoint,
        ApiMiddleWare middleWare,
        params ISimpleRestRouteHandler[] routeHandlers
    )
    {
        AddMiddleware(endpoint, SimpleRestMethod.DELETE, middleWare, routeHandlers);
    }

    public void Delete(ApiMiddleWare middleWare, params ISimpleRestRouteHandler[] routeHandlers)
    {
        AddMiddleware("/*", SimpleRestMethod.DELETE, middleWare, routeHandlers);
    }

    public void Head(
        string endpoint,
        ApiMiddleWare middleWare,
        params ISimpleRestRouteHandler[] routeHandlers
    )
    {
        AddMiddleware(endpoint, SimpleRestMethod.HEAD, middleWare, routeHandlers);
    }

    public void Head(ApiMiddleWare middleWare, params ISimpleRestRouteHandler[] routeHandlers)
    {
        AddMiddleware("/*", SimpleRestMethod.HEAD, middleWare, routeHandlers);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="OnStartup"></param>
    /// <returns></returns>
    public async Task Start(Action<int, string>? OnStartup = null)
    {
        try
        {
            m_Listener.Start();
            MapApiHandlers();
            // MapRouteHandlers();
            m_Middleware.Dump();
            OnServerStart?.Invoke(this);
            OnStartup?.Invoke(m_Port, m_Listener.Prefixes.First().Replace("*", "localhost"));
            HasStarted = true;
            Disposed = false;
            while (!Disposed)
            {
                try
                {
                    HttpListenerContext context = await m_Listener.GetContextAsync();
                    OnBeforeRequestCreate?.Invoke(this);

                    SimpleRestRequest request = SimpleRestRequest.FromHttpListenerContext(
                        context,
                        m_EndpointFormatter
                    );
                    OnRequestCreate?.Invoke(this, request);
                    OnBeforeResponseCreate?.Invoke(this, request);
                    SimpleRestResponse response = new SimpleRestResponse(
                        context.Response,
                        m_ResponseTypeParser
                    );
                    OnResponseCreate?.Invoke(this, request, response);

                    m_Logger.Log(request);
                    OnLog?.Invoke(this, request);

                    await RunMiddleWare(request, response);
                    if (response.HasCompleted)
                        continue;
                    OnBeforeRequestEnd?.Invoke(this, request, response);
                    response.Return();
                    OnRequestEnd?.Invoke(this, request, response);
                }
                catch (ObjectDisposedException ode)
                {
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine(
                        "Something went wrong while getting request: "
                            + e.Message
                            + " "
                            + e.StackTrace
                    );
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
            .Where(m =>
                m.Pattern.Match(new System.Uri(request.Endpoint, UriKind.Relative)) != null
                || m.Endpoint == "/*"
            )
            .ToDictionary(
                m =>
                    m.Pattern.Match(new System.Uri(request.Endpoint, UriKind.Relative))
                    ?? new Uri.UriTemplate("/*").Match(new System.Uri("/*")),
                m => m
            );
        OnHandleRequestStack?.Invoke(this, request, response, matches);

        foreach (KeyValuePair<UriTemplateMatch, SimpleRestMap> match in matches)
        {
            if (response.HasCompleted)
                return;
            SimpleRestMap map = match.Value;
            UriTemplateMatch uriTemplateMatch = match.Key;

            if (map.Method == SimpleRestMethod.ANY || map.Method == request.Method)
            {

                OnRequestMatch?.Invoke(this, request, response, uriTemplateMatch, map);
                ApplyUriParams(uriTemplateMatch, request);
                OnApplyUriParams?.Invoke(this, request, response, uriTemplateMatch, map);
                map.RouteHandlers?.ToList()
                    .ForEach(h =>
                    {
                        h.OnRequest(this, request, response);
                        response.OnSend += (result) =>
                        {
                            h.OnResponse(this, request, response);
                        };
                    });

                OnBeforeRunMiddleware?.Invoke(this, request, response, uriTemplateMatch, map);
                await map.Middleware.Invoke(request, response);
                OnRunMiddleware?.Invoke(this, request, response, uriTemplateMatch, map);
            }
        }
    }

    void ApplyUriParams(UriTemplateMatch match, SimpleRestRequest request)
    {
        Dictionary<string, object?> paramsToAdd = new Dictionary<string, object?>();
        foreach (string key in match.Bindings.Keys)
        {
            paramsToAdd.Add(key, match.Bindings[key].Value);
            object? converted = null;
            try
            {
                converted = JsonConvert.DeserializeObject(
                    match.Bindings[key].Value.ToString() ?? "null"
                );
            }
            catch (JsonException je)
            {
                string stringifiedObject = $"\"{match.Bindings[key].Value.ToString()}\"";
                converted = JsonConvert.DeserializeObject(stringifiedObject ?? "null");
            }
            converted = ApplyIntType(converted);
            paramsToAdd[key] = converted;
        }
        request.Params.NonDistructiveUnion(paramsToAdd);
    }

    object? ApplyIntType(object? value)
    {
        if (
            value != null && value is short
            || value is ushort
            || value is int
            || value is uint
            || value is long
            || value is ulong
        )
        {
            return Convert.ChangeType(value, m_DefaultIntType);
        }
        return value;
    }

    public void Stop()


    {
        Disposed = true;

        m_Listener.Stop();
    }

    public void Dispose()
    {
        Stop();
    }
}
