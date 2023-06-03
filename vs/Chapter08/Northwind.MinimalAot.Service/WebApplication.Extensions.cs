using Microsoft.Data.SqlClient; // To use SqlConnection and so on.
using Northwind.Models; // To use Product.
using System.Data; // To use CommandType.

public static class WebApplicationExtensions
{
  public static void MapGets(this WebApplication app)
  {
    // app.MapGet(pattern, handler);

    app.MapGet("/", () => "Hello from a native AOT minimal API web service.");

    app.MapGet("/products", GetProducts);

    app.MapGet("/products/{minimumUnitPrice:decimal?}", GetProducts);
  }

  private static List<Product> GetProducts(decimal? minimumUnitPrice = null)
  {
    SqlConnectionStringBuilder builder = new();

    builder.InitialCatalog = "Northwind";
    builder.MultipleActiveResultSets = true;
    builder.Encrypt = true;
    builder.TrustServerCertificate = true;
    builder.ConnectTimeout = 10; // Default is 30 seconds.
    builder.DataSource = "."; // Local SQL Server
    builder.IntegratedSecurity = true;

    /*
    // To use SQL Server Authentication:
    builder.UserID = userId;
    builder.Password = password;
    builder.PersistSecurityInfo = false;
    */

    SqlConnection connection = new(builder.ConnectionString);

    connection.Open();

    SqlCommand cmd = connection.CreateCommand();

    cmd.CommandType = CommandType.Text;
    cmd.CommandText =
      "SELECT ProductId, ProductName, UnitPrice FROM Products";

    if (minimumUnitPrice.HasValue)
    {
      cmd.CommandText += " WHERE UnitPrice >= @minimumUnitPrice";

      cmd.Parameters.AddWithValue("minimumUnitPrice", minimumUnitPrice);
    }

    SqlDataReader r = cmd.ExecuteReader();

    List<Product> products = new();

    while (r.Read())
    {
      Product p = new()
      {
        ProductId = r.GetInt32("ProductId"),
        ProductName = r.GetString("ProductName"),
        UnitPrice = r.GetDecimal("UnitPrice")
      };
      products.Add(p);
    }

    r.Close();

    return products;
  }
}
