await CreateCosmosGraphResources();

SectionTitle("Gremlin Server details:");
WriteLine($"  Uri:      {gremlinServer.Uri}");
WriteLine($"  Username: {gremlinServer.Username}");
WriteLine($"  Password: {gremlinServer.Password}");

await CreateProductVertices();
await CreateCustomerVertices();
