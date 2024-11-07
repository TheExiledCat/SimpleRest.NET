#!/usr/bin/env python3

import sys
import os


def generate_cs_script(n, file):
    # Start building the C# script with an XML comment at the top
    cs_script = f"""
/// <summary>
/// This file is auto-generated. Do not edit it manually.
/// </summary>
using System;
using System.Collections.Generic;

namespace SimpleRest.Extensions.Native;

public static partial class NativeExtensions
{{
    /// <summary>
    /// Tries to get the value associated with the first key in the dictionary.
    /// </summary>
    /// <param name="value1">The value associated with the first key.</param>
    /// <returns>True if the value was found; otherwise, false.</returns>
    public static bool TryGet<TValue1>(this Dictionary<string, object?> dictionary, out TValue1? value1)
    {{
        if (dictionary == null || dictionary.Count == 0)
        {{
            throw new ArgumentException(nameof(dictionary));
        }}
        object?[] values = dictionary.Values.ToArray();
        value1 = (TValue1?)values[0];

        if (value1 != null)
        {{
            return true;
        }}

        return false;
    }}
    
"""

    # Generate overloads
    for i in range(2, n + 1):  # Start from 2 to avoid duplicating the first definition
        # Create the generic parameters for the overload
        generic_params = ", ".join([f"TValue{j + 1}" for j in range(i)])
        out_params_list = ", ".join(
            [f"out TValue{j + 1}? value{j + 1}" for j in range(i)]
        )
        value_assignments = "\n        ".join(
            [f"value{j + 1} = (TValue{j + 1}?)values[{j}];" for j in range(i)]
        )

        overload_comment = f"""
    /// <summary>
    /// Tries to get the values associated with the keys in the dictionary.
    /// The out values are ordered left to right based on the original keys in the dictionary.
    /// </summary>
    /// <param name="{', '.join([f'value{j + 1}' for j in range(i)])}">The values associated with the keys.</param>
    /// <returns>True if all values were found; otherwise, false.</returns>
"""
        overload = f"{overload_comment}    public static bool TryGet<{generic_params}>(this Dictionary<string, object?> dictionary, {out_params_list}) \n    {{\n        if (dictionary == null || dictionary.Count == 0) \n        {{\n            throw new ArgumentException(nameof(dictionary));\n        }}\n        object?[] values = dictionary.Values.ToArray();\n        \n        {value_assignments}\n\n        return { ' && '.join([f'value{j + 1} != null' for j in range(i)]) }; \n    }}\n"
        cs_script += overload

    cs_script += "}\n"

    # Write the generated script to the specified file
    with open(file, "w") as f:
        f.write(cs_script)


if __name__ == "__main__":
    if len(sys.argv) != 3:
        print("Usage: python generate_cs.py <n> <file>")
        sys.exit(1)

    n = int(sys.argv[1])
    file = sys.argv[2]

    # Validate n
    if n < 1 or n > 32:
        print("Error: n must be between 1 and 32.")
        sys.exit(1)

    # Get the current working directory
    current_dir = os.getcwd()
    # Create the full path for the output file
    full_path = os.path.join(current_dir, file)

    generate_cs_script(n, full_path)
    print(f"C# script generated at: {os.path.abspath(full_path)}")
