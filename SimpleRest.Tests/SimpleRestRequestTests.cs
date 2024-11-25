namespace SimpleRest.Tests;

using SimpleRest.Api;
using SimpleRest.Tests.Helpers;

[Collection("Sequential api tests")]
public class SimpleRestRequestTests : IDisposable
{
    private readonly TextWriter _originalOutput;

    public SimpleRestRequestTests()
    {
        // Store the original output
        _originalOutput = Console.Out;

        // Redirect output to a TextWriter that does nothing
        Console.SetOut(TextWriter.Null);
    }

    [Fact]
    public void TestRequestMatchWithMultipleMethods()
    {
        SimpleRestApi api = new SimpleRestApi(3000, new SimpleRestLogger(SimpleRestLogLevel.NONE));

        Task.Run(async () =>
        {
            api.Get(
                "/",
                async (req, res) =>
                {
                    res.Send("GET /");
                }
            );

            api.Post(
                "/",
                async (req, res) =>
                {
                    res.Send("POST /");
                }
            );

            api.Put(
                "/",
                async (req, res) =>
                {
                    res.Send("PUT /");
                }
            );

            api.Delete(
                "/",
                async (req, res) =>
                {
                    res.Send("DELETE /");
                }
            );

            api.Patch(
                "/",
                async (req, res) =>
                {
                    res.Send("PATCH /");
                }
            );
            await api.Start();
        });

        Task.Run(async () =>
            {
                while (!api.HasStarted)
                    ;

                // GET method
                var responseGet = await HttpHelpers.HttpCall(
                    "localhost:3000",
                    SimpleRestMethod.GET,
                    "/"
                );
                string correctMethodGet = (await responseGet.Content.ReadAsStringAsync()).Trim('"');
                Assert.Equal("GET /", correctMethodGet);

                // POST method
                var responsePost = await HttpHelpers.HttpCall(
                    "localhost:3000",
                    SimpleRestMethod.POST,
                    "/"
                );
                string correctMethodPost = (await responsePost.Content.ReadAsStringAsync()).Trim(
                    '"'
                );
                Assert.Equal("POST /", correctMethodPost);

                // PUT method
                var responsePut = await HttpHelpers.HttpCall(
                    "localhost:3000",
                    SimpleRestMethod.PUT,
                    "/"
                );
                string correctMethodPut = (await responsePut.Content.ReadAsStringAsync()).Trim('"');
                Assert.Equal("PUT /", correctMethodPut);

                // DELETE method
                var responseDelete = await HttpHelpers.HttpCall(
                    "localhost:3000",
                    SimpleRestMethod.DELETE,
                    "/"
                );
                string correctMethodDelete = (
                    await responseDelete.Content.ReadAsStringAsync()
                ).Trim('"');
                Assert.Equal("DELETE /", correctMethodDelete);

                // PATCH method
                var responsePatch = await HttpHelpers.HttpCall(
                    "localhost:3000",
                    SimpleRestMethod.PATCH,
                    "/"
                );
                string correctMethodPatch = (await responsePatch.Content.ReadAsStringAsync()).Trim(
                    '"'
                );
                Assert.Equal("PATCH /", correctMethodPatch);
            })
            .Wait();
        api.Stop();
    }

    [Fact]
    public void TestRequestMatchWithMultipleMethodsAndSingleParam()
    {
        SimpleRestApi api = new SimpleRestApi(3000, new SimpleRestLogger(SimpleRestLogLevel.NONE));

        Task.Run(async () =>
        {
            // GET method
            api.Get(
                "/{id}",
                async (req, res) =>
                {
                    res.Send($"GET /{req["id"]}");
                }
            );

            // POST method
            api.Post(
                "/{id}",
                async (req, res) =>
                {
                    res.Send($"POST /{req["id"]}");
                }
            );

            // PUT method
            api.Put(
                "/{id}",
                async (req, res) =>
                {
                    res.Send($"PUT /{req["id"]}");
                }
            );

            // PATCH method
            api.Patch(
                "/{id}",
                async (req, res) =>
                {
                    res.Send($"PATCH /{req["id"]}");
                }
            );

            // DELETE method
            api.Delete(
                "/{id}",
                async (req, res) =>
                {
                    res.Send($"DELETE /{req["id"]}");
                }
            );

            await api.Start();
        });

        Task.Run(async () =>
            {
                while (!api.HasStarted)
                    ;

                int id = 10;

                // GET method
                var responseGet = await HttpHelpers.HttpCall(
                    "localhost:3000",
                    SimpleRestMethod.GET,
                    "/" + id
                );
                string correctMethodGet = (await responseGet.Content.ReadAsStringAsync()).Trim('"');
                Assert.Equal("GET /" + id, correctMethodGet);

                // POST method
                var responsePost = await HttpHelpers.HttpCall(
                    "localhost:3000",
                    SimpleRestMethod.POST,
                    "/" + id
                );
                string correctMethodPost = (await responsePost.Content.ReadAsStringAsync()).Trim(
                    '"'
                );
                Assert.Equal("POST /" + id, correctMethodPost);

                // PUT method
                var responsePut = await HttpHelpers.HttpCall(
                    "localhost:3000",
                    SimpleRestMethod.PUT,
                    "/" + id
                );
                string correctMethodPut = (await responsePut.Content.ReadAsStringAsync()).Trim('"');
                Assert.Equal("PUT /" + id, correctMethodPut);

                // PATCH method
                var responsePatch = await HttpHelpers.HttpCall(
                    "localhost:3000",
                    SimpleRestMethod.PATCH,
                    "/" + id
                );
                string correctMethodPatch = (await responsePatch.Content.ReadAsStringAsync()).Trim(
                    '"'
                );
                Assert.Equal("PATCH /" + id, correctMethodPatch);

                // DELETE method
                var responseDelete = await HttpHelpers.HttpCall(
                    "localhost:3000",
                    SimpleRestMethod.DELETE,
                    "/" + id
                );
                string correctMethodDelete = (
                    await responseDelete.Content.ReadAsStringAsync()
                ).Trim('"');
                Assert.Equal("DELETE /" + id, correctMethodDelete);
            })
            .Wait();
        api.Stop();
    }

    [Fact]
    public void TestRequestMatchWithMulipleMethodsAndMultipleParams()
    {
        SimpleRestApi api = new SimpleRestApi(3000, new SimpleRestLogger(SimpleRestLogLevel.NONE));

        Task.Run(async () =>
        {
            // GET method
            api.Get(
                "/{id}/{name}",
                async (req, res) =>
                {
                    res.Send($"GET /{req["id"]}/{req["name"]}");
                }
            );

            // POST method
            api.Post(
                "/{id}/{name}",
                async (req, res) =>
                {
                    res.Send($"POST /{req["id"]}/{req["name"]}");
                }
            );

            // PUT method
            api.Put(
                "/{id}/{name}",
                async (req, res) =>
                {
                    res.Send($"PUT /{req["id"]}/{req["name"]}");
                }
            );

            // DELETE method
            api.Delete(
                "/{id}/{name}",
                async (req, res) =>
                {
                    res.Send($"DELETE /{req["id"]}/{req["name"]}");
                }
            );

            // PATCH method
            api.Patch(
                "/{id}/{name}",
                async (req, res) =>
                {
                    res.Send($"PATCH /{req["id"]}/{req["name"]}");
                }
            );

            await api.Start();
        });

        Task.Run(async () =>
            {
                while (!api.HasStarted)
                    ;

                int id = 10;
                string name = "example"; // You can set this to any string you wish to test

                // GET method
                var responseGet = await HttpHelpers.HttpCall(
                    "localhost:3000",
                    SimpleRestMethod.GET,
                    $"/{id}/{name}"
                );
                string correctMethodGet = (await responseGet.Content.ReadAsStringAsync()).Trim('"');
                Assert.Equal($"GET /{id}/{name}", correctMethodGet);

                // POST method
                var responsePost = await HttpHelpers.HttpCall(
                    "localhost:3000",
                    SimpleRestMethod.POST,
                    $"/{id}/{name}"
                );
                string correctMethodPost = (await responsePost.Content.ReadAsStringAsync()).Trim(
                    '"'
                );
                Assert.Equal($"POST /{id}/{name}", correctMethodPost);

                // PUT method
                var responsePut = await HttpHelpers.HttpCall(
                    "localhost:3000",
                    SimpleRestMethod.PUT,
                    $"/{id}/{name}"
                );
                string correctMethodPut = (await responsePut.Content.ReadAsStringAsync()).Trim('"');
                Assert.Equal($"PUT /{id}/{name}", correctMethodPut);

                // PATCH method
                var responsePatch = await HttpHelpers.HttpCall(
                    "localhost:3000",
                    SimpleRestMethod.PATCH,
                    $"/{id}/{name}"
                );
                string correctMethodPatch = (await responsePatch.Content.ReadAsStringAsync()).Trim(
                    '"'
                );
                Assert.Equal($"PATCH /{id}/{name}", correctMethodPatch);

                // DELETE method
                var responseDelete = await HttpHelpers.HttpCall(
                    "localhost:3000",
                    SimpleRestMethod.DELETE,
                    $"/{id}/{name}"
                );
                string correctMethodDelete = (
                    await responseDelete.Content.ReadAsStringAsync()
                ).Trim('"');
                Assert.Equal($"DELETE /{id}/{name}", correctMethodDelete);
            })
            .Wait();
        api.Stop();
    }

    public void Dispose()
    {
        // Restore the original output
        Console.SetOut(_originalOutput);

    }
}
