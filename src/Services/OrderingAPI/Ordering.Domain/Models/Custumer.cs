namespace Ordering.Domain.Models;

public class Custumer:Entity<Guid>
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
}
