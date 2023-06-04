**Rate limiting using ASP.NET Core middleware**

In this online-only section, you will be introduced to the built-in ASP.NET Core rate limiting middleware. During its development in previews for .NET 7 it was distributed as a separate NuGet package but with the release of .NET 7 it was added to the main ASP.NET Core assemblies.

# Implementing ASP.NET Core rate limiting

Let's add rate limiting to the AOT project:

1.	In the `Northwind.MinimalAot.Service` project, in `WebApplication.Extensions.cs`, add a method to define a policy named `fixed5per10seconds` to control rate limiting, as shown in the following code:
```cs
public static void UseCustomRateLimiting(this WebApplication app)
{
  // Configure ASP.NET Core rate limiting middleware.
  RateLimiterOptions rateLimiterOptions = new();

  rateLimiterOptions.AddFixedWindowLimiter(
    policyName: "fixed5per10Seconds", options =>
    {
      options.PermitLimit = 5;
      options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
      options.QueueLimit = 2;
      options.Window = TimeSpan.FromSeconds(10);
    });

  app.UseRateLimiter(rateLimiterOptions);
}
```

2.	In `WebApplication.Extensions.cs`, in the `MapGets` method, in the statement that maps the `GET` endpoint for products, require that it use a rate limiting policy named `fixed5per10seconds`, as shown highlighted in the following code:
```cs
app.MapGet("api/products", GetProducts)
  .RequireRateLimiting("fixed5per10seconds");
```

3.	At the bottom of `Program.cs`, before running the host, call the extension method:
```cs
app.UseCustomRateLimiting()
```

4.  In the `Northwind.WebApi.Client.Console` project, change the address of the web service, as shown highlighted in the following code:
```cs

```

5.  Start the web service.
5.	Start the `Northwind.WebApi.Client.Console` project without debugging.
6.	In the console app, press *Enter* to generate a GUID-based client ID.
7.	Note the console app will now make up to five requests in each 10 second window but then have to pause until that window has passed, as shown in the following output:
```
Enter a client name:
X-Client-Id will be: console-client-16b15d15-f3f3-4e28-a6b7-692c76673e0c
01:40:19: Chai, Chang, Aniseed Syrup, Chef Anton's Cajun Seasoning, Grandma's Boysenberry Spread, Uncle Bob's Organic Dried Pears, Northwoods Cranberry Sauce, Ikura, Queso Cabrales, Queso Manchego La Pastora,
01:40:20: Chai, Chang, Aniseed Syrup, Chef Anton's Cajun Seasoning, Grandma's Boysenberry Spread, Uncle Bob's Organic Dried Pears, Northwoods Cranberry Sauce, Ikura, Queso Cabrales, Queso Manchego La Pastora,
01:40:21: Chai, Chang, Aniseed Syrup, Chef Anton's Cajun Seasoning, Grandma's Boysenberry Spread, Uncle Bob's Organic Dried Pears, Northwoods Cranberry Sauce, Ikura, Queso Cabrales, Queso Manchego La Pastora,
01:40:22: Chai, Chang, Aniseed Syrup, Chef Anton's Cajun Seasoning, Grandma's Boysenberry Spread, Uncle Bob's Organic Dried Pears, Northwoods Cranberry Sauce, Ikura, Queso Cabrales, Queso Manchego La Pastora,
01:40:23: Chai, Chang, Aniseed Syrup, Chef Anton's Cajun Seasoning, Grandma's Boysenberry Spread, Uncle Bob's Organic Dried Pears, Northwoods Cranberry Sauce, Ikura, Queso Cabrales, Queso Manchego La Pastora,
01:40:28: Chai, Chang, Aniseed Syrup, Chef Anton's Cajun Seasoning, Grandma's Boysenberry Spread, Uncle Bob's Organic Dried Pears, Northwoods Cranberry Sauce, Ikura, Queso Cabrales, Queso Manchego La Pastora,
01:40:29: Chai, Chang, Aniseed Syrup, Chef Anton's Cajun Seasoning, Grandma's Boysenberry Spread, Uncle Bob's Organic Dried Pears, Northwoods Cranberry Sauce, Ikura, Queso Cabrales, Queso Manchego La Pastora,
01:40:30: Chai, Chang, Aniseed Syrup, Chef Anton's Cajun Seasoning, Grandma's Boysenberry Spread, Uncle Bob's Organic Dried Pears, Northwoods Cranberry Sauce, Ikura, Queso Cabrales, Queso Manchego La Pastora,
01:40:31: Chai, Chang, Aniseed Syrup, Chef Anton's Cajun Seasoning, Grandma's Boysenberry Spread, Uncle Bob's Organic Dried Pears, Northwoods Cranberry Sauce, Ikura, Queso Cabrales, Queso Manchego La Pastora,
01:40:32: Chai, Chang, Aniseed Syrup, Chef Anton's Cajun Seasoning, Grandma's Boysenberry Spread, Uncle Bob's Organic Dried Pears, Northwoods Cranberry Sauce, Ikura, Queso Cabrales, Queso Manchego La Pastora,
01:40:38: Chai, Chang, Aniseed Syrup, Chef Anton's Cajun Seasoning, Grandma's Boysenberry Spread, Uncle Bob's Organic Dried Pears, Northwoods Cranberry Sauce, Ikura, Queso Cabrales, Queso Manchego La Pastora,
You can learn more about the different types of built-in rate limiter at the following link: https://docs.microsoft.com/en-us/aspnet/core/performance/rate-limit
```

1. Close the web browser and shut down the web service. 
 