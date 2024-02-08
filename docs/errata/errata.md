**Errata** (5 items)

If you find any mistakes, then please [raise an issue in this repository](https://github.com/markjprice/apps-services-net8/issues) or email me at markjprice (at) gmail.com.

- [Page 104 - Table-per-concrete-type (TPC) mapping strategy](#page-104---table-per-concrete-type-tpc-mapping-strategy)
- [Page 166 - Implementing stored procedures](#page-166---implementing-stored-procedures)
- [Page 249 - Creating a console app to generate PDF documents](#page-249---creating-a-console-app-to-generate-pdf-documents)
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

In Step 6, I show the code to generate a PDF file but it includes a statement to set the license type. The `License` property and `enum` type are only available with package versions. If you use the 2022.12.3 or earlier versions with MIT license then you do not need to set the license. 

The following statement can be commented out:
```cs
QuestPDF.Settings.License = LicenseType.Community;
```

In the next edition, I will add a note to explain this. I have commented out this statement in the GitHub solution since it references the older package.

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
