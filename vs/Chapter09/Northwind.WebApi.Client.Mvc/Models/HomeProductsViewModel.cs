using Northwind.EntityModels; // To use Product.

namespace Northwind.WebApi.Client.Mvc.Models;

public class HomeProductsViewModel
{
  public string? NameContains { get; set; }
  public Uri? BaseAddress { get; set; }
  public IEnumerable<Product>? Products { get; set; }
  public string? ErrorMessage { get; set; }
}
