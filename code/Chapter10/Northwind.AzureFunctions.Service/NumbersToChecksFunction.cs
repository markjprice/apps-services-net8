using Humanizer; // To use ToWords extension method.
using Microsoft.Azure.Functions.Worker; // To use [Function] and so on.
using Microsoft.Azure.Functions.Worker.Http; // To use HttpRequestData.
using Microsoft.Extensions.Logging; // To use ILogger.

namespace Northwind.AzureFunctions.Service;

public class NumbersToChecksFunction
{
  private readonly ILogger _logger;

  public NumbersToChecksFunction(ILoggerFactory loggerFactory)
  {
    _logger = loggerFactory.CreateLogger<NumbersToChecksFunction>();
  }

  [Function(nameof(NumbersToChecksFunction))]
  [QueueOutput("checksQueue")] // Return value is written to this queue.
  public string Run(
    [HttpTrigger(AuthorizationLevel.Anonymous,
      "get", "post", Route = null)] HttpRequestData request)
  {
    _logger.LogInformation("C# HTTP trigger function processed a request.");

    string? amount = request.Query["amount"];

    if (long.TryParse(amount, out long number))
    {
       return number.ToWords();

    }
    else
    {
      return $"Failed to parse: {amount}";
    }
  }
}
