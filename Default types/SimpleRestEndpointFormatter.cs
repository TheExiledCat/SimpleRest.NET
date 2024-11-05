namespace SimpleRest.Api;
internal class SimpleRestEndpointFormatter : ISimpleRestEndpointFormatter
{
    bool m_IgnoreTrailingSlash;
    public SimpleRestEndpointFormatter(bool ignoreTrailingSlash = true)
    {
        m_IgnoreTrailingSlash = ignoreTrailingSlash;
    }
    public string GetEndpoint(string endpoint)
    {
        if (m_IgnoreTrailingSlash) return endpoint.TrimEnd('/');

        return endpoint;
    }
}