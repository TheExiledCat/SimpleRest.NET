using Newtonsoft.Json;
using Uri = UriTemplate.Core;
namespace SimpleRest.Extensions;
public static class SimpleRestExtensions
{
    public static object? SafeDeserialize(this string json, object fallbackValue)
    {
        try
        {
            return JsonConvert.DeserializeObject(json);
        }
        catch (JsonException je)
        {
            return fallbackValue;
        }
    }
    public static Uri.UriTemplate IgnoreTrailingSlash(this Uri.UriTemplate uriTemplate)
    {
        return new Uri.UriTemplate(uriTemplate.Template.TrimEnd('/'));
    }

}