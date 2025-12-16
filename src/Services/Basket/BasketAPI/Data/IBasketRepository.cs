namespace BasketAPI.Data
{
    public interface IBasketRepository
    {
        public Task<ShoppingCar> GetBasket(string userName,CancellationToken cancellationToken = default);
        public Task<ShoppingCar> StoreBasket(ShoppingCar basket,CancellationToken cancellationToken = default);
        public Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default);


    }
}
