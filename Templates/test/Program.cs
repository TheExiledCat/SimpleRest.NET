namespace test;

using SimpleRest.Api;

class Program
{
    static async Task Main(string[] args)
    {
        SimpleRestApi api = new SimpleRestApi(3000, new SimpleRestLogger(SimpleRestLogLevel.DEBUG));
        api.Get(
            "/",
            async (req, res) =>
            {
                res.Send("Hello World");
            }
        );
        await api.Start(
            (port, url) =>
            {
                Console.WriteLine("Started server: " + url);
            }
        );
    }
}
