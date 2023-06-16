using Microsoft.AspNetCore.Mvc;
using Northwind.OData.Client.Mvc.Models;
using System.Diagnostics;

namespace Northwind.OData.Client.Mvc.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    protected readonly IHttpClientFactory _clientFactory;

    public HomeController(ILogger<HomeController> logger,
      IHttpClientFactory clientFactory)
    {
      _logger = logger;
      _clientFactory = clientFactory;
    }

    public async Task<IActionResult> Index(string startsWith = "Cha")
    {
      try
      {
        HttpClient client = _clientFactory.CreateClient(
          name: "Northwind.OData.Service");

        HttpRequestMessage request = new(
          method: HttpMethod.Get, requestUri:
          "catalog/products/?$filter=startswith(ProductName, " +
          $"'{startsWith}')&$select=ProductId,ProductName,UnitPrice");

        HttpResponseMessage response = await client.SendAsync(request);

        ViewData["startsWith"] = startsWith;
        ViewData["products"] = (await response.Content
          .ReadFromJsonAsync<ODataProducts>())?.Value;
      }
      catch (Exception ex)
      {
        _logger.LogWarning($"Northwind.OData.Service exception: {ex.Message}");
      }

      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}