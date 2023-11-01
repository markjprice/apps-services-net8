// To use ObservableObject and [ObservableProperty].
using CommunityToolkit.Mvvm.ComponentModel;

namespace Northwind.Maui.Client;

// ObservableObject implements INotifyPropertyChanged.
internal partial class ProductViewModel : ObservableObject
{
  // This attribute uses the source generator to add the public property
  // named ProductId to the class
  [ObservableProperty]
  private int productId;

  [ObservableProperty]
  private string productName;

  [ObservableProperty]
  private int supplierId;

  [ObservableProperty]
  private int categoryId;

  [ObservableProperty]
  private string quantityPerUnit;

  [ObservableProperty]
  private decimal unitPrice;

  [ObservableProperty]
  private int unitsInStock;

  [ObservableProperty]
  private int unitsOnOrder;

  [ObservableProperty]
  private int reorderLevel;

  [ObservableProperty]
  private bool discontinued;

  // A readonly property to show information about stock.
  public string Stock
  {
    get => $"Stock: {UnitsInStock} in stock, {UnitsOnOrder} on order, reorder at {ReorderLevel}.";
  }
}
