using Microsoft.AspNetCore.SignalR; // To use Hub.
using System.Runtime.CompilerServices; // To use [EnumeratorCancellation].
using Northwind.SignalR.Streams; // To use StockPrice.

namespace Northwind.SignalR.Service.Hubs;

public class StockPriceHub : Hub
{
  public async IAsyncEnumerable<StockPrice> GetStockPriceUpdates(
    string stock,
    [EnumeratorCancellation] CancellationToken cancellationToken)
  {
    double currentPrice = 267.10; // Simulated initial price.

    for (int i = 0; i < 10; i++)
    {
      // Check the cancellation token regularly so that the server will stop
      // producing items if the client disconnects.
      cancellationToken.ThrowIfCancellationRequested();

      // Increment or decrement the current price by a random amount.
      // The compiler does not need the extra parentheses but it
      // is clearer for humans if you put them in.
      currentPrice += (Random.Shared.NextDouble() * 10.0) - 5.0;

      StockPrice stockPrice = new(stock, currentPrice);

      Console.WriteLine("[{0}] {1} at {2:C}",
        DateTime.UtcNow, stockPrice.Stock, stockPrice.Price);

      yield return stockPrice;

      await Task.Delay(4000, cancellationToken); // milliseconds
    }
  }

  public async Task UploadStocks(IAsyncEnumerable<string> stocks)
  {
    await foreach (string stock in stocks)
    {
      Console.WriteLine($"Uploading {stock}...");
    }
  }
}
