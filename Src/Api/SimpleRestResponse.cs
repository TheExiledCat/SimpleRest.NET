using System.Net;
using System.Text;
using Newtonsoft.Json;
using SimpleRest.Views;

namespace SimpleRest.Api;

public class SimpleRestResponse : ISimpleRestHttpObject
{
    public delegate void Result(string result);
    public HttpListenerResponse Response { get; private set; }
    ISimpleRestContentTypeParser m_TypeParser;
    public string? ContentType { get; private set; }
    public event Result? OnSend;
    public bool HasCompleted { get; private set; }
    ushort m_StatusCode = 200;
    public ushort StatusCode
    {
        get => m_StatusCode;
        set
        {
            if (value.ToString().Length == 3)
            {
                m_StatusCode = value;
            }
        }
    }

    public SimpleRestBody Body { get; private set; }

    public Dictionary<string, string>? Headers { get; set; }

    public long ContentLength { get; private set; }

    public string? UserAgent { get; private set; }

    public SimpleRestResponse(HttpListenerResponse response, ISimpleRestContentTypeParser parser)
    {
        Response = response;
        m_TypeParser = parser;
        Body = new SimpleRestBody("");
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
