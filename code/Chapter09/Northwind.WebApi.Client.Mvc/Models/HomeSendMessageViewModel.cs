using Northwind.Queue.Models; // To use ProductQueueMessage.

namespace Northwind.WebApi.Client.Mvc.Models;

public class HomeSendMessageViewModel
{
  public string? Info { get; set; }
  public string? Error { get; set; }
  public ProductQueueMessage? Message { get; set; }
}
