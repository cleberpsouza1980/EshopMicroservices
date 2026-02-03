using Ordering.Domain.Enumns;

namespace Ordering.Application.Dtos;

public record OrderDto(
    Guid Id,
    Guid CustumerId,
    string OrderName,
    AddressDto ShippingAddress,
    AddressDto BillingAddress,
    PaymentDto Payment,
    OrderStatus Status,
    List<OrderItemDto> OrderItems
    );