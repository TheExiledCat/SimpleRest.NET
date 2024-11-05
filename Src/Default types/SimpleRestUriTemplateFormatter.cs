namespace SimpleRest.Api;
public class SimpleRestUriTemplateHandler : ISimpleRestUriTemplateFormatter
{
    public string GetTemplatePattern(string originalTemplatePattern)
    {
        string newPattern = originalTemplatePattern;
        //handle wildcards at end of url, still working on this to get it working with rfc 6570 and the uritemplate.net classes
        // newPattern = newPattern.Replace("/*", "/{:.*?}");
        return newPattern;
    }
}