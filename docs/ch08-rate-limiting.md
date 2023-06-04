# Rate limiting using ASP.NET Core middleware

In this online-only section, you will be introduced to the built-in ASP.NET Core rate limiting middleware. During its development in .NET 7 previews it was distributed as a separate NuGet package but with the release of .NET 7 it was added to the main ASP.NET Core assemblies.

> To complete this section, you must have created the `Northwind.MinimalAot.Service` project as described in the **Building a native AOT project** section of the book.

Let's configure fixed window rate limiting (set a maximum number of requests allowed within a time window) in the minimal AOT web service project:

1.	In the `Northwind.MinimalAot.Service` project, in `WebApplication.Extensions.cs`, import `System` and ASP.NET Core namespaces for working with rate limiting, as shown in the following code:
```cs
using Microsoft.AspNetCore.RateLimiting; // To use RateLimiterOptions.
using System.Threading.RateLimiting; // To use QueueProcessingOrder.
```

2.  in `WebApplication.Extensions.cs`, in the class, define a `string` field for the policy name, and then add an extension method for `WebApplication` to define a policy named `fixed5per10seconds` to control rate limiting, as shown in the following code:
```cs
private static string _policyName = "fixed5per10seconds";

public static void UseCustomRateLimiting(this WebApplication app)
{
  // Configure ASP.NET Core rate limiting middleware.
  RateLimiterOptions rateLimiterOptions = new();

  rateLimiterOptions.AddFixedWindowLimiter(
    policyName: _policyName, options =>
    {
      options.PermitLimit = 5;
      options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
      options.QueueLimit = 2;
      options.Window = TimeSpan.FromSeconds(10);
    });

  app.UseRateLimiter(rateLimiterOptions);
}
```

> **More Information**: You can learn more about the different types of built-in rate limiter at the following link: https://docs.microsoft.com/en-us/aspnet/core/performance/rate-limit

3.	In `WebApplication.Extensions.cs`, in the `MapGets` extension method, in the statement that maps the `GET` endpoint for products, require that it use the rate limiting policy, as shown highlighted in the following code:
```cs
app.MapGet("api/products", GetProducts)
  .RequireRateLimiting(policyName: _policyName);
```

4.	At the bottom of `Program.cs`, before mapping the `GET` requests, call the extension method:
```cs
app.UseCustomRateLimiting();
```

5.  In the `Northwind.WebApi.Client.Console` project, change the scheme, port number, and path of the web service to talk to the AOT web service, as shown in the following code:
```cs
string scheme = "http"; // Web API: https, AOT: http.
string port = "5083"; // Web API: 5081, AOT: 5083.
string path = "products"; // Web API: api/products, AOT: products.

client.BaseAddress = new($"{scheme}://localhost:{port}");
```

6.  Start the `Northwind.MinimalAot.Service` web service project using the `http` profile without debugging.
7.	Start the `Northwind.WebApi.Client.Console` project without debugging.
8.	In the console app, press *Enter* to generate a GUID-based client ID.
9.	Note the console app will now make up to five requests in each 10 second window but then have to pause until that window has passed.
10. Close the web browser and shut down the web service. 
