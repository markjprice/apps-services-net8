namespace Northwind.EntityModels;

// This record will only have a constructor with the parameters below.
// Objects will be immutable after instantiation using this constructor.
// It will not have a default parameterless constructor.
public record class Customer(
  string FirstName,
  string LastName
);
