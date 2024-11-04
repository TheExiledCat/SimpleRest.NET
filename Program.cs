
using Dumpify;
namespace SimpleRest.Api;

class Program
{
    static async Task Main(string[] args)
    {
        SimpleRestApi api = new SimpleRestApi(3000, new SimpleRestLogger(logLevel: SimpleRestLogLevel.MEDIUM));

        api.Get("/", async (req, res) =>
        {
            Console.WriteLine("this is a get map");
            res.Send("Hello world");
        });
        await api.Start((int port) =>
        {
            Console.WriteLine("Server started on port " + port);
        });
    }
}






