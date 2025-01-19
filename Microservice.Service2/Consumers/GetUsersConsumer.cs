using MassTransit;
using Microservice.Service2;
using Microservices.Common.Contracts;

namespace Microservices.Service2.Consumers;

public class GetUsersConsumer(SingltoneDatabase db) : IConsumer<GetUsersEvent>
{
    public Task Consume(ConsumeContext<GetUsersEvent> context)
    {
        db.GetUsersEvents.Add(context.Message);
        return Task.CompletedTask;
    }
}
