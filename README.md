[TOC]

# SimpleREST.Net

_The simplest Rest Api framework for .Net_

## Getting Started

To create a basic rest api and serve it on a host and port number all you need to do is add `using SimpleRest;` and the following:

```csharp
    static async Task Main(string[] args)
    {
        //Create the api
        SimpleRestApi api = new SimpleRestApi(3000);
        //Map some middleware, using the request and response objects optionally to change the data before the HTTP Methods get called on them
        api.Map("/",(req,res)=>{
            req.Query["name"] = req.Query["name"].ToUpper()
         })
        //Map an HTTP method as a middleware
        api.Get("/",(req,res)=>{
            //send object and terminate the connection and middleware chain
            res.Send("Hello World");
        })
        //Start the server on given port with a callback to call before continuing with the rest of the server functions
        await api.Start((int port) =>
        {
            Console.WriteLine("Server started on port " + port);
        });
    }
```
