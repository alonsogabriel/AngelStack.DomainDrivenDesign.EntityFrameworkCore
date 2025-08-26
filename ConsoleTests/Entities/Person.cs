using AngelStack.DomainDrivenDesign.Entities;
using AngelStack.DomainDrivenDesign.ValueObjects;

namespace ConsoleTests.Entities;

public class Person : AbstractPerson
{
    private Person() { }
    public Person(PersonName name, DateOfBirth? dateOfBirth, Gender? gender, City? placeOfBirth, Address? address)
        : base(name, dateOfBirth, gender, placeOfBirth)
    {
        AddressId = address?.Id;
        Address = address;
    }

    public Guid? AddressId { get; private set; }
    public Address? Address { get; private set; }
}