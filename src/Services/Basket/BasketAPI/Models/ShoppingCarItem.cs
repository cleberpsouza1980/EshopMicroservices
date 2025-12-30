namespace BasketAPI.Models
{
    public class ShoppingCarItem
    {
        public string Id { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public int Quantity { get; set; } = default!;
        public string Color { get; set; } = default!;
        public double Price { get; set; } = default!;
        public double DiscountValue { get; set; } = 0;
        public Guid ProductId { get; set; } = default !;
    }
}
