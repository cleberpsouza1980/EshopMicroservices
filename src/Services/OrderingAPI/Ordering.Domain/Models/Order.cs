namespace Ordering.Domain.Models;

public class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = new();

    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public CustomerId CustumerId { get; private set; } = default!;
    public OrderName OrderName { get; private set; } = default!;
    public Adress ShippingAdress { get; private set; } = default!;
    public Adress BillingAdress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public decimal TotalPrice
    {
        get
        {
            return _orderItems.Sum(item => item.Price * item.Quantity);
        }
        private set { }
    }

    //public static Order Create(OrderId id, CustumerId custumerId, OrderName orderName, Adress shippingAdress, Adress billingAdress, Payment payment, IEnumerable<OrderItem> items)
    public static Order Create(OrderId id, CustomerId custumerId, OrderName orderName, Adress shippingAdress, Adress billingAdress, Payment payment)

    {
        var order = new Order
        {
            Id = id,
            CustumerId = custumerId,
            OrderName = orderName,
            ShippingAdress = shippingAdress,
            BillingAdress = billingAdress,
            Payment = payment,
            Status = OrderStatus.Pending
        };
        //order.orderItems.AddRange(items);

        order.AddDomainEvent(new OrderCreatedEvent(order));
        return order;
    }

    public void Update(OrderName orderName, Adress shippingAddress, Adress billingAdress, Payment payment, OrderStatus orderStatus)
    {
        OrderName = orderName;
        ShippingAdress = shippingAddress;
        BillingAdress = billingAdress;
        Payment = payment;
        Status = orderStatus;

        AddDomainEvent(new OrderUpdatedEvent(this));
    }

    public void Add(ProductId productId,int quantity,decimal price)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity, nameof(quantity));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price, nameof(price));

        var orderItem = new OrderItem(Id,productId, quantity, price);
        _orderItems.Add(orderItem);
        
    }
    public void Remove(ProductId productId)
    { 
        var orderItem = _orderItems.FirstOrDefault(item => item.ProductId == productId);

        if (orderItem != null) 
        { 
            _orderItems.Remove(orderItem);
        }

    }
}
