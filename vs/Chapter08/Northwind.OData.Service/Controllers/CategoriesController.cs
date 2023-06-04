using Microsoft.AspNetCore.Mvc; // To use IActionResult.
using Microsoft.AspNetCore.OData.Query; // To use [EnableQuery].
using Microsoft.AspNetCore.OData.Routing.Controllers; // To use ODataController.
using Northwind.EntityModels; // To use NorthwindContext.

namespace Northwind.OData.Service.Controllers;

public class CategoriesController : ODataController
{
  protected readonly NorthwindContext db;

  public CategoriesController(NorthwindContext db)
  {
    this.db = db;
  }

  [EnableQuery]
  public IActionResult Get()
  {
    return Ok(db.Categories);
  }

  [EnableQuery]
  public IActionResult Get(int key)
  {
    return Ok(db.Categories.Where(
      category => category.CategoryId == key));
  }
}
