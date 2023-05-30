namespace Northwind.EntityModels;

public record class Cart(
  Customer Customer,
  List<LineItem> Items
);
