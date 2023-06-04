using System.Text.Json.Serialization; // To use JsonSerializerContext.
using Northwind.Models; // To use Product.

[JsonSerializable(typeof(Product))]
[JsonSerializable(typeof(List<Product>))]
internal partial class NorthwindJsonSerializerContext
  : JsonSerializerContext { }
