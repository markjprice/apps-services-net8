using AutoMapper; // To use MapperConfiguration.
using MappingObjects.Mappers; // To use CartToSummaryMapper.
using Northwind.EntityModels;
using Northwind.ViewModels;

namespace MappingObjects.Tests;

public class TestAutoMapperConfig
{
  [Fact]
  public void TestSummaryMapping()
  {
    MapperConfiguration config = CartToSummaryMapper.GetMapperConfiguration();

    config.AssertConfigurationIsValid();
  }

  [Fact]
  public void TestSummaryMappingValues()
  {
    // Create an object model from "entity" model types that
    // might have come from a data store like SQL Server.

    Cart cart = new(
      Customer: new(
        FirstName: "John",
        LastName: "Smith"
      ),
      Items: new()
      {
        new(ProductName: "Apples", UnitPrice: 0.49M, Quantity: 10),
        new(ProductName: "Bananas", UnitPrice: 0.99M, Quantity: 4)
      }
    );

    // Get the mapper configuration for converting a Cart to a Summary.

    MapperConfiguration config = CartToSummaryMapper.GetMapperConfiguration();

    // Create a mapper using the configuration.

    IMapper mapper = config.CreateMapper();

    // Perform the mapping.

    Summary summary = mapper.Map<Cart, Summary>(cart);

    // Assert the results.

    Assert.True(summary.FullName == "John Smith");
    Assert.True(summary.Total == 8.86M);
  }
}