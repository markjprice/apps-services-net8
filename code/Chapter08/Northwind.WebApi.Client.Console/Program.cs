using Northwind.EntityModels; // To use Product.
using System.Net.Http.Json; // To use ReadFromJsonAsync<T> method.

Write("Enter a client name or press Enter: ");
string? clientName = ReadLine();

if (string.IsNullOrEmpty(clientName))
{
  clientName = $"console-client-{Guid.NewGuid()}";
}

WriteLine($"X-Client-Id will be: {clientName}");

HttpClient client = new();

string scheme = "http"; // Web API: https, AOT: http.
string port = "5083"; // Web API: 5081, AOT: 5083.
string path = "products"; // Web API: api/products, AOT: products.

client.BaseAddress = new($"{scheme}://localhost:{port}");

client.DefaultRequestHeaders.Accept.Add(new("application/json"));

// Specify the rate limiting client id for this console app.
client.DefaultRequestHeaders.Add("X-Client-Id", clientName);

while (true)
{
  WriteInColor(string.Format("{0:hh:mm:ss}: ", 
    DateTime.UtcNow), ConsoleColor.DarkGreen);

  int waitFor = 1; // Second.

  try
  {
    HttpResponseMessage response = await client.GetAsync(path);

    if (response.IsSuccessStatusCode)
    {
      Product[]? products =
        await response.Content.ReadFromJsonAsync<Product[]>();

      if (products != null)
      {
        foreach (Product product in products)
        {
          Write(product.ProductName);
          Write(", ");
        }
        WriteLine();
      }
    }
    else
    {
      string retryAfter = response.Headers
        .GetValues("Retry-After").ToArray()[0];

      if (int.TryParse(retryAfter, out waitFor))
      {
        retryAfter = string.Format(
          "I will retry after {0} seconds.", waitFor);
      }

      WriteInColor(string.Format("{0}: {1} {2}", (int)response.StatusCode,
        await response.Content.ReadAsStringAsync(), retryAfter),
        ConsoleColor.DarkRed);

      WriteLine();
    }
  }
  catch (Exception ex)
  {
    WriteLine(ex.Message);
  }

  await Task.Delay(TimeSpan.FromSeconds(waitFor));
}
