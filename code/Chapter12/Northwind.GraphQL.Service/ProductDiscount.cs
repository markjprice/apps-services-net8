namespace Northwind.GraphQL.Service;

public class ProductDiscount
{
  public int? ProductId { get; set; }
  public decimal? OriginalUnitPrice { get; set; }
  public decimal? NewUnitPrice { get; set; }
}
