using Microsoft.AspNetCore.Mvc;
using Northwind.Grpc.Client.Mvc.Models;
using System.Diagnostics;
using Grpc.Net.ClientFactory; // To use GrpcClientFactory.
using Grpc.Core; // To use AsyncUnaryCall<T>.

namespace Northwind.Grpc.Client.Mvc.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly Greeter.GreeterClient _greeterClient;
    private readonly Shipper.ShipperClient _shipperClient;
    private readonly Product.ProductClient _productClient;
    private readonly Employee.EmployeeClient _employeeClient;

    public HomeController(ILogger<HomeController> logger,
      GrpcClientFactory factory)
    {
      _logger = logger;

      _greeterClient = factory.CreateClient<Greeter.GreeterClient>("Greeter");
      _shipperClient = factory.CreateClient<Shipper.ShipperClient>("Shipper");
      _productClient = factory.CreateClient<Product.ProductClient>("Product");
      _employeeClient = factory.CreateClient<Employee.EmployeeClient>("Employee");
    }

    public async Task<IActionResult> Index(
      string name = "Henrietta", int id = 1)
    {
      HomeIndexViewModel model = new();

      try
      {
        HelloReply reply = await _greeterClient.SayHelloAsync(
          new HelloRequest { Name = name });

        model.Greeting = "Greeting from gRPC service: " + reply.Message;

        //ShipperReply shipperReply = await _shipperClient.GetShipperAsync(
        //  new ShipperRequest { ShipperId = id });

        // The same call as above but not awaited.
        AsyncUnaryCall<ShipperReply> shipperCall = _shipperClient.GetShipperAsync(
          new ShipperRequest { ShipperId = id },
          // Deadline must be a UTC DateTime.
          deadline: DateTime.UtcNow.AddSeconds(3));

        Metadata metadata = await shipperCall.ResponseHeadersAsync;

        foreach (Metadata.Entry entry in metadata)
        {
          // Not really critical, just doing this to make it easier to see.
          _logger.LogCritical($"Key: {entry.Key}, Value: {entry.Value}");
        }

        ShipperReply shipperReply = await shipperCall.ResponseAsync;

        model.ShipperSummary = "Shipper from gRPC service: " +
          $"ID: {shipperReply.ShipperId}, Name: {shipperReply.CompanyName},"
          + $" Phone: {shipperReply.Phone}.";
      }
      catch (RpcException rpcex) when (rpcex.StatusCode ==
        global::Grpc.Core.StatusCode.DeadlineExceeded)
      {
        _logger.LogWarning("Northwind.Grpc.Service deadline exceeded.");

        model.ErrorMessage = rpcex.Message;
      }
      catch (Exception ex)
      {
        _logger.LogWarning($"Northwind.Grpc.Service is not responding.");

        model.ErrorMessage = ex.Message;
      }

      return View(model);
    }

    public async Task<IActionResult> Products(decimal minimumPrice = 0M)
    {
      ProductsReply reply = await _productClient.GetProductsMinimumPriceAsync(
        new ProductsMinimumPriceRequest() { MinimumPrice = minimumPrice });

      return View(reply.Products);
    }

    public async Task<IActionResult> Employees()
    {
      EmployeesReply reply = await _employeeClient.GetEmployeesAsync(
        new EmployeesRequest());

      return View(reply.Employees);
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