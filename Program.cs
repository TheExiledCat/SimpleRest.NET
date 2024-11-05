
using Dumpify;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace SimpleRest.Api;

class Program
{
    static async Task Main(string[] args)
    {
        SimpleRestApi api = new SimpleRestApi(3000, new SimpleRestLogger(logLevel: SimpleRestLogLevel.MEDIUM));
        var parameters = new ApiMiddleWare(async (req, res) =>
        {
            User? u = req.Body?.As<User>();
            u.Dump();
        });

        api.Post("/users", parameters);
        api.Get("/users/{id}", parameters);
        api.Get("/users/{userid}/posts{postid}", parameters);
        api.Get("/", async (req, res) =>
        {
            Console.WriteLine("this is  a get map");
            res.Send("Hello world");
        });
        await api.Start((int port) =>
        {
            Console.WriteLine("Server started on port " + port);
        });
    }
}
class User
{
    public string Name { get; set; }
    public string Email { get; set; }
}





