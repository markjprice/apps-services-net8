using NodaTime; // To use SystemClock, Instant and so on.

SectionTitle("Converting Noda Time types");

// Get the current instant in time.
Instant now = SystemClock.Instance.GetCurrentInstant();

WriteLine($"Now (Instant): {now}");
WriteLine();

ZonedDateTime nowInUtc = now.InUtc();

WriteLine($"Now (DateTimeZone): {nowInUtc.Zone}");
WriteLine($"Now (ZonedDateTime): {nowInUtc}");
WriteLine($"Now (DST): {nowInUtc.IsDaylightSavingTime()}");
WriteLine();

// Use the Tzdb provider to get the time zone for US Pacific.
// To use .NET compatible time zones, use the Bcl provider.

DateTimeZone zonePT = DateTimeZoneProviders.Tzdb["US/Pacific"];
ZonedDateTime nowInPT = now.InZone(zonePT);

WriteLine($"Now (DateTimeZone): {nowInPT.Zone}");
WriteLine($"Now (ZonedDateTime): {nowInPT}");
WriteLine($"Now (DST): {nowInPT.IsDaylightSavingTime()}");
WriteLine();

DateTimeZone zoneUK = DateTimeZoneProviders.Tzdb["Europe/London"];
ZonedDateTime nowInUK = now.InZone(zoneUK);

WriteLine($"Now (DateTimeZone): {nowInUK.Zone}");
WriteLine($"Now (ZonedDateTime): {nowInUK}");
WriteLine($"Now (DST): {nowInUK.IsDaylightSavingTime()}");
WriteLine();

LocalDateTime nowInLocal = nowInUtc.LocalDateTime;

WriteLine($"Now (LocalDateTime): {nowInLocal}");
WriteLine($"Now (LocalDate): {nowInLocal.Date}");
WriteLine($"Now (LocalTime): {nowInLocal.TimeOfDay}");
WriteLine();

SectionTitle("Working with periods");

// The modern .NET era began with the release of .NET Core 1.0
// on June 27, 2016 at 10am Pacific Time, or 5pm UTC.
LocalDateTime start = new(year: 2016, month: 6, day: 27, hour: 17, minute: 0, second: 0);
LocalDateTime end = LocalDateTime.FromDateTime(DateTime.UtcNow);

WriteLine("Modern .NET era");
WriteLine($"Start: {start}");
WriteLine($"End: {end}");
WriteLine();

Period period = Period.Between(start, end);

WriteLine($"Period: {period}");
WriteLine($"Years: {period.Years}");
WriteLine($"Months: {period.Months}");
WriteLine($"Weeks: {period.Weeks}");
WriteLine($"Days: {period.Days}");
WriteLine($"Hours: {period.Hours}");
WriteLine();

Period p1 = Period.FromWeeks(2);
Period p2 = Period.FromDays(14);

WriteLine($"p1 (period of two weeks): {p1}");
WriteLine($"p2 (period of 14 days): {p2}");
WriteLine($"p1 == p2: {p1 == p2}");
WriteLine($"p1.Normalize() == p2: {p1.Normalize() == p2}");
WriteLine();
