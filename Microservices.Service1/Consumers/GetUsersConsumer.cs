using MassTransit;
using Microservices.Common.Contracts;

namespace Microservices.Service1.Consumers;

public class GetUsersConsumer(ILogger<GetUsersConsumer> logger) : IConsumer<GetUsersEvent>
{
    public Task Consume(ConsumeContext<GetUsersEvent> context)
    {
        logger.LogInformation("{dateTime}", context.Message.DateTime);
        return Task.CompletedTask;
    }
}
