using System.Text.RegularExpressions;

namespace SimpleRest.Api;
public class SimpleRestMap
{
    public SimpleRestMethod Method { get; }
    public Regex Pattern { get; }
    public ApiMiddleWare Middleware { get; }
    public string Endpoint { get; }
    public SimpleRestMap(string endpoint, SimpleRestMethod method, ApiMiddleWare middleWare)
    {
        Method = method;
        //extract params from url in format {param} and allow wildcards like * to be used
        // Convert `{param}` to named regex groups and `*` to single-segment wildcard
        // Escape special characters in the route pattern for Regex
        string regexPattern = Regex.Replace(endpoint, @"\{(.+?)\}", @"(?<$1>[^/]+)");

        // After capturing named parameters, handle wildcards (*)
        regexPattern = regexPattern.Replace("*", @"[^/]*");

        // Handle single-character optional wildcard (?)
        regexPattern = regexPattern.Replace("?", @"[^/]");

        // Ensure full match with anchors
        regexPattern = "^" + regexPattern + "$";


        // Return a compiled regex for performance
        Pattern = new Regex(regexPattern, RegexOptions.Compiled);
        Middleware = middleWare;
        Endpoint = endpoint;
    }
    public override string ToString()
    {
        return Endpoint;
    }
}