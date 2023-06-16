using Grpc.Core; // To use ServerCallContext.
using Microsoft.Data.SqlClient; // To use SqlConnection and so on.
using System.Data; // To use CommandType.

namespace Northwind.Grpc.Service.Services;

public class ProductService : Product.ProductBase
{
  private readonly ILogger<ProductService> _logger;

  public ProductService(ILogger<ProductService> logger)
  {
    _logger = logger;
  }

  private async Task<SqlCommand> GetCommand()
  {
    SqlConnectionStringBuilder builder = new();

    builder.InitialCatalog = "Northwind";
    builder.MultipleActiveResultSets = true;
    builder.Encrypt = true;
    builder.TrustServerCertificate = true;
    builder.ConnectTimeout = 10; // Default is 30 seconds.
    builder.DataSource = "."; // To use local SQL Server.
    builder.IntegratedSecurity = true;

    /*
    // To use SQL Server Authentication:
    builder.UserID = userId;
    builder.Password = password;
    builder.PersistSecurityInfo = false;
    */

    SqlConnection connection = new(builder.ConnectionString);

    await connection.OpenAsync();

    SqlCommand cmd = connection.CreateCommand();
    cmd.CommandType = CommandType.Text;
    return cmd;
  }

  private ProductReply ReaderToProduct(SqlDataReader r)
  {
    ProductReply p = new();

    p.ProductId = r.GetInt32("ProductId");
    p.ProductName = r.GetString("ProductName");
    p.SupplierId = r.GetInt32("SupplierId");
    p.CategoryId = r.GetInt32("CategoryId");
    p.QuantityPerUnit = r.GetString("QuantityPerUnit");

    // Uses our custom conversion from decimal to DecimalValue.
    p.UnitPrice = r.GetDecimal("UnitPrice");

    p.UnitsInStock = r.GetInt16("UnitsInStock");
    p.UnitsOnOrder = r.GetInt16("UnitsOnOrder");
    p.ReorderLevel = r.GetInt16("ReorderLevel");
    p.Discontinued = r.GetBoolean("Discontinued");

    return p;
  }

  public override async Task<ProductReply?> GetProduct(
    ProductRequest request, ServerCallContext context)
  {
    SqlCommand cmd = await GetCommand();
    cmd.CommandText = "SELECT * FROM Products WHERE ProductId = @id";
    cmd.Parameters.AddWithValue("id", request.ProductId);

    SqlDataReader r = await cmd.ExecuteReaderAsync(
      CommandBehavior.SingleRow);

    ProductReply? product = null;

    // Read the expected single row.
    if (await r.ReadAsync())
    {
      product = ReaderToProduct(r);
    }

    await r.CloseAsync();

    return product;
  }

  public override async Task<ProductsReply?> GetProducts(
    ProductsRequest request, ServerCallContext context)
  {
    SqlCommand cmd = await GetCommand();
    cmd.CommandText = "SELECT * FROM Products";

    SqlDataReader r = await cmd.ExecuteReaderAsync(
      CommandBehavior.SingleResult);

    ProductsReply? products = new();

    while (await r.ReadAsync())
    {
      products.Products.Add(ReaderToProduct(r));
    }

    await r.CloseAsync();

    return products;
  }

  public override async Task<ProductsReply?> GetProductsMinimumPrice(
    ProductsMinimumPriceRequest request, ServerCallContext context)
  {
    SqlCommand cmd = await GetCommand();
    cmd.CommandText = "SELECT * FROM Products WHERE UnitPrice >= @price";

    // We must cast DecimalValue to a decimal value because SqlClient
    // does not understand what to do with a DecimalValue.
    cmd.Parameters.AddWithValue("price", (decimal)request.MinimumPrice);

    SqlDataReader r = await cmd.ExecuteReaderAsync(
      CommandBehavior.SingleResult);

    ProductsReply? products = new();

    while (await r.ReadAsync())
    {
      products.Products.Add(ReaderToProduct(r));
    }

    await r.CloseAsync();

    return products;
  }
}
