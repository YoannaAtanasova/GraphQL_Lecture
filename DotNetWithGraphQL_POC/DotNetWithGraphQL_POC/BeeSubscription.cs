using HotChocolate.Execution;
using HotChocolate.Subscriptions;

namespace DotNetWithGraphQL_POC;

public class BeeHiveSubscription
{
    public Bee BeeAdded([EventMessage] Bee bee) => bee;
}

public class BeeHiveSubscriptionType : ObjectType<BeeHiveSubscription>
{
    protected override void Configure(IObjectTypeDescriptor<BeeHiveSubscription> descriptor)
    {
        descriptor
            .Field(f => f.BeeAdded(default))
            .Type<BeeType>()
            .Resolve(context => context.GetEventMessage<Bee>())
            .Subscribe(async context =>
            {
                var receiver = context.Service<ITopicEventReceiver>();

                ISourceStream stream =
                    await receiver.SubscribeAsync<Bee>("BeeAdded");

                return stream;
            });
    }
}


