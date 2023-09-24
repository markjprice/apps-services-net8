OutputTimeZones();

OutputDateTime(DateTime.Now, "DateTime.Now");
OutputDateTime(DateTime.UtcNow, "DateTime.UtcNow");

OutputTimeZone(TimeZoneInfo.Local, "TimeZoneInfo.Local");
OutputTimeZone(TimeZoneInfo.Utc, "TimeZoneInfo.Utc");

Write("Enter a time zone or press Enter for US East Coast: ");
string zoneId = ReadLine()!;

if (string.IsNullOrEmpty(zoneId))
{
  zoneId = "Eastern Standard Time";
}

try
{
  TimeZoneInfo otherZone = TimeZoneInfo.FindSystemTimeZoneById(zoneId);
  OutputTimeZone(otherZone,
    $"TimeZoneInfo.FindSystemTimeZoneById(\"{zoneId}\")");

  SectionTitle($"What's the time in {zoneId}?");

  Write("Enter a local time or press Enter for now: ");
  string? timeText = ReadLine();
  DateTime localTime;
  if ((string.IsNullOrEmpty(timeText)) ||
    (!DateTime.TryParse(timeText, out localTime)))
  {
    localTime = DateTime.Now;
  }

  DateTime otherZoneTime = TimeZoneInfo.ConvertTime(
    dateTime: localTime, sourceTimeZone: TimeZoneInfo.Local,
    destinationTimeZone: otherZone);

  WriteLine($"{localTime} {GetCurrentZoneName(TimeZoneInfo.Local, 
    localTime)} is {otherZoneTime} {GetCurrentZoneName(otherZone, 
    otherZoneTime)}.");
}
catch (TimeZoneNotFoundException)
{
  WriteLine($"The {zoneId} zone cannot be found on the local system.");
}
catch (InvalidTimeZoneException)
{
  WriteLine($"The {zoneId} zone contains invalid or missing data.");
}
catch (System.Security.SecurityException)
{
  WriteLine("The application does not have permission to read time zone information.");
}
catch (OutOfMemoryException)
{
  WriteLine($"Not enough memory is available to load information on the {zoneId} zone.");
}
