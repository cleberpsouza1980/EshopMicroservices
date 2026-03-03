namespace BasketAPI.Models
{
    public class ShoppingCarItem
    {
        public string Id { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public int Quantity { get; set; } = default!;
        public string Color { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public decimal DiscountValue { get; set; } = 0;
        public Guid ProductId { get; set; } = default !;
    }
}
