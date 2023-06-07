using Northwind.EntityModels; // To use Product.
using System.Net; // To use HttpStatusCode.

namespace Northwind.GraphQL.Client.Mvc.Models;

public class IndexViewModel
{
  public HttpStatusCode Code { get; set; }
  public string? RawResponseBody { get; set; }
  public Product[]? Products { get; set; }
  public Category[]? Categories { get; set; }
  public Error[]? Errors { get; set; }
}
