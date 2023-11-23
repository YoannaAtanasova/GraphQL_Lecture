namespace DotNetWithGraphQL_POC;

public class Hive
{
    public int Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Bee> Bees { get; set; }
}

public class HiveType : ObjectType<Hive>
{
    protected override void Configure(IObjectTypeDescriptor<Hive> descriptor)
    {
        descriptor.Field(f => f.Name).Description("Bad ass hive name");
    }
}

public class Bee
{
    public int Id { get; set; }
    public int Age { get; set; }

    public bool IsQueen { get; set; }

    public int HiveId { get; set; }

    public virtual Hive Hive { get; set; }
}

public class BeeType : ObjectType<Bee>
{
    protected override void Configure(IObjectTypeDescriptor<Bee> descriptor)
    {
        descriptor.Field(f => f.Age).Description("Bee age");
    }
}
