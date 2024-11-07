[TOC]

# Installation

SimpleREST.NET is available as a NuGet package and works and has been tested in >= .NET 6.0

---

## Using NuGet:

```dotnet
dotnet add package SimpleREST
```

## Or from source using git

```git
git clone https://theexiledcat.github.io/SimpleRest.NET/
```

then add the using tag `using SimpleRest.Api`

# SimpleRest.NET Examples

## Basic Setup

The simplest way to create an API with SimpleRest.NET is to create a new instance of the `SimpleRest.Api.SimpleRestApi` class and define your routes. The following example shows how to set up a basic API with logging enabled:

```csharp
using SimpleRest.Api;

SimpleRestApi api = new SimpleRestApi(3000, new SimpleRestLogger(SimpleRestLogLevel.DEBUG));

api.Get("/", async (req, res) =>
{
    res.Send("Hello World");
});

await api.Start((int port) =>
{
    Console.WriteLine($"Server started on port {port}");
});
```

## URI Template Parameters

SimpleRest.NET supports URI template parameters following the RFC 6570 specification. This allows you to capture values from the URL path:

```csharp
// Single parameter
api.Get("/users/{id}", async (req, res) =>
{
    object? id = req.Params["id"];
    string? name = req.Query["name"] as string;

    res.Send($"User ID: {id}, Name: {name ?? "Not provided"}");
});

// Multiple parameters at once, with type casts handles for you
api.Get("/api/organizations/{orgId}/projects/{projectId}", async (req, res) =>
{
    req.Params.TryGet(out int orgId, out int projectId);

    res.Send(new {
        Organization = orgId,
        Project = projectId
    });
});
```

## JSON Request/Response Handling

SimpleRest.NET automatically handles JSON serialization and deserialization. Here's how to work with JSON data:

```csharp
public class User
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

api.Post("/users", async (req, res) =>
{
    // Parse JSON body to User object
    User? user = req.Body.As<User>();
    if (user != null)
    {
        // Response will automatically be serialized to JSON
        res.Send(new {
            Success = true,
            Message = $"User {user.Name} created"
        });
    }
    else
    {
        res.Send(new {
            Success = false,
            Message = "Invalid user data"
        });
    }
});
```

## Request Information

You can access various properties of the incoming request:

```csharp
api.Get("/info", async (req, res) =>
{
    res.Send(new {
        Method = req.Method,
        Endpoint = req.Endpoint,
        ContentType = req.ContentType,
        UserAgent = req.UserAgent
    });
});
```

## Shared Route Logic

The `Map` method allows you to apply shared logic to a route regardless of the HTTP method by mapping Middleware:

```csharp
api.Map("/api/items/{id}", async (req, res) =>
{
    Console.WriteLine($"Accessing item {req.Params["id"]}");
    // This middleware will run for any HTTP method on this route
});
```

## Complete Example

Here's a complete example that demonstrates multiple features of SimpleRest.NET:

```csharp
using SimpleRest.Api;

class Program
{
    static async Task Main(string[] args)
    {
        // Initialize API with logging
        SimpleRestApi api = new SimpleRestApi(3000, new SimpleRestLogger(SimpleRestLogLevel.DEBUG));

        // Basic route
        api.Get("/", async (req, res) =>
        {
            res.Send("Hello World");
        });

        // Route with URI template parameters
        api.Get("/users/{id}", async (req, res) =>
        {
            object? id = req.Params["id"];
            res.Send($"User ID: {id}");
        });

        // POST endpoint with JSON handling
        api.Post("/users", async (req, res) =>
        {
            User? user = req.Body.As<User>();
            res.Send(new { Success = true, User = user });
        });

        // Route with multiple parameters and query string
        api.Get("/api/organizations/{orgId}/projects/{projectId}", async (req, res) =>
        {
            object? orgId = req.Params["orgId"];
            object? projectId = req.Params["projectId"];
            string? filter = req.Query["filter"] as string;

            res.Send(new {
                Organization = orgId,
                Project = projectId,
                Filter = filter
            });
        });

        // Shared middleware
        api.Map("/api/items/{id}", async (req, res) =>
        {
            Console.WriteLine($"Accessing item {req.Params["id"]}");
        });

        // Start the server
        await api.Start((int port) =>
        {
            Console.WriteLine($"Server started on port {port}");
        });
    }
}

class User
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
```

## Key Features

- **Async Support**: All route handlers are async by default
- **Automatic JSON Serialization**: Response objects are automatically serialized to JSON or another corresponding type
- **URI Templates**: Following RFC 6570 specification
- **Query Parameters**: Accessible via `req.Query["paramName"]`(TODO: remove need for manual typecast)
- **URI Parameters**: Accessible via `req.Params["paramName"]` or `req.Params.TryGet<paramTypes>(out param1, outparam2, etc...)`
- **Request Body Parsing**: Using `req.Body.As<T>()`
- **Logging**: Built-in configurable logging system

## Common Usage Patterns

### Accessing Query Parameters

```csharp
string? value = req.Query["paramName"] as string;
```

### Parsing Request Body

```csharp
TBody? data = req.Body?.As<TBody>();
```

### Using URL Parameters

```csharp
int? id = req.Params["id"] as int?;
```

### Setting Response Data

```csharp
res.Send(new { message = "Success" });
```

## Notes

- The framework uses URI templates following the `RFC 6570` specification
- All handlers are async to ensure better performance
- JSON serialization is handled automatically
- The logging system can be configured with different levels (NONE, LOW, MEDIUM, HIGH, LONG, DEBUG), you can also create your own logger by implementing `ISimpleRestLogger`
- Route parameters are strongly typed when accessed and must be manually converted from `object?` to their corresponding type
