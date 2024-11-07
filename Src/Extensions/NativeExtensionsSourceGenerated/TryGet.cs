
/// <summary>
/// This file is auto-generated. Do not edit it manually.
/// </summary>
using System;
using System.Collections.Generic;

namespace SimpleRest.Extensions.Native;

public static partial class NativeExtensions
{
    /// <summary>
    /// Tries to get the value associated with the first key in the dictionary.
    /// </summary>
    /// <param name="value1">The value associated with the first key.</param>
    /// <returns>True if the value was found; otherwise, false.</returns>
    public static bool TryGet<TValue1>(this Dictionary<string, object?> dictionary, out TValue1? value1)
    {
        if (dictionary == null || dictionary.Count == 0)
        {
            throw new ArgumentException(nameof(dictionary));
        }
        object?[] values = dictionary.Values.ToArray();
        value1 = (TValue1?)values[0];

        if (value1 != null)
        {
            return true;
        }

        return false;
    }
    

    /// <summary>
    /// Tries to get the values associated with the keys in the dictionary.
    /// The out values are ordered left to right based on the original keys in the dictionary.
    /// </summary>
    /// <param name="value1, value2">The values associated with the keys.</param>
    /// <returns>True if all values were found; otherwise, false.</returns>
    public static bool TryGet<TValue1, TValue2>(this Dictionary<string, object?> dictionary, out TValue1? value1, out TValue2? value2) 
    {
        if (dictionary == null || dictionary.Count == 0) 
        {
            throw new ArgumentException(nameof(dictionary));
        }
        object?[] values = dictionary.Values.ToArray();
        
        value1 = (TValue1?)values[0];
        value2 = (TValue2?)values[1];

        return value1 != null && value2 != null; 
    }

    /// <summary>
    /// Tries to get the values associated with the keys in the dictionary.
    /// The out values are ordered left to right based on the original keys in the dictionary.
    /// </summary>
    /// <param name="value1, value2, value3">The values associated with the keys.</param>
    /// <returns>True if all values were found; otherwise, false.</returns>
    public static bool TryGet<TValue1, TValue2, TValue3>(this Dictionary<string, object?> dictionary, out TValue1? value1, out TValue2? value2, out TValue3? value3) 
    {
        if (dictionary == null || dictionary.Count == 0) 
        {
            throw new ArgumentException(nameof(dictionary));
        }
        object?[] values = dictionary.Values.ToArray();
        
        value1 = (TValue1?)values[0];
        value2 = (TValue2?)values[1];
        value3 = (TValue3?)values[2];

        return value1 != null && value2 != null && value3 != null; 
    }

    /// <summary>
    /// Tries to get the values associated with the keys in the dictionary.
    /// The out values are ordered left to right based on the original keys in the dictionary.
    /// </summary>
    /// <param name="value1, value2, value3, value4">The values associated with the keys.</param>
    /// <returns>True if all values were found; otherwise, false.</returns>
    public static bool TryGet<TValue1, TValue2, TValue3, TValue4>(this Dictionary<string, object?> dictionary, out TValue1? value1, out TValue2? value2, out TValue3? value3, out TValue4? value4) 
    {
        if (dictionary == null || dictionary.Count == 0) 
        {
            throw new ArgumentException(nameof(dictionary));
        }
        object?[] values = dictionary.Values.ToArray();
        
        value1 = (TValue1?)values[0];
        value2 = (TValue2?)values[1];
        value3 = (TValue3?)values[2];
        value4 = (TValue4?)values[3];

        return value1 != null && value2 != null && value3 != null && value4 != null; 
    }

    /// <summary>
    /// Tries to get the values associated with the keys in the dictionary.
    /// The out values are ordered left to right based on the original keys in the dictionary.
    /// </summary>
    /// <param name="value1, value2, value3, value4, value5">The values associated with the keys.</param>
    /// <returns>True if all values were found; otherwise, false.</returns>
    public static bool TryGet<TValue1, TValue2, TValue3, TValue4, TValue5>(this Dictionary<string, object?> dictionary, out TValue1? value1, out TValue2? value2, out TValue3? value3, out TValue4? value4, out TValue5? value5) 
    {
        if (dictionary == null || dictionary.Count == 0) 
        {
            throw new ArgumentException(nameof(dictionary));
        }
        object?[] values = dictionary.Values.ToArray();
        
        value1 = (TValue1?)values[0];
        value2 = (TValue2?)values[1];
        value3 = (TValue3?)values[2];
        value4 = (TValue4?)values[3];
        value5 = (TValue5?)values[4];

        return value1 != null && value2 != null && value3 != null && value4 != null && value5 != null; 
    }

    /// <summary>
    /// Tries to get the values associated with the keys in the dictionary.
    /// The out values are ordered left to right based on the original keys in the dictionary.
    /// </summary>
    /// <param name="value1, value2, value3, value4, value5, value6">The values associated with the keys.</param>
    /// <returns>True if all values were found; otherwise, false.</returns>
    public static bool TryGet<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6>(this Dictionary<string, object?> dictionary, out TValue1? value1, out TValue2? value2, out TValue3? value3, out TValue4? value4, out TValue5? value5, out TValue6? value6) 
    {
        if (dictionary == null || dictionary.Count == 0) 
        {
            throw new ArgumentException(nameof(dictionary));
        }
        object?[] values = dictionary.Values.ToArray();
        
        value1 = (TValue1?)values[0];
        value2 = (TValue2?)values[1];
        value3 = (TValue3?)values[2];
        value4 = (TValue4?)values[3];
        value5 = (TValue5?)values[4];
        value6 = (TValue6?)values[5];

        return value1 != null && value2 != null && value3 != null && value4 != null && value5 != null && value6 != null; 
    }

    /// <summary>
    /// Tries to get the values associated with the keys in the dictionary.
    /// The out values are ordered left to right based on the original keys in the dictionary.
    /// </summary>
    /// <param name="value1, value2, value3, value4, value5, value6, value7">The values associated with the keys.</param>
    /// <returns>True if all values were found; otherwise, false.</returns>
    public static bool TryGet<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7>(this Dictionary<string, object?> dictionary, out TValue1? value1, out TValue2? value2, out TValue3? value3, out TValue4? value4, out TValue5? value5, out TValue6? value6, out TValue7? value7) 
    {
        if (dictionary == null || dictionary.Count == 0) 
        {
            throw new ArgumentException(nameof(dictionary));
        }
        object?[] values = dictionary.Values.ToArray();
        
        value1 = (TValue1?)values[0];
        value2 = (TValue2?)values[1];
        value3 = (TValue3?)values[2];
        value4 = (TValue4?)values[3];
        value5 = (TValue5?)values[4];
        value6 = (TValue6?)values[5];
        value7 = (TValue7?)values[6];

        return value1 != null && value2 != null && value3 != null && value4 != null && value5 != null && value6 != null && value7 != null; 
    }

    /// <summary>
    /// Tries to get the values associated with the keys in the dictionary.
    /// The out values are ordered left to right based on the original keys in the dictionary.
    /// </summary>
    /// <param name="value1, value2, value3, value4, value5, value6, value7, value8">The values associated with the keys.</param>
    /// <returns>True if all values were found; otherwise, false.</returns>
    public static bool TryGet<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8>(this Dictionary<string, object?> dictionary, out TValue1? value1, out TValue2? value2, out TValue3? value3, out TValue4? value4, out TValue5? value5, out TValue6? value6, out TValue7? value7, out TValue8? value8) 
    {
        if (dictionary == null || dictionary.Count == 0) 
        {
            throw new ArgumentException(nameof(dictionary));
        }
        object?[] values = dictionary.Values.ToArray();
        
        value1 = (TValue1?)values[0];
        value2 = (TValue2?)values[1];
        value3 = (TValue3?)values[2];
        value4 = (TValue4?)values[3];
        value5 = (TValue5?)values[4];
        value6 = (TValue6?)values[5];
        value7 = (TValue7?)values[6];
        value8 = (TValue8?)values[7];

        return value1 != null && value2 != null && value3 != null && value4 != null && value5 != null && value6 != null && value7 != null && value8 != null; 
    }

    /// <summary>
    /// Tries to get the values associated with the keys in the dictionary.
    /// The out values are ordered left to right based on the original keys in the dictionary.
    /// </summary>
    /// <param name="value1, value2, value3, value4, value5, value6, value7, value8, value9">The values associated with the keys.</param>
    /// <returns>True if all values were found; otherwise, false.</returns>
    public static bool TryGet<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9>(this Dictionary<string, object?> dictionary, out TValue1? value1, out TValue2? value2, out TValue3? value3, out TValue4? value4, out TValue5? value5, out TValue6? value6, out TValue7? value7, out TValue8? value8, out TValue9? value9) 
    {
        if (dictionary == null || dictionary.Count == 0) 
        {
            throw new ArgumentException(nameof(dictionary));
        }
        object?[] values = dictionary.Values.ToArray();
        
        value1 = (TValue1?)values[0];
        value2 = (TValue2?)values[1];
        value3 = (TValue3?)values[2];
        value4 = (TValue4?)values[3];
        value5 = (TValue5?)values[4];
        value6 = (TValue6?)values[5];
        value7 = (TValue7?)values[6];
        value8 = (TValue8?)values[7];
        value9 = (TValue9?)values[8];

        return value1 != null && value2 != null && value3 != null && value4 != null && value5 != null && value6 != null && value7 != null && value8 != null && value9 != null; 
    }

    /// <summary>
    /// Tries to get the values associated with the keys in the dictionary.
    /// The out values are ordered left to right based on the original keys in the dictionary.
    /// </summary>
    /// <param name="value1, value2, value3, value4, value5, value6, value7, value8, value9, value10">The values associated with the keys.</param>
    /// <returns>True if all values were found; otherwise, false.</returns>
    public static bool TryGet<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TValue10>(this Dictionary<string, object?> dictionary, out TValue1? value1, out TValue2? value2, out TValue3? value3, out TValue4? value4, out TValue5? value5, out TValue6? value6, out TValue7? value7, out TValue8? value8, out TValue9? value9, out TValue10? value10) 
    {
        if (dictionary == null || dictionary.Count == 0) 
        {
            throw new ArgumentException(nameof(dictionary));
        }
        object?[] values = dictionary.Values.ToArray();
        
        value1 = (TValue1?)values[0];
        value2 = (TValue2?)values[1];
        value3 = (TValue3?)values[2];
        value4 = (TValue4?)values[3];
        value5 = (TValue5?)values[4];
        value6 = (TValue6?)values[5];
        value7 = (TValue7?)values[6];
        value8 = (TValue8?)values[7];
        value9 = (TValue9?)values[8];
        value10 = (TValue10?)values[9];

        return value1 != null && value2 != null && value3 != null && value4 != null && value5 != null && value6 != null && value7 != null && value8 != null && value9 != null && value10 != null; 
    }

    /// <summary>
    /// Tries to get the values associated with the keys in the dictionary.
    /// The out values are ordered left to right based on the original keys in the dictionary.
    /// </summary>
    /// <param name="value1, value2, value3, value4, value5, value6, value7, value8, value9, value10, value11">The values associated with the keys.</param>
    /// <returns>True if all values were found; otherwise, false.</returns>
    public static bool TryGet<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TValue10, TValue11>(this Dictionary<string, object?> dictionary, out TValue1? value1, out TValue2? value2, out TValue3? value3, out TValue4? value4, out TValue5? value5, out TValue6? value6, out TValue7? value7, out TValue8? value8, out TValue9? value9, out TValue10? value10, out TValue11? value11) 
    {
        if (dictionary == null || dictionary.Count == 0) 
        {
            throw new ArgumentException(nameof(dictionary));
        }
        object?[] values = dictionary.Values.ToArray();
        
        value1 = (TValue1?)values[0];
        value2 = (TValue2?)values[1];
        value3 = (TValue3?)values[2];
        value4 = (TValue4?)values[3];
        value5 = (TValue5?)values[4];
        value6 = (TValue6?)values[5];
        value7 = (TValue7?)values[6];
        value8 = (TValue8?)values[7];
        value9 = (TValue9?)values[8];
        value10 = (TValue10?)values[9];
        value11 = (TValue11?)values[10];

        return value1 != null && value2 != null && value3 != null && value4 != null && value5 != null && value6 != null && value7 != null && value8 != null && value9 != null && value10 != null && value11 != null; 
    }

    /// <summary>
    /// Tries to get the values associated with the keys in the dictionary.
    /// The out values are ordered left to right based on the original keys in the dictionary.
    /// </summary>
    /// <param name="value1, value2, value3, value4, value5, value6, value7, value8, value9, value10, value11, value12">The values associated with the keys.</param>
    /// <returns>True if all values were found; otherwise, false.</returns>
    public static bool TryGet<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TValue10, TValue11, TValue12>(this Dictionary<string, object?> dictionary, out TValue1? value1, out TValue2? value2, out TValue3? value3, out TValue4? value4, out TValue5? value5, out TValue6? value6, out TValue7? value7, out TValue8? value8, out TValue9? value9, out TValue10? value10, out TValue11? value11, out TValue12? value12) 
    {
        if (dictionary == null || dictionary.Count == 0) 
        {
            throw new ArgumentException(nameof(dictionary));
        }
        object?[] values = dictionary.Values.ToArray();
        
        value1 = (TValue1?)values[0];
        value2 = (TValue2?)values[1];
        value3 = (TValue3?)values[2];
        value4 = (TValue4?)values[3];
        value5 = (TValue5?)values[4];
        value6 = (TValue6?)values[5];
        value7 = (TValue7?)values[6];
        value8 = (TValue8?)values[7];
        value9 = (TValue9?)values[8];
        value10 = (TValue10?)values[9];
        value11 = (TValue11?)values[10];
        value12 = (TValue12?)values[11];

        return value1 != null && value2 != null && value3 != null && value4 != null && value5 != null && value6 != null && value7 != null && value8 != null && value9 != null && value10 != null && value11 != null && value12 != null; 
    }
}
