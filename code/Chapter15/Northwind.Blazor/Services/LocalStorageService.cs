using Microsoft.JSInterop; // To use IJSRuntime and so on.

namespace Northwind.Blazor.Services;

public class LocalStorageService : IAsyncDisposable
{
  private readonly IJSRuntime jsRuntime;
  private Lazy<IJSObjectReference> jsModule = new();

  public LocalStorageService(IJSRuntime jsRuntime)
  {
    this.jsRuntime = jsRuntime;
  }

  private async Task WaitForReference()
  {
    if (!jsModule.IsValueCreated)
    {
      jsModule = new(await jsRuntime.InvokeAsync<IJSObjectReference>(
        "import", "/js/LocalStorageInterop.js"));
    }
  }

  public async ValueTask DisposeAsync()
  {
    if (jsModule.IsValueCreated)
    {
      await jsModule.Value.DisposeAsync();
    }
  }

  public async Task<string> GetValueAsync(string key)
  {
    await WaitForReference();
    var result = await jsModule.Value.InvokeAsync<string>("get", key);
    return result;
  }

  public async Task SetValueAsync(string key, string value)
  {
    await WaitForReference();
    await jsModule.Value.InvokeVoidAsync("set", key, value);
  }

  public async Task ClearAsync()
  {
    await WaitForReference();
    await jsModule.Value.InvokeVoidAsync("clear");
  }

  public async Task RemoveAsync(string key)
  {
    await WaitForReference();
    await jsModule.Value.InvokeVoidAsync("remove", key);
  }
}
