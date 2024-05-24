**Errata** (14 items)

If you find any mistakes, then please [raise an issue in this repository](https://github.com/markjprice/apps-services-net8/issues) or email me at markjprice (at) gmail.com.

- [Page 104 - Table-per-concrete-type (TPC) mapping strategy](#page-104---table-per-concrete-type-tpc-mapping-strategy)
- [Page 148 - Using a .NET app to create Azure Cosmos DB resources](#page-148---using-a-net-app-to-create-azure-cosmos-db-resources)
- [Page 166 - Implementing stored procedures](#page-166---implementing-stored-procedures)
- [Page 209 - Generating grayscale thumbnails](#page-209---generating-grayscale-thumbnails)
- [Page 249 - Creating a console app to generate PDF documents](#page-249---creating-a-console-app-to-generate-pdf-documents)
- [Page 328 - Configuring HTTP logging for the web service and Page 363 - Authenticating service clients using JWT bearer authentication](#page-328---configuring-http-logging-for-the-web-service-and-page-363---authenticating-service-clients-using-jwt-bearer-authentication)
- [Page 366 - Exercise 8.4 – Exposing data via the web using OData services](#page-366---exercise-84--exposing-data-via-the-web-using-odata-services)
- [Page 449 - Implementing a simple function](#page-449---implementing-a-simple-function)
- [Page 457 - Testing the Timer triggered function](#page-457---testing-the-timer-triggered-function)
- [Page 462 - Implementing a function that works with queues and BLOBs](#page-462---implementing-a-function-that-works-with-queues-and-blobs)
- [Page 595 - Getting request and response metadata](#page-595---getting-request-and-response-metadata)
- [Page 607 - Adding product and employee gRPC clients](#page-607---adding-product-and-employee-grpc-clients)
- [Page 644 - Comparing HTML Helpers and Tag Helpers](#page-644---comparing-html-helpers-and-tag-helpers)
- [Page 726 - Adding shell navigation and more content pages](#page-726---adding-shell-navigation-and-more-content-pages)

# Page 104 - Table-per-concrete-type (TPC) mapping strategy

> Thanks to [Jorge Morales](https://github.com/jmoralesv) for raising this [issue on December 12, 2023](https://github.com/markjprice/apps-services-net7/issues/22).

I show the SQL to define the two tables used in the TPC mapping strategy but it includes a foreign key constraint to a `People` table that does not exist, as shown in the following code:

```sql
CREATE TABLE [Students] (
  [Id] int NOT NULL DEFAULT (NEXT VALUE FOR [PersonIds]),
  [Name] nvarchar(max) NOT NULL,
  [Subject] nvarchar(max) NULL,
  CONSTRAINT [PK_Students] PRIMARY KEY ([Id])
  CONSTRAINT [FK_Students_People] FOREIGN KEY ([Id]) REFERENCES [People] ([Id])
);

CREATE TABLE [Employees] (
  [Id] int NOT NULL DEFAULT (NEXT VALUE FOR [PersonIds]),
  [Name] nvarchar(max) NOT NULL,
  [HireDate] nvarchar(max) NULL,
  CONSTRAINT [PK_Employees] PRIMARY KEY ([Id])
  CONSTRAINT [FK_Employees_People] FOREIGN KEY ([Id]) REFERENCES [People] ([Id])
);
```

I mistakenly copied some of the SQL from another strategy. In the next edition, I will correct the SQL, as shown in the following code:

```sql
CREATE TABLE [Employees] (
  [Id] int NOT NULL DEFAULT (NEXT VALUE FOR [PersonIds]),
  [Name] nvarchar(40) NOT NULL,
  [HireDate] datetime2 NOT NULL,
  CONSTRAINT [PK_Employees] PRIMARY KEY ([Id])
);

CREATE TABLE [Students] (
  [Id] int NOT NULL DEFAULT (NEXT VALUE FOR [PersonIds]),
  [Name] nvarchar(40) NOT NULL,
  [Subject] nvarchar(max) NULL,
  CONSTRAINT [PK_Students] PRIMARY KEY ([Id])
);
```

After the note saying, "Since there is not a single table with an IDENTITY column to assign Id values, we can use the (NEXT VALUE FOR [PersonIds]) command to define a sequence shared between the two tables so they do not assign the same Id values." I will show the SQL to define the sequence, as shown in the following code:

```sql
CREATE SEQUENCE [PersonIds] AS int START WITH 4 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
```

# Page 148 - Using a .NET app to create Azure Cosmos DB resources

> Thanks to **mdj._** in the Discord channel for raising this issue.

In Step 6, you "define a method for the `Program` class that creates a Cosmos client and uses it to create a database named `Northwind` and a container named `Products`". 

But when checking for a bad response from the Cosmos DB service when creating a container, I mistakenly copied code from earlier that checked creating the database. 

The incorrect code in the book that creates and checks the container is as follows:
```cs
ContainerResponse containerResponse = await dbResponse.Database
  .CreateContainerIfNotExistsAsync(
    containerProperties, throughput: 1000 /* RU/s */);

status = dbResponse.StatusCode switch
{
  HttpStatusCode.OK => "exists",
  HttpStatusCode.Created => "created",
  _ => "unknown",
};

WriteLine("Container Id: {0}, Status: {1}.",
  arg0: containerResponse.Container.Id, arg1: status);
```

But `status = dbResponse.StatusCode switch` should be `status = containerResponse.StatusCode switch`.

So the corrected code in the book would be as follows:
```cs
ContainerResponse containerResponse = await dbResponse.Database
  .CreateContainerIfNotExistsAsync(
    containerProperties, throughput: 1000 /* RU/s */);

status = containerResponse.StatusCode switch
{
  HttpStatusCode.OK => "exists",
  HttpStatusCode.Created => "created",
  _ => "unknown",
};

WriteLine("Container Id: {0}, Status: {1}.",
  arg0: containerResponse.Container.Id, arg1: status);
```

> This issue has been fixed in the code in this repository here: https://github.com/markjprice/apps-services-net8/blob/main/code/Chapter04/Northwind.CosmosDb.SqlApi/Program.Methods.cs#L68C18-L68C35.

# Page 166 - Implementing stored procedures

> Thanks to [Tomasz](https://github.com/sikora507) for raising this [issue on January 21, 2024](https://github.com/markjprice/apps-services-net8/issues/3).

I wrote, "Stored procedures are the only way to ensure ACID (Atomic, Consistent, Isolated, Durable) transactions 
that combine multiple discrete activities into a single action that can be committed or rolled back.
You cannot use client-side code to implement transactions."

But there is another way if you are making multiple changes with the same partition key in a container by using a transactional batch.

In the next edition, I will write, "Stored procedures are the only way to ensure ACID (Atomic, Consistent, Isolated, Durable) transactions 
that combine multiple discrete activities into a single action that can be committed or rolled back across more than a single partition.
You can use client-side code to implement transactions with the same partition key in a container by using a transactional batch, as described 
at the following link: https://learn.microsoft.com/en-us/azure/cosmos-db/nosql/transactional-batch."

# Page 209 - Generating grayscale thumbnails

> Thanks to **Servant of Time** aka `qkxl` in the Discord channel for this book for raising this issue.

In Step 4, I tell the reader to add a package reference for SixLabors.ImageSharp, as shown in the following markup:
```xml
<ItemGroup>
  <PackageRefrence Include="SixLabors.ImageSharp" Version="3.0.2" />
</ItemGroup>
```

If you use this version of the package then it uses the implicit namespace import features of the .NET SDK to add statements to the autogenerated file, as described in Step 7.

However, if you use the latest version `3.1.4` then they have disabled this feature! I do not know which specific version they made this change but I'm guessing version `3.1.0`. It doesn't really matter. 

To get the same effect, in the `WorkingWithImages.csproj` project file, add elements to import the namespaces, as shown in the following markup:
```xml
<ItemGroup>
  <PackageReference Include="SixLabors.ImageSharp" Version="3.1.4" />
</ItemGroup>

<ItemGroup>
  <Using Include="SixLabors.ImageSharp" />
  <Using Include="SixLabors.ImageSharp.PixelFormats" />
  <Using Include="SixLabors.ImageSharp.Processing" />
</ItemGroup>
```

> It is bad practice for ImageSharp to introduce a breaking change like this in a non-major version number.

# Page 249 - Creating a console app to generate PDF documents

> Thanks to [Phil Edmunds](https://github.com/Pip1987) for raising this [issue on February 8, 2024](https://github.com/markjprice/apps-services-net8/issues/5).

In Step 6, I show the code to generate a PDF file but it includes a statement to set the license type. The `License` property and `enum` type are only available with package versions `2023.4.0` or later. If you use the `2022.12-*` or earlier versions with MIT license then you cannot set the license. 

The following statement can therefore be commented out:
```cs
QuestPDF.Settings.License = LicenseType.Community;
```

I have commented out this statement in the GitHub solution since the project references the older packages with MIT license. If you choose to use a later version of the package, then you must uncomment the statement.

In the next edition, I will add a note and code comments to explain this. 

# Page 328 - Configuring HTTP logging for the web service and Page 363 - Authenticating service clients using JWT bearer authentication

> Thanks to [Phil Edmunds](https://github.com/Pip1987) for raising this issue on [February 14, 2024](https://github.com/markjprice/apps-services-net8/issues/6).

In Step 4, I wrote, "In the `Northwind.WebApi.Service` project folder, at the command prompt or terminal, create
a local JWT, as shown in the following command:"
```
dotnet user-jwts create
```

You will see an error, `'/' is an invalid start of a property name. Expected a '"'. Path: $ | LineNumber: 6 | BytePositionInLine: 6.` if any of the following files contain comment characters: `launchSettings.json`, `appSettings.json`, or `appSettings.<environment>.json`. 

In the next edition, I will add a warning about this on page 363, and on page 328 I will NOT tell the reader to add a comment in the `appsettings.Development.json` file, as shown in the following markup:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",

      // To enable logging HTTP requests, this must be
      // set to Information (3) or higher.
      "Microsoft.AspNetCore.HttpLogging": "Information"
    }
  }
}
```

# Page 366 - Exercise 8.4 – Exposing data via the web using OData services

> Thanks to [Phil Edmunds](https://github.com/Pip1987) for raising this issue on [February 16, 2024](https://github.com/markjprice/apps-services-net8/issues/7) and [here](https://github.com/markjprice/apps-services-net8/issues/8).

The online section was written when Visual Studio 2022 did not have an HTTP Editor so the `.http` files were not tested with it. Instead, the section assumes you will use Visual Studio Code extension **REST Client**. But now that readers can use Visual Studio, readers should be warned that it has limitations that mean the `.http` files do not work.

In particular, multi-line requests and requests containing spaces will give errors.

For example, the following request that works with REST Client:
```
GET {{base_address}}categories/
  ?$select=CategoryId,CategoryName
```
Must be written all on one line with Visual Studio HTTP Editor:
```
GET {{base_address}}categories/?$select=CategoryId,CategoryName
```

For another example, the following request containing spaces that works with REST Client:
```
GET {{base_address}}products/?$filter=startswith(ProductName,'Ch') or (UnitPrice gt 50)
```
Must be written using %20 instead where there must be a space character with Visual Studio HTTP Editor:
```
GET {{base_address}}products/?$filter=startswith(ProductName,'Ch')%20or%20(UnitPrice%20gt%2050)
```

> Syntax not supported by Visual Studio's HTTP Editor can be found at the following link: https://learn.microsoft.com/en-us/aspnet/core/test/http-files?#unsupported-syntax.

# Page 449 - Implementing a simple function

> Thanks to [Phil Edmunds](https://github.com/Pip1987) for raising this issue on [February 19, 2024](https://github.com/markjprice/apps-services-net8/issues/10) and to [DocVD](https://github.com/DocVD) for confirming that they experienced the same issue.

If you get the following runtime exception:
```
[2024-02-19T22:04:57.896Z] Function 'NumbersToWordsFunction', Invocation id '1c7e2c25-32c6-4027-b30b-f29216597cfa': An exception was thrown by the invocation.
[2024-02-19T22:04:57.897Z] Result: Function 'NumbersToWordsFunction', Invocation id '1c7e2c25-32c6-4027-b30b-f29216597cfa': An exception was thrown by the invocation.
Exception: System.InvalidOperationException: Synchronous operations are disallowed. Call WriteAsync or set AllowSynchronousIO to true instead.
```

Then you can add the following statements to `Program.cs` to fix it:
```cs
services.Configure<KestrelServerOptions>(options =>
  {
      options.AllowSynchronousIO = true;
  });
```

# Page 457 - Testing the Timer triggered function

> Thanks to [Phil Edmunds](https://github.com/Pip1987) for raising this issue on [February 16, 2024](https://github.com/markjprice/apps-services-net8/issues/11).

In Step 4, I tell the reader to write some HTTP requests to trigger the functions. If you use the Visual Studio Code extension **REST Client** then they work. But if you use Visual Studio's **HTTP Editor** then each HTTP request must include an action like `GET`, `POST` and so on. 

In the next edition, I will add `GET` actions to the requests, as shown in the following code:
```
### Get information about the NumbersToWordsFunction function.
GET {{base_address}}admin/functions/NumbersToWordsFunction

### Get information about the ScrapeAmazonFunction function.
GET {{base_address}}admin/functions/ScrapeAmazonFunction
```

The solution files in GitHub were already correct. For example: https://github.com/markjprice/apps-services-net8/blob/main/scripts/http-requests/azurefunctions-scrapeamazon.http

# Page 462 - Implementing a function that works with queues and BLOBs

> Thanks to [Phil Edmunds](https://github.com/Pip1987) for raising this issue on [February 20, 2024](https://github.com/markjprice/apps-services-net8/issues/12).

In Step 3, I tell the reader to reference two `ImageSharp` packages, as shown in the following markup:
```xml
<PackageReference Include="SixLabors.ImageSharp" Version="3.0.2" />
<PackageReference Include="SixLabors.ImageSharp.Drawing" Version="2.0.0" />
```

If you reference exactly the same package versions as shown in the book, then the instructions given in the book work. For example, version `3.0.2` automatically imports relevant namespaces that are used later in the file `Northwind.AzureFunctions.Service.GlobalUsings.g.cs` that is found in `obj\Debug\net8.0`, as shown in the following code:
```cs
// <auto-generated/>
global using global::SixLabors.ImageSharp;
global using global::SixLabors.ImageSharp.PixelFormats;
global using global::SixLabors.ImageSharp.Processing;
global using global::System;
global using global::System.Collections.Generic;
global using global::System.IO;
global using global::System.Linq;
global using global::System.Net.Http;
global using global::System.Threading;
global using global::System.Threading.Tasks;
global using ExecutionContext = global::System.Threading.ExecutionContext;
```

But if you reference a later version of the packages, then ImageSharp has made some breaking changes, like NOT automatically importing those namespaces.

To use the latest packages, in Step 3, reference the two latest `ImageSharp` packages (last checked on March 29, 2024), as shown in the following markup:
```xml
<PackageReference Include="SixLabors.ImageSharp" Version="3.1.3" />
<PackageReference Include="SixLabors.ImageSharp.Drawing" Version="2.1.2" />
```

> You can use the following link to check for later versions: https://www.nuget.org/packages?q=SixLabors.ImageSharp. But note more regressions could be introduced.

In Step 9, add additional statements to import some needed namespaces for `ImageSharp`, as shown in the following code:
```cs
using SixLabors.ImageSharp; // To use Image.
using SixLabors.ImageSharp.PixelFormats; // To use Rgba32.
using SixLabors.ImageSharp.Processing; // To use Mutate.
```

Later in the print book code in the `Run` method for the `CheckGeneratorFunction`, the variable for the logger was incorrectly written as `log`, as shown in the following code:
```cs
log.LogInformation($"Blobs folder: {folder}");
```
The variable name should be `_logger`, as shown in the following code:
```cs
_logger.LogInformation($"Blobs folder: {folder}");
```

The same issue occurred in the `catch` at the end of the method. The variable for the logger was incorrectly written as `log`, as shown in the following code:
```cs
log.LogError(ex.Message);
```

The variable name should be `_logger`, as shown in the following code:
```cs
_logger.LogError(ex.Message);
```

The code in the GitHub repository was already correct.

# Page 595 - Getting request and response metadata

> Thanks to [Phil Edmunds](https://github.com/Pip1987) for raising this issue on [March 13, 2024](https://github.com/markjprice/apps-services-net8/issues/14).

In Step 2, the code includes a statement written in an early task that stores the shipper summary in `ViewData`, as shown in the following code:
```cs
ViewData["shipper"] = "Shipper from gRPC service: " +
  $"ID: {shipperReply.ShipperId}, Name: {shipperReply.CompanyName},"
  + $" Phone: {shipperReply.Phone}.";
```

But that code should use the `ShipperSummary` property of the view model, as shown in the following code:
```cs
model.ShipperSummary = "Shipper from gRPC service: " +
  $"ID: {shipperReply.ShipperId}, Name: {shipperReply.CompanyName},"
  + $" Phone: {shipperReply.Phone}.";
```

Since that statement was already written and was only included to show that the new statements should be inserted before it, and this task does not change that statement, no reader should have been affected.

# Page 607 - Adding product and employee gRPC clients

> Thanks to [Phil Edmunds](https://github.com/Pip1987) for raising this issue on [March 20, 2024](https://github.com/markjprice/apps-services-net8/issues/15).

In Step 10, I wrote, "In the `Views\Home` folder, add a new Razor View file named `Products.cshtml`, and modify it to show a table of products." The view renders a table of products. Above the table should be a simple form to allow the visitor to enter a minimum unit price to filter the products. But in the book, this form is missing from the markup. 

Between the `<h1>` and the `<table>`, the markup in the book should have included the form, as shown in the following markup:
```html
<h1>@ViewData["Title"]</h1>
<form asp-action="Products" method="post">
  <input name="minimumPrice" placeholder="Enter a minimum unit price" />
  <input type="submit" value="Filter Products" />
</form>
<table class="table table-primary table-bordered">
```

> The file was already correct in the GitHub repository, as shown at the following link: https://github.com/markjprice/apps-services-net8/blob/main/code/Chapter13/Northwind.Grpc.Client.Mvc/Views/Home/Products.cshtml

# Page 644 - Comparing HTML Helpers and Tag Helpers

> Thanks to [Stephen Harper](https://github.com/sjharper79) for raising this issue on [January 3, 2024](https://github.com/markjprice/apps-services-net7/issues/27).

I show three statements that should produce the following markup:
```html
<a href="/home/privacy">View our privacy policy.</a>
```

But the first two statements use `Index` instead of `Home`. They should be as follows:
```
@Html.ActionLink("View our privacy policy.", "Privacy", "Home")

@Html.ActionLink(linkText: "View our privacy policy.",
  action: "Privacy", controller: "Home")
```

This will be fixed in the third edition.

# Page 726 - Adding shell navigation and more content pages

> Thanks to [Stephen Harper](https://github.com/sjharper79) for raising this [issue on 8 January 2024](https://github.com/markjprice/apps-services-net7/issues/29).

In Step 20, the method name should be `ClickMeButton_Clicked`.
