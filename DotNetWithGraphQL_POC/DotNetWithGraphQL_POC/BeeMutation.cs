using HotChocolate.Subscriptions;

namespace DotNetWithGraphQL_POC;

public class BeeHiveMutation
{
    public async Task<BeeAddedPayload> AddBee(Context context, [Service] ITopicEventSender sender, AddBeeInput beeInput)
    {
        var newBee = new Bee { Id = beeInput.Id, Age = beeInput.Age, HiveId = beeInput.Hive, IsQueen = beeInput.IsQueen };
        context.Add(newBee);
        context.SaveChanges();

        await sender.SendAsync(nameof(BeeHiveSubscription.BeeAdded), newBee);

        return new BeeAddedPayload(true);
    }
}

public record BeeAddedPayload(bool BeeAdded);
public record AddBeeInput(int Id, int Age, int Hive, bool IsQueen);

public class BeeHiveMutationType : ObjectType<BeeHiveMutation>
{
    protected override void Configure(
        IObjectTypeDescriptor<BeeHiveMutation> descriptor)
    {
        descriptor.Field(f => f.AddBee(default, default, default));
    }
}
