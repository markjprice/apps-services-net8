using Northwind.GraphQL.Service; // To use Query.
using Northwind.EntityModels; // To use AddNorthwindContext method.

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddNorthwindContext();

builder.Services
  .AddGraphQLServer()
  .AddFiltering()
  .AddSorting()
  .AddSubscriptionType<Subscription>()
  .AddInMemorySubscriptions()
  .RegisterDbContext<NorthwindContext>()
  .AddQueryType<Query>()
  .AddMutationType<Mutation>();

var app = builder.Build();

app.UseWebSockets(); // For subscriptions.

app.MapGet("/", () => "Navigate to: https://localhost:5121/graphql");

app.MapGraphQL();

app.Run();
