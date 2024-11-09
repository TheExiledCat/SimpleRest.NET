using SimpleRest.Api;

namespace SimpleRest.Handlers;

using SimpleRest.Views;

public class ResourceNotFoundHandler : SimpleRestApiHandler
{
    public override void OnBeforeRequestEnd(
        SimpleRestApi api,
        SimpleRestRequest request,
        SimpleRestResponse response
    )
    {
        base.OnBeforeRequestEnd(api, request, response);
        response.View(new ResourceNotFoundView());
    }
}
