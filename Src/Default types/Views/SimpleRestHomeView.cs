using System.Runtime.CompilerServices;
using SimpleRest.Api;

namespace SimpleRest.Views;
public class SimpleRestHomeView : ISimpleRestView
{
    public byte[] GetBytes()
    {
        throw new NotImplementedException();
    }

    public string GetView()
    {
        string fullPath = GetRelativeFilePath("../../Templates/home.html");
        StreamReader reader = File.OpenText(fullPath);
        return reader.ReadToEnd();
    }
    public string GetRelativeFilePath(string pathFromCurrentFile, [CallerFilePath] string sourceFilePath = "")
    {
        string sourceDirectory = Path.GetDirectoryName(sourceFilePath);
        return Path.Combine(sourceDirectory, pathFromCurrentFile);
    }
}