
using JasperFx.Events.Daemon;

namespace BasketAPI.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCar Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);

public class StoreCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart cant not be null");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("User name required");
        RuleFor(x => x.Cart.Items).NotNull().WithMessage("Items not found");
    }
}
public class StoreBasketHandler(IBasketRepository repository, DiscontGRP.DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        //ShoppingCar cart = command.Cart;
        await DeductDiscount(command.Cart, cancellationToken);

        await repository.StoreBasket(command.Cart, cancellationToken);

        return new StoreBasketResult(command.Cart.UserName);
    }

    private async Task DeductDiscount(ShoppingCar cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            var coupon = await discountProtoServiceClient.GetDiscountAsync(new DiscontGRP.GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);

            if (coupon != null)
            {
                item.DiscountValue = coupon.Amount;
                item.Price -= coupon.Amount;
            }
        }
    }
}
