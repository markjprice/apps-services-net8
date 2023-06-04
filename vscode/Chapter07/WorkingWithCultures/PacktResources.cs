using Microsoft.Extensions.Localization; // To use IStringLocalizer and so on.

public class PacktResources
{
  private readonly IStringLocalizer<PacktResources> localizer = null!;

  public PacktResources(IStringLocalizer<PacktResources> localizer)
  {
    this.localizer = localizer;
  }

  public string? GetEnterYourNamePrompt()
  {
    string resourceStringName = "EnterYourName";

    // 1. Get the LocalizedString object.
    LocalizedString localizedString = localizer[resourceStringName];

    // 2. Check if the resource string was found.
    if (localizedString.ResourceNotFound)
    {
      ConsoleColor previousColor = ForegroundColor;
      ForegroundColor = ConsoleColor.Red;
      WriteLine($"Error: resource string \"{resourceStringName}\" not found."
        + Environment.NewLine
        + $"Search path: {localizedString.SearchedLocation}");
      ForegroundColor = previousColor;

      return $"{localizedString}: ";
    }
    // 3. Return the found resource string.
    return localizedString;
  }

  public string? GetEnterYourDobPrompt()
  {
    // LocalizedString has an implicit cast to string
    // that falls back to the key if the resource
    // string is not found.
    return localizer["EnterYourDob"];
  }

  public string? GetEnterYourSalaryPrompt()
  {
    return localizer["EnterYourSalary"];
  }

  public string? GetPersonDetails(
    string name, DateTime dob, int minutes, decimal salary)
  {
    return localizer["PersonDetails", name, dob, minutes, salary];
  }
}
