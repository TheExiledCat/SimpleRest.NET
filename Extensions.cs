using Newtonsoft.Json;

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
}