namespace Microservices.Service1.Data.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Firstname { get; set; } = null!;
    public string Surname { get; set; } = null!;
}
