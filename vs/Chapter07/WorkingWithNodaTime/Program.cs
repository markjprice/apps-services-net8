using NodaTime; // To use SystemClock, Instant and so on.

SectionTitle("Converting Noda Time types");

// Get the current instant in time.
Instant now = SystemClock.Instance.GetCurrentInstant();

WriteLine("Now (Instant): {0}", now);
WriteLine();

ZonedDateTime nowInUtc = now.InUtc();

WriteLine("Now (DateTimeZone): {0}", nowInUtc.Zone);
WriteLine("Now (ZonedDateTime): {0}", nowInUtc);
WriteLine("Now (DST): {0}", nowInUtc.IsDaylightSavingTime());
WriteLine();

// Use the Tzdb provider to get the time zone for US Pacific.
// To use .NET compatible time zones, use the Bcl provider.

DateTimeZone zonePT = DateTimeZoneProviders.Tzdb["US/Pacific"];
ZonedDateTime nowInPT = now.InZone(zonePT);

WriteLine("Now (DateTimeZone): {0}", nowInPT.Zone);
WriteLine("Now (ZonedDateTime): {0}", nowInPT);
WriteLine("Now (DST): {0}", nowInPT.IsDaylightSavingTime());
WriteLine();

DateTimeZone zoneUK = DateTimeZoneProviders.Tzdb["Europe/London"];
ZonedDateTime nowInUK = now.InZone(zoneUK);

WriteLine("Now (DateTimeZone): {0}", nowInUK.Zone);
WriteLine("Now (ZonedDateTime): {0}", nowInUK);
WriteLine("Now (DST): {0}", nowInUK.IsDaylightSavingTime());
WriteLine();

LocalDateTime nowInLocal = nowInUtc.LocalDateTime;

WriteLine("Now (LocalDateTime): {0}", nowInLocal);
WriteLine("Now (LocalDate): {0}", nowInLocal.Date);
WriteLine("Now (LocalTime): {0}", nowInLocal.TimeOfDay);
WriteLine();

SectionTitle("Working with periods");

// The modern .NET era began with the release of .NET Core 1.0
// on June 27, 2016 at 10am Pacific Time, or 5pm UTC.
LocalDateTime start = new(year: 2016, month: 6, day: 27, hour: 17, minute: 0, second: 0);
LocalDateTime end = LocalDateTime.FromDateTime(DateTime.UtcNow);

WriteLine("Modern .NET era");
WriteLine("Start: {0}", start);
WriteLine("End: {0}", end);
WriteLine();

Period period = Period.Between(start, end);

WriteLine("Period: {0}", period);
WriteLine("Years: {0}", period.Years);
WriteLine("Months: {0}", period.Months);
WriteLine("Weeks: {0}", period.Weeks);
WriteLine("Days: {0}", period.Days);
WriteLine("Hours: {0}", period.Hours);
WriteLine();

Period p1 = Period.FromWeeks(2);
Period p2 = Period.FromDays(14);

WriteLine("p1 (period of two weeks): {0}", p1);
WriteLine("p2 (period of 14 days): {0}", p2);
WriteLine("p1 == p2: {0}", p1 == p2);
WriteLine("p1.Normalize() == p2: {0}", p1.Normalize() == p2);
WriteLine();


