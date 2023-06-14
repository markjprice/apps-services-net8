using Northwind.Grpc.Client.Mvc; // To use Greeter.GreeterClient.

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddGrpcClient<Greeter.GreeterClient>("Greeter",
  options =>
  {
  options.Address = new Uri("https://localhost:5131");
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
  });

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
