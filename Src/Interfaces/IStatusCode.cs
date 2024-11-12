using System;

namespace SimpleRest.Api;

public interface IStatusCode
{
    public int Code { get; }
    public string Name { get; }
    public string Message { get; }
    public string Severity { get; }
}
