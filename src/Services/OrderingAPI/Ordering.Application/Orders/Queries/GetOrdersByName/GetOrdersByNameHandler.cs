
using Ordering.Application.OrderExtensions;

namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public class GetOrdersByNameHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {

        //get orders by name usgin dbcintext
        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(o => o.OrderName.ToString().ToLower().Contains(query.Name.ToLower()))
            .OrderBy(o => o.OrderName)
            .ToListAsync(cancellationToken);

        //result
        //var orderDtos = ProjectToOrderDto(orders);
        return new GetOrdersByNameResult(orders.ProjectToOrderDto());
    }   
}
