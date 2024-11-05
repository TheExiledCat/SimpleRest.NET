[TOC]

# SimpleREST.Net

_The simplest Rest Api framework for .Net_

## Getting Started

To create a basic rest api and serve it on a host and port number all you need to do is add `using SimpleRest.Api;` and the following code to your program:

```csharp
    static async Task Main(string[] args)
    {
        //Create the api
        SimpleRestApi api = new SimpleRestApi(3000);
        //Map a route handler using the async ApiMiddleWare delegate
        api.Get("/",async (req,res)=>{
            //send the result and terminate the connection
            res.Send("Hello World");
        });
        //map a route with uri segment parameters
        api.Put("/users/{id}",async (res,res)=>{
            int id = req.P
            res.Send()
        })
        //Start the server on given port with a callback to call before continuing with the rest of the server functions
        await api.Start((int port) =>
        {
            Console.WriteLine("Server started on port " + port);
        });
    }
```
