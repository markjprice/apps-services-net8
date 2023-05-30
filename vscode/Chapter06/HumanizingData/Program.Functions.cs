using Humanizer; // To use common Humanizer extension methods.
using Humanizer.Inflections; // To use Vocabularies.
using Packt.Shared; // To use WondersOfTheAncientWorld.
using System.Globalization; // To use CultureInfo.

partial class Program
{
  static void ConfigureConsole(string culture = "en-US")
  {
    // To enable special characters like … (ellipsis) as a single character.
    OutputEncoding = System.Text.Encoding.UTF8;

    Thread t = Thread.CurrentThread;
    t.CurrentCulture = CultureInfo.GetCultureInfo(culture);
    t.CurrentUICulture = t.CurrentCulture;

    WriteLine("Current culture: {0}", t.CurrentCulture.DisplayName);
    WriteLine();
  }

  static void OutputCasings(string original)
  {
    WriteLine("Original casing: {0}", original);
    WriteLine("Lower casing: {0}", original.Transform(To.LowerCase));
    WriteLine("Upper casing: {0}", original.Transform(To.UpperCase));
    WriteLine("Title casing: {0}", original.Transform(To.TitleCase));
    WriteLine("Sentence casing: {0}", original.Transform(To.SentenceCase));
    WriteLine("Lower, then Sentence casing: {0}",
      original.Transform(To.LowerCase, To.SentenceCase));
    WriteLine();
  }

  static void OutputSpacingAndDashes()
  {
    string ugly = "ERROR_MESSAGE_FROM_SERVICE";

    WriteLine("Original string: {0}", ugly);

    WriteLine("Humanized: {0}", ugly.Humanize());

    // LetterCasing is legacy and will be removed in future.
    WriteLine("Humanized, lower case: {0}",
      ugly.Humanize(LetterCasing.LowerCase));

    // Use Transform for casing instead.
    WriteLine("Transformed (lower case, then sentence case): {0}",
      ugly.Transform(To.LowerCase, To.SentenceCase));

    WriteLine("Humanized, Transformed (lower case, then sentence case): {0}",
      ugly.Humanize().Transform(To.LowerCase, To.SentenceCase));
  }

  static void OutputEnumNames()
  {
    var favoriteAncientWonder = WondersOfTheAncientWorld.StatueOfZeusAtOlympia;

    WriteLine("Raw enum value name: {0}", favoriteAncientWonder);

    WriteLine("Humanized: {0}.", favoriteAncientWonder.Humanize());

    WriteLine("Humanized, then Titlerized: {0}",
      favoriteAncientWonder.Humanize().Titleize());

    WriteLine("Truncated to 8 characters: {0}",
      favoriteAncientWonder.ToString().Truncate(length: 8));

    WriteLine("Kebaberized: {0}",
      favoriteAncientWonder.ToString().Kebaberize());
  }

  static void NumberFormatting()
  {
    Vocabularies.Default.AddIrregular("biceps", "bicepses");
    Vocabularies.Default.AddIrregular("attorney general", "attorneys general");

    int number = 123;

    WriteLine("Original number: {0}", number);
    WriteLine("Roman: {0}", number.ToRoman());
    WriteLine("Words: {0}", number.ToWords());
    WriteLine("Ordinal words: {0}", number.ToOrdinalWords());
    WriteLine();
    
    string[] things = new[] { "fox", "person", "sheep", 
      "apple", "goose", "oasis", "potato", "die", "dwarf",
      "attorney general","biceps"};

    for (int i = 1; i <= 3; i++)
    {
      for (int j = 0; j < things.Length; j++)
      {
        Write(things[j].ToQuantity(i, ShowQuantityAs.Words));

        if (j < things.Length - 1) Write(", ");
      }
      WriteLine();
    }
    WriteLine();

    int thousands = 12345;
    int millions = 123456789;

    WriteLine("Original: {0}, Metric: About {1}", thousands,
      thousands.ToMetric(decimals: 0));

    WriteLine("Original: {0}, Metric: About {1}", thousands,
      thousands.ToMetric(MetricNumeralFormats.WithSpace 
        | MetricNumeralFormats.UseShortScaleWord, 
        decimals: 0));

    WriteLine("Original: {0}, Metric: {1}", millions,
      millions.ToMetric(decimals: 1));
  }

  static void DateTimeFormatting()
  {
    DateTimeOffset now = DateTimeOffset.Now;

    // By default, all Humanizer comparisons are to Now (UTC).
    WriteLine("Now (UTC): {0}", now);

    WriteLine("Add 3 hours, Humanized: {0}", 
      now.AddHours(3).Humanize());

    WriteLine("Add 3 hours and 1 minute, Humanized: {0}", 
      now.AddHours(3).AddMinutes(1).Humanize());

    WriteLine("Subtract 3 hours, Humanized: {0}", 
      now.AddHours(-3).Humanize());

    WriteLine("Add 24 hours, Humanized: {0}", 
      now.AddHours(24).Humanize());

    WriteLine("Add 25 hours, Humanized: {0}", 
      now.AddHours(25).Humanize());

    WriteLine("Add 7 days, Humanized: {0}", 
      now.AddDays(7).Humanize());

    WriteLine("Add 7 days and 1 minute, Humanized: {0}", 
      now.AddDays(7).AddMinutes(1).Humanize());

    WriteLine("Add 1 month, Humanized: {0}", 
      now.AddMonths(1).Humanize());

    WriteLine();

    // Examples of TimeSpan humanization.

    int[] daysArray = new[] { 12, 13, 14, 15, 16 };

    foreach (int days in daysArray)
    {
      WriteLine("{0} days, Humanized: {1}",
        days, TimeSpan.FromDays(days).Humanize());

      WriteLine("{0} days, Humanized with precision 2: {1}",
        days, TimeSpan.FromDays(days).Humanize(precision: 2));

      WriteLine("{0} days, Humanized with max unit days: {1}",
        days, TimeSpan.FromDays(days).Humanize(
          maxUnit: Humanizer.Localisation.TimeUnit.Day));

      WriteLine();
    }

    // Examples of clock notation.

    TimeOnly[] times = new[] { new TimeOnly(9, 0),
      new TimeOnly(9, 15), new TimeOnly(15, 30) };

    foreach (TimeOnly time in times)
    {
      WriteLine("{0}: {1}", time, time.ToClockNotation());
    }
  }
}
