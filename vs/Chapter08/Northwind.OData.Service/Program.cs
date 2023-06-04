using Microsoft.AspNetCore.OData; // To use AddOData extension method.
using Northwind.EntityModels; // To use AddNorthwindContext extension method.

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddNorthwindContext();

builder.Services.AddControllers()
  .AddOData(options => options

    // Register two OData models.
    .AddRouteComponents(routePrefix: "catalog",
      model: GetEdmModelForCatalog())

    .AddRouteComponents(routePrefix: "ordersystem",
      model: GetEdmModelForOrderSystem())

    .AddRouteComponents(routePrefix: "catalog/v{version}",
      model: GetEdmModelForCatalog())

    // Enable query options:
    .Select()       // $select for projection.
    .Expand()       // $expand to navigate to related entities.
    .Filter()       // $filter.
    .OrderBy()      // $orderby to sort.
    .SetMaxTop(100) // $top.
    .Count()        // $count.
  );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
