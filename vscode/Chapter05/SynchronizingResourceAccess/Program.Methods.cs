partial class Program
{
  static void MethodA()
  {
    try
    {
      if (Monitor.TryEnter(SharedObjects.Conch, TimeSpan.FromSeconds(15)))
      {
        for (int i = 0; i < 5; i++)
        {
          // Simulate two seconds of work on the current thread.
          Thread.Sleep(Random.Shared.Next(2000));

          // Concatenate the letter "A" to the shared message.
          SharedObjects.Message += "A";

          // Increment the shared counter.
          Interlocked.Increment(ref SharedObjects.Counter);

          // Show some activity in the console output.
          Write(".");
        }
      }
      else
      {
        WriteLine("Method A timed out when entering a monitor on conch.");
      }
    }
    finally
    {
      Monitor.Exit(SharedObjects.Conch);
    }
  }

  static void MethodB()
  {
    try
    {
      if (Monitor.TryEnter(SharedObjects.Conch, TimeSpan.FromSeconds(15)))
      {
        for (int i = 0; i < 5; i++)
        {
          Thread.Sleep(Random.Shared.Next(2000));
          SharedObjects.Message += "B";
          Interlocked.Increment(ref SharedObjects.Counter);
          Write(".");
        }
      }
      else
      {
        WriteLine("Method B timed out when entering a monitor on conch.");
      }
    }
    finally
    {
      Monitor.Exit(SharedObjects.Conch);
    }
  }
}