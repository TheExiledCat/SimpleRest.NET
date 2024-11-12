using System;
using Spectre.Console;

namespace SimpleRest.Api;

public class RedirectCode : StatusCode
{
    public RedirectCode(int code, string name = "", string message = "", string severity = "")
        : base(code, name, message, severity)
    {
        ;
    }

    public static RedirectCode MovedPermanently =>
        new RedirectCode(
            301,
            "Moved Permanently",
            "The resource has been moved permanently.",
            "info"
        );
    public static RedirectCode Found =>
        new RedirectCode(
            302,
            "Found",
            "The resource has been found at a different location.",
            "info"
        );
    public static RedirectCode SeeOther =>
        new RedirectCode(303, "See Other", "The response can be found at another URI.", "info");
    public static RedirectCode NotModified =>
        new RedirectCode(
            304,
            "Not Modified",
            "The resource has not been modified since the last request.",
            "info"
        );
    public static RedirectCode UseProxy =>
        new RedirectCode(
            305,
            "Use Proxy",
            "The requested resource must be accessed through the proxy.",
            "warning"
        );
    public static RedirectCode TemporaryRedirect =>
        new RedirectCode(
            307,
            "Temporary Redirect",
            "The resource is temporarily located at a different URI.",
            "info"
        );
    public static RedirectCode PermanentRedirect =>
        new RedirectCode(
            308,
            "Permanent Redirect",
            "The resource is permanently located at a different URI.",
            "info"
        );
}
