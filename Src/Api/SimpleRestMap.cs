using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Uri = UriTemplate.Core;

namespace SimpleRest.Api;

public class SimpleRestMap
{
    public SimpleRestMethod Method { get; }

    // public Regex Pattern { get; }
    public ApiMiddleWare Middleware { get; }
    public Uri.UriTemplate Pattern { get; }
    public string Endpoint { get; }
    public ISimpleRestRouteHandler[]? RouteHandlers { get; private set; }

    public SimpleRestMap(
        string endpoint,
        SimpleRestMethod method,
        ApiMiddleWare middleWare,
        ISimpleRestUriTemplateFormatter? templateHandler = null,
        ISimpleRestRouteHandler[]? routeHandlers = null
    )
    {
        Method = method;
        string finalPattern = endpoint;
        finalPattern = templateHandler?.GetTemplatePattern(finalPattern) ?? finalPattern;
        Pattern = new Uri.UriTemplate(finalPattern);
        RouteHandlers = routeHandlers;
        Middleware = middleWare;
        Endpoint = finalPattern;
    }

    public override string ToString()
    {
        return Endpoint;
    }
}
