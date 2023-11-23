**Improvements** (1 item)

If you have suggestions for improvements, then please [raise an issue in this repository](https://github.com/markjprice/apps-services-net8/issues) or email me at markjprice (at) gmail.com.

- [Print Book](#print-book)
  - [Page 169 - Nested and child tasks](#page-169---nested-and-child-tasks)
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

# Bonus Content 

None so far.
