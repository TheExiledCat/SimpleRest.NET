[TOC]

# SimpleREST.Net

<h3 style="color:pink"> <i>The simplest Rest Api framework for .Net</i></h3>

## Installation 🔧

<a href="https://www.nuget.org/packages/SimpleRestApi/" target="_blank"><img alt="Nuget link" src="https://img.shields.io/nuget/v/SimpleRestApi?style=for-the-badge&logo=nuget&logoSize=auto&link=https%3A%2F%2Fwww.nuget.org%2Fpackages%2FSimpleRestApi%2F"></a>

For installation and getting started see [Getting Started](./docs/guides/Getting Started.md)

## Example ✅

To create a basic rest api and serve it on a host and port number all you need to do is add `using SimpleRest.Api;` and the following code to your program:

```c#
    static async Task Main(string[] args)
    {
        //Create the api on port
        SimpleRestApi api = new SimpleRestApi(3000);

        //Map a route handler using the async ApiMiddleWare delegate
        //(allows for awaitable code)
        api.Get("/", async (req, res) =>
        {
            //send the result and terminate the connection
            res.Send("Hello World");
        });

        //Start the server on given port with a
        //callback to call before continuing with the rest of the server functions
        await api.Start((int port) =>
        {
            Console.WriteLine("Server started on port " + port);
        });
    }
```

## Features ⤵️

- Easy to use 👶

- Express.js like route mapping and middleware using SimpleRestApi.METHOD (see [routing](./docs/guides/Routing.md)) 🌐
  ```csharp
  api.Get("/route",async (req,res)=>{
      //use the request and response objects to your liking
      string sortBy = req.Query["sortBy"];
      //use one of the response terminating
      //functions to send a result to the client
      res.Send("Success!")
  });
  ```
- Automatic response parsing using Newtonsoft.Json (or a custom converter) 🫡

  ```csharp
  api.Get("/",async (req,res)=>{
    //use some .net or custom class and fill it with some query data, e.g. "John"
    User user = new User{Name = req.Query["name"],Id=1}
    res.send(user)
  });
  ```

  output:

  ```json
  {
    "name": "John",
    "Id": 1
  }
  ```

  this will also set the content-type header to a fitting type based on the implementation of [ISimpleRestContentTypeParser](./Src/Interfaces/ISimpleRestContentTypeParser.cs) (injectable)

- Uri route parameters following the [RFC 6570 Spec]("https://www.rfc-editor.org/rfc/rfc6570")

  ```csharp
    api.Put("/users/{userId}/posts/{postId}",async (req,res)=>{
            int? id = req["userId"] as int?;
            int postId = req["postId"] as int?;
            //or using generics and the out keyword to convert and assign directly with up to 12 names parameters
            req.Params.TryGet(out int userId, out int postId);
    });
  ```

- Parsing json body as object

  ```csharp
  api.Post("/users",async(req,res)=>{
    User? user = req.Body.As<User>()
  });
  ```

- Expandable

  - using dependency injection(DI), Extension methods, Custom Middleware or Middleware handlers

- NativeAOT Compatible (assuming you dont add non AOT compatible libraries)

## Planned Features

- [ ] Route Handlers that can divide endpoint handling into different classes (Like ASP)
- [ ] wildcards for routes (\*,?,+ etc..)
- [ ] Seperate routers that can have their own routes and middleware to seperate route logic
- [ ] More options for result sending, like response.Download(), response.Redirect() etc..
- [ ] Custom Error Handling and validation
- [ ] Route forwarding to move to a different route before sending result (like the express next() function)
- and more...

## Extending

The SimpleRestApi Constructor allows for easy extending through dependency injection by adding your own custom implementations for things like:

- Loggers
- Formatting
- Endpoint and route parsing
- Middleware
- _More to come_

its also possible to add extension methods to the SimpleRestApi class to inject your own custom optional functions

## Documentation
