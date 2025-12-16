
namespace BasketAPI.Basket.StoreBasket;

public record StoreBasketRequest(ShoppingCar Cart);
public record StoreBasketResponse(string UserName);

public class StoreBasketValidator : AbstractValidator<StoreBasketRequest>
{
    public StoreBasketValidator()
    {
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("User name required");
    }
}
public class StoreBasketEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
        {
            var command = request.Adapt<StoreBasketCommand>();
            var result = await sender.Send(command);

            var response = result.Adapt<StoreBasketResponse>();

            return Results.Created($"/basket/{response.UserName}", response);

        }).WithName("StoreBasket")
        .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Stores a shopping cart for a user")
        .WithDescription("Stores a shopping cart for a user");
    }
}
