using GeneratingPdf.Models; // Catalog
using QuestPDF.Drawing; // DocumentMetadata
using QuestPDF.Fluent; // Page
using QuestPDF.Helpers; // Colors
using QuestPDF.Infrastructure; // IDocument, IDocumentContainer

namespace GeneratingPdf.Document;

public class CatalogDocument : IDocument
{
  public Catalog Model { get; }

  public CatalogDocument(Catalog model)
  {
    Model = model;
  }

  public void Compose(IDocumentContainer container)
  {
    container
      .Page(page =>
      {
        page.Margin(50 /* points */);

        page.Header()
          .Height(100).Background(Colors.Grey.Lighten1)
          .AlignCenter().Text("Catalogue")
          .Style(TextStyle.Default.FontSize(20));

        page.Content()
          .Background(Colors.Grey.Lighten3)
          .Table(table =>
          {
            table.ColumnsDefinition(columns =>
            {
              columns.ConstantColumn(100);
              columns.RelativeColumn();
            });

            foreach (var item in Model.Categories)
            {
              table.Cell().Text(item.CategoryName);

              string imagePath = Path.Combine(
                Environment.CurrentDirectory, "images",
                $"category{item.CategoryId}.jpeg");

              table.Cell().Image(imagePath);
            }
          });

        page.Footer()
          .Height(50).Background(Colors.Grey.Lighten1)
          .AlignCenter().Text(x =>
          {
            x.CurrentPageNumber();
            x.Span(" of ");
            x.TotalPages();
          });
      });
  }

  public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
}
