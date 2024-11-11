using System;
using System.Net;

namespace SimpleRest.Api;

public interface ISimpleRestHttpObject
{
    public SimpleRestBody Body { get; }
    public WebHeaderCollection Headers { get; }
    public string? ContentType { get; }
    public long ContentLength { get; }
    public string? UserAgent { get; }
}
