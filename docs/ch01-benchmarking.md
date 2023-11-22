**Benchmarking Performance and Testing**

This online-only section is about benchmarking performance and testing.

- [Monitoring performance and resource usage](#monitoring-performance-and-resource-usage)
  - [Evaluating the efficiency of types](#evaluating-the-efficiency-of-types)
  - [Monitoring performance and memory using diagnostics](#monitoring-performance-and-memory-using-diagnostics)
  - [Useful members of the Stopwatch and Process types](#useful-members-of-the-stopwatch-and-process-types)
  - [Implementing a Recorder class](#implementing-a-recorder-class)
  - [Measuring the efficiency of processing strings](#measuring-the-efficiency-of-processing-strings)
- [Monitoring performance and memory using Benchmark.NET](#monitoring-performance-and-memory-using-benchmarknet)
  - [Building a console app with Benchmark.NET](#building-a-console-app-with-benchmarknet)
  - [Running a console app with Benchmark.NET](#running-a-console-app-with-benchmarknet)
- [Practicing and exploring](#practicing-and-exploring)
  - [Exercise 1B.1 – Test your knowledge](#exercise-1b1--test-your-knowledge)
  - [Exercise 1B.2 – Explore topics](#exercise-1b2--explore-topics)
- [Summary](#summary)

# Monitoring performance and resource usage

Before we can improve the performance of any code, we need to be able to monitor its speed and efficiency to record a baseline that we can then measure improvements against.

## Evaluating the efficiency of types

What is the best type to use for a scenario? To answer this question, we need to carefully consider what we mean by *best*, and through this, we should consider the following factors:

- **Functionality**: This can be decided by checking whether the type provides the features you need.
- **Memory size**: This can be decided by the number of bytes of memory the type takes up.
- **Performance**: This can be decided by how fast the type is.
- **Future needs**: This depends on the changes in requirements and maintainability.

There will be scenarios, such as when storing numbers, where multiple types have the same functionality, so we will need to consider memory and performance to make a choice.

If we need to store millions of numbers, then the best type to use would be the one that requires the fewest bytes of memory. But if we only need to store a few numbers, yet we need to perform lots of calculations on them, then the best type to use would be the one that runs fastest on a specific CPU.

The `sizeof()` function shows the number of bytes that a single instance of a type uses in memory. When we are storing many values in more complex data structures, such as arrays and lists, then we need a better way of measuring memory usage.

You can read lots of advice online and in books, but the only way to know for sure what the best type would be for your code is to compare the types yourself.

In the next section, you will learn how to write code to monitor the actual memory requirements and performance when using different types.

Today a `short` variable might be the best choice, but it might be an even better choice to use an `int` variable, even though it takes twice as much space in the memory. This is because we might need a wider range of values to be stored in the future.

As listed above, there is an important metric that developers often forget: maintenance. This is a measure of how much effort another programmer would have to put in to understand and modify your code. If you make a nonobvious choice of type without explaining that choice with a helpful comment, then it might confuse the programmer who comes along later and needs to fix a bug or add a feature.

## Monitoring performance and memory using diagnostics

The `System.Diagnostics` namespace has lots of useful types for monitoring your code. The first useful type that we will look at is the `Stopwatch` type:

1.	Use your preferred coding tool to create a class library project, as defined in the following list:
    - Project template: **Class Library** / `classlib`
    - Solution file and folder: `Chapter1`
    - Project file and folder: `MonitoringLib`
2.	Add a console app project, as defined in the following list:
    - Project template: **Console App** / `console`
    - Solution file and folder: `Chapter1`
    - Project file and folder: `MonitoringApp`
3.	Use your preferred coding tool to set which project is active:
    - If you are using Visual Studio 2022, set the startup project for the solution to the current selection.
4.	In the `MonitoringLib` project, rename the `Class1.cs` file to `Recorder.cs`.
5.	In the `MonitoringLib` project, globally and statically import the `System.Console` class.
6.	In the `MonitoringApp` project, globally and statically import the `System.Console` class and add a project reference to the `MonitoringLib` class library, as shown in the following markup:
```xml
<ItemGroup>
  <Using Include="System.Console" Static="true" />
</ItemGroup>

<ItemGroup> 
  <ProjectReference Include="..\MonitoringLib\MonitoringLib.csproj" />
</ItemGroup>
```
7.	Build the `MonitoringApp` project.

## Useful members of the Stopwatch and Process types

The `Stopwatch` type has some useful members that we will use, as shown in the following table:

Member|Description
---|---
`Restart` method|This resets the elapsed time to zero and then starts the timer.
`Stop` method|This stops the timer.
`Elapsed` property|This is the elapsed time stored as a `TimeSpan` format (for example, hours:minutes:seconds).
`ElapsedMilliseconds` property|This is the elapsed time in milliseconds stored as an `Int64` value.

The `Process` type has some useful members that we will use, as shown in the following table:
Member|Description
---|---
VirtualMemorySize64|This displays the amount of virtual memory, in bytes, allocated for the process.
WorkingSet64|This displays the amount of physical memory, in bytes, allocated for the process.

## Implementing a Recorder class

We will create a `Recorder` class that makes it easy to monitor time and memory resource usage. To implement our `Recorder` class, we will use the `Stopwatch` and `Process` classes:

1.	In `Recorder.cs`, change its contents to use a `Stopwatch` instance to record timings and the current `Process` instance to record memory usage, as shown in the following code:
```cs
using System.Diagnostics; // Stopwatch

using static System.Diagnostics.Process; // GetCurrentProcess()

namespace Packt.Shared;

public static class Recorder
{
  private static Stopwatch timer = new();

  private static long bytesPhysicalBefore = 0;
  private static long bytesVirtualBefore = 0;

  public static void Start()
  {
    // force some garbage collections to release memory that is
    // no longer referenced but has not been released yet
    GC.Collect();
    GC.WaitForPendingFinalizers();
    GC.Collect();
    GC.WaitForPendingFinalizers();
    GC.Collect();

    // store the current physical and virtual memory use 
    bytesPhysicalBefore = GetCurrentProcess().WorkingSet64;
    bytesVirtualBefore = GetCurrentProcess().VirtualMemorySize64;

    timer.Restart();
  }

  public static void Stop()
  {
    timer.Stop();

    long bytesPhysicalAfter =
      GetCurrentProcess().WorkingSet64;

    long bytesVirtualAfter =
      GetCurrentProcess().VirtualMemorySize64;

    WriteLine("{0:N0} physical bytes used.",
      bytesPhysicalAfter - bytesPhysicalBefore);

    WriteLine("{0:N0} virtual bytes used.",
      bytesVirtualAfter - bytesVirtualBefore);

    WriteLine("{0} time span elapsed.", timer.Elapsed);

    WriteLine("{0:N0} total milliseconds elapsed.",
      timer.ElapsedMilliseconds);
  }
}
```

The `Start` method of the `Recorder` class uses the `GC` type (garbage collector) to ensure that any currently allocated but not referenced memory is collected before recording the amount of used memory. This is an advanced technique that you should almost never use in application code, because the thr garbage collector understands memory usage better than a programmer would and should be trusted to make decisions about when to collect unused memory itself. Our need to take control in this scenario is exceptional.

2.	In `Program.cs`, delete the existing statements and then add statements to start and stop the `Recorder` while generating an array of 10,000 integers, as shown in the following code:
```cs
using Packt.Shared; // Recorder

WriteLine("Processing. Please wait...");
Recorder.Start();

// simulate a process that requires some memory resources...
int[] largeArrayOfInts = Enumerable.Range(
  start: 1, count: 10_000).ToArray();

// ...and takes some time to complete
Thread.Sleep(new Random().Next(5, 10) * 1000);
Recorder.Stop();
```

3.	Run the code and view the result, as shown in the following output:
```
Processing. Please wait...
827,392 physical bytes used.
131,072 virtual bytes used.
00:00:06.0123934 time span elapsed.
6,012 total milliseconds elapsed.
```

Remember that the time elapsed is randomly between 5 and 10 seconds. Your results will vary even between multiple subsequent runs on the same machine. For example, when run on my Mac mini M1, less physical memory but more virtual memory was used, as shown in the following output:
```
Processing. Please wait...
294,912 physical bytes used.
10,485,760 virtual bytes used.
00:00:06.0074221 time span elapsed.
6,007 total milliseconds elapsed.
```

## Measuring the efficiency of processing strings

Now that you've seen how the `Stopwatch` and `Process` types can be used to monitor your code, we will use them to evaluate the best way to process `string` variables.

1.	In the `MonitoringApp` project, add a new class file named `Program.Helpers.cs`.
2.	In `Program.Helpers.cs`, delete any existing statements and then define a partial `Program` class with a method to output a section title in dark yellow color, as shown in the following code:
```cs
partial class Program
{
  static void SectionTitle(string title)
  {
    ConsoleColor previousColor = ForegroundColor;
    ForegroundColor = ConsoleColor.DarkYellow;
    WriteLine("*");
    WriteLine($"* {title}");
    WriteLine("*");
    ForegroundColor = previousColor;
  }
}
```

3.	In `Program.cs`, comment out the previous statements by wrapping them in multi-line comment characters: `/* */`.
4.	Add statements to create an array of 50,000 `int` variables and then concatenate them with commas as separators using a `string` and `StringBuilder` class, as shown in the following code:
```cs
int[] numbers = Enumerable.Range(start: 1, count: 50_000).ToArray();

SectionTitle("Using StringBuilder");
Recorder.Start();

System.Text.StringBuilder builder = new();

for (int i = 0; i < numbers.Length; i++)
{
  builder.Append(numbers[i]);
  builder.Append(", ");
}

Recorder.Stop();
WriteLine();

SectionTitle("Using string with +");
Recorder.Start();

string s = string.Empty; // i.e. ""

for (int i = 0; i < numbers.Length; i++)
{
  s += numbers[i] + ", ";
}

Recorder.Stop();
```

5.	Run the code and view the result, as shown in the following output:
```
*
* Using StringBuilder
*
1,150,976 physical bytes used.
0 virtual bytes used.
00:00:00.0010796 time span elapsed.
1 total milliseconds elapsed.

*
* Using string with +
*
11,849,728 physical bytes used.
1,638,400 virtual bytes used.
00:00:01.7754252 time span elapsed.
1,775 total milliseconds elapsed. 
```

We can summarize the results as follows:
- The `StringBuilder` class used about 1 MB of physical memory, zero virtual memory, and took about 1 millisecond.
- The `string` class with the `+` operator used about 11 MB of physical memory, 1.5 MB of virtual memory, and took 1.7 seconds.

In this scenario, `StringBuilder` is more than 1,000 times faster and about 10 times more memory-efficient when concatenating text! This is because `string` concatenation creates a new `string` each time you use it because `string` values are immutable (so they can be safely pooled for reuse). `StringBuilder` creates a single buffer in memory while it appends more characters.

> **Good Practice**: Avoid using the `String.Concat` method or the `+` operator inside loops. Use `StringBuilder` instead.

Now that you've learned how to measure the performance and resource efficiency of your code using types built into .NET, let's learn about a NuGet package that provides more sophisticated performance measurements.

# Monitoring performance and memory using Benchmark.NET

There is a popular benchmarking NuGet package for .NET that Microsoft uses in its blog posts about performance improvements, so it is good for .NET developers to know how it works and use it for their own performance testing. 

## Building a console app with Benchmark.NET

Let's see how we could use it to compare performance between `string` concatenation and `StringBuilder`:

1.	Use your preferred code editor to add a new console app to the `Chapter1` solution named `Benchmarking`.
2.	In the `Benchmarking` project, add a package reference to Benchmark.NET, remembering that you can find out the latest version and use that instead of the version I used, as shown in the following markup:
```xml
<ItemGroup>
  <PackageReference Include="BenchmarkDotNet" Version="0.13.10" />
</ItemGroup>
```
3.	Build the project to restore packages.
4.	Add a new class file named `StringBenchmarks.cs`.
5.	In `StringBenchmarks.cs`, add statements to define a class with methods for each benchmark you want to run, in this case, two methods that both combine twenty numbers comma-separated using either `string` concatenation or `StringBuilder`, as shown in the following code:
```cs
using BenchmarkDotNet.Attributes; // [Benchmark]

public class StringBenchmarks
{
  int[] numbers;

  public StringBenchmarks()
  {
    numbers = Enumerable.Range(
      start: 1, count: 20).ToArray();
  }

  [Benchmark(Baseline = true)]
  public string StringConcatenationTest()
  {
    string s = string.Empty; // e.g. ""

    for (int i = 0; i < numbers.Length; i++)
    {
      s += numbers[i] + ", ";
    }

    return s;
  }

  [Benchmark]
  public string StringBuilderTest()
  {
    System.Text.StringBuilder builder = new();

    for (int i = 0; i < numbers.Length; i++)
    {
      builder.Append(numbers[i]);
      builder.Append(", ");
    }

    return builder.ToString();
  }
}
```

6.	`In Program.cs`, delete the existing statements and then import the namespace for running benchmarks and add a statement to run the benchmarks class, as shown in the following code:
```cs
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<StringBenchmarks>();
```

## Running a console app with Benchmark.NET

1.	Use your preferred coding tool to run the console app with its release configuration:
    - In Visual Studio 2022, in the toolbar, set **Solution Configurations** to **Release**, and then navigate to **Debug** | **Start Without Debugging**.
    - In Visual Studio Code, in a terminal, enter the `dotnet run --configuration Release` command.

> **Important!** You must build the benchmarks in a **Release** build. This is important for performance testing, as most optimizations are disabled in **Debug** builds, in both the C# compiler and the JIT compiler.

2.	Note the results, including some artifacts like report files, and the most important, a summary table that shows that `string` concatenation took a mean of 412.990 ns and `StringBuilder` took a mean of 275.082 ns, as shown in the following partial output:
```
// ***** BenchmarkRunner: Finish  *****

// * Export *
  BenchmarkDotNet.Artifacts\results\StringBenchmarks-report.csv
  BenchmarkDotNet.Artifacts\results\StringBenchmarks-report-github.md
  BenchmarkDotNet.Artifacts\results\StringBenchmarks-report.html

// * Detailed results *
StringBenchmarks.StringConcatenationTest: DefaultJob
Runtime = .NET 7.0.0 (7.0.22.22904), X64 RyuJIT; GC = Concurrent Workstation
Mean = 412.990 ns, StdErr = 2.353 ns (0.57%), N = 46, StdDev = 15.957 ns
Min = 373.636 ns, Q1 = 413.341 ns, Median = 417.665 ns, Q3 = 420.775 ns, Max = 434.504 ns
IQR = 7.433 ns, LowerFence = 402.191 ns, UpperFence = 431.925 ns
ConfidenceInterval = [404.708 ns; 421.273 ns] (CI 99.9%), Margin = 8.282 ns (2.01% of Mean)
Skewness = -1.51, Kurtosis = 4.09, MValue = 2
-------------------- Histogram --------------------
[370.520 ns ; 382.211 ns) | @@@@@@
[382.211 ns ; 394.583 ns) | @
[394.583 ns ; 411.300 ns) | @@
[411.300 ns ; 422.990 ns) | @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
[422.990 ns ; 436.095 ns) | @@@@@
---------------------------------------------------

StringBenchmarks.StringBuilderTest: DefaultJob
Runtime = .NET 7.0.0 (7.0.22.22904), X64 RyuJIT; GC = Concurrent Workstation
Mean = 275.082 ns, StdErr = 0.558 ns (0.20%), N = 15, StdDev = 2.163 ns
Min = 271.059 ns, Q1 = 274.495 ns, Median = 275.403 ns, Q3 = 276.553 ns, Max = 278.030 ns
IQR = 2.058 ns, LowerFence = 271.409 ns, UpperFence = 279.639 ns
ConfidenceInterval = [272.770 ns; 277.394 ns] (CI 99.9%), Margin = 2.312 ns (0.84% of Mean)
Skewness = -0.69, Kurtosis = 2.2, MValue = 2
-------------------- Histogram --------------------
[269.908 ns ; 278.682 ns) | @@@@@@@@@@@@@@@
---------------------------------------------------

// * Summary *

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.22000
11th Gen Intel Core i7-1165G7 2.80GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.22904), X64 RyuJIT
  DefaultJob : .NET 7.0.0 (7.0.22.22904), X64 RyuJIT

|                  Method |     Mean |   Error |   StdDev | Ratio | RatioSD |
|------------------------ |---------:|--------:|---------:|------:|--------:|
| StringConcatenationTest | 413.0 ns | 8.28 ns | 15.96 ns |  1.00 |    0.00 |
|       StringBuilderTest | 275.1 ns | 2.31 ns |  2.16 ns |  0.69 |    0.04 |

// * Hints *
Outliers
  StringBenchmarks.StringConcatenationTest: Default -> 7 outliers were removed, 14 outliers were detected (376.78 ns..391.88 ns, 440.79 ns..506.41 ns)
  StringBenchmarks.StringBuilderTest: Default       -> 2 outliers were detected (274.68 ns, 274.69 ns)

// * Legends *
  Mean    : Arithmetic mean of all measurements
  Error   : Half of 99.9% confidence interval
  StdDev  : Standard deviation of all measurements
  Ratio   : Mean of the ratio distribution ([Current]/[Baseline])
  RatioSD : Standard deviation of the ratio distribution ([Current]/[Baseline])
  1 ns    : 1 Nanosecond (0.000000001 sec)

// ***** BenchmarkRunner: End *****
// ** Remained 0 benchmark(s) to run **

Run time: 00:01:13 (73.35 sec), executed benchmarks: 2
Global total time: 00:01:29 (89.71 sec), executed benchmarks: 2
// * Artifacts cleanup *
```

The `Outliers` section is especially interesting because it shows that not only is `string` concatenation slower than `StringBuilder`, but it is also more inconsistent in how long it takes. Your results will vary, of course. Note there might not be a `Hints` and an `Outliers` section if there are no outliers when you run your benchmarks!

# Practicing and exploring

Test your knowledge and understanding by answering some questions, getting some hands-on practice, and exploring with deeper research the topics in this chapter.

## Exercise 1B.1 – Test your knowledge

Use the web to answer the following questions:

1.	What information can you find out about a process?
2.	How accurate is the `Stopwatch` class?

## Exercise 1B.2 – Explore topics

Use the links on the following page to learn more about the topics covered in this chapter:
https://github.com/markjprice/apps-services-net8/blob/main/docs/book-links.md#chapter-1b---benchmarking-performance-and-testing

# Summary

In this online-only section, you learned: 
- You have now seen two ways to measure performance.
