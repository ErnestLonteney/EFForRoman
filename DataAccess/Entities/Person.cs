namespace DataAccess.Entities;

public abstract class Person
{
    protected Person(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    protected Person()
    {
            
    }

    public Guid Id { get; private set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateOnly DateOfBirth { get; set; }   

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public Address? Address { get; set; }
}
