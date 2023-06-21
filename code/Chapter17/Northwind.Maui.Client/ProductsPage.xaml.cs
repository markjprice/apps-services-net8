namespace Northwind.Maui.Client;

public partial class ProductsPage : ContentPage
{
  public ProductsPage()
  {
    InitializeComponent();

    ProductsViewModel products = new();

    products.AddSampleData();

    BindingContext = products;
  }
}