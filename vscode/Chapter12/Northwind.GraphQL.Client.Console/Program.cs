using Microsoft.Extensions.DependencyInjection; // To use ServiceCollection.
using Northwind.GraphQL.Client.Console; // To use INorthwindClient.
using StrawberryShake; // To use EnsureNoErrors extension method.

ServiceCollection serviceCollection = new();

serviceCollection
  .AddNorthwindClient() // Strawberry Shake extension method.
  .ConfigureHttpClient(client =>
    client.BaseAddress = new Uri("https://localhost:5121/graphql"));

IServiceProvider services = serviceCollection.BuildServiceProvider();

INorthwindClient client = services.GetRequiredService<INorthwindClient>();

var result = await client.SeafoodProducts.ExecuteAsync();
result.EnsureNoErrors();

if (result.Data is null)
{
  WriteLine("No data!");
  return;
}

foreach (var product in result.Data.ProductsInCategory)
{
  WriteLine("{0}: {1}",
    product.ProductId, product.ProductName);
}
