using Microsoft.AspNetCore.Mvc; // To use IActionResult and so on.
using Microsoft.Azure.WebJobs; // To use [FunctionName] and [HttpTrigger].
using Microsoft.Azure.WebJobs.Extensions.Http; // To use AuthorizationLevel.
using Microsoft.AspNetCore.Http; // To use HttpRequest.
using Microsoft.Extensions.Logging; // To use ILogger.
using Humanizer; // To use ToWords extension method.
using System.Threading.Tasks; // To use Task<T>.

namespace Northwind.AzureFunctions.Service;

[StorageAccount("AzureWebJobsStorage")]
public static class NumbersToWordsFunction
{
  [FunctionName(nameof(NumbersToWordsFunction))]
  public static async Task<IActionResult> Run(
    [HttpTrigger(AuthorizationLevel.Anonymous,
      "get", "post", Route = null)] HttpRequest req,
    [Queue("checksQueue")] ICollector<string> collector,
    ILogger log)
  {
    log.LogInformation("C# HTTP trigger function processed a request.");

    string amount = req.Query["amount"];

    if (long.TryParse(amount, out long number))
    {
      string words = number.ToWords();
      collector.Add(words);
      return await Task.FromResult(new OkObjectResult(words));
    }
    else
    {
      return new BadRequestObjectResult($"Failed to parse: {amount}");
    }
  }
}
