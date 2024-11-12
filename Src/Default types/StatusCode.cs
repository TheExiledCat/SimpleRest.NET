using System;

namespace SimpleRest.Api;

public class StatusCode : IStatusCode
{
    public int Code { get; private set; }
    public string Name { get; private set; }
    public string Message { get; private set; }
    public string Severity { get; private set; }

    public StatusCode(int code, string name = "", string message = "", string severity = "")
    {
        Code = code;
        Name = name;
        Message = message;
        Severity = severity;
    }

    public static readonly StatusCode Success = new StatusCode(
        200,
        "OK",
        "Operation completed successfully",
        "Info"
    );
    public static readonly StatusCode BadRequest = new StatusCode(
        400,
        "BadRequest",
        "Bad request",
        "Warning"
    );
    public static readonly StatusCode Unauthorized = new StatusCode(
        401,
        "Unauthorized",
        "Unauthorized access",
        "Error"
    );
    public static readonly StatusCode NotFound = new StatusCode(
        404,
        "NotFound",
        "Resource not found",
        "Warning"
    );
    public static readonly StatusCode InternalServerError = new StatusCode(
        500,
        "ServerError",
        "Internal server error",
        "Critical"
    );

    public override string ToString() => $"{Code} {Name}: {Message} (Severity: {Severity})";
}
