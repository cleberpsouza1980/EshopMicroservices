using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace BasketAPI.Basket.CheckoutBasket;

public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<CheckoutBasketResult>;
public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketCommandValidator
    : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto can't be null");
        RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty().WithMessage("UserName is required");
    }
}


public class CheckoutBasketCommandHandler(IBasketRepository repository, IPublishEndpoint publishEndpoint, ILogger<CheckoutBasketCommandHandler> logger)
    : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        //Obtem uma cesta existente com o preco total
        //Seta o preco total na BasketCheckouEvent message
        //Envia basketchecout event para o RabbitMQ usando o MassTransit
        //deleta a cesta do usuario
        try
        {
            var basket = await repository.GetBasket(command.BasketCheckoutDto.UserName, cancellationToken);
            if (basket == null)
            {
                return new CheckoutBasketResult(false);
            }

            var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvents>();
            eventMessage.TotalPrice = basket.TotalPrice;

            //await repository.StoreBasket(command.BasketCheckoutDto, basket, cancellationToken);            
            await publishEndpoint.Publish(eventMessage, cancellationToken);

            await repository.DeleteBasket(command.BasketCheckoutDto.UserName, cancellationToken);

            return new CheckoutBasketResult(true);
        }
        catch (Exception ex)
        {
            //Log exception
            logger.LogError(ex, "Error occurred while checking out basket for user {UserName}", command.BasketCheckoutDto.UserName);
            return new CheckoutBasketResult(false);
        }
    }
}

