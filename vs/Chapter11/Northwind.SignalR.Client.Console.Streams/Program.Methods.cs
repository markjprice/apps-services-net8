// Defined in the empty default namespace to merge with the auto-
// generated partial Program class.

partial class Program
{
  static async IAsyncEnumerable<string> GetStocksAsync()
  {
    for (int i = 0; i < 10; i++)
    {
      // Return a random four-letter stock code.
      yield return $"{AtoZ()}{AtoZ()}{AtoZ()}{AtoZ()}";

      await Task.Delay(TimeSpan.FromSeconds(3));
    }
  }

  static string AtoZ()
  {
    return char.ConvertFromUtf32(Random.Shared.Next(65, 91));
  }
}
