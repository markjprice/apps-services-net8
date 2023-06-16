partial class Program
{
  static void MethodA()
  {
    TaskTitle("Starting Method A...");
    OutputThreadInfo();
    Thread.Sleep(3000); // Simulate three seconds of work.
    TaskTitle("Finished Method A.");
  }

  static void MethodB()
  {
    TaskTitle("Starting Method B...");
    OutputThreadInfo();
    Thread.Sleep(2000); // Simulate two seconds of work.
    TaskTitle("Finished Method B.");
  }

  static void MethodC()
  {
    TaskTitle("Starting Method C...");
    OutputThreadInfo();
    Thread.Sleep(1000); // Simulate one second of work.
    TaskTitle("Finished Method C.");
  }

  static decimal CallWebService()
  {
    TaskTitle("Starting call to web service...");
    OutputThreadInfo();
    Thread.Sleep((new Random()).Next(2000, 4000));
    TaskTitle("Finished call to web service.");
    return 89.99M;
  }

  static string CallStoredProcedure(decimal amount)
  {
    TaskTitle("Starting call to stored procedure...");
    OutputThreadInfo();
    Thread.Sleep((new Random()).Next(2000, 4000));
    TaskTitle("Finished call to stored procedure.");
    return $"12 products cost more than {amount:C}.";
  }

  static void OuterMethod()
  {
    TaskTitle("Outer method starting...");

    Task innerTask = Task.Factory.StartNew(InnerMethod,
      TaskCreationOptions.AttachedToParent);
    
    TaskTitle("Outer method finished.");
  }

  static void InnerMethod()
  {
    TaskTitle("Inner method starting...");
    Thread.Sleep(2000);
    TaskTitle("Inner method finished.");
  }
}
