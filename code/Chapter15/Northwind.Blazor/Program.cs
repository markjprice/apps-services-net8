using Northwind.Blazor;
using Northwind.EntityModels; // To use AddNorthwindContext method.
using Microsoft.AspNetCore.Mvc; // To use [FromServices].
using System.Text.Json.Serialization; // To use ReferenceHandler.
using System.Net.Http.Headers; // To use MediaTypeWithQualityHeaderValue.
using Northwind.Blazor.Services; // To use LocalStorageService.

// Define an alias for the JsonOptions class.
using HttpJsonOptions = Microsoft.AspNetCore.Http.Json.JsonOptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
  .AddServerComponents();

builder.Services.AddNorthwindContext();

builder.Services.Configure<HttpJsonOptions>(options =>
{
  // If we do not preserve references then when the JSON serializer
  // encounters a circular reference it will throw an exception.
  options.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;

  // Is this necessary?
  //options.SerializerOptions.PropertyNameCaseInsensitive = true;
});

builder.Services.AddHttpClient(name: "Northwind.Blazor.Service",
  configureClient: options =>
  {
    options.BaseAddress = new("https://localhost:5151/");
    options.DefaultRequestHeaders.Accept.Add(
      new MediaTypeWithQualityHeaderValue(
        "application/json", 1.0));
  });

builder.Services.AddScoped<LocalStorageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapGet("api/employees", (
  [FromServices] NorthwindContext db) =>
    Results.Json(db.Employees))
  .WithName("GetEmployees")
  .Produces<Employee[]>(StatusCodes.Status200OK);

app.MapGet("api/employees/{id:int}", (
  [FromServices] NorthwindContext db,
  [FromRoute] int id) =>
{
  Employee? employee = db.Employees.Find(id);
  if (employee == null)
  {
    return Results.NotFound();
  }
  else
  {
    return Results.Json(employee);
  }
})
  .WithName("GetEmployeesById")
  .Produces<Employee>(StatusCodes.Status200OK)
  .Produces(StatusCodes.Status404NotFound);

app.MapGet("api/employees/{country}", (
  [FromServices] NorthwindContext db,
  [FromRoute] string country) =>
    Results.Json(db.Employees.Where(employee =>
    employee.Country == country)))
  .WithName("GetEmployeesByCountry")
  .Produces<Employee[]>(StatusCodes.Status200OK);

app.MapPost("api/employees", async ([FromBody] Employee employee,
  [FromServices] NorthwindContext db) =>
{
  db.Employees.Add(employee);
  await db.SaveChangesAsync();
  return Results.Created($"api/employees/{employee.EmployeeId}", employee);
})
  .Produces<Employee>(StatusCodes.Status201Created);

app.MapRazorComponents<App>();

app.Run();
