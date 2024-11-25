using Dumpify;
using Newtonsoft.Json;
using SimpleRest.Api;
using SimpleRest.Handlers;

public class BodyHandler<T> : SimpleRestRouteHandler
{
    public override void OnRequest(
        SimpleRestApi api,
        SimpleRestRequest request,
        SimpleRestResponse response
    )
    {
        base.OnRequest(api, request, response);
        try
        {
            T Value = request.Body.As<T>();

        }
        catch (JsonException je)
        {
            response.Error(
                       response.StatusCode = new StatusCode(
                           422,
                           "Unprocessable entity",
                           je.Message
                       )
                   );
        }



    }
}
