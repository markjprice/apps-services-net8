using Grpc.Core;
using Grpc.Net.Client.Configuration;
using Northwind.Grpc.Client.Mvc; // To use Greeter.GreeterClient.
using Northwind.Grpc.Client.Mvc.Interceptors; // To use ClientLoggingInterceptor.

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register the interceptor before attaching it to a gRPC client.
builder.Services.AddSingleton<ClientLoggingInterceptor>();

MethodConfig configForAllMethods = new() 
{
  Names = { MethodName.Default },
  RetryPolicy = new RetryPolicy
  {
    MaxAttempts = 5,
    InitialBackoff = TimeSpan.FromSeconds(1),
    MaxBackoff = TimeSpan.FromSeconds(5),
    BackoffMultiplier = 1.5,
    RetryableStatusCodes = { StatusCode.Unavailable }
  }
};

builder.Services.AddGrpcClient<Greeter.GreeterClient>("Greeter",
  options =>
  {
    options.Address = new Uri("https://localhost:5131");
  })
  .ConfigureChannel(channel =>
  {
    channel.ServiceConfig = new ServiceConfig
    {
      MethodConfigs = { configForAllMethods }
    };
  });

builder.Services.AddGrpcClient<Shipper.ShipperClient>("Shipper",
  options =>
  {
    options.Address = new Uri("https://localhost:5131");
  });

builder.Services.AddGrpcClient<Product.ProductClient>("Product",
  options =>
  {
    options.Address = new Uri("https://localhost:5131");
  })
  .AddInterceptor<ClientLoggingInterceptor>();

builder.Services.AddGrpcClient<Employee.EmployeeClient>("Employee",
  options =>
  {
    options.Address = new Uri("https://localhost:5131");
  })
  .AddInterceptor<ClientLoggingInterceptor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
