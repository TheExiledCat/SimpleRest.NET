using System.Net;
using System.Text;
using Newtonsoft.Json;
using SimpleRest.Views;
namespace SimpleRest.Api;

public class SimpleRestResponse
{
    public delegate void Result(string result);
    public HttpListenerResponse Response { get; private set; }
    ISimpleRestContentTypeParser m_TypeParser;
    public string? ContentType { get; private set; }
    public event Result? OnSend;
    public bool HasCompleted { get; private set; }
    ushort statusCode = 200;
    public ushort StatusCode
    {
        get => statusCode; set
        {
            if (value.ToString().Length == 3)
            {
                statusCode = value;
            }
        }
    }
    public SimpleRestResponse(HttpListenerResponse response, ISimpleRestContentTypeParser parser)
    {
        Response = response;
        m_TypeParser = parser;
    }
    public void Send<T>(T result)
    {
        if (result == null)
        {
            Response.StatusCode = 500;
        }
        string finalOutput = JsonConvert.SerializeObject(result);
        ContentType = m_TypeParser.GetType<T>();
        byte[] buffer = Encoding.UTF8.GetBytes(finalOutput);
        Response.ContentLength64 = buffer.Length;
        Response.ContentType = ContentType;
        Response.StatusCode = StatusCode;

        Response.OutputStream.Write(buffer, 0, buffer.Length);
        Response?.Close();
        HasCompleted = true;
        OnSend?.Invoke(finalOutput);

    }
    public void View(string content, string contentType = "text/html; charset=urf-8")
    {

        ContentType = contentType;
        byte[] buffer = Encoding.UTF8.GetBytes(content);
        Response.ContentLength64 = buffer.Length;
        Response.StatusCode = StatusCode;
        Response.ContentType = ContentType;
        Response.OutputStream.Write(buffer, 0, buffer.Length);

        Response?.Close();
        HasCompleted = true;
        OnSend?.Invoke(content);

    }
    public void View(ISimpleRestView view, string contentType = "text/html; charset=urf-8")
    {

        ContentType = contentType;
        byte[] buffer = Encoding.UTF8.GetBytes(view.GetView());
        Response.ContentLength64 = buffer.Length;
        Response.StatusCode = StatusCode;
        Response.ContentType = ContentType;
        Response.OutputStream.Write(buffer, 0, buffer.Length);

        Response?.Close();
        HasCompleted = true;
        OnSend?.Invoke(view.GetView());

    }
}