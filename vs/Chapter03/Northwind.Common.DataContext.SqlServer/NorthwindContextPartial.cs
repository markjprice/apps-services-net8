using Microsoft.Data.SqlClient; // SqlConnectionStringBuilder
using Microsoft.EntityFrameworkCore; // DbContext

namespace Northwind.EntityModels;

public partial class NorthwindContext
{
  private static readonly SetLastRefreshedInterceptor
    setLastRefreshedInterceptor = new();

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    if (!optionsBuilder.IsConfigured)
    {
      SqlConnectionStringBuilder builder = new();

      builder.DataSource = ".";
      builder.InitialCatalog = "Northwind";
      builder.IntegratedSecurity = true;
      builder.TrustServerCertificate = true;
      builder.MultipleActiveResultSets = true;

      // Because we want to fail fast. Default is 15 seconds.
      builder.ConnectTimeout = 3;

      optionsBuilder.UseSqlServer(builder.ConnectionString);
    }

    optionsBuilder.AddInterceptors(setLastRefreshedInterceptor);
  }
}
