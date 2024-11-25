namespace SimpleRest.Api;

public interface ISimpleRestRouteHandler
{
    public void OnRequest(
        SimpleRestApi api,
        SimpleRestRequest request,
        SimpleRestResponse response
    );
    public void OnResponse(
        SimpleRestApi api,
        SimpleRestRequest request,
        SimpleRestResponse response
    );
}
