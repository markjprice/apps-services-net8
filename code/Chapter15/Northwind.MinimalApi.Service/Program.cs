using Microsoft.AspNetCore.Mvc; // To use [FromServices].
using Northwind.EntityModels; // To use AddNorthwindContext.
using System.Text.Json.Serialization; // To use ReferenceHandler.

// Define an alias for the JsonOptions class.
using HttpJsonOptions = Microsoft.AspNetCore.Http.Json.JsonOptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddNorthwindContext();

builder.Services.Configure<HttpJsonOptions>(options =>
{
  // If we do not preserve references then when the JSON serializer
  // encounters a circular reference it will throw an exception.
  options.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
  var forecast = Enumerable.Range(1, 5).Select(index =>
      new WeatherForecast
      (
          DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
          Random.Shared.Next(-20, 55),
          summaries[Random.Shared.Next(summaries.Length)]
      ))
      .ToArray();
  return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

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

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
  public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
