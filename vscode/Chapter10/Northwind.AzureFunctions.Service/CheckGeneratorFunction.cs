using Azure.Storage.Blobs; // To use BlobContainerClient.
using Azure.Storage.Blobs.Models; // To use BlobContainerInfo.
using Azure.Storage.Queues.Models; // To use QueueMessage.
using Microsoft.Azure.WebJobs; // To use [FunctionName], [QueueTrigger].
using Microsoft.Extensions.Logging; // To use ILogger.
using SixLabors.Fonts; // To use Font.
using SixLabors.ImageSharp; // To use Image.
using SixLabors.ImageSharp.Drawing; // To use IPath.
using SixLabors.ImageSharp.Drawing.Processing; // To use IBrush, IPen.
using SixLabors.ImageSharp.PixelFormats; // To use PixelColorBlendingMode.
using SixLabors.ImageSharp.Processing; // To use Mutate extension method.
using System.IO; // To use Stream, FileAccess.
using System.Threading.Tasks; // To use Task<T>.

namespace Northwind.AzureFunctions.Service;

[StorageAccount("AzureWebJobsStorage")]
public static class CheckGeneratorFunction
{
  [FunctionName(nameof(CheckGeneratorFunction))]
  public static async Task Run(
    [QueueTrigger("checksQueue")] QueueMessage message,
    [Blob("checks-blob-container")] BlobContainerClient blobContainerClient,
    ILogger log)
  {
    // Write some information about the message to the log.
    log.LogInformation("C# Queue trigger function executed.");
    log.LogInformation($"MessageId: {message.MessageId}.");
    log.LogInformation($"InsertedOn: {message.InsertedOn}.");
    log.LogInformation($"ExpiresOn: {message.ExpiresOn}.");
    log.LogInformation($"Body: {message.Body}.");

    // Create a new blank image with a white background.
    using (Image<Rgba32> image = new(width: 1200, height: 600,
      backgroundColor: new Rgba32(r: 255, g: 255, b: 255, a: 100)))
    {
      // Load the font file and create a large font.
      FontCollection collection = new();
      FontFamily family = collection.Add(
        @"fonts\Caveat\static\Caveat-Regular.ttf");

      Font font = family.CreateFont(72);

      string amount = message.Body.ToString();

      DrawingOptions options = new()
      {
        GraphicsOptions = new()
        {
          ColorBlendingMode = PixelColorBlendingMode.Multiply
        }
      };

      // Define some pens and brushes.

      IPen blackPen = Pens.Solid(Color.Black, 2);
      IPen blackThickPen = Pens.Solid(Color.Black, 8);
      IPen greenPen = Pens.Solid(Color.Green, 3);
      IBrush redBrush = Brushes.Solid(Color.Red);
      IBrush blueBrush = Brushes.Solid(Color.Blue);

      // Define some paths and draw them.

      IPath border = new RectangularPolygon(
        x: 50, y: 50, width: 1100, height: 500);

      image.Mutate(x => x.Draw(options, blackPen, border));

      IPath star = new Star(x: 150.0f, y: 150.0f,
        prongs: 5, innerRadii: 20.0f, outerRadii: 30.0f);

      image.Mutate(x => x.Fill(options, redBrush, star)
                         .Draw(options, greenPen, star));

      IPath line1 = new Polygon(new LinearLineSegment(
        new PointF(x: 100, y: 275), new PointF(x: 1050, y: 275)));

      image.Mutate(x => x.Draw(options, blackPen, line1));

      IPath line2 = new Polygon(new LinearLineSegment(
        new PointF(x: 100, y: 365), new PointF(x: 1050, y: 365)));

      image.Mutate(x => x.Draw(options, blackPen, line2));

      TextOptions textOptions = new(font)
      {
        Origin = new PointF(100, 200),
        WrappingLength = 1000,
        HorizontalAlignment = HorizontalAlignment.Left
      };

      image.Mutate(x => x.DrawText(
        textOptions, amount, blueBrush, blackPen));

      string blobName = $"{System.DateTime.UtcNow:yyyy-MM-dd-hh-mm-ss}.png";
      log.LogInformation($"Blob name: {blobName}.");

      try
      {
        if (System.Environment.GetEnvironmentVariable("IS_LOCAL") == "true")
        {
          // Create blob in the local filesystem.

          string folder = $@"{System.Environment.CurrentDirectory}\blobs";
          if (!Directory.Exists(folder))
          {
            Directory.CreateDirectory(folder);
          }
          log.LogInformation($"Blobs folder: {folder}");

          string blobPath = $@"{folder}\{blobName}";

          await image.SaveAsPngAsync(blobPath);
        }

        // Create BLOB in Blob Storage via a memory stream.

        Stream stream = new MemoryStream();

        await image.SaveAsPngAsync(stream);

        stream.Seek(0, SeekOrigin.Begin);

        blobContainerClient.CreateIfNotExists();

        BlobContentInfo info = await blobContainerClient.UploadBlobAsync(
          blobName, stream);

        log.LogInformation(
          $"Blob sequence number: {info.BlobSequenceNumber}.");
      }
      catch (System.Exception ex)
      {
        log.LogError(ex.Message);
      }
    }
  }
}
