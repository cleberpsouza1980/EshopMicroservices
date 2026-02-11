namespace Ordering.Application.Orders.Queries.GetOrderByCustumer;

public record GetOrdersByCustumerQuery(Guid CustumerId) : IQuery<GetOrdersByCustumerResult>;

public record GetOrdersByCustumerResult(IEnumerable<OrderDto> Orders);