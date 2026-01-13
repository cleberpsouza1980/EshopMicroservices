namespace Ordering.Domain.ValueObjects;

public class Adress
{
    public string FirstName { get; } = default!;
    public string LastName { get;  } = default!;
    public string Email { get;  } = default!; 
    public string AadressLine { get;  } = default!; 
    public string Country { get;  } = default!;
    public string State { get;  } = default!;
    public string ZipCode { get;  } = default!;


    protected Adress() { }

    private Adress(string firstName, string lastName, string email, string aadressLine, string country, string state, string zipCode)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        AadressLine = aadressLine;
        Country = country;
        State = state;
        ZipCode = zipCode;
    }
    public static Adress Of(string firstName, string lastName, string email, string aadressLine, string country, string state, string zipCode)
    {
        ArgumentException.ThrowIfNullOrEmpty(email);
        ArgumentException.ThrowIfNullOrEmpty(aadressLine);
     
        return new Adress(firstName, lastName, email, aadressLine, country, state, zipCode);
    }
}
