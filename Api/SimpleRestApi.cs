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
    List<SimpleRestMap> m_Middleware = [];

    int m_Port;
    public SimpleRestApi(int port, ISimpleRestLogger? logger = null, ISimpleRestContentTypeParser? responseParser = null)
    {
        m_Logger = logger ?? new SimpleRestLogger();
        m_ResponseTypeParser = responseParser ?? new SimpleRestContentTypeParser();
        m_Listener = new HttpListener();
        m_Port = port;
        m_Listener.Prefixes.Add("http://*:" + port + "/");


    }

    public void Map(string endpoint, ApiMiddleWare middleWare)
    {
        m_Middleware.Add(new SimpleRestMap(endpoint, SimpleRestMethod.ANY, middleWare));
    }


    public void Get(string endpoint, ApiMiddleWare middleWare)
    {
        m_Middleware.Add(new SimpleRestMap(endpoint, SimpleRestMethod.GET, middleWare));

    }
    public void Post(string endpoint, ApiMiddleWare middleWare)
    {
        m_Middleware.Add(new SimpleRestMap(endpoint, SimpleRestMethod.POST, middleWare));


    }
    public void Put(string endpoint, ApiMiddleWare middleWare)
    {
        m_Middleware.Add(new SimpleRestMap(endpoint, SimpleRestMethod.PUT, middleWare));


    }
    public void Patch(string endpoint, ApiMiddleWare middleWare)
    {
        m_Middleware.Add(new SimpleRestMap(endpoint, SimpleRestMethod.PATCH, middleWare));


    }
    public void Delete(string endpoint, ApiMiddleWare middleWare)
    {
        m_Middleware.Add(new SimpleRestMap(endpoint, SimpleRestMethod.DELETE, middleWare));


    }
    public void Head(string endpoint, ApiMiddleWare middleWare)
    {
        m_Middleware.Add(new SimpleRestMap(endpoint, SimpleRestMethod.HEAD, middleWare));


    }
    public async Task Start(Action<int>? OnStartup = null)
    {
        try
        {
            m_Listener.Start();
            OnStartup?.Invoke(m_Port);
            m_Middleware.Dump();
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
        SimpleRestMap[] matches = m_Middleware.Where(m => m.Pattern.Match(request.Endpoint).Success).ToArray();
        matches.ToList().ForEach(m => m.Pattern.GetGroupNames().Dump());
        foreach (SimpleRestMap map in matches)
        {
            if (response.HasCompleted)
                return;

            if (map.Method == SimpleRestMethod.ANY || map.Method == request.Method)
                await map.Middleware.Invoke(request, response);
        }
    }
    public void Stop()
    {
        m_Listener.Close();
    }

}