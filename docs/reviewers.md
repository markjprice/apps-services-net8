**Information for Technical Reviewers**

If you are a Technical Reviewer for this book, then you will find useful information on this page.

- [Installing previews](#installing-previews)
- [Word document files](#word-document-files)
- [Operating systems and code editors](#operating-systems-and-code-editors)
- [Lifetime of .NET 8 and the book](#lifetime-of-net-8-and-the-book)

# Installing previews

Microsoft releases official previews of .NET 8 on [Patch Tuesday](https://en.wikipedia.org/wiki/Patch_Tuesday) each month or one week later. I maintain a list of [.NET 8 preview releases](https://github.com/markjprice/cs11dotnet7/blob/main/docs/dotnet8.md).

- [Download .NET 8 Previews](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

# Word document files

I will add a comment to the top of each Word document for a chapter that specifies the version of .NET 8 preview that I used. You can either use exactly the same version or a newer version but be aware that there may be differences in behavior. These differences could be temporary (new bugs are sometimes added and later removed) or permanent so it is always useful to add a comment about any unexpected behavior that you experience. 

# Operating systems and code editors

Inevitably there will be differences in .NET on different operating systems. Historically about 70% of readers use Visual Studio 2022 on Windows so that is the primary code editor and OS that I use while writing preliminary drafts (PDs) during May to July. I often do not test on macOS with Visual Studio Code until final drafts (FDs) in September. I may not test on Linux at all, so if you're willing to, that'd be awesome! 

# Lifetime of .NET 8 and the book

.NET 8 will release in November 2023. It will reach end-of-life in November 2026.

I want to enable readers to pick up the book and use it to learn at any point in those three years so the book needs to be written with that in mind.

For example, many readers will consume the book early in .NET 8's lifetime. These readers should be easy to cater for. I write instructions today as if they are to be read after November 2023, for example, .NET packages references like `Microsoft.EntityFramework.SqlServer` are `8.0.0` instead of `8.0.0-preview.1.5673.345` and so on. In early chapters I remind readers to check if there is a later version for third-party packages.

A reader who starts reading the book in Spring 2024 might want to use .NET 9 previews. A reader who starts reading the book after November 2024 might want to use the release version of .NET 9. And then in February 2025 they could start using .NET 10 previews!  I plan to add a notes in Chapter 1 to help those readers be successful too.

Packt will likely only publish a .NET 9 edition of the *C# 13 and .NET 9 - Modern Cross-Platform Development Fundamentals* book. The third edition of the *Apps and Services* book is likely to be for .NET 10. So the second edition *Apps and Services with .NET 8* will need to be in the market for two years before being updated for .NET 10.
