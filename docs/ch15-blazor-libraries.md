**Leveraging Open Source Blazor Component Libraries**

- [Understanding open-source Blazor component libraries](#understanding-open-source-blazor-component-libraries)
- [Exploring Radzen Blazor components](#exploring-radzen-blazor-components)
  - [Enabling the Radzen dialog, notification, context menu, and tooltip components](#enabling-the-radzen-dialog-notification-context-menu-and-tooltip-components)
  - [Using the Radzen tooltip and context menu components](#using-the-radzen-tooltip-and-context-menu-components)
  - [Using the Radzen notification and dialog components](#using-the-radzen-notification-and-dialog-components)
- [Building a web service for Northwind entities](#building-a-web-service-for-northwind-entities)
- [Summary](#summary)

This chapter is about exploring open-source Blazor component libraries. We will look at Radzen Blazor in detail because it is free forever, and many of the other component libraries work in the same way. For example, they all include:
- A NuGet package to install.
-	Themes, stylesheets, and JavaScript libraries to register, that often work like or integrate with Bootstrap.
-	Namespaces to import, usually in `_Imports.razor`, so the components are available in your Razor files.
-	Services that must be registered as scoped dependency services, and matching components that must be instantiated in shared layouts before you can use features like notifications and dialog boxes.

Once you have learned how one component library does this, the others are very similar.

# Understanding open-source Blazor component libraries

In *Chapter 15, Building Web Components Using Blazor*, you learned the key concepts around Blazor components and the practicalities of how to build them. Most of the time, you do not need to build your own components for common scenarios because there are plenty of Blazor component libraries, as shown in the following alphabetical list:
- Ant Design Blazor: https://antblazor.com/
- Blazored libraries and components: https://github.com/Blazored
- Blazorise: https://blazorise.com/
- BlazorStrap: https://blazorstrap.io/
- DevExpress Blazor Components: https://www.devexpress.com/blazor/
- MatBlazor: https://www.matblazor.com/
- MudBlazor: https://mudblazor.com/
- PanoramicData.Blazor: https://panoramicdata.github.io/PanoramicData.Blazor/
- Radzen Blazor: https://blazor.radzen.com/
- SyncFusion Blazor UI Components: https://blazor.syncfusion.com/
- Telerik UI for Blazor: https://www.telerik.com/blazor-ui

In this chapter, we will look at some of the components from the free open-source library named Radzen Blazor. You can then choose to investigate some of the others and whether you feel it is worth paying for their commercial licenses.

# Exploring Radzen Blazor components

First, we will create a Blazor Web App project that we will then explore some of the Radzen Blazor components with:

1. Use your preferred code editor to create a new Blazor WebAssembly project, as defined in the following list:
    - Project template: **Blazor WebAssembly App Empty** / `blazorwasm-empty`
    - Workspace/solution file and folder: `Chapter15`
    - Project file and folder: `Northwind.BlazorLibraries`
    - Configure for HTTPS: Selected.
    - ASP.NET Core hosted: Selected or use the `--hosted` switch.
    - Progressive Web Application: Selected or use the `--pwa` switch.
2. Note that three projects have been created, as shown in the following list:
    - `Northwind.BlazorLibraries.Client`
    - `Northwind.BlazorLibraries.Server`
    - `Northwind.BlazorLibraries.Shared`
3. In the `Northwind.BlazorLibraries.Server` project, expand the `Properties` folder, and open the `launchSettings.json` file.
4. For the `https` profile, for its `applicationUrl` setting, change the port numbers to 5153 for `https` and `5154` for `http`, as shown in the following setting:
```json
"applicationUrl": "https://localhost:5153;http://localhost:5154",
```

5. In the `Northwind.BlazorLibraries.Server` project, treat warnings as errors.
6. In the `Northwind.BlazorLibraries.Client` project file, treat warnings as errors, and in the <ItemGroup> with package references, add a reference to the Radzen Blazor package, as shown in the following markup:
```xml
<PackageReference Include="Radzen.Blazor" Version="4.13.2" />
```

7. Build the `Northwind.BlazorLibraries.Client` project to restore packages.
8.	In the `_Imports.razor` file, add statements to import the Radzen and Radzen Blazor namespaces, as shown in the following code:
```cs
@using Radzen
@using Radzen.Blazor
```

9.	In the `Northwind.BlazorLibraries.Client` project, in the `wwwroot` folder, in `index.html`, add markup in the <head> to set a blank favicon, use the latest version of Bootstrap including a <meta> element in the <head> to set the viewport, and to link to the default Radzen Blazor theme CSS file, as shown highlighted in the following markup:
```html
<link rel="icon" href="data:;base64,iVBORw0KGgo=">
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-9ndCyUaIbzAi2FUVXJi0CjmCapSmO7SnpJef0486qhLnuZ2cdeRhO02iuK6FUUVM" crossorigin="anonymous">
<link rel="stylesheet" href="_content/Radzen.Blazor/css/default.css">
```

10.	In `index.html`, at the bottom of the `<body>`, add a `<script>` element to add support for advanced features provided by Popper.js and Radzen Blazor, as shown in the following markup:
```html
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js" integrity="sha384-geWF76RCwLtnZ8qwWowPQNguL3RmwHVBC9FhGdlKrxdiJJigb/j/68SIy3Te4Bkz" crossorigin="anonymous" suppress-error="BL9992"></script>
<script src="_content/Radzen.Blazor/Radzen.Blazor.js" suppress-error="BL9992"></script>
```

Some Radzen Blazor themes require Bootstrap. If you want to avoid Bootstrap, then you can reference the _content/Radzen.Blazor/css/default-base.css file instead, but you then can only use the base default theme without advanced layouts.

## Enabling the Radzen dialog, notification, context menu, and tooltip components

Let's enable some components that have related services that must be configured in the services collection and referenced in the main layout:

1. In the `Northwind.BlazorLibraries.Client` project, in `Program.cs`, import the namespace for working with Radzen services, as shown in the following code:
```cs
using Radzen; // To use DialogService and so on.
```

2. In `Program.cs`, at the bottom of the section that configures services, add statements to enable dialog, notification, tooltip components, and context menu, as shown in the following code:
```cs
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
```

3.	In the Shared folder, in MainLayout.razor, after the @inherits directive, add statements to embed dialog, notification, context menu, and tooltip components, as shown in the following markup:
```html
<RadzenDialog />
<RadzenNotification />
<RadzenContextMenu />
<RadzenTooltip />
```

## Using the Radzen tooltip and context menu components

Let's use the context menu component to show a menu of shipping company items:

1.	In the Pages folder, in Index.razor, add statements to show a tooltip for the heading, show a context menu with shippers as items when the visitor right-clicks the heading, and then show what they clicked in the browser console, as shown in the following code:
```html
@page "/"
@inject TooltipService tooltipService
@inject ContextMenuService contextMenuService

<PageTitle>Index</PageTitle>

<h1 @ref="h1Element"
    @onmouseover="@(args => ShowTooltip(h1Element , 
      new TooltipOptions { Duration = 5000 }))"
    @oncontextmenu=@(args => ShowContextMenuWithItems(args)) 
    @oncontextmenu:preventDefault="true">

    Hello, Radzen Blazor!
</h1>
```
```cs
@code {
  ElementReference h1Element;

  void ShowTooltip(ElementReference elementReference, 
    TooltipOptions? options = null)
  {
    tooltipService.Open(elementReference, 
      "Right-click this heading to see shipping companies.", 
      options);
  }

  void ShowContextMenuWithItems(MouseEventArgs args)
  {
    ContextMenuItem[] menuItems =
    {
      new() { Value = 1, Text = "Speedy Express" },
      new() { Value = 2, Text = "United Package" },
      new() { Value = 3, Text = "Federal Shipping" },
    };

    contextMenuService.Open(args, menuItems, OnMenuItemClick);
  }

  void OnMenuItemClick(MenuItemEventArgs args)
  {
    Console.WriteLine(
      $"Menu item clicked, Value={args.Value}, Text={args.Text}");

    contextMenuService.Close();
  }
}
```

2.	Start the Northwind.BlazorLibraries project without debugging.
    - If you are using Visual Studio 2022, then in the Visual Studio 2022 toolbar, select the https profile as the Startup Project, and Google Chrome as the Web Browser.
    - If you are using Visual Studio Code, then at the command line or terminal, enter the following command: dotnet run --launch-profile https
3.	In Chrome, show Developer Tools and view the Console.
4.	On the home page, right-click the heading and note the menu items are the shipping companies, as shown in *Figure 15A.1*:

![]()
*Figure 15A.1: A context menu with shipping companies*

5.	Select a shipping company and note the output in the browser console, as shown in *Figure 15A.2*:

![]()
*Figure 16.2: The browser console showing the visitor clicked some items in the context menu*

6.	Close the browser and shut down the web server.

## Using the Radzen notification and dialog components

Let's use the notification and dialog components to show which shipping company the visitor selected:

1.	In the Pages folder, in Index.razor, add statements to inject the notification and dialog services, as shown in the following code:
@inject NotificationService notificationService
@inject DialogService dialogService
2.	In Index.razor, in the OnMenuItemClick method, make the method asynchronous, comment out the statement that writes the message to the browser console, and then after closing the context menu, add statements to either pop up a dialog or pop up a notification to show what the visitor clicked in the context menu, depending on if they hold down the Ctrl key when they click, as shown highlighted in the following code:
```cs
async void OnMenuItemClick(MenuItemEventArgs args)
{
  //Console.WriteLine(
  //  $"Menu item clicked, Value={args.Value}, Text={args.Text}");

  contextMenuService.Close();

  if (args.CtrlKey) // show dialog box
  {
    bool? clickedYes = await dialogService.Confirm(
      message: $"Visitor selected: {args.Text}",
      title: $"Value={args.Value}",
      new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });

    string title = string.Format("You clicked \"{0}\"",
      (clickedYes.GetValueOrDefault(true) ? "Yes" : "No"));

    DialogOptions options = new()
    {
      CloseDialogOnOverlayClick = true,
      CloseDialogOnEsc = true            
    };

    dialogService.Open(title, ds =>
      @<div>
        <div class="row">
          <div class="col-md-12">
            @title
          </div>
        </div>
      </div>
      , options);
  }
  else // show notification
  {
    NotificationMessage message = new()
      {
        // 1=Info/Speedy Express
        // 2=Success/United Package
        // 3=Warning/Federal Shipping
        Severity = (NotificationSeverity)args.Value,
        Summary = $"Value={args.Value}",
        Detail = $"Visitor selected: {args.Text}",
        Duration = 4000 // milliseconds
      };

    notificationService.Notify(message);
  }
}
```

3.	Start the Northwind.BlazorLibraries.Server project without debugging.
4.	On the home page, right-click the heading, click United Package, and note the notification that pops up for four seconds, as shown in *Figure 15A.3*:

![]()
*Figure 15A.3: A notification message with the success color scheme and icon*

5.	Right-click the heading, hold down the *Ctrl* key and click **United Package**, and note the dialog box that pops up, as shown in *Figure 15A.4*:

![]()
*Figure 15A.4: A confirmation dialog box with Yes and No button choices*

6.	Click **Yes**, and note the dialog box appears with custom content, as shown in *Figure 15A.5*:

![]()
*Figure 15A.5: A dialog box with custom content and a close button*

7.	Either click the close button or click outside the dialog box to close it.
8.	Select the other shipping company menu items and note the difference in color scheme and icons for the notifications.
9.	Close the browser and shut down the web server.

# Building a web service for Northwind entities

Now that you have seen the minimum implementation of an entity component, we can add the functionality to fetch entities. In this case, we will use the Northwind database context to fetch employees from the database and expose it as a Minimal API web service:

1.	In Northwind.BlazorLibraries.Server.csproj, add a reference to the Northwind database context project for SQL Server, as shown in the following markup:
```xml
<ItemGroup>
  <ProjectReference Include="..\..\..\Chapter02\Northwind.Common.DataContext.SqlServer\Northwind.Common.DataContext.SqlServer.csproj" />
</ItemGroup>
```

> **Warning!** Unlike previous projects, relative path references for shared projects like the entity models and the database are three levels up, for example, "..\..\..", because we have additional depths of folders for Server, Client, and Shared.

2.	Build the Northwind.BlazorLibraries.Server project at the command line or terminal.
3.	In the Northwind.BlazorLibraries.Server project, in Program.cs, import namespaces for working with Minimal API attributes, registering the Northwind database context extension method, and serializing JSON, as shown in the following code:
```cs
using Microsoft.AspNetCore.Mvc; // [FromServices]
using Packt.Shared; // AddNorthwindContext extension method
using System.Text.Json.Serialization; // ReferenceHandler
using Microsoft.EntityFrameworkCore; // Include extension method

using HttpJsonOptions = Microsoft.AspNetCore.Http.Json.JsonOptions;
```

4.	In `Program.cs`, after the call to CreateBuilder, add a statement to configure the registered dependency service for JSON options and set its reference handler to preserve references, so that the reference between an employee and their manager does not cause a runtime exception due to circular references, as shown in the following code:
builder.Services.Configure<HttpJsonOptions>(options =>
{
  options.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
5.	In Program.cs, before the call to Build, add a statement to register the Northwind database context in the dependency services collection, as shown in the following code:
builder.Services.AddNorthwindContext();
6.	In Program.cs, before the call to the MapRazorPages method, add statements to define some endpoints to GET categories and orders, as shown in the following code:
app.MapGet("api/categories", (
  [FromServices] NorthwindContext db) => 
    Results.Json(
      db.Categories.Include(c => c.Products)))
  .WithName("GetCategories")
  .Produces<Category[]>(StatusCodes.Status200OK);

app.MapGet("api/orders/", (
  [FromServices] NorthwindContext db) =>
    Results.Json(
      db.Orders.Include(o => o.OrderDetails)))
  .WithName("GetOrders")
  .Produces<Order[]>(StatusCodes.Status200OK);
7.	Start the Northwind.BlazorLibraries.Server project without debugging.
8.	In the browser address box, enter the path to request categories, as shown in the following link: https://localhost:5161/api/categories, and note the response as shown in Figure 16.6:
 
Figure 16.6: Testing that the web service returns categories
9.	In the browser address box, enter the path to request orders, as shown in the following link: https://localhost:5161/api/orders, and note the response as shown in Figure 16.7:
 
Figure 16.7: Testing that the web service returns orders
10.	Close the browser and stop the server.
Using the Radzen tabs, image, and icon components
Let's use the tabs component to show the categories and their products from the Northwind database.
There are eight categories in the Northwind database. If we use the category names for the tabs, then they will be too wide. Instead, we will choose an icon for each category that will be shown in the tab.
Radzen has an icon component that uses Google Material Icons, as shown in the following markup:
<RadzenIcon Icon="facebook" />
<RadzenIcon Icon="accessibility" />
<RadzenIcon Icon="accessibility" IconStyle="IconStyle.Primary">
Some other components, like the <RadzenTabsItem> component, have an Icon property that can be set to the same keywords.
You can search for appropriate icons at the following link: https://fonts.google.com/icons?selected=Material+Icons
Microsoft have open sourced their Fluent Emoji, a collection of familiar, friendly, and modern emoji. We will use some of them to add a brighter, more colorful image icon for each category.
You can review and download the collection of Fluent Emoji at the following link: https://github.com/microsoft/fluentui-emoji
Each category has a Picture property that is a byte array containing a low-quality JPEG image. We will create a helper extension method for byte arrays to encode the JPEG image as a Base64 string for use as the src attribute for an <img> element on a web page.
Let's go!
1.	In Northwind.BlazorLibrary.Shared.csproj, in the SharedClass.cs file, add statements to define an extension method, as shown in the following code:
namespace Packt.Shared;

public static class NorthwindExtensionMethods
{
  public static string ConvertToBase64Jpeg(this byte[] picture)
  {
    return string.Format("data:image/jpg;base64,{0}",
      Convert.ToBase64String(picture));
  }
}
2.	In Northwind.BlazorLibrary.Client.csproj, add a reference to the Northwind entities project for SQL Server, as shown in the following markup:
<ItemGroup>
  <ProjectReference Include="..\..\..\Chapter02\Northwind.Common.EntityModels
.SqlServer\Northwind.Common.EntityModels.SqlServer.csproj" />
</ItemGroup>
3.	Build the Northwind.BlazorLibrary.Client project at the command line or terminal.
4.	In the project folder, in _Imports.razor, import the namespaces for serializing JSON, and the Northwind entities so that Blazor components that we build do not need to import the namespaces individually, as shown in the following markup:
@using Packt.Shared
@using System.Text.Json
@using System.Text.Json.Serialization
5.	In MainLayout.razor, remove the temporary coming soon text and add statements to define navigation links to the home page and a categories page component, as shown in the following markup:
<nav class="nav nav-pills flex-column">
  <div class="nav-item px-3">
    <NavLink class="nav-link" href="/" Match="NavLinkMatch.All">
      Home
    </NavLink>
  </div>
  <div class="nav-item px-3">
    <NavLink class="nav-link" href="categories" Match="NavLinkMatch.All">
      Categories
    </NavLink>
  </div>
</nav>
6.	In the Northwind.BlazorLibraries.Client project, in the Pages folder, add a new Razor Component named Categories.razor.
7.	In Categories.razor, add statements to define a tab for each category and a list of products on each tab, as shown in the following code:
@page "/categories"

@inject IHttpClientFactory httpClientFactory

<h3>Categories</h3>

<RadzenTabs>
  <Tabs>
    @if (categories is null)
    {
      <RadzenTabsItem Text="None">
        <h3>No category found.</h3>
        <div class="alert alert-info">
          No products found for this category.
        </div>
      </RadzenTabsItem>
    }
    else
    {
      @foreach (Category category in categories)
      {
        <RadzenTabsItem Icon="@ConvertToIcon(category.CategoryName)">
          <h3>
            <RadzenImage Path="@ConvertToEmoji(category.CategoryName)" 
                         Style="height:48px;width:48px;" />
            @category.CategoryName
            <RadzenBadge BadgeStyle="BadgeStyle.Warning" IsPill="true"
                         Text="@category.Products.Count().ToString()" />
          </h3>
          <div class="alert alert-info">
            @foreach (Product product in category.Products)
            {
              <RadzenBadge BadgeStyle="BadgeStyle.Info" 
                           Text="@product.ProductName" />
            }
          </div>
          @if (category.Picture is not null)
          {
            <div>
              <img class="rounded float-start"
                   src="@category.Picture.ConvertToBase64Jpeg()" />
            </div>
          }
        </RadzenTabsItem>
      }
    }
  </Tabs>
</RadzenTabs>

@code {
  private IQueryable<Category>? categories;

  private string ConvertToIcon(string categoryName)
  {
    return categoryName switch
    {
      "Beverages" => "coffee", // Google Material Icons
      "Condiments" => "liquor",
      "Confections" => "cake",
      "Dairy Products" => "water_drop",
      "Grains/Cereals" => "breakfast_dining",
      "Meat/Poultry" => "kebab_dining",
      "Produce" => "restaurant",
      "Seafood" => "set_meal",
      _ => "device_unknown"
    };
  }

  private string ConvertToEmoji(string categoryName)
  {
    return categoryName switch
    {
      // These paths are relative to the wwwroot folder.
      "Beverages" => "assets/Hot beverage/3D/hot_beverage_3d.png",
      "Condiments" => "assets/Honey pot/3D/honey_pot_3d.png",
      "Confections" => "assets/Lollipop/3D/lollipop_3d.png",
      "Dairy Products" => "assets/Cheese wedge/3D/cheese_wedge_3d.png",
      "Grains/Cereals" => "assets/Bread/3D/bread_3d.png",
      "Meat/Poultry" => "assets/Cut of meat/3D/cut_of_meat_3d.png",
      "Produce" => "assets/Leafy green/3D/leafy_green_3d.png",
      "Seafood" => "assets/Lobster/3D/lobster_3d.png",
      _ => "assets/Pot of food/3D/pot_of_food_3d.png"
    };
  }

  protected override async Task OnParametersSetAsync()
  {
    Category[]? categoriesArray = null;

    // Web API service uses "Preserve" so
    // we must control how references are handled.
    JsonSerializerOptions jsonOptions = new()
      {
        ReferenceHandler = ReferenceHandler.Preserve,
        PropertyNameCaseInsensitive = true
      };

    HttpClient client = httpClientFactory.CreateClient(
      "Northwind.BlazorLibraries.ServerAPI");

    string path = "api/categories";

    try
    {
      categoriesArray = (await client.GetFromJsonAsync<Category[]?>(
          path, jsonOptions));
    }
    catch (Exception ex)
    {
      Console.WriteLine($"{ex.GetType()}: {ex.Message}");
    }

    if (categoriesArray is not null)
    {
      categories = categoriesArray.AsQueryable();
    }
  }
}
8.	Start your browser and navigate to the GitHub repository at the following link: https://github.com/microsoft/fluentui-emoji
9.	Click the green Code button and then click Download ZIP.
10.	Extract the ZIP file.
11.	In the Northwind.BlazorLibraries.Client project, in the wwwroot folder, create a folder named assets.
12.	From the fluentui-emoji-main\assets folder, copy the folders for the images used by the categories, as shown in the following list and in Figure 16.8:
•	assets/Hot beverage/3D/hot_beverage_3d.png
•	assets/Honey pot/3D/honey_pot_3d.png
•	assets/Lollipop/3D/lollipop_3d.png
•	assets/Cheese wedge/3D/cheese_wedge_3d.png
•	assets/Bread/3D/bread_3d.png
•	assets/Cut of meat/3D/cut_of_meat_3d.png
•	assets/Leafy green/3D/leafy_green_3d.png
•	assets/Lobster/3D/lobster_3d.png
•	assets/Pot of food/3D/pot_of_food_3d.png
 
Figure 16.8: Fluent UI Emoji image assets added to the Client project
13.	Start the Northwind.BlazorLibraries.Server project without debugging.
14.	On the home page, in the left navigation, click Categories, and note the tabs for the eight Northwind categories, as shown in Figure 16.9:
 
Figure 16.9: The Grains/Cereals tab selected to show its products and image from the database
15.	Close the browser and shut down the web server.
Using the Radzen HTML editor component
Now we will use the Radzen HTML editor component to provide an editing experience:
1.	In the Pages folder, add a new file named HtmlEditor.razor.
2.	In HtmlEditor.razor, add statements to define an instance of the Radzen HTML editor component and bind it to a string property that contains some simple HTML, as shown in the following code:
@page "/htmleditor"

<RadzenHtmlEditor @bind-Value=@HtmlValue />

@code {
  [Parameter]
  public string HtmlValue { get; set; } = 
    "<h1>Hello, Radzen Blazor!</h1><p></p>";
}
3.	In MainLayout.razor, add statements to define a navigation link to the HTML editor page component, as shown in the following markup:
<div class="nav-item px-3">
  <NavLink class="nav-link" href="htmleditor">
    HTML Editor
  </NavLink>
</div>
4.	Start the Northwind.BlazorLibraries.Server project without debugging.
5.	On the home page, in the left navigation, click HTML Editor, and note the HTML editor with its toolbar, as shown in Figure 16.10:
 
Figure 16.10: The HTML editor component in action
6.	Close the browser and shut down the web server.
You can learn more about customizing the HTML editor component at the following link: https://blazor.radzen.com/docs/guides/components/htmleditor.html

Using the Radzen chart component
Now we will use the Radzen chart component to visualize some numeric data about orders in the Northwind database:
1.	In the Pages folder, add a new file named OrdersBarChart.razor.
2.	In OrdersBarChart.razor, add statements to inject the HTTP client factory and then use it to output a bar chart of revenue grouped by country, as shown in the following code:
@page "/orders-bar-chart"
@using System.Globalization
@inject IHttpClientFactory httpClientFactory

<RadzenCheckBox @bind-Value="@showDataLabels" 
                Name="dataLabels"></RadzenCheckBox>
<RadzenLabel Text="Show Data Labels" For="dataLabels" 
             Style="margin-left: 8px; vertical-align: middle;" />

<RadzenChart>
  <RadzenBarSeries Data="@revenue" CategoryProperty="Country" 
                   LineType="LineType.Dashed" ValueProperty="Revenue">
    <RadzenSeriesDataLabels Visible="@showDataLabels" />
  </RadzenBarSeries>
  <RadzenValueAxis Formatter="@FormatAsUSD">
    <RadzenGridLines Visible="true" />
    <RadzenAxisTitle Text="Revenue in USD" />
  </RadzenValueAxis>
  <RadzenBarOptions Radius="5" />
  <RadzenLegend Visible="false" />
</RadzenChart>

@code {
  bool showDataLabels = false;

  class DataItem
  {
    public string? Country { get; set; }
    public decimal Revenue { get; set; }
  }

  private string FormatAsUSD(object value)
  {
    return ((double)value).ToString("C0",
      CultureInfo.GetCultureInfo("en-US"));
  }

  private DataItem[]? revenue;

  protected override async Task OnParametersSetAsync()
  {
    Order[]? ordersArray = null;

    // Web API service uses "Preserve" so
    // we must control how references are handled.
    JsonSerializerOptions jsonOptions = new()
      {
        ReferenceHandler = ReferenceHandler.Preserve ,
        PropertyNameCaseInsensitive = true
      };

    HttpClient client = httpClientFactory.CreateClient(
      "Northwind.BlazorLibraries.ServerAPI");

    string path = "api/orders";

    try
    {
      ordersArray = (await client.GetFromJsonAsync<Order[]?>(
        path, jsonOptions));

      revenue = ordersArray?
        .GroupBy(order => order.ShipCountry)
        .Select(group => new DataItem
          {
            Country = group.Key,
            Revenue = group.Sum(order => order.OrderDetails.Sum(
            detail => detail.UnitPrice * detail.Quantity))
          })
        .OrderByDescending(dataitem => dataitem.Revenue)
        .ToArray();
    }
    catch (Exception ex)
    {
      Console.WriteLine($"{ex.GetType()}: {ex.Message}");
    }
  }
}
3.	In the Pages folder, add a new file named CategoriesPieChart.razor.
4.	In CategoriesPieChart.razor, add statements to inject the HTTP client factory and then use it to output a pie chart of the number of products in each category, as shown in the following code:
@page "/categories-pie-chart"

@inject IHttpClientFactory httpClientFactory

<RadzenCheckBox @bind-Value="@showDataLabels" Name="dataLabels">
</RadzenCheckBox>
<RadzenLabel Text="Show Data Labels" For="dataLabels" 
             Style="margin-left: 8px; vertical-align: middle;" />

<RadzenChart>
  <RadzenPieSeries Data="@categoryProducts" Title="Product Count" 
      CategoryProperty="Category" ValueProperty="ProductCount">
    <RadzenSeriesDataLabels Visible="@showDataLabels" />
  </RadzenPieSeries>
</RadzenChart>

@code {
  bool showDataLabels = false;

  class DataItem
  {
    public string? Category { get; set; }
    public decimal ProductCount { get; set; }
  }

  private DataItem[]? categoryProducts;

  protected override async Task OnParametersSetAsync()
  {
    Category[]? categoriesArray = null;

    // Web API service uses "Preserve" so
    // we must control how references are handled.
    JsonSerializerOptions jsonOptions = new()
      {
        ReferenceHandler = ReferenceHandler.Preserve ,
        PropertyNameCaseInsensitive = true
      };

    HttpClient client = httpClientFactory.CreateClient(
      "Northwind.BlazorLibraries.ServerAPI");

    string path = "api/categories";

    try
    {
      categoriesArray = (await client.GetFromJsonAsync<Category[]?>(
        path, jsonOptions));

      categoryProducts = categoriesArray?
        .Select(category => new DataItem
          {
            Category = category.CategoryName,
            ProductCount = category.Products.Count()
          })
        .OrderByDescending(dataitem => dataitem.ProductCount)
        .ToArray();
    }
    catch (Exception ex)
    {
      Console.WriteLine($"{ex.GetType()}: {ex.Message}");
    }
  }
}
5.	In MainLayout.razor, add statements to define navigation links to the orders bar chart and categories pie chart page components, as shown in the following markup:
<div class="nav-item px-3">
  <NavLink class="nav-link" href="orders-bar-chart">
    Orders Bar Chart
  </NavLink>
</div>
<div class="nav-item px-3">
  <NavLink class="nav-link" href="categories-pie-chart">
    Categories Pie Chart
  </NavLink>
</div>
6.	Start the Northwind.BlazorLibraries.Server project without debugging.
7.	Start Chrome and navigate to https://localhost:5161/.
8.	In the left navigation menu, click Orders Bar Chart, select the Show Data Labels check box, hover over one of the bars, and note tooltip shows details of the total revenue for that country, as shown for the UK in Figure 16.11:
 
Figure 16.11: A bar chart of order revenue per country
9.	In the left navigation menu, click Categories Pie Chart, and note that categories in the chart are ordered from highest to lowest number of products, starting with Condiments which has 13 products, as shown in Figure 16.12:
 
Figure 16.12: A pie chart of the number of products per category
10.	Select Show Data Labels and hover over a pie segment to see a tooltip.
11.	Close Chrome and shut down the web server.
Using the Radzen form components
Now we will use the Radzen form components to enable viewing and editing of employees in the Northwind database:
1.	In the Northwind.BlazorLibraries.Server project, in Program.cs, before the call to the MapRazorPages method, add statements to define some endpoints to GET and PUT employees and related data like a list of cities and countries, as shown in the following code:
app.MapGet("api/employees/", (
  [FromServices] NorthwindContext db) =>
    Results.Json(db.Employees))
  .WithName("GetEmployees")
  .Produces<Employee[]>(StatusCodes.Status200OK);

app.MapGet("api/countries/", (
  [FromServices] NorthwindContext db) =>
    Results.Json(db.Employees.Select(emp => emp.Country).Distinct()))
  .WithName("GetCountries")
  .Produces<string[]>(StatusCodes.Status200OK);

app.MapGet("api/cities/", (
  [FromServices] NorthwindContext db) =>
    Results.Json(db.Employees.Select(emp => emp.City).Distinct()))
  .WithName("GetCities")
  .Produces<string[]>(StatusCodes.Status200OK);

app.MapPut("api/employees/{id:int}", async (
    [FromRoute] int id,
    [FromBody] Employee employee,
    [FromServices] NorthwindContext db) =>
  {
    Employee? foundEmployee = await db.Employees.FindAsync(id);

    if (foundEmployee is null) return Results.NotFound();

    foundEmployee.FirstName = employee.FirstName;
    foundEmployee.LastName = employee.LastName;
    foundEmployee.BirthDate = employee.BirthDate;
    foundEmployee.HireDate = employee.HireDate;
    foundEmployee.Address = employee.Address;
    foundEmployee.City = employee.City;
    foundEmployee.Country = employee.Country;
    foundEmployee.Region = employee.Region;
    foundEmployee.PostalCode = employee.PostalCode;
    foundEmployee.ReportsTo = employee.ReportsTo;
    foundEmployee.Title = employee.Title;
    foundEmployee.TitleOfCourtesy = employee.TitleOfCourtesy;
    foundEmployee.Notes = employee.Notes;

    int affected = await db.SaveChangesAsync();

    return Results.Json(affected);
  })
  .Produces(StatusCodes.Status200OK)
  .Produces(StatusCodes.Status404NotFound);
2.	Start the Northwind.BlazorLibraries.Server project without debugging.
3.	In the browser address box, enter the path to request employees, as shown in the following link: https://localhost:5161/api/employees, and note the response as shown in Figure 16.13:
 
Figure 16.13: Testing that the web service returns employees
4.	In the browser address box, enter the path to request countries, as shown in the following link: https://localhost:5161/api/countries, and note the response as shown in the following output:
["UK","USA"]
5.	In the browser address box, enter the path to request cities, as shown in the following link: https://localhost:5161/api/cities, and note the response as shown in the following output:
[null,"Kirkland","London","Redmond","Seattle","Tacoma"]
6.	In MainLayout.razor, add statements to define navigation links to the employees page component, as shown in the following markup:
<div class="nav-item px-3">
  <NavLink class="nav-link" href="employees">
    Employees
  </NavLink>
</div>
7.	In the Northwind.BlazorLibraries.Client project, in the Pages folder, add a new file named Employees.razor.
8.	In Employees.razor, add statements to inject the HTTP client factory and then use it to output a form to select and then edit employees, as shown in the following code:
This is a long section of code because employees have more than 20 properties and each must be data bound to a control to edit it. You might prefer to copy it from the GitHub repository and then review it line-by-line rather than enter it yourself. I have highlighted the most interesting blocks of code, and there are notes about them after the code.
@page "/employees"
@using System.Net
@inject IHttpClientFactory httpClientFactory
@inject NotificationService notificationService

<h3>Employees</h3>

<RadzenCard>
  <RadzenListBox AllowFiltering="true" TValue="int"
                 FilterCaseSensitivity="FilterCaseSensitivity.CaseInsensitive"
                 Data=@employees
                 TextProperty="FirstName"
                 ValueProperty="EmployeeId"
                 Change=@(args => OnChange(args, "ListBox with filtering"))
                 Style="height:150px" Class="w-100" />
</RadzenCard>
<hr />
@if (employee != null)
{
  <RadzenTemplateForm Data="@employee" 
      Submit="@((Employee employee) => { Submit(employee); })">
    <div class="row">
      <div class="col-md-6">
        <RadzenFieldset Text="Employee Details">
          <div class="row">
            <div class="col-md-4 align-items-center d-flex">
              <RadzenLabel Text="Employee ID" />
            </div>
            <div class="col-md-8">
              <RadzenNumeric style="width: 100%;" Name="EmployeeId" 
                @bind-Value="employee.EmployeeId" ReadOnly="true" />
            </div>
          </div>
          <div class="row">
            <div class="col-md-4 align-items-center d-flex">
              <RadzenLabel Text="Title" />
            </div>
            <div class="col-md-8">
              <RadzenTextBox style="width: 100%;" Name="Title" 
                @bind-Value="employee.Title" />
            </div>
          </div>
          <div class="row">
            <div class="col-md-4 align-items-center d-flex">
              <RadzenLabel Text="Title of Courtesy" />
            </div>
            <div class="col-md-8">
              <RadzenTextBox style="width: 100%;" Name="TitleOfCourtesy" 
                @bind-Value="employee.TitleOfCourtesy" />
            </div>
          </div>
          <div class="row">
            <div class="col-md-4 align-items-center d-flex">
              <RadzenLabel Text="First Name" />
            </div>
            <div class="col-md-8">
              <RadzenTextBox style="width: 100%;" Name="FirstName" 
                @bind-Value="employee.FirstName" />
            </div>
          </div>
          <div class="row">
            <div class="col-md-4 align-items-center d-flex">
              <RadzenLabel Text="Last Name" />
            </div>
            <div class="col-md-8">
              <RadzenTextBox style="width: 100%;" Name="LastName" 
                @bind-Value="employee.LastName" />
            </div>
          </div>
          <div class="row">
            <div class="col-md-4 align-items-center d-flex">
              <RadzenLabel Text="Birth Date" />
            </div>
            <div class="col-md-8">
              <RadzenDatePicker style="width: 100%;" Name="BirthDate" 
                @bind-Value="employee.BirthDate" />
            </div>
          </div>
          <div class="row">
            <div class="col-md-4 align-items-center d-flex">
              <RadzenLabel Text="Hire Date" />
            </div>
            <div class="col-md-8">
              <RadzenDatePicker style="width: 100%;" Name="HireDate" 
                @bind-Value="employee.HireDate" />
            </div>
          </div>
          <div class="row">
            <div class="col-md-12">
              <RadzenTextArea style="width: 100%;" Name="Notes" 
                @bind-Value="employee.Notes" Rows="6" />
            </div>
          </div>
        </RadzenFieldset>
      </div>
      <div class="col-md-6">
        <RadzenFieldset Text="Home Address">
          <div class="row">
            <div class="col-md-4 align-items-center d-flex">
              <RadzenLabel Text="Country" />
            </div>
            <div class="col-md-8">
              <RadzenDropDown TValue="string" @bind-Value="employee.Country" 
                Placeholder="USA" Data="@countries" style="width: 100%;" 
                Name="Country">
              </RadzenDropDown>
            </div>
          </div>
          <div class="row">
            <div class="col-md-4 align-items-center d-flex">
              <RadzenLabel Text="City" />
            </div>
            <div class="col-md-8">
              <RadzenDropDown TValue="string" @bind-Value="employee.City" 
                Data="@cities" style="width: 100%;" Name="City">
              </RadzenDropDown>
            </div>
          </div>
          <div class="row">
            <div class="col-md-4 align-items-center d-flex">
              <RadzenLabel Text="Region" />
            </div>
            <div class="col-md-8">
              <RadzenTextBox style="width: 100%;" Name="Region" 
                @bind-Value="employee.Region" />
            </div>
          </div>
          <div class="row">
            <div class="col-md-4 align-items-center d-flex">
              <RadzenLabel Text="Postal Code" />
            </div>
            <div class="col-md-8">
              <RadzenTextBox style="width: 100%;" Name="PostalCode" 
                @bind-Value="employee.PostalCode" />
            </div>
          </div>
          <div class="row">
            <div class="col-md-4 align-items-center d-flex">
              <RadzenLabel Text="Building/Street" />
            </div>
            <div class="col-md-8">
              <RadzenTextBox style="width: 100%;" Name="Address" 
                @bind-Value="employee.Address" />
            </div>
          </div>
          <div class="row">
            <div class="col-md-4 align-items-center d-flex">
              <RadzenLabel Text="Home Phone" />
            </div>
            <div class="col-md-8">
              <RadzenTextBox style="width: 100%;" Name="HomePhone" 
                @bind-Value="employee.HomePhone" />
            </div>
          </div>
          <div class="row">
            <div class="col-md-4 align-items-center d-flex">
              <RadzenLabel Text="Picture" />
            </div>
            <div class="col-md-8">
              @if (employee.Photo is not null)
              {
                <div>
                  <img class="rounded float-start"
                   src="@employee.Photo.ConvertToBase64Jpeg()" />
                </div>
              }
            </div>
          </div>
        </RadzenFieldset>
      </div>
    </div>
    <div class="row justify-content-center">
      <div class="col-md-12 d-flex align-items-end justify-content-center" 
           style="margin-top: 16px;">
        <RadzenButton ButtonType="ButtonType.Submit" Icon="save" 
          Text="Save Changes" />
      </div>
    </div>
  </RadzenTemplateForm>
}

@code {
  private IQueryable<Employee>? employees;
  private string[]? countries;
  private string[]? cities;

  private Employee? employee = null;

  // Web API service uses "Preserve" so
  // we must control how references are handled.
  private JsonSerializerOptions jsonOptions = new()
    {
      ReferenceHandler = ReferenceHandler.Preserve,
      PropertyNameCaseInsensitive = true
    };

  protected override async Task OnParametersSetAsync()
  {
    Employee[]? employeesArray = null;

    HttpClient client = httpClientFactory.CreateClient(
      "Northwind.BlazorLibraries.ServerAPI");

    string path = "api/employees";

    try
    {
      employeesArray = (await client.GetFromJsonAsync<Employee[]?>(
        path, jsonOptions));

      employees = employeesArray?.AsQueryable();

      countries = (await client.GetFromJsonAsync<string[]?>(
        "api/countries"));

      cities = (await client.GetFromJsonAsync<string[]?>(
        "api/cities"));
    }
    catch (Exception ex)
    {
      Console.WriteLine($"{ex.GetType()}: {ex.Message}");
    }
  }

  void OnChange(object value, string name)
  {
    string? str = value is IEnumerable<object> ?
      string.Join(", ", (IEnumerable<object>)value) :
      value.ToString();

    Console.WriteLine($"{name} value changed to {str}");

    if (str != null)
    {
      employee = employees?.FirstOrDefault(employee =>
        employee.EmployeeId == int.Parse(str));
    }
  }

  private async void Submit(Employee employee)
  {
    HttpClient client = httpClientFactory.CreateClient(
      "Northwind.BlazorLibraries.ServerAPI");

    string path = $"api/employees/{employee.EmployeeId}";

    try
    {
      HttpResponseMessage response = await client
        .PutAsJsonAsync<Employee>(path, employee);

      NotificationMessage message = new()
        {
          Severity = response.StatusCode == HttpStatusCode.OK ? 
            NotificationSeverity.Success : NotificationSeverity.Error,
          Summary = $"{response.StatusCode}",
          Detail = $"Employees affected: {await response.Content.ReadAsStringAsync()}",
          Duration = 5000 // milliseconds
        };

      notificationService.Notify(message);
    }
    catch (Exception ex)
    {
      Console.WriteLine($"{ex.GetType()}: {ex.Message}");
    }
  }
}
Note the following:
•	The RadzenListBox component is bound to a list of employees. The text shown in the list is the FirstName. The value selected is the EmployeeId. When the list selection changes, the OnChange method is called.
•	If the employee property is not null, then a RadzenTemplateForm is bound to it. Within the template, two RadzenFieldset components (visual groups) are used to separate the left and right halves of the form, titled Employee Details and Home Address. 
•	RadzenLabel components are used to label each field within the form, like Employee ID.
•	A read-only RadzenNumeric component is bound to the EmployeeId property. We do not want the user changing the value used as the primary key for an employee.
•	A RadzenTextBox component is bound to the Title property. The same is used for the TitleOfCourtesy, FirstName, LastName, Region, PostalCode, Address, and HomePhone properties.
•	A RadzenDatePicker component is bound to the BirthDate property. The same is used for the HireDate property.
•	A RadzenTextArea component with six rows is bound to the Notes property.
•	A RadzenDropDown component is bound to the Country property. It binds to the countries property to supply the list of country text values. A similar one is used for the City property. It binds to the cities property to supply the list of city text values.
•	If the Photo property is not null, then an img element has its src set to the bytes of the Photo property converted into a Base64-encoded JPEG image.
•	At the bottom of the form is a RadzenButton labeled Save Changes.
•	In the @code block, note the employees, countries, cities, and employee fields that are bound to by various components.
•	When parameters for this component are set, an HttpClient is used to get all the employee entities and lists of cities and countries from the web service and this data is then stored in the local fields.
•	When the list of employees above the form changes, the OnChange method writes the selected employee name to the console output and then uses the selected employee ID to set the local employee field that is bound to the form.
•	When the Save Changes button is clicked, the form is submitted, and an HttpClient is used to send an HTTP PUT request to the web service to update the appropriate employee in the database with the current values bound to the employee field. A notification message appears for five seconds.
Testing the employees page component
Now, we can try out the interactions with the Employees page component:
1.	Start the Northwind.BlazorLibraries.Server project without debugging.
2.	Start Chrome and navigate to https://localhost:5161/.
3.	In the left navigation menu, click Employees, enter ne in the search box and note the list is filtered to only show the two employees with ne in their first name, Janet and Anne, as shown in Figure 16.14:
 
Figure 16.14: Filtering employees by first name
4.	Click Janet, and note that all her details are shown in a form below the list box, as shown in Figure 16.15:
 
Figure 16.15: Details for the employee named Janet
5.	Change some of the details for Janet and note that while the Blazor app is open, those changes remain in memory. However, if you were to close the browser tab or window, those changes would be lost.
6.	Click the Save Changes button and note the notification message to inform you that the changes were successfully saved to the database, as shown in Figure 16.16:
 
Figure 16.16: A successful update to an employee in the database
7.	Close Chrome and shut down the web server.
8.	Start the Northwind.BlazorLibraries.Server project without debugging.
9.	Start Chrome and navigate to https://localhost:5161/.
10.	In the left navigation menu, click Employees, search for Janet, and confirm that her details were saved correctly.
11.	Close Chrome and shut down the web server.
Practicing and exploring
Test your knowledge and understanding by answering some questions, getting some hands-on practice, and exploring this chapter's topics with deeper research.
Exercise 17.1 – Test your knowledge
Answer the following questions:
1.	Why is the Radzen Blazor component library a good choice compared to alternatives like DevExpress or SyncFusion?
2.	Does using the Radzen Blazer component library require your project to also use Bootstrap?
3.	Which four Radzen Blazer components require you to register dependency services?
4.	In a NotificationMessage, what does setting the Severity property to 1, 2, or 3 do?
5.	What is the name of the icon library that can be used by default to set icons for tabs and other components?
6.	How can you customize the formatting of data values in a RadzenChart?
7.	How do you two-way data bind a RadzenTextBox component to a property?
8.	What three properties should be set on a RadzenListBox or RadzenDropDown component to display the list of items to select from?
9.	How can you trigger a Submit event for a form?
10.	Which component provides visual grouping in a form?
Exercise 17.2 – Practice by exploring MudBlazor
Create a new solution and set of projects to explore the MudBlazor component library. You can follow the instructions to get started with it at the following link: https://mudblazor.com/getting-started/installation#manual-install.
MudBlazor has similar components to Radzen Blazor, so you should try completing all the tasks in this chapter using MudBlazor instead of Radzen Blazor to see the subtle differences and all the similarities.
Exercise 17.3 – Explore topics
Use the links on the following page to learn more detail about the topics covered in this chapter:
https://github.com/markjprice/apps-services-net8/blob/main/docs/book-links.md#chapter-17---leveraging-open-source-blazor-component-libraries

# Summary

In this chapter, you learned:
- How to install Radzen Blazor and enable its services for features like notifications.
- How to use tooltip and context menu components.
- How to use notification and dialog components.
- How to use tabs, images, and icon components.
- How to use HTML editor, chart, and form components.
