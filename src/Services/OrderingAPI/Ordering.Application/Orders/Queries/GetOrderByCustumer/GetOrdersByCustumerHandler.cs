namespace Ordering.Application.Orders.Queries.GetOrderByCustumer;

public class GetOrdersByCustumerHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByCustumerQuery, GetOrdersByCustumerResult>
{
    public async Task<GetOrdersByCustumerResult> Handle(GetOrdersByCustumerQuery query, CancellationToken cancellationToken)
    {
        //get orders from database
        var orders = await dbContext.Orders
             .Include(o => o.OrderItems)
             .AsNoTracking()
             .Where(o => o.CustumerId == CustumerId.Of(query.CustumerId))
             .OrderBy(o => o.OrderName)
             .ToListAsync(cancellationToken);

        //return orders
        return new GetOrdersByCustumerResult(orders.ProjectToOrderDto());
    }
}
