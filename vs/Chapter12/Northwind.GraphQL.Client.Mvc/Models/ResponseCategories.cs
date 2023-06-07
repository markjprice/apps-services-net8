using Northwind.EntityModels; // To use Category.

namespace Northwind.GraphQL.Client.Mvc.Models;

public class ResponseCategories
{
  public class DataCategories
  {
    public Category[]? Categories { get; set; }
  }

  public DataCategories? Data { get; set; }
}
