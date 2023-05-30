namespace FluentValidation.Models;

public class Order
{
  public long OrderId { get; set; }
  public string? CustomerName { get; set; }
  public string? CustomerEmail { get; set; }
  public CustomerLevel CustomerLevel { get; set; }
  public decimal Total { get; set; }
  public DateTime OrderDate { get; set; }
  public DateTime ShipDate { get; set; }
}
