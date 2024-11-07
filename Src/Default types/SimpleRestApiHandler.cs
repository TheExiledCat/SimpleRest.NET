using SimpleRest.Api;
using UriTemplate.Core;

namespace SimpleRest.Handlers;
public abstract class SimpleRestApiHandler : ISimpleRestApiHandler
{
    public virtual void OnApplyUriParams(SimpleRestApi api, SimpleRestRequest request, SimpleRestResponse response, UriTemplateMatch match, SimpleRestMap routeMap)
    {

    }

    public virtual void OnBeforeRequestCreate(SimpleRestApi api)
    {
    }

    public virtual void OnBeforeRequestEnd(SimpleRestApi api, SimpleRestRequest request, SimpleRestResponse response)
    {
    }

    public virtual void OnBeforeResponseCreate(SimpleRestApi api, SimpleRestRequest request)
    {
    }

    public virtual void OnBeforeRunMiddleware(SimpleRestApi api, SimpleRestRequest request, SimpleRestResponse response, UriTemplateMatch match, SimpleRestMap routeMap)
    {
    }

    public virtual void OnHandleRequestStack(SimpleRestApi api, SimpleRestRequest request, SimpleRestResponse response, Dictionary<UriTemplateMatch, SimpleRestMap> matches)
    {
    }

    public virtual void OnLog(SimpleRestApi api, SimpleRestRequest request)
    {
    }

    public virtual void OnRequestCreate(SimpleRestApi api, SimpleRestRequest request)
    {
    }

    public virtual void OnRequestEnd(SimpleRestApi api, SimpleRestRequest request, SimpleRestResponse response)
    {
    }

    public virtual void OnRequestMatch(SimpleRestApi api, SimpleRestRequest request, SimpleRestResponse response, UriTemplateMatch match, SimpleRestMap routeMap)
    {
    }

    public virtual void OnResponseCreate(SimpleRestApi api, SimpleRestRequest request, SimpleRestResponse response)
    {
    }

    public virtual void OnRunMiddleware(SimpleRestApi api, SimpleRestRequest request, SimpleRestResponse response, UriTemplateMatch match, SimpleRestMap routeMap)
    {
    }

    public virtual void OnServerStart(SimpleRestApi api)
    {
    }
}