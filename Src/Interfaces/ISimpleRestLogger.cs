namespace SimpleRest.Api;
public interface ISimpleRestLogger
{
    public void Log(string customMessage, SimpleRestLogLevel? logLevel = null);
    public void Log(SimpleRestRequest? request, SimpleRestLogLevel? logLevel = null);
}
