using System.ComponentModel.DataAnnotations.Schema; // [NotMapped]

namespace Northwind.EntityModels;

public partial class Employee : IHasLastRefreshed
{
  [NotMapped]
  public DateTimeOffset LastRefreshed { get; set; }
}
