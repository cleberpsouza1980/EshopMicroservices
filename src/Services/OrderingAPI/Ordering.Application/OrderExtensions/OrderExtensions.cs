using System.Net.Mail;

namespace Ordering.Application.OrderExtensions;

public static class OrderExtensions
{
    public static IEnumerable<OrderDto> ProjectToOrderDto(this IEnumerable<Domain.Models.Order> orders)
    {
        return orders.Select(order => new OrderDto
         (
             Id: order.Id.Value,
             CustumerId: order.CustumerId.Value,
             OrderName: order.OrderName.Value,
             ShippingAddress: new AddressDto
             (
                order.ShippingAdress.FirstName,
                order.ShippingAdress.LastName,
                order.ShippingAdress.Email,
                order.ShippingAdress.AddressLine,
                order.ShippingAdress.State,
                order.ShippingAdress.ZipCode,
                order.ShippingAdress.Country
             ),
             BillingAddress: new AddressDto(
                order.BillingAdress.FirstName,
                order.BillingAdress.LastName,
                order.BillingAdress.Email,
                order.BillingAdress.AddressLine,
                order.BillingAdress.State,
                order.BillingAdress.ZipCode,
                order.BillingAdress.Country
             ),
             Payment: new PaymentDto(
               order.Payment.CardName!,
               order.Payment.CardNumber,
               order.Payment.Expiration,
               order.Payment.CVV,
               order.Payment.PaymentMethod
             ),
             Status: order.Status,
             OrderItems: order.OrderItems.Select(oi => new OrderItemDto
                (
                    oi.OrderId.Value,
                    oi.ProductId.Value,
                    oi.Quantity,
                    oi.Price
                )).ToList()
         ));
    }
}
