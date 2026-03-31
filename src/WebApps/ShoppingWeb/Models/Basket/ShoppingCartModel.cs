namespace ShoppingWeb.Models.Basket
{
    public class ShoppingCartModel
    {        
        public string UserName { get; set; } = default!;
        public List<ShoppingCarItem> Items { get; set; } = new();
        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
    }

    public class ShoppingCarItem
    {        
        public string ProductName { get; set; } = default!;
        public int Quantity { get; set; } = default!;
        public string Color { get; set; } = default!;
        public decimal Price { get; set; } = default!;     
        public Guid ProductId { get; set; } = default!;
    }

    public record GetBasketResponse(ShoppingCartModel Cart);
    public record StoreBasketRequest(ShoppingCartModel Cart);
    public record StoreBasketResponse(string userName);
    public record DeleteBasketResponse(bool IsSuccess);
}
