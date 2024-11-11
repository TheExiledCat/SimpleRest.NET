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

    public override void OnServerStart(SimpleRestApi api)
    {
        base.OnServerStart(api);
        api.Options(
            async (req, res) =>
            {
                res.Headers?.Add("Access-Control-Allow-Origin", m_AllowedOrigins);
            }
        );
    }
}
