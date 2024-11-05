[TOC]

# SimpleREST.Net

_The simplest Rest Api framework for .Net_

## Example

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

## Features

- Easy to use
- Express.Js like route mapping and middleware
- Automatic response parsing
- Uri route parameters following the RFC 6570 Spec
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
