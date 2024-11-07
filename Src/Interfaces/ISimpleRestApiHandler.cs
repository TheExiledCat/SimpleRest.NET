using UriTemplate.Core;

namespace SimpleRest.Api;
public interface ISimpleRestApiHandler
{
    public void OnServerStart(SimpleRestApi api);
    public void OnBeforeRequestCreate(SimpleRestApi api);
    public void OnRequestCreate(SimpleRestApi api, SimpleRestRequest request);
    public void OnBeforeResponseCreate(SimpleRestApi api, SimpleRestRequest request);
    public void OnResponseCreate(SimpleRestApi api, SimpleRestRequest request, SimpleRestResponse response);
    public void OnLog(SimpleRestApi api, SimpleRestRequest request);
    public void OnHandleRequestStack(SimpleRestApi api, SimpleRestRequest request, SimpleRestResponse response, Dictionary<UriTemplateMatch, SimpleRestMap> matches);
    public void OnRequestMatch(SimpleRestApi api, SimpleRestRequest request, SimpleRestResponse response, UriTemplateMatch match, SimpleRestMap routeMap);
    public void OnApplyUriParams(SimpleRestApi api, SimpleRestRequest request, SimpleRestResponse response, UriTemplateMatch match, SimpleRestMap routeMap);
    public void OnBeforeRunMiddleware(SimpleRestApi api, SimpleRestRequest request, SimpleRestResponse response, UriTemplateMatch match, SimpleRestMap routeMap);
    public void OnRunMiddleware(SimpleRestApi api, SimpleRestRequest request, SimpleRestResponse response, UriTemplateMatch match, SimpleRestMap routeMap);
}