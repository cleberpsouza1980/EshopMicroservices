namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrdersHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);
            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;

            var orders = await dbContext.Orders                
                .Include(o => o.OrderItems)
                .OrderByDescending(o => o.OrderName.Value)
                .Skip(pageIndex * pageSize )
                .Take(pageSize)                
                .ToListAsync(cancellationToken);


            return new GetOrdersResult(new PaginationResult<OrderDto>(
                pageIndex,
                pageSize,
                totalCount,
                orders.ProjectToOrderDto().ToList()));
        }
    }
}
