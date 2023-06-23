using CommunityToolkit.Mvvm.Input; // To use [RelayCommand].
using System.Collections.ObjectModel; // To use ObservableCollection<T>.
using System.Net.Http.Headers; // To use MediaTypeWithQualityHeaderValue.
using System.Net.Http.Json; // To use ReadFromJsonAsync<T>.

namespace Northwind.Maui.Blazor.Client.Views.Categories;

internal partial class CategoriesViewModel : ObservableCollection<Category>
{
  // These properties do not need to support two-way binding
  // because they are set programmatically to show to user.

  public string InfoMessage { get; set; } = string.Empty;
  public string ErrorMessage { get; set; } = string.Empty;
  public bool ErrorMessageVisible { get; set; }

  public CategoriesViewModel()
  {
    try
    {
      string domain = DeviceInfo.Platform
        == DevicePlatform.Android ? "10.0.2.2" : "localhost";

      HttpClient client = new()
        { BaseAddress = new Uri($"http://{domain}:5182") };

      InfoMessage = $"BaseAddress: {client.BaseAddress}. ";

      client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));

      HttpResponseMessage response = client
        .GetAsync("api/categories").Result;

      response.EnsureSuccessStatusCode();

      IEnumerable<Category> categories =
        response.Content.ReadFromJsonAsync
        <IEnumerable<Category>>().Result;

      foreach (Category category in categories)
      {
        category.PicturePath = $"category{category.CategoryId}_small.jpeg";

        Add(category);
      }

      InfoMessage += $"{Count} categories loaded.";
    }
    catch (Exception ex)
    {
      ErrorMessage = ex.Message;
      ErrorMessageVisible = true;
    }
  }

  [RelayCommand]
  private void AddCategoryToFavorites()
  {
    Console.WriteLine("Add category to favorites");
  }

  [RelayCommand]
  private void DeleteCategory()
  {
    Console.WriteLine("Delete category");
  }
}
