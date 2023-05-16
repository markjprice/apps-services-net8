**Observing**

- [Using an analyzer to write better code](#using-an-analyzer-to-write-better-code)


# Using an analyzer to write better code

.NET analyzers find potential issues and suggest fixes for them. StyleCop is a commonly used analyzer for helping you write better C# code.
Let's see it in action:
1.	Use your preferred code editor to create a Console App/console project named CodeAnalyzing in a Chapter01 solution/workspace.
2.	In the CodeAnalyzing project, add a package reference for StyleCop.Analyzers.
3.	Add a JSON file to your project named stylecop.json for controlling StyleCop settings.
4.	Modify its contents, as shown in the following markup:
{
  "$schema": "https://raw.githubusercontent.com/DotNetAnalyzers/StyleCopAnalyzers/master/StyleCop.Analyzers/StyleCop.Analyzers/Settings/stylecop.schema.json",
  "settings": {

  }
}
The $schema entry enables IntelliSense while editing the stylecop.json file in your code editor.
5.	Move the insertion point inside the settings section and press Ctrl + Space, and note the IntelliSense showing valid subsections of settings, as shown in Figure 1.2:
 
Figure 1.2: stylecop.json IntelliSense showing valid subsections of settings
6.	In the CodeAnalyzing project file, add entries to configure the file named stylecop.json to not be included in published deployments, and to enable it as an additional file for processing during development, as shown highlighted in the following markup:
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net7.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="StyleCop.Analyzers" Version="1.2.0-*">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers</IncludeAssets>
    </PackageReference>
  </ItemGroup>

  <ItemGroup>
    <None Remove="stylecop.json" />
  </ItemGroup>

  <ItemGroup>
    <AdditionalFiles Include="stylecop.json" />
  </ItemGroup>

</Project>
At the time of writing, the StyleCop.Analyzers package is in preview. I have set the version to 1.2.0-* to make sure that as soon as a newer preview version is released, it will upgrade automatically. Once a stable version is available, I recommend fixing the version.
7.	In Program.cs, delete the existing statements and then add some statements to explicitly define the Program class with its Main method, as shown in the following code:
using System.Diagnostics;

namespace CodeAnalyzing;

class Program
{
  static void Main(string[] args)
  {
    Debug.WriteLine("Hello, Debugger!");
  }
}
8.	Build the CodeAnalyzing project.
9.	You will see warnings for everything it thinks is wrong, as shown in Figure 1.3:
 
Figure 1.3: StyleCop code analyzer warnings
For example, it wants using directives to be put within the namespace declaration, as shown in the following output:
C:\apps-services-net7\Chapter01\CodeAnalyzing\Program.cs(1,1): warning SA1200: Using directive should appear within a namespace declaration [C:\apps-services-net7\Chapter01\CodeAnalyzing\CodeAnalyzing.csproj]
Suppressing warnings
To suppress a warning, you have several options, including adding code and setting configuration.
To suppress a warning using an attribute, add an assembly-level attribute, as shown in the following code:
[assembly:SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1200:UsingDirectivesMustBePlacedWithinNamespace", Justification = "Reviewed.")]
To suppress a warning using a directive, add #pragma statements around the statement that is causing the warning, as shown in the following code:
#pragma warning disable SA1200 // UsingDirectivesMustBePlacedWithinNamespace
using System.Diagnostics;
#pragma warning restore SA1200 // UsingDirectivesMustBePlacedWithinNamespace
Let's suppress the warning by modifying the stylecop.json file:
1.	In stylecop.json, add a configuration option to set using statements to be allowable outside a namespace, as shown highlighted in the following markup:
{
  "$schema": "https://raw.githubusercontent.com/DotNetAnalyzers/StyleCopAnalyzers/master/StyleCop.Analyzers/StyleCop.Analyzers/Settings/stylecop.schema.json",
  "settings": {
    "orderingRules": {
      "usingDirectivesPlacement": "outsideNamespace"
    }
  }
}
2.	Build the project and note that warning SA1200 has disappeared.
3.	In stylecop.json, set the using directives placement to preserve, which allows using statements both inside and outside a namespace, as shown highlighted in the following markup:
"orderingRules": {
  "usingDirectivesPlacement": "preserve"
}
Fixing the code
Now, let's fix all the other warnings:
1.	In CodeAnalyzing.csproj, add an element to automatically generate an XML file for documentation and add an element to treat warnings as errors, as shown highlighted in the following markup:
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net7.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
  </PropertyGroup>
2.	In stylecop.json, add a configuration option to provide values for documentation for the company name and copyright text, as shown highlighted in the following markup:
{
  "$schema": "https://raw.githubusercontent.com/DotNetAnalyzers/StyleCopAnalyzers/master/StyleCop.Analyzers/StyleCop.Analyzers/Settings/stylecop.schema.json",
  "settings": {
    "orderingRules": {
      "usingDirectivesPlacement": "preserve"
    },
    "documentationRules": {
      "companyName": "Packt",
      "copyrightText": "Copyright (c) Packt. All rights reserved."
    }
  }
}
3.	In Program.cs, add comments for a file header with company and copyright text, move the using System; declaration inside the namespace, and set explicit access modifiers and XML comments for the class and method, as shown in the following code:
// <copyright file="Program.cs" company="Packt">
// Copyright (c) Packt. All rights reserved.
// </copyright>
namespace CodeAnalyzing;

using System.Diagnostics;

/// <summary>
/// The main class for this console app.
/// </summary>
public class Program
{
  /// <summary>
  /// The main entry point for this console app.
  /// </summary>
  /// <param name="args">
  /// A string array of arguments passed to the console app.
  /// </param>
  public static void Main(string[] args)
  {
    Debug.WriteLine("Hello, Debugger!");
  }
}
4.	Build the project.
5.	Expand the bin/Debug/net7.0 folder (remember to Show All Files if you are using Visual Studio 2022) and note the autogenerated file named CodeAnalyzing.xml, as shown in the following markup:
<?xml version="1.0"?>
<doc>
    <assembly>
        <name>CodeAnalyzing</name>
    </assembly>
    <members>
        <member name="T:CodeAnalyzing.Program">
            <summary>
            The main class for this console app.
            </summary>
        </member>
        <member name="M:CodeAnalyzing.Program.Main(System.String[])">
            <summary>
            The main entry point for this console app.
            </summary>
            <param name="args">
            A string array of arguments passed to the console app.
            </param>
        </member>
    </members>
</doc>
The CodeAnalyzing.xml file can then be processed by a tool like DocFX to convert it into documentation files, as shown at the following link: https://www.jamescroft.co.uk/building-net-project-docs-with-docfx-on-github-pages/ 
Understanding common StyleCop recommendations
Inside a code file, you should order the contents as shown in the following list:
1.	External alias directives
2.	Using directives
3.	Namespaces
4.	Delegates
5.	Enums
6.	Interfaces
7.	Structs
8.	Classes
Within a class, record, struct, or interface, you should order the contents as shown in the following list:
1.	Fields
2.	Constructors
3.	Destructors (finalizers)
4.	Delegates
5.	Events
6.	Enums
7.	Interfaces
8.	Properties
9.	Indexers
10.	Methods
11.	Structs
12.	Nested classes and records
Good Practice: You can learn about all the StyleCop rules at the following link: https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/DOCUMENTATION.md.
