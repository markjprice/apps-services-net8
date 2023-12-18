**Command-Lines**

To make it easier to enter commands at the prompt, this page lists all commands as a single line that can be copied and pasted. 

- [Chapter 1 - Introducing Apps and Services with .NET](#chapter-1---introducing-apps-and-services-with-net)
  - [Page 20 - Managing Visual Studio Code extensions at the command prompt](#page-20---managing-visual-studio-code-extensions-at-the-command-prompt)
  - [Page 22 - Using other project templates](#page-22---using-other-project-templates)
  - [Page 29 - Getting help for the dotnet tool](#page-29---getting-help-for-the-dotnet-tool)
- [Chapter 3 - Building Entity Models for SQL Server Using EF Core](#chapter-3---building-entity-models-for-sql-server-using-ef-core)
  - [Page 85 - Setting up the dotnet-ef tool](#page-85---setting-up-the-dotnet-ef-tool)
  - [Page 90 - Defining the Northwind database model](#page-90---defining-the-northwind-database-model)
  - [Page 117 - Creating a class library for entity models using SQL Server](#page-117---creating-a-class-library-for-entity-models-using-sql-server)
  - [Page 126 - Running unit tests using Visual Studio Code](#page-126---running-unit-tests-using-visual-studio-code)
- [Chapter 8 - Building and Securing Web Services Using Minimal APIs](#chapter-8---building-and-securing-web-services-using-minimal-apis)
  - [Page 363 - Authenticating service clients using JWT bearer authentication](#page-363---authenticating-service-clients-using-jwt-bearer-authentication)
- [Chapter 9 - Caching, Queuing, and Resilient Background Services](#chapter-9---caching-queuing-and-resilient-background-services)
  - [Page 402 - Setting up RabbitMQ using Docker](#page-402---setting-up-rabbitmq-using-docker)
  - [Page 419 - Processing queued message using a worker service](#page-419---processing-queued-message-using-a-worker-service)
- [Chapter 10 - Building Serverless Nanoservices Using Azure Functions](#chapter-10---building-serverless-nanoservices-using-azure-functions)
  - [Page 444 - Testing locally with Azurite](#page-444---testing-locally-with-azurite)
  - [Page 447 - Using the func CLI](#page-447---using-the-func-cli)
- [Chapter 11 - Broadcasting Real-Time Communication Using SignalR](#chapter-11---broadcasting-real-time-communication-using-signalr)
  - [Page 484 - Building a web client using the SignalR JavaScript library](#page-484---building-a-web-client-using-the-signalr-javascript-library)
- [Chapter 12 - Combining Data Sources Using GraphQL](#chapter-12---combining-data-sources-using-graphql)
  - [Page 544 - Creating a console app client using Strawberry Shake](#page-544---creating-a-console-app-client-using-strawberry-shake)
- [Chapter 13 - Building Efficient Microservices Using gRPC](#chapter-13---building-efficient-microservices-using-grpc)
  - [Page 594 - Improving a gRPC service with native AOT publish](#page-594---improving-a-grpc-service-with-native-aot-publish)
- [Chapter 14 - Building Web User Interfaces Using ASP.NET Core](#chapter-14---building-web-user-interfaces-using-aspnet-core)
  - [Page 623 - Creating an ASP.NET Core MVC website](#page-623---creating-an-aspnet-core-mvc-website)
- [Chapter 15 - Building Web Components Using Blazor](#chapter-15---building-web-components-using-blazor)
  - [Page 672 - Reviewing the new Blazor project template](#page-672---reviewing-the-new-blazor-project-template)
- [Chapter 16 - Building Mobile and Desktop Apps Using .NET MAUI](#chapter-16---building-mobile-and-desktop-apps-using-net-maui)
- [Page 710 - Installing .NET MAUI workloads manually](#page-710---installing-net-maui-workloads-manually)
  - [Page 723 - Adding shell navigation and more content pages](#page-723---adding-shell-navigation-and-more-content-pages)
  - [Page 729 - Defining resources to share across an app](#page-729---defining-resources-to-share-across-an-app)

# Chapter 1 - Introducing Apps and Services with .NET

## Page 20 - Managing Visual Studio Code extensions at the command prompt

```
code --list-extensions
```

```
code --install-extension ms-dotnettools.csdevkit
```

## Page 22 - Using other project templates

Listing all installed project templates:
```
dotnet new list
```

Installing a new project template:
```
dotnet new --install "Vue.Simple.Template"
```

## Page 29 - Getting help for the dotnet tool

Getting help for a `dotnet` command like `build` in a web browser:
```
dotnet help build
```

Getting help for a `dotnet` command like `build` at the command prompt:
```
dotnet build -?
```

# Chapter 3 - Building Entity Models for SQL Server Using EF Core

## Page 85 - Setting up the dotnet-ef tool

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

To install the latest preview version 9 as a global tool:
```
dotnet tool install --global dotnet-ef --version 9-*
```

## Page 90 - Defining the Northwind database model

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

## Page 117 - Creating a class library for entity models using SQL Server

```
dotnet ef dbcontext scaffold "Data Source=.;Initial Catalog=Northwind;Integrated Security=true;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer --namespace Northwind.EntityModels --data-annotations
```

## Page 126 - Running unit tests using Visual Studio Code

```
dotnet test
```

# Chapter 8 - Building and Securing Web Services Using Minimal APIs

## Page 363 - Authenticating service clients using JWT bearer authentication

Create a local JWT, as shown in the following command:
```
dotnet user-jwts create
```

Print all the information for the ID that was assigned, as shown in the following command:
```
dotnet user-jwts print d7e22000 --show-all
```

# Chapter 9 - Caching, Queuing, and Resilient Background Services

## Page 402 - Setting up RabbitMQ using Docker

Pull down the latest container image for RabbitMQ on Docker and run it, opening ports 5672 and 15672 to the container, which are used by default by AMQP, as shown in the following command:
```
docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.12-management
```

## Page 419 - Processing queued message using a worker service

Start the RabbitMQ container, as shown in the following command:
```
docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.12-management
```

# Chapter 10 - Building Serverless Nanoservices Using Azure Functions

## Page 444 - Testing locally with Azurite

To install Azurite at the command prompt:
```
npm install -g azurite
```

## Page 447 - Using the func CLI

Create a new Azure Functions project using C#, as shown in the following command:
```
func init --csharp
```

Create a new Azure Functions function using HTTP trigger that can be called anonymously, as shown in the following command:
```
func new --name NumbersToWordsFunction --template "HTTP trigger" --authlevel "anonymous"
```

Start the function locally, as shown in the following command:
```
func start
```

# Chapter 11 - Broadcasting Real-Time Communication Using SignalR

## Page 484 - Building a web client using the SignalR JavaScript library

Install the Library Manager CLI tool, as shown in the following command:
```
dotnet tool install -g Microsoft.Web.LibraryManager.Cli
```

Add the signalr.js and signalr.min.js libraries to the project from the unpkg source, as shown in the following command:
```
libman install @microsoft/signalr@latest -p unpkg -d wwwroot/js/signalr --files dist/browser/signalr.js --files dist/browser/signalr.min.js
```

# Chapter 12 - Combining Data Sources Using GraphQL

## Page 544 - Creating a console app client using Strawberry Shake

Create a tools manifest file, as shown in the following command:
```
dotnet new tool-manifest
```

Install Strawberry Shake tools for the local project, as shown in the following command:
```
dotnet tool install StrawberryShake.Tools --local
```

Add a client for your GraphQL service, as shown in the following command:
```
dotnet graphql init https://localhost:5121/graphql/ -n NorthwindClient
```

# Chapter 13 - Building Efficient Microservices Using gRPC

## Page 594 - Improving a gRPC service with native AOT publish

Run `Northwind.Grpc.Service.exe` and explicitly specify the URL with the port number to use, as shown in the following command:
```
Northwind.Grpc.Service.exe --urls "https://localhost:5131"
```

# Chapter 14 - Building Web User Interfaces Using ASP.NET Core

## Page 623 - Creating an ASP.NET Core MVC website

Run database migrations so that the database used to store credentials for authentication is created, as shown in the following command:
```
dotnet ef database update
```

# Chapter 15 - Building Web Components Using Blazor

## Page 672 - Reviewing the new Blazor project template

Create a new project using the Blazor Web App project template:
```
dotnet new blazor --interactivity None -o Northwind.Blazor
```

# Chapter 16 - Building Mobile and Desktop Apps Using .NET MAUI

# Page 710 - Installing .NET MAUI workloads manually

To see which workloads are currently installed, enter the following command:
```
dotnet workload list
```

To see which workloads are available to install, enter the following command:
```
dotnet workload search
```

To install the .NET MAUI workloads for all platforms, enter the following command at the command line or terminal:
```
dotnet workload install maui
```

To update all existing workload installations, enter the following command:
```
dotnet workload update
```

To add missing workload installations required for a project, in the folder containing the project file,
enter the following command:
```
dotnet workload restore <projectname>
```

To remove leftover and unneeded workloads, as shown in the following command:
```
dotnet workload clean
```

## Page 723 - Adding shell navigation and more content pages

Create a **.NET MAUI ContentPage (XAML)** item using the CLI:
```
dotnet new maui-page-xaml --name SettingsPage.xaml
```

## Page 729 - Defining resources to share across an app

Create a **.NET MAUI Resource Dictionary** item using the CLI:
```
dotnet new maui-dict-xaml --name Northwind.xaml
```

