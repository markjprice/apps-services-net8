using System.Globalization; // To use CultureInfo.

ConfigureConsole(); // Defaults to en-US culture.
//ConfigureConsole(overrideComputerCulture: false);
//ConfigureConsole("fr-FR");
//ConfigureConsole("es-AR");
//ConfigureConsole("en-GB");

SectionTitle("Specifying date and time values");

WriteLine($"DateTime.MinValue:  {DateTime.MinValue}");
WriteLine($"DateTime.MaxValue:  {DateTime.MaxValue}");
WriteLine($"DateTime.UnixEpoch: {DateTime.UnixEpoch}");
WriteLine($"DateTime.Now:       {DateTime.Now}");
WriteLine($"DateTime.Today:     {DateTime.Today}");
WriteLine($"DateTime.Today:     {DateTime.Today:d}");
WriteLine($"DateTime.Today:     {DateTime.Today:D}");

DateTime xmas = new(year: 2024, month: 12, day: 25);
WriteLine($"Christmas (default format): {xmas}");
WriteLine($"Christmas (custom short format): {xmas:ddd, d/M/yy}");
WriteLine($"Christmas (custom long format): {
  xmas:dddd, dd MMMM yyyy}");
WriteLine($"Christmas (standard long format): {xmas:D}");
WriteLine($"Christmas (sortable): {xmas:u}");
WriteLine($"Christmas is in month {xmas.Month} of the year.");
WriteLine($"Christmas is day {xmas.DayOfYear} of {xmas.Year}.");
WriteLine($"Christmas {xmas.Year} is on a {xmas.DayOfWeek}.");

SectionTitle("Date and time calculations");

DateTime beforeXmas = xmas.Subtract(TimeSpan.FromDays(12));
DateTime afterXmas = xmas.AddDays(12);

WriteLine($"12 days before Christmas: {beforeXmas:d}");
WriteLine($"12 days after Christmas: {afterXmas:d}");

TimeSpan untilXmas = xmas - DateTime.Now;

WriteLine($"Now: {DateTime.Now}");
WriteLine($"There are {untilXmas.Days} days and {untilXmas.Hours
  } hours until Christmas {xmas.Year}.");

WriteLine($"There are {untilXmas.TotalHours:N0} hours " +
  $"until Christmas {xmas.Year}.");

DateTime kidsWakeUp = new(
  year: 2024, month: 12, day: 25,
  hour: 6, minute: 30, second: 0);

WriteLine($"Kids wake up: {kidsWakeUp}");

WriteLine($"The kids woke me up at {
  kidsWakeUp.ToShortTimeString()}");

SectionTitle("Milli-, micro-, and nanoseconds");

DateTime preciseTime = new(
  year: 2022, month: 11, day: 8,
  hour: 12, minute: 0, second: 0,
  millisecond: 6, microsecond: 999);

WriteLine($"Millisecond: {preciseTime.Millisecond}, Microsecond: {
  preciseTime.Microsecond}, Nanosecond: {preciseTime.Nanosecond}");

preciseTime = DateTime.UtcNow;

// Nanosecond value will be 0 to 900 in 100 nanosecond increments.
WriteLine($"Millisecond: {preciseTime.Millisecond}, Microsecond: {
  preciseTime.Microsecond}, Nanosecond: {preciseTime.Nanosecond}");

SectionTitle("Globalization with dates and times");

// Same as Thread.CurrentThread.CurrentCulture.
WriteLine($"Current culture: {CultureInfo.CurrentCulture.Name}");

string textDate = "4 July 2024";
DateTime independenceDay = DateTime.Parse(textDate);

WriteLine($"Text: {textDate}, DateTime: {independenceDay:d MMMM}");

textDate = "7/4/2024";
independenceDay = DateTime.Parse(textDate);

WriteLine($"Text: {textDate}, DateTime: {independenceDay:d MMMM}");

// Explicitly override the current culture by setting a provider.
independenceDay = DateTime.Parse(textDate,
  provider: CultureInfo.GetCultureInfo("en-US"));

WriteLine($"Text: {textDate}, DateTime: {independenceDay:d MMMM}");

for (int year = 2022; year <= 2028; year++)
{
  Write($"{year} is a leap year: {DateTime.IsLeapYear(year)}. ");
  WriteLine($"There are {DateTime.DaysInMonth(year: year, month: 2)
    } days in February {year}.");
}

WriteLine($"Is Christmas daylight saving time? {
  xmas.IsDaylightSavingTime()}");

WriteLine($"Is July 4th daylight saving time? {
  independenceDay.IsDaylightSavingTime()}");

SectionTitle("Localizing the DayOfWeek enum");

CultureInfo previousCulture = Thread.CurrentThread.CurrentCulture;

// Explicitly set culture to Danish (Denmark).
Thread.CurrentThread.CurrentCulture =
  CultureInfo.GetCultureInfo("da-DK");

// DayOfWeek is not localized to Danish.
WriteLine($"Culture: {Thread.CurrentThread.CurrentCulture
  .NativeName}, DayOfWeek: {DateTime.Now.DayOfWeek}");

// Use dddd format code to get day of the week localized.
WriteLine($"Culture: {Thread.CurrentThread.CurrentCulture
  .NativeName}, DayOfWeek: {DateTime.Now:dddd}");

// Use GetDayName method to get day of the week localized.
WriteLine($"Culture: {Thread.CurrentThread.CurrentCulture
  .NativeName}, DayOfWeek: {DateTimeFormatInfo.CurrentInfo
  .GetDayName(DateTime.Now.DayOfWeek)}");

Thread.CurrentThread.CurrentCulture = previousCulture;

SectionTitle("Working with only a date or a time");

DateOnly party = new(year: 2024, month: 11, day: 12);
WriteLine($"The .NET 9 release party is on {party.ToLongDateString()}.");

TimeOnly starts = new(hour: 11, minute: 30);
WriteLine($"The party starts at {starts}.");

DateTime calendarEntry = party.ToDateTime(starts);
WriteLine($"Add to your calendar: {calendarEntry}.");

SectionTitle("Working with date/time formats");

DateTimeFormatInfo dtfi = DateTimeFormatInfo.CurrentInfo;
// Or use Thread.CurrentThread.CurrentCulture.DateTimeFormat.

WriteLine($"Date separator: {dtfi.DateSeparator}");
WriteLine($"Time separator: {dtfi.TimeSeparator}");

WriteLine($"Long date pattern: {dtfi.LongDatePattern}");
WriteLine($"Short date pattern: {dtfi.ShortDatePattern}");

WriteLine($"Long time pattern: {dtfi.LongTimePattern}");
WriteLine($"Short time pattern: {dtfi.ShortTimePattern}");

Write("Day names:");
for (int i = 0; i < dtfi.DayNames.Length - 1; i++)
{
  Write($"  {dtfi.GetDayName((DayOfWeek)i)}");
}
WriteLine();

Write("Month names:");
for (int i = 1; i < dtfi.MonthNames.Length; i++)
{
  Write($"  {dtfi.GetMonthName(i)}");
}
WriteLine();

