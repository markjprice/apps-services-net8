partial class Program
{
  private static void OutputCultures(string title)
  {
    ConsoleColor previousColor = ForegroundColor;
    ForegroundColor = ConsoleColor.DarkYellow;

    WriteLine($"*** {title}");

    // Get the cultures from the current thread.
    CultureInfo globalization = CultureInfo.CurrentCulture;
    CultureInfo localization = CultureInfo.CurrentUICulture;

    WriteLine($"The current globalization culture is {
      globalization.Name}: {globalization.DisplayName}");

    WriteLine($"The current localization culture is {
      localization.Name}: {localization.DisplayName}");

    WriteLine($"Days of the week: {string.Join(", ", 
      globalization.DateTimeFormat.DayNames)}");

    WriteLine($"Months of the year: {string.Join(", ", 
      globalization.DateTimeFormat.MonthNames
      // Some have 13 months; most 12, and the last is empty.
      .TakeWhile(month => !string.IsNullOrEmpty(month)))}");

    WriteLine($"1st day of this year: {new DateTime(
      year: DateTime.Today.Year, month: 1, day: 1)
      .ToString("D", globalization)}");

    WriteLine($"Number group separator: {globalization
      .NumberFormat.NumberGroupSeparator}");

    WriteLine($"Number decimal separator: {globalization
      .NumberFormat.NumberDecimalSeparator}");

    RegionInfo region = new(globalization.LCID);

    WriteLine($"Currency symbol: {region.CurrencySymbol}");

    WriteLine($"Currency name: {region.CurrencyNativeName} ({
      region.CurrencyEnglishName})");

    WriteLine($"IsMetric: {region.IsMetric}");

    WriteLine();

    ForegroundColor = previousColor;
  }
}
