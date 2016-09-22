# dotnet-core-webapi-seed

Seed project for a dot net core webapi 

## Projects 

`StarterProject.Web.Api` -> dev code

`StarterProject.Web.Api.UnitTests` -> unit tests

`StarterProject.Web.Api.IntegrationTests` - integration tests with a self hosted endpoint

## Api

The default dotnet core template `ValuesController` is included. This will always return 2 hardcoded values, since we are just trying to setup a dummy project to start with :smile:

```c#
[Route("api/[controller]")]
public class ValuesController : Controller
{
    // GET api/values
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }
}
```

## Unit tests

The unit tests project is meant for testing our individual units, and ensuring that they work in isolation. For instance, we would want to test our controllers without having to go through the whole WebAPI pipeline. Provided our dependencies are correctly structured, that should be easy to achieve.

One unit test for the controller is included, using the [AAA pattern](https://www.typemock.com/unit-test-patterns-for-net/#aaa).  

```c#
[TestMethod]
public void ValuesController_Constructor()
{
    // Act
    new ValuesController();
}
```

## Integration tests

Following the pattern described in the official ASP.NET docs on [integration testing](https://docs.asp.net/en/latest/testing/integration-testing.html), after we have gained confidence in our code with unit tests and that our individual building blocks work in isolation, we want to test the integration points and make sure they also work together. 

For this project we have setup a self hosted endpoint, meaning the hosting library we use will spin up an endpoint for us which we can hit in order to validate our api by going through the stack. 

This part will setup the endpoint for us using the web host builder. 

```c#
[ClassInitialize]
public static void ClassInit(TestContext testContext)
{
    // Arrange
    server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
    client = server.CreateClient();
}
```

And then we can write an integration test to make sure we can hit the api values endpoint and get a 200 back. Pretty neat.

```c#
[TestMethod]
public async Task API_Values()
{
    // Act
    var response = await client.GetAsync("/api/values");
    response.EnsureSuccessStatusCode();

    // Assert
    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
}
```
