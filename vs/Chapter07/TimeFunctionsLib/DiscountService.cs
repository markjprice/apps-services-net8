namespace Northwind.Services;

public class DiscountService
{
  private TimeProvider _timeProvider;

  public DiscountService(TimeProvider timeProvider)
  {
    _timeProvider = timeProvider;
  }

  public decimal GetDiscount()
  {
    // This has a dependency on the current time provided by the system.
    // var now = DateTime.UtcNow;

    var now = _timeProvider.GetUtcNow();

    // This has a dependency on the current time provided by the system.
    return now.DayOfWeek switch
    {
      DayOfWeek.Saturday or DayOfWeek.Sunday => 0.2M,
      _ => 0M
    };
  }
}