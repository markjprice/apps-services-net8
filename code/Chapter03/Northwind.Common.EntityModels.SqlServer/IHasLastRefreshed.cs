namespace Northwind.EntityModels;

public interface IHasLastRefreshed
{
  DateTimeOffset LastRefreshed { get; set; }
}
