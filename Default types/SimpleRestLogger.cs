using Dumpify;
using SimpleRest.Api;

class SimpleRestLogger : ISimpleRestLogger
{
    SimpleRestLogLevel m_LogLevel;
    public SimpleRestLogger(SimpleRestLogLevel logLevel = SimpleRestLogLevel.NONE)
    {
        m_LogLevel = logLevel;

    }
    public void Log(string customMessage, SimpleRestLogLevel? loglevel = null)
    {
        Console.WriteLine($"[{DateTime.Now.ToShortDateString()}] {customMessage}");
    }

    public void Log(SimpleRestRequest request, SimpleRestLogLevel? loglevel = null)
    {
        loglevel ??= m_LogLevel;

        switch (loglevel)
        {
            case SimpleRestLogLevel.LOW:
                Console.WriteLine($"{request.Method.ToString()}: {request.Endpoint}");

                break;
            case SimpleRestLogLevel.MEDIUM:
                Console.WriteLine($"[{DateTime.Now.ToShortDateString()}] {request.Method.ToString()}: {request.Endpoint}");
                break;
            case SimpleRestLogLevel.HIGH:
                Console.WriteLine($"[{DateTime.Now.ToShortDateString()}] {request.Method.ToString()}: {request.Endpoint}{(request.Query?.Count > 0 ? "?" + string.Join("&", request.Query.Select(kvp => kvp.Key + "=" + kvp.Value)) : string.Empty)}");
                break;
            case SimpleRestLogLevel.LONG:
                Console.WriteLine($"[{DateTime.Now.ToLongDateString()}] {request.Method.ToString()}: {request.Endpoint}{(request.Query?.Count > 0 ? "?" + string.Join("&", request.Query.Select(kvp => kvp.Key + "=" + kvp.Value)) : string.Empty)}");
                break;

            case SimpleRestLogLevel.DEBUG:
                Console.WriteLine($@"[{DateTime.Now.ToLongDateString()}] {request.Method.ToString()}: {request.Endpoint}{'\n'}Query:{'\n'}{request.Query.DumpText(
                typeNames: new TypeNamingConfig { ShowTypeNames = false },
                tableConfig: new TableConfig
                {
                    ShowTableHeaders = false,
                },
                members: new MembersConfig { IncludeFields = true }




                )}");
                break;
        }
    }
}
