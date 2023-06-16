**Blazor WebAssembly topics**

- [Understanding the browser compatibility analyzer for Blazor WebAssembly](#understanding-the-browser-compatibility-analyzer-for-blazor-webassembly)
- [Deployment choices for Blazor WebAssembly apps](#deployment-choices-for-blazor-webassembly-apps)

# Understanding the browser compatibility analyzer for Blazor WebAssembly

With .NET 6 and later, Microsoft has unified the .NET library for all workloads. However, although in theory, this means that a Blazor WebAssembly app has full access to all .NET APIs, in practice, it runs inside a browser sandbox so there are limitations. If you call an unsupported API, this will throw a `PlatformNotSupportedException`.

To be forewarned about unsupported APIs, you can add a platform compatibility analyzer that will warn you when your code uses APIs that are not supported by browsers.

Both the **Blazor WebAssembly App** and **Razor Class Library** project templates automatically enable browser compatibility checks.

To manually activate browser compatibility checks, for example, in a **Class Library** project, add an entry to the project file, as shown in the following markup:
```xml
<ItemGroup>
  <SupportedPlatform Include="browser" />
</ItemGroup>
```

Microsoft decorates unsupported APIs, as shown in the following code:
```cs
[UnsupportedOSPlatform("browser")]
public void DoSomethingOutsideTheBrowserSandbox()
{
  ...
}
```

> **Good Practice**: If you create libraries that should not be used in Blazor WebAssembly apps, then you should decorate your APIs in the same way.

# Deployment choices for Blazor WebAssembly apps

There are two main ways to deploy a Blazor WebAssembly app:
- You can create and deploy just a Blazor WebAssembly app client project by placing its published files in any static hosting web server. For example, Azure Static Web Apps is a potential choice of host for Blazor WebAssembly app. You can read more at the following link: https://learn.microsoft.com/en-us/azure/static-web-apps/overview.
- You can deploy a Blazor WebAssembly app server project, which references the client app and perhaps hosts services called by the Blazor WebAssembly app as well as the app itself. The Blazor WebAssembly app is placed in the server website `wwwroot` folder, along with any other static assets.

> **More Information**: You can read more about these choices at the following link: https://learn.microsoft.com/en-us/aspnet/core/blazor/host-and-deploy/webassembly.
