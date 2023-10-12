using System.ComponentModel.DataAnnotations; // To use [Required].

namespace Northwind.Models;

public abstract class Person
{
  public int Id { get; set; }

  [Required]
  [StringLength(40)]
  public string? Name { get; set; }
}
