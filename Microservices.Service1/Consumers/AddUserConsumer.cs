using MassTransit;
using Microservices.Common.Contracts;

namespace Microservices.Service1.Consumers;

public class AddUserConsumer(ILogger<AddUserConsumer> logger) : IConsumer<AddUserEvent>
{
    public Task Consume(ConsumeContext<AddUserEvent> context)
    {
        logger.LogInformation("{firstname} - {surname} was added at {datetime}", 
            context.Message.Firstname, 
            context.Message.Lastname, 
            context.Message.DateTime);

        return Task.CompletedTask;
    }
}
