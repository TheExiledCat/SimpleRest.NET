namespace SimpleRest.Api;
public interface ISimpleRestUriTemplateFormatter
{
    public string GetTemplatePattern(string originalTemplatePattern);
}