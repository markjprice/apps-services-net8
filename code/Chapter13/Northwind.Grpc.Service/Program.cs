using Northwind.Grpc.Service.Services;
using Northwind.EntityModels; // To use AddNorthwindContext method.

// Use the slim builder to reduce the size of the application
// when using the publish AOT project option.
// var builder = WebApplication.CreateSlimBuilder(args);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc().AddJsonTranscoding();

builder.Services.AddNorthwindContext();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<ShipperService>();
app.MapGrpcService<ProductService>();
app.MapGrpcService<EmployeeService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
