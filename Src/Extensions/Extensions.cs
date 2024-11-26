﻿using System.Net;
using System.Text.Json;
using Uri = UriTemplate.Core;

namespace SimpleRest.Extensions;

public static class SimpleRestExtensions
{
    public static object? SafeDeserialize(this string json, object fallbackValue, JsonSerializerOptions options)
    {
        try
        {
            return JsonSerializer.Deserialize<object?>(json);
        }
        catch (JsonException)
        {
            return fallbackValue;
        }
    }

    public static Uri.UriTemplate IgnoreTrailingSlash(this Uri.UriTemplate uriTemplate)
    {
        return new Uri.UriTemplate(uriTemplate.Template.TrimEnd('/'));
    }

    public static WebHeaderCollection ToWebHeaderCollection(
        this Dictionary<string, string> dictionary
    )
    {
        WebHeaderCollection webHeaderCollection = new WebHeaderCollection();
        foreach (KeyValuePair<string, string> kvp in dictionary)
        {
            webHeaderCollection.Add(kvp.Key, kvp.Value);
        }
        return webHeaderCollection;
    }

    public static void Merge<Tkey, TValue>(
        this Dictionary<Tkey, TValue> dictionary,
        Dictionary<Tkey, TValue> dictionaryToMerge
    )
        where Tkey : notnull
    {
        foreach (var kvp in dictionaryToMerge)
        {
            // If the key exists, update the value; otherwise, add the new key-value pair
            dictionary[kvp.Key] = kvp.Value;
        }
    }

    /// <summary>
    /// Adds key-value pairs from the source dictionary to the target dictionary
    /// only if the key does not already exist in the target dictionary.
    /// </summary>
    public static void NonDistructiveUnion<TKey, TValue>(
        this Dictionary<TKey, TValue> target,
        Dictionary<TKey, TValue> source
    )
        where TKey : notnull
    {
        foreach (var kvp in source)
        {
            // Only add the key-value pair if the key does not already exist in the target dictionary
            if (!target.ContainsKey(kvp.Key))
            {
                target[kvp.Key] = kvp.Value;
            }
        }
    }
}
