using Northwind.Services; // To use DiscountService.
using Moq; // To use Mock.Of<T> method.

namespace TestingWithTimeProvider;

public class TimeTests
{
  [Fact]
  public void TestDiscountDuringWorkdays()
  {
    // Arrange
    // DiscountService service = new(TimeProvider.System);

    TimeProvider timeProvider = Mock.Of<TimeProvider>();

    // Mock the time provider so it always returns the date of
    // 2023-11-07 09:30:00 UTC which is a Tuesday.
    Mock.Get(timeProvider).Setup(s => s.GetUtcNow()).Returns(
      new DateTimeOffset(year: 2023, month: 11, day: 7, 
      hour: 9, minute: 30, second: 0, offset: TimeSpan.Zero));

    DiscountService service = new(timeProvider);

    // Act
    decimal discount = service.GetDiscount();

    // Assert
    Assert.Equal(0M, discount);
  }

  [Fact]
  public void TestDiscountDuringWeekends()
  {
    TimeProvider timeProvider = Mock.Of<TimeProvider>();

    // Mock the time provider so it always returns the date of
    // 2023-11-04 09:30:00 UTC which is a Saturday.
    Mock.Get(timeProvider).Setup(s => s.GetUtcNow()).Returns(
      new DateTimeOffset(year: 2023, month: 11, day: 4,
      hour: 9, minute: 30, second: 0, offset: TimeSpan.Zero));

    DiscountService service = new(timeProvider);

    decimal discount = service.GetDiscount();

    Assert.Equal(0.2M, discount);
  }
}