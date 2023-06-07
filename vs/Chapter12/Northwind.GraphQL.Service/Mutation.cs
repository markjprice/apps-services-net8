using HotChocolate.Subscriptions;
using Northwind.EntityModels; // To use Product.

namespace Northwind.GraphQL.Service;

// Inputs are readonly so we will use a record.
public record AddProductInput(
  string ProductName,
  int? SupplierId,
  int? CategoryId,
  string QuantityPerUnit,
  decimal? UnitPrice,
  short? UnitsInStock,
  short? UnitsOnOrder,
  short? ReorderLevel,
  bool Discontinued);

public record UpdateProductPriceInput(
  int? ProductId,
  decimal? UnitPrice);

public record UpdateProductUnitsInput(
  int? ProductId,
  short? UnitsInStock,
  short? UnitsOnOrder,
  short? ReorderLevel);

public record DeleteProductInput(
  int? ProductId);

public class AddProductPayload
{
  public AddProductPayload(Product product)
  {
    Product = product;
  }

  public Product Product { get; }
}

public class UpdateProductPayload
{
  public UpdateProductPayload(Product? product, bool updated)
  {
    Product = product;
    Success = updated;
  }

  public Product? Product { get; }
  public bool Success { get; }
}

public class DeleteProductPayload
{
  public DeleteProductPayload(bool deleted)
  {
    Success = deleted;
  }

  public bool Success { get; }
}

public class Mutation
{
  public async Task<AddProductPayload> AddProductAsync(
    AddProductInput input, NorthwindContext db)
  {
    // This could be a good place to use a tool like AutoMapper,
    // but we will do the mapping between two objects manually.

    Product product = new()
    {
      ProductName = input.ProductName,
      SupplierId = input.SupplierId,
      CategoryId = input.CategoryId,
      QuantityPerUnit = input.QuantityPerUnit,
      UnitPrice = input.UnitPrice,
      UnitsInStock = input.UnitsInStock,
      UnitsOnOrder = input.UnitsOnOrder,
      ReorderLevel = input.ReorderLevel,
      Discontinued = input.Discontinued
    };

    db.Products.Add(product);

    int affectedRows = await db.SaveChangesAsync();

    // We could use affectedRows to return an error
    // or some other action if it is 0.

    return new AddProductPayload(product);
  }

  public async Task<UpdateProductPayload> UpdateProductPriceAsync(
    UpdateProductPriceInput input, NorthwindContext db,
    ITopicEventSender eventSender)
  {
    Product? product = await db.Products.FindAsync(input.ProductId);

    int affectedRows = 0;

    if (product is not null)
    {
      if (input.UnitPrice < product.UnitPrice)
      {
        // If the product has been discounted,
        // send a message to subscribers.
        ProductDiscount productDiscount = new()
        {
          ProductId = input.ProductId,
          OriginalUnitPrice = product.UnitPrice,
          NewUnitPrice = input.UnitPrice
        };

        await eventSender.SendAsync(topicName:
          nameof(Subscription.OnProductDiscounted),
          message: productDiscount);
      }

      product.UnitPrice = input.UnitPrice;

      affectedRows = await db.SaveChangesAsync();
    }

    return new UpdateProductPayload(product,
      updated: affectedRows == 1);
  }

  public async Task<UpdateProductPayload> UpdateProductUnitsAsync(
    UpdateProductUnitsInput input, NorthwindContext db)
  {
    Product? product = await db.Products.FindAsync(input.ProductId);

    int affectedRows = 0;

    if (product is not null)
    {
      product.UnitsInStock = input.UnitsInStock;
      product.UnitsOnOrder = input.UnitsOnOrder;
      product.ReorderLevel = input.ReorderLevel;

      affectedRows = await db.SaveChangesAsync();
    }

    return new UpdateProductPayload(product,
      updated: affectedRows == 1);
  }

  public async Task<DeleteProductPayload> DeleteProductAsync(
    DeleteProductInput input, NorthwindContext db)
  {
    Product? product = await db.Products.FindAsync(input.ProductId);

    int affectedRows = 0;

    if (product is not null)
    {
      db.Products.Remove(product);

      affectedRows = await db.SaveChangesAsync();
    }

    return new DeleteProductPayload(
      deleted: affectedRows == 1);
  }
}
