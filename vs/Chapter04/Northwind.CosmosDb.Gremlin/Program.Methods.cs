// CosmosClient, DatabaseResponse, Database, IndexingPolicy, and so on.
using Microsoft.Azure.Cosmos;

using Packt.Shared; // NorthwindContext, Product, Customer, and so on.
using System.Net; // HttpStatusCode

using Gremlin.Net.Driver; // GremlinServer, GremlinClient, ResultSet<T>
using Gremlin.Net.Structure.IO.GraphSON; // GraphSON2Reader, GraphSON2Writer
using Newtonsoft.Json; // JsonConvert
using System.Globalization;

partial class Program
{
  private static bool useLocal = false;

  private static string RequestChargeHeader = "x-ms-request-charge";

  // To use Azure Cosmos DB in the local emulator.
  private static string endpointUriLocal = "https://localhost:8081/";
  private static string primaryKeyLocal = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

  // To use Azure Cosmos DB in the cloud.
  private static string account =
    "apps-services-net7-graph"; // change to your account

  // Change to your primary key for your cloud account.
  private static string primaryKeyCloud
    = "qeYFsBVUR77I5dJS2OhP83jjyrYUn4HuOZV8u3VkkChaluXeM0EDhRNPDUsxt6mQRe0HY5CeaM3ZR86mJtry2g=="; // change to your key

  private static string endpointUriCloud =
    $"https://{account}.documents.azure.com:443/";

  // to use Gremlin client in the local emulator
  private static string hostLocal = "localhost";
  private static int portLocal = 8081;

  // to use Gremlin client in the cloud
  private static string hostCloud =
    $"{account}.gremlin.cosmos.azure.com";

  private static int portCloud = 443;

  // Common names.
  private static string database = "NorthwindGraphDb";
  private static string collection = "CustomerProductViews";

  public static GremlinServer gremlinServer = new(
    hostname: useLocal ? hostLocal : hostCloud,
    port: useLocal ? portLocal : portCloud,
    enableSsl: true,
    username: $"/dbs/" + database + "/colls/" + collection,
    password: useLocal ? primaryKeyLocal : primaryKeyCloud);

  static async Task<(int, double)> ExecuteGremlinScript(string script)
  {
    int affected = 0;
    double requestChargeTotal = 0.0;

    try
    {
      using (GremlinClient gremlinClient = new(
        gremlinServer,
        new GraphSON2Reader(),
        new GraphSON2Writer(),
        GremlinClient.GraphSON2MimeType))
      {
        SectionTitle("Gremlin request script");
        WriteLine(script);

        ResultSet<dynamic> resultSet = await gremlinClient
          .SubmitAsync<dynamic>(script);

        affected = resultSet.Count;

        if (double.TryParse(resultSet.StatusAttributes[
          RequestChargeHeader].ToString(), out double requestCharge))
        {
          requestChargeTotal += requestCharge;
        }

        if (affected > 0)
        {
          SectionTitle($"{affected} matches:");
          foreach (dynamic result in resultSet)
          {
            string jsonOutput = JsonConvert.SerializeObject(result);
            WriteLine(jsonOutput);
          }
        }
      }
    }
    catch (Exception ex)
    {
      WriteLine("{0} says {1}", ex.GetType(), ex.Message);
    }

    return (affected, requestChargeTotal);
  }

  static async Task CreateCosmosGraphResources()
  {
    SectionTitle("Creating Cosmos graph resources");

    try
    {
      using (CosmosClient client = new(
        accountEndpoint: useLocal ? endpointUriLocal : endpointUriCloud,
        authKeyOrResourceToken: useLocal ? endpointUriLocal : primaryKeyCloud))
      {
        SectionTitle("CosmosClient details:");
        WriteLine($"  Uri: {client.Endpoint}");

        DatabaseResponse dbResponse = await client
          .CreateDatabaseIfNotExistsAsync(
            database, throughput: 400 /* RU/s */);

        string status = dbResponse.StatusCode switch
        {
          HttpStatusCode.OK => "exists",
          HttpStatusCode.Created => "created",
          _ => "unknown",
        };

        WriteLine("Database Id: {0}, Status: {1}.",
          arg0: dbResponse.Database.Id, arg1: status);

        IndexingPolicy indexingPolicy = new()
        {
          IndexingMode = IndexingMode.Consistent,
          Automatic = true, // items are indexed unless explicitly excluded
          IncludedPaths = { new IncludedPath { Path = "/*" } }
        };

        ContainerProperties containerProperties = new(collection,
          partitionKeyPath: "/partitionKey")
        {
          IndexingPolicy = indexingPolicy
        };

        ContainerResponse containerResponse = await dbResponse.Database
          .CreateContainerIfNotExistsAsync(
            containerProperties, throughput: 1000 /* RU/s */);

        status = dbResponse.StatusCode switch
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
      }
    }
    catch (HttpRequestException ex)
    {
      WriteLine("Error: {0}", arg0: ex.Message);
      WriteLine("Hint: Make sure the Azure Cosmos Emulator is running.");
    }
    catch (Exception ex)
    {
      WriteLine("Error: {0} says {1}",
        arg0: ex.GetType(),
        arg1: ex.Message);
    }
  }

  static async Task CreateProductVertices()
  {
    double totalCharge = 0.0;

    try
    {
      using (NorthwindContext db = new())
      {
        SectionTitle("Creating product vertices");

        foreach (Product p in db.Products)
        {
          string getProduct = $"""
              g.V().hasLabel("product")
                .has("productId", {p.ProductId})
              """;

          (int Count, double Cost) found =
            await ExecuteGremlinScript(getProduct);

          totalCharge += found.Cost;

          if (found.Count == 0)
          {
            WriteLine("Product {0} not found. Adding its vertex...",
              p.ProductName);

            string addProduct = $"""
              g.addV("product")
                .property("partitionKey", {p.ProductId})
                .property("productId", {p.ProductId})
                .property("productName", "{p.ProductName}")
                .property("quantityPerUnit", "{p.QuantityPerUnit}")
                .property("unitPrice", {p.UnitPrice?.ToString(CultureInfo.InvariantCulture) ?? 0})
                .property("unitsInStock", {p.UnitsInStock ?? 0})
                .property("reorderLevel", {p.ReorderLevel ?? 0})
                .property("unitsOnOrder", {p.UnitsOnOrder ?? 0})
                .property("discontinued", {p.Discontinued.ToString().ToLower()})
              """;

            (int Count, double Cost) added =
              await ExecuteGremlinScript(addProduct);

            totalCharge += added.Cost;

            WriteLine("{0} vertex was added at a cost of {1} RUs.",
              added.Count, added.Cost);
          }
          else
          {
            WriteLine($"Product {p.ProductId} already exists.");
          }
        }
      }
    }
    catch (Exception ex)
    {
      OutputException(ex);
    }

    WriteLine("Total requests charge: {0:N2} RUs", totalCharge);
  }

  static async Task CreateCustomerVertices()
  {
    double totalCharge = 0.0;

    try
    {
      using (NorthwindContext db = new())
      {
        SectionTitle("Creating customer vertices");

        foreach (Customer c in db.Customers)
        {
          string getCustomer = $"""
              g.V().hasLabel("customer")
                .has("customerId", "{c.CustomerId}")
              """;

          (int Count, double Cost) found =
            await ExecuteGremlinScript(getCustomer);

          totalCharge += found.Cost;

          if (found.Count == 0)
          {
            WriteLine("Customer {0} not found. Adding its vertex...",
              c.CompanyName);

            string addCustomer = $"""
              g.addV("customer")
                .property("partitionKey", "{c.CustomerId}")
                .property("customerId", "{c.CustomerId}")
                .property("companyName", "{c.CompanyName}")
                .property("contactName", "{c.ContactName}")
                .property("contactTitle", "{c.ContactTitle}")
                .property("address", "{c.Address}")
                .property("city", "{c.City}")
                .property("region", "{c.Region}")
                .property("postalCode", "{c.PostalCode}")
                .property("country", "{c.Country}")
                .property("phone", "{c.Phone}")
                .property("fax", "{c.Fax}")
              """;

            (int Count, double Cost) added =
              await ExecuteGremlinScript(addCustomer);

            totalCharge += added.Cost;

            WriteLine("{0} vertex was added at a cost of {1} RUs.",
              added.Count, added.Cost);
          }
          else
          {
            WriteLine($"Customer {c.CustomerId} already exists.");
          }
        }
      }
    }
    catch (Exception ex)
    {
      OutputException(ex);
    }

    WriteLine("Total requests charge: {0:N2} RUs", totalCharge);
  }
}
