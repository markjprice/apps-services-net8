namespace Northwind.GraphQL.Service;

public class Subscription
{
  [Subscribe]
  [Topic]
  public ProductDiscount OnProductDiscounted(
    [EventMessage] ProductDiscount productDiscount)
      => productDiscount;
}
