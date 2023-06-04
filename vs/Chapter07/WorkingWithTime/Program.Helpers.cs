using System.Globalization; // To use CultureInfo.

partial class Program
{
  /// <summary>
  /// Configure the Console to use UTF-8 encoding and a culture (US English by default).
  /// </summary>
  /// <param name="culture">Set to a culture code like fr-CA to use that culture.</param>
  /// <param name="overrideComputerCulture">Set to false to use your computer's culture.</param>
  static void ConfigureConsole(string culture = "en-US",
    bool overrideComputerCulture = true)
  {
    // To enable special characters like Euro currency symbol.
    OutputEncoding = System.Text.Encoding.UTF8;

    Thread t = Thread.CurrentThread;

    if (overrideComputerCulture)
    {
      t.CurrentCulture = CultureInfo.GetCultureInfo(culture);
      t.CurrentUICulture = t.CurrentCulture;
    }

    CultureInfo ci = t.CurrentCulture;
    WriteLine("Current culture: {0}", ci.DisplayName);
    WriteLine("Short date pattern: {0}", ci.DateTimeFormat.ShortDatePattern);
    WriteLine("Long date pattern: {0}", ci.DateTimeFormat.LongDatePattern);
    WriteLine();
  }

  static void SectionTitle(string title)
  {
    ConsoleColor previousColor = ForegroundColor;
    ForegroundColor = ConsoleColor.DarkYellow;
    WriteLine($"*** {title}");
    ForegroundColor = previousColor;
  }
}
