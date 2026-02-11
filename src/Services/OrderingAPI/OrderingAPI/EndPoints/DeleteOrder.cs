
namespace OrderingAPI.EndPoints;

public record DeleteOrderRequest(Guid Id);
public record DeleteOrderResponse(bool IsSuccess);
public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{id}", async (Guid Id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteOrderCommand(Id));
            var response = result.Adapt<DeleteOrderResponse>();
            if (response.IsSuccess)
            {
                return Results.Ok(response);
            }
            else
            {
                return Results.NotFound();
            }
        }).WithName("DeleteOrder")
        .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete a Order")
        .WithDescription("Delete Order.");
    }
}
