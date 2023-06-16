using Microsoft.Extensions.Caching.Memory; // To use IMemoryCache and so on.
using Northwind.EntityModels; // To use the AddNorthwindContext method.

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IMemoryCache>(new MemoryCache(
  new MemoryCacheOptions
  {
    TrackStatistics = true,
    SizeLimit = 50 // Products.
  }));

builder.Services.AddDistributedMemoryCache();

builder.Services.AddResponseCaching();

builder.Services.AddNorthwindContext();
builder.Services.AddControllers();
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

app.UseResponseCaching();

app.UseAuthorization();

app.MapControllers();

app.Run();
