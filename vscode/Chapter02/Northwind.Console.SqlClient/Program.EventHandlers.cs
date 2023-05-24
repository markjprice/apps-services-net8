using Microsoft.Data.SqlClient; // SqlInfoMessageEventArgs
using System.Data; // StateChangeEventArgs

partial class Program
{
  static void Connection_StateChange(object sender, StateChangeEventArgs e)
  {
    WriteLineInColor(
      $"State change from {e.OriginalState} to {e.CurrentState}.",
      ConsoleColor.DarkYellow);
  }

  static void Connection_InfoMessage(object sender, SqlInfoMessageEventArgs e)
  {
    WriteLineInColor($"Info: {e.Message}.", ConsoleColor.DarkBlue);
  }
}
