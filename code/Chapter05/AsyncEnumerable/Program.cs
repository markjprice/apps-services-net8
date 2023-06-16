// Use async streams to iterate over a collection asynchronously.
await foreach (int number in GetNumbersAsync())
{
  WriteLine($"Number: {number}");
}
