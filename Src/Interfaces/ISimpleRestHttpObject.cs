using System;

namespace SimpleRest.Api;

public interface ISimpleRestHttpObject
{
    public SimpleRestBody Body { get; }
    public Dictionary<string, string>? Headers { get; }
    public string? ContentType { get; }
    public long ContentLength { get; }
    public string? UserAgent { get; }
}
