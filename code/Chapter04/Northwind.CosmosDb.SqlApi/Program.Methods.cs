using Microsoft.Azure.Cosmos; // To use CosmosClient and so on.
using System.Net; // To use HttpStatusCode.
using Northwind.EntityModels; // To use NorthwindContext and so on.
using Northwind.CosmosDb.Items; // To use ProductCosmos and so on.
using Microsoft.EntityFrameworkCore; // To use Include extension method.
using Microsoft.Azure.Cosmos.Scripts; // To use StoredProcedureResponse and so on.
using Azure;
using System.Xml;

// This is defined in the default empty namespace, so it merges with
// the SDK-generated partial Program class.

partial class Program
{
  // To use Azure Cosmos DB in the local emulator.
  private static string endpointUri = "https://localhost:8081/";
  private static string primaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

  /*
  // To use Azure Cosmos DB in the cloud.
  private static string account = "apps-services-book"; // use your account
  private static string endpointUri =
    $"https://{account}.documents.azure.com:443/";
  private static string primaryKey = "xN05xvMzv9ig8aNseFmMLCEGFE5ScZdW9xbDcfnJgsmZjvTUQFsBZaZwzcrl3pCij5udbBKm3fGtACDbJfJMzg=="; // use your key
  */

  static async Task CreateCosmosResources()
  {
    SectionTitle("Creating Cosmos resources");

    try
    {
      using (CosmosClient client = new(
        accountEndpoint: endpointUri,
        authKeyOrResourceToken: primaryKey))
      {
        DatabaseResponse dbResponse = await client
          .CreateDatabaseIfNotExistsAsync(
            "Northwind", throughput: 400 /* RU/s */);

        string status = dbResponse.StatusCode switch
        {
          HttpStatusCode.OK => "exists",
          HttpStatusCode.Created => "created",
          _ => "unknown"
        };

        WriteLine("Database Id: {0}, Status: {1}.",
          arg0: dbResponse.Database.Id, arg1: status);

        IndexingPolicy indexingPolicy = new()
        {
          IndexingMode = IndexingMode.Consistent,
          Automatic = true, // items are indexed unless explicitly excluded
          IncludedPaths = { new IncludedPath { Path = "/*" } }
        };

        ContainerProperties containerProperties = new("Products",
          partitionKeyPath: "/productId")
        {
          IndexingPolicy = indexingPolicy
        };

        ContainerResponse containerResponse = await dbResponse.Database
          .CreateContainerIfNotExistsAsync(
            containerProperties, throughput: 1000 /* RU/s */);

        status = containerResponse.StatusCode switch
        {
          HttpStatusCode.OK => "exists",
          HttpStatusCode.Created => "created",
          _ => "unknown",
        };

        WriteLine("Container Id: {0}, Status: {1}.",
          arg0: containerResponse.Container.Id, arg1: status);

        Container container = containerResponse.Container;

        ContainerProperties properties = await container.ReadContainerAsync();
        WriteLine($"  PartitionKeyPath: {properties.PartitionKeyPath}");
        WriteLine($"  LastModified: {properties.LastModified}");
        WriteLine("  IndexingPolicy.IndexingMode: {0}",
          arg0: properties.IndexingPolicy.IndexingMode);
        WriteLine("  IndexingPolicy.IncludedPaths: {0}",
          arg0: string.Join(",", properties.IndexingPolicy
            .IncludedPaths.Select(path => path.Path)));
        WriteLine($"  IndexingPolicy: {properties.IndexingPolicy}");
      }
    }
    catch (HttpRequestException ex)
    {
      WriteLine($"Error: {ex.Message}");
      WriteLine("Hint: If you are using the Azure Cosmos Emulator then please make sure it is running.");
    }
    catch (Exception ex)
    {
      WriteLine("Error: {0} says {1}",
        arg0: ex.GetType(),
        arg1: ex.Message);
    }
  }

  static async Task CreateProductItems()
  {
    SectionTitle("Creating product items");

    double totalCharge = 0.0;

    try
    {
      using (CosmosClient client = new(
        accountEndpoint: endpointUri,
        authKeyOrResourceToken: primaryKey))
      {
        Container container = client.GetContainer(
          databaseId: "Northwind", containerId: "Products");

        using (NorthwindContext db = new())
        {
          if (!db.Database.CanConnect())
          {
            WriteLine("Cannot connect to the SQL Server database to " + 
              " read products using database connection string: " +
              db.Database.GetConnectionString());
            return;
          }

          ProductCosmos[] products = db.Products

            // Get the related data for embedding.
            .Include(p => p.Category)
            .Include(p => p.Supplier)

            // Filter any products with null category or supplier
            // to avoid null warnings.
            .Where(p => (p.Category != null) && (p.Supplier != null))

            // Project the EF Core entities into Cosmos JSON types.
            .Select(p => new ProductCosmos
            {
              id = p.ProductId.ToString(),
              productId = p.ProductId.ToString(),
              productName = p.ProductName,
              quantityPerUnit = p.QuantityPerUnit,

              // If the related category is null, store null,
              // else store the category mapped to Cosmos model.
              category = p.Category == null ? null :
                new CategoryCosmos
                {
                  categoryId = p.Category.CategoryId,
                  categoryName = p.Category.CategoryName,
                  description = p.Category.Description
                },

              supplier = p.Supplier == null ? null :
                new SupplierCosmos
                {
                  supplierId = p.Supplier.SupplierId,
                  companyName = p.Supplier.CompanyName,
                  contactName = p.Supplier.ContactName,
                  contactTitle = p.Supplier.ContactTitle,
                  address = p.Supplier.Address,
                  city = p.Supplier.City,
                  country = p.Supplier.Country,
                  postalCode = p.Supplier.PostalCode,
                  region = p.Supplier.Region,
                  phone = p.Supplier.Phone,
                  fax = p.Supplier.Fax,
                  homePage = p.Supplier.HomePage
                },

              unitPrice = p.UnitPrice,
              unitsInStock = p.UnitsInStock,
              reorderLevel = p.ReorderLevel,
              unitsOnOrder = p.UnitsOnOrder,
              discontinued = p.Discontinued,
            })
            .ToArray();

          foreach (ProductCosmos product in products)
          {
            try
            {
              // Try to read the item to see if it exists.
              ItemResponse<ProductCosmos> productResponse =
                await container.ReadItemAsync<ProductCosmos>(
                id: product.id, new PartitionKey(product.productId));

              WriteLine("Item with id: {0} exists. Query consumed {1} RUs.",
                productResponse.Resource.id, productResponse.RequestCharge);

              totalCharge += productResponse.RequestCharge;
            }
            catch (CosmosException ex)
              when (ex.StatusCode == HttpStatusCode.NotFound)
            {
              // Create the item if it does not exist.
              ItemResponse<ProductCosmos> productResponse =
                await container.CreateItemAsync(product);

              WriteLine("Created item with id: {0}. Insert consumed {1} RUs.",
                productResponse.Resource.id, productResponse.RequestCharge);

              totalCharge += productResponse.RequestCharge;
            }
            catch (Exception ex)
            {
              WriteLine("Error: {0} says {1}",
                arg0: ex.GetType(),
                arg1: ex.Message);
            }
          }
        }
      }
    }
    catch (HttpRequestException ex)
    {
      WriteLine($"Error: {ex.Message}");
      WriteLine("Hint: If you are using the Azure Cosmos Emulator then please make sure it is running.");
    }
    catch (Exception ex)
    {
      WriteLine("Error: {0} says {1}",
        arg0: ex.GetType(),
        arg1: ex.Message);
    }

    WriteLine("Total requests charge: {0:N2} RUs", totalCharge);
  }

  static async Task ListProductItems(string sqlText = "SELECT * FROM c")
  {
    SectionTitle("Listing product items");

    try
    {
      using (CosmosClient client = new(
        accountEndpoint: endpointUri,
        authKeyOrResourceToken: primaryKey))
      {
        Container container = client.GetContainer(
          databaseId: "Northwind", containerId: "Products");

        WriteLine("Running query: {0}", sqlText);

        QueryDefinition query = new(sqlText);

        using FeedIterator<ProductCosmos> resultsIterator =
          container.GetItemQueryIterator<ProductCosmos>(query);

        if (!resultsIterator.HasMoreResults)
        {
          WriteLine("No results found.");
        }

        while (resultsIterator.HasMoreResults)
        {
          FeedResponse<ProductCosmos> products =
            await resultsIterator.ReadNextAsync();

          WriteLine("Status code: {0}, Request charge: {1} RUs.",
            products.StatusCode, products.RequestCharge);

          WriteLine($"{products.Count} products found.");

          foreach (ProductCosmos product in products)
          {
            WriteLine("id: {0}, productName: {1}, unitPrice: {2:C}",
              arg0: product.id, arg1: product.productName,
              arg2: product.unitPrice.ToString());
          }
        }
      }
    }
    catch (HttpRequestException ex)
    {
      WriteLine($"Error: {ex.Message}");
      WriteLine("Hint: If you are using the Azure Cosmos Emulator then please make sure it is running.");
    }
    catch (Exception ex)
    {
      WriteLine("Error: {0} says {1}",
        arg0: ex.GetType(),
        arg1: ex.Message);
    }
  }

  static async Task DeleteProductItems()
  {
    SectionTitle("Deleting product items");

    double totalCharge = 0.0;

    try
    {
      using (CosmosClient client = new(
        accountEndpoint: endpointUri,
        authKeyOrResourceToken: primaryKey))
      {
        Container container = client.GetContainer(
          databaseId: "Northwind", containerId: "Products");

        string sqlText = "SELECT * FROM c";

        WriteLine("Running query: {0}", sqlText);

        QueryDefinition query = new(sqlText);

        using FeedIterator<ProductCosmos> resultsIterator =
          container.GetItemQueryIterator<ProductCosmos>(query);

        while (resultsIterator.HasMoreResults)
        {
          FeedResponse<ProductCosmos> products =
            await resultsIterator.ReadNextAsync();

          foreach (ProductCosmos product in products)
          {
            WriteLine("Delete id: {0}, productName: {1}",
              arg0: product.id, arg1: product.productName);

            ItemResponse<ProductCosmos> response =
              await container.DeleteItemAsync<ProductCosmos>(
              id: product.id, partitionKey: new(product.id));

            WriteLine("Status code: {0}, Request charge: {1} RUs.",
              response.StatusCode, response.RequestCharge);

            totalCharge += response.RequestCharge;
          }
        }
      }
    }
    catch (HttpRequestException ex)
    {
      WriteLine($"Error: {ex.Message}");
      WriteLine("Hint: If you are using the Azure Cosmos Emulator then please make sure it is running.");
    }
    catch (Exception ex)
    {
      WriteLine("Error: {0} says {1}",
        arg0: ex.GetType(),
        arg1: ex.Message);
    }

    WriteLine("Total requests charge: {0:N2} RUs", totalCharge);
  }

  static async Task CreateInsertProductStoredProcedure()
  {
    SectionTitle("Creating the insertProduct stored procedure");

    try
    {
      using (CosmosClient client = new(
        accountEndpoint: endpointUri,
        authKeyOrResourceToken: primaryKey))
      {
        Container container = client.GetContainer(
          databaseId: "Northwind", containerId: "Products");

        StoredProcedureResponse response = await container
          .Scripts.CreateStoredProcedureAsync(new StoredProcedureProperties
          {
            Id = "insertProduct",
            Body = """
function insertProduct(product) {
  if (!product) throw new Error(
    "product is undefined or null.");

  tryInsert(product, callbackInsert);

  function tryInsert(product, callbackFunction) {
    var options = { disableAutomaticIdGeneration: false };

    // __ is an alias for getContext().getCollection()
    var isAccepted = __.createDocument(
      __.getSelfLink(), product, options, callbackFunction);

    if (!isAccepted) 
      getContext().getResponse().setBody(0);
  }

  function callbackInsert(err, item, options) {
    if (err) throw err;
    getContext().getResponse().setBody(1);
  }
}
"""
          });

        WriteLine("Status code: {0}, Request charge: {1} RUs.",
          response.StatusCode, response.RequestCharge);
      }
    }
    catch (HttpRequestException ex)
    {
      WriteLine($"Error: {ex.Message}");
      WriteLine("Hint: If you are using the Azure Cosmos Emulator then please make sure it is running.");
    }
    catch (Exception ex)
    {
      WriteLine("Error: {0} says {1}",
        arg0: ex.GetType(),
        arg1: ex.Message);
    }
  }

  static async Task ExecuteInsertProductStoredProcedure()
  {
    SectionTitle("Executing the insertProduct stored procedure");

    try
    {
      using (CosmosClient client = new(
        accountEndpoint: endpointUri,
        authKeyOrResourceToken: primaryKey))
      {
        Container container = client.GetContainer(
          databaseId: "Northwind", containerId: "Products");

        string pid = "78";

        ProductCosmos product = new()
        {
          id = pid, productId = pid,
          productName = "Barista's Chilli Jam",
          unitPrice = 12M, unitsInStock = 10
        };

        StoredProcedureExecuteResponse<string> response = await container.Scripts
          .ExecuteStoredProcedureAsync<string>("insertProduct",
          new PartitionKey(pid), new[] { product });

        WriteLine("Status code: {0}, Request charge: {1} RUs.",
          response.StatusCode, response.RequestCharge);
      }
    }
    catch (HttpRequestException ex)
    {
      WriteLine($"Error: {ex.Message}");
      WriteLine("Hint: If you are using the Azure Cosmos Emulator then please make sure it is running.");
    }
    catch (Exception ex)
    {
      WriteLine("Error: {0} says {1}",
        arg0: ex.GetType(),
        arg1: ex.Message);
    }
  }
}
