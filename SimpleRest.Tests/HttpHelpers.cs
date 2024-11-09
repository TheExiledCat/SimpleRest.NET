using System.Collections.Specialized;
using System.Web;
using SimpleRest.Api;

namespace SimpleRest.Tests.Helpers;

public static class HttpHelpers
{
    public static async Task<HttpResponseMessage> HttpCall(
        string hostAndPort,
        SimpleRestMethod method,
        string endpoint,
        Dictionary<string, object>? _parameters = null,
        Dictionary<string, object>? _query = null,
        string body = ""
    )
    {
        HttpClient client = new HttpClient();
        UriBuilder builder = new UriBuilder($"http://{hostAndPort}");
        builder.Path = endpoint;
        NameValueCollection query = new NameValueCollection();
        foreach (KeyValuePair<string, object?> kvp in query)
        {
            if (kvp.Value != null)
                query.Add(kvp.Key, kvp.Value?.ToString());
        }

        builder.Query = query.ToString();
        string url = builder.ToString();
        HttpContent content = new StringContent(body);
        switch (method)
        {
            case SimpleRestMethod.GET:
                return await client.GetAsync(url);
            case SimpleRestMethod.POST:
                return await client.PostAsync(url, content);
            case SimpleRestMethod.PUT:
                return await client.PutAsync(url, content);
            case SimpleRestMethod.DELETE:
                return await client.DeleteAsync(url);
            case SimpleRestMethod.PATCH:
                return await client.PatchAsync(url, content);
            case SimpleRestMethod.OPTIONS:
                return await client.SendAsync(new HttpRequestMessage(HttpMethod.Options, url));
            default:
                throw new NotSupportedException($"The HTTP method '{method}' is not supported.");
        }
    }
}
