using Microservices.Common.Contracts;

namespace Microservice.Service2;

public class SingeltoneDatabase
{
    public List<AddUserEvent> AddUserEvents { get; } = [];
    public List<GetUsersEvent> GetUsersEvents { get; } = [];
}
