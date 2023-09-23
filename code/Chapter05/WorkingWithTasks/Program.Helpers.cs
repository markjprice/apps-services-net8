partial class Program
{
  private static void SectionTitle(string title)
  {
    ConsoleColor previousColor = ForegroundColor;
    ForegroundColor = ConsoleColor.DarkYellow;
    WriteLine($"*** {title}");
    ForegroundColor = previousColor;
  }

  private static void TaskTitle(string title)
  {
    ConsoleColor previousColor = ForegroundColor;
    ForegroundColor = ConsoleColor.Green;
    WriteLine($"{title}");
    ForegroundColor = previousColor;
  }

  private static void OutputThreadInfo()
  {
    Thread t = Thread.CurrentThread;

    ConsoleColor previousColor = ForegroundColor;
    ForegroundColor = ConsoleColor.DarkCyan;

    WriteLine(
      "Thread Id: {0}, Priority: {1}, Background: {2}, Name: {3}",
      t.ManagedThreadId, t.Priority, t.IsBackground, t.Name ?? "null");

    ForegroundColor = previousColor;
  }
}
