using CommunityToolkit.Mvvm.Input; // To use [RelayCommand].
using System.Collections.ObjectModel; // To use ObservableCollection<T>.

namespace Northwind.Maui.Client;

internal partial class ProductsViewModel : ObservableCollection<ProductViewModel>
{
  [RelayCommand(CanExecute = nameof(CanDeleteProduct))]
  private void DeleteProduct(int productId)
  {
    ProductViewModel productToRemove =
      this.SingleOrDefault(p => p.ProductId == productId);

    if (productToRemove is not null)
    {
      Remove(productToRemove);
    }
  }

  private bool CanDeleteProduct(int productId)
  {
    ProductViewModel productToRemove =
      this.SingleOrDefault(p => p.ProductId == productId);

    return (productToRemove is not null);
  }

  public void AddSampleData()
  {
    Add(new ProductViewModel()
    {
      ProductId = 1,
      ProductName = "Chai",
      SupplierId = 1,
      CategoryId = 1,
      QuantityPerUnit = "10 boxes x 20 bags",
      UnitPrice = 18M,
      UnitsInStock = 39,
      UnitsOnOrder = 0,
      ReorderLevel = 10,
      Discontinued = false
    });

    Add(new ProductViewModel()
    {
      ProductId = 2,
      ProductName = "Chang",
      SupplierId = 1,
      CategoryId = 1,
      QuantityPerUnit = "24 - 12 oz bottles",
      UnitPrice = 19M,
      UnitsInStock = 17,
      UnitsOnOrder = 40,
      ReorderLevel = 25,
      Discontinued = false
    });

    Add(new ProductViewModel()
    {
      ProductId = 3,
      ProductName = "Aniseed Syrup",
      SupplierId = 1,
      CategoryId = 2,
      QuantityPerUnit = "12 - 550 ml bottles",
      UnitPrice = 10M,
      UnitsInStock = 13,
      UnitsOnOrder = 70,
      ReorderLevel = 25,
      Discontinued = false
    });
  }
}
