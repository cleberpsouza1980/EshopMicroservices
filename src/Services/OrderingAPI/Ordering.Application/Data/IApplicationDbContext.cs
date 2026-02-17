namespace Ordering.Application.Data;

public interface IApplicationDbContext
{
    DbSet<Custumer> Custumers { get;  }
    DbSet<Product> Products { get;  }
    DbSet<Order> Orders { get;  }
    DbSet<OrderItem> OrderItems { get;  }    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
