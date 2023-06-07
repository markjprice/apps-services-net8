namespace Northwind.GraphQL.Client.Mvc.Models;

public class ResponseErrors
{
  public Error[]? Errors { get; set; }
}

public class Error
{
  public string Message { get; set; } = null!;
  public Location[] Locations { get; set; } = null!;
  public string[] Path { get; set; } = null!;
}

public class Location
{
  public int Line { get; set; }
  public int Column { get; set; }
}
