using NCrontab; // To use CrontabSchedule and so on.

DateTime start = new(year: 2023, month: 1, day: 1);
DateTime end = start.AddYears(1);

WriteLine($"Start at:   {start:ddd, dd MMM yyyy HH:mm:ss}");
WriteLine($"End at:     {end:ddd, dd MMM yyyy HH:mm:ss}");
WriteLine();

/* // Initial configuration.
string sec = "0,30";
string min = "*";
string hour = "*";
string dayOfMonth = "*";
string month = "*";
string dayOfWeek = "*";
*/

// Last configuration.
string sec = "0";
string min = "0";
string hour = "*/4";
string dayOfMonth = "*";
string month = "*";
string dayOfWeek = "*";

string expression = string.Format(
  "{0,-3} {1,-3} {2,-3} {3,-3} {4,-3} {5,-3}",
  sec, min, hour, dayOfMonth, month, dayOfWeek);

WriteLine($"Expression: {expression}");
WriteLine(@"            \ / \ / \ / \ / \ / \ /");
WriteLine($"             -   -   -   -   -   -");
WriteLine($"             |   |   |   |   |   |");
WriteLine($"             |   |   |   |   |   +--- day of week (0 - 6) (Sunday=0)");
WriteLine($"             |   |   |   |   +------- month (1 - 12)");
WriteLine($"             |   |   |   +----------- day of month (1 - 31)");
WriteLine($"             |   |   +--------------- hour (0 - 23)");
WriteLine($"             |   +------------------- min (0 - 59)");
WriteLine($"             +----------------------- sec (0 - 59)");
WriteLine();

CrontabSchedule schedule = CrontabSchedule.Parse(expression,
  new CrontabSchedule.ParseOptions { IncludingSeconds = true });

IEnumerable<DateTime> occurrences = schedule.GetNextOccurrences(start, end);

// Output the first 40 occurrences.
foreach (DateTime occurrence in occurrences.Take(40))
{
  WriteLine($"{occurrence:ddd, dd MMM yyyy HH:mm:ss}");
}
