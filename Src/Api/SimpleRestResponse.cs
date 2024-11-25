using System.Net;
using System.Text;
using Dumpify;
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


        ContentLength = 0;
        ContentType = null;
        StatusCode = new StatusCode(204, "No Resource");
        SetHeaders();
        WriteResponse();
    }

    public void Send<T>(T result)
    {

        Response.StatusCode = StatusCode.InternalServerError.Code;

        string finalOutput = JsonConvert.SerializeObject(result);
        ContentType = m_TypeParser.GetType<T>();
        Body = new SimpleRestBody(finalOutput);
        SetHeaders();
        WriteResponse();
    }

    public void View(string content, string contentType = "text/html; charset=utf-8")
    {
        ContentType = contentType;
        Body = new SimpleRestBody(content);
        SetHeaders();
        WriteResponse();
    }

    public void View(ISimpleRestView view, string contentType = "text/html; charset=urf-8")
    {
        ContentType = contentType;
        Body = new SimpleRestBody(view.GetView());
        SetHeaders();
        WriteResponse();
    }

    public void Redirect(string location, RedirectCode? redirectCode = null)
    {
        redirectCode = redirectCode ?? RedirectCode.TemporaryRedirect;
        Response.StatusCode = redirectCode.Code;
        Response.StatusDescription = redirectCode.Name;
        Body = new SimpleRestBody(redirectCode.ToString());
        SetHeaders();
        Response.Headers["Location"] = location;

    }

    void SetHeaders()
    {
        ContentLength = Body.Bytes.Length;
        Response.Headers = Headers;
        Response.ContentType = ContentType;
        Response.ContentLength64 = ContentLength;
        Response.StatusCode = StatusCode.Code;
    }

    void WriteResponse()
    {

        byte[] buffer = Body.Bytes;
        Response.OutputStream.Write(buffer, 0, buffer.Length);

        Response.Close();
        HasCompleted = true;
        // OnSend?.Invoke(Body.Content);

    }

    public void Error(StatusCode? statusCode = null)
    {
        statusCode ??= m_StatusCode;
        m_StatusCode = statusCode;
        Body = new SimpleRestBody(m_StatusCode.Message);
        SetHeaders();
        WriteResponse();
    }
}
