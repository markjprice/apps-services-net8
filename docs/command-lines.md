**Command-Lines**

To make it easier to enter commands at the prompt, this page lists all commands as a single line that can be copied and pasted. 

- [Chapter 1 - Introducing Apps and Services with .NET](#chapter-1---introducing-apps-and-services-with-net)
  - [Page ? - Managing Visual Studio Code extensions at the command prompt](#page----managing-visual-studio-code-extensions-at-the-command-prompt)
  - [Page ? - Using other project templates](#page----using-other-project-templates)
  - [Page ? - Getting help for the dotnet tool](#page----getting-help-for-the-dotnet-tool)
- [Chapter 3 - Building Entity Models for SQL Server Using EF Core](#chapter-3---building-entity-models-for-sql-server-using-ef-core)
  - [Page ? - Setting up the dotnet-ef tool](#page----setting-up-the-dotnet-ef-tool)
  - [Page ? - Defining the Northwind database model](#page----defining-the-northwind-database-model)

# Chapter 1 - Introducing Apps and Services with .NET

## Page ? - Managing Visual Studio Code extensions at the command prompt

```
code --list-extensions
```

```
code --install-extension ms-dotnettools.csharp
```

## Page ? - Using other project templates

Listing all installed project templates:
```
dotnet new list
```

Installing a new project template:
```
dotnet new --install "Vue.Simple.Template"
```

## Page ? - Getting help for the dotnet tool

Getting help for a `dotnet` command like `build` in a web browser:
```
dotnet help build
```

Getting help for a `dotnet` command like `build` at the command prompt:
```
dotnet build -?
```

# Chapter 3 - Building Entity Models for SQL Server Using EF Core

## Page ? - Setting up the dotnet-ef tool

To check if you have already installed `dotnet-ef` as a global tool:
```
dotnet tool list --global
```

To uninstall an existing global tool:
```
dotnet tool uninstall --global dotnet-ef
```

To install the latest version as a global tool:
```
dotnet tool install --global dotnet-ef
```

To install the latest preview version 8 as a global tool:
```
dotnet tool install --global dotnet-ef --version 8-*
```

## Page ? - Defining the Northwind database model

Generate an entity model (from a local SQL Server):
```
dotnet ef dbcontext scaffold "Data Source=.;Initial Catalog=Northwind;Integrated Security=true;TrustServerCertificate=true;" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models --namespace Northwind.Models --data-annotations --context NorthwindDb
```

**Common connection strings**

Local SQL Server (default instance):
```
"Data Source=.;Initial Catalog=Northwind;Integrated Security=true;TrustServerCertificate=true;"
```

Local SQL Server (replace `<your_instance_name>` with named instance):
```
"Data Source=.\<your_instance_name>;Initial Catalog=Northwind;Integrated Security=true;TrustServerCertificate=true;"
```

Azure SQL Database (replace `<your_server_name>`, `<your_user_name>`, and `<your_password>`):
```
"Data Source=tcp:<your_server_name>.database.windows.net,1433;Initial Catalog=Northwind;User ID=<your_user_name>;Password=<your_password>;Persist Security Info=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;"
```
