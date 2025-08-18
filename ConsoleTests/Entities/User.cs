using AngelStack.DomainDrivenDesign.Entities;
using AngelStack.DomainDrivenDesign.ValueObjects;

namespace ConsoleTests.Entities;

public class User : AbstractUser<int>
{
    private User()
    {
    }
    public User(Username username, Email email, PhoneNumber? phoneNumber, Person person)
        : base(username, email, phoneNumber)
    {
        PersonId = person.Id;
        Person = person;
    }

    public Guid PersonId { get; private set; }
    public Person Person { get; private set; }
}
