partial class Program
{
  async static IAsyncEnumerable<int> GetNumbersAsync()
  {
    Random r = Random.Shared;

    // Simulate some work that takes 1.5 to 3 seconds.
    await Task.Delay(r.Next(1500, 3000));

    // Return a random number between 1 and 1000.
    yield return r.Next(1, 1001);

    await Task.Delay(r.Next(1500, 3000));
    yield return r.Next(1, 1001);

    await Task.Delay(r.Next(1500, 3000));
    yield return r.Next(1, 1001);
  }
}