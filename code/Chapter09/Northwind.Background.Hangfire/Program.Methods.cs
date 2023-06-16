using static System.Console;

partial class Program
{
  public static void WriteMessage(string? message)
  {
    ConsoleColor previousColor = ForegroundColor;
    ForegroundColor = ConsoleColor.Green;
    WriteLine(message);
    ForegroundColor = previousColor;
  }
}

