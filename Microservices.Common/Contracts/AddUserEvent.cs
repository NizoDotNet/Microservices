namespace Microservices.Common.Contracts;

public class AddUserEvent
{
    public AddUserEvent()
    {
    }

    public AddUserEvent(string firstname, string lastname)
    {
        Firstname = firstname;
        Lastname = lastname;
    }

    public string Firstname { get; set; } = null!;
    public string Lastname { get; set; } = null;
    public DateTime DateTime { get;} = DateTime.UtcNow;
}
