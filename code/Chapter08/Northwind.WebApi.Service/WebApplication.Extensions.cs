using AspNetCoreRateLimit; // To use IClientPolicyStore and so on.
using Microsoft.AspNetCore.Http.HttpResults; // To use Results.
using Microsoft.AspNetCore.Mvc; // To use [FromServices] and so on.
using Northwind.EntityModels; // To use NorthwindContext, Product.
using System.Security.Claims; // To use ClaimsPrincipal.

public static class WebApplicationExtensions
{
  public static void MapGets(this WebApplication app,
    int pageSize = 10)
  {
    app.MapGet("/", () => "Hello World!")
      .ExcludeFromDescription();

    app.MapGet("/secret", (ClaimsPrincipal user) =>
      string.Format("Welcome, {0}. The secret ingredient is love.",
        user.Identity?.Name ?? "secure user"))
      .RequireAuthorization();

    app.MapGet("api/products", (
      [FromServices] NorthwindContext db,
      [FromQuery] int? page) =>
      db.Products
        .Where(p => (p.UnitsInStock > 0) && (!p.Discontinued))
        .OrderBy(product => product.ProductId)
        .Skip(((page ?? 1) - 1) * pageSize)
        .Take(pageSize)
      )
      .WithName("GetProducts")
      .WithOpenApi(operation =>
      {
        operation.Description =
          "Get products with UnitsInStock > 0 and Discontinued = false.";
        operation.Summary = "Get in-stock products that are not discontinued.";
        return operation;
      })
      .Produces<Product[]>(StatusCodes.Status200OK);

    app.MapGet("api/products/outofstock",
      ([FromServices] NorthwindContext db) => db.Products
        .Where(p => (p.UnitsInStock == 0) && (!p.Discontinued))
      )
      .WithName("GetProductsOutOfStock")
      .WithOpenApi()
      .Produces<Product[]>(StatusCodes.Status200OK);

    app.MapGet("api/products/discontinued",
      ([FromServices] NorthwindContext db) =>
        db.Products.Where(product => product.Discontinued)
      )
      .WithName("GetProductsDiscontinued")
      .WithOpenApi()
      .Produces<Product[]>(StatusCodes.Status200OK);

    app.MapGet("api/products/{id:int}",
      async Task<Results<Ok<Product>, NotFound>> (
      [FromServices] NorthwindContext db,
      [FromRoute] int id) =>
        await db.Products.FindAsync(id) is Product product ?
          TypedResults.Ok(product) : TypedResults.NotFound())
      .WithName("GetProductById")
      .WithOpenApi()
      .Produces<Product>(StatusCodes.Status200OK)
      .Produces(StatusCodes.Status404NotFound);

    app.MapGet("api/products/{name}", (
      [FromServices] NorthwindContext db,
      [FromRoute] string name) =>
        db.Products.Where(p => p.ProductName.Contains(name)))
      .WithName("GetProductsByName")
      .WithOpenApi()
      .Produces<Product[]>(StatusCodes.Status200OK)
      .RequireCors(policyName: "Northwind.Mvc.Policy");
  }

  public static void MapPosts(this WebApplication app)
  {
    app.MapPost("api/products", async ([FromBody] Product product,
      [FromServices] NorthwindContext db) =>
      {
        db.Products.Add(product);
        await db.SaveChangesAsync();
        return Results.Created($"api/products/{product.ProductId}", product);
      }).WithOpenApi()
      .Produces<Product>(StatusCodes.Status201Created);
  }

  public static void MapPuts(this WebApplication app)
  {
    app.MapPut("api/products/{id:int}", async (
      [FromRoute] int id,
      [FromBody] Product product,
      [FromServices] NorthwindContext db) =>
    {
      Product? foundProduct = await db.Products.FindAsync(id);

      if (foundProduct is null) return Results.NotFound();

      foundProduct.ProductName = product.ProductName;
      foundProduct.CategoryId = product.CategoryId;
      foundProduct.SupplierId = product.SupplierId;
      foundProduct.QuantityPerUnit = product.QuantityPerUnit;
      foundProduct.UnitsInStock = product.UnitsInStock;
      foundProduct.UnitsOnOrder = product.UnitsOnOrder;
      foundProduct.ReorderLevel = product.ReorderLevel;
      foundProduct.UnitPrice = product.UnitPrice;
      foundProduct.Discontinued = product.Discontinued;

      await db.SaveChangesAsync();

      return Results.NoContent();
    }).WithOpenApi()
      .Produces(StatusCodes.Status404NotFound)
      .Produces(StatusCodes.Status204NoContent);
  }

  public static void MapDeletes(this WebApplication app)
  {
    app.MapDelete("api/products/{id:int}", async (
      [FromRoute] int id,
      [FromServices] NorthwindContext db) =>
    {
      if (await db.Products.FindAsync(id) is Product product)
      {
        db.Products.Remove(product);
        await db.SaveChangesAsync();
        return Results.NoContent();
      }
      return Results.NotFound();
    }).WithOpenApi()
      .Produces(StatusCodes.Status404NotFound)
      .Produces(StatusCodes.Status204NoContent);
  }

  public static async Task UseCustomClientRateLimiting(this WebApplication app)
  {
    using (IServiceScope scope = app.Services.CreateScope())
    {
      IClientPolicyStore clientPolicyStore = scope.ServiceProvider
        .GetRequiredService<IClientPolicyStore>();

      await clientPolicyStore.SeedAsync();
    }

    app.UseClientRateLimiting();
  }
}
