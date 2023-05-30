namespace Northwind.EntityModels;

public record class LineItem(
  string ProductName,
  decimal UnitPrice,
  int Quantity
);
