using System;
using SimpleRest.Api;

namespace SimpleRest.Handlers;

public class CorsHandler : SimpleRestApiHandler
{
    string m_AllowedOrigins;

    public CorsHandler(string allowedOrigins = "*")
    {
        m_AllowedOrigins = allowedOrigins;
    }

    public override void OnResponseCreate(
        SimpleRestApi api,
        SimpleRestRequest request,
        SimpleRestResponse response
    )
    {
        base.OnResponseCreate(api, request, response);
        response.Headers?.Add("Access-Control-Allow-Origin", m_AllowedOrigins);
    }
}
