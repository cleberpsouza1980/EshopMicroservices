
namespace Ordering.Domain.Models;

public class Order : Aggregate<Guid>
{
    private readonly List<OrderItem> orderItems = new();

    public IReadOnlyList<OrderItem> OrderItems => orderItems.AsReadOnly();

    public Guid CustumerId { get;private set; } = default!;
    public string OrderName { get;private set; } = default!;

    public Adress ShippingAdress { get; private set; } = default!;
    public Adress BillingAdress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public OrderStatus Status  { get; private set; } = OrderStatus.Pending;
    public decimal TotalPrice 
    { 
        get 
        {
            return orderItems.Sum(item => item.Price * item.Quantity);
        }
        private set { }
    }
}
