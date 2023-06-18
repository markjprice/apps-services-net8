**Book Links**

- [Chapter 1 - Introducing Apps and Services with .NET](#chapter-1---introducing-apps-and-services-with-net)
  - [Visual Studio 2022 for Windows and Mac](#visual-studio-2022-for-windows-and-mac)
  - [Visual Studio Code](#visual-studio-code)
  - [Other C# code editors and platforms](#other-c-code-editors-and-platforms)
  - [Command-Line Interfaces](#command-line-interfaces)
  - [.NET](#net)
  - [Azure and .NET](#azure-and-net)
  - [Help and learning](#help-and-learning)
- [Chapter 1A - What's New in Modern C# and .NET](#chapter-1a---whats-new-in-modern-c-and-net)
  - [C# language version and the journey to C# 11](#c-language-version-and-the-journey-to-c-11)
  - [C# implemented proposals](#c-implemented-proposals)
- [Chapter 1B - Benchmarking Performance and Testing](#chapter-1b---benchmarking-performance-and-testing)
- [Chapter 1C - Observing and Modifying Code Execution Dynamically](#chapter-1c---observing-and-modifying-code-execution-dynamically)
  - [Assemblies and reflection](#assemblies-and-reflection)
  - [Expression Trees](#expression-trees)
  - [Source generators](#source-generators)
- [Chapter 2 - Managing Relational Data Using SQL Server](#chapter-2---managing-relational-data-using-sql-server)
  - [Microsoft SQL Server](#microsoft-sql-server)
  - [Dapper and comparing to EF Core performance](#dapper-and-comparing-to-ef-core-performance)
- [Chapter 3 - Building Entity Models for SQL Server Using EF Core](#chapter-3---building-entity-models-for-sql-server-using-ef-core)
  - [EF Core](#ef-core)
  - [EF Core database providers](#ef-core-database-providers)
  - [EF Core models](#ef-core-models)
  - [EF Core querying and manipulating](#ef-core-querying-and-manipulating)
- [Chapter 4 - Managing NoSQL Data Using Azure Cosmos DB](#chapter-4---managing-nosql-data-using-azure-cosmos-db)
  - [NoSQL data stores](#nosql-data-stores)
  - [Azure Cosmos DB](#azure-cosmos-db)
- [Chapter 5 - Multitasking and Concurrency](#chapter-5---multitasking-and-concurrency)
- [Chapter 6 - Implementing Popular Third-Party Libraries](#chapter-6---implementing-popular-third-party-libraries)
- [Chapter 7 - Handling Dates, Times, and Internationalization](#chapter-7---handling-dates-times-and-internationalization)
- [Chapter 8 - Building and Securing Web Services Using Minimal APIs](#chapter-8---building-and-securing-web-services-using-minimal-apis)
  - [Web service technologies and design](#web-service-technologies-and-design)
  - [Web service routing](#web-service-routing)
  - [Web service clients](#web-service-clients)
  - [Documenting web services](#documenting-web-services)
  - [Securing web services](#securing-web-services)
  - [Exposing Data via the Web Using OData](#exposing-data-via-the-web-using-odata)
- [Chapter 9 - Caching, Queuing, and Resilient Background Services](#chapter-9---caching-queuing-and-resilient-background-services)
  - [Understanding service architecture](#understanding-service-architecture)
  - [Caching with ASP.NET Core and Redis](#caching-with-aspnet-core-and-redis)
  - [Resilience with Polly](#resilience-with-polly)
  - [Queueing with RabbitMQ, Azure Service Bus, and Event Hub](#queueing-with-rabbitmq-azure-service-bus-and-event-hub)
  - [Background services](#background-services)
  - [Health checks and reliable web services](#health-checks-and-reliable-web-services)
- [Chapter 10 - Building Serverless Nanoservices Using Azure Functions](#chapter-10---building-serverless-nanoservices-using-azure-functions)
- [Chapter 11 - Broadcasting Real-Time Communication Using SignalR](#chapter-11---broadcasting-real-time-communication-using-signalr)
- [Chapter 12 - Combining Data Sources Using GraphQL](#chapter-12---combining-data-sources-using-graphql)
- [Chapter 13 - Building Efficient Microservices Using gRPC](#chapter-13---building-efficient-microservices-using-grpc)
- [Chapter 14 - Building Web User Interfaces Using ASP.NET Core](#chapter-14---building-web-user-interfaces-using-aspnet-core)
- [Chapter 15 - Building Web Components Using Blazor](#chapter-15---building-web-components-using-blazor)
  - [Blazor hosting models](#blazor-hosting-models)
  - [Blazor components](#blazor-components)
  - [Advanced techniques](#advanced-techniques)
- [Chapter 16 - Leveraging Open-Source Blazor Component Libraries](#chapter-16---leveraging-open-source-blazor-component-libraries)
- [Chapter 17 - Building Mobile and Desktop Apps Using .NET MAUI](#chapter-17---building-mobile-and-desktop-apps-using-net-maui)
- [Chapter 18 - Implementing Model-View-ViewModel for .NET MAUI](#chapter-18---implementing-model-view-viewmodel-for-net-maui)
- [Chapter 19 - Integrating .NET MAUI Apps with Blazor and Native Platforms](#chapter-19---integrating-net-maui-apps-with-blazor-and-native-platforms)
- [Chapter 20 - Introducing the Survey Project Challenge](#chapter-20---introducing-the-survey-project-challenge)
- [Epilogue](#epilogue)
  - [Next steps on your C# and .NET learning journey](#next-steps-on-your-c-and-net-learning-journey)
  - [Alternatives to using Azure resources](#alternatives-to-using-azure-resources)
  - [Learn from other Packt books](#learn-from-other-packt-books)

# Chapter 1 - Introducing Apps and Services with .NET

## Visual Studio 2022 for Windows and Mac
- [Download Visual Studio for Windows](https://visualstudio.microsoft.com/downloads/)
- [Visual Studio for Windows documentation](https://learn.microsoft.com/en-us/visualstudio/windows/)
- [MSBuild and 64-bit Visual Studio 2022](https://devblogs.microsoft.com/dotnet/msbuild-and-64-bit-visual-studio-2022/)
- [Visual Studio 2022 for Mac is now available](https://devblogs.microsoft.com/visualstudio/visual-studio-2022-for-mac-is-now-available/)
- [Visual Studio for Mac documentation](https://learn.microsoft.com/en-us/visualstudio/mac/)

## Visual Studio Code
- [Download Visual Studio Code](https://code.visualstudio.com/)
- [Set up Visual Studio Code](https://code.visualstudio.com/docs/setup/setup-overview)
- [Visual Studio Code user interface](https://code.visualstudio.com/docs/getstarted/userinterface)
- [Visual Studio Code support for C#](https://code.visualstudio.com/docs/languages/csharp)
- [Visual Studio Code key bindings and shortcuts](https://code.visualstudio.com/docs/getstarted/keybindings)
  - [Windows keyboard shortcuts PDF](https://code.visualstudio.com/shortcuts/keyboard-shortcuts-windows.pdf)
  - [macOS keyboard shortcuts PDF](https://code.visualstudio.com/shortcuts/keyboard-shortcuts-macos.pdf)
  - [Linux keyboard shortcuts PDF](https://code.visualstudio.com/shortcuts/keyboard-shortcuts-linux.pdf)

## Other C# code editors and platforms
- [Stack Overflow survey 2021 - Integrated development environment](https://insights.stackoverflow.com/survey/2021#section-most-popular-technologies-integrated-development-environment)
- [Stack Overflow survey 2019 - Most Popular Development Environments](https://insights.stackoverflow.com/survey/2019#development-environments-and-tools)
- [GitHub Codespaces](https://docs.github.com/en/codespaces/overview)
- [JetBrains Rider](https://www.jetbrains.com/rider/)
- [Rider documentation](https://www.jetbrains.com/help/rider/Introduction.html)

## Command-Line Interfaces
- [Windows Terminal as your Default Command Line Experience](https://devblogs.microsoft.com/commandline/windows-terminal-as-your-default-command-line-experience/)
- [.NET CLI overview](https://aka.ms/dotnet-cli-docs)

## .NET
- [Download .NET SDK](https://www.microsoft.com/net/download)
- [.NET Conf 2022 Keynote: Welcome to .NET 7 | .NET Conf 2022](https://www.youtube.com/watch?v=8V_BUGFKdaI)
- [Stack Overflow survey 2021 - Most loved frameworks and libraries](https://insights.stackoverflow.com/survey/2021#section-most-loved-dreaded-and-wanted-other-frameworks-and-libraries)
- [Themes of .NET](https://themesof.net/)
- [Official list of .NET 7 supported operating systems](https://github.com/dotnet/core/blob/main/release-notes/7.0/supported-os.md)
- [.NET Support Policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core)
- [.NET versions](https://learn.microsoft.com/en-us/dotnet/core/versions/)
- [.NET Uninstall Tool](https://learn.microsoft.com/en-us/dotnet/core/additional-tools/uninstall-tool)
- [How to remove the .NET Runtime and SDK](https://learn.microsoft.com/en-us/dotnet/core/install/remove-runtime-sdk-versions)
- [.NET Runtime](https://github.com/dotnet/runtime/blob/main/README.md)
- [.NET Release Schedule](https://github.com/dotnet/core/blob/master/roadmap.md)
- [.NET Team Members](https://twitter.com/i/lists/120961876//members)

## Azure and .NET
- [State of Azure + .NET | .NET Conf 2022](https://www.youtube.com/watch?v=NBbI52liUR4)
- [The Whirlwind Tour of Building .NET Apps in Azure | .NET Conf 2022](https://www.youtube.com/watch?v=45gTMoqSYHg)

## Help and learning
- [Raise an issue with the book](https://github.com/markjprice/apps-services-net7/issues)
- [Microsoft Learn](https://learn.microsoft.com/)
- [Official .NET Blog written by the .NET engineering teams](https://devblogs.microsoft.com/dotnet/)
- [Stack Overflow](https://stackoverflow.com/)
- [Google Advanced Search](https://www.google.com/advanced_search)
- [.NET Videos](https://dotnet.microsoft.com/en-us/learn/videos)

# Chapter 1A - What's New in Modern C# and .NET

## C# language version and the journey to C# 11
- [What's New in C# 11 | .NET Conf 2022](https://www.youtube.com/watch?v=H18CfoinPZg)
- [Early peek at C# 11 features](https://devblogs.microsoft.com/dotnet/early-peek-at-csharp-11-features/)
- [What's new in C# 11](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-11)
- [Welcome to C# 10](https://devblogs.microsoft.com/dotnet/welcome-to-csharp-10/)
- [What's new in C# 10](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-10)
- [What's new in C# 9](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-9)
- [What's new in C# 8](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8)
- [What's new in C# 7.3](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7-3)
- [Rosalyn and C# compiler versions](https://github.com/dotnet/roslyn/blob/master/docs/wiki/NuGet-packages.md)
- [Current status of the C# language](https://github.com/dotnet/roslyn/blob/master/docs/Language%20Feature%20Status.md)
- [Draft proposals for C# Language Specifications for 6.0 and later](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/)
- [C# language versioning](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/configure-language-version)
- [EPIC - .NET 6 C# project templates use latest language idioms #3359](https://github.com/dotnet/templating/issues/3359)
- [SDK support for implicit namespaces in C# projects #25066](https://github.com/dotnet/docs/issues/25066)
- [C# Reference](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/)
- [C# Programming Guide](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/)
- [C# Keywords](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/index)
- [Naming guidelines](https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/naming-guidelines)
- [Types (C# Programming Guide)](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/types/)
- [Statements (C# Programming Guide)](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/statements)
- [Pattern matching overview](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/pattern-matching)
- [Patterns (C# reference)](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns)

## C# implemented proposals
- [C# 11](https://github.com/dotnet/csharplang/tree/main/proposals/csharp-11.0)
- [C# 10](https://github.com/dotnet/csharplang/tree/main/proposals/csharp-10.0)
- [C# 9](https://github.com/dotnet/csharplang/tree/main/proposals/csharp-9.0)
- [C# 8](https://github.com/dotnet/csharplang/tree/main/proposals/csharp-8.0)
- [C# 7.3](https://github.com/dotnet/csharplang/tree/main/proposals/csharp-7.3)
- [C# 7.2](https://github.com/dotnet/csharplang/tree/main/proposals/csharp-7.2)
- [C# 7.1](https://github.com/dotnet/csharplang/tree/main/proposals/csharp-7.1)
- [C# 7.0](https://github.com/dotnet/csharplang/tree/main/proposals/csharp-7.0)
- [C# 6.0](https://github.com/dotnet/csharplang/tree/main/proposals/csharp-6.0)
- [Proposal: Required Properties #3630](https://github.com/dotnet/csharplang/issues/3630)
- [Proposal: Semi-Auto-Properties; field keyword #140](https://github.com/dotnet/csharplang/issues/140)
- [Working Set (of proposals)](https://github.com/dotnet/csharplang/milestone/19)

# Chapter 1B - Benchmarking Performance and Testing

- [Benchmark.NET](https://github.com/dotnet/BenchmarkDotNet)

# Chapter 1C - Observing and Modifying Code Execution Dynamically

## Assemblies and reflection
- [.NET API Reference](https://learn.microsoft.com/en-us/dotnet/api/)
- [Compiler-generated display class](http://stackoverflow.com/a/2509524/55847)
- [Dynamically load assemblies that are not currently referenced](https://learn.microsoft.com/en-us/dotnet/standard/assembly/unloadability-howto)
- [Dynamically execute code](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.methodbase.invoke)
- [Dynamically generate new code and assemblies](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.emit.assemblybuilder)
- [Extending Metadata Using Attributes](https://learn.microsoft.com/en-us/dotnet/standard/attributes/)

## Expression Trees
- [Expression Trees (C#)](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/expression-trees/)
- [Building Expression Trees](https://learn.microsoft.com/en-us/dotnet/csharp/expression-trees-building)
- [Expression trees for dummies?](https://stackoverflow.com/questions/623413/expression-trees-for-dummies)

## Source generators
- [Source Generators](https://learn.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/source-generators-overview)
- [Introducing C# Source Generators](https://devblogs.microsoft.com/dotnet/introducing-c-source-generators/)
- [Source Generators Cookbook](https://github.com/dotnet/roslyn/blob/main/docs/features/source-generators.cookbook.md)
- [C# Source Generators](https://github.com/amis92/csharp-source-generators): "A list of C# Source Generators (not necessarily awesome), because I haven't found a good list yet."

# Chapter 2 - Managing Relational Data Using SQL Server

## Microsoft SQL Server
- [Try SQL Server on-premises or in the cloud](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Download SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)
- [Quickstart: Connect and query a SQL Server instance using SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/sql/ssms/quickstarts/ssms-connect-query-sql-server)
- [Quickstart: Connect and query an Azure SQL Database or an Azure Managed Instance using SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/sql/ssms/quickstarts/ssms-connect-query-azure-sql)
- [Use Visual Studio Code to create and run Transact-SQL scripts](https://learn.microsoft.com/en-us/sql/tools/visual-studio-code/sql-server-develop-use-vscode)
- [.NET Interactive with SQL!| .NET Notebooks in Visual Studio Code](https://devblogs.microsoft.com/dotnet/net-interactive-with-sql-net-notebooks-in-visual-studio-code/)

## Dapper and comparing to EF Core performance
- [Dapper](https://github.com/DapperLib/Dapper)
- [Announcing Entity Framework Core 6.0 Preview 4: Performance Edition](https://devblogs.microsoft.com/dotnet/announcing-entity-framework-core-6-0-preview-4-performance-edition/)
- [Announcing Entity Framework Core 7 Preview 6: Performance Edition](https://devblogs.microsoft.com/dotnet/announcing-ef-core-7-preview6-performance-optimizations/)

# Chapter 3 - Building Entity Models for SQL Server Using EF Core

## EF Core
- [EF Core documentation](https://learn.microsoft.com/en-us/ef/core/)
- [Announcing the Plan for EF7](https://devblogs.microsoft.com/dotnet/announcing-the-plan-for-ef7/)
- [New features in EF Core 7](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-7.0/whatsnew)
- [New features in EF Core 6](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-6.0/whatsnew)
- [New features in EF Core 5](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-5.0/whatsnew)
- [Get to Know EF Core 6](https://devblogs.microsoft.com/dotnet/get-to-know-ef-core-6/)
- [Plans for Entity Framework Core 6.0 revealed as Microsoft admits it is unlikely to match Dapper for performance](https://www.theregister.com/2021/01/19/entity_framework_core_6/)
- [Entity Framework Community Standup - Performance Tuning an EF Core App](https://www.youtube.com/watch?v=VgNFFEqwZPU)

## EF Core database providers
- [EF Core database providers](https://learn.microsoft.com/en-us/ef/core/providers/)
- [Devart database providers](https://www.devart.com/dotconnect/entityframework.html)
- [Check the latest NuGet package version](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Sqlite/)
- [Document Database Providers for Entity Framework Core](https://github.com/BlueshiftSoftware/EntityFrameworkCore)

## EF Core models
- [EF Core model conventions](https://learn.microsoft.com/en-us/ef/core/modeling/)
- [Data seeding](https://learn.microsoft.com/en-us/ef/core/modeling/data-seeding)
- [Humanizer library](http://humanizr.net)
- [Scaffolding](https://learn.microsoft.com/en-us/ef/core/managing-schemas/scaffolding?tabs=dotnet-core-cli)
- [Deep Dive into Many-to-Many: A Tour of EF Core 5.0](https://channel9.msdn.com/Shows/On-NET/Deep-Dive-into-Many-to-Many-A-Tour-of-EF-Core-50-pt-2)
- [Naming Conventions for Entity Framework Core Tables and Columns](https://github.com/efcore/EFCore.NamingConventions)
- [Table-per-concrete-type (TPC) inheritance mapping](https://learn.microsoft.com/en-gb/ef/core/what-is-new/ef-core-7.0/whatsnew#table-per-concrete-type-tpc-inheritance-mapping)
- [T4 goodness with Entity Framework Core 7 | .NET Conf 2022](https://www.youtube.com/watch?v=PUexkGWErNk)

## EF Core querying and manipulating
- [Filtered include](https://learn.microsoft.com/en-us/ef/core/querying/related-data/eager#filtered-include)
- [Simple logging](https://learn.microsoft.com/en-us/ef/core/logging-events-diagnostics/simple-logging)
- [Query tags](https://learn.microsoft.com/en-us/ef/core/querying/tags)
- [Loading patterns](https://learn.microsoft.com/en-us/ef/core/querying/related-data)
- [Pooling database contexts](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-2.0#dbcontext-pooling)

# Chapter 4 - Managing NoSQL Data Using Azure Cosmos DB

## NoSQL data stores
- [Use NoSQL databases as a persistence infrastructure](https://learn.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/nosql-database-persistence-infrastructure)

## Azure Cosmos DB
- [Welcome to Azure Cosmos DB](https://learn.microsoft.com/en-us/azure/cosmos-db/introduction)
- [Azure Cosmos DB .NET SDK v3 for SQL API: Download and release notes](https://learn.microsoft.com/en-us/azure/cosmos-db/sql/sql-api-sdk-dotnet-standard)
- [Data modeling in Azure Cosmos DB](https://learn.microsoft.com/en-us/azure/cosmos-db/sql/modeling-data)
- [How to model and partition data on Azure Cosmos DB using a real-world example](https://learn.microsoft.com/en-us/azure/cosmos-db/sql/how-to-model-partition-example)
- [Getting started with SQL queries](https://learn.microsoft.com/en-us/azure/cosmos-db/sql/sql-query-getting-started)
- [Azure Cosmos DB query cheat sheets](https://learn.microsoft.com/en-us/azure/cosmos-db/sql/query-cheat-sheet)
- [Stored procedures, triggers, and user-defined functions](https://learn.microsoft.com/en-us/azure/cosmos-db/sql/stored-procedures-triggers-udfs)
- [Manage consistency levels in Azure Cosmos DB](https://learn.microsoft.com/en-us/azure/cosmos-db/sql/how-to-manage-consistency)
- [Create a container in Azure Cosmos DB SQL API](https://learn.microsoft.com/en-us/azure/cosmos-db/sql/how-to-create-container)
- [Query an Azure Cosmos container](https://learn.microsoft.com/en-us/azure/cosmos-db/sql/how-to-query-container)
- [Best practices for Azure Cosmos DB .NET SDK](https://learn.microsoft.com/en-us/azure/cosmos-db/sql/best-practice-dotnet)
- [Diagnose and troubleshoot issues when using Azure Cosmos DB .NET SDK](https://learn.microsoft.com/en-us/azure/cosmos-db/sql/troubleshoot-dot-net-sdk)
- [Migrate one-to-few relational data into Azure Cosmos DB SQL API account](https://learn.microsoft.com/en-us/azure/cosmos-db/sql/migrate-relational-to-cosmos-db-sql-api)
- [Frequently asked questions about different APIs in Azure Cosmos DB](https://learn.microsoft.com/en-us/azure/cosmos-db/faq)

# Chapter 5 - Multitasking and Concurrency

- [Thread pool](https://learn.microsoft.com/en-us/dotnet/standard/threading/the-managed-thread-pool)
- [Pros and cons of different ways to start tasks](https://devblogs.microsoft.com/pfxteam/task-factory-startnew-vs-new-task-start/)
- [Events and thread-safety](https://learn.microsoft.com/en-us/archive/blogs/cburrows/field-like-events-considered-harmful)
- [Stephen Cleary's thoughts on events and thread-safety](https://blog.stephencleary.com/2009/06/threadsafe-events.html)
- [Threads and threading](https://learn.microsoft.com/en-us/dotnet/standard/threading/threads-and-threading)
- [Async in depth](https://learn.microsoft.com/en-us/dotnet/standard/async-in-depth)
- [await (C# reference)](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/await)
- [Parallel Programming in .NET](https://learn.microsoft.com/en-us/dotnet/standard/parallel-programming/)
- [Overview of synchronization primitives](https://learn.microsoft.com/en-us/dotnet/standard/threading/overview-of-synchronization-primitives)
- [Async streams tutorial](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/generate-consume-asynchronous-stream)
- [Performance Improvements in .NET 7 | .NET Conf 2022](https://www.youtube.com/watch?v=yNPEdaxkTZw)

# Chapter 6 - Implementing Popular Third-Party Libraries

- [ImageSharp](https://github.com/SixLabors/ImageSharp)
- [Humanizer](https://github.com/Humanizr/Humanizer)
- [Serilog](https://serilog.net/)
- [AutoMapper, a convention-based object-object mapper](https://automapper.org)
- [FluentValidation documentation](https://docs.fluentvalidation.net/en/latest/)
- [Fluent Assertions - Introduction](https://fluentassertions.com/introduction)
- [Modern .NET library for PDF document generation](https://www.questpdf.com/)

# Chapter 7 - Handling Dates, Times, and Internationalization

- [Date, Time, and Time Zone Enhancements in .NET 6](https://devblogs.microsoft.com/dotnet/date-time-and-time-zone-enhancements-in-net-6/)
- [.NET 6: Date and Time Structures](https://www.infoq.com/news/2021/04/Net6-Date-Time/)
- [Noda Time: A better date and time API for .NET](https://nodatime.org/)
- [Globalizing and localizing .NET applications](https://learn.microsoft.com/en-us/dotnet/standard/globalization-localization/)
- [Time zones](https://devblogs.microsoft.com/dotnet/cross-platform-time-zones-with-net-core/)

# Chapter 8 - Building and Securing Web Services Using Minimal APIs

## Web service technologies and design
- [Media types](http://en.wikipedia.org/wiki/Media_type)
- [HTTP OPTIONS method and other HTTP methods](https://developer.mozilla.org/en-US/docs/Web/HTTP/Methods/OPTIONS)
- [HTTP POST requests](https://developer.mozilla.org/en-US/docs/Web/HTTP/Methods/POST)
- [Create web APIs with ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/web-api/)
- [RESTful web API design](https://learn.microsoft.com/en-us/azure/architecture/best-practices/api-design)
- [Making the Most of Minimal APIs in .NET 7 | .NET Conf 2022](https://www.youtube.com/watch?v=HXHwtEjQoyM)
- [Building modern high performance services with ASP.NET Core and .NET 7](https://www.youtube.com/watch?v=P7lmBFuw92s)

## Web service routing
- [Endpoint routing](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing)
- [Previous routing system](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-2.1)
- [Route constraints](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing#route-constraint-reference)
- [Dependency injection for ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection)
- [Proposed standard for Problem Details for HTTP APIs](https://tools.ietf.org/html/rfc7807)
- [Implementing problem details](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.problemdetails)
- [Design decisions around endpoint routing](https://devblogs.microsoft.com/aspnet/asp-net-core-2-2-0-preview1-endpoint-routing/)

## Web service clients
- [REST Client](https://github.com/Huachao/vscode-restclient/blob/master/README.md)
- [It is the BaseAddress and DefaultRequestHeaders properties that you should treat with caution with multiple threads](https://medium.com/@nuno.caneco/c-httpclient-should-not-be-disposed-or-should-it-45d2a8f568bc)
- [You're using HttpClient wrong and it is destabilizing your software](https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/)
- [How to initiate HTTP requests](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests)
- [Issues with the original HttpClient class available in .NET](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests#issues-with-the-original-httpclient-class-available-in-net)
- [HttpClient extension methods for easily working with JSON](https://github.com/dotnet/designs/blob/main/accepted/2020/json-http-extensions/json-http-extensions.md)

## Documenting web services
- [Swagger](https://swagger.io/)
- [Swagger Tools](https://swagger.io/tools/)
- [Swashbuckle for ASP.NET Core](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
- [How Swagger can support multiple versions of an API](https://stackoverflow.com/questions/30789045/leverage-multipleapiversions-in-swagger-with-attribute-versioning/30789944)
- [Importance of documenting services](https://idratherbewriting.com/learnapidoc/)
- [Benefits of setting version compatibility](https://learn.microsoft.com/en-us/aspnet/core/mvc/compatibility-version)
- [Check latest version of analyzers package](http://www.nuget.org/packages/Microsoft.AspNetCore.Mvc.Api.Analyzers/)

## Securing web services
- [Verifying that the tokens used to call your web APIs are requested with the expected claims](https://learn.microsoft.com/en-us/azure/active-directory/develop/scenario-protected-web-api-verification-scope-app-roles)
- [CORS can be enabled to allow different origin requests](https://learn.microsoft.com/en-us/aspnet/core/security/cors)
- [Common HTTP security headers that you might want to add](https://www.meziantou.net/security-headers-in-asp-net-core.htm)
- [Built-in features for compliance with modern privacy requirements like GDPR](https://learn.microsoft.com/en-us/aspnet/core/security/gdpr)
- [ASP.NET Core's support for authenticator apps](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity-enable-qrcodes)
- [Identity UI library](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/scaffold-identity?tabs=netcore-cli)
- [Authorization](https://learn.microsoft.com/en-us/aspnet/core/security/authorization/introduction)
- [Anti-forgery tokens](https://learn.microsoft.com/en-us/aspnet/core/security/anti-request-forgery)

## Exposing Data via the Web Using OData

- [OData - the best way to REST](https://www.odata.org/)
- [Welcome to OData](https://learn.microsoft.com/en-us/odata/overview)
- [OData reference](https://learn.microsoft.com/en-us/dotnet/api/overview/odata-dotnet/)
- [OData DevBlog](https://devblogs.microsoft.com/odata/)
- [$compute and $search in ASP.NET Core OData 8](https://devblogs.microsoft.com/odata/compute-and-search-in-asp-net-core-odata-8/)
- [The Future of OData NxT (Neo)](https://devblogs.microsoft.com/odata/the-future-of-odata-odata-nxt/)
- [API versioning extension with ASP.NET Core OData 8](https://devblogs.microsoft.com/odata/api-versioning-extension-with-asp-net-core-odata-8/)

# Chapter 9 - Caching, Queuing, and Resilient Background Services

## Understanding service architecture

- [Are networks now faster than disks?](https://serverfault.com/questions/238417/are-networks-now-faster-than-disks)
- [The Infinite Space Between Words](https://blog.codinghorror.com/the-infinite-space-between-words/)

## Caching with ASP.NET Core and Redis
- [Overview of caching in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/performance/caching/overview)
- [Cache in-memory in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/performance/caching/memory)
- [Distributed caching in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/performance/caching/distributed)
- [Distributed Redis Cache](https://learn.microsoft.com/en-us/aspnet/core/performance/caching/distributed#distributed-redis-cache)
- [Redis C#/.NET guide](https://redis.io/docs/clients/dotnet/)
- [Response caching in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/performance/caching/response)

## Resilience with Polly
- [The Polly Project](http://www.thepollyproject.org/)
- [Resilience policies](https://github.com/App-vNext/Polly#resilience-policies)
- [Retry with jitter](https://github.com/App-vNext/Polly/wiki/Retry-with-jitter)
- [Transient fault handling and proactive resilience engineering](https://github.com/App-vNext/Polly/wiki/Transient-fault-handling-and-proactive-resilience-engineering)

## Queueing with RabbitMQ, Azure Service Bus, and Event Hub

## Background services
- [Worker services in .NET](https://learn.microsoft.com/en-us/dotnet/core/extensions/workers)
- [Background tasks with hosted services in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services)
- [Implement the IHostedService interface](https://learn.microsoft.com/en-us/dotnet/core/extensions/timer-service)
- [Hangfire](https://www.hangfire.io/)
- [Quartz.NET](https://www.quartz-scheduler.net/)
- [Quartz.NET GitHub repository](https://github.com/quartznet/quartznet)

## Health checks and reliable web services
- [Health checks in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks)
- [How to extend the health check response](https://blogs.msdn.microsoft.com/webdev/2018/08/22/asp-net-core-2-2-0-preview1-healthcheck/)
- [How Polly can make your web services more reliable](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/implement-http-call-retries-exponential-backoff-polly)
- [Use HttpClientFactory to implement resilient HTTP requests](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests)

# Chapter 10 - Building Serverless Nanoservices Using Azure Functions

- [.NET 7 comes to Azure Functions & Visual Studio 2022](https://devblogs.microsoft.com/dotnet/dotnet-7-comes-to-azure-functions/)
- [Azure Functions](https://azure.microsoft.com/en-us/services/functions/)
- [Azure Functions documentation](https://learn.microsoft.com/en-us/azure/azure-functions/)
- [Azure serverless community library](https://www.serverlesslibrary.net/)
- [Building Serverless Applications with .NET 7 and Azure functions | .NET Conf 2022](https://www.youtube.com/watch?v=ITpcihRwbTc)

# Chapter 11 - Broadcasting Real-Time Communication Using SignalR

- [Overview of ASP.NET Core SignalR](https://learn.microsoft.com/en-us/aspnet/core/signalr/introduction)
- [SignalR GitHub repository](https://github.com/dotnet/aspnetcore/tree/main/src/SignalR)
- [Azure SignalR Service](https://azure.microsoft.com/en-gb/services/signalr-service/)

# Chapter 12 - Combining Data Sources Using GraphQL

- [GraphQL](https://graphql.org/)
- [HotChocolate - ChilliCream GraphQL Platform](https://chillicream.com/docs/hotchocolate)
- [HotChocolate GitHub repository](https://github.com/ChilliCream/hotchocolate)
- [Getting started with GraphQL and HotChocolate](https://learn.microsoft.com/en-us/shows/on-net/getting-started-with-hotchocolate)
- [Say hello to Hot Chocolate 12!](https://chillicream.com/blog/2021/09/27/hot-chocolate-12)

# Chapter 13 - Building Efficient Microservices Using gRPC

- [gRPC](https://grpc.io/)
- [gRPC - Architecting Cloud Native .NET Applications for Azure](https://learn.microsoft.com/en-us/dotnet/architecture/cloud-native/grpc)
- [Introduction to gRPC on .NET](https://learn.microsoft.com/en-us/aspnet/core/grpc/)
- [Protocol Buffers](https://developers.google.com/protocol-buffers/docs/overview)
- [ASP.NET Core gRPC for WCF Developers](https://learn.microsoft.com/en-us/dotnet/architecture/grpc-for-wcf-developers/)
- [Introduction to HTTP/2](https://web.dev/performance-http2/)
- [High-performance services with gRPC: What's new in .NET 7 | .NET Conf 2022](https://www.youtube.com/watch?v=et_2NBk4N4Y)
- [From RESTful HTTP API to gRPC | .NET Conf 2022](https://www.youtube.com/watch?v=o7MHxvcjFm4)
- [Using CoreWCF to unblock modernization of WCF apps | .NET Conf 2022](https://www.youtube.com/watch?v=jbcNF-QsxNs)

# Chapter 14 - Building Web User Interfaces Using ASP.NET Core

- [State of ASP.NET Core | .NET Conf 2022](https://www.youtube.com/watch?v=gNyEpkJMmcM)
- [ASP.NET Core fundamentals](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/)
- [Overview of ASP.NET Core MVC](https://learn.microsoft.com/en-us/aspnet/core/mvc/overview)
- [Free TLS/SSL certificates](https://letsencrypt.org)
- [Razor syntax reference for ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/razor)
- [HtmlHelper class](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.viewfeatures.htmlhelper)
- [Views in ASP.NET Core MVC](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/overview)
- [Layout in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/layout)
- [Tag Helpers in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/intro)
- [Human-readable Razor views with ASP.NET 7 Tag Helpers | .NET Conf 2022](https://www.youtube.com/watch?v=8B9bqdMIXZk)
- [ASP.NET Core Razor Pages with EF Core](https://learn.microsoft.com/en-us/aspnet/core/data/ef-rp/intro)
- [Registering a database context for use as a dependency service](https://learn.microsoft.com/en-us/ef/core/miscellaneous/configuring-dbcontext#using-dbcontext-with-dependency-injection)
- [The `<partial>` tag helper](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/built-in/partial-tag-helper)
- [Why it is good to put <script> elements at the bottom of the <body>](https://stackoverflow.com/questions/436411/where-should-i-put-script-tags-in-html-markup)

# Chapter 15 - Building Web Components Using Blazor

## Blazor hosting models
- [Blazor](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
- [Official list of supported Blazor platforms](https://learn.microsoft.com/en-us/aspnet/core/blazor/supported-platforms)
- [Blazor hosting models](https://learn.microsoft.com/en-us/aspnet/core/blazor/hosting-models)
- [Blazor Hybrid apps](https://devblogs.microsoft.com/aspnet/hybrid-blazor-apps-in-mobile-blazor-bindings-july-update/)
- [What's new for Blazor in .NET 7 | .NET Conf 2022](https://www.youtube.com/watch?v=evW4Gj4sHsk)
- [.NET ❤️’s WebAssembly in .NET 7 | .NET Conf 2022](https://www.youtube.com/watch?v=Ru-kO77d3F8)

## Blazor components
- [The reason for needing CSS isolation for Blazor components](https://github.com/dotnet/aspnetcore/issues/10170)
- [OI icons](https://iconify.design/icon-sets/oi/)
- [Forms and validation](https://learn.microsoft.com/en-us/aspnet/core/blazor/forms-validation)
- [NavigationManager with Blazor routes](https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/routing#uri-and-navigation-state-helpers)

## Advanced techniques
- [Implementing offline support for Blazor WebAssembly projects](https://learn.microsoft.com/en-us/aspnet/core/blazor/progressive-web-app#offline-support)
- [Lazy loading assemblies](https://learn.microsoft.com/en-us/aspnet/core/blazor/webassembly-lazy-load-assemblies)
- [Routing in Blazor Apps: Comparing the routing of popular web frameworks like React and Angular with Blazor](https://devblogs.microsoft.com/premier-developer/routing-in-blazor-apps/)
- [Use .NET from any JavaScript app in .NET 7](https://devblogs.microsoft.com/dotnet/use-net-7-from-any-javascript-app-in-net-7/)
- [Testing Blazor Applications with Playwright | .NET Conf 2022](https://www.youtube.com/watch?v=gBky9_AskNQ)
- [CSS Techniques for Blazor Developers | .NET Conf 2022](https://www.youtube.com/watch?v=w_x1keHyXAY)

# Chapter 16 - Leveraging Open-Source Blazor Component Libraries

- [Awesome Blazor: A collection of awesome Blazor resources](https://github.com/AdrienTorris/awesome-blazor)
- [Blazor University: Learn the new .NET SPA framework from Microsoft](https://blazor-university.com)
- [Blazor - app building workshop: In this workshop, we will build a complete Blazor app and learn about the various Blazor framework features along the way](https://github.com/dotnet-presentations/blazor-workshop/)
- [Carl Franklin's Blazor Train](https://www.youtube.com/playlist?list=PL8h4jt35t1wjvwFnvcB2LlYL4jLRzRmoz)
- [Welcome to PACMAN written in C# and running on Blazor WebAssembly](https://github.com/SteveDunn/PacManBlazor)

# Chapter 17 - Building Mobile and Desktop Apps Using .NET MAUI

- [State of .NET MAUI | .NET Conf 2022](https://www.youtube.com/watch?v=Z6UPJmerTo8)
- [.NET Multi-platform App UI](https://dotnet.microsoft.com/en-us/apps/maui)
- [.NET Multi-platform App UI documentation](https://learn.microsoft.com/en-us/dotnet/maui/)
- [.NET Multi-platform App UI (.NET MAUI) Support Policy](https://dotnet.microsoft.com/en-us/platform/support/policy/maui)
- [What's new in .NET MAUI and Desktop Apps | .NET Conf 2022](https://www.youtube.com/watch?v=nu6sv94QvAg)
- [.NET Conf Focus on MAUI – That’s a wrap!](https://devblogs.microsoft.com/dotnet/dotnet-conf-focus-on-maui-recap/)
- [.NET Podcasts - Sample Application](https://github.com/microsoft/dotnet-podcasts)
- [.NET MAUI eBook Now Available – Enterprise Application Pattern](https://devblogs.microsoft.com/dotnet/dotnet-maui-ebook-released/)
- [Announcing .NET MAUI support for Xcode 14 and iOS 16](https://devblogs.microsoft.com/dotnet/dotnet-maui-xcode-14/)
- [Introducing .NET MAUI – One Codebase, Many Platforms](https://devblogs.microsoft.com/dotnet/introducing-dotnet-maui-one-codebase-many-platforms/)
- [Introducing .NET Multi-platform App UI](https://devblogs.microsoft.com/dotnet/introducing-net-multi-platform-app-ui/)
- [Layouts](https://learn.microsoft.com/en-us/dotnet/maui/user-interface/layouts/)
- [Pages - ContentPage](https://learn.microsoft.com/en-us/dotnet/maui/user-interface/pages/contentpage)
- [Add a splash screen to a .NET MAUI app project](https://learn.microsoft.com/en-us/dotnet/maui/user-interface/images/splashscreen)
- [Inspect the visual tree of a .NET MAUI app](https://learn.microsoft.com/en-us/dotnet/maui/user-interface/live-visual-tree)
- [Phone dialer](https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/communication/phone-dialer)
- [Connect to local web services from Android emulators and iOS simulators](https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/local-web-services)
- [.NET MAUI - Workshop](https://github.com/dotnet-presentations/dotnet-maui-workshop)
- [Build mobile and desktop apps with .NET MAUI learning path](https://learn.microsoft.com/en-gb/learn/paths/build-apps-with-dotnet-maui/)
- [Forums](https://learn.microsoft.com/en-us/answers/topics/dotnet-maui.html)
- [Building Windows apps with WinUI 3 with .NET | .NET Conf 2022](https://www.youtube.com/watch?v=sYBCFTRmHOA)
- [Accelerate your WinUI 3 app with the Windows Community Toolkit | .NET Conf 2022](https://www.youtube.com/watch?v=WH-vRxvY95M)
- [OSS Spotlight - Build amazing cross-platform UI for .NET with Avalonia UI!](https://www.youtube.com/watch?v=qcZSr2ejH5I)

# Chapter 18 - Implementing Model-View-ViewModel for .NET MAUI

- [MVVM is easier than ever before with Source Generators, .NET 7, and the MVVM Toolkit| .NET Conf 2022](https://www.youtube.com/watch?v=oQluWTag-e4)

# Chapter 19 - Integrating .NET MAUI Apps with Blazor and Native Platforms

- [Create native desktop & mobile apps using web skills in Blazor Hybrid | .NET Conf 2022](https://www.youtube.com/watch?v=ojcvL8KCOwo)
- [Host a Blazor web app in a .NET MAUI app using BlazorWebView](https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/blazorwebview)
- [Enterprise Application Patterns Using .NET MAUI](https://learn.microsoft.com/en-us/dotnet/architecture/maui/)
- [CarouselView](https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/carouselview/)
- [Display a menu bar in a .NET MAUI desktop app](https://learn.microsoft.com/en-us/dotnet/maui/user-interface/menubar)
- [Xamarin.Essentials 1.7 and introducing .NET MAUI Essentials](https://devblogs.microsoft.com/xamarin/xamarin-essentials-1-7-and-introducing-net-maui-essentials/)
- [Platform integration](https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/)
- [Permissions](https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/appmodel/permissions)
- [Connectivity](https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/communication/networking)
- [Device information](https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/device/information)
- [Geolocation](https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/device/geolocation)
- [App actions](https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/appmodel/app-actions)
- [File picker](https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/storage/file-picker)
- [Media picker](https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/device-media/picker)
- [Clipboard](https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/data/clipboard)
- [Secure storage](https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/storage/secure-storage)
- [Modernize your WPF and Windows Forms application with Blazor | .NET Conf 2022](https://www.youtube.com/watch?v=niX1DbFwgq4)

# Chapter 20 - Introducing the Survey Project Challenge

- [Survey Monkey Question Types](https://help.surveymonkey.com/en/create/question-types/)

# Epilogue

## Next steps on your C# and .NET learning journey
- [Follow the Framework Design Guidelines](https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/)
- [Follow the code style rules](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/)
- [Cloud-Native learning resources for .NET developers](https://devblogs.microsoft.com/dotnet/cloud-native-learning-resources-for-net-developers/)

## Alternatives to using Azure resources
- [.NET on AWS Documentation & Developer Guides](https://aws.amazon.com/developer/language/net/net-developer-docs/)

## Learn from other Packt books
- [Mobile Development with .NET](https://www.packtpub.com/product/mobile-development-with-net-second-edition/9781800204690)
- [Enterprise Application Development with C# 10 and .NET 6](https://www.packtpub.com/product/enterprise-application-development-with-c-10-and-net-6-second-edition/9781803232973)
- [Software Architecture with C# 10 and .NET 6](https://www.packtpub.com/product/software-architecture-with-c-10-and-net-6-third-edition/9781803235257)
- [An Atypical ASP.NET Core 6 Design Patterns Guide](https://www.packtpub.com/product/an-atypical-aspnet-core-6-design-patterns-guide-second-edition/9781803249841)
- [Customizing ASP.NET Core 6.0](https://www.packtpub.com/product/customizing-aspnet-core-60-second-edition/9781803233604)
- [ASP.NET Core 5 Secure Coding Cookbook](https://www.packtpub.com/product/aspnet-core-5-secure-coding-cookbook/9781801071567)
- [ASP.NET Core 5 and React](https://www.packtpub.com/product/asp-net-core-5-and-react-second-edition/9781800206168)
- [ASP.NET Core 6 and Angular](https://www.packtpub.com/product/aspnet-core-6-and-angular-fifth-edition/9781803239705)
- [ASP.NET Core and Vue.js](https://www.packtpub.com/product/aspnet-core-and-vuejs/9781800206694)
- [Practical Microservices with Dapr and .NET](https://www.packtpub.com/product/practical-microservices-with-dapr-and-net/9781800568372)
- [Web Development with Blazor](https://www.packtpub.com/product/web-development-with-blazor-second-edition/9781803241494)
- [Building Blazor WebAssembly Applications with gRPC](https://www.packtpub.com/product/building-blazor-webassembly-applications-with-grpc/9781804610558)
- [Mastering Windows Presentation Foundation](https://www.packtpub.com/product/mastering-windows-presentation-foundation-second-edition/9781838643416)
