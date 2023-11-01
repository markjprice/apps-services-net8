using Foundation;

namespace Northwind.Maui.Client
{
  [Register("AppDelegate")]
  public class AppDelegate : MauiUIApplicationDelegate
  {
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
  }
}
