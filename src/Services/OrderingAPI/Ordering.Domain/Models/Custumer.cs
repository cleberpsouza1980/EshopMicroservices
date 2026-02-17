namespace Ordering.Domain.Models;

public class Custumer:Entity<CustumerId>
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;


    public static Custumer Create(CustumerId id, string name, string email)
    {
        ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
        ArgumentException.ThrowIfNullOrEmpty(email, nameof(email));

        var custumer = new Custumer
        {
            Id = id,
            Name = name,
            Email = email,            
        };
        return custumer;
    }
}
