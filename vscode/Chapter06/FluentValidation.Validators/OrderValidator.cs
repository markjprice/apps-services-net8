using FluentValidation.Models;

namespace FluentValidation.Validators;

public class OrderValidator : AbstractValidator<Order>
{
  public OrderValidator()
  {
    RuleFor(order => order.OrderId)
      .NotEmpty(); // Not default(long) which is 0.

    RuleFor(order => order.CustomerName)
      .NotNull()
      .WithName("Name"); // Use Name instead of CustomerName in messages.

    RuleFor(order => order.CustomerName)
      .MinimumLength(5)
      .WithSeverity(Severity.Warning);

    RuleFor(order => order.CustomerEmail)
      .NotEmpty()
      .EmailAddress();

    RuleFor(order => order.CustomerLevel)
      .IsInEnum();

    RuleFor(order => order.Total)
      .GreaterThan(0);

    RuleFor(order => order.ShipDate)
      .GreaterThan(order => order.OrderDate);

    When(order => order.CustomerLevel == CustomerLevel.Gold, () =>
    {
      RuleFor(order => order.Total).LessThan(50M);
      RuleFor(order => order.Total).GreaterThanOrEqualTo(20M);
    }).Otherwise(() =>
    {
      RuleFor(order => order.Total).LessThan(20M);
    });
  }
}
