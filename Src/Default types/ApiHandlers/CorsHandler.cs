using System;
using SimpleRest.Api;

namespace SimpleRest.Handlers;

public class CorsHandler : SimpleRestApiHandler
{
    string m_AllowedOrigins;
    string[] m_Routes;

    public CorsHandler(string allowedOrigins = "*", params string[] routes)
    {
        m_AllowedOrigins = allowedOrigins;
        m_Routes = routes;
    }

    public override void OnServerStart(SimpleRestApi api)
    {
        base.OnServerStart(api);
        if (m_Routes.Length == 0)
        {
            api.Options(
                async (req, res) =>
                {
                    res.Headers["Access-Control-Allow-Origin"] = m_AllowedOrigins;
                }
            );
        }
        else
        {
            foreach (var route in m_Routes)
            {
                api.Options(
                    route,
                    async (req, rest) =>
                    {
                        res.Headers["Access-Control-Allow-Origin"] = m_AllowedOrigins;
                    }
                );
            }
        }
    }
}
