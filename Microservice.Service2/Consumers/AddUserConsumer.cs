using MassTransit;
using Microservice.Service2;
using Microservices.Common.Contracts;

namespace Microservices.Service2.Consumers;

public class AddUserConsumer(SingltoneDatabase db) : IConsumer<AddUserEvent>
{
    public Task Consume(ConsumeContext<AddUserEvent> context)
    {
        db.AddUserEvents.Add(context.Message);

        return Task.CompletedTask;
    }
}
