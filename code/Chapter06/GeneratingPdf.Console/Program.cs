using GeneratingPdf.Document; // To use CatalogDocument.
using GeneratingPdf.Models; // To use Catalog, Category.
using QuestPDF.Fluent; // To use the GeneratePdf extension method.
// using QuestPDF.Infrastructure; // To use LicenseType.

// For evaluation purposes, feel free to use the QuestPDF Community 
// License in a non-production environment.
// Setting the license type is only required or possible with
// version 2023.4.0 or later.
// QuestPDF.Settings.License = LicenseType.Community;

string filename = "catalog.pdf";

Catalog model = new()
{
  Categories = new()
  {
    new() { CategoryId = 1, CategoryName = "Beverages"},
    new() { CategoryId = 2, CategoryName = "Condiments"},
    new() { CategoryId = 3, CategoryName = "Confections"},
    new() { CategoryId = 4, CategoryName = "Dairy Products"},
    new() { CategoryId = 5, CategoryName = "Grains/Cereals"},
    new() { CategoryId = 6, CategoryName = "Meat/Poultry"},
    new() { CategoryId = 7, CategoryName = "Produce"},
    new() { CategoryId = 8, CategoryName = "Seafood"}
  }
};

CatalogDocument document = new(model);
document.GeneratePdf(filename);

WriteLine("PDF catalog has been created: {0}",
  Path.Combine(Environment.CurrentDirectory, filename));

try
{
  if (OperatingSystem.IsWindows())
  {
    System.Diagnostics.Process.Start("explorer.exe", filename);
  }
  else
  {
    WriteLine("Open the file manually.");
  }
}
catch (Exception ex)
{
  WriteLine($"{ex.GetType()} says {ex.Message}");
}
