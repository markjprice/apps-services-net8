using Microsoft.AspNetCore.Mvc; // To use IActionResult.
using Microsoft.AspNetCore.OData.Query; // To use [EnableQuery].
using Microsoft.AspNetCore.OData.Routing.Controllers; // To use ODataController.
using Northwind.EntityModels; // To use NorthwindContext.

namespace Northwind.OData.Services.Controllers;

public class ShippersController : ODataController
{
  protected readonly NorthwindContext db;

  public ShippersController(NorthwindContext db)
  {
    this.db = db;
  }

  [EnableQuery]
  public IActionResult Get()
  {
    return Ok(db.Shippers);
  }

  [EnableQuery]
  public IActionResult Get(int key)
  {
    return Ok(db.Shippers.Where(
      shipper => shipper.ShipperId == key));
  }
}
