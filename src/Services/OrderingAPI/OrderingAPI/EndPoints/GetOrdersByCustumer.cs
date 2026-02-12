using Ordering.Application.Orders.Queries.GetOrderByCustumer;
using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace OrderingAPI.EndPoints;

public record GetOrdersByCustumerRequest(Guid CustumerId);
public record GetOrdersByCustumerResponse(IEnumerable<OrderDto> Orders);
public class GetOrdersByCustumer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/custumer/{CustumerId}", async (Guid CustumerId, ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersByCustumerQuery(CustumerId));
            var response = result.Adapt<GetOrdersByCustumerResponse>();
            return Results.Ok(response);
        })
        .WithName("GetOrdersByCustumer")
        .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Orders by Custumer")
        .WithDescription("Get Orders by Custumer");
    }
}
