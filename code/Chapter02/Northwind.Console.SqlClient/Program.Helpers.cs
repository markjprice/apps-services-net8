using Microsoft.Data.SqlClient; // SqlConnection
using System.Collections; // IDictionary

partial class Program
{
  static void WriteLineInColor(string value, 
    ConsoleColor color = ConsoleColor.Black)
  {
    ConsoleColor previousColor = ForegroundColor;
    ForegroundColor = color;
    WriteLine(value);
    ForegroundColor = previousColor;
  }

  static void OutputStatistics(SqlConnection connection)
  {
    // Remove all the string values to see all the statistics.
    string[] includeKeys = new string[] { "BytesSent",
      "BytesReceived", "ExecutionTime", "SelectRows" };

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