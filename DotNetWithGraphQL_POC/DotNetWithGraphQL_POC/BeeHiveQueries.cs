using Microsoft.EntityFrameworkCore;

namespace DotNetWithGraphQL_POC;

public class BeeHiveQueries
{
    public IQueryable<Bee> GetBees(Context context) => context.Set<Bee>().AsNoTracking();
    public IQueryable<Hive> GetHives(Context context) => context.Set<Hive>().AsNoTracking();
}
public class BeeHiveQueriesType : ObjectType<BeeHiveQueries>
{
    protected override void Configure(IObjectTypeDescriptor<BeeHiveQueries> descriptor)
    {
        descriptor
            .Field(f => f.GetBees(default!))
            .Type<ListType<NonNullType<BeeType>>>()
            .UseProjection()
            .UseFiltering();

        descriptor
            .Field(f => f.GetHives(default!))
            .Type<ListType<NonNullType<HiveType>>>()
            .UseProjection()
            .UseFiltering();
    }
}

