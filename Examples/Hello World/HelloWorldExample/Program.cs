using SimpleRest.Api;

public class Program
{
    static async Task Main()
    {
        SimpleRestApi api = new SimpleRestApi(3000, new SimpleRestLogger(SimpleRestLogLevel.DEBUG));
        api.Get("/", async (req, res) =>
        {
            res.Send("Hello World");
        });
        await api.Start((port, url) =>
        {
            Console.WriteLine("Server started: " + url);
        });
    }
}