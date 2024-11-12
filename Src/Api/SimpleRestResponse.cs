using System.Net;
using System.Text;
using Newtonsoft.Json;
using SimpleRest.Views;
using Spectre.Console;

namespace SimpleRest.Api;

public class SimpleRestResponse : ISimpleRestHttpObject
{
    public delegate void Result(string result = "");
    public HttpListenerResponse Response { get; private set; }
    ISimpleRestContentTypeParser m_TypeParser;
    public string? ContentType { get; private set; }
    public event Result? OnSend;
    public bool HasCompleted { get; private set; }
    StatusCode m_StatusCode = StatusCode.Success;
    public StatusCode StatusCode
    {
        get => m_StatusCode;
        set
        {
            if (value.Code.ToString().Length == 3)
            {
                m_StatusCode = value;
            }
        }
    }

    public SimpleRestBody Body { get; private set; }

    public WebHeaderCollection Headers { get; set; } = new WebHeaderCollection();

    public long ContentLength { get; private set; }

    public string? UserAgent { get; private set; }

    public SimpleRestResponse(HttpListenerResponse response, ISimpleRestContentTypeParser parser)
    {
        Response = response;
        m_TypeParser = parser;
        Body = new SimpleRestBody("");
    }

    public void Return()
    {
        if (!HasCompleted)
        {
            Response.StatusCode = StatusCode.InternalServerError.Code;
        }

        ContentLength = 0;
        ContentType = null;
        StatusCode = new StatusCode(204, "No Resource");
        SetHeaders();
        WriteResponse(null);
    }

    public void Send<T>(T result)
    {
        if (result == null && !HasCompleted)
        {
            Response.StatusCode = StatusCode.InternalServerError.Code;
        }
        string finalOutput = JsonConvert.SerializeObject(result);
        ContentType = m_TypeParser.GetType<T>();

        SetHeaders();
        WriteResponse(finalOutput);
    }

    public void View(string content, string contentType = "text/html; charset=utf-8")
    {
        ContentType = contentType;

        SetHeaders();
        WriteResponse(content);
    }

    public void View(ISimpleRestView view, string contentType = "text/html; charset=urf-8")
    {
        ContentType = contentType;

        SetHeaders();
        WriteResponse(view.GetView());
    }

    public void Redirect(string location, RedirectCode? redirectCode = null)
    {
        redirectCode = redirectCode ?? RedirectCode.TemporaryRedirect;
        Response.StatusCode = redirectCode.Code;
        Response.StatusDescription = redirectCode.Name;
        SetHeaders();
        Response.Headers["Location"] = location;
        WriteResponse(redirectCode.ToString());
    }

    void SetHeaders()
    {
        Response.Headers = Headers;
        Response.ContentType = ContentType;
        Response.ContentLength64 = ContentLength;
        Response.StatusCode = StatusCode.Code;
    }

    void WriteResponse(string? response)
    {
        if (response != null)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(response);
            Response.OutputStream.Write(buffer, 0, buffer.Length);
            Response?.Close();
            HasCompleted = true;
            OnSend?.Invoke(Encoding.UTF8.GetString(buffer));
            return;
        }
        Response?.Close();
        HasCompleted = true;
        OnSend?.Invoke();
    }
}
