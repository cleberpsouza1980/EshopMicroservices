
namespace BasketAPI.Basket.StoreBasket;
public record StoreBasketCommand (ShoppingCar Cart) :ICommand<StoreBasketResult>;
public record StoreBasketResult (string UserName);

public class StoreCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart cant not be null");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("User name required");
        RuleFor(x => x.Cart.Items).NotNull().WithMessage("Items not found");
    }
}
public class StoreBasketHandler(IBasketRepository repository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCar cart = command.Cart;

        await repository.StoreBasket(command.Cart,cancellationToken);

        return new StoreBasketResult(command.Cart.UserName);
    }
}
