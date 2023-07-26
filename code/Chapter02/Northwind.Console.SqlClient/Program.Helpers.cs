using Microsoft.Data.SqlClient; // To use SqlConnection.
using System.Collections; // To use IDictionary.
using System.Globalization; // To use CultureInfo.

partial class Program
{
  private static void ConfigureConsole(string culture = "en-US",
    bool useComputerCulture = false)
  {
    // To enable Unicode characters like Euro symbol in the console.
    OutputEncoding = System.Text.Encoding.UTF8;

    if (!useComputerCulture)
    {
      CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(culture);
    }
    WriteLine($"CurrentCulture: {CultureInfo.CurrentCulture.DisplayName}");
  }

  private static void WriteLineInColor(string value, 
    ConsoleColor color = ConsoleColor.Black)
  {
    ConsoleColor previousColor = ForegroundColor;
    ForegroundColor = color;
    WriteLine(value);
    ForegroundColor = previousColor;
  }

  private static void OutputStatistics(SqlConnection connection)
  {
    // Remove all the string values to see all the statistics.
    string[] includeKeys = { 
      "BytesSent", "BytesReceived", "ExecutionTime", "SelectRows" 
    };

    IDictionary statistics = connection.RetrieveStatistics();

    foreach (object? key in statistics.Keys)
    {
      if (!includeKeys.Any() || includeKeys.Contains(key))
      {
        if (int.TryParse(statistics[key]?.ToString(), out int value))
        {
          WriteLineInColor($"{key}: {value:N0}", ConsoleColor.Cyan);
        }
      }
    }
  }
}