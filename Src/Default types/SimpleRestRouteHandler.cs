using SimpleRest.Api;

namespace SimpleRest.Handlers;

public class SimpleRestRouteHandler : ISimpleRestRouteHandler
{
    public virtual void OnRequest(
        SimpleRestApi api,
        SimpleRestRequest request,
        SimpleRestResponse response
    )
    {
        ;
    }

    public virtual void OnResponse(
        SimpleRestApi api,
        SimpleRestRequest request,
        SimpleRestResponse response
    )
    {
        if (response.HasCompleted)
        {
            return;
        }
    }
}
