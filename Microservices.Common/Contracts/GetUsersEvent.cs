namespace Microservices.Common.Contracts;

public class GetUsersEvent
{
    public DateTime DateTime { get; } = DateTime.UtcNow;
}
