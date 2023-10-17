using Microsoft.AspNetCore.Mvc;
using Northwind.Mvc.Models;
using System.Diagnostics;
using Northwind.EntityModels; // To use Northwind entity models.
using Microsoft.EntityFrameworkCore; // To use Include method.

namespace Northwind.Mvc.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly NorthwindContext _db;

    public HomeController(ILogger<HomeController> logger, 
      NorthwindContext db)
    {
      _logger = logger;
      _db = db;
    }

    public IActionResult Index(
      string? id = null, string? country = null)
    {
      // Start with a simplified initial model.
      IEnumerable<Order> model = _db.Orders
        .Include(order => order.Customer)
        .Include(order => order.OrderDetails);

      // Add filtering based on parameters.
      if (id is not null)
      {
        model = model.Where(order => order.Customer?.CustomerId == id);
      }
      else if (country is not null)
      {
        model = model.Where(order => order.Customer?.Country == country);
      }

      // Add ordering and make enumerable.
      model = model
        .OrderByDescending(order => order.OrderDetails
          .Sum(detail => detail.Quantity * detail.UnitPrice))
        .AsEnumerable();

      return View(model);
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

    public IActionResult Shipper(Shipper shipper)
    {
      return View(shipper);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ProcessShipper(Shipper shipper)
    {
      return Json(shipper);
    }
  }
}