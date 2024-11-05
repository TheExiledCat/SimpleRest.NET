namespace SimpleRest.Api;
internal interface ISimpleRestLogger
{
    public void Log(string customMessage, SimpleRestLogLevel? logLevel = null);
    public void Log(SimpleRestRequest? request, SimpleRestLogLevel? logLevel = null);
}