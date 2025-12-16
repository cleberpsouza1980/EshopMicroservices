

namespace BasketAPI.Data;

public class BasketRepository(IDocumentSession session) : IBasketRepository
{
    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        session.Delete<ShoppingCar>(userName);
        return await session.SaveChangesAsync(cancellationToken).ContinueWith(t => true, cancellationToken);
    }

    public async Task<ShoppingCar> GetBasket(string userName, CancellationToken cancellationToken = default)
    {
        var basket = await session.LoadAsync<ShoppingCar>(userName, cancellationToken);

        return basket is null ? throw new BasketNotFoundException(userName) : basket;
    }

    public async Task<ShoppingCar> StoreBasket(ShoppingCar basket, CancellationToken cancellationToken = default)
    {
        session.Store(basket);
        await session.SaveChangesAsync(cancellationToken);

        return basket;
    }
}
