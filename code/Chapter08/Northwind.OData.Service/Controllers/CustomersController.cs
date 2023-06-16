using Microsoft.AspNetCore.Mvc; // To use IActionResult.
using Microsoft.AspNetCore.OData.Query; // To use [EnableQuery].
using Microsoft.AspNetCore.OData.Routing.Controllers; // To use ODataController.
using Northwind.EntityModels; // To use NorthwindContext.

namespace Northwind.OData.Services.Controllers;

public class CustomersController : ODataController
{
  protected readonly NorthwindContext db;

  public CustomersController(NorthwindContext db)
  {
    this.db = db;
  }

  [EnableQuery]
  public IActionResult Get()
  {
    return Ok(db.Customers);
  }

  [EnableQuery]
  public IActionResult Get(string key)
  {
    return Ok(db.Customers.Where(
      customer => customer.CustomerId == key));
  }
}
