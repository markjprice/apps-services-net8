**Errata** (7 items)

If you find any mistakes, then please [raise an issue in this repository](https://github.com/markjprice/apps-services-net8/issues) or email me at markjprice (at) gmail.com.

- [Page 104 - Table-per-concrete-type (TPC) mapping strategy](#page-104---table-per-concrete-type-tpc-mapping-strategy)
- [Page 166 - Implementing stored procedures](#page-166---implementing-stored-procedures)
- [Page 249 - Creating a console app to generate PDF documents](#page-249---creating-a-console-app-to-generate-pdf-documents)
- [Page 328 - Configuring HTTP logging for the web service and Page 363 - Authenticating service clients using JWT bearer authentication](#page-328---configuring-http-logging-for-the-web-service-and-page-363---authenticating-service-clients-using-jwt-bearer-authentication)
- [Page 366 - Exercise 8.4 – Exposing data via the web using OData services](#page-366---exercise-84--exposing-data-via-the-web-using-odata-services)
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
