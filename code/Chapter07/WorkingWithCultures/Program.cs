using Microsoft.Extensions.Hosting; // To use IHost, Host.

// To use AddLocalization, AddTransient<T>.
using Microsoft.Extensions.DependencyInjection;

using IHost host = Host.CreateDefaultBuilder(args)
  .ConfigureServices(services =>
  {
    services.AddLocalization(options =>
    {
      options.ResourcesPath = "Resources";
    });

    services.AddTransient<PacktResources>();
  })
  .Build();

// To enable special characters like €.
OutputEncoding = System.Text.Encoding.UTF8;

OutputCultures("Current culture");

WriteLine("Example ISO culture codes:");

string[] cultureCodes = {
  "da-DK", "en-GB", "en-US", "fa-IR",
  "fr-CA", "fr-FR", "he-IL", "pl-PL", "sl-SI" };

foreach (string code in cultureCodes)
{
  CultureInfo culture = CultureInfo.GetCultureInfo(code);
  WriteLine($"  {culture.Name}: {culture.EnglishName} / {
    culture.NativeName}");
}

WriteLine();

Write("Enter an ISO culture code: ");
string? cultureCode = ReadLine();

if (string.IsNullOrWhiteSpace(cultureCode))
{
  cultureCode = "en-US";
}

CultureInfo ci;

try
{
  ci = CultureInfo.GetCultureInfo(cultureCode);
}
catch (CultureNotFoundException)
{
  WriteLine($"Culture code not found: {cultureCode}");
  WriteLine("Exiting the app.");
  return;
}

// change the current cultures on the thread
CultureInfo.CurrentCulture = ci;
CultureInfo.CurrentUICulture = ci;

OutputCultures("After changing the current culture");

PacktResources resources =
    host.Services.GetRequiredService<PacktResources>();

Write(resources.GetEnterYourNamePrompt());
string? name = ReadLine();
if (string.IsNullOrWhiteSpace(name))
{
  name = "Bob";
}

Write(resources.GetEnterYourDobPrompt());
string? dobText = ReadLine();

if (string.IsNullOrWhiteSpace(dobText))
{
  // If they do not enter a DOB then use
  // sensible defaults for their culture.
  dobText = ci.Name switch
  {
    "en-US" or "fr-CA" => "1/27/1990",
    "da-DK" or "fr-FR" or "pl-PL" => "27/1/1990",
    "fa-IR" => "1990/1/27",
    _ => "1/27/1990"
  };
}

Write(resources.GetEnterYourSalaryPrompt());
string? salaryText = ReadLine();

if (string.IsNullOrWhiteSpace(salaryText))
{
  salaryText = "34500";
}

DateTime dob = DateTime.Parse(dobText);
int minutes = (int)DateTime.Today.Subtract(dob).TotalMinutes;
decimal salary = decimal.Parse(salaryText);

// WriteLine($"{name} was born on a {dob:dddd}. {name} is {
//   minutes:N0} minutes old. {name} earns {salary:C}.");

WriteLine(resources.GetPersonDetails(name, dob, minutes, salary));
