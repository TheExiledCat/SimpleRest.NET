namespace SimpleRest.Api;
public class SimpleRestEndpointFormatter : ISimpleRestEndpointFormatter
{
    bool m_IgnoreTrailingSlash;
    public SimpleRestEndpointFormatter(bool ignoreTrailingSlash = true)
    {
        m_IgnoreTrailingSlash = ignoreTrailingSlash;
    }
    public string GetEndpoint(string endpoint)
    {
        if (m_IgnoreTrailingSlash) return endpoint.Length > 1 ? endpoint.TrimEnd('/') : endpoint;

        return endpoint;
    }
}
