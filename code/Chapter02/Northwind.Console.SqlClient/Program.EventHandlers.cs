using Microsoft.Data.SqlClient; // To use SqlInfoMessageEventArgs.
using System.Data; // To use StateChangeEventArgs.

partial class Program
{
  private static void Connection_StateChange(object sender, StateChangeEventArgs e)
  {
    WriteLineInColor(
      $"State change from {e.OriginalState} to {e.CurrentState}.",
      ConsoleColor.DarkYellow);
  }

  private static void Connection_InfoMessage(object sender, SqlInfoMessageEventArgs e)
  {
    WriteLineInColor($"Info: {e.Message}.", ConsoleColor.DarkBlue);
  }
}
