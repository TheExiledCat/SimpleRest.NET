[TOC]

# SimpleREST.Net

<h3 style="color:pink"> <i>The simplest Rest Api framework for .Net</i></h3>

## Introduction

SimpleREST.Net is a lightweight, easy-to-use, and highly customizable REST API framework for .Net
intended for creating small, fast and expendable RestAPis without any larger dependencies like in ASP.NET.

SimpleRest.Net was heavily based on express.js, so users familiar with express can easily migrate to .NET applications and libraries with SimpleRest.Net

## Installation 🔧

<a href="https://www.nuget.org/packages/SimpleRestApi/" target="_blank"><img alt="Nuget link" src="https://img.shields.io/nuget/v/SimpleRestApi?style=for-the-badge&logo=nuget&logoSize=auto&link=https%3A%2F%2Fwww.nuget.org%2Fpackages%2FSimpleRestApi%2F"></a>

For installation and getting started see [Getting Started](<./docs/guides/Getting Started.md>)

## Example ✅

To create a basic rest api and serve it on a host and port number all you need to do is add `using SimpleRest.Api;` and the following code to your program:

```csharp
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

  this will also set the content-type header to a fitting type based on the implementation of `SimpleRest.Api.ISimpleRestContentTypeParser` (injectable)

- Uri route parameters following the [RFC 6570 Spec](https://www.rfc-editor.org/rfc/rfc6570)

  ```csharp
    api.Put("/users/{userId}/posts/{postId}",async (req,res)=>{
            int? id = req["userId"] as int?;
            int postId = req["postId"] as int?;
            //or using generics and the out keyword to convert and assign directly with up to 12 named parameters
            req.Params.TryGet(out int userId, out int postId);
    });
  ```

- Parsing json body as object

  ```csharp
  api.Post("/users",async(req,res)=>{
    User? user = req.Body.As<User>()
  });
  ```

- Custom Api Handlers by inheriting from `SimpleRest.Api.SimpleRestApiHandler`

  ```csharp
    //example handler:
    api.Use(new ResourceNotFoundHandler());
    //every handler has overrides that get called by events in the api
    //This example will return a 404 on the response object
    //if after the middleware stack no middleware returned a response
  ```

- Expandable

  - using dependency injection(DI), Extension methods, Custom Middleware or Middleware handlers

## Planned Features

- [ ] Route Handlers that can divide endpoint handling into different classes (Like ASP)
- [ ] wildcards for routes (\*,?,+ etc..)
- [ ] Seperate routers that can have their own routes and middleware to seperate route logic
- [ ] More options for result sending, like response.Download(), response.Redirect() etc..
- [ ] Custom Error Handling and validation
- [ ] Route forwarding to move to a different route before sending result (like the express next() function)
- [ ] NativeAOT Compatibility (by migrating to System.Text.Json and source generation)
- [ ] Write Unit tests
- [ ] Write more extensive documentation
- and more...

## Extending

The SimpleRestApi Constructor allows for easy extending through dependency injection by adding your own custom implementations for things like:

- Loggers `ISimpleRestLogger`
- Formatting `ISimpleRestUriTemplateFormatter`
- Endpoint and route parsing `ISimpleRestEndpointFormatter`
- Middleware handlers and Api Handlers `ISimpleRestApiHandler`
- _More to come_

its also possible to add extension methods to the SimpleRestApi class to inject your own custom optional functions

## Documentation

See the [Documentation website](https://theexiledcat.github.io/SimpleRest.NET/html/index.html)
