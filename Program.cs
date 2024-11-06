
using Dumpify;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SimpleRest.Api;
class Program
{
    static async Task Main(string[] args)
    {
        //Create the api
        SimpleRestApi api = new SimpleRestApi(3000, new SimpleRestLogger(SimpleRestLogLevel.DEBUG));
        api.Map("/users/{id}", async (req, res) =>
        {
            Console.WriteLine("doubling id");
            req.Params["id"] = (int?)req.Params["id"] * 2;
            Console.WriteLine("doubled id: " + req.Params["id"]);
        });
        //Map a route handler using the async ApiMiddleWare delegate (allows for async code)
        api.Get("/", async (req, res) =>
        {
            //send the result and terminate the connection
            User u = new User { Name = "Armando", Email = "Armando@test.com" };
            res.Send(u);
        });
        //map a route with uri segment parameters ((RFC 6570 Spec))
        api.Put("/users/{id}", async (req, res) =>
        {
            int? id = req.Params["id"] as int?;
            res.Send($"user with id {id} updated");
        });
        api.Post("/users", async (req, res) =>
        {
            //convert a custom or .NET class from the request body
            User? user = req.Body?.As<User>();
            //get query params
            string? token = req.Query["token"] as string;
            //automatically convert response to correct type for http content type standard, in this case JSON for classes
            res.Send(user);
        });
        //Start the server on given port with a callback to call before continuing with the rest of the server functions
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





