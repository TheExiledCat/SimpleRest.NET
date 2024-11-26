using Dumpify;
using Newtonsoft.Json;
using SimpleRest.Api;
using SimpleRest.Extensions.Native;
using SimpleRest.Handlers;

public class Program
{
    static async Task Main()
    {
        SimpleRestApi api = new SimpleRestApi(3000, new SimpleRestLogger(SimpleRestLogLevel.DEBUG));
        // api.Use(new CorsHandler());
        // api.Use(new ResourceNotFoundHandler());

        api.Get(
            "/",
            async (req, res) =>
            {
                res.Send("HelloWorld");
            }
        );
        api.Get("/users", async (req, res) =>
        {
            int? age = req.Query["age"] as int?;
            res.Send(new User { Age = age });

        });
        api.Post(
            "/users",
            async (req, res) =>
            {
                res.Send("POSTT");
            },
            new BodyHandler<User>()
        );
        api.Post("/users/{id}", async (req, res) =>
        {
            req.Params.TryGet(out int id);
            res.Send(id);
        });
        api.Post(
            "/",
            async (req, res) =>
            {
                res.Redirect("www.google.com", RedirectCode.TemporaryRedirect);
            }
        );
        await api.Start(
            (port, url) =>
            {
                Console.WriteLine("Server started: " + url);
            }
        );
    }
}
public class User
{
    public string Username { get; set; }
    public int? Age { get; set; }
}
