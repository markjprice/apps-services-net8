**Improvements** (3 items)

If you have suggestions for improvements, then please [raise an issue in this repository](https://github.com/markjprice/apps-services-net8/issues) or email me at markjprice (at) gmail.com.

- [Print Book](#print-book)
  - [Page 169 - Nested and child tasks](#page-169---nested-and-child-tasks)
  - [Page 258 - Formatting date and time values](#page-258---formatting-date-and-time-values)
  - [Page 460 - Implementing a function that works with queues and BLOBs](#page-460---implementing-a-function-that-works-with-queues-and-blobs)
- [Bonus Content](#bonus-content)

# Print Book

## Page 169 - Nested and child tasks

> Thanks to Amer Cejudo for emailing about this item.

In this section about making one task the child of a parent task, I only showed output for one of two possible results, and had two notes to explain the other possible outputs. In the next edition, I will show both possible outputs before and after applying the `AttachedToParent` option.

In Step 3, you run the code and review the result, as shown in the following output:
```
Outer method starting...
Inner method starting...
Outer method finished.
Console app is stopping.
```

As the note at the top of page 170 says, you might not see any output from the `InnerMethod` at all, but I do not show that output. In the next edition, I will include it, as shown in the following output:
```
Outer method starting...
Outer method finished.
Console app is stopping.
```

In Step 4, you add the `AttachedToParent` option. 

In Step 5, you review the result, and the `OuterMethod` could start *and finish* before the `InnerMethod` starts but I do not show this possible output. I only show the scenario where both methods start before the methods finish, as shown in the following output:
```
Outer method starting...
Inner method starting...
Outer method finished.
Inner method finished.
Console app is stopping.
```
The addition of the `AttachedToParent` option means that the `InnerMethod` is guaranteed to both start and end before the console app ends. But it does not guarantee the order in which the methods finish. The `OuterMethod` could finish before the `InnerMethod` starts. Although I had a note about this after my output I did not show the actual output. In the next edition I will include it, as shown in the following output:
```
Outer method starting...
Outer method finished.
Inner method starting...
Inner method finished.
Console app is stopping.
```

## Page 258 - Formatting date and time values

In the next edition, I will add an extra row to *Table 7.3: Standard format code for date and time values* for the format code `m` or `M`. This uses a format that only shows day and month name, for example, **15 June**. I will also add a note that this format code only works when it is the only code. Combined with other codes it means minute (`m`) or month (`M`).

## Page 460 - Implementing a function that works with queues and BLOBs

> Thanks to [Jim Campbell](https://github.com/jimcbell) who raised this issue and provided solutions on [December 24, 2023](https://github.com/markjprice/apps-services-net7/issues/24).

In Step 9, I tell the reader to modify the statements in the `CheckGeneratorFunction.cs` class file. There is an issue if running on Mac or Linux because I hardcoded the path to the fonts with the Windows path separator, as shown in the following code:
```cs
FontFamily family = collection.Add(
  @"fonts\Caveat\static\Caveat-Regular.ttf");
```

I should have used cross-platform technique to build the path, as shown in the following code:
```cs
string pathToFont = System.IO.Path.Combine(
  "fonts", "Caveat", "static", "Caveat-Regular.ttf");

FontFamily family = collection.Add(pathToFont);
```

And to build the paths to the local folder to write the blob to, as shown in the following code:
```cs
if (System.Environment.GetEnvironmentVariable("IS_LOCAL") == "true")
{
  // Create blob in the local filesystem.
  string folder = System.IO.Path.Combine(
    System.Environment.CurrentDirectory, "blobs");

  if (!Directory.Exists(folder))
  {
    Directory.CreateDirectory(folder);
  }

  log.LogInformation($"Blobs folder: {folder}");

  string blobPath = System.IO.Path.Combine(folder,blobName);

  await image.SaveAsPngAsync(blobPath);
}
```

# Bonus Content 

None so far.
