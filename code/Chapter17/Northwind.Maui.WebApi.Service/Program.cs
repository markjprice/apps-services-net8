using Microsoft.AspNetCore.Mvc; // To use [FromServices] .
using Northwind.EntityModels; // To use AddNorthwindContext method.

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddNorthwindContext();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapGet("api/customers", (
  [FromServices] NorthwindContext db) => db.Customers)
  .WithName("GetCustomers")
  .Produces<Customer[]>(StatusCodes.Status200OK);

app.MapGet("api/customers/{id}", (
  [FromRoute] string id,
  [FromServices] NorthwindContext db) => db.Customers
    .FirstOrDefault(c => c.CustomerId == id))
  .WithName("GetCustomer")
  .Produces<Customer>(StatusCodes.Status200OK);

app.MapPost("api/customers", async (
  [FromBody] Customer customer,
  [FromServices] NorthwindContext db) =>
{
  db.Customers.Add(customer);
  await db.SaveChangesAsync();
  return Results.Created($"api/customers/{customer.CustomerId}", customer);
}).WithOpenApi()
  .Produces<Customer>(StatusCodes.Status201Created);

app.MapPut("api/customers/{id}", async (
  [FromRoute] string id,
  [FromBody] Customer customer,
  [FromServices] NorthwindContext db) =>
{
  Customer? foundCustomer = await db.Customers.FindAsync(id);

  if (foundCustomer is null) return Results.NotFound();

  foundCustomer.CompanyName = customer.CompanyName;
  foundCustomer.ContactName = customer.ContactName;
  foundCustomer.ContactTitle = customer.ContactTitle;
  foundCustomer.Address = customer.Address;
  foundCustomer.City = customer.City;
  foundCustomer.Region = customer.Region;
  foundCustomer.PostalCode = customer.PostalCode;
  foundCustomer.Country = customer.Country;
  foundCustomer.Phone = customer.Phone;
  foundCustomer.Fax = customer.Fax;

  await db.SaveChangesAsync();

  return Results.NoContent();
}).WithOpenApi()
  .Produces(StatusCodes.Status404NotFound)
  .Produces(StatusCodes.Status204NoContent);

app.MapDelete("api/customers/{id}", async (
  [FromRoute] string id,
  [FromServices] NorthwindContext db) =>
{
  if (await db.Customers.FindAsync(id) is Customer customer)
  {
    db.Customers.Remove(customer);
    await db.SaveChangesAsync();
    return Results.NoContent();
  }
  return Results.NotFound();
}).WithOpenApi()
  .Produces(StatusCodes.Status404NotFound)
  .Produces(StatusCodes.Status204NoContent);

app.Run();
