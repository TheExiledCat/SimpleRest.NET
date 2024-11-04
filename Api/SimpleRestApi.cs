using System.Net;
using Dumpify;

namespace SimpleRest.Api;
public delegate Task ApiMiddleWare(SimpleRestRequest request, SimpleRestResponse response);
public delegate void ApiMiddleWareHandler(string endpoint, ApiMiddleWare middleWare);
class SimpleRestApi
{

    ISimpleRestLogger m_Logger;
    ISimpleRestContentTypeParser m_ResponseTypeParser;
    HttpListener m_Listener;
    Dictionary<string, List<Tuple<SimpleRestMethod?, ApiMiddleWare>>> m_Middleware = [];

    int m_Port;
    public SimpleRestApi(int port, ISimpleRestLogger? logger = null, ISimpleRestContentTypeParser? responseParser = null)
    {
        m_Logger = logger ?? new SimpleRestLogger();
        m_ResponseTypeParser = responseParser ?? new SimpleRestContentTypeParser();
        m_Listener = new HttpListener();
        m_Port = port;
        m_Listener.Prefixes.Add("http://*:" + port + "/");


    }
    List<Tuple<SimpleRestMethod?, ApiMiddleWare>> FindOrCreateEndpoint(string endpoint)
    {
        if (!m_Middleware.ContainsKey(endpoint))
        {
            m_Middleware[endpoint] = [];

        }
        return m_Middleware[endpoint];
    }
    public void Map(string endpoint, ApiMiddleWare middleWare)
    {
        FindOrCreateEndpoint(endpoint).Add(new Tuple<SimpleRestMethod?, ApiMiddleWare>(SimpleRestMethod.ANY, middleWare));
    }


    public void Get(string endpoint, ApiMiddleWare middleWare)
    {
        FindOrCreateEndpoint(endpoint).Add(new Tuple<SimpleRestMethod?, ApiMiddleWare>(SimpleRestMethod.GET, middleWare));
    }
    public void Post(string endpoint, ApiMiddleWare middleWare)
    {
        FindOrCreateEndpoint(endpoint).Add(new Tuple<SimpleRestMethod?, ApiMiddleWare>(SimpleRestMethod.POST, middleWare));

    }
    public void Put(string endpoint, ApiMiddleWare middleWare)
    {
        FindOrCreateEndpoint(endpoint).Add(new Tuple<SimpleRestMethod?, ApiMiddleWare>(SimpleRestMethod.PUT, middleWare));

    }
    public void Patch(string endpoint, ApiMiddleWare middleWare)
    {
        FindOrCreateEndpoint(endpoint).Add(new Tuple<SimpleRestMethod?, ApiMiddleWare>(SimpleRestMethod.PATCH, middleWare));

    }
    public void Delete(string endpoint, ApiMiddleWare middleWare)
    {
        FindOrCreateEndpoint(endpoint).Add(new Tuple<SimpleRestMethod?, ApiMiddleWare>(SimpleRestMethod.DELETE, middleWare));

    }
    public void Head(string endpoint, ApiMiddleWare middleWare)
    {
        FindOrCreateEndpoint(endpoint).Add(new Tuple<SimpleRestMethod?, ApiMiddleWare>(SimpleRestMethod.HEAD, middleWare));

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

        foreach (Tuple<SimpleRestMethod?, ApiMiddleWare> middlewareMap in m_Middleware[request.Endpoint])
        {
            if (response.HasCompleted)
                return;
            if (middlewareMap.Item1 == SimpleRestMethod.ANY || middlewareMap.Item1 == request.Method)
                await middlewareMap.Item2.Invoke(request, response);
        }
    }
    public void Stop()
    {
        m_Listener.Close();
    }

}