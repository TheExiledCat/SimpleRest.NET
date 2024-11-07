using SimpleRest.Api;

namespace SimpleRest.Views;
public class ResourceNotFoundView : ISimpleRestView
{
    public byte[] GetBytes()
    {
        throw new NotImplementedException();
    }

    public string GetView()
    {
        return "<h1>404: Resource or Route not found</h1>";
    }
}