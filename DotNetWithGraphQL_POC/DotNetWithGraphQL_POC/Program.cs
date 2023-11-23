using DotNetWithGraphQL_POC;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddDbContext<Context>(o => o.UseLazyLoadingProxies())
    .AddGraphQLServer()
    .RegisterDbContext<Context>()
    .AddInMemorySubscriptions()
    .AddQueryType<BeeHiveQueriesType>()
    .AddMutationType<BeeHiveMutationType>()
    .AddSubscriptionType<BeeHiveSubscriptionType>()
    .AddProjections()
    .AddFiltering();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGraphQL();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<Context>();
context.Database.EnsureCreated();

app.Run();