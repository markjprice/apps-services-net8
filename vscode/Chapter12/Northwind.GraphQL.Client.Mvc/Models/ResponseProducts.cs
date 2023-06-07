using Northwind.EntityModels; // To use Product.

namespace Northwind.GraphQL.Client.Mvc.Models;

public class ResponseProducts
{
  public class DataProducts
  {
    public Product[]? ProductsInCategory { get; set; }
  }

  public DataProducts? Data { get; set; }
}
